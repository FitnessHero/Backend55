using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Entities.Models.Database;
using Entities.Models.Request;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using PwnedPasswords.Validator;
using PwnedPasswords.Client;

namespace Fams_Backend.Controllers.Auth
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private IRepositoryWrapper _repository;
		private IConfiguration _iconfiguration;
		private IPwnedPasswordsClient _pwnedPasswordsClient;
		private readonly string _passwordValidityRegex = @"^.*(?=.{8,100})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z0-9]).*$";
		private readonly string _passwordGoodEntropyRegex = @"^(?!.*(.)\1{2})(.*?){3,29}$";

		public AuthController(IConfiguration iconfiguration, IRepositoryWrapper repository, IPwnedPasswordsClient pwnedPasswordsClient)
		{
			_repository = repository;
			_iconfiguration = iconfiguration;
			_pwnedPasswordsClient = pwnedPasswordsClient;
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("/api/auth/register")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public async Task<IActionResult> Post([FromBody] Entities.Models.Request.Register register)
		{
			var users = _repository.User.FindByCondition(user => user.Email == register.Email);
			if (users.Count() != 0)
				return UnprocessableEntity(new JsonResult("User already exists"));
			var isValid = await ValidatePassword(register.Password, new List<string> { register.UserName, register.Email});
			if (isValid.Item1 == false)
				return new JsonResult(isValid.Item2);

			byte[] hash, salt;
			CreatePasswordHash(register.Password, out hash, out salt);

			_repository.User.Create(new Entities.Models.Database.User()
			{
				Email = register.Email,
				UserName = register.UserName,
				Password = hash,
				PasswordSalt = salt,
				CreatedAt = DateTime.UtcNow
			});
			_repository.Save();
			return Ok();
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("/api/auth/login")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public IActionResult Post([FromBody] Login login)
		{
			try
			{
				var users = _repository.User.FindByCondition(user => user!.Email == login.Email);

				if (users.Count() != 1)
					return Unauthorized();
				User currentUser = users.First();

				if (!VerifyPassword(login.Password, currentUser.Password, currentUser.PasswordSalt))
					return Unauthorized();

				string? issuer = _iconfiguration["Jwt:Issuer"];
				string? audience = _iconfiguration["Jwt:Audience"];
				byte[] secretKey = Encoding.UTF8.GetBytes(_iconfiguration["Jwt:Key"]);

				var signingCredentials = new SigningCredentials(
					new SymmetricSecurityKey(secretKey),
					SecurityAlgorithms.HmacSha512Signature
				);

				var subject = new ClaimsIdentity(new[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, currentUser.UserName),
					new Claim(JwtRegisteredClaimNames.Sub, currentUser.UserName)
				});

				var expires = DateTime.UtcNow.AddHours(24);

				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = subject,
					Expires = expires,
					Issuer = issuer,
					Audience = audience,
					SigningCredentials = signingCredentials
				};

				var tokenHandler = new JwtSecurityTokenHandler();
				var token = tokenHandler.CreateToken(tokenDescriptor);
				var jwtToken = tokenHandler.WriteToken(token);
				var auth = new Entities.Models.Response.Auth
				{
					Email = currentUser.Email,
					UserName = currentUser.UserName,
					Token = jwtToken
				};
				currentUser.AuthToken = jwtToken;
				_repository.User.Update(currentUser);
				_repository.Save();
				return new JsonResult(auth);
			}
			catch (Exception)
			{
				return StatusCode(500, "Internal server error");
			}
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			var hmac = new System.Security.Cryptography.HMACSHA512();
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
		}

		private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);
			var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			return computedHash.SequenceEqual(passwordHash);
		}

		private async Task<(bool, string)> ValidatePassword(string password, List<string> bannedWords)
		{
			if (string.IsNullOrEmpty(password) || Regex.Matches(password, _passwordValidityRegex).Count == 0)
			{
				return (false, "Your password must consist of 8 characters, digits or special characters and must contain at least 1 uppercase, 1 lowercase and 1 numeric value");
			}
			if (Regex.Matches(password, _passwordGoodEntropyRegex).Count == 0)
			{
				return (false, "Your password cannot repeat the same character or digit more than 3 times consecutively, please choose another");
			}
			//var badPassword = _context.LookupItem.FirstOrDefault(l => l.LookupTypeId == Consts.LookupTypeId.BadPassword && l.Description.ToLower() == password.ToLower());
			//if (badPassword != null)
			//{
			//	return (false, "Your password is on a list of easy to guess passwords, please choose another");
			//}
			foreach (string bannedWord in bannedWords.Where(a => !string.IsNullOrWhiteSpace(a)))
			{
				if (password.IndexOf(bannedWord, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					return (false, "Your password cannot contain any of your personal information");
				}
			}
			//if (!user.Password.IsNullOrEmpty() && !user.PasswordSalt.IsNullOrEmpty())
			//{
			//	// Check the user is not changing the password to the same one
			//	var hashOfPasswordUsingCurrentSettings = new SecuredPassword(password, Convert.FromBase64String(user.PasswordSalt), user.HashStrategy);
			//	if (Convert.ToBase64String(hashOfPasswordUsingCurrentSettings.Hash) == user.PasswordHash)
			//	{
			//		return (false, $"You cannot use any of your {_configuration.MaxNumberOfPreviousPasswords} previous passwords");
			//	}
			//}
			//if (user.PreviousPasswords.Count > 0)
			//{
			//	foreach (var previousPassword in user.PreviousPasswords)
			//	{
			//		var hashOfPasswordUsingPreviousSettings = new SecuredPassword(password, Convert.FromBase64String(previousPassword.Salt), previousPassword.HashStrategy);
			//		if (Convert.ToBase64String(hashOfPasswordUsingPreviousSettings.Hash) == previousPassword.Hash)
			//		{
			//			return (false, $"You cannot use any of your {_configuration.MaxNumberOfPreviousPasswords} previous passwords");
			//		}
			//	}
			//}
			var isPwned = await _pwnedPasswordsClient.HasPasswordBeenPwned(password);
			if (isPwned)
			{
				return (false, "Your password has previously been found in a data breach, please choose another");
			}
			return (true, string.Empty);
		}
	}
}
