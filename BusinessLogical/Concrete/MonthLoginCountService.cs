using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;
using BusinessLogical.Abstract;

namespace BusinessLogical.Concrete
{
    public class MonthLoginCountService:ServiceBase,IMonthLoginCountService
    {
        public IMonthLoginCountRepository Repository { get; private set; }
        public MonthLoginCountService(IMonthLoginCountRepository repository)
        {
            this.Repository = repository;
            this.AddDisposableObjects(repository);
        }
        public IEnumerable<MonthLoginCount> GetMonthLoginCounts()
        {
            return this.Repository.GetMonthLoginCounts();
        }
        public IEnumerable<MonthLoginCount> GetMonthLoginCounts(DateTime start, DateTime end)
        {
            return this.Repository.GetMonthLoginCounts(start,end);
        }
        public int GetLoginCountPerMonth(DateTime month)
        {
            return this.Repository.GetMonthLoginCount(month).Count;
        }
        /// <summary>
        /// 本月之前的所有访问量
        /// </summary>
        /// <returns></returns>
        public int GetTotalLoginCount()
        {
            return this.Repository.GetMonthLoginCounts().Distinct().Sum(l => l.Count);
        }
        public void AddMonthLoginCount(MonthLoginCount monthLoginCount)
        {
            this.Repository.AddMonthLoginCount(monthLoginCount);
        }
    }
}
