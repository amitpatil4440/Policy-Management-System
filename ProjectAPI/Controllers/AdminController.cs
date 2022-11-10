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
    public class AdminController : ControllerBase
    {
        private readonly IAdmin admin = null;
        public AdminController(IAdmin admin)
        {
            this.admin = admin;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AdminModel adminmodel)
        {
            string a = await admin.AdminRegister(adminmodel);
            return Ok(a);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAdmin(string emailid)
        {
            string a = await admin.DeleteAdmin(emailid);
            return Ok(a);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAdmin(string emailid, AdminModel adminmodel)
        {
            string a = await admin.UpdateAdmin(emailid, adminmodel);
            return Ok(a);
        }
        [HttpDelete("PolicyDelete")]
        public async Task<IActionResult> Deletepolicy(int policyid)
        {
            var ar = await admin.DeletePolicy(policyid);
            return Ok(ar);
        }
        [HttpPut("PolicyUpdate")]
        public async Task<IActionResult> UpdatePolicy(int policyid, PolicyRegistrationModel policyRegistrationModel)
        {
            var ar = await admin.UpdatePolicy(policyid, policyRegistrationModel);
            return Ok(ar);
        }
        [HttpPost("Adminlogin")]
        public async Task<IActionResult> AdminLogin(AdminModel adminModel)
        {
            string ar = await admin.AdminLogin(adminModel);
            return Ok(ar);

        }
        [HttpGet("Forgotpassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var res = await admin.ForgotPassword(email);
            return Ok(res);
        }

    }
}
