using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class VideoCatRepository:RepositoryBase<VideoCategory>,IVideoCatRepository
    {
        public VideoCatRepository(StudyDbContext context)
            : base(context)
        { }
        public IEnumerable<VideoCategory> GetVideoCategories()
        {
            return this.Get();
        }
        public void AddVideoCategory(VideoCategory videoCategory)
        {
            this.Add(videoCategory);
        }
        public void UpdateVideoCategory(VideoCategory videoCategory)
        {
            this.Update(videoCategory);
        }
        public void DeleteVideoCategory(VideoCategory videoCategory)
        {
            this.Delete(videoCategory);
        }
    }
}
