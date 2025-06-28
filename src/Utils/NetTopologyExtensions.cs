using System;
using System.Collections.Generic;
using MapControl;

using NetTopologySuite.Geometries;

using Location = MapControl.Location;

namespace WpfMapPlayground
{
    public static class NetTopologyExtensions
    {
        public static LocationCollection ToLocationCollection(this LineString source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "LineString cannot be null");
            }

            LocationCollection res = new LocationCollection();
            foreach (Coordinate coordinate in source.Coordinates)
            {
                res.Add(coordinate.ToLocation());
            }

            return res;
        }

        public static LocationCollection ToLocationCollection(this IEnumerable<Coordinate> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "LineString cannot be null");
            }

            LocationCollection res = new LocationCollection();
            foreach (Coordinate coordinate in source)
            {
                res.Add(coordinate.ToLocation());
            }

            return res;
        }


        public static Location ToLocation(this Point source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Point cannot be null");
            }

            return new Location(source.Y, source.X);
        }

        public static Location ToLocation(this Coordinate source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Coordinate cannot be null");
            }

            return new Location(source.Y, source.X);
        }
    }
}