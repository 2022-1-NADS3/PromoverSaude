﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPSOne
{
    public class CalendarioFlyoutMenuItem
    {
        public CalendarioFlyoutMenuItem()
        {
            TargetType = typeof(CalendarioFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public string Image { get; set; }

        public Type TargetType { get; set; }
    }
}