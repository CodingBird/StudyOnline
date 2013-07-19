using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class DocRepository:RepositoryBase<DocData>,IDocRepository
    {
        public DocRepository(StudyDbContext context)
            : base(context)
        { }

        public IEnumerable<DocData> GetDocsByViewCount(int count)
        {
            return this.Get(d => true, count, 1, d => d.ViewCount, false);
        }
        public IEnumerable<DocData> GetDocs(int pageSize, int pageIndex, out int docCount)
        {
            docCount = this.Count(d=>true);
            return this.Get(d => true, pageSize, pageIndex, d => d.UploadTime, false);
        }
        public IEnumerable<DocData> GetDocsByCatId(int catId, int pageSize, int pageIndex, out int docCount)
        {
            docCount = this.Count(d=>d.DocCategoryId == catId);
            return this.Get(d => d.DocCategoryId == catId, pageSize, pageIndex, d => d.UploadTime, false);
        }
        public DocData GetDoc(int docId)
        {
            return this.Get(d => d.Id == docId).FirstOrDefault();
        }

        public void AddDoc(DocData doc)
        {
            this.Add(doc);
        }
        public void UpdataDoc(DocData doc)
        {
            this.Update(doc);
        }
        public void DeleteDoc(DocData doc)
        {
            this.Delete(doc);
        }
    }
}
