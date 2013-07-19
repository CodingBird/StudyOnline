using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class RecordRepository:RepositoryBase<RecordData>,IRecordRepository
    {
        public RecordRepository(StudyDbContext context)
            : base(context)
        { }
        public RecordData GetRedord(int id)
        {
            return  this.Get(r => r.Id == id).FirstOrDefault();
        }
        public IEnumerable<RecordData> GetRecordsByUserName(string userName, int pageSize, int pageIndex, out int recordCount)
        {
            recordCount = this.Count(r => r.UserName == userName);
            return this.Get(r => r.UserName == userName, pageSize, pageIndex, r => r.StartTime, false);
        }
        public IEnumerable<RecordData> GetAllReocrdsByUserName(string userName, out int recordCount)
        {
            recordCount = this.Count(r=>r.UserName == userName);
            return this.Get(r=>r.UserName==userName);
        }
        public IEnumerable<RecordData> GetRecords(int pageSize, int pageIndex, out int recordCount)
        {
            recordCount = this.Count(r=>true);
            return this.Get(r => true, pageSize, pageIndex, r => r.StartTime, false);
        }

        public void AddRecord(RecordData record)
        {
            this.Add(record);
        }
        public void DeleteRecord(RecordData record)
        {
            this.Delete(record);
        }
    }
}
