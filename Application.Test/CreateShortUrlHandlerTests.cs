using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Threading.Tasks;
using AutoMapper;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases.ShortUrls.CreateShortUrl;
using Domain.Entities;
using Application.DTOs;

namespace Application.Test
{
    public class CreateShortUrlHandlerTests
    {
        private readonly Mock<IShortUrlRepository> _repoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IShortUrlCodeGeneratorService> _codeGenMock = new();

        [Fact]
        public async Task HandleAsync_GeneratesUniqueCodeAndAddsEntity_ReturnsDto()
        {
            // Arrange
            string usedCodeFirst = "code1231";
            string usedCodeSecond = "code2312";
            Guid usedGuid = Guid.NewGuid();
            string usedLongUrl = "http://test.com";

            var command = new CreateShortUrlCommand
            {
                LongUrl = usedLongUrl,
                OwnerId = usedGuid.ToString()
            };

            var handler = new CreateShortUrlHandler(_repoMock.Object, _mapperMock.Object, _codeGenMock.Object);

            _codeGenMock
                .SetupSequence(c => c.Generate(8))
                .Returns(usedCodeFirst)
                .Returns(usedCodeSecond);

            _repoMock
                .Setup(r => r.GetByCodeAsync(usedCodeFirst))
                .ReturnsAsync(new ShortUrl());

            _repoMock
                .Setup(r => r.GetByCodeAsync(usedCodeSecond))
                .ReturnsAsync((ShortUrl?)null);

            _repoMock
                .Setup(r => r.AddAsync(It.IsAny<ShortUrl>()))
                .Returns(Task.CompletedTask);

            _mapperMock
                .Setup(m => m.Map<ShortUrlDto>(It.IsAny<ShortUrl>()))
                .Returns(new ShortUrlDto());

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.NotNull(result);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<ShortUrl>()), Times.Once);
            _codeGenMock.Verify(c => c.Generate(8), Times.AtLeast(2));
        }
    }
}