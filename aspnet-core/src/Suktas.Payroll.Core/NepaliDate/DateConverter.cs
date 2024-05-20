using System;

namespace Suktas.Payroll.NepaliDate;

/// <summary>
///     DateConverter class
///     DateConverter converter = new DateConverter();
/// </summary>
public class DateConverter : IDateConverter
{
    /// <summary>
    ///     Year property
    /// </summary>
    public int Year { get; private set; }

    /// <summary>
    ///     Month property
    /// </summary>
    public int Month { get; private set; }

    /// <summary>
    ///     Day property
    /// </summary>
    public int Day { get; private set; }

    /// <summary>
    ///     WeekDayName property
    /// </summary>
    public string WeekDayName { get; private set; }

    /// <summary>
    ///     MonthName property
    /// </summary>
    public string MonthName { get; private set; }

    /// <summary>
    ///     WeekDay property
    /// </summary>
    public int WeekDay { get; private set; }

    /// <summary>
    ///     Converts Given English Year, Month and Day into equivalent Nepali Date
    /// </summary>
    /// <param name="yy">2016</param>
    /// <param name="mm">4</param>
    /// <param name="dd">13</param>
    /// <returns>
    ///     converter.Year => 2073
    ///     converter.Month => 1
    ///     converter.Day => 1
    ///     converter.WeekDayName => Wednesday
    ///     converter.WeekDay => 4
    ///     converter.MonthName => Baishakh
    /// </returns>
    public static string ConvertToNepali(DateTime engdate)
    {
        var yy = engdate.Year;
        var mm = engdate.Month;
        var dd = engdate.Day;
        var calendar = new Calendar();
        var converter = new DateConverter();
        string months, days;

        if (calendar.ValidEnglishDate(yy, mm, dd))
        {
            // english months
            int[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            // leap year months
            int[] lmonth = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // initial values
            var defEyy = 1944;
            var defNyy = 2000;
            var defNmm = 9;
            var defNdd = 17 - 1;

            var day = 7 - 1;
            var totalEDays = 0;
            var totalNDays = 0;
            var a = 0;
            var m = 0;
            var y = 0;
            var i = 0;
            var j = 0;
            var numDay = 0;

            for (i = 0; i < yy - defEyy; i++)
                if (calendar.IsLeapYear(defEyy + i))
                    for (j = 0; j < 12; j++)
                        totalEDays += lmonth[j];
                else
                    for (j = 0; j < 12; j++)
                        totalEDays += month[j];

            // count total no. of days in terms of month
            for (i = 0; i < mm - 1; i++)
                if (calendar.IsLeapYear(yy))
                    totalEDays += lmonth[i];
                else
                    totalEDays += month[i];

            // count total no. of days in terms of day
            totalEDays += dd;
            i = 0;
            j = defNmm;
            totalNDays = defNdd;
            m = defNmm;
            y = defNyy;

            // count nepali date from calendar array
            while (totalEDays != 0)
            {
                a = calendar.BsCalendar[i][j];
                totalNDays++;
                day++;
                if (totalNDays > a)
                {
                    m++; // increment month
                    totalNDays = 1; // reset nepali day to 1
                    j++; // index to next calendar's position
                }

                // reset day at the end of week passed
                if (day > 7)
                    day = 1;

                // reset month if end of month passed
                // And increment year by 1
                if (m > 12)
                {
                    y++;
                    m = 1;
                }

                // reset index after last position of calender
                // and increment i th position (meaning next year started)
                if (j > 12)
                {
                    j = 1;
                    i += 1;
                }

                totalEDays -= 1;
            }

            // public attribute accessors
            converter.Year = y;
            converter.Month = m;
            converter.Day = totalNDays;
            converter.WeekDay = day;
            converter.WeekDayName = calendar.GetDayOfWeek(day);
            converter.MonthName = calendar.GetNepaliMonth(m);
            //asigning "0" in day and month
            months = converter.Month.ToString().Length == 1 ? "0" + converter.Month : converter.Month.ToString();
            days = converter.Day.ToString().Length == 1 ? "0" + converter.Day : converter.Day.ToString();

            return converter.Year + "/" + months + "/" + days;
        }

        months = converter.Month.ToString().Length == 1 ? "0" + converter.Month : converter.Month.ToString();
        days = converter.Day.ToString().Length == 1 ? "0" + converter.Day : converter.Day.ToString();
        return converter.Year + "-" + months + "-" + days;
    }

    public static string ConvertToNepaliFormat2(DateTime engdate)
    {
        var yy = engdate.Year;
        var mm = engdate.Month;
        var dd = engdate.Day;
        var calendar = new Calendar();
        var converter = new DateConverter();
        string months, days;

        if (calendar.ValidEnglishDate(yy, mm, dd))
        {
            // english months
            int[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            // leap year months
            int[] lmonth = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // initial values
            var defEyy = 1944;
            var defNyy = 2000;
            var defNmm = 9;
            var defNdd = 17 - 1;

            var day = 7 - 1;
            var totalEDays = 0;
            var totalNDays = 0;
            var m = 0;
            var y = 0;
            var i = 0;
            var j = 0;

            for (i = 0; i < yy - defEyy; i++)
                if (calendar.IsLeapYear(defEyy + i))
                    for (j = 0; j < 12; j++)
                        totalEDays += lmonth[j];
                else
                    for (j = 0; j < 12; j++)
                        totalEDays += month[j];

            // count total no. of days in terms of month
            for (i = 0; i < mm - 1; i++)
                if (calendar.IsLeapYear(yy))
                    totalEDays += lmonth[i];
                else
                    totalEDays += month[i];

            // count total no. of days in terms of day
            totalEDays += dd;
            i = 0;
            j = defNmm;
            totalNDays = defNdd;
            m = defNmm;
            y = defNyy;

            // count nepali date from calendar array
            while (totalEDays != 0)
            {
                var a = calendar.BsCalendar[i][j];
                totalNDays++;
                day++;
                if (totalNDays > a)
                {
                    m++; // increment month
                    totalNDays = 1; // reset nepali day to 1
                    j++; // index to next calendar's position
                }

                // reset day at the end of week passed
                if (day > 7)
                    day = 1;

                // reset month if end of month passed
                // And increment year by 1
                if (m > 12)
                {
                    y++;
                    m = 1;
                }

                // reset index after last position of calender
                // and increment i th position (meaning next year started)
                if (j > 12)
                {
                    j = 1;
                    i += 1;
                }

                totalEDays -= 1;
            }

            // public attribute accessors
            converter.Year = y;
            converter.Month = m;
            converter.Day = totalNDays;
            converter.WeekDay = day;
            converter.WeekDayName = calendar.GetDayOfWeek(day);
            converter.MonthName = calendar.GetNepaliMonth(m);
            //asigning "0" in day and month
            months = converter.Month.ToString().Length == 1 ? "0" + converter.Month : converter.Month.ToString();
            days = converter.Day.ToString().Length == 1 ? "0" + converter.Day : converter.Day.ToString();

            return converter.Year + "-" + months + "-" + days;
        }

        months = converter.Month.ToString().Length == 1 ? "0" + converter.Month : converter.Month.ToString();
        days = converter.Day.ToString().Length == 1 ? "0" + converter.Day : converter.Day.ToString();

        return converter.Year + "-" + months + "-" + days;
    }


    public static DateConverter ConvertToNepali(int yy, int mm, int dd)
    {
        var calendar = new Calendar();
        var converter = new DateConverter();

        if (calendar.ValidEnglishDate(yy, mm, dd))
        {
            // english months
            int[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            // leap year months
            int[] lmonth = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            // initial values
            var defEyy = 1944;
            var defNyy = 2000;
            var defNmm = 9;
            var defNdd = 17 - 1;

            var day = 7 - 1;
            var totalEDays = 0;
            var totalNDays = 0;
            var a = 0;
            var m = 0;
            var y = 0;
            var i = 0;
            var j = 0;
            var numDay = 0;

            for (i = 0; i < yy - defEyy; i++)
                if (calendar.IsLeapYear(defEyy + i))
                    for (j = 0; j < 12; j++)
                        totalEDays += lmonth[j];
                else
                    for (j = 0; j < 12; j++)
                        totalEDays += month[j];

            // count total no. of days in terms of month
            for (i = 0; i < mm - 1; i++)
                if (calendar.IsLeapYear(yy))
                    totalEDays += lmonth[i];
                else
                    totalEDays += month[i];

            // count total no. of days in terms of day
            totalEDays += dd;
            i = 0;
            j = defNmm;
            totalNDays = defNdd;
            m = defNmm;
            y = defNyy;

            // count nepali date from calendar array
            while (totalEDays != 0)
            {
                a = calendar.BsCalendar[i][j];
                totalNDays++;
                day++;
                if (totalNDays > a)
                {
                    m++; // increment month
                    totalNDays = 1; // reset nepali day to 1
                    j++; // index to next calendar's position
                }

                // reset day at the end of week passed
                if (day > 7)
                    day = 1;

                // reset month if end of month passed
                // And increment year by 1
                if (m > 12)
                {
                    y++;
                    m = 1;
                }

                // reset index after last position of calender
                // and increment i th position (meaning next year started)
                if (j > 12)
                {
                    j = 1;
                    i += 1;
                }

                totalEDays -= 1;
            }

            numDay = day;

            // public attribute accessors
            converter.Year = y;
            converter.Month = m;
            converter.Day = totalNDays;
            converter.WeekDay = day;
            converter.WeekDayName = calendar.GetDayOfWeek(day);
            converter.MonthName = calendar.GetNepaliMonth(m);

            return converter;
        }

        return converter;
    }

    /// <summary>
    ///     Returns English Date for given Nepali Year, Month and Day
    /// </summary>
    /// <param name="yy">2073</param>
    /// <param name="mm">1</param>
    /// <param name="dd">1</param>
    /// <returns>
    ///     converter.Year => 2016
    ///     converter.Month => 4
    ///     converter.Day => 13
    ///     converter.WeekDayName => Wednesday
    ///     converter.WeekDay => 4
    ///     converter.MonthName => April
    /// </returns>
    public static DateTime ConvertToEnglish(string npdate)
    {
        //    if (!Validation.ValidationHelper.IsValidNepaliDate(npdate))
        //    {
        //        var nepdate = ConvertToNepali(DateTime.Today);
        //        throw new UserFriendlyException("Date Format Must Be  yyyy/mm/dd");
        //    }

        var calendar = new Calendar();
        var converter = new DateConverter();
        DateTime dates;

        // Initial / Default values
        var defEyy = 1943;
        var defEmm = 4;
        var defEdd = 14 - 1;

        // equivalent nepali date
        var defNyy = 2000;

        var totalEDays = 0;
        var totalNDays = 0;
        var a = 0;
        var day = 4 - 1;
        var m = 0;
        var y = 0;
        var i = 0;
        var j = 0;
        var k = 0;

        int[] month = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int[] lmonth = { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        var _dates = npdate.Split('/');
        var yy = Convert.ToInt32(_dates[0]);
        var mm = Convert.ToInt32(_dates[1]);
        var dd = Convert.ToInt32(_dates[2]);

        if (calendar.ValidNepaliDate(yy, mm, dd))
        {
            // count total days in terms of year
            for (i = 0; i < yy - defNyy; i++)
            {
                for (j = 1; j <= 12; j++) totalNDays += calendar.BsCalendar[k][j];
                k++;
            }

            // count total days in terms of month
            for (j = 1; j < mm; j++) totalNDays += calendar.BsCalendar[k][j];

            // count total days in terms of day
            totalNDays += dd;

            // calculation of equivalent english date
            totalEDays = defEdd;
            m = defEmm;
            y = defEyy;

            while (totalNDays != 0)
            {
                if (calendar.IsLeapYear(y))
                    a = lmonth[m];
                else
                    a = month[m];

                totalEDays++;
                day++;

                if (totalEDays > a)
                {
                    m++;
                    totalEDays = 1;

                    if (m > 12)
                    {
                        y++;
                        m = 1;
                    }
                }

                if (day > 7)
                    day = 1;

                totalNDays--;
            }

            // public attribute accessors
            // public attribute accessors
            converter.Year = y;
            converter.Month = m;
            converter.Day = totalEDays;
            converter.WeekDay = day;
            converter.WeekDayName = calendar.GetDayOfWeek(day);
            converter.MonthName = calendar.GetEnglishMonth(m);
            var finaldates = converter.Year.ToString() + '/' +
                             converter.Month + '/' + converter.Day;
            dates = Convert.ToDateTime(finaldates);
            return dates;
        }

        return DateTime.Today;
    }

    public static string AddMonth(string npDate, int month)
    {
        var _dates = npDate.Split('/');
        var yy = Convert.ToInt32(_dates[0]);
        var mm = Convert.ToInt32(_dates[1]);
        var dd = Convert.ToInt32(_dates[2]);
        mm = mm + month;
        if (mm >= 13)
        {
            yy = yy + decimal.ToInt32(mm / 12);
            mm = mm % 12;
        }

        return (yy.ToString() + '/' + mm + '/' + dd);
    }


    public static string AddDays(string npDate, int days)
    {
        var engDate = ConvertToEnglish(npDate);
        var addedDate = engDate.AddDays(days);
        var nepDate = ConvertToNepali(addedDate);
        return nepDate;
        //var _dates = npDate.Split('/');
        //var yy = Convert.ToInt32(_dates[0]);
        //var mm = Convert.ToInt32(_dates[1]);
        //var dd = Convert.ToInt32(_dates[2]);
        //dd = dd + days;
        //if (dd >= 30)
        //{
        //    mm = mm + Decimal.ToInt32(dd/30);
        //    dd = dd % 30;
        //}
        //if (mm >= 13)
        //{
        //    yy = yy + Decimal.ToInt32(mm / 12);
        //    mm = mm % 12;
        //}
        //return (yy.ToString() + '/' + mm.ToString() + '/' + dd.ToString()).ToString();
    }
}

