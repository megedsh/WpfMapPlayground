using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

using WpfMapPlayground.Models;
using WpfMapPlayground.Utils;

namespace WpfMapPlayground;

public static class DataProvider
{
    public static ObservableCollection<TimedLineForMap> TimedItems { get; } = createTimedItems();
    public static ObservableCollection<IItemForMap> Geometries { get; } = createGeometries();

    private static ObservableCollection<IItemForMap> createGeometries()
    {
        PointItemForMap pointItemForMap = new PointItemForMap
        {
            Name = "SinglePointItem",
            Item = NetTopologyHelper.GetLocationFromWktPoint("POINT(-111.65542602539065 35.19401151791166)")
        };

        List<IItemForMap> multiPoints = NetTopologyHelper.GetItemsFromWkt("GEOMETRYCOLLECTION(POINT(-98.17382812499999 29.382175075145298),POINT(-83.58398437499997 32.32427558887656),POINT(-81.65039062499997 40.513799155044154),POINT(-93.16406249999999 46.01222384063237),POINT(-111.62109374999997 46.49839225859765),POINT(-119.26757812499999 39.97712009843963),POINT(-101.95312499999999 40.11168866559598))");

        IItemForMap lineItem = NetTopologyHelper.GetItemsFromWkt("LINESTRING(-87.63793945312501 41.857287927691345,-90.17578125000001 38.63403645291922,-94.581298828125 39.078908097064755,-97.51464843749997 35.487511023853756,-106.62506103515624 35.09743980936422,-111.66503906250001 35.20972164522138,-114.33471679687499 34.461277288437046,-117.01538085937501 34.89494244739733,-118.23486328124999 34.05265942137598)")[0];

        List<IItemForMap> multiLine = NetTopologyHelper.GetItemsFromWkt("GEOMETRYCOLLECTION(LINESTRING(-108.10546874999999 37.50972584293751, -115.75195312499997 44.71551373202132), LINESTRING(-107.44628906249999 37.47485808497102, -99.84374999999999 44.65302415981199), LINESTRING(-106.9189453125 37.020098201368114, -99.31640625 28.960088688006806), LINESTRING(-108.369140625 36.597889133070225, -115.53222656249999 29.305561325527705))");
        lineItem.Name = "SingleLineItem";

        IItemForMap singlePolygon = NetTopologyHelper.GetItemsFromWkt("POLYGON((-88.154296875 36.597889133070225,-80.5078125 36.597889133070225,-79.01367187500001 33.35806161277887,-81.29882812500003 31.54108987958584,-80.9912109375 29.190532832294565,-80.15625000000003 27.64460638194332,-79.892578125 25.91852616207518,-82.0458984375 26.15543796871357,-82.79296875000001 27.877928333679534,-82.79296875000001 28.998531814051802,-84.0673828125 30.18312184219552,-87.71484375000001 35.960222969296694,-88.154296875 36.597889133070225))")[0];
        singlePolygon.Name = "SinglePolygonItem";

        string txt = File.ReadAllText("geometry_collection.wkt");
        List<IItemForMap> multiPolygons = NetTopologyHelper.GetItemsFromWkt(txt);

        List<IItemForMap> innerRingPolygons = NetTopologyHelper.GetItemsFromWkt("MULTIPOLYGON(((-108.72070312499997 34.99400375757577,-100.01953124999997 46.58906908309183,-90.79101562499996 34.92197103616377,-108.72070312499997 34.99400375757577),(-100.10742187499997 41.47566020027821,-102.91992187499996 37.61423141542416,-96.85546874999996 37.54457732085582,-100.10742187499997 41.47566020027821)),((-85.16601562499999 34.84987503195417,-80.771484375 28.497660832963476,-76.904296875 34.92197103616377,-85.16601562499999 34.84987503195417)))");

        return new ObservableCollection<IItemForMap>
        {
            pointItemForMap,
            new CompositeMapItem("MultiPointsItem", multiPoints),
            lineItem,
            new CompositeMapItem("MultiLineItem", multiLine),
            singlePolygon,
            new CompositeMapItem("MultiPolygonItem",  multiPolygons),
            new CompositeMapItem("InnerRingPolygons", innerRingPolygons)
        };
    }

    private static ObservableCollection<TimedLineForMap> createTimedItems() =>
        new()
        {
            new TimedLineForMap
            {
                Name = "Timed Line 1",
                Item = new TimedLineItem
                {
                    Locations = new ObservableCollection<TimedLocation>
                    {
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:00Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-80.06835937500001 33.32134852669881)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:01Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-81.9580078125 33.906895551288684)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:02Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-84.9462890625 34.01624188966704)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:03Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-88.37402343750001 33.651208299204995)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:04Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-92.02148437500001 33.94335994657882)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:05Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-95.00976562500001 35.0659731379842)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:06Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-98.39355468750001 34.84987503195417)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:07Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-101.38183593750001 34.27083595165)")
                        }
                    }
                }
            },

            new TimedLineForMap
            {
                Name = "Timed Line 2",
                Item = new TimedLineItem
                {
                    Locations = new ObservableCollection<TimedLocation>
                    {
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:00Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-122.3321 47.6062)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:01Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-122.6587 45.5122)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:02Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-122.8756 42.3265)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:03Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-119.8138 39.5296)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:04Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-119.4179 36.7783)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:05Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-118.2437 34.0522)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:06Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-117.1611 32.7157)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:07Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-117.0371 32.5343)")
                        }
                    }
                }
            },

            new TimedLineForMap
            {
                Name = "Timed Line 3",
                Item = new TimedLineItem
                {
                    Locations = new ObservableCollection<TimedLocation>
                    {
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:00Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-69.4455 45.2538)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:01Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-71.3824 42.4072)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:02Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-74.0060 40.7128)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:03Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-77.0369 38.9072)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:04Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-78.6382 35.7796)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:05Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-84.3880 33.7490)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:06Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-110.9747 32.2226)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:07Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-111.0937 31.9686)")
                        }
                    }
                }
            },

            new TimedLineForMap
            {
                Name = "Timed Line 4",
                Item = new TimedLineItem
                {
                    Locations = new ObservableCollection<TimedLocation>
                    {
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:00Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-80.1918 25.7617)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:01Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-95.3698 29.7604)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:02Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-96.7970 32.7767)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:03Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-97.5164 35.4676)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:04Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-115.1398 36.1699)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:05Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-118.2437 34.0522)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:06Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-119.4179 36.7783)")
                        },
                        new TimedLocation
                        {
                            Time = DateTime.Parse("2025-06-27T10:00:07Z"),
                            Value = NetTopologyHelper.GetLocationFromWktPoint("POINT(-122.4194 37.7749)")
                        }
                    }
                }
            }
        };
}