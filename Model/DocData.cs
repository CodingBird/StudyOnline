using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class DocData
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Author { set; get; }  //作者或来源
        public string Content { set; get; }
        public DateTime UploadTime { set; get; }
        public int ViewCount { set; get; }
        public bool CanView { set; get; }         //是否可见

        public int DocCategoryId { set; get; }
        public DocCategory DocCategory { set; get; }
    }
}
