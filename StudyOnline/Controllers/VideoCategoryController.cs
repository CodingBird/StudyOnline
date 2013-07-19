using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Infrastructure;
using StudyOnline.Models;
using Model;
using BusinessLogical.Abstract;

namespace StudyOnline.Controllers
{
    /// <summary>
    /// 处理用户前台视频类别请求
    /// </summary>
    [Authorize]
    public class VideoCategoryController : StudyOnlineController
    {
        public IVideoCatService VideoCatService { get; private set; }
        public VideoCategoryController(IVideoCatService catService)
        {
            this.VideoCatService = catService;
            this.AddDisposableObject(catService);
        }

        public ActionResult Menu()
        {
            IEnumerable<VideoCategory> cats = this.VideoCatService.GetVideoCategories();
            return PartialView("_Menu",cats);
        }

    }
}
