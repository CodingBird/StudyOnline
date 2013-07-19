using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Model;

namespace DataAccess
{
    public class InitializeDBWithSeedData:DropCreateDatabaseIfModelChanges<StudyDbContext>
    {
        protected override void Seed(StudyDbContext context)
        {
            #region 视频类别
            context.VideoCategory.Add(
                new VideoCategory
                {
                    Name = "视频类别1",
                    Description =""
                }
            );
            context.VideoCategory.Add(
                new VideoCategory
                {
                    Name = "视频类别2",
                    Description = ""
                }
            );
            context.VideoCategory.Add(
                new VideoCategory
                {
                    Name = "视频类别3",
                    Description = ""
                }
            );
            #endregion
            #region 视频内容
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频0南京工业大学组织部视频0",
                    Description="",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(0),
                    Url="",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频1",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(1),
                    Url = "",
                    VideoCategoryId = 2
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频2",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(2),
                    Url = "",
                    VideoCategoryId = 2
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频3",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(3),
                    Url = "",
                    VideoCategoryId = 3
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频4",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(4),
                    Url = "",
                    VideoCategoryId = 2
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频5",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(5),
                    Url = "",
                    VideoCategoryId = 2
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频6",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(6),
                    Url = "",
                    VideoCategoryId = 2
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频7",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(7),
                    Url = "",
                    VideoCategoryId = 3
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频8",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(8),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频9",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(9),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频10",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(10),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频11",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(11),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频12",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(12),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频13",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(13),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频14",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(14),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频15",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(15),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频16",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(16),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频17",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(17),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频18",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(18),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            context.VideoData.Add(
                new VideoData
                {
                    Name = "南京工业大学组织部视频19",
                    Description = "",
                    CanView = true,
                    PlayCount = 10,
                    UploadTime = DateTime.Now.AddHours(19),
                    Url = "",
                    VideoCategoryId = 1
                }
                );
            #endregion
            #region 文档类别
            context.DocCategory.Add(
                new DocCategory
                {
                    Name = "文档类别1",
                    Description = ""
                });
            context.DocCategory.Add(
                new DocCategory
                {
                    Name = "文档类别1",
                    Description = ""
                });
            context.DocCategory.Add(
                new DocCategory
                {
                    Name = "文档类别2",
                    Description = ""
                });
            context.DocCategory.Add(
                new DocCategory
                {
                    Name = "文档类别3",
                    Description = ""
                });
            context.DocCategory.Add(
                new DocCategory
                {
                    Name = "文档类别4",
                    Description = ""
                });
            #endregion
            #region 文档
            context.DocData.Add(
                new DocData
                {
                    Name = "文档1",
                    Content = "文档1",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(1),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档2",
                    Content = "文档2",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(2),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档3",
                    Content = "文档3",
                    DocCategoryId = 2,
                    UploadTime = DateTime.Now.AddHours(3),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档4",
                    Content = "文档4",
                    DocCategoryId = 2,
                    UploadTime = DateTime.Now.AddHours(4),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档5",
                    Content = "文档5",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(5),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档6",
                    Content = "文档6",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(6),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档7",
                    Content = "文档7",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(7),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档8",
                    Content = "文档8",
                    DocCategoryId = 3,
                    UploadTime = DateTime.Now.AddHours(8),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档9",
                    Content = "文档9",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(9),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档10",
                    Content = "文档10",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(10),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档11",
                    Content = "文档11",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(11),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档12",
                    Content = "文档12",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(12),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档13",
                    Content = "文档13",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(13),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档14",
                    Content = "文档14",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(14),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档15",
                    Content = "文档15",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(15),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档16",
                    Content = "文档16",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(16),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档17",
                    Content = "文档17",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(17),
                    ViewCount = 1,
                    CanView = true
                });
            context.DocData.Add(
                new DocData
                {
                    Name = "文档18",
                    Content = "文档18",
                    DocCategoryId = 1,
                    UploadTime = DateTime.Now.AddHours(18),
                    ViewCount = 1,
                    CanView = true
                });
            #endregion
            #region 记录
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                UserName = "admin",
                ResourceName ="文档1",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "南京工业大学组织部视频19",
                UserName = "admin",
                ResourceType = ResourceType.VideoResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                UserName = "admin",
                ResourceName = "文档1",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                UserName = "admin",
                ResourceName = "文档1",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            context.RecordData.Add(new RecordData
            {
                ResourceId = 1,
                ResourceName = "文档1",
                UserName = "admin",
                ResourceType = ResourceType.DocResource,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2)
            });
            #endregion
            base.Seed(context);
        }
    }
}
