using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

using MapControl;

namespace WpfMapPlayground.Views
{
    [ContentProperty(nameof(MapLayer))]
    public class MapLayerMenuItem : MapMenuItem
    {
        public virtual FrameworkElement MapLayer { get; set; }

        public Func<Task<FrameworkElement>> MapLayerFactory { get; set; }

        protected override bool GetIsChecked(MapBase map) => map.Children.Contains(MapLayer);

        public override async Task Execute(MapBase map)
        {
            FrameworkElement layer = MapLayer ?? (MapLayer = await MapLayerFactory.Invoke());

            if (layer != null)
            {
                map.MapLayer = layer;
                IsChecked = true;
            }
        }
    }

    public class MapOverlayMenuItem : MapLayerMenuItem
    {
        public override async Task Execute(MapBase map)
        {
            FrameworkElement layer = MapLayer ?? (MapLayer = await MapLayerFactory.Invoke());

            if (layer != null)
            {
                if (map.Children.Contains(layer))
                {
                    map.Children.Remove(layer);
                }
                else
                {
                    int index = 1;

                    foreach (FrameworkElement itemLayer in ParentMenuItems
                                                           .OfType<MapOverlayMenuItem>()
                                                           .Select(item => item.MapLayer)
                                                           .Where(itemLayer => itemLayer != null))
                    {
                        if (itemLayer == layer)
                        {
                            map.Children.Insert(index, itemLayer);
                            break;
                        }

                        if (map.Children.Contains(itemLayer))
                        {
                            index++;
                        }
                    }
                }

                IsChecked = true;
            }
        }
    }


    [ContentProperty(nameof(MapProjection))]
    public class MapProjectionMenuItem : MapMenuItem
    {
        
        public string MapProjection { get; set; }

        protected override bool GetIsChecked(MapBase map)
        {
            return map.MapProjection.CrsId == MapProjection;
        }

        public override Task Execute(MapBase map)
        {
            bool success = true;

            if (map.MapProjection.CrsId != MapProjection)
            {
                try
                {
                    map.MapProjection = MapProjectionFactory.Instance.GetProjection(MapProjection);
                }
                catch (Exception ex)
                {                    
                    success = false;
                }
            }

            IsChecked = success;

            return Task.CompletedTask;
        }
    }
}