﻿using System.Web;
using System.Web.Mvc;

namespace FreeWebsiteHeatmap.MvcApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}