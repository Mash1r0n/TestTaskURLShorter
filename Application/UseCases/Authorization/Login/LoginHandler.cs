using Application.Common;
using Application.DTOs;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Authorization.Login
{
    public class LoginHandler
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginHandler(UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signInManager,
                            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<Result<UserLoginResponseDto>> HandleAsync(LoginCommand command)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user == null) return Result<UserLoginResponseDto>.Failure("Account with this email does not exist");

            var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
            if (!result.Succeeded) return Result<UserLoginResponseDto>.Failure("Incorrect password");

            var token = await _tokenService.CreateToken(user);
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            return Result<UserLoginResponseDto>.Success(new UserLoginResponseDto
            {
                Token = token,
                UserId = user.Id,
                IsUserAdmin = isAdmin
            });
        }
    }
}
