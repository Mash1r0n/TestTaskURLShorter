﻿using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.UseCases.ShortUrls.CreateShortUrl;
using Application.UseCases.ShortUrls.RetrieveShortUrlInfo;
using Domain.Entities;
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
        private readonly CreateShortUrlHandler _createShortUrlHandler;
        private readonly RetrieveShortUrlInfoHandler _retrieveShortUrlInfoHandler;

        public ShortUrlController(IShortUrlRepository shortUrlRepository, 
                                  CreateShortUrlHandler createShortUrlHandler,
                                  RetrieveShortUrlInfoHandler retrieveShortUrlInfoHandler)
        {
            _shortUrlRepository = shortUrlRepository;
            _createShortUrlHandler = createShortUrlHandler;
            _retrieveShortUrlInfoHandler = retrieveShortUrlInfoHandler;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllShortUrls()
        {
            return Ok(await _shortUrlRepository.GetAllShortUrlsAsync());
        }

        [Authorize]
        [HttpGet("info")]
        public async Task<IActionResult> GetShortUrlInfoByCode([FromQuery] RetrieveShortUrlInfoModel retrieveShortUrlInfoModel)
        {
            RetrieveShortUrlInfoCommand retrieveShortUrlInfoCommand = new RetrieveShortUrlInfoCommand
            {
                ShortUrlId = retrieveShortUrlInfoModel.ShortUrlId
            };

            return Ok(await _retrieveShortUrlInfoHandler.HandleAsync(retrieveShortUrlInfoCommand));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewShortUrl([FromBody] CreateShortUrlModel createShortUrlModel)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) { return Unauthorized(); }

            var isLongUrlForThisUserExists = await _shortUrlRepository.ExistsForUserAsync(createShortUrlModel.LongUrl, userId);
            if (isLongUrlForThisUserExists) return BadRequest("Only one unique long url per user");

            CreateShortUrlCommand createShortUrlCommand = new CreateShortUrlCommand
            {
                LongUrl = createShortUrlModel.LongUrl,
                OwnerId = userId
            };

            var createdShortUrl = await _createShortUrlHandler.HandleAsync(createShortUrlCommand);

            return Ok(createdShortUrl);
        }

        [Authorize]
        [HttpDelete("code/{code}")]
        public async Task<IActionResult> DeleteShortUrlByCode([FromRoute] string code)
        {
            var foundEntityForDelete = await _shortUrlRepository.GetByCodeAsync(code);
            if (foundEntityForDelete == null) return NotFound();

            var userId = RetrieveUserIdFromControllerBase();
            if (userId == null) return Unauthorized();

            if (IsUserCantDelete(foundEntityForDelete, userId)) return StatusCode(403, new { error = "User can delete only their shorted urls" });

            await _shortUrlRepository.DeleteAsync(foundEntityForDelete);

            return NoContent();
        }

        private bool IsUserNotAdmin()
        {
            return !User.IsInRole("Admin");
        }

        private bool IsUserCantDelete(ShortUrl entityForDelete, string userId)
        {
            return entityForDelete.OwnerId != userId && IsUserNotAdmin();
        }

        private string? RetrieveUserIdFromControllerBase()
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
