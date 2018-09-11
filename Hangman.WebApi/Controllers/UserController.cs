using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangman.Domain;
using Hangman.Domain.DTO;
using Hangman.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hangman.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Guest")]
    public class UserController : BaseController
    {
        private readonly IUserLeaveService _userLeaveService;
        private readonly IMapper _mapper;
        public UserController(IUserLeaveService _userLeaveService, IMapper _mapper)
        {
            try
            {
                this._userLeaveService = _userLeaveService;
                this._mapper = _mapper;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("ApplyLeave")]
        public IActionResult Post([FromBody]UserLeave userLeave)
        {
            try
            {
                return Ok(_userLeaveService.ApplyLeave(userLeave));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("CancelLeave")]
        public IActionResult Cancel([FromBody]UserLeave userLeaves)
        {
            return Ok(_userLeaveService.CancelLeave(userLeaves));
        }

        
        [HttpGet]
        [Route("GetAllLeave")]
        public IActionResult Get()
        {
            try
            {
                List<object> userLeaves = _userLeaveService.GetAllLeave().ToList();
                if (userLeaves.Count > 0)
                {
                    return Ok(userLeaves);
                }
                else
                {
                    return NotFound("No leave details");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetLeaveById/{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                List<object> userLeaves = _userLeaveService.GetLeaveById(id).ToList();
                if (userLeaves.Count > 0)
                {
                    return Ok(userLeaves);
                }
                else
                {
                    return NotFound("No leave details");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}