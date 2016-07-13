using AutoMapper;
using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Web.Api.Filters;
using Starter.Web.Api.Models;
using System.Web.Http;

namespace Starter.Web.Api.Controllers
{
    public class AuthController : BaseApiController
    {

        public IAuthService authService { get; }
        public IServiceBase<Domain.Entities.Profile> profileService { get; }

        public AuthController()
        {
            authService = resolver.GetService(typeof(IAuthService)) as IAuthService;
            profileService = resolver.GetService(typeof(IServiceBase<Domain.Entities.Profile>)) as IServiceBase<Domain.Entities.Profile>;
        }

        [HttpPost]
        [Route("api/register")]
        [ValidateModel("model")]
        public IHttpActionResult Register(UserModel model)
        {
            model.ProfileId = profileService.Get(x => !x.Deleted.HasValue)?.Id ?? 1;
            var user = authService.Get(x => x.Username == model.Username) ?? Mapper.Map<User>(model);
            if (user.Id != 0)
                return BadRequest("Username already in use.");
            authService.Register(user);
            return Ok(Mapper.Map<UserModel>(user));
        }

        [HttpPost]
        [Route("api/login")]
        public IHttpActionResult Login(UserModel model)
        {
            var user = authService.Login(model.Username, model.Password);
            return Ok(Mapper.Map<UserModel>(user));
        }


        [Authorize]
        [HttpGet]
        [Route("api/user")]
        public IHttpActionResult Get()
        {
            return Ok(new { musica="tititit" });
        }
    }
}
