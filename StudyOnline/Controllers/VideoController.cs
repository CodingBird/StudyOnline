using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Infrastructure;
using Model;
using BusinessLogical.Abstract;
using StudyOnline.Models;
using System.Configuration;


namespace StudyOnline.Controllers
{
    /// <summary>
    /// 处理用户前台视频请求
    /// </summary>
    [Authorize]
    public class VideoController : StudyOnlineController
    {
        public IVideoService VideoService { get; private set; }
        public IVideoCatService VideoCatService { get; private set; }
        public VideoController(IVideoService videoService, IVideoCatService catService)
        {
            this.VideoService = videoService;
            this.VideoCatService = catService;
            this.AddDisposableObject(VideoCatService);
            this.AddDisposableObject(videoService);
        }
        #region 视频列表呈现
        /// <summary>
        /// 获取视频列表，调用RenderVideoList方法呈现
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult List(int pageIndex = 1)
        {
            int videoCount;
            IEnumerable<VideoInfo> videos = this.VideoService.GetVideos(PagingInfo.PageSize, pageIndex, out videoCount)
                .Select(v => VideoInfo.FromVideoData(v));
            Func<int, string> pageUrl = (currentPage) => Url.Action("RenderVideoTable", new { pageIndex = currentPage });
            return RenderVideoList(pageIndex, videoCount, pageUrl, videos);
        }
        /// <summary>
        /// 按类别获取视频列表，调用RenderVideoList方法呈现
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="categoryId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult Genre(string categoryName, int categoryId, int pageIndex = 1)
        {
            int videoCount;
            IEnumerable<VideoInfo> videos = this.VideoService.GetVideosByCatId(categoryId, PagingInfo.PageSize, pageIndex, out videoCount)
                .Select(v => VideoInfo.FromVideoData(v));
            Func<int, string> pageUrl = (currentPage) => Url.Action("RenderGenreVideoTable",
                new { pageIndex = currentPage, categoryId = categoryId, categoryName = categoryName });
            ViewBag.CurrentCategory = categoryName;
            ViewBag.CurrentCategoryUrl = Url.Action("Genre", new { categoryName = categoryName, categoryId = categoryId });
            return RenderVideoList(pageIndex, videoCount, pageUrl, videos);
        }
        /// <summary>
        /// 在视图”VideoList“中呈现视频
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="videoCount"></param>
        /// <param name="pageUrl"></param>
        /// <param name="videos"></param>
        /// <returns></returns>
        public ActionResult RenderVideoList(int pageIndex, int videoCount, Func<int, string> pageUrl, IEnumerable<VideoInfo> videos)
        {
            IEnumerable<VideoCategory> cats = this.VideoCatService.GetVideoCategories();
            ViewData["videoCats"] = cats;
            ViewBag.PagingInfo = new PagingInfo
            {
                PageIndex = pageIndex,
                RecordCount = videoCount
            };
            ViewBag.PageUrl = pageUrl;
            return View("VideoList", videos);
        }
        /// <summary>
        /// 根据ajax请求，获取视频列表并在分部视图中呈现
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RenderVideoTable(int pageIndex = 1)
        {
            int videoCount;
            IEnumerable<VideoInfo> videos = this.VideoService.GetVideos(PagingInfo.PageSize, pageIndex, out videoCount)
                .Select(v => VideoInfo.FromVideoData(v));
            IEnumerable<VideoCategory> cats = this.VideoCatService.GetVideoCategories();
            ViewData["videoCats"] = cats;
            return PartialView("_VideoTable", videos);
        }
        /// <summary>
        /// 根据ajax请求，按类别获取视频列表并在分部视图中呈现
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="categoryId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RenderGenreVideoTable(string categoryName, int categoryId, int pageIndex = 1)
        {
            int videoCount;
            IEnumerable<VideoInfo> videos = this.VideoService.GetVideosByCatId(categoryId, PagingInfo.PageSize, pageIndex, out videoCount)
                .Select(v => VideoInfo.FromVideoData(v));
            IEnumerable<VideoCategory> cats = this.VideoCatService.GetVideoCategories();
            ViewData["videoCats"] = cats;
            return PartialView("_VideoTable", videos);
        }
        #endregion
        /// <summary>
        /// 获取视频的详细信息，通过分部视图呈现
        /// </summary>
        /// <param name="videoId"></param>
        /// <returns></returns>
        public ActionResult Detail(int videoId)
        {
            VideoData video = this.VideoService.GetVideo(videoId);
            if (null != video)
            {
                AddPlayCount(videoId);
                VideoInfo videoInfo = VideoInfo.FromVideoData(video);
                return PartialView("_Detail", videoInfo);
            }
            return PartialView("_Detail");
        }
        /// <summary>
        /// 增加视频的播放次数
        /// </summary>
        /// <param name="videoId">视频id</param>
        public void AddPlayCount(int videoId)
        {
            VideoData video = this.VideoService.GetVideo(videoId);
            video.PlayCount += 1;
            this.VideoService.UpdateVideo(video);
        }
        /// <summary>
        /// 呈现最新上传的视频
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderNewerVideos()
        {
            int videoCount;
            int count = Convert.ToInt32(ConfigurationManager.AppSettings["itemCountToShowInHome"]);
            IEnumerable<VideoData> videos = this.VideoService.GetVideos(count, 1, out videoCount);
            ViewBag.Title = "最新上传视频";
            return RenderVideosToHome(videos);
        }
        /// <summary>
        /// 呈现播放次数最多的视频
        /// </summary>
        /// <returns></returns>
        public ActionResult RenderHoterVideos()
        {
            int count = Convert.ToInt32(ConfigurationManager.AppSettings["itemCountToShowInHome"]);
            IEnumerable<VideoData> videos = this.VideoService.GetVideosByPlayCount(count);
            ViewBag.Title = "热门点击视频";
            return RenderVideosToHome(videos);
        }
        public ActionResult RenderVideosToHome(IEnumerable<VideoData> videos)
        {
            IEnumerable<VideoCategory> cats = this.VideoCatService.GetVideoCategories();
            ViewBag.Categories = cats;
            return PartialView("_VideosInHome", videos);
        }

    }
}
