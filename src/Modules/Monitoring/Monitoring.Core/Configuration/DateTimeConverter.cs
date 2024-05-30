using System.Globalization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Monitoring.Core.Configuration;

public class DateTimeConverter
{

    protected long ToMilliseconds(DateTime dateTime)
    {
        DateTime now = DateTime.Now; // Get the current date and time in UTC

        long milliseconds = (long)(now - new DateTime(1970, 1, 1)).TotalMilliseconds; // Convert to milliseconds
        return milliseconds;
    }

    protected DateTime ToDateTime(long millisecond)
    {

        DateTime unixEpoch = new DateTime(1970, 1, 1);
        DateTime gregorianDate = unixEpoch.AddMilliseconds(millisecond);

        return gregorianDate;
      
    }
}