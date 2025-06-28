using System.Collections.Generic;

using MapControl;

using NetTopologySuite.Geometries;

using Geometry = NetTopologySuite.Geometries.Geometry;

namespace WpfMapPlayground.Views
{
    public class NetTopologyHelper
    {
        public static List<ItemForMap> GetItems(Geometry geometry)
        {
            List<ItemForMap> itemsForMap = new List<ItemForMap>();
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
                            Item = polygon.Shell.ToLocationCollection(),
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

            l.Add(polygon.Shell.ToLocationCollection());
            foreach (LineString item in polygon.InteriorRings)
            {
                l.Add(item.ToLocationCollection());
            }

            return new MultiPolygonItemForMap
            {
                Item = l.ToArray(),
            };
        }
    }
}