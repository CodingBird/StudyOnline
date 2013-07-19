using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Infrastructure;
using BusinessLogical.Abstract;
using Model;
using StudyOnline.Models;

namespace StudyOnline.Areas.Admin.Controllers
{
    /// <summary>
    /// 后台管理：视频类别
    /// </summary>
    //[Authorize(Roles = "admin")]
    public class VideoCategoryController : StudyOnlineController
    {
        public IVideoCatService VideoCatService { get; private set; }
        public VideoCategoryController(IVideoCatService videoCatService)
        {
            this.VideoCatService = videoCatService;
            this.AddDisposableObject(videoCatService);
        }
        /// <summary>
        /// 获取视频类别，用于在左侧导航处显示
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            IEnumerable<VideoCategory> videoCats = VideoCatService.GetVideoCategories();
            return PartialView("_Menu",videoCats);
        }
        //public ActionResult ListForCheckBox()
        //{
        //    IEnumerable<VideoCategory> videoCats = VideoCatService.GetVideoCategories();
        //    return PartialView(videoCats);
        //}
        public ActionResult ListForDropdownList(string categoryId = "1")
        {
            ViewBag.CatId = categoryId;
            IEnumerable<VideoCategory> videoCats = VideoCatService.GetVideoCategories();
            return PartialView("_ListForDropdownList",videoCats);
        }
        public ActionResult List(int categoryId = 0)
        {
            ViewBag.EditCateId = categoryId;
            IEnumerable<VideoCategory> cats = this.VideoCatService.GetVideoCategories();
            return View(cats);
        }

        [HttpPost]
        public ActionResult Add(VideoCategoryInfo categoryInfo)
        {
            if (ModelState.IsValid)
            {
                this.VideoCatService.AddVideoCategory(VideoCategoryInfo.ToVideoCategory(categoryInfo));

            }
            return RedirectToAction("List");

        }
        [HttpPost]
        public ActionResult Edit(VideoCategoryInfo categoryInfo)
        {
            if (ModelState.IsValid)
            {
                VideoCategory category = VideoCategoryInfo.ToVideoCategory(categoryInfo);
                this.VideoCatService.UpdateVideoCategory(category);
            }
            return RedirectToAction("List");
        }
        public ActionResult Delete(int categoryId)
        {
            VideoCategory cat = new VideoCategory { Id = categoryId };
            this.VideoCatService.DeleteVideoCategory(cat);
            return RedirectToAction("List");
        }
    }
}
