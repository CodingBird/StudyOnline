﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DataAccess.Abstract;

namespace BusinessLogical.Abstract
{
    public interface ILoginInfoService
    {
        IEnumerable<LoginInfo> GetAllLoginInfo();
        IEnumerable<LoginInfo> GetLoginInfoByUserName(string userName);
        IEnumerable<LoginInfo> GetLoginInfoByUserName(string userName, int pageSize, int pageIndex);
        IEnumerable<LoginInfo> GetLoginInfoByTime(DateTime start, DateTime end);
        IEnumerable<LoginInfo> GetLoginInfoByTime(string userName, DateTime start, DateTime end);
        int GetCount(string userName);
        int GetCount(DateTime beforeTime);
        int GetCount(DateTime start, DateTime end);
        int GetCount(string userName, DateTime start, DateTime end);

        void AddLoginInfo(LoginInfo loginInfo);
        void DeleteLoginInfo(LoginInfo loginInfo);
        void DeleteLoginInfos(IEnumerable<LoginInfo> loginInfos);
    }
}
