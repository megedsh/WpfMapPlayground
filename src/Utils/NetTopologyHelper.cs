using System.Collections.Generic;
using System.Linq;
using MapControl;

using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

using WpfMapPlayground.Models;

using Geometry = NetTopologySuite.Geometries.Geometry;

namespace WpfMapPlayground.Utils
{
    public class NetTopologyHelper
    {
        public static  WKTReader WktReader = new WKTReader();
        public static List<IItemForMap> GetItemsFromWkt(string wkt)
        {
            
            Geometry geometry = WktReader.Read(wkt);
            return GetItems(geometry);
        }

        public static MapControl.Location GetLocationFromWktPoint(string wktPointString) 
        {
            Point geometry = WktReader.Read(wktPointString) as Point;
            return geometry.ToLocation();
        }

        public static List<IItemForMap> GetItems(Geometry geometry)
        {
            List<IItemForMap> itemsForMap = new List<IItemForMap>();
            switch (geometry)
            {
                case GeometryCollection gc:
                    foreach (Geometry item in gc)
                    {
                        itemsForMap.AddRange(GetItems(item));
                    }

                    break;
                case LineString lineString:
                {
                    itemsForMap.Add(new LineItemForMap
                    {
                        Item = lineString.ToLocationCollection(),
                    });
                    break;
                }
                case Polygon polygon:
                {
                    if (polygon.InteriorRings.Length > 0)
                    {
                        itemsForMap.Add(getMultiPolygon(polygon));
                    }
                    else
                    {
                        itemsForMap.Add(new SimplePolygonItemForMap
                        {
                            Item = polygon.Shell.Coordinates.Take(polygon.Shell.Count - 1).ToLocationCollection(),
                        });
                    }

                    break;
                }
                case Point point:
                {
                    itemsForMap.Add(new PointItemForMap
                    {
                        Item = point.ToLocation(),
                    });
                    break;
                }
            }

            return itemsForMap;
        }

        private static ItemForMap getMultiPolygon(Polygon polygon)
        {
            List<LocationCollection> l = new List<LocationCollection>();

            l.Add(polygon.Shell.Coordinates.Take(polygon.Shell.Count - 1).ToLocationCollection());
            foreach (LineString item in polygon.InteriorRings)
            {
                l.Add(item.Coordinates.Take(item.Coordinates.Length - 1).ToLocationCollection());
            }

            return new MultiPolygonItemForMap
            {
                Item = l.ToArray(),
            };
        }
    }
}