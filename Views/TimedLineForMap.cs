using System;
using System.Collections.Generic;
using System.Linq;

using MapControl;

namespace WpfMapPlayground.Views;

public class TimedLineForMap : ItemForMap<TimedLineItem>
{
    private LocationCollection m_polyLine;

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
            a.AddRange(trackItem.Positions.Select(toMapLocation));
            PolyLines = a;
        }
    }

    private Location toMapLocation(TimedLlaPosition arg) => new(arg.Value.Latitude, arg.Value.Longitude);

    public void UpdateByDate(DateTime date)
    {
        if (Item is TimedLineItem trackItem)
        {
            List<TimedLlaPosition> filteredPositions = trackItem.Positions
                                                                .Where(p => p.Time <= date)
                                                                .ToList();
            PolyLines = new LocationCollection(filteredPositions.Select(toMapLocation));
        }
    }
}