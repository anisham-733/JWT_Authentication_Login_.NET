using Login_Project.Business;
using Login_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Login_Project.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[action]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _log;

        public LoginController(ILoginService log)
        {
            _log = log;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login(string UserName, [DataType(DataType.Password)] string Password)
        {
             var a = _log.GetLogin(UserName,Password);
             if(a.Count == 0)
            {
                return BadRequest("oops no user found");
            }
            var s=_log.Authenticate(UserName);
            return Ok(s);
        }

        [HttpPost]
        public Task<int> SignUp([FromBody] Logins login)
        {
            return _log.SignUp(login);
        }
        [HttpGet]
        public List<Student> GetStudent()
        {
            return _log.GetStudent();
        }

    }
}
