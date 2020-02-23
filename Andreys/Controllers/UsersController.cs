using System;
using System.Collections.Generic;
using System.Text;
using Andreys.Services;
using Andreys.ViewModels.Users;
using SIS.HTTP;
using SIS.MvcFramework;

namespace Andreys.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet]
        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel inputModel)
        {
            var userId = this.usersService.GetUserId(inputModel.Username, inputModel.Password);

            if (userId != null)
            {
                this.SignIn(userId);
                return this.Redirect("/");
            }

            return this.Redirect("/Users/Login");
        }

        [HttpGet]
        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.Username) || string.IsNullOrWhiteSpace(inputModel.Email)
            || string.IsNullOrWhiteSpace(inputModel.Password) || string.IsNullOrWhiteSpace(inputModel.ConfirmPassword))
            {
                return this.Redirect("/Users/Register");
            }
            if (inputModel.Username.Length < 4 || inputModel.Username.Length > 10)
            {
                return this.Redirect("/Users/Register");
            }

            if (inputModel.Password.Length < 6 || inputModel.Password.Length > 20)
            {
                return this.Redirect("/Users/Register");
            }

            if (inputModel.Password != inputModel.ConfirmPassword)
            {
                return this.Redirect("/Users/Register");
            }

            if (usersService.UserNameExist(inputModel.Username) || usersService.EmailExist(inputModel.Email))
            {
                return this.Redirect("/Users/Register");
            }

            this.usersService.Register(inputModel.Username,inputModel.Email,inputModel.Password);

            return this.Redirect("/Users/Login");
        }

        [HttpGet("/Logout")]
        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            this.SignOut();
            return this.Redirect("/");
        }
    }
}
