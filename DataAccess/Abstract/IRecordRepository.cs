using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DataAccess.Abstract
{
    public interface IRecordRepository
    {
        RecordData GetRedord(int id);
        IEnumerable<RecordData> GetRecordsByUserName(string userName, int pageSize, int pageIndex, out int recordCount);
        IEnumerable<RecordData> GetRecords(int pageSize, int pageIndex, out int recordCount);
        IEnumerable<RecordData> GetAllReocrdsByUserName(string userName, out int recordCount);

        void AddRecord(RecordData record);
        void DeleteRecord(RecordData record);
    }
}
