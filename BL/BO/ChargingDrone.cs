﻿namespace BO
{
    // drone in charge
    public class ChargingDrone
    {
        public uint ID { init; get; }
        public double Battery { init; get; }
        public override string ToString()
        {
            return $"ID: {ID}, Battery: {Battery}";
        }
    }
}
