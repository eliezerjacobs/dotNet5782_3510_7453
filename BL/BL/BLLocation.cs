﻿using BO;
using System;
using System.Collections.Generic;

namespace BL
{
    partial class BL : BlApi.IBL
    {
        private Location CoordinateToLocation(DalApi.Util.Coordinate coord)
        {
            return new Location()
            {
                Latitude = coord.Latitude,
                Longitude = coord.Longitude
            };
        }

        /// <summary>
        /// Calculates distance in meters between two BO.Locations
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns>Meters between source and dest</returns>
        internal double Distance(Location source, Location dest)
        {
            return LocationToCoordinate(source).DistanceTo(LocationToCoordinate(dest)) / 1000;
        }

        private DalApi.Util.Coordinate LocationToCoordinate(Location location)
        {
            return new DalApi.Util.Coordinate(location.Latitude, location.Longitude);
        }
    }
}
