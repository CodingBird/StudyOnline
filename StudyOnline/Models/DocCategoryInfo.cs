using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.ComponentModel.DataAnnotations;

namespace StudyOnline.Models
{
    public class DocCategoryInfo
    {
        public int Id { set; get; }
        [Display(Name = "类别名称")]
        [Required(ErrorMessage = "标题不能为空")]
        public string Name { set; get; }
        [Display(Name = "描述信息")]
        public string Description { set; get; }

        public static DocCategory ToDocCategory(DocCategoryInfo categoryInfo)
        {
            return new DocCategory
            {
                Id = categoryInfo.Id,
                Name = categoryInfo.Name,
                Description = categoryInfo.Description
            };
        }
        public static DocCategoryInfo FromDocCategory(DocCategory category)
        {
            return new DocCategoryInfo
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}