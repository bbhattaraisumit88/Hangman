using Hangman.Domain;
using Hangman.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Superuser")]
    public class AdminController : BaseController
    {
        private readonly IUserLeaveService _userLeaveService;
        public AdminController(IUserLeaveService _userLeaveService)
        {
            this._userLeaveService = _userLeaveService;
        }
        [HttpPost]
        [Route("ApproveLeave")]
        public IActionResult Approve([FromBody]UserLeave userLeaves)
        {
            return Ok(_userLeaveService.ApproveLeave(userLeaves));
        }

        [HttpPost]
        [Route("RejectLeave")]
        public IActionResult Reject([FromBody]UserLeave userLeaves)
        {
            return Ok(_userLeaveService.RejectLeave(userLeaves));
        }
    }
}