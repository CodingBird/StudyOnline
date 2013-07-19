using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Infrastructure;
using StudyOnline.Models;
using BusinessLogical.Abstract;
using System.Web.Security;
using Model;


namespace StudyOnline.Areas.Admin.Controllers
{
    /// <summary>
    /// 处理网站管理员的身份验证请求
    /// </summary>
    public class AccountController : StudyOnlineController
    {
        public ILoginInfoService LoginInfoService { get; private set; }
        public IMonthLoginCountService MonthLoginCountService { get; private set; }
        public AccountController(ILoginInfoService loginInfoService,IMonthLoginCountService monthLoginCountService)
        {
            this.LoginInfoService = loginInfoService;
            this.MonthLoginCountService = monthLoginCountService;
            this.AddDisposableObject(monthLoginCountService);
            this.AddDisposableObject(loginInfoService);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            DealLoginInfo();    //要改为异步，不然登录时间太长
            
            if (!Roles.RoleExists("admin"))
            {
                Roles.CreateRole("admin");
            }
            if (Membership.FindUsersByName("admin").Count == 0)
            {
                Membership.CreateUser("admin", "123456");
                Roles.AddUserToRole("admin", "admin");
            }

            
            if (Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie("admin", false);
                RecordLoginInfo();
                return RedirectToAction("List", "Video", new { area="admin"});
            }
            return View();
            
        }
        /// <summary>
        /// 记录用户登录时的ip，时间等信息
        /// </summary>
        public void RecordLoginInfo()
        {
            this.LoginInfoService.AddLoginInfo(new LoginInfo
            {
                IpAddress = Request.UserHostAddress,
                IpDateTime = DateTime.Now,
                UserName = User.Identity.Name
            });
        }
        /// <summary>
        /// 1.统计上月访问量，存入表LoginCount
        /// 2.删除本月之前的12个月的访问ip信息，
        /// </summary>
        public void DealLoginInfo()
        {
            //当前月的初始日期
            DateTime firstDayOfThisMonth= new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
            DateTime start;
            DateTime end;
            int loginInfoCount;
            for (var i = 1; i <= 12; i++)  //处理过去一年的登录记录
            {
                start = firstDayOfThisMonth.AddMonths(-i);
                end = firstDayOfThisMonth.AddMonths(-i + 1) ;
                if (this.LoginInfoService.GetCount(end) == 0) //当前月之前不存在任何登陆记录,则退出循环
                {
                    break;  
                }
                loginInfoCount = this.LoginInfoService.GetCount(start,end);
                if (loginInfoCount != 0)   
                {
                    //统计该月访问来量，存入LoginCount表
                    MonthLoginCount monthLoginCount = new MonthLoginCount
                    {
                        Count = loginInfoCount,
                        YearMonth = start
                    };
                    this.MonthLoginCountService.AddMonthLoginCount(monthLoginCount);
                    //然后删除该月份记录
                    IEnumerable<LoginInfo> loginInfos = this.LoginInfoService.GetLoginInfoByTime(start,end);
                    this.LoginInfoService.DeleteLoginInfos(loginInfos);
                }
            }
        }

    }
}
