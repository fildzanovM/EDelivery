using AutoMapper;
using EDelivery.Data;
using EDelivery.Models;
using EDelivery.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EDelivery.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IEDeliveryRepository _repository;
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;

        public LoginController(IEDeliveryRepository repository, ILogger<LoginController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Login to EDelivery.
        /// </summary>
        /// <param name="model">Data to create the houshold from.</param>
        /// <response code="200">Succesfully logged in</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid client request");
            }

            var result = _repository.ValidateLogin(model.UserName, model.Password);

            if (result != null)
            {

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, model.UserName),
                    new Claim("MemberId", result)
                };

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

                var tokenOptions = new JwtSecurityToken
                    (
                        issuer: "http://localhost:6390",
                        audience: "http://localhost:6390",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signInCredentials
                    );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new { Token = tokenString });

            }
            else
            {
                return Unauthorized();
            }
        }

    }

}