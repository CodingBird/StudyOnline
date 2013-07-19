using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class VideoRepository:RepositoryBase<VideoData>,IVideoRepository
    {
        public VideoRepository(StudyDbContext context)
            : base(context)
        { }
        public IEnumerable<VideoData> GetVideos(int pageSize, int pageIndex, out int videoCount)
        {
            videoCount = this.DbSet.Count();
            return this.Get(v => true, pageSize, pageIndex, v => v.UploadTime, false);
        }
        /// <summary>
        /// 通过播放次数，获取播放次数较多的视频
        /// </summary>
        /// <param name="count">获取视频个数</param>
        /// <returns></returns>
        public IEnumerable<VideoData> GetVideosByPlayCount(int count)
        {
            return this.Get(v=>true,count,1,v=>v.PlayCount,false);
        }
        public IEnumerable<VideoData> GetVideosByCatId(int videoId, int pageSize, int pageIndex, out int videoCount)
        {
            videoCount = this.Count(v=>v.VideoCategoryId == videoId);
            return this.Get<DateTime>(v => v.VideoCategoryId == videoId, pageSize, pageIndex, v => v.UploadTime, false);
        }
        public VideoData GetVideo(int videoId)
        {
            return this.Get(v => v.Id == videoId).FirstOrDefault();
        }

        public void AddVideo(VideoData video)
        {
            this.Add(video);
        }
        public void UpdateVideo(VideoData video)
        {
            this.Update(video);
        }
        public void DeleteVideo(VideoData video)
        {
            this.Delete(video);
        }
    }
}
