using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class DocCatRepository:RepositoryBase<DocCategory>,IDocCatRepository
    {
        public DocCatRepository(StudyDbContext context)
            : base(context)
        { }
        public IEnumerable<DocCategory> GetCategories()
        {
            return this.Get();
        }

        public void AddCategory(DocCategory category)
        {
            this.Add(category);
        }
        public void UpdateCategory(DocCategory category)
        {
            this.Update(category);
        }
        public void DeleteCategory(DocCategory category)
        {
            this.Delete(category);
        }

    }
}
