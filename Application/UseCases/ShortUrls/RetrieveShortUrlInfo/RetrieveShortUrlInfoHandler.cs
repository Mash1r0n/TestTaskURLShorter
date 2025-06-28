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

namespace Application.UseCases.ShortUrls.RetrieveShortUrlInfo
{
    public class RetrieveShortUrlInfoHandler
    {
        private readonly IShortUrlRepository _repository;
        private readonly IMapper _mapper;

        public RetrieveShortUrlInfoHandler(IShortUrlRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ShortUrlInfoDto> HandleAsync(RetrieveShortUrlInfoCommand command)
        {
            var entity = await _repository.GetByIdAsync(command.ShortUrlId);

            return _mapper.Map<ShortUrlInfoDto>(entity);
        }
    }
}
