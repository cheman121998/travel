using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace travel.Models.ViewModels
{
    public class SidebarViewModel
    {
        public List<Post> Places { get; set; }
        public List<Post> Foods { get; set; }
    }
}