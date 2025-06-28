using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace WpfMapPlayground.Views;

public class CompositeMapItem : ObservableObject, IItemForMap
{
    private Color m_color;
    private bool  m_visibleOnMap;
    private int   m_thickness;
    public List<ItemForMap> ItemForMaps { get; }

    public CompositeMapItem(string name,
        List<ItemForMap> itemForMaps, Color color)
    {
        ItemForMaps = itemForMaps;
        Name = name;
        Color = color;
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
            foreach (ItemForMap item in ItemForMaps)
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
            foreach (ItemForMap item in ItemForMaps)
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
            foreach (ItemForMap item in ItemForMaps)
            {
                item.Thickness = value;
            }

            SetProperty(ref m_thickness, value);
        }
    }

    public object Item { get; set; }
}