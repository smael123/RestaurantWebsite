﻿using System.Web;
using System.Web.Mvc;

namespace RestaurantWebsite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //globally require authorization
            filters.Add(new AuthorizeAttribute());
        }
    }
}
