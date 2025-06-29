using Application.DTOs;
using Application.Interfaces.Services;
using Application.UseCases.Authorization.Login;
using Application.UseCases.Authorization.Registration;
using Humanizer;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RegistrationHandler _registrationHandler;
        private readonly LoginHandler _loginHandler;

        public UserController(RegistrationHandler registrationHandler,
                              LoginHandler loginHandler)
        {
            _registrationHandler = registrationHandler;
            _loginHandler = loginHandler;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            RegistrationCommand command = new RegistrationCommand
            {
                Email = model.Email,
                Password = model.Password,
            };

            var result = await _registrationHandler.HandleAsync(command);
            if (!result.IsSuccess) return BadRequest(result.Errors);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            LoginCommand command = new LoginCommand
            {
                Email = model.Email,
                Password = model.Password,
            };

            var result = await _loginHandler.HandleAsync(command);
            if (!result.IsSuccess) return Unauthorized(result.Errors);

            return Ok(result.Value);
        }
    }
}
