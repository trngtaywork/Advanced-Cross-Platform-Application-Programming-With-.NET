﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenClosedPrinciple.Model
{
    public class Book : IBook
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
    }
}
