using Application.Interfaces.Repositories;
using Application.UseCases.ShortUrls.RegisterClickAndReturnLongUrl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("{code}")]
    public class RedirectController : ControllerBase
    {
        private readonly RegisterClickAndReturnLongUrlHandler _registerClickAndReturnLongUrlHandler;

        public RedirectController(RegisterClickAndReturnLongUrlHandler registerClickAndReturnLongUrlHandler)
        {
            _registerClickAndReturnLongUrlHandler = registerClickAndReturnLongUrlHandler;
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToLongUrl(string code)
        {
            RegisterClickAndReturnLongUrlCommand command = new RegisterClickAndReturnLongUrlCommand
            {
                code = code
            };

            var longUrl = await _registerClickAndReturnLongUrlHandler.HandleAsync(command);

            return longUrl == null ? NotFound("URL not found") : Ok(longUrl);
        }
    }
}