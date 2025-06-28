using System;
using System.Collections.Generic;
using System.Linq;

using MapControl;
using WpfMapPlayground.ViewModels;

namespace WpfMapPlayground.Models;

public class TimedLineForMap : ItemForMap<TimedLineItem>
{
    private LocationCollection m_polyLine;

    public override int PointCount => TypedItem.Locations.Count;
    public LocationCollection PolyLines
    {
        get => m_polyLine;
        set => SetProperty(ref m_polyLine, value);
    }

    public void UpdateAll()
    {
        if (Item is TimedLineItem trackItem)
        {
            LocationCollection a = new LocationCollection();
            a.AddRange(trackItem.Locations.Select(ti=>ti.Value));
            PolyLines = a;
        }
    }
    
    public void UpdateByDate(DateTime date)
    {
        if (Item is TimedLineItem trackItem)
        {
            List<TimedLocation> filteredPositions = trackItem.Locations
                                                                .Where(p => p.Time <= date)
                                                                .ToList();
            PolyLines = new LocationCollection(filteredPositions.Select(ti=>ti.Value));
        }
    }
}