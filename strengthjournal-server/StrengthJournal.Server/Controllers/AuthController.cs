using Microsoft.AspNetCore.Mvc;
using RestSharp;
using StrengthJournal.Server.Integrations;
using StrengthJournal.Server.Integrations.Models;
using StrengthJournal.Server.Models;
using StrengthJournal.Server.Services;

namespace StrengthJournal.Server.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ProfileService profileService;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly UserService userService;

        public AuthController(
            IAuthenticationService authenticationService, 
            IHostEnvironment hostEnvironment,
            ProfileService profileService,
            UserService userService)
        {
            _authenticationService = authenticationService;
            _hostEnvironment = hostEnvironment;
            this.profileService = profileService;
            this.userService = userService;
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
                    return RedirectToAction("Login", new { serviceFailure = true });
                case AuthenticationResponse.AuthResult.EmailNotVerified:
                    return RedirectToAction("Login", new { notVerified = true });
                case AuthenticationResponse.AuthResult.Success:
                    var config = userService.GetConfig(loginModel.Email);
                    return View(new SubmitLoginModel(result.Token, loginModel.Email, config));
                default:
                    throw new NotImplementedException("Authentication result not recognized");
            }
        }

        [Route("view-submit-login")]
        public IActionResult ViewSubmitLogin(LoginModel loginModel)
        {
            if (!_hostEnvironment.IsDevelopment())
                return NotFound();
            return View("SubmitLogin", new SubmitLoginModel(new AppConfig { PreferredWeightUnit = "kg" }));
        }

        [Route("login")]
        public IActionResult Login([FromQuery] bool wrongPassword = false, [FromQuery] bool serviceFailure = false, [FromQuery] bool notVerified = false)
        {
            string? loginError = null;
            if (wrongPassword)
                loginError = "The email and password combination that you used is incorrect.";
            else if (serviceFailure)
                loginError = "We encountered an unexpected error trying to log you in. Please try again.";
            else if (notVerified)
                return View(new LoginModel() { Email = "", Password = "", IsEmailConfirmed = false, Error = null });
            return View(new LoginModel() { Email = "", Password = "", Error = loginError });
        }

        [Route("signup")]
        public IActionResult SignUp([FromQuery] string errorMessage)
        {
            return View(new SignUpModel() 
            { 
                Email = "", 
                Password = "", 
                Password2 = "", 
                Error = errorMessage == String.Empty ? null : errorMessage, 
                CountryList = profileService.GetCountries().Result 
            });
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult SubmitSignUp(SignUpModel model)
        {
            if (model.Password != model.Password2)
            {
                return RedirectToAction("signup", new { errorMessage = "Passwords do not match" });
            }
            var result = _authenticationService.CreateAccount(model.Email, model.Password, model.ConsentCEM, model.CountryCode);
            switch (result.Result)
            {
                case CreateAccountResponse.CreateResult.ValidationError:
                    return RedirectToAction("signup", new { errorMessage = result.ErrorMessage });
                case CreateAccountResponse.CreateResult.ServiceFailure:
                    return RedirectToAction("signup", new { errorMessage = result.ErrorMessage });
                case CreateAccountResponse.CreateResult.Success:
                    return View();
                default:
                    throw new NotImplementedException("Authentication result not recognized");
            }
        }

        [Route("view-signup")]
        public IActionResult ViewSubmitSignUp()
        {
            if (!_hostEnvironment.IsDevelopment())
                return NotFound();
            return View("SubmitSignUp");
        }

        [Route("reset-password")]
        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordModel());
        }

        [HttpPost]
        [Route("reset-password")]
        public IActionResult SubmitResetPassword(ResetPasswordModel model)
        {
            _authenticationService.ResetPassword(model.Email);
            return View();
        }

        [HttpGet]
        [Route("send-verification")]
        public IActionResult SendEmailVerification()
        {
            return View();
        }

        [HttpPost]
        [Route("send-verification")]
        public IActionResult SubmitSendEmailVerification(string email)
        {
            _authenticationService.ResendVerificationEmail(email);
            return View();
        }
    }
}
