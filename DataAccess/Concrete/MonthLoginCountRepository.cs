using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class MonthLoginCountRepository:RepositoryBase<MonthLoginCount>,IMonthLoginCountRepository
    {
        public MonthLoginCountRepository(StudyDbContext context)
            : base(context)
        { }
        public IEnumerable<MonthLoginCount> GetMonthLoginCounts()
        {
            return this.Get();
        }
        public IEnumerable<MonthLoginCount> GetMonthLoginCounts(DateTime start, DateTime end)
        {
            return this.Get(l => l.YearMonth >= start && l.YearMonth < end);
        }
        public MonthLoginCount GetMonthLoginCount(DateTime month)
        {
            return this.Get(l => l.YearMonth == month).FirstOrDefault();
        }

        public void AddMonthLoginCount(MonthLoginCount monthLoginCount)
        {
            this.Add(monthLoginCount);
        }
    }
}
