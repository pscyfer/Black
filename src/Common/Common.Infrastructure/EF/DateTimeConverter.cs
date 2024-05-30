using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace Common.Infrastructure.EF;

public class DateTimeConverter : ValueConverter<long, DateTime>
{
    public DateTimeConverter()
    : base(
        corevVal => ToDateTime(corevVal),
        efVal => ToMilliseconds(efVal))
    {

    }
    private static long ToMilliseconds(DateTime dateTime)
    {
        PersianCalendar persianCalendar = new PersianCalendar();
        DateTime persianDate = persianCalendar.ToDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
            dateTime.Minute, dateTime.Second, dateTime.Millisecond);

        DateTime gregorianDate = persianCalendar.ToDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
            dateTime.Minute, dateTime.Second, dateTime.Millisecond, PersianCalendar.PersianEra);

        long milliseconds = (long)(gregorianDate - new DateTime(1970, 1, 1)).TotalMilliseconds;

        return milliseconds;
    }

    private static DateTime ToDateTime(long millisecond)
    {

        DateTime unixEpoch = new DateTime(1970, 1, 1);
        DateTime gregorianDate = unixEpoch.AddMilliseconds(millisecond);

        PersianCalendar persianCalendar = new PersianCalendar();
        int persianYear = persianCalendar.GetYear(gregorianDate);
        int persianMonth = persianCalendar.GetMonth(gregorianDate);
        int persianDay = persianCalendar.GetDayOfMonth(gregorianDate);
        int hour = persianCalendar.GetHour(gregorianDate);
        int minute = persianCalendar.GetMinute(gregorianDate);
        int second = persianCalendar.GetSecond(gregorianDate);
        double milliseconds = persianCalendar.GetMilliseconds(gregorianDate);
        return persianCalendar.ToDateTime(persianYear, persianMonth, persianDay, hour, minute, second, Convert.ToInt32(milliseconds));
    }
}