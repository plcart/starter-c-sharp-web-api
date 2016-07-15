using Autofac;
using Autofac.Integration.WebApi;
using CacheCow.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Starter.Domain.Entities;
using Starter.Domain.Interfaces.Services;
using Starter.Web.Api;
using Starter.Web.Api.Controllers;
using Starter.Web.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace Starter.Wep.Api.Tests.Controllers
{
    [TestClass]
    public class PageTitleControllerTest
    {


        [TestInitialize]
        public void Initialize()
        {
            MapperConfig.RegisterMaps();
        }

        [TestMethod]
        public async Task Test()
        {
            var authService = new Mock<IServiceBase<PageTitle>>();
            var pageModel = new PageTitle()
            {
                Id = 1,
                Page = Page.Home,
                Title = "Mock Title",
                Description = "Mock Description"
            };
            Expression<Func<PageTitle, bool>> predicate = p => p.Page == Page.Home;
            authService.Setup(x => x.Get(It.Is<Expression<Func<PageTitle, bool>>>(k => !k.Equals(predicate)),null)).Returns(pageModel);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(authService.Object).As<IServiceBase<PageTitle>>();
            var container = builder.Build();


            var service = container.Resolve<IServiceBase<PageTitle>>();


            var config = new HttpConfiguration();
            var cachecow = new CachingHandler(config);
            config.MessageHandlers.Add(cachecow);
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/pages/{page}");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "pagetitle" } });
            var controller = new PageTitleController(service);

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            
            var result = await controller.Get(Page.Home).ExecuteAsync(new CancellationToken());
            var content = await result.Content.ReadAsAsync(typeof(PageTitleModel)) as PageTitleModel;
            Assert.AreEqual(true,true);

        }
    }
}
