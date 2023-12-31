﻿using Final_Report.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Report.Areas.Navigation
{
    public class NavigationMenu
    {
        public List<NavigationMenuItem> MenuItemsAdmin { get; set; }
        public List<NavigationMenuItem> MenuItemsManager { get; set; }
    }
    public class NavigationMenuItem
    {
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public bool IsNested { get; set; }
        public int Sequence { get; set; }
        public List<string> UserRoles { get; set; }
        public List<NavigationMenuItem> NestedItems { get; set; }
    }
}