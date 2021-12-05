﻿using System;

namespace IBL.BO
{
    public class PackageList
    {
        public int ID { init; get; }
        public string Sender { init; get; }
        public string Receiver { init; get; }
        public WeightCategories Weight { init; get; }
        public Priorities Priority { init; get; }
        public Statuses Status { init; get; }
    }
}
