using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

using MapControl;

using NetTopologySuite.IO;

using WpfMapPlayground.Dialogs;

using Geometry = NetTopologySuite.Geometries.Geometry;

namespace WpfMapPlayground.Views
{
    public class ItemsForMapViewModel : ObservableObject
    {
        private          IItemForMap                                   m_selectedItem;
        private          ObservableCollection<TimedLineForMap>         m_timedLineItems;
        private          ObservableCollection<LineItemForMap>          m_lineItems;
        private          ObservableCollection<SimplePolygonItemForMap> m_simplePolygonItems;
        private          ObservableCollection<PointItemForMap>         m_pointItems;
        private          ObservableCollection<MultiPolygonItemForMap>  m_multiPolygonItems;
        private readonly ColorRoundRobin                               m_colors;
        public ObservableCollection<IItemForMap> Items { get; } = new ObservableCollection<IItemForMap>();

        public ObservableCollection<TimedLineForMap> TimedLineItems
        {
            get => m_timedLineItems;
            set => SetProperty(ref m_timedLineItems, value);
        }

        public ObservableCollection<LineItemForMap> LineItems
        {
            get => m_lineItems;
            set => SetProperty(ref m_lineItems, value);
        }

        public ObservableCollection<SimplePolygonItemForMap> SimplePolygonItems
        {
            get => m_simplePolygonItems;
            set => SetProperty(ref m_simplePolygonItems, value);
        }

        public ObservableCollection<MultiPolygonItemForMap> MultiPolygonItems
        {
            get => m_multiPolygonItems;
            set => SetProperty(ref m_multiPolygonItems, value);
        }

        public ObservableCollection<PointItemForMap> PointItems
        {
            get => m_pointItems;
            set => SetProperty(ref m_pointItems, value);
        }

        public ICommand AddFromStringCommand { get; }
        public ICommand RemoveCommand { get; set; }

        public ItemsForMapViewModel()
        {
            TimedLineItems = new ObservableCollection<TimedLineForMap>();
            PointItems = new ObservableCollection<PointItemForMap>();
            LineItems = new();
            SimplePolygonItems = new();
            MultiPolygonItems = new ObservableCollection<MultiPolygonItemForMap>();
            RemoveCommand = new RelayCommand<IItemForMap>(remove);
            AddFromStringCommand = new RelayCommand(addFromString);
            m_colors = new ColorRoundRobin();
        }

        private void addFromString()
        {
            AddTextItemViewModel vm = new AddTextItemViewModel
            {
                //Name = "Wkt1",
                Text = "GEOMETRYCOLLECTION(POINT(34.9733695343668 31.350654293881988),POINT(35.05679837390489 31.904742591635866),POINT(34.66746378939381 31.41712905957327),POINT(34.15576690689353 31.421875456441796),POINT(35.10129375499187 30.778947748038846),POINT(34.200262287980514 32.22525517006349))"
            };
            PropertyDialog dialog = new PropertyDialog
            {
                OkButtonDataErrorAware = true,
                DataContext = vm,
                Title = "Add Text Item",
            };

            if (dialog.ShowDialog().Value)
            {
                WKTReader reader = new WKTReader();
                Geometry geometry = reader.Read(vm.Text);

                List<ItemForMap> itemForMaps = NetTopologyHelper.GetItems(geometry);
                Add(new CompositeMapItem(vm.Name, itemForMaps, m_colors.GetNextColor()));
            }
        }

        private void remove(IItemForMap item)
        {
            if (item == null)
            {
                return;
            }
            if(item is CompositeMapItem compositeItem)
            {
                foreach (ItemForMap subItem in compositeItem.ItemForMaps)
                {
                    remove(subItem);
                }
            }

            Items.Remove(item);

            foreach (var coll in new IList[] {
                TimedLineItems,
                LineItems,
                SimplePolygonItems,
                MultiPolygonItems,
                PointItems
            }

)
            {
                coll.Remove(item);
            }            
        }

        public IItemForMap SelectedItem
        {
            get => m_selectedItem;
            set => SetProperty(ref m_selectedItem, value);
        }

        public void Add(object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }

            if (Items.Select(i => i.Item).Contains(item))
            {
                return; // Item already exists in the collection
            }

            switch (item)
            {
                case TimedLineItem tli:
                    addTimedLineItem(item, tli);
                    break;
                case CompositeMapItem cmi:
                    addCompositItem(cmi);
                    break;
            }
        }

        private void addCompositItem(CompositeMapItem cmi)
        {
            foreach (ItemForMap item in cmi.ItemForMaps)
            {
                switch (item)
                {
                    case PointItemForMap pointItem:
                        PointItems.Add(pointItem);
                        break;
                    case LineItemForMap lineItem:
                        LineItems.Add(lineItem);
                        break;
                    case SimplePolygonItemForMap polygonItem:
                        SimplePolygonItems.Add(polygonItem);
                        break;
                    case MultiPolygonItemForMap mpi:
                        MultiPolygonItems.Add(mpi);
                        break;
                }
            }

            Items.Add(cmi);
        }

        private void addTimedLineItem(object item, TimedLineItem tli)
        {
            string itemName = string.Empty;
            ItemForMap newItem = null;

            itemName = tli.Name;

            TimedLineForMap t = new TimedLineForMap
            {
                Name = itemName,
                Item = item,
                Color = m_colors.GetNextColor(),
            };
            t.UpdateAll();
            newItem = t;
            TimedLineItems.Add(t);
            Items.Add(newItem);
        }

        public void GetTimeRange(out DateTime start, out DateTime end)
        {
            start = DateTime.MaxValue;
            end = DateTime.MaxValue;
            foreach (ItemForMap item in Items)
            {
                if (item is TimedLineForMap ti)
                {
                    if (ti.Item is TimedLineItem trackItem && trackItem.Positions.Any())
                    {
                        start = trackItem.Positions.Min(p => p.Time);
                        end = trackItem.Positions.Max(p => p.Time);
                        return;
                    }
                }
            }
        }

        public void UpdateSelectedDate(DateTime date)
        {
            foreach (ItemForMap item in Items)
            {
                if (item is TimedLineForMap trackItemForMap)
                {
                    trackItemForMap.UpdateByDate(date);
                }
            }
        }
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