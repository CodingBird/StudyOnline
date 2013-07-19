using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogical.Abstract;
using Model;
using DataAccess.Abstract;

namespace BusinessLogical.Concrete
{
    public class DocCatService:ServiceBase,IDocCatService
    {
        public IDocCatRepository Repository { get; private set; }
        public DocCatService(IDocCatRepository repository)
        {
            this.Repository = repository;
            this.AddDisposableObjects(repository);
        }
        public IEnumerable<DocCategory> GetCategories()
        {
            return this.Repository.GetCategories();
        }

        public void AddCategory(DocCategory category)
        {
            this.Repository.AddCategory(category);
        }
        public void UpdateCategory(DocCategory category)
        {
            this.Repository.UpdateCategory(category);
        }
        public void DeleteCategory(DocCategory category)
        {
            this.Repository.DeleteCategory(category);
        }
    }
}
