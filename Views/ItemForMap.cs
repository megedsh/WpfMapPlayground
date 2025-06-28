using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace WpfMapPlayground.Views;

public interface IItemForMap 
{
    Guid Id { get; set; }
    string Name { get; set; }
    Color Color { get; set; }
    bool VisibleOnMap { get; set; }
    int Thickness { get; set; }
    object Item { get; set; }
}
public abstract class ItemForMap : ObservableObject, IItemForMap
{
    private Color  m_color        = Colors.White;
    private bool   m_visibleOnMap = true;
    private int    m_thickness    = 3;        
    private object m_item;
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }

    public Color Color
    {
        get => m_color;
        set => SetProperty(ref m_color, value);
    }

    public bool VisibleOnMap
    {
        get => m_visibleOnMap;
        set => SetProperty(ref m_visibleOnMap, value);
    }

    [Range(1, 10)]
    public int Thickness
    {
        get => m_thickness;
        set => SetProperty(ref m_thickness, value);
    }

    public object Item
    {
        get => m_item;            
        set => SetProperty(ref m_item, value);            
    }
}