using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DataAccess;
using StudyOnline.Infrastructure;
using BusinessLogical.Abstract;
using BusinessLogical.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace StudyOnline
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new InitializeDBWithSeedData());

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectDependencyResolver dependencyResolver = new NinjectDependencyResolver();
            dependencyResolver.Register<IVideoRepository, VideoRepository>();
            dependencyResolver.Register<IVideoService, VideoService>();
            dependencyResolver.Register<IVideoCatRepository, VideoCatRepository>();
            dependencyResolver.Register<IVideoCatService, VideoCatService>();
            dependencyResolver.Register<IDocRepository, DocRepository>();
            dependencyResolver.Register<IDocService, DocService>();
            dependencyResolver.Register<IDocCatRepository, DocCatRepository>();
            dependencyResolver.Register<IDocCatService, DocCatService>();
            dependencyResolver.Register<IRecordRepository, RecordRepository>();
            dependencyResolver.Register<IRecordService, RecordService>();
            dependencyResolver.Register<ILoginInfoRepository, LoginInfoRepository>();
            dependencyResolver.Register<ILoginInfoService, LoginInfoService>();
            dependencyResolver.Register<IMonthLoginCountRepository, MonthLoginCountRepository>();
            dependencyResolver.Register<IMonthLoginCountService, MonthLoginCountService>();
            DependencyResolver.SetResolver(dependencyResolver);
        }
    }
}