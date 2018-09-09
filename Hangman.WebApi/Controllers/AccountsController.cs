using AutoMapper;
using Hangman.Domain;
using Hangman.Service;
using Hangman.Service.Helpers;
using Hangman.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hangman.WebApi.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly ApplicationDbContext _gpsRecon;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IUserService<AppUser> _userService;

        public AccountsController(ApplicationDbContext gpsRecon, IJwtService jwtService, IMapper mapper, IOptions<JwtIssuerOptions> jwtOptions, IUserService<AppUser> userService)
        {
            this._mapper = mapper;
            this._gpsRecon = gpsRecon;
            this._jwtService = jwtService;
            this._jwtOptions = jwtOptions.Value;
            this._userService = userService;
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
    }
}