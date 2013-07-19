using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace StudyOnline.Models
{
    public class PagingInfo
    {
        public static int PageSize
        {
            get { return int.Parse(ConfigurationManager.AppSettings["pageSize"]); }
        }

        public int PageIndex { set; get; }

        public int RecordCount { set; get; }

        public int PageCount
        {
            get { return (int)Math.Ceiling((decimal)RecordCount / PageSize); }
        }
    }
}