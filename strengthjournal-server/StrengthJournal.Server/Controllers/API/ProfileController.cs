﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.Extensions;
using StrengthJournal.Server.Models;
using StrengthJournal.Server.Services;

namespace StrengthJournal.Server.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService profileService;
        private readonly UserService userService;

        public ProfileController(ProfileService profileService, UserService userService)
        {
            this.profileService = profileService;
            this.userService = userService;
        }

        [HttpGet("countries")]
        public async Task<IEnumerable<CountryDto>> GetCountries()
        {
            return await profileService.GetCountries();
        }

        [HttpPut("password-reset")]
        public async Task<ActionResult> ResetPassword()
        {
            var userId = HttpContext.GetUserId();
            await profileService.ResetPassword(userId);
            return Ok();
        }

        [HttpGet("settings")]
        public async Task<ProfileSettingsDto> GetSettings()
        {
            var userId = HttpContext.GetUserId();
            return await profileService.GetSettings(userId);
        }

        [HttpPost("settings")]
        public async Task<ActionResult> UpdateSettings(ProfileSettingsDto settings)
        {
            var userId = HttpContext.GetUserId();
            await profileService.UpdateSettings(userId, settings);
            return Ok();
        }

        [HttpPut("email")]
        public async Task<ActionResult> UpdateEmail([FromBody] UpdateEmailDto updateEmail)
        {
            var userId = HttpContext.GetUserId();
            var result = await profileService.UpdateEmail(userId, updateEmail.NewEmail);
            if (result)
                return Ok();
            return new StatusCodeResult(500);
        }

        [HttpGet("config")]
        public async Task<AppConfig> GetConfig()
        {
            var userId = HttpContext.GetUserId();
            return userService.GetConfig(userId);
        }
    }
}
