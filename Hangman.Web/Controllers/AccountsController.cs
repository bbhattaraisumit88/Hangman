using AutoMapper;
using Hangman.Domain;
using Hangman.Service;
using Hangman.Service.Helpers;
using Hangman.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Hangman.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var userIdentity = _mapper.Map<AppUser>(model);

            var result = await _userService.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return BadRequest(Errors.AddErrorToModelState(result, ModelState));

            return new JsonResult("User Registered Successfully");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]CredentialsDTO credentials)
        {
            try
            {
                var identity = await _userService.GetClaimsIdentity(credentials.UserName, credentials.Password);
                if (identity == null)
                {
                    return BadRequest(Utility.AddErrorToModelState("Invalid username or password.", ModelState));
                }
                var jwt = await Tokens.GenerateJwt(identity, _jwtService, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}