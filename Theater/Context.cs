﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Theater
{
    partial class Context
    {
        public static Context DB { get; } = new Context();
    }
}