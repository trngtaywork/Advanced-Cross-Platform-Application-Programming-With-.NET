﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenClosedPrinciple.Model
{
    interface IBook
    {
        string Title { get; set; }
        string Author { get; set; }
        string Price { get; set; }
    }
}
