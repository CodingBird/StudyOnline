using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Infrastructure;
using BusinessLogical.Abstract;
using Model;
using StudyOnline.Models;
using System.IO;

namespace StudyOnline.Areas.Admin.Controllers
{
    /// <summary>
    /// 管理员后台，管理视频资源
    /// </summary>
    //[Authorize(Roles="admin")]
    public class VideoController : StudyOnlineController
    {
        public IVideoService VideoService { get; private set; }
        public IVideoCatService VideoCatService { get; private set; }

        public VideoController(IVideoService videoService, IVideoCatService videoCatService)
        {
            this.VideoService = videoService;
            this.VideoCatService = videoCatService;
            this.AddDisposableObject(videoService);
            this.AddDisposableObject(videoCatService);
        }
        #region 与视图"VideoList"有关的action
        /// <summary>
        /// 获取视频列表，调用RendVideoList方法呈现
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult List(int pageIndex = 1)
        {
            int videoCount;
            IEnumerable<VideoInfo> videos = this.VideoService.GetVideos(PagingInfo.PageSize, pageIndex, out videoCount).Select(v => VideoInfo.FromVideoData(v));
            Func<int,UrlHelper,string> pageUrlAccessor = (currentPage,helper)=>helper.Action("List",
                new { pageIndex = currentPage }).ToString();
            //Func<int, UrlHelper, string> pageUrlAccessor = (currentPage, helper) => helper.RouteUrl("AdminVideoPage",
            //    new { pageIndex = currentPage }).ToString();

            if (videos.Count() == 0 && pageIndex > 1)
            {
                return RedirectToRoute("AdminVideoPage", new { pageIndex = pageIndex - 1 });
            }

            return RenderVideoList(pageIndex, videoCount, pageUrlAccessor, videos);
        }
        /// <summary>
        /// 按类别获取视频列表，调用RendVideoList方法呈现
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="categoryId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Genre(string categoryName, int categoryId, int pageIndex = 1)
        {
            int videoCount;
            IEnumerable<VideoInfo> videos = this.VideoService.GetVideosByCatId(categoryId, PagingInfo.PageSize, pageIndex, out videoCount).Select(v => VideoInfo.FromVideoData(v));
            Func<int, UrlHelper, string> pageUrlAccessor = (currentPage, helper) => helper.Action("Genre",
                new { pageIndex = currentPage, CategoryId = categoryId, CategoryName = categoryName }).ToString();
            //Func<int, UrlHelper, string> pageUrlAccessor = (currentPage, helper) => helper.RouteUrl("AdminVideoGenrePage",
            //    new { pageIndex = currentPage, CategoryId = categoryId, CategoryName = categoryName }).ToString();
            ViewBag.Title = categoryName;
            if (videos.Count() == 0 && pageIndex > 1)
            {
                return RedirectToRoute("AdminVideoGenrePage", new { pageIndex = pageIndex - 1, categoryId = categoryId, categoryName = categoryName });
            }
            ViewBag.CurrentCategory = categoryName;
            //ViewBag.CurrentCategoryUrl = Url.RouteUrl("AdminVideoGenre", new { categoryName = categoryName, categoryId = categoryId });
            ViewBag.CurrentCategoryUrl = Url.Action("Genre", 
                new { categoryName = categoryName, categoryId = categoryId });
            return RenderVideoList(pageIndex, videoCount, pageUrlAccessor, videos);
        }
        /// <summary>
        /// 呈现视频列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="pageUrlAccessor"></param>
        /// <param name="videos"></param>
        /// <returns></returns>
        private ActionResult RenderVideoList(int pageIndex, int recordCount, Func<int, UrlHelper, string> pageUrlAccessor,
            IEnumerable<VideoInfo> videos)
        {


            IEnumerable<VideoCategory> cats = this.VideoCatService.GetVideoCategories();
            ViewBag.Categories = cats;
            ViewResult result = View("VideoList", videos);
            ViewBag.RecordCount = recordCount;
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageUrlAccessor = pageUrlAccessor;
            return result;
        }
        #endregion


        /// <summary>
        /// 呈现视频详细信息
        /// </summary>
        /// <param name="videoName"></param>
        /// <param name="videoId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult Detail(string videoName, int videoId, string returnUrl)
        {
            VideoData video = VideoService.GetVideo(videoId);
            if (null == video)
            {
                return new HttpNotFoundResult(string.Format("指定的视频资源\"{0}\"不存在", videoId));
            }
            ViewBag.returnUrl = returnUrl;
            ViewBag.CurrentCategory = videoName;
            //ViewBag.CurrentCategoryUrl = Url.RouteUrl("AdminVideoDetail", new { videoName = videoName, videoId = videoId, returnUrl = returnUrl });
            ViewBag.CurrentCategoryUrl = Url.Action("Detail",
                new { videoName = videoName, videoId = videoId, returnUrl = returnUrl });
            return View(VideoInfo.FromVideoData(video));
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(VideoInfo video)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                video.UploadTime = DateTime.Now;
                this.VideoService.AddVideo(VideoInfo.ToVideoData(video));
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int videoId)
        {
            VideoData video = this.VideoService.GetVideo(videoId);
            return PartialView("_Edit", VideoInfo.FromVideoData(video));
        }

        [HttpPost]
        public void Edit()
        {
            string videoName = Request["videoName"];
            string videoId = Request["videoId"];
            string catId = Request["catId"];
            string videoDsc = Request["videoDsc"] != null ? Request["videoDsc"] : "";
            if (string.IsNullOrEmpty(videoName))
            {
                Response.Write("视频名称不能为空");
                return;
            }
            if (string.IsNullOrEmpty(catId))
            {
                Response.Write("请选择视频类别");
                return;
            }
            if (!string.IsNullOrEmpty(videoId))
            {
                VideoData video = this.VideoService.GetVideo(Convert.ToInt32(videoId));
                if (video == null)
                {
                    Response.Write("该视频不存在");
                }
                video.Name = videoName;
                video.Description = videoDsc;
                video.VideoCategoryId = Convert.ToInt32(catId);
                this.VideoService.UpdateVideo(video);
                Response.Write("更新成功");
                return;
            }
            else
            {
                Response.Write("编辑视频信息时发生错误，请重试！");
                return;
            }

        }
        public void ValidateVideoInfo(string property, string errorMessage)
        {
            if (string.IsNullOrEmpty(property))
            {
                ViewBag.ErrorMessage = errorMessage;
            }
        }


        public ActionResult Delete(int videoId, string returnUrl)
        {
            VideoData video = this.VideoService.GetVideo(videoId);
            if (null != video)
            {
                this.VideoService.DeleteVideo(video);
            }
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToRoute("AdminVideoHome");
            }
            return Redirect(returnUrl);

        }



        [HttpPost]
        [AllowAnonymous]
        public JsonResult UploadVideo(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    string filePath = Server.MapPath("~/Uploads/video/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = Guid.NewGuid().ToString() + fileExtension; // 保存文件名称
                    fileData.SaveAs(filePath + saveName);

                    return Json(new { Success = true, FileName = fileName, SaveName = saveName });
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { Success = false, Message = "请选择要上传的文件" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
