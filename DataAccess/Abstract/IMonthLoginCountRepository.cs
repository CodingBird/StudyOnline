using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;


namespace DataAccess.Abstract
{
    public interface IMonthLoginCountRepository
    {
        IEnumerable<MonthLoginCount> GetMonthLoginCounts();
        IEnumerable<MonthLoginCount> GetMonthLoginCounts(DateTime start, DateTime end);
        MonthLoginCount GetMonthLoginCount(DateTime month);
        void AddMonthLoginCount(MonthLoginCount monthLoginCount);
    }
}
