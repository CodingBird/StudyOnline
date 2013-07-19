using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class LoginInfo
    {
        public int Id { set; get; }
        public string UserName { set; get; }
        public string IpAddress { set; get; }
        public DateTime IpDateTime { set; get; }
    }
}
