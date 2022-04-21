using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BloggerPortal.Common
{
    public static class DateConversion
    {
        /// <summary>
        /// Converts not nullable string to datetime
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime StringToDateConversion(string date, string format)
        {
            try
            {
                if (date == null)
                {
                    return DateTime.Now;
                }
                /* Convert Date to Currrnt Culture */
                DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
                dateTimeFormatterProvider.ShortDatePattern = format; //source date format
                DateTime NewDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
                return NewDate;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Convert nullable to string to datetime
        /// </summary>
        /// <param name="date"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime? StringToNullableDateConversion(string date, string format)
        {
            try
            {
                if (date == null || date == string.Empty)
                {
                    return null;
                }
                /* Convert Date to Currrnt Culture */
                DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;
                dateTimeFormatterProvider.ShortDatePattern = format; //source date format
                DateTime NewDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
                return NewDate;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// convertable nullable datetime to string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string NullableDateTimeToString(DateTime? dateTime, string format)
        {
            DateTime temp = DateTime.Now;
            if (dateTime == null)
            {
                return "";
            }
            else
            {
                return DateTimeToString(Convert.ToDateTime(dateTime), format);
            }
        }

        /// <summary>
        /// converts datetime to string
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="Format"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime dateTime, string Format)
        {
            string returnValue = string.Empty;
            returnValue = dateTime.ToString(Format, CultureInfo.InvariantCulture);
            return returnValue;
        }
    }
}