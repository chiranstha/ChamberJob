using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Suktas.Payroll.NepaliDate;

/// <summary>
///     Calendar calendar = new Calendar();
/// </summary>
public class Calendar : ICalendar
{
    private IDictionary<int, string> _englishMonthNames;
    private IDictionary<int, string> _nepaliMonthNames;
    private IDictionary<int, string> _weekNames;

    /// <summary>
    ///     List of BS calendar months in each year
    /// </summary>
    public IDictionary<int, int[]> BsCalendar;

    /// <summary>
    ///     Calendar calendar = new Calendar();
    /// </summary>
    public Calendar()
    {
        SetWeekNames();
        SetEnglishMonthNames();
        SetNepaliMonthNames();
        SetBsCalendar();
    }

    /// <summary>
    ///     Will return Day of week for given day index
    /// </summary>
    /// <param name="day">1</param>
    /// <returns>Sunday</returns>
    public string GetDayOfWeek(int day)
    {
        if (day < 1 || day > 7)
            day = 1;
        return _weekNames[day];
    }

    /// <summary>
    ///     Will return English month name for given month index
    /// </summary>
    /// <param name="month">2</param>
    /// <returns>February</returns>
    public string GetEnglishMonth(int month)
    {
        if (month < 1 || month > 12)
            month = 1;
        return _englishMonthNames[month];
    }

    /// <summary>
    ///     Will return Nepali month name for given month index
    /// </summary>
    /// <param name="month">12</param>
    /// <returns>Chaitra</returns>
    public string GetNepaliMonth(int month)
    {
        if (month < 1 || month > 12)
            month = 1;
        return _nepaliMonthNames[month];
    }

    /// <summary>
    ///     Check if given year is Leap Year or not
    /// </summary>
    /// <param name="year">2016</param>
    /// <returns>True</returns>
    public bool IsLeapYear(int year)
    {
        return DateTime.IsLeapYear(year);
    }

    /// <summary>
    ///     Check if given date is in valid English Date range or not.
    ///     Only supports Date range between 1944 To 2033
    /// </summary>
    /// <param name="year">2017</param>
    /// <param name="month">5</param>
    /// <param name="day">8</param>
    /// <returns>True</returns>
    public bool ValidEnglishDate(int year, int month, int day)
    {
        if (year < 1944 || year > 2033)
        {
            Debug.WriteLine("Year should be between 1944 - 2033");
            return false;
        }

        if (month < 1 || month > 12)
        {
            Debug.WriteLine("Month should be between 1 - 12");
            return false;
        }

        if (day < 1 || day > 31)
        {
            Debug.WriteLine("Day should be between 1 - 31");
            return false;
        }

        return true;
    }

    /// <summary>
    ///     Check if Given date is in Valid Nepali date range or not.
    ///     Only supports Date range Between 2000 To 2089
    /// </summary>
    /// <param name="year">2073</param>
    /// <param name="month">1</param>
    /// <param name="day">1</param>
    /// <returns>True</returns>
    public bool ValidNepaliDate(int year, int month, int day)
    {
        if (year < 2000 || year > 2089)
        {
            Debug.WriteLine("Year should be between 1944 - 2033");
            return false;
        }

        if (month < 1 || month > 12)
        {
            Debug.WriteLine("Month should be between 1 - 12");
            return false;
        }

        if (day < 1 || day > 32)
        {
            Debug.WriteLine("Day should be between 1 - 31");
            return false;
        }

        return true;
    }

    private void SetWeekNames()
    {
        _weekNames = new Dictionary<int, string>
        {
            { 1, "Sunday" },
            { 2, "Monday" },
            { 3, "Tuesday" },
            { 4, "Wednesday" },
            { 5, "Thursday" },
            { 6, "Friday" },
            { 7, "Saturday" }
        };
    }

