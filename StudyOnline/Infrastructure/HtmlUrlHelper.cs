using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudyOnline.Models;
using System.Text;

namespace StudyOnline.Infrastructure
{
    public static class HtmlUrlHelperExtensions
    {
        public static string ShortenText(this HtmlHelper html, string text, int wordCountToShow)
        {
            if (text.Length > wordCountToShow)
            {
                return text.Substring(0, wordCountToShow - 1) + "...";
            }
            return text;
        }
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrlAccessor)
        {
            StringBuilder result = new StringBuilder();
            result.Append("<ul>");
            string className;
            string link;
            for (int i = 1; i <= pagingInfo.PageCount; i++)
            {
                link = CreateLinkTag(i, i.ToString(), pageUrlAccessor).ToString();
                className = null;
                if (i == pagingInfo.PageIndex)  //如果是当前页
                {
                    className = "active";
                }
                if (i == 1)   //首页
                {
                    result.Append(CreateLiTag(link, className, "pagination-first").ToString());
                    if (pagingInfo.PageIndex > 7)  //d当前页码大于6，显示前省略号
                    {
                        result.Append(CreateLiTag("<a>......</a>", null, "pre-ellipsis").ToString());
                    }
                }
                else if (i == pagingInfo.RecordCount)  //尾页
                {
                    if (pagingInfo.PageCount > 10 && Math.Abs(pagingInfo.PageCount - pagingInfo.PageIndex) > 6)  //超过10页，显示后省略号
                    {
                        result.Append(CreateLiTag("<a>......</a>", null, "next-ellipsis").ToString());
                    }

                    result.Append(CreateLiTag(link, className, "pagination-last").ToString());
                }
                else if (Math.Abs(pagingInfo.PageIndex - i) <= 5)//显示当前页码的左右5个页标签
                {
                    result.Append(CreateLiTag(link, className, null).ToString());
                }
                result.Append("</li>");
            }
            result.Append("</ul>");
            return MvcHtmlString.Create(result.ToString());
        }
        public static TagBuilder CreateLiTag(string innerHtml, string className = (string)null, string id = (string)null)
        {
            TagBuilder tag = new TagBuilder("li");
            tag.InnerHtml = innerHtml;
            if (!String.IsNullOrEmpty(className))
                tag.AddCssClass(className);
            if (!String.IsNullOrEmpty(id))
                tag.MergeAttribute("id", id);
            return tag;
        }
        public static TagBuilder CreateLinkTag(int page, string innerHtml, Func<int, string> pageUrlAccessor)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrlAccessor(page));
            tag.InnerHtml = innerHtml;
            return tag;
        }
        /// <summary>
        /// 生成Unobtrusive Ajax action link分页
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pagingInfo">分页信息</param>
        /// <param name="pageUrl">生成链接委托</param>
        /// <param name="leastCountToGenerateFirstLast">页面数量超过该数则生成“首页”“尾页”</param>
        /// <param name="updateTargetId">回调更新的页面元素id</param>
        /// <returns></returns>
        public static MvcHtmlString AjaxPageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl, int leastCountToGenerateFirstLast, string updateTargetId)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.PageCount; i++)
            {
                if (i == 1)  //首页
                {
                    //result.Append(@"<div id=""pagination-div-container"">");
                    result.Append(@"<ul id=""pagination-ul-container"">");

                    if (pagingInfo.PageIndex == 1)
                    {

                        result.Append("<li class='active'>");
                    }
                    else
                    {
                        result.Append("<li>");
                    }
                    result.Append(CreateAjaxLinkTag("1", i, pageUrl, updateTargetId, null, "pagination-first").ToString());
                    result.Append("</li>");

                    result.Append("<li>");
                    result.Append(@"<a id=""pre-ellipsis"">......</a>");
                    result.Append("</li>");
                }
                else if (i == pagingInfo.PageCount)  //尾页
                {
                    result.Append("<li>");
                    result.Append(@"<a id=""next-ellipsis"">......</a>");
                    result.Append("</li>");

                    if (pagingInfo.PageIndex == pagingInfo.PageCount)
                    {
                        result.Append("<li class='active'>");
                    }
                    else
                    {
                        result.Append("<li>");
                    }
                    result.Append(CreateAjaxLinkTag(pagingInfo.PageCount.ToString(), i, pageUrl, updateTargetId, null,
                            "pagination-last").ToString());
                    result.Append("</li>");
                    result.Append("</ul>");
                    //result.Append("</div>");
                }
                else    //其他
                {
                    if (i == pagingInfo.PageIndex)
                    {
                        result.Append("<li class='active'>");
                    }
                    else
                    {
                        result.Append("<li>");
                    }
                    result.Append(CreateAjaxLinkTag(i.ToString(), i, pageUrl, updateTargetId).ToString());
                    result.Append("</li>");
                }
            }

            return MvcHtmlString.Create(result.ToString());
        }

        //public void CreatePageIndexItem(int currentPage,int currentPageToMatch, string innerHtml,int page, Func<int, string> pageUrl,
        //    string updateTargetId,ref StringBuilder result, string className = (string)null, string id = (string)null)
        //{
        //    result.Append("<li>");
        //    if (currentPage == currentPageToMatch)
        //    {
        //        result.Append(CreateAjaxLinkTag(innerHtml, page, pageUrl, updateTargetId, className, id).ToString());
        //    }
        //    else
        //    {
        //        result.Append(CreateAjaxLinkTag(innerHtml, page, pageUrl, updateTargetId, className, id).ToString());
        //    }
        //    result.Append("</li>");
        //}

        private static TagBuilder CreateAjaxLinkTag(string innerHtml, int page, Func<int, string> pageUrl, string updateTargetId,
            string className = (string)null, string id = (string)null)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(page));
            tag.MergeAttribute("data-ajax-mode", "replace");
            tag.MergeAttribute("data-ajax", "true");
            tag.MergeAttribute("data-ajax-success", "bulidEventAfterAjax");
            tag.MergeAttribute("data-ajax-update", "#" + updateTargetId);
            if (!String.IsNullOrEmpty(className))
                tag.AddCssClass(className);
            if (!String.IsNullOrEmpty(id))
                tag.MergeAttribute("id", id);
            tag.InnerHtml = innerHtml;

            return tag;
        }
    }
}