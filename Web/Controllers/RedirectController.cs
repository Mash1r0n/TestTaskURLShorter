using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("{code}")]
    public class RedirectController : ControllerBase
    {
        private readonly IShortUrlRepository _repository;

        public RedirectController(IShortUrlRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToLongUrl(string code)
        {
            var shortUrl = await _repository.GetByCodeAsync(code);
            if (shortUrl == null) return NotFound("Посилання не знайдено");

            shortUrl.RegisterClick();

            await _repository.UpdateAsync(shortUrl);

            return Redirect(shortUrl.LongUrl);
        }
    }
}
