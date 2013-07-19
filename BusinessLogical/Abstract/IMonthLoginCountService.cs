using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;


namespace BusinessLogical.Abstract
{
    public interface IMonthLoginCountService
    {
        IEnumerable<MonthLoginCount> GetMonthLoginCounts();
        IEnumerable<MonthLoginCount> GetMonthLoginCounts(DateTime start, DateTime end);
        int GetLoginCountPerMonth(DateTime month);
        int GetTotalLoginCount();
        void AddMonthLoginCount(MonthLoginCount monthLoginCount);
    }
}
