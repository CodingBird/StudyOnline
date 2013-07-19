using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MonthLoginCount
    {
        public int Id { set; get; }
        public DateTime YearMonth { set; get; }
        public int Count { get; set; }
    }
}
