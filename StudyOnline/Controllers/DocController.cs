using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Infrastructure;
using StudyOnline.Models;
using Model;
using BusinessLogical.Abstract;
using System.Configuration;

namespace StudyOnline.Controllers
{
    /// <summary>
    /// 处理前台用户的文档请求
    /// </summary>
    [Authorize]
    public class DocController : StudyOnlineController
    {

        public IDocService DocService { get; private set; }
        public IDocCatService DocCatService { get; private set; }
        public DocController(IDocService docService, IDocCatService docCatService)
        {
            this.DocService = docService;
            this.DocCatService = docCatService;
            this.AddDisposableObject(docService);
            this.AddDisposableObject(DocCatService);
        }
        #region  与文档列表呈现有关的action
        /// <summary>
        /// 根据页码获取文档列表，调用RenderDocList方法呈现视图
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult List(int pageIndex = 1)
        {
            int docCount;
            IEnumerable<DocInfo> docs = this.DocService.GetDocs(PagingInfo.PageSize, pageIndex, out docCount).Select(d => DocInfo.FromDocData(d));
            Func<int, string> pageUrl = (currentPage) => Url.Action("RenderDocTable", new { pageIndex = currentPage });
            return RenderDocList(pageIndex, docCount, pageUrl, docs);
        }
        /// <summary>
        /// 按类别和页码获取文档，调用RenderDocList方法呈现视图
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="categoryId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Genre(string categoryName, int categoryId, int pageIndex = 1)
        {
            int docCount;
            IEnumerable<DocInfo> docs = this.DocService.GetDocsByCatId(categoryId, PagingInfo.PageSize, pageIndex, out docCount)
                .Select(d => DocInfo.FromDocData(d));
            Func<int, string> pageUrl = (currentPage) => Url.Action("RenderGenreDocTable",
                new { pageIndex = currentPage, categoryName = categoryName, categoryId = categoryId });
            ViewBag.CurrentCategory = categoryName;
            ViewBag.CurrentCategoryUrl = Url.Action("Genre", new { categoryName = categoryName, categoryId = categoryId });
            return RenderDocList(pageIndex, docCount, pageUrl, docs);
        }
        /// <summary>
        /// 在”list“视图页，呈现文档视图
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="docCount"></param>
        /// <param name="pageUrl"></param>
        /// <param name="docs"></param>
        /// <returns></returns>
        private ActionResult RenderDocList(int pageIndex, int docCount, Func<int, string> pageUrl, IEnumerable<DocInfo> docs)
        {
            IEnumerable<DocCategory> cats = this.DocCatService.GetCategories();
            ViewData["cats"] = cats;
            ViewResult result = View("DocList", docs);
            ViewBag.PagingInfo = new PagingInfo
            {
                PageIndex = pageIndex,
                RecordCount = docCount
            };
            ViewBag.PageUrl = pageUrl;
            return result;
        }
        /// <summary>
        /// 接受ajax请求，获取文档，在分部视图”_DocTable“上呈现
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RenderDocTable(int pageIndex = 1)
        {
            int docCount;
            IEnumerable<DocInfo> docs = this.DocService.GetDocs(PagingInfo.PageSize, pageIndex, out docCount).Select(d => DocInfo.FromDocData(d));
            IEnumerable<DocCategory> cats = this.DocCatService.GetCategories();
            ViewData["cats"] = cats;
            return PartialView("_DocTable", docs);
        }
        /// <summary>
        /// 接受ajax请求，按类别获取文档，在分部视图”_DocTable“上呈现
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="categoryId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RenderGenreDocTable(string categoryName, int categoryId, int pageIndex = 1)
        {
            int docCount;
            IEnumerable<DocInfo> docs = this.DocService.GetDocsByCatId(categoryId, PagingInfo.PageSize, pageIndex, out docCount)
                .Select(d => DocInfo.FromDocData(d));
            IEnumerable<DocCategory> cats = this.DocCatService.GetCategories();
            ViewData["cats"] = cats;
            return PartialView("_DocTable", docs);
        }
        #endregion
        /// <summary>
        /// 文档详细信息，通过分部视图呈现
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public ActionResult Detail(int docId)
        {
            DocData doc = this.DocService.GetDoc(docId);
            if (doc != null)
            {
                AddViewCount(docId);
                doc.Name = Server.HtmlDecode(doc.Name);
                doc.Content = Server.HtmlDecode(doc.Content);
                return PartialView("_Detail", DocInfo.FromDocData(doc));
            }
            return PartialView("_Detail");
        }
        public void AddViewCount(int docId)
        {
            DocData doc = this.DocService.GetDoc(docId);
            doc.ViewCount += 1;
            this.DocService.UpdataDoc(doc);
        }
        /// <summary>
        /// 呈现最新上传文档
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderNewerDocs()
        {
            int docCount;
            int count = Convert.ToInt32(ConfigurationManager.AppSettings["itemCountToShowInHome"]);
            IEnumerable<DocData> docs = this.DocService.GetDocs(count, 1, out docCount);
            ViewBag.Title = "最新上传文档";
            return RenderDocsToHome(docs);
        }
        /// <summary>
        /// 呈现热门点击文档
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderHoterDocs()
        {
            int count = Convert.ToInt32(ConfigurationManager.AppSettings["itemCountToShowInHome"]);
            IEnumerable<DocData> docs = this.DocService.GetDocsByViewCount(count);
            ViewBag.Title = "热门点击文档";
            return RenderDocsToHome(docs);
        }
        public ActionResult RenderDocsToHome(IEnumerable<DocData> docs)
        {

            IEnumerable<DocCategory> cats = this.DocCatService.GetCategories();
            ViewBag.Categories = cats;
            return PartialView("_DocsInHome", docs);
        }
    }
}
