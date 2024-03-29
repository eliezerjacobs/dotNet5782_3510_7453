﻿using System.Collections.Generic;

namespace BO
{
    public class BaseStation
    {
        public uint ID { init; get; }
        public string Name { init; get; }
        public Location Location { init; get; }
        public uint AvailableChargingSlots { get; set; }
        public List<ChargingDrone> ChargingDrones { init; get; }
        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Location: ({Location}), Available Charge Slots: {AvailableChargingSlots}";
        }
    }
}
