using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;

namespace DataAccess.Concrete
{
    public class LoginInfoRepository:RepositoryBase<LoginInfo>,ILoginInfoRepository
    {
        public LoginInfoRepository(StudyDbContext contenxt)
            : base(contenxt)
        { }
        public IEnumerable<LoginInfo> GetAllLoginInfo()
        {
            return this.Get();
        }
        public IEnumerable<LoginInfo> GetLoginInfoByUserName(string userName)
        {
            return this.Get(l => l.UserName == userName);
        }
        public IEnumerable<LoginInfo> GetLoginInfoByUserName(string userName, int pageSize, int pageIndex)
        {
            return this.Get(l => l.UserName == userName, pageSize, pageIndex, l => l.IpDateTime, false);
        }

        public IEnumerable<LoginInfo> GetLoginInfoByTime(DateTime start, DateTime end)
        {
            return this.Get(l => l.IpDateTime >= start && l.IpDateTime < end);
        }
        public IEnumerable<LoginInfo> GetLoginInfoByTime(string userName, DateTime start, DateTime end)
        {
            return this.Get(l => l.UserName == userName && l.IpDateTime >= start && l.IpDateTime < end);
        }

        
        public int GetCount(string userName)
        {
            return this.Count(l => l.UserName == userName);
        }
        /// <summary>
        /// 指定日期之前的所有登陆信息的个数
        /// </summary>
        /// <param name="beforeTime"></param>
        /// <returns></returns>
        public int GetCount(DateTime beforeTime)
        {
            return this.Count(l => l.IpDateTime < beforeTime);
        }
        public int GetCount(DateTime start, DateTime end)
        {
            return this.Count(l => l.IpDateTime >= start && l.IpDateTime < end);
        }
        public int GetCount(string userName, DateTime start, DateTime end)
        {
            return this.Count(l => l.UserName == userName && l.IpDateTime >= start && l.IpDateTime < end);
        }

        public void AddLoginInfo(LoginInfo loginInfo)
        {
            this.Add(loginInfo);
        }
        public void DeleteLoginInfo(LoginInfo loginInfo)
        {
            this.Delete(loginInfo);
        }
    }
}
