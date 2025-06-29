using Application.Common;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases.ShortUrls.CreateShortUrl;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Authorization.Registration
{
    public class RegistrationHandler
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegistrationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> HandleAsync(RegistrationCommand command)
        {
            var user = new IdentityUser
            {
                Email = command.Email,
                UserName = command.Email
            };

            var result = await _userManager.CreateAsync(user, command.Password);
            if (!result.Succeeded) return Result.Failure(result.Errors);

            await _userManager.AddToRoleAsync(user, "User");

            return Result.Success();
        }
    }
}
