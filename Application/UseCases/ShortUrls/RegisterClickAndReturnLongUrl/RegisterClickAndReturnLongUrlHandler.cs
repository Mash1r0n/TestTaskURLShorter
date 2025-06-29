using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases.ShortUrls.CreateShortUrl;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ShortUrls.RegisterClickAndReturnLongUrl
{
    public class RegisterClickAndReturnLongUrlHandler
    {
        private readonly IShortUrlRepository _repository;

        public RegisterClickAndReturnLongUrlHandler(IShortUrlRepository repository)
        {
            _repository = repository;
        }

        public async Task<string?> HandleAsync(RegisterClickAndReturnLongUrlCommand command)
        {
            var shortUrl = await _repository.GetByCodeAsync(command.code);
            if (shortUrl == null) { return null; }

            shortUrl.RegisterClick();

            await _repository.UpdateAsync(shortUrl);

            return shortUrl.LongUrl;
        }
    }
}
