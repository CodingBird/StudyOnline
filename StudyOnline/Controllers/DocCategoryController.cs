using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Infrastructure;
using BusinessLogical.Abstract;
using Model;

namespace StudyOnline.Controllers
{
    /// <summary>
    /// 用于处理用户前台的文档类别请求请求
    /// </summary>
    [Authorize]
    public class DocCategoryController : StudyOnlineController
    {
        public IDocCatService DocCatService { get; private set; }
        public DocCategoryController(IDocCatService docCatService)
        {
            this.DocCatService = docCatService;
            this.AddDisposableObject(docCatService);
        }

        public ActionResult Menu()
        {
            IEnumerable<DocCategory> cats = this.DocCatService.GetCategories();
            return PartialView("_Menu",cats);
        }

    }
}
