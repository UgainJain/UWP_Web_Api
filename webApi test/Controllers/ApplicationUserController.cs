using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webApi_test.Models;
using webApi_test.ViewModels.User;

namespace webApi_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        //private readonly ApplicationSettings _appSetting;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_appSetting = appSetting;
        }

        [Route("Register")]
        [HttpPost]


        //POST : /api/ApplicationUser/Register

        public async Task<Object> RegisterUser(ApplicationUserViewModel model)
        {
            //model.Role = "Traveller";
            var applicationUser = new ApplicationUser()
            {

                UserName = model.UserName,
                Email = model.Email,
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                //await _userManager.AddToRoleAsync(applicationUser, model.Role);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost]
        //[Route("Login")]
        //public async Task<Object> Login(LoginUserViewModel model)
        //{
        //    var user = await _userManager.FindByNameAsync(model.Username);
        //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        await _signInManager.SignInAsync(user, false);
        //        var Role = _userManager.GetRolesAsync(user);
        //        IdentityOptions _options = new IdentityOptions();
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //                 {
        //                     new Claim("UserID", user.Id.ToString()),
        //                     new Claim(_options.ClaimsIdentity.RoleClaimType,Role.Result.First()),
        //                     new Claim("ApplicationID",_appSetting.Application_ID)
        //                 }),
        //            Expires = DateTime.UtcNow.AddDays(1),
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
        //        };
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        //        var token = tokenHandler.WriteToken(securityToken);
        //        var User = new Dictionary<string, string>();
        //        User.Add("FullName", user.FullName);
        //        User.Add("Email", user.Email);
        //        User.Add("UserName", user.UserName);
        //        var resp = new Dictionary<string, Object>();
        //        resp.Add("token", token);
        //        resp.Add("Role", Role.Result.First());
        //        resp.Add("User", User);
        //        return Ok(resp);
        //    }
        //    else
        //        return BadRequest(new { message = "Username or Password is incorrect" });
        //}

    }
}