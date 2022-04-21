using BloggerPortal.Models.ViewModels;
using BloggerPortal.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloggerPortal.Controllers
{
    public class CommentController : BaseController
    {
        public ActionResult Index(int blogId)
        {
            if (!SessionService.CheckLoggedInUserID())
            {
                return RedirectToAction("Index", "Login");
            }
            LoadListViewBags("menu-comment");
            return View(blogId);
        }



        #region Bind Grid view data


        /// <summary>
        /// Get Data table list
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetTableList(DataTableAjaxPostModel model)
        {
            DataTableResultSet obj = new DataTableResultSet();
            obj = GetGirdData(model, out int filteredResultsCount, out int totalResultsCount, false, true);

            object resultJSONList = null;
            if (obj.ResultJSON != null && obj.ResultJSON != string.Empty)
            {
                switch (model.PageName)
                {
                    case "comment":
                        var levelList = JsonConvert.DeserializeObject<List<CommentListVM>>(obj.ResultJSON.ToString()).ToList();
                        resultJSONList = levelList;
                        break;
                }
            }
            else
            {
                switch (model.PageName)
                {
                    case "comment":
                        resultJSONList = new List<CommentListVM>().ToList();
                        break;

                }
            }
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.Draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = resultJSONList
            });
        }

        public DataTableResultSet GetGirdData(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount, bool isForExport, bool isFromList = true)
        {
            int pageSize = model.Length;
            int startPage = model.Start;
            string sortBy = "";
            bool sortDir = true;

            if (model.Order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.Columns[model.Order[0].Column].Data;
                sortDir = model.Order[0].Dir.ToLower() == "asc";
            }

            // search the dbase taking into consideration table sorting and paging
            DataTableResultSet result = GetGirdData(pageSize, startPage, sortBy, sortDir, model, out filteredResultsCount, out totalResultsCount);
            return result;
        }

        public DataTableResultSet GetGirdData(int pageSize, int startPage, string sortBy,
            bool sortDir, DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            filteredResultsCount = totalResultsCount = 0;
            int pageNo = (startPage / (pageSize == 0 ? 10 : pageSize)) + 1;

            if (model.ValueList == null)
                model.ValueList = "";

            DataTableResultSet obj = new DataTableResultSet();
            string[] strParameter = null;
            string[] strValue = null;

            obj = CommonServices.GetDataTableListWithCustomParam(model.ProcedureName, pageNo, sortBy, sortDir, strParameter, strValue, model.BlogId);

            int totalRecords = obj.PageSize;
            filteredResultsCount = totalResultsCount = totalRecords;
            return obj;
        }

        #endregion




        [HttpPost]
        public ActionResult SaveReplyComment(string comment, int commentId)
        {
            try
            {
                int replyCommentId = UserComments.SaveUserComments(comment, 0, SessionService.GetLoggedInUserID(), commentId, false);
                if (replyCommentId > 0)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }                
            }
            catch (Exception ex)
            {

            }
            return Json(-100, JsonRequestBehavior.AllowGet);

        }
    }
}