using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DataAccess.Abstract
{
    public interface IDocRepository
    {
        IEnumerable<DocData> GetDocs(int pageSize, int pageIndex, out int docCount);
        IEnumerable<DocData> GetDocsByCatId(int catId, int pageSize, int pageIndex, out int docCount);
        DocData GetDoc(int docId);
        IEnumerable<DocData> GetDocsByViewCount(int count);

        void AddDoc(DocData doc);
        void UpdataDoc(DocData doc);
        void DeleteDoc(DocData doc);
        
    }
}
