using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ApiRestful.Business;
using ApiRestful.Business.Interfaces;
using ApiRestful.Controllers.Inputs;
using ApiRestful.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestful.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _jwtService;
        public UserController(IUserService userService, ITokenService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Signup([FromBody] SignupInput signupInput)
        {
            var listError = ValidateSignupInput(signupInput);

            if (listError.Any())
                return Json(Utils.Utils.Instance.ResultMessage(string.Join(",", listError), false, 403));

            var hasEmail = _userService.Handle(new CheckHasEmail { Email = signupInput.Email });

            if (hasEmail)
                return Json(Utils.Utils.Instance.ResultMessage("E-mail already exists", false, 403));

            var saveUser = new SaveUser
            {
                Email = signupInput.Email,
                CreatedAt = DateTime.Now,
                FirstName = signupInput.FirstName,
                LastName = signupInput.LastName,
                Password = signupInput.Password,
                Phones = signupInput.Phones.Select(s =>
                new UserContact
                {
                    CodeArea = s.AreaCode.Value,
                    CountryCode = s.CountryCode,
                    Number = s.Number.Value
                }).ToList()
            };

            var newuser = _userService.Handle(saveUser);

            return Json(Utils.Utils.Instance.ResultMessage($"User created successfully, userId:{newuser.Id}", true, 201));

        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Signin([FromBody] SiginInput siginInput)
        {
            var listError = ValidateSiginInput(siginInput);

            if (listError.Any())
                return Json(Utils.Utils.Instance.ResultMessage(string.Join(",", listError), false, 403));

            var user = _userService.Handle(new FindUser { Email = siginInput.Email, Password = siginInput.Password });

            if (user == null)
                return Json(Utils.Utils.Instance.ResultMessage("Invalid e-mail or password", false, 403));


            _userService.Handle(new UpdateLastLoginUser { Id = user.Id });
            var token = _jwtService.CreateToken(user);

            return Json(new { UserId = user.Id, IsSucess = true, MessageCode = 201, Token = token });
        }


        [HttpPost]
        public User FindUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return _userService.Handle(new FindUser { Id = Convert.ToInt32(userId)});
        }

        //[HttpPost]
        //public User FindUser([FromBody] FindUserInput findUserInput)
        //{
        //    return _userService.Handle(new FindUser { Id = findUserInput.Id });
        //}

        [AllowAnonymous]
        public User FindUser(int id)
        {
            return _userService.Handle(new FindUser { Id = id });
        }

        #region PrivateMethods

        private static IList<string> ValidateSignupInput(SignupInput signupInput)
        {
            var listError = new List<string>();

            if (string.IsNullOrWhiteSpace(signupInput.FirstName))
            {
                listError.Add("Firt Name not informed");
            }
            if (string.IsNullOrWhiteSpace(signupInput.LastName))
            {
                listError.Add("Last Name not informed");
            }
            if (string.IsNullOrWhiteSpace(signupInput.Email))
            {
                listError.Add("Email not informed");
            }
            else
            {
                try
                {
                    var email = new System.Net.Mail.MailAddress(signupInput.Email);
                }
                catch
                {

                    listError.Add("Email invalid");
                }
            }
            if (string.IsNullOrWhiteSpace(signupInput.Password))
            {
                listError.Add("Password not informed");
            }
            //if (signupInput.Phones != null && !signupInput.Phones.Any())
            //{
            //    listError.Add("Phone not informed");
            //}
            //else
            //{
            //    //if (signupInput.Phones.Any(a => !a.AreaCode.HasValue))
            //    //{
            //    //    listError.Add("Phone informed without CodeArea");

            //    //}
            //    //if (signupInput.Phones.Any(a => string.IsNullOrWhiteSpace(a.CountryCode)))
            //    //{
            //    //    listError.Add("Phone not informed without CountryCode");

            //    //}
            //    //if (signupInput.Phones.Any(a => !a.Number.HasValue))
            //    //{
            //    //    listError.Add("Phone not informed without Number");

            //    //}
            //}

            return listError;
        }

        private static IList<string> ValidateSiginInput(SiginInput siginInput)
        {
            var listError = new List<string>();

            if (string.IsNullOrWhiteSpace(siginInput.Email))
            {
                listError.Add("Email not informed");
            }
            if (string.IsNullOrWhiteSpace(siginInput.Email))
            {
                listError.Add("Password not informed");
            }


            return listError;
        }

        #endregion
    }
}