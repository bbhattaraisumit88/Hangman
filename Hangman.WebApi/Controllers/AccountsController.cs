using AutoMapper;
using Hangman.Domain;
using Hangman.Service;
using Hangman.Service.Helpers;
using Hangman.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hangman.WebApi.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly ApplicationDbContext _gpsRecon;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IUserService<AppUser> _userService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AccountsController(ApplicationDbContext gpsRecon, IJwtService jwtService, IMapper mapper, IOptions<JwtIssuerOptions> jwtOptions, IUserService<AppUser> userService, IHostingEnvironment _hostingEnvironment)
        {
            this._mapper = mapper;
            this._gpsRecon = gpsRecon;
            this._jwtService = jwtService;
            this._jwtOptions = jwtOptions.Value;
            this._userService = userService;
            this._hostingEnvironment = _hostingEnvironment;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post([FromBody]RegistrationDTO model)
        {
            try
            {
                AppUser userIdentity = _mapper.Map<AppUser>(model);
                string role = Constants.Strings.DefaultRoles.Guest;
                IdentityResult result = await _userService.CreateAsync(userIdentity, model.Password);
                if (result.Succeeded)
                {
                    await _userService.AddClaimAsync(userIdentity, role);
                    await _userService.AddToRoleAsync(userIdentity, role);
                }
                else return BadRequest(Errors.AddErrorToModelState(result, ModelState));
                return new JsonResult("User Registered Successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Post([FromBody]CredentialsDTO credentials)
        {
            try
            {
                ClaimsIdentity identity = await _userService.GetClaimsIdentity(credentials.UserName, credentials.Password);
                if (identity == null)
                {
                    return Unauthorized();
                }
                string jwt = await Tokens.GenerateJwt(identity, _jwtService, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return Ok(JsonConvert.SerializeObject(jwt));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Post(IFormFile files)
        {
            try
            {
                string newPath = Path.Combine(_hostingEnvironment.WebRootPath, "Upload");
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                string fileName = ContentDispositionHeaderValue.Parse(files.ContentDisposition).FileName.Trim('"');
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                }
                var image = System.IO.File.OpenRead(fullPath);
                byte[] imageArray = System.IO.File.ReadAllBytes(fullPath);
                return Ok(JsonConvert.SerializeObject(Convert.ToBase64String(imageArray)));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }


}
