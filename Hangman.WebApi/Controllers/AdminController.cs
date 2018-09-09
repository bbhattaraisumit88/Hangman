using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Guest")]
    public class AdminController : BaseController
    {
        [HttpGet]
        [Route("GetAllData")]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }
    }
}