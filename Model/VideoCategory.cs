using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class VideoCategory
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }

        public ICollection<VideoData> VideoDatas { set; get; }
    }
}
