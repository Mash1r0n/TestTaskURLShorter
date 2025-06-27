using Application.DTOs;
using AutoMapper;
using Domain.AdditionalFunctionality;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.ShortUrls.CreateShortUrl
{
    public class CreateShortUrlHandler
    {
        private readonly IShortUrlRepository _repository;
        private readonly IMapper _mapper;

        public CreateShortUrlHandler(IShortUrlRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ShortUrlDto> Handle(CreateShortUrlCommand command)
        {
            string code;

            do
            {
                code = CodeGenerator.Generate();
                
            }
            while (await _repository.GetByCodeAsync(code) != null);

            var entity = ShortUrl.Create(command.LongUrl, command.OwnerId, code);

            await _repository.AddAsync(entity);

            return _mapper.Map<ShortUrlDto>(entity);
        }
    }
}
