using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Model;

namespace StudyOnline.Models
{
    public class VideoInfo
    {
        public int Id { set; get; }
        [Display(Name = "视频名称")]
        [Required]
        public string Name { set; get; }

        [Display(Name = "视频描述信息")]
        public string Description { set; get; }

        [Display(Name = "视频来源")]
        public string Origin { set; get; }

        [Display(Name = "上传时间")]
        public DateTime UploadTime { set; get; }


        [Display(Name = "播放次数")]
        public int PlayCount { set; get; }
        [Display(Name = "是否可见")]
        public bool CanView { set; get; }

        [Required(ErrorMessage = "请等待文件上传完成")]
        public string Url { set; get; }

        [Required(ErrorMessage = "请为视频文件指定类别")]
        public string CategoryId { set; get; }

        public static VideoInfo FromVideoData(VideoData video)
        {
            return new VideoInfo
            {
                Id = video.Id,
                Name = video.Name,
                Description = video.Description,
                Origin = video.Origin,
                UploadTime = video.UploadTime,
                PlayCount = video.PlayCount,
                CanView = video.CanView,
                Url = video.Url,
                CategoryId = video.VideoCategoryId.ToString()
            };
        }
        public static VideoData ToVideoData(VideoInfo videoInfo)
        {
            return new VideoData
            {
                Id = videoInfo.Id,
                Name = videoInfo.Name,
                Description = videoInfo.Description,
                Origin = videoInfo.Origin,
                UploadTime = videoInfo.UploadTime,
                CanView = videoInfo.CanView,
                Url = videoInfo.Url,
                VideoCategoryId = Convert.ToInt32(videoInfo.CategoryId)
            };
        }
    }
}