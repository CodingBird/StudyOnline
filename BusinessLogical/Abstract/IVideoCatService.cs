using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace BusinessLogical.Abstract
{
    public interface IVideoCatService
    {
        IEnumerable<VideoCategory> GetVideoCategories();
        void AddVideoCategory(VideoCategory videoCategory);
        void UpdateVideoCategory(VideoCategory videoCategory);
        void DeleteVideoCategory(VideoCategory videoCategory);
    }
}
