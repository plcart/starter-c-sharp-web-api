﻿using AutoMapper;
using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Web.Api.Filters;
using Starter.Web.Api.Models;
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

        [HttpGet]
        [HttpHead]
        [Route("api/currentuser")]
        [DigestAuthorize]
        public IHttpActionResult Get()
        {
            var username = ActionContext.RequestContext.Principal.Identity.Name;
            var user = authService.Get(x => x.Username == username);
            return Ok(Mapper.Map<UserModel>(user));
        }

        [HttpGet]
        [Route("api/currentuser/roles")]
        [DigestAuthorize]
        public IHttpActionResult GetRoles()
        {
            var username = ActionContext.RequestContext.Principal.Identity.Name;
            var roles = authService.Get(x => x.Username == username, new Expression<Func<User, object>>[]
            { x=>x.Profile.Roles}).Profile.Roles;
            return Ok(Mapper.Map<List<RoleModel>>(roles));
        }

    }
}
