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

namespace Fams_Backend.Controllers.Auth
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CalorieController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IConfiguration _iconfiguration;

        public CalorieController(IConfiguration iconfiguration, IRepositoryWrapper repository)
        {
            _repository = repository;
            _iconfiguration = iconfiguration;
        }

        [HttpGet]
        [Route("/api/calorie/counter")]
        [Produces("application/json")]
        public IActionResult Get()
        {
            var res = _repository.CalorieCounter.FindAll();
            return Ok(new JsonResult(res));
        }
    }
}
