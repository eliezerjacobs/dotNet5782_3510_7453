﻿using BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : BlApi.IBL
    {
        // gets EnroutePackage based off parcel ID
        private EnroutePackage GetEnroutePackage(uint parcelID)
        {
            try
            {
                lock (dalObject)
                {
                    DO.Parcel parcel = dalObject.GetParcel(parcelID);
                    Package package = GetPackage(parcelID);

                    Location collectionLocation = GetCustomer(package.Sender.ID).Location;
                    Location deliveryLocation = GetCustomer(package.Receiver.ID).Location;
                    double deliveryDistance = Distance(collectionLocation, deliveryLocation);
                    EnroutePackage enroute = new EnroutePackage()
                    {
                        ID = parcelID,
                        Weight = (WeightCategories)parcel.WeightCategory,
                        Priority = (Priorities)parcel.Priority,
                        Delivering = ParcelStatus(parcel) == Statuses.Collected,
                        Sender = package.Sender,
                        Receiver = package.Receiver,
                        CollectionLocation = collectionLocation,
                        DeliveryLocation = deliveryLocation,
                        DeliveryDistance = deliveryDistance
                    };

                    return enroute;
                }
            }
            catch (DO.ObjectNotFound e)
            {
                throw new ObjectNotFound(e.Message);
            }
        }
    }
}
