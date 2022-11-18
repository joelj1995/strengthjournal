﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Server.ApiModels;
using StrengthJournal.Server.Extensions;
using StrengthJournal.Server.Services;

namespace StrengthJournal.Server.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileService profileService;

        public ProfileController(ProfileService profileService)
        {
            this.profileService = profileService;
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
    }
}