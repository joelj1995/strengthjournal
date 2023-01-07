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

        [HttpPost("create")]
        public async Task<ActionResult<CreateAccountResponse>> CreateAccount(CreateAccountRequest body)
        {
            var result = await identityService.Register(body);
            switch (result.ResultCode)
            {
                case CreateAccountResponse.CreateResult.Success: return Ok(result);
                default: return BadRequest(result);
            }
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ResetPasswordResponse>> ResetPassword(ResetPasswordRequest body)
        {
            var result = await identityService.ResetPassword(body);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost("send-verification")]
        public async Task<ActionResult<SendVerificationResponse>> SendVerification(SendVerificationRequest body)
        {
            var result = await identityService.SendVerification(body);
            if (result.Succeeded)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}
