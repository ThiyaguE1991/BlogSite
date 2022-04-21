
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BloggerPortal.Models.ViewModels
{
    #region Server Side Pagination

    public class DataTableAjaxPostModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public bool IsFilter { get; set; }
        public List<Column> Columns { get; set; }
        public Search Search { get; set; }
        public List<Order> Order { get; set; }
        public string ParameterList { get; set; }
        public string ValueList { get; set; }
        public string ProcedureName { get; set; }
        public int BlogId { get; set; }
        public string PageName { get; set; }

    }

    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public Search Search { get; set; }

    }

    public class Search
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }

    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
    public class DataTableResultSet
    {
        public int PageSize { get; set; }
        public string ResultJSON { get; set; }
        public DataSet ResultDataSet { get; set; }
        public DataTable Datatable { get; set; }

        public List<dynamic> lstTables1 { get; set; }
        public List<dynamic> lstTables2 { get; set; }
        public List<dynamic> lstTables3 { get; set; }
    }

    #endregion
}