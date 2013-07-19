using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace BusinessLogical.Abstract
{
    public interface IVideoService
    {
        IEnumerable<VideoData> GetVideos(int pageSize, int pageIndex, out int videoCount);
        IEnumerable<VideoData> GetVideosByCatId(int videoId, int pageSize, int pageIndex, out int videoCount);
        VideoData GetVideo(int videoId);
        IEnumerable<VideoData> GetVideosByPlayCount(int count);

        void AddVideo(VideoData video);
        void UpdateVideo(VideoData video);
        void DeleteVideo(VideoData video);
    }
}
