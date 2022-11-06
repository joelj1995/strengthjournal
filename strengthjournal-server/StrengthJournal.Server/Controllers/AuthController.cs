using Microsoft.AspNetCore.Mvc;
using RestSharp;
using StrengthJournal.Server.Integrations;
using StrengthJournal.Server.Integrations.Models;
using StrengthJournal.Server.Models;

namespace StrengthJournal.Server.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult SubmitLogin(LoginModel loginModel)
        {
            var result = _authenticationService.Authenticate(loginModel.Email, loginModel.Password);
            switch (result.Result)
            {
                case AuthenticationResponse.AuthResult.WrongPassword:
                    return RedirectToAction("Login", new { wrongPassword = true });
                case AuthenticationResponse.AuthResult.ServiceFailure:
                    return RedirectToAction("Login", new { wrongPassword = true });
                case AuthenticationResponse.AuthResult.Success:
                    return View(new SubmitLoginModel(result.Token, loginModel.Email));
                default:
                    throw new NotImplementedException("Authentication result not recognized");
            }
        }

        [Route("login")]
        public IActionResult Login([FromQuery] bool wrongPassword = false, [FromQuery] bool serviceFailure = false)
        {
            return View();
        }

        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
