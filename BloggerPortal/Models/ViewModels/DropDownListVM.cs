using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloggerPortal.Models.ViewModels
{
    public class DropDownListVM
    {
        public DropDownListVM(long value,string text)
        {
            Value = value;
            Text = text;
        }
        public Nullable<long> Value { get; set; }
        public string Text { get; set; }
    }
}