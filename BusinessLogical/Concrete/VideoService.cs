using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogical.Abstract;
using DataAccess.Abstract;
using Model;
using DataAccess.Concrete;
using DataAccess;
using System.Data.Entity;

namespace BusinessLogical.Concrete
{
    public class VideoService:ServiceBase,IVideoService
    {
        public IVideoRepository VideoRepository { get; private set; }
        
        public VideoService(IVideoRepository videoRepository)
        {
            this.VideoRepository = videoRepository;
            this.AddDisposableObjects(videoRepository);
        }

        public IEnumerable<VideoData> GetVideos(int pageSize, int pageIndex, out int videoCount)
        {
            return this.VideoRepository.GetVideos(pageSize,pageIndex,out videoCount);
        }
        public IEnumerable<VideoData> GetVideosByCatId(int videoId, int pageSize, int pageIndex, out int videoCount)
        {
            return this.VideoRepository.GetVideosByCatId(videoId, pageSize, pageIndex, out videoCount);
        }
        public VideoData GetVideo(int videoId)
        {
            return this.VideoRepository.GetVideo(videoId);
        }
        /// <summary>
        /// 通过播放次数，获取播放次数较多的视频
        /// </summary>
        /// <param name="count">获取视频个数</param>
        /// <returns></returns>
        public IEnumerable<VideoData> GetVideosByPlayCount(int count)
        {
            return this.VideoRepository.GetVideosByPlayCount(count);
        }
        public void AddVideo(VideoData video)
        {
            this.VideoRepository.AddVideo(video);
        }
        public void UpdateVideo(VideoData video)
        {
            this.VideoRepository.UpdateVideo(video);
        }
        public void DeleteVideo(VideoData video)
        {
            this.VideoRepository.DeleteVideo(video);
        }
    }
}
