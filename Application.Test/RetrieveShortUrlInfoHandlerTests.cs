using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AutoMapper;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.UseCases.ShortUrls.RetrieveShortUrlInfo;
using Domain.Entities;

namespace Application.Test
{
    public class RetrieveShortUrlInfoHandlerTests
    {
        private readonly Mock<IShortUrlRepository> _repoMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task HandleAsync_MapsEntityToDto()
        {
            // Arrange
            var entity = new ShortUrl();
            var dto = new ShortUrlInfoDto();

            _repoMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(entity);

            _mapperMock
                .Setup(m => m.Map<ShortUrlInfoDto>(entity))
                .Returns(dto);

            var handler = new RetrieveShortUrlInfoHandler(_repoMock.Object, _mapperMock.Object);
            var command = new RetrieveShortUrlInfoCommand { ShortUrlId = Guid.NewGuid() };

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.Equal(dto, result);
        }
    }
}
