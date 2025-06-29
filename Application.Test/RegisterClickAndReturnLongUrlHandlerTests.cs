using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.UseCases.ShortUrls.RegisterClickAndReturnLongUrl;
using Domain.Entities;

namespace Application.Test
{
    public class RegisterClickAndReturnLongUrlHandlerTests
    {
        private readonly Mock<IShortUrlRepository> _repoMock = new();

        [Fact]
        public async Task HandleAsync_ReturnsNull_WhenShortUrlNotFound()
        {
            // Arrange
            string usedCode = "codecode";
            var handler = new RegisterClickAndReturnLongUrlHandler(_repoMock.Object);

            _repoMock
                .Setup(r => r.GetByCodeAsync(It.IsAny<string>()))
                .ReturnsAsync((ShortUrl?)null);

            // Act
            var result = await handler.HandleAsync(new RegisterClickAndReturnLongUrlCommand { code = usedCode });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task HandleAsync_RegistersClickAndReturnsLongUrl()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string usedCode = "codecode";
            var shortUrl = ShortUrl.Create("http://test.com", $"{userId}", usedCode);
            var handler = new RegisterClickAndReturnLongUrlHandler(_repoMock.Object);

            _repoMock
                .Setup(r => r.GetByCodeAsync(usedCode))
                .ReturnsAsync(shortUrl);

            _repoMock
                .Setup(r => r.UpdateAsync(shortUrl))
                .Returns(Task.CompletedTask);

            // Act
            var result = await handler.HandleAsync(new RegisterClickAndReturnLongUrlCommand { code = usedCode });

            // Assert
            Assert.NotNull(result);
            Assert.Equal(shortUrl.LongUrl, result);
            _repoMock.Verify(r => r.UpdateAsync(shortUrl), Times.Once);
        }
    }
}