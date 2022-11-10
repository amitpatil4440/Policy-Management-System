using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.DI;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user = null;
        public UserController(IUser user)
        {
            this.user = user;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel usermodel)
        {
            string a = await user.Register(usermodel);
            return Ok(a);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(string emailid, UserModel usermodel)
        {
            string a = await user.UpdateUser(emailid, usermodel);
            return Ok(a);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string emailid)
        {
            string a = await user.DeleteUser(emailid);
            return Ok(a);
        }
        [HttpPost("Userlogin")]
        public async Task<IActionResult> UserLogin(UserModel userModel)
        {
            string ar = await user.UserLogin(userModel);
            return Ok(ar);

        }
        [HttpGet("GetPolicyByType/{policytype}")]
        public async Task<IActionResult> GetByPolicyType(string policytype)
        {
            var pt = await user.GetbyType(policytype);
            if (pt != null)
            {
                return Ok(pt);
            }
            else
            {
                return NotFound();
            }
        }
            [HttpGet("Forgotpassword")]
            public async Task<IActionResult> ForgotPassword(string email)
            {
                var res = await user.ForgotPassword(email);
                return Ok(res);
            }
        }
        //public async Task<IActionResult> ShowPolicies()
        //{
        //    var a = await PolicyRegistrationModel.Showpolicies();
        //    return Ok(a);
        //}


    }

