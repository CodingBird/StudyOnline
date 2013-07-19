using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.ComponentModel.DataAnnotations;

namespace StudyOnline.Models
{
    public class DocInfo
    {
        public int Id { set; get; }
        [Required]
        [Display(Name = "文档名称")]
        public string Name { set; get; }
        [Required]
        [Display(Name = "文档内容")]
        public string Content { set; get; }
        [Display(Name = "作者或来源")]
        public string Author { set; get; }


        [Display(Name = "上传时间")]
        public DateTime UploadTime { set; get; }
        [Display(Name = "点击次数")]
        public int ViewCount { set; get; }
        [Display(Name = "是否显示")]
        public bool CanView { set; get; }         //是否可见

        [Required(ErrorMessage = "请为文档指定类别")]
        public string DocCategoryId { set; get; }

        public static DocInfo FromDocData(DocData doc)
        {
            return new DocInfo
            {
                Id = doc.Id,
                Name = doc.Name,
                Content = doc.Content,
                Author = doc.Author,
                UploadTime = doc.UploadTime,
                ViewCount = doc.ViewCount,
                CanView = doc.CanView,
                DocCategoryId = doc.DocCategoryId.ToString()
            };
        }

        public static DocData ToDocData(DocInfo docInfo)
        {
            return new DocData
            {
                Id = docInfo.Id,
                Name = docInfo.Name,
                Content = docInfo.Content,
                Author = docInfo.Author,
                UploadTime = docInfo.UploadTime,
                ViewCount = docInfo.ViewCount,
                CanView = docInfo.CanView,
                DocCategoryId = Convert.ToInt32(docInfo.DocCategoryId)
            };
        }
    }
}