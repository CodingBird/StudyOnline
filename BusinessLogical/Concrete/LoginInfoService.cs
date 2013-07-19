using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using BusinessLogical.Abstract;
using DataAccess.Abstract;

namespace BusinessLogical.Concrete
{
    public class LoginInfoService:ServiceBase,ILoginInfoService
    {
        public ILoginInfoRepository Repository { get; private set; }
        public LoginInfoService(ILoginInfoRepository repository)
        {
            this.Repository = repository;
            this.AddDisposableObjects(repository);
        }
        public IEnumerable<LoginInfo> GetAllLoginInfo()
        {
            return this.Repository.GetAllLoginInfo();
        }
        public IEnumerable<LoginInfo> GetLoginInfoByUserName(string userName)
        {
            return this.Repository.GetLoginInfoByUserName(userName);
        }
        public IEnumerable<LoginInfo> GetLoginInfoByUserName(string userName, int pageSize, int pageIndex)
        {
            return this.Repository.GetLoginInfoByUserName(userName, pageSize, pageIndex);
        }
        public IEnumerable<LoginInfo> GetLoginInfoByTime(DateTime start, DateTime end)
        {
            return this.Repository.GetLoginInfoByTime(start, end);
        }
        public IEnumerable<LoginInfo> GetLoginInfoByTime(string userName, DateTime start, DateTime end)
        {
            return this.Repository.GetLoginInfoByTime(userName, start, end);
        }



        public int GetCount(string userName)
        {
            return this.Repository.GetCount(userName);
        }
        public int GetCount(DateTime start, DateTime end)
        {
            return this.Repository.GetCount(start, end);
        }
        public int GetCount(string userName, DateTime start, DateTime end)
        {
            return this.Repository.GetCount(userName, start, end);
        }
        public int GetCount(DateTime beforeTime)
        {
            return this.Repository.GetCount(beforeTime);
        }


        public void AddLoginInfo(LoginInfo loginInfo)
        {
            this.Repository.AddLoginInfo(loginInfo);
        }
        public void DeleteLoginInfo(LoginInfo loginInfo)
        {
            this.Repository.DeleteLoginInfo(loginInfo);
        }
        public void DeleteLoginInfos(IEnumerable<LoginInfo> loginInfos)
        {
            foreach (LoginInfo loginInfo in loginInfos.ToList())
            {
                this.DeleteLoginInfo(loginInfo);
            }
        }
    }
}
