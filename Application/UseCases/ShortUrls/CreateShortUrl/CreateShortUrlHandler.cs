using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
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
        private readonly ICodeGenerator _codeGenerator;

        public CreateShortUrlHandler(IShortUrlRepository repository, IMapper mapper, ICodeGenerator codeGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _codeGenerator = codeGenerator;
        }

        public async Task<ShortUrlDto> Handle(CreateShortUrlCommand command)
        {
            string code;

            do
            {
                code = _codeGenerator.Generate();
                
            }
            while (await _repository.GetByCodeAsync(code) != null);

            var entity = ShortUrl.Create(command.LongUrl, command.OwnerId, code);

            await _repository.AddAsync(entity);

            return _mapper.Map<ShortUrlDto>(entity);
        }
    }
}
