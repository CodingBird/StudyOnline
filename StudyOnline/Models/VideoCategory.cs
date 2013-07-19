using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Model;

namespace StudyOnline.Models
{
    public class VideoCategoryInfo
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "标题不能为空")]
        [Display(Name = "标题")]
        public string Name { set; get; }
        [Display(Name = "描述信息")]
        public string Description { set; get; }

        public static VideoCategory ToVideoCategory(VideoCategoryInfo categoryInfo)
        {
            return new VideoCategory
            {
                Id = categoryInfo.Id,
                Name = categoryInfo.Name,
                Description = categoryInfo.Description
            };
        }
        public static VideoCategoryInfo FromVideoCategory(VideoCategory category)
        {
            return new VideoCategoryInfo
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}