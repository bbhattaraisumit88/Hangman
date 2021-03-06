using Hangman.Domain;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hangman.Service.Helpers
{
    public class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtService jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
        {
            try
            {
                var response = new
                {
                    username = userName,
                    id = identity.Claims.Single(c => c.Type == "id").Value,
                    role = identity.Claims.Single(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value,
                    auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                    expires_in = (int)jwtOptions.ValidFor.TotalSeconds
                };
                return JsonConvert.SerializeObject(response, serializerSettings);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}