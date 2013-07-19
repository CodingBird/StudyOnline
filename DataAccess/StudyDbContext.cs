using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Model;

namespace DataAccess
{
    public class StudyDbContext:DbContext
    {
        public StudyDbContext()
            : base("name=StudyOnlineDb")
        { }
        public DbSet<VideoCategory> VideoCategory { set; get; }
        public DbSet<VideoData> VideoData { set; get; }

        public DbSet<DocCategory> DocCategory { set; get; }
        public DbSet<DocData> DocData { set; get; }

        public DbSet<RecordData> RecordData { set; get; }

        public DbSet<LoginInfo> LoginInfoo { set; get; }
        public DbSet<MonthLoginCount> MonthLoginCount { set; get; }
    }
}
