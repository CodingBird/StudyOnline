using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using BusinessLogical.Abstract;
using DataAccess.Abstract;

namespace BusinessLogical.Concrete
{
    public class RecordService:ServiceBase,IRecordService
    {
        public IRecordRepository RecordRepository { get; private set; }
        public RecordService(IRecordRepository repository)
        {
            this.RecordRepository = repository;
            this.AddDisposableObjects(repository);
        }
        public RecordData GetRedord(int id)
        {
            return this.RecordRepository.GetRedord(id);
        }
        public IEnumerable<RecordData> GetRecordsByUserName(string userName, int pageSize, int pageIndex, out int recordCount)
        {
            return this.RecordRepository.GetRecordsByUserName(userName,pageSize,pageIndex,out recordCount);
        }
        public IEnumerable<RecordData> GetRecords(int pageSize, int pageIndex, out int recordCount)
        {
            return this.RecordRepository.GetRecords(pageSize,pageIndex,out recordCount);
        }

        public void AddRecord(RecordData record)
        {
            this.RecordRepository.AddRecord(record);
        }
        public void DeleteRecord(RecordData record)
        {
            this.RecordRepository.DeleteRecord(record);
        }
        public IEnumerable<RecordData> GetAllReocrdsByUserName(string userName, out int recordCount)
        {
            return this.RecordRepository.GetAllReocrdsByUserName(userName,out recordCount);
        }
    }
}
