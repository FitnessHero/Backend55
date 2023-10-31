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

namespace Backend.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IConfiguration _iconfiguration;

        public ForgotPasswordController(IConfiguration iconfiguration, IRepositoryWrapper repository)
        {
            _repository = repository;
            _iconfiguration = iconfiguration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/api/auth/forgot_password")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult Post(Entities.Models.Request.Register register)
        {
            throw new NotImplementedException("");
        }
    }
}
