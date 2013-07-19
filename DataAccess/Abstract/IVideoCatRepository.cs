using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DataAccess.Abstract
{
    public interface IVideoCatRepository
    {
        IEnumerable<VideoCategory> GetVideoCategories();
        void AddVideoCategory(VideoCategory videoCategory);
        void UpdateVideoCategory(VideoCategory videoCategory);
        void DeleteVideoCategory(VideoCategory videoCategory);
    }
}
