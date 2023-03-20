namespace WebAPI.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {
        protected string GetUserId()
        {
            var sid = (this.User.Identity as ClaimsIdentity)
                .FindFirst(JwtRegisteredClaimNames.Sid);

            if (sid == null)
            {
                throw new ArgumentNullException("Problem with jwt id occured! Report to developers!");
            }

            return sid.Value;
        }

        protected string GetUsername()
        {
            var sid = (this.User.Identity as ClaimsIdentity)
                .FindFirst(JwtRegisteredClaimNames.NameId);

            if (sid == null)
            {
                throw new ArgumentNullException("Problem with jwt username occured! Report to developers!");
            }

            return sid.Value;
        }
    }
}
