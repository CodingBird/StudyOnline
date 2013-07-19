using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using StudyOnline.Infrastructure;
using BusinessLogical.Abstract;
using StudyOnline.Models;

namespace StudyOnline.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理：文档类别
    /// </summary>
   // [Authorize(Roles = "admin")]
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
        public ActionResult ListForDropdownList(string catId = "1")
        {
            ViewBag.CatId = catId;
            IEnumerable<DocCategory> cats = this.DocCatService.GetCategories();
            return PartialView("_ListForDropdownList",cats);
        }
        public ActionResult List(int categoryId = 0)
        {
            ViewBag.EditCategoryId = categoryId;
            IEnumerable<DocCategory> cats = this.DocCatService.GetCategories();
            return View(cats);
        }

        public ActionResult Add(DocCategoryInfo categoryInfo)
        {
            if (ModelState.IsValid)
            {
                DocCategory category = DocCategoryInfo.ToDocCategory(categoryInfo);
                this.DocCatService.AddCategory(category);
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(DocCategoryInfo categoryInfo)
        {
            if (ModelState.IsValid)
            {
                DocCategory category = DocCategoryInfo.ToDocCategory(categoryInfo);
                this.DocCatService.UpdateCategory(category);
            }
            return RedirectToAction("List");
        }
        public ActionResult Delete(int categoryId)
        {
            DocCategory cat = new DocCategory { Id = categoryId };
            this.DocCatService.DeleteCategory(cat);
            return RedirectToAction("List");
        }
    }
}
