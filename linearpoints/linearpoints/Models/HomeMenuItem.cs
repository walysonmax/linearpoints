using System;
using System.Collections.Generic;
using System.Text;

namespace linearpoints.Models
{
    public enum MenuItemType
    {
        Draw
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
