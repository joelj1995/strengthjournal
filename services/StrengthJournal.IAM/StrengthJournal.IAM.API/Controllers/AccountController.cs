using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrengthJournal.IAM.API.Models;
using StrengthJournal.IAM.API.Services;

namespace StrengthJournal.IAM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IIdentityService identityService;

        public AccountController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest body)
        {
            var result = await identityService.Authenticate(body);
            switch (result.ResultCode) 
            {
                case LoginResponse.AuthResult.Success: return Ok(result);
                default: return Unauthorized(result);
            }
        }
    }
}
