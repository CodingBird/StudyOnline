using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;
using BusinessLogical.Abstract;

namespace BusinessLogical.Concrete
{
    public class VideoCatService:ServiceBase,IVideoCatService
    {
        public IVideoCatRepository Repository { get; private set; }

        public VideoCatService(IVideoCatRepository repository)
        {
            this.Repository = repository;
            this.AddDisposableObjects(repository);
        }
        public IEnumerable<VideoCategory> GetVideoCategories()
        {
            return this.Repository.GetVideoCategories();
        }
        public void AddVideoCategory(VideoCategory videoCategory)
        {
            this.Repository.AddVideoCategory(videoCategory);
        }
        public void UpdateVideoCategory(VideoCategory videoCategory)
        {
            this.Repository.UpdateVideoCategory(videoCategory);
        }
        public void DeleteVideoCategory(VideoCategory videoCategory)
        {
            this.Repository.DeleteVideoCategory(videoCategory);
        }
    }
}
