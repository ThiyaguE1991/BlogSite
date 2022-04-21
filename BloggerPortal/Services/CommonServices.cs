using BloggerPortal.Common;
using BloggerPortal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace BloggerPortal.Services
{
    public class CommonServices
    {
        public static string SQLDBConnectionString = ConfigurationManager.ConnectionStrings["BloggerModelStr"].ToString();
        public static DataTableResultSet GetDataTableList(string procedureName,
           int pageNo, string sortBy, bool sortDir, string[] strParameter, string[] strValue)
        {
            DataTableResultSet obj = new DataTableResultSet();
            StringBuilder mJsonResponse = new StringBuilder();
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[3];

                commandParameters[0] = new SqlParameter("@PageNumber", pageNo);
                commandParameters[1] = new SqlParameter("@OrderByColumn", sortBy);
                commandParameters[2] = new SqlParameter("@OrderBy", sortDir);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(
                    SQLDBConnectionString,
                    CommandType.StoredProcedure,
                    procedureName
                  ,
                    commandParameters))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj.PageSize = Convert.ToInt32(reader.GetValue(0).ToString());
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            obj.ResultJSON += Convert.ToString(reader.GetValue(0));
                        }
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                //  LogService.AddGeneralExceptionLogs(ex, System.Reflection.MethodBase.GetCurrentMethod(), LogMessageType.PortalRequest, Guid.Empty, 4, "PORTAL");
            }

            return obj;
        }



        public static DataTableResultSet GetDataTableListWithCustomParam(string procedureName,
         int pageNo, string sortBy, bool sortDir, string[] strParameter, string[] strValue,int blogId)
        {
            DataTableResultSet obj = new DataTableResultSet();
            StringBuilder mJsonResponse = new StringBuilder();
            try
            {
                SqlParameter[] commandParameters = new SqlParameter[4];

                commandParameters[0] = new SqlParameter("@PageNumber", pageNo);
                commandParameters[1] = new SqlParameter("@OrderByColumn", sortBy);
                commandParameters[2] = new SqlParameter("@OrderBy", sortDir);
                commandParameters[3] = new SqlParameter("@BlogId", blogId);

                using (SqlDataReader reader = SqlHelper.ExecuteReader(
                    SQLDBConnectionString,
                    CommandType.StoredProcedure,
                    procedureName
                  ,
                    commandParameters))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            obj.PageSize = Convert.ToInt32(reader.GetValue(0).ToString());
                        }

                        reader.NextResult();

                        while (reader.Read())
                        {
                            obj.ResultJSON += Convert.ToString(reader.GetValue(0));
                        }
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                //  LogService.AddGeneralExceptionLogs(ex, System.Reflection.MethodBase.GetCurrentMethod(), LogMessageType.PortalRequest, Guid.Empty, 4, "PORTAL");
            }

            return obj;
        }



    }
}