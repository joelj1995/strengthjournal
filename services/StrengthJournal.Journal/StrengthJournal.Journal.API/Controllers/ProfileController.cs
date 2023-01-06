using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.Journal.API.ApiModels;
using StrengthJournal.Core.Extensions;
using StrengthJournal.Journal.API.Services;
using StrengthJournal.Core.ApiModels;

namespace StrengthJournal.Journal.API.Controllers
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
            throw new Exception("Endpoint disabled");
            //var userId = HttpContext.GetUserId();
            //var result = await profileService.UpdateEmail(userId, updateEmail.NewEmail);
            //if (result)
            //    return Ok();
            //return new StatusCodeResult(500);
        }

        [HttpGet("config")]
        public async Task<AppConfig> GetConfig()
        {
            var userId = HttpContext.GetUserId();
            return userService.GetConfig(userId);
        }
    }
}
