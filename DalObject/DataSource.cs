﻿using DalApi.Util;
using DO;
using System;
using System.Collections.Generic;

namespace Dal
{
    internal class DataSource
    {
        internal static List<Drone> Drones = new();
        internal static List<DroneCharge> DroneCharges = new(10);
        internal static List<Station> Stations = new();
        internal static List<Customer> Customers = new();
        internal static List<Parcel> Parcels = new();

        internal class Config
        {
            public static uint PackageID = 0;
            public static double Free = 2000;                        // km when free
            public static double LightConsumption = 1500;            // km when carrying light package
            public static double MediumConsumption = 1200;           // km when carrying mid weight package
            public static double HeavyConsumption = 1000;            // km when carrying heavy package
            public static double ChargeRate = 10;                    // % charged per minute
        }


        /// <summary>
        /// Initializes the entities with random data
        /// </summary>
        internal static void Initialize() //internal
        {
            Random rd = new Random();

            for (int i = 0; i < 2; ++i)
            {
                Station s = new Station()
                {
                    AvailableChargeSlots = (uint)rd.Next(10, 50),
                    Name = "Station" + (i + 1),
                    Location = new Coordinate(-90 + 180 * rd.NextDouble(), -180 + 360 * rd.NextDouble())
                };

                do
                {
                    s.ID = (uint)rd.Next(100000000, 999999999);
                } while (Stations.Exists(sta => sta.ID == s.ID));

                Stations.Add(s);
            }

            for (int i = 0; i < 5; ++i)
            {
                Drone d = new Drone()
                {
                    Model = "Drone" + rd.Next(10),
                    WeightCategory = (DO.WeightCategories)rd.Next(0, 3)
                };

                do
                {
                    d.ID = (uint)rd.Next(100000000, 999999999);
                } while (Drones.Exists(drone => drone.ID == d.ID));

                Drones.Add(d);

                /* If Drone is currently Free, set it to be charging at a Base Station
                if (Drones[index].DroneStatus == DroneStatuses.Free)
                {
                    //int droneIndex = Config.DronesCharging++;

                    DroneCharge dc = new DroneCharge();
                    dc.DroneID = Drones[index].ID;
                    dc.StationID = Stations.Take(Config.FreeStation).Where(s => s.ChargeSlots > DataSource.DroneCharges.Count(dc => dc.StationID == s.ID && dc.DroneID != 0)).ElementAt(rd.Next(Config.FreeStation)).ID;
                    DroneCharges.Add(dc);
                }*/
            }

            for (int i = 0; i < 10; ++i)
            {
                Customer c = new Customer()
                {
                    Name = "Customer" + (i + 1),
                    Location = new Coordinate(-90 + 180 * rd.NextDouble(), -180 + 360 * rd.NextDouble()),
                };

                do
                {
                    c.ID = (uint)rd.Next(100000000, 999999999);
                } while (Customers.Exists(cust => cust.ID == c.ID)); // prevent overlapping IDs

                do
                {
                    c.Phone = "0" + rd.Next(500000000, 589999999);
                } while (Customers.Exists(cust => cust.Phone == c.Phone)); // prevent overlapping Phone Numbers

                Customers.Add(c);
            }

            bool deliveredPackage = false; // Guarantee that at least one package has been delivered
            for (int i = 0; i < 10; ++i)
            {
                DateTime earliest = new DateTime(2018, 1, 1, 0, 0, 0);

                Parcel p = new Parcel()
                {
                    SenderID = Customers[rd.Next(Customers.Count)].ID,
                    WeightCategory = (WeightCategories)rd.Next(0, 3),
                    Priority = (Priorities)rd.Next(0, 3),
                    Scheduled = earliest.AddSeconds(rd.NextDouble() * DateTime.Now.Subtract(earliest).TotalSeconds)
                };

                do
                {
                    p.ID = (uint)rd.Next(100000000, 999999999);
                } while (Parcels.Exists(parc => parc.ID == p.ID));

                do
                {
                    p.TargetID = Customers[rd.Next(Customers.Count)].ID;
                } while (p.TargetID == p.SenderID); // prevent customer from sending to itself

                if (rd.NextDouble() < .5 || i == 9 && !deliveredPackage)
                {
                    p.Assigned = p.Scheduled.AddMinutes(rd.Next(10));
                    p.DroneID = Drones[rd.Next(Drones.Count)].ID;
                    if (rd.NextDouble() < .5 || i == 9 && !deliveredPackage)
                    {
                        p.PickedUp = p.Assigned.Value.AddMinutes(rd.Next(5, 60));
                        if (p.PickedUp.Value.AddDays(1).CompareTo(DateTime.Now) < 0 || i == 9 && !deliveredPackage)
                            p.Delivered = p.PickedUp.Value.AddMinutes(rd.Next(10, 120));
                    }
                }
                Parcels.Add(p);
            }
        }
    }
}
