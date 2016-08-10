using AutoMapper;
using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Web.Api.Filters;
using Starter.Web.Api.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model">user model</param>
        /// <remarks>Should have unique username</remarks>
        [HttpPost]
        [Route("api/register")]
        [ValidateModel("model")]
        [SwaggerResponse(200, "Created User", typeof(UserModel))]
        [SwaggerResponse(412, "Wrong model or existing username")]
        public IHttpActionResult Register(UserModel model)
        {
            model.ProfileId = profileService.Get(x => !x.Deleted.HasValue)?.Id ?? 1;
            var user = authService.Get(x => x.Username == model.Username) ?? Mapper.Map<User>(model);
            if (user.Id != 0)
                return BadRequest("Username already in use.");
            authService.Register(user);
            return Ok(Mapper.Map<UserModel>(user));
        }

        /// <summary>
        /// Authenticate a user
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [Route("api/login")]
        [SwaggerResponse(200, "User Model", typeof(UserModel))]
        [SwaggerResponse(404, "User Not Found")]
        public IHttpActionResult Login(UserModel model)
        {
            var user = authService.Login(model.Username, model.Password);
            if (user != null)
                return Ok(Mapper.Map<UserModel>(user));
            else
                return NotFound();
        }

        [HttpGet]
        [Route("api/currentuser")]
        [BasicAuthorize]
        public IHttpActionResult Get()
        {
            var username = ActionContext.RequestContext.Principal.Identity.Name;
            var user = authService.Get(x => x.Username == username);
            return Ok(Mapper.Map<UserModel>(user));
        }

        [HttpGet]
        [Route("api/currentuser/roles")]
        [BasicAuthorize]
        public IHttpActionResult GetRoles()
        {
            var username = ActionContext.RequestContext.Principal.Identity.Name;
            var roles = authService.Get(x => x.Username == username, new Expression<Func<User, object>>[]
            { x=>x.Profile.Roles}).Profile.Roles;
            return Ok(Mapper.Map<List<RoleModel>>(roles));
        }

    }
}
