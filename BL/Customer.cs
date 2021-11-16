﻿using System.Collections.Generic;

namespace IBL.BO
{
    public class Customer
    {
        int ID;
        string Name;
        string Phone;
        Location Location;
        List<DeliveredPackage> Outgoing;
        List<DeliveredPackage> Incoming;
    }
}