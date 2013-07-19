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
    /// 后台管理：管理文档
    /// </summary>
    //[Authorize(Roles = "admin")]
    public class DocController : StudyOnlineController
    {
        public IDocService DocService { get; private set; }
        public IDocCatService DocCategoryService { get; private set; }
        public DocController(IDocService docService, IDocCatService docCatService)
        {
            this.DocService = docService;
            this.DocCategoryService = docCatService;
            this.AddDisposableObject(docService);
        }

        #region 与呈现文档列表有关的action
        public ActionResult List(int pageIndex = 1)
        {
            int docCount;
            IEnumerable<DocInfo> docs = this.DocService.GetDocs(PagingInfo.PageSize, pageIndex, out docCount).Select(d => DocInfo.FromDocData(d));
            Func<int, UrlHelper, string> pageUrlAccessor = (currentPage, helper) => helper.Action("List", new { pageIndex = currentPage }).ToString();
            if (docs.Count() == 0 && pageIndex > 1)
            {
                return RedirectToRoute("AdminDocPage", new { pageIndex = pageIndex - 1 });
            }
            return RendDocList(pageIndex, docCount, pageUrlAccessor, docs);
        }

        public ActionResult Genre(string categoryName, int categoryId, int pageIndex = 1)
        {
            int docCount;
            IEnumerable<DocInfo> docs = this.DocService.GetDocsByCatId(categoryId, PagingInfo.PageSize, pageIndex, out docCount)
                .Select(d => DocInfo.FromDocData(d));
            Func<int, UrlHelper, string> pageUrlAccessor = (currentPage, helper) => helper.Action("Genre",
                new { pageIndex = currentPage, categoryId = categoryId, categoryName = categoryName }).ToString();

            if (docs.Count() == 0 && pageIndex > 1)
            {
                return RedirectToRoute("AdminDocGenrePage", new { pageIndex = pageIndex - 1, categoryId = categoryId, categoryName = categoryName });
            }
            ViewBag.CurrentCategory = categoryName;
            ViewBag.CurrentCategoryUrl = Url.Action("Genre", new { categoryName = categoryName, categoryId = categoryId });
            return RendDocList(pageIndex, docCount, pageUrlAccessor, docs);
        }

        public ActionResult RendDocList(int pageIndex, int docCount, Func<int, UrlHelper, string> pageUrlAccessor, IEnumerable<DocInfo> docs)
        {
            IEnumerable<DocCategory> cats = this.DocCategoryService.GetCategories();
            ViewResult result = View("DocList", docs);
            ViewBag.PageIndex = pageIndex;
            ViewBag.DocCount = docCount;
            ViewBag.PageUrlAccessor = pageUrlAccessor;
            ViewBag.Categories = cats;
            return result;
        }
        #endregion

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(DocInfo docInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(docInfo);
            }
            docInfo.Name = Server.HtmlEncode(docInfo.Name);
            docInfo.Content = Server.HtmlEncode(docInfo.Content);
            docInfo.UploadTime = DateTime.Now;
            DocData doc = DocInfo.ToDocData(docInfo);
            this.DocService.AddDoc(doc);
            return RedirectToAction("List");
        }
        public ActionResult Detail(int docId, string docName, string returnUrl)
        {
            DocData doc = this.DocService.GetDoc(docId);
            if (doc != null)
            {
                doc.Name = Server.HtmlDecode(doc.Name);
                doc.Content = Server.HtmlDecode(doc.Content);
            }
            ViewBag.CurrentCategory = docName;
            ViewBag.CurrentCategoryUrl = Url.Action("Detail", new { docName = docName, docId = docId, returnUrl = returnUrl });
            return View(DocInfo.FromDocData(doc));
        }
        public ActionResult Edit(int docId, string docName,string returnUrl)
        {
            DocData doc = this.DocService.GetDoc(docId);
            doc.Content = Server.HtmlDecode(doc.Content);
            ViewBag.CurrentCategory = docName;
            ViewBag.CurrentCategoryUrl = Url.Action("Edit", new { docName = docName, docId = docId, returnUrl = returnUrl });
            return View(DocInfo.FromDocData(doc));
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(DocInfo docInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(docInfo);
            }
            docInfo.Name = Server.HtmlEncode(docInfo.Name);
            docInfo.Content = Server.HtmlEncode(docInfo.Content);
            DocData doc = DocInfo.ToDocData(docInfo);
            this.DocService.UpdataDoc(doc);
            return RedirectToAction("List");
        }
        public ActionResult Delete(int docId, string returnUrl)
        {
            DocData doc = this.DocService.GetDoc(docId);
            if (doc != null)
            {
                this.DocService.DeleteDoc(doc);
            }
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToRoute("AdminDocHome");
            }
            return Redirect(returnUrl);

        }

        

    }
}
