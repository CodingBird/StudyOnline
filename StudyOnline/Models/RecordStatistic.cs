using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;

namespace StudyOnline.Models
{
    public class RecordStatistic
    {
        public string ResourceName { private set; get; }
        public ResourceType ResourceType { private set; get; }
        public Double TotalHours { private set; get; }
        public Double TotalMinutes { private set; get; }

        public static IEnumerable<RecordStatistic> GetStatistics(IEnumerable<RecordData> recordDatas)
        {
            IList<RecordStatistic> Statistics = new List<RecordStatistic>();
            RecordStatistic statistic;
            foreach (var record in recordDatas)
            {
                statistic = Statistics.Where(s => s.ResourceType == record.ResourceType && s.ResourceName == record.ResourceName).FirstOrDefault();
                TimeSpan duration = record.EndTime - record.StartTime;
                if (statistic == null)  //尚未统计该条record
                {
                    statistic = new RecordStatistic();
                    statistic.ResourceName = record.ResourceName;
                    statistic.ResourceType = record.ResourceType;
                    statistic.ResourceType = record.ResourceType;

                    statistic.TotalHours = duration.TotalHours;
                    statistic.TotalMinutes = duration.TotalMinutes;
                    Statistics.Add(statistic);
                }
                else
                {
                    statistic.TotalHours += duration.TotalHours;
                    statistic.TotalMinutes += duration.TotalMinutes;
                }
            }
            return Statistics;
        }

    }
}