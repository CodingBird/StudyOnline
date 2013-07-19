using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class VideoData
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Origin { set; get; }   //视频来源
        public string Description { set; get; }
        public string Url { set; get; }
        public int PlayCount { set; get; }       //播放次数
        public bool CanView { set; get; }         //是否可见
        public DateTime UploadTime { set; get; }

        public int VideoCategoryId { set; get; }
        public VideoCategory VideoCategory { set; get; }
    }
}
