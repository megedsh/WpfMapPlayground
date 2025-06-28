using MapControl;
using NetTopologySuite.Index.HPRtree;
using System;
using System.Collections.ObjectModel;

using WpfMapPlayground.Views;

namespace WpfMapPlayground.Models
{

    public class TimedLineItem
    {
        public ObservableCollection<TimedLocation> Locations { get; init; }
    }

    public class TimedLocation : Timed<Location>
    {
    }

    public class Timed<T>
    {
        public DateTime Time { get; init; }
        public T Value { get; init; }
    }


    public class ItemForMap<T> : ItemForMap
    {
        public T TypedItem => (T)Item;
    }
       

    public class LineItemForMap : ItemForMap<LocationCollection>
    {
    }

    public class SimplePolygonItemForMap : ItemForMap<LocationCollection>
    {
    }

    public class MultiPolygonItemForMap : ItemForMap<LocationCollection[]>
    {
    }
    public class PointItemForMap : ItemForMap<Location>
    {
    }
}