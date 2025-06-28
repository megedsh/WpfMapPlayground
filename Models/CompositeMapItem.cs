using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using WpfMapPlayground.Views;

namespace WpfMapPlayground.Models;

public class CompositeMapItem : ObservableObject, IItemForMap
{
    private Color m_color;
    private bool  m_visibleOnMap;
    private int   m_thickness;
    public List<IItemForMap> ItemsForMap { get; }

    public CompositeMapItem(string name,List<IItemForMap> itemsForMap)
    {
        ItemsForMap = itemsForMap;
        Name = name;        
        Thickness = 3;
        VisibleOnMap = true;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public Color Color
    {
        get => m_color;
        set
        {
            foreach (IItemForMap item in ItemsForMap)
            {
                item.Color = value;
            }

            SetProperty(ref m_color, value);
        }
    }

    public bool VisibleOnMap
    {
        get => m_visibleOnMap;
        set
        {
            foreach (IItemForMap item in ItemsForMap)
            {
                item.VisibleOnMap = value;
            }

            SetProperty(ref m_visibleOnMap, value);
        }
    }

    public int Thickness
    {
        get => m_thickness;
        set
        {
            foreach (IItemForMap item in ItemsForMap)
            {
                item.Thickness = value;
            }

            SetProperty(ref m_thickness, value);
        }
    }

    public int PointCount => ItemsForMap.Select(i => i.PointCount).Sum();

    public object Item { get; set; }
}