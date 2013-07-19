using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using System.Data.OleDb;
using StudyOnline.Models;
using System.IO;

namespace StudyOnline.Areas.Admin.Controllers
{
    //[Authorize(Roles = "admin")]
    public class UserManageController : Controller
    {
        /// <summary>
        /// 获取用户列表，如果参数userName不为空，则根据userName查询用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult List(string userName, int pageIndex = 1)
        {
            MembershipUserCollection users;
            int userCount;
            if (string.IsNullOrEmpty(userName))
            {
                users = Membership.GetAllUsers(pageIndex - 1, PagingInfo.PageSize, out userCount);
            }
            else
            {
                users = Membership.FindUsersByName(userName);
                userCount = 1;
            }
            Func<int, UrlHelper, string> pageUrlAccessor = (currentPage, helper) => helper.Action("List", new { pageIndex = currentPage }).ToString();
            ViewBag.PageUrlAccessor = pageUrlAccessor;
            ViewBag.PageIndex = pageIndex;
            ViewBag.UserCount = userCount;
            return View("UserList", users);
        }
        public ActionResult Add()
        {
            return View();
        }
        /// <summary>
        /// 注册添加单个用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                CreateMember(model.UserName, model.Password);
                ModelState.AddModelError("", "用户" + model.UserName + "添加成功!");
                return View(model);
            }

            return View();
        }
        public ActionResult AddUsers()
        {
            return View("Add");
        }

       
        /// <summary>
        /// 读取上传的excel文件，将其中的“username”列作为添加用户的用户名和密码
        /// </summary>
        /// <param name="file">上传的excel文件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddUsers(HttpPostedFileBase file)
        {
            ViewBag.ActivieTab = "addUsers";
            if (file == null)
            {
                ModelState.AddModelError("", "请选择要上传的文件");
                return View("Add");
            }
            string fileName = Path.GetFileName(file.FileName);// 原始文件名称
            string fileExtension = Path.GetExtension(fileName); // 文件扩展名
            if (fileExtension != ".xls")
            {
                ModelState.AddModelError("", "上传的文件格式必须为\".xls\"");
                return View("Add");
            }
            string saveName;
            if (SaveFile(file, out saveName))
            {
                DataSet ds = ExcelToDS(saveName);
                DataRow[] drs = ds.Tables[0].Select();//定义一个DataRow数组
                int rowsnum = ds.Tables[0].Rows.Count;
                //rowsnum = drs.Length;
                if (rowsnum == 0)
                {
                    ModelState.AddModelError("", "上传的excel不能为空"); //当Excel表为空时,对用户进行提示
                    return View("Add");
                }
                else
                {
                    string userName = "";
                    foreach (var dr in drs)
                    {
                        try
                        {
                            userName = dr["username"].ToString();
                            CreateMember(userName, userName);
                        }
                        catch (ArgumentException e)
                        {
                            ModelState.AddModelError("", "excel表格式不正确，第一行请填\"username\"");
                            return View("Add");
                        }
                    }
                }
            }

            return View("Add");
        }
        public void CreateMember(string userName, string password)
        {
            try
            {
                if (!Roles.RoleExists("member"))
                {
                    Roles.CreateRole("member");
                }
                if (!string.IsNullOrEmpty(userName))
                {
                    Membership.CreateUser(userName, password);
                }

                Roles.AddUserToRole(userName, "member");
            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError("", ErrorCodeToString(e.StatusCode, userName));
            }
        }
        public ActionResult Delete(string userName, string returnUrl)
        {
            Membership.DeleteUser(userName, true);
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("List");
            }
            return Redirect(returnUrl);
        }
        /// <summary>
        /// 返回管理员设置视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Setting()
        {
            return View();
        }
        /// <summary>
        /// 修改呢管理员密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ChangeAdminPassword(LocalPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser user = Membership.GetUser(User.Identity.Name);
                    user.ChangePassword(model.OldPassword, model.NewPassword);
                    Membership.UpdateUser(user);
                    changePasswordSucceeded = true;
                }
                catch
                {
                    changePasswordSucceeded = false;
                }
                if (changePasswordSucceeded)
                {
                    ModelState.AddModelError("", "密码修改成功。");
                }
                else
                {
                    ModelState.AddModelError("", "当前密码不正确或新密码无效。");
                }
            }
            return View("Setting");
        }
        private static string ErrorCodeToString(MembershipCreateStatus createStatus, string userName)
        {
            // 请参见 http://go.microsoft.com/fwlink/?LinkID=177550 以查看
            // 状态代码的完整列表。
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名" + userName + "已存在。请输入其他用户名。";

                //case MembershipCreateStatus.DuplicateEmail:
                //    return "该电子邮件地址的用户名已存在。请输入其他电子邮件地址。";

                case MembershipCreateStatus.InvalidPassword:
                    //return "提供的密码无效。请输入有效的密码值。";
                    return "用户" + userName + "名较短，无法用作原始密码，至少为6位";

                //case MembershipCreateStatus.InvalidEmail:
                //    return "提供的电子邮件地址无效。请检查该值并重试。";

                //case MembershipCreateStatus.InvalidAnswer:
                //    return "提供的密码取回答案无效。请检查该值并重试。";

                //case MembershipCreateStatus.InvalidQuestion:
                //    return "提供的密码取回问题无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidUserName:
                    return "提供的用户名" + userName + "无效。请检查该值并重试。";

                case MembershipCreateStatus.ProviderError:
                    return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                case MembershipCreateStatus.UserRejected:
                    return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                default:
                    return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
            }
        }
        public bool SaveFile(HttpPostedFileBase file, out string saveName)
        {
            string filePath = Server.MapPath("~/Uploads/excelfile/");
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string fileName = file.FileName;
                saveName = filePath + file.FileName;
                file.SaveAs(saveName);

                return true;
            }
            catch (Exception ex)
            {
                saveName = "";
                return false;
            }

        }

        public DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds;
        }
    }
}
