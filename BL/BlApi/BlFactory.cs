﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public class BlFactory
    {
        public static BlApi.IBL GetBl()
        {
            return BL.BL.Instance;
        }
    }
}
