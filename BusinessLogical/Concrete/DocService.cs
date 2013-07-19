using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogical.Abstract;
using DataAccess.Abstract;
using Model;


namespace BusinessLogical.Concrete
{
    public class DocService:ServiceBase,IDocService
    {
        public IDocRepository Repository { get; private set; }
        public DocService(IDocRepository repository)
        {
            this.Repository = repository;
            this.AddDisposableObjects(repository);
        }
        public IEnumerable<DocData> GetDocsByViewCount(int count)
        {
            return this.Repository.GetDocsByViewCount(count);
        }
        public IEnumerable<DocData> GetDocs(int pageSize, int pageIndex, out int docCount)
        {
            return this.Repository.GetDocs(pageSize, pageIndex, out docCount);
        }
        public IEnumerable<DocData> GetDocsByCatId(int catId, int pageSize, int pageIndex, out int docCount)
        {
            return this.Repository.GetDocsByCatId(catId, pageSize, pageIndex, out docCount);
        }
        public DocData GetDoc(int docId)
        {
            return this.Repository.GetDoc(docId);
        }

        public void AddDoc(DocData doc)
        {
            this.Repository.AddDoc(doc);
        }
        public void UpdataDoc(DocData doc)
        {
            this.Repository.UpdataDoc(doc);
        }
        public void DeleteDoc(DocData doc)
        {
            this.Repository.DeleteDoc(doc);
        }
    }
}
