﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mono_praksa1.Common
{
    public class Sorting
    {
        public string OrderBy { get; set; }
        public string SortOrder { get; set; }

        public Sorting(string orderBy, string sortOrder)
        {
            OrderBy = orderBy;
            SortOrder = sortOrder;
        }

        public Sorting() { }
    }
}