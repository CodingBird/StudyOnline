using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Infrastructure;
using BusinessLogical.Abstract;
using StudyOnline.Models;
using Model;
using System.Web.Security;

namespace StudyOnline.Controllers
{
    /// <summary>
    /// 处理用户的身份验证请求
    /// </summary>
    public class AccountController : StudyOnlineController
    {
        public ILoginInfoService LoginInfoService { get; private set; }
        public IMonthLoginCountService MonthLoginCountService { get; private set; }
        public AccountController(ILoginInfoService loginInfoService, IMonthLoginCountService monthLoginCountService)
        {
            this.LoginInfoService = loginInfoService;
            this.MonthLoginCountService = monthLoginCountService;
            this.AddDisposableObject(monthLoginCountService);
            this.AddDisposableObject(loginInfoService);
        }

        public ActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.ToLower().Contains("admin")) //如果路由中包含”admin“,导向管理员登陆页
            {
                return RedirectToAction("Login", "Account", new {area="admin", returnUrl = returnUrl });
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                if (model.RememberMe)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                }
                RecordLoginInfo();
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("index", "Home");
                }
                return Redirect(returnUrl);
            }
            ModelState.AddModelError("", "用户名或密码不正确");
            return View(model);
        }
        /// <summary>
        /// 记录用户登录时的ip，时间等信息
        /// </summary>
        public void RecordLoginInfo()
        {
            for (int i = 0; i < 1000; i++)
            {
                this.LoginInfoService.AddLoginInfo(new LoginInfo
                {
                    IpAddress = Request.UserHostAddress,
                    IpDateTime = DateTime.Now.AddMonths(-2),
                    UserName = User.Identity.Name
                });
            }
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult RenderTotalLoginCount()
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int loginCountOfCurrentMonth = this.LoginInfoService.GetCount(firstDay, DateTime.Now);
            ViewData["totalLoginCount"] = this.MonthLoginCountService.GetTotalLoginCount() + loginCountOfCurrentMonth;
            return PartialView("_TotalLoginCount");
        }
    }
}
