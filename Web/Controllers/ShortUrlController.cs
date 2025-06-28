using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases.ShortUrls.CreateShortUrl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShortUrlController : ControllerBase
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IUrlDynamicMetadataRepository _urlDynamicMetadataRepository;
        private readonly CreateShortUrlHandler _createShortUrlHandler;

        public ShortUrlController(IShortUrlRepository shortUrlRepository, 
                                  CreateShortUrlHandler createShortUrlHandler, 
                                  IUrlDynamicMetadataRepository urlDynamicMetadataRepository)
        {
            _shortUrlRepository = shortUrlRepository;
            _createShortUrlHandler = createShortUrlHandler;
            _urlDynamicMetadataRepository = urlDynamicMetadataRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShortUrls()
        {
            return Ok(await _shortUrlRepository.GetAllShortUrlsAsync());
        }

        [Authorize]
        [HttpGet("{code}")]
        public async Task<IActionResult> GetShortUrlInfoByCode([FromRoute] string code)
        {
            return Ok(await _urlDynamicMetadataRepository.GetByCodeAsync(code));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewShortUrl([FromBody] CreateShortUrlModel createShortUrlModel)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) { return Unauthorized(); }

            var isLongUrlForThisUserExists = await _shortUrlRepository.ExistsForUserAsync(createShortUrlModel.LongUrl, userId);
            if (isLongUrlForThisUserExists) { return BadRequest("Only unique long urls per user"); }

            CreateShortUrlCommand createShortUrlCommand = new CreateShortUrlCommand
            {
                LongUrl = createShortUrlModel.LongUrl,
                OwnerId = userId
            };

            var createdShortUrl = await _createShortUrlHandler.HandleAsync(createShortUrlCommand);

            return Ok(createdShortUrl);
        }

        private bool IsUserNotAdmin()
        {
            return !User.IsInRole("Admin");
        }

        [Authorize]
        [HttpDelete("code/{code}")]
        public async Task<IActionResult> DeleteShortUrlByCode([FromRoute] string code)
        {
            var foundEntityForDelete = await _shortUrlRepository.GetByCodeAsync(code);
            if (foundEntityForDelete == null) { return NotFound(); }

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) { return Unauthorized(); }

            if (foundEntityForDelete.OwnerId != userId && IsUserNotAdmin()) { return StatusCode(403, new { error = "User can delete only their shorted urls" }); }

            await _shortUrlRepository.DeleteAsync(foundEntityForDelete);

            return NoContent();
        }
    }
}
