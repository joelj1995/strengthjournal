﻿using Microsoft.AspNetCore.Mvc;
using RestSharp;
using StrengthJournal.Server.Integrations;
using StrengthJournal.Server.Integrations.Models;
using StrengthJournal.Server.Models;

namespace StrengthJournal.Server.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IHostEnvironment _hostEnvironment;

        public AuthController(IAuthenticationService authenticationService, IHostEnvironment hostEnvironment)
        {
            _authenticationService = authenticationService;
            _hostEnvironment = hostEnvironment;
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

        [Route("view-submit-login")]
        public IActionResult ViewSubmitLogin(LoginModel loginModel)
        {
            if (!_hostEnvironment.IsDevelopment())
                return NotFound();
            return View("SubmitLogin", new SubmitLoginModel("", "", true));
        }

        [Route("login")]
        public IActionResult Login([FromQuery] bool wrongPassword = false, [FromQuery] bool serviceFailure = false)
        {
            string? loginError = null;
            if (wrongPassword)
                loginError = "The email and password combination that you used is incorrect.";
            else if (serviceFailure)
                loginError = "We encountered an unexpected error trying to log you in. Please try again.";
            return View(new LoginModel() { Email = "", Password = "", Error = loginError });
        }

        [Route("signup")]
        public IActionResult SignUp([FromQuery] string errorMessage)
        {
            return View(new SignUpModel() { Email = "", Password = "", Password2 = "", Error = errorMessage == String.Empty ? null : errorMessage });
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult SubmitSignUp(SignUpModel model)
        {
            if (model.Password != model.Password2)
            {
                return RedirectToAction("signup", new { errorMessage = "Passwords do not match" });
            }
            var result = _authenticationService.CreateAccount(model.Email, model.Password);
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
    }
}
