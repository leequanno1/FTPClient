using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPClient.utils
{
    public class DateTimeHelper
    {
        public static string ConvertIsoDateToFormatted(string isoDate)
        {
            if (DateTime.TryParse(isoDate, out DateTime parsedDate))
            {
                return parsedDate.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                return "Invalid format";
            }
        }

    }
}
