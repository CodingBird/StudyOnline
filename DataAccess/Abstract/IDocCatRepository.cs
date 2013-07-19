using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DataAccess.Abstract
{
    public interface IDocCatRepository
    {
        IEnumerable<DocCategory> GetCategories();

        void AddCategory(DocCategory category);
        void UpdateCategory(DocCategory category);
        void DeleteCategory(DocCategory category);
    }
}
