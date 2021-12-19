﻿namespace BL
{
    public class BaseStationList
    {
        public int ID { init; get; }
        public string Name { init; get; }
        public uint ChargingSlotsAvailable { init; get; }
        public uint ChargingSlotsOccupied { init; get; }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Charging Slots Used: {ChargingSlotsOccupied}/{ChargingSlotsAvailable}";
        }
    }
}
