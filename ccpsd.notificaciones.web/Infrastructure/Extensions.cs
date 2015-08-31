using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ccpsd.notificaciones.web.Infrastructure
{
    public static class Extensions
    {
        public  static string ListToString(this IEnumerable<string> list, string separator = ",")
        {
            return list.Select(s => s).Aggregate((s, j) => s + string.Format("{0}", separator) + j);
        }

        public static List<string> StringToList(this string cad, string separator = ",")
        {
            return cad.Split(separator.ToArray(), StringSplitOptions.None).ToList();
        }

        
    }
}