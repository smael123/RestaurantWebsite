﻿using RestaurantWebsite.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantWebsite.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Special> CurrentSpecials { get; set; }
    }
}