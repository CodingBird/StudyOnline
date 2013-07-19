using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Infrastructure;
using StudyOnline.Models;
using BusinessLogical.Abstract;
using Model;

namespace StudyOnline.Controllers
{
    /// <summary>
    /// 处理前台用户学习记录请求
    /// </summary>
    [Authorize]
    public class RecordController : StudyOnlineController
    {
        public IRecordService RecordService { get; private set; }
        public RecordController(IRecordService recordService)
        {
            this.RecordService = recordService;
            this.AddDisposableObject(recordService);
        }
        /// <summary>
        /// 获取学习记录，并呈现在视图RecordList中
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
        
        /// <summary>
        /// 呈现一个分部视图，在此视图中显示指定用户的记录和记录统计
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public ActionResult RenderRecordsInUserManagePage(string userName)
        {
            ViewBag.UserName = userName;
            return PartialView("_RecordsInUserManagePage");
        }
        /// <summary>
        /// 接受ajax请求，获取学习记录，通过分部视图呈现
        /// 同时获取记录总数，在view总构造ajax分页
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RenderRecerdTableWithPaging(string userName, int pageIndex = 1)
        {
            int recordCount;
            IEnumerable<RecordData> records = this.RecordService.GetRecordsByUserName(userName,
                PagingInfo.PageSize, pageIndex, out recordCount);
            if (records.Count() == 0 && pageIndex > 1)
            {
                pageIndex = pageIndex - 1;
                records = this.RecordService.GetRecords(PagingInfo.PageSize, pageIndex, out recordCount);
            }
            ViewBag.PagingInfo = new PagingInfo
            {
                PageIndex = pageIndex,
                RecordCount = recordCount
            };
            ViewBag.PageIndex = pageIndex;
            Func<int, string> pageUrl = (currentPage) => Url.Action("RenderRecordTable", new { pageIndex = currentPage });
            ViewBag.PageUrl = pageUrl;

            return PartialView("_RecordTableWithPaging", records);
        }
        /// <summary>
        /// 接受ajax请求，获取学习记录，通过分部视图呈现
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RenderRecordTable(string userName,int pageIndex = 1)
        {
            int recordCount;
            IEnumerable<RecordData> records = this.RecordService.GetRecordsByUserName(userName,
                PagingInfo.PageSize, pageIndex, out recordCount);
            if (records.Count() == 0 && pageIndex > 1)
            {
                pageIndex = pageIndex - 1;
                records = this.RecordService.GetRecords(PagingInfo.PageSize, pageIndex, out recordCount);
            }
            ViewBag.PageIndex = pageIndex;
            return PartialView("_RecordTable", records);
        }

        public ActionResult Delete(int recordId, int pageIndex = 1)
        {
            RecordData record = this.RecordService.GetRedord(recordId);
            this.RecordService.DeleteRecord(record);

            return RedirectToAction("List", new { pageIndex = pageIndex });
        }
        public void Add()
        {
            string resourceName = Request["resourceName"];
            string strStart = Request["strStart"];
            string strEnd = Request["strEnd"];
            string resourceId = Request["resourceId"];
            string result = "";
            if (string.IsNullOrEmpty(resourceId) || string.IsNullOrEmpty(strEnd) ||
                string.IsNullOrEmpty(strStart) || string.IsNullOrEmpty(resourceName))
            {
                result = "wrong1";
                Response.Write(result);
                return;
            }
            RecordData record = new RecordData();

            try
            {
                record.StartTime = Convert.ToDateTime(strStart);
                record.EndTime = Convert.ToDateTime(strEnd);
                record.ResourceName = resourceName;
                record.ResourceId = Convert.ToInt32(resourceId);
                record.UserName = User.Identity.Name;
                this.RecordService.AddRecord(record);
                result = "success";
            }
            catch (Exception e)
            {
                result = "wrong2";
            }
            Response.Write(result);
        }
        #region 与记录统计有关的action
        public ActionResult Statistics(int pageIndex = 1)
        {
            return View();
        }
        /// <summary>
        /// 获取指定用户的记录，并统计。
        /// 通过分部视图_StatisticTableWithPaging呈现统计结果，并生成ajax分页
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RenderStatisticTableWidthPaging(string userName,int pageIndex =1)
        {
            int recordCount;
            IEnumerable<RecordData> records = this.RecordService.GetAllReocrdsByUserName(userName, out recordCount);
            IEnumerable<RecordStatistic> statistics = RecordStatistic.GetStatistics(records);

            ViewBag.PagingInfo = new PagingInfo
            {
                PageIndex = pageIndex,
                RecordCount = statistics.Count()
            };
            ViewBag.PageIndex = pageIndex;
            Func<int, string> pageUrl = (currentPage) => Url.Action("RenderStatisticTable", new { pageIndex = currentPage });
            ViewBag.PageUrl = pageUrl;


            return PartialView("_StatisticTableWithPaging", statistics.Skip((pageIndex - 1) * PagingInfo.PageSize).Take(PagingInfo.PageSize).AsEnumerable());
        }
        /// <summary>
        /// 获取指定用户的记录，并统计，将统计结果通过分部视图_StatisticTable呈现
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult RenderStatisticTable(string userName,int pageIndex)
        {
            int recordCount;
            IEnumerable<RecordData> records = this.RecordService.GetAllReocrdsByUserName(User.Identity.Name, out recordCount);
            IEnumerable<RecordStatistic> statistics = RecordStatistic.GetStatistics(records);
            return PartialView("_StatisticTable", statistics.Skip((pageIndex - 1) * PagingInfo.PageSize).Take(PagingInfo.PageSize).AsEnumerable());
        }
        #endregion
    }
}