    private void SetEnglishMonthNames()
    {
        _englishMonthNames = new Dictionary<int, string>
        {
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "November" },
            { 12, "December" }
        };
    }

    private void SetNepaliMonthNames()
    {
        _nepaliMonthNames = new Dictionary<int, string>
        {
            { 1, "Baishakh" },
            { 2, "Jestha" },
            { 3, "Ashad" },
            { 4, "Shrawan" },
            { 5, "Bhadra" },
            { 6, "Aswin" },
            { 7, "Kartik" },
            { 8, "Mangshir" },
            { 9, "Poush" },
            { 10, "Magh" },
            { 11, "Falgun" },
            { 12, "Chaitra" }
        };
    }

    private void SetBsCalendar()
    {
        BsCalendar = new Dictionary<int, int[]>
        {
            { 0, new[] { 2000, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 1, new[] { 2001, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 2, new[] { 2002, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 3, new[] { 2003, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 4, new[] { 2004, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 5, new[] { 2005, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 6, new[] { 2006, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 7, new[] { 2007, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 8, new[] { 2008, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31 } },
            { 9, new[] { 2009, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 10, new[] { 2010, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 11, new[] { 2011, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 12, new[] { 2012, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
            { 13, new[] { 2013, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 14, new[] { 2014, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 15, new[] { 2015, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 16, new[] { 2016, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
            { 17, new[] { 2017, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 18, new[] { 2018, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 19, new[] { 2019, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 20, new[] { 2020, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 21, new[] { 2021, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 22, new[] { 2022, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
            { 23, new[] { 2023, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 24, new[] { 2024, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 25, new[] { 2025, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 26, new[] { 2026, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 27, new[] { 2027, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 28, new[] { 2028, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 29, new[] { 2029, 31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30 } },
            { 30, new[] { 2030, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 31, new[] { 2031, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 32, new[] { 2032, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 33, new[] { 2033, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 34, new[] { 2034, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 35, new[] { 2035, 30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31 } },
            { 36, new[] { 2036, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 37, new[] { 2037, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 38, new[] { 2038, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 39, new[] { 2039, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
            { 40, new[] { 2040, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 41, new[] { 2041, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 42, new[] { 2042, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 43, new[] { 2043, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
            { 44, new[] { 2044, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 45, new[] { 2045, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 46, new[] { 2046, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 47, new[] { 2047, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 48, new[] { 2048, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 49, new[] { 2049, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
            { 50, new[] { 2050, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 51, new[] { 2051, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 52, new[] { 2052, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 53, new[] { 2053, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
            { 54, new[] { 2054, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 55, new[] { 2055, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 56, new[] { 2056, 31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30 } },
            { 57, new[] { 2057, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 58, new[] { 2058, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 59, new[] { 2059, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 60, new[] { 2060, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 61, new[] { 2061, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 62, new[] { 2062, 30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31 } },
            { 63, new[] { 2063, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 64, new[] { 2064, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 65, new[] { 2065, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 66, new[] { 2066, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31 } },
            { 67, new[] { 2067, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 68, new[] { 2068, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 69, new[] { 2069, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 70, new[] { 2070, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30 } },
            { 71, new[] { 2071, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 72, new[] { 2072, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30 } },
            { 73, new[] { 2073, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31 } },
            { 74, new[] { 2074, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 75, new[] { 2075, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 76, new[] { 2076, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
            { 77, new[] { 2077, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31 } },
            { 78, new[] { 2078, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 79, new[] { 2079, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30 } },
            { 80, new[] { 2080, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30 } },
            { 81, new[] { 2081, 31, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30 } },
            { 82, new[] { 2082, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 } },
            { 83, new[] { 2083, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30 } },
            { 84, new[] { 2084, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30 } },
            { 85, new[] { 2085, 31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30 } },
            { 86, new[] { 2086, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 } },
            { 87, new[] { 2087, 31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30 } },
            { 88, new[] { 2088, 30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30 } },
            { 89, new[] { 2089, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 } },
            { 90, new[] { 2090, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30 } }
        };
    }
}

