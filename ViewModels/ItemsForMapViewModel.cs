using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using NetTopologySuite.IO;

using WpfMapPlayground.Dialogs;
using WpfMapPlayground.Models;
using WpfMapPlayground.Utils;
using WpfMapPlayground.Views;

using Geometry = NetTopologySuite.Geometries.Geometry;

namespace WpfMapPlayground.ViewModels
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
                Name = "Wkt1",
                Text = "GEOMETRYCOLLECTION(POINT(-103.44726562500001 35.49645605658418),POINT(-106.787109375 36.102376448736436),POINT(-108.720703125 39.027718840211605),POINT(-102.48046875000001 43.06888777416961),POINT(-87.05566406250001 39.909736234537206),POINT(-81.47460937500001 35.24561909420683),POINT(-87.36328125 33.32134852669881),POINT(-95.66894531250001 31.391157522824727),POINT(-95.75683593750003 36.38591277287654),POINT(-94.1748046875 42.488301979602255))"
            };
            PropertyDialog dialog = new PropertyDialog
            {
                OkButtonDataErrorAware = true,
                DataContext = vm,
                Title = "Add Text Item",
            };

            if (dialog.ShowDialog().Value)
            {
                try
                {
                    List<IItemForMap> itemForMaps = NetTopologyHelper.GetItemsFromWkt(vm.Text);

                    Add(new CompositeMapItem(vm.Name, itemForMaps));
                }
                catch(Exception e)
                {
                    System.Windows.MessageBox.Show($"Error parsing WKT: {e.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }   
            }
        }

        private void remove(IItemForMap item)
        {
            if (item == null)
            {
                return;
            }

            if (item is CompositeMapItem compositeItem)
            {
                foreach (ItemForMap subItem in compositeItem.ItemsForMap)
                {
                    remove(subItem);
                }
            }

            Items.Remove(item);

            foreach (IList coll in new IList[]
                     {
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
                return;
            }

            if (Items.Contains(item))
            {
                return; // Item already exists in the collection
            }

            switch (item)
            {
                case TimedLineForMap tli:
                    addTimedLineItem(tli);
                    break;
                case CompositeMapItem cmi:
                    addCompositItem(cmi);
                    break;
                case PointItemForMap pi:
                    pi.Color = m_colors.GetNextColor();
                    PointItems.Add(pi);
                    Items.Add(pi);
                    break;
                case LineItemForMap li:
                    li.Color = m_colors.GetNextColor();
                    LineItems.Add(li);
                    Items.Add(li);
                    break;
                case SimplePolygonItemForMap polygonItem:
                    polygonItem.Color = m_colors.GetNextColor();
                    SimplePolygonItems.Add(polygonItem);
                    Items.Add(polygonItem);
                    break;
            }
        }

        private void addCompositItem(CompositeMapItem cmi)
        {
            cmi.Color = m_colors.GetNextColor();
            foreach (ItemForMap item in cmi.ItemsForMap)
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

        private void addTimedLineItem(TimedLineForMap tli)
        {
            tli.Color = m_colors.GetNextColor();
            tli.UpdateAll();
            TimedLineItems.Add(tli);
            Items.Add(tli);
        }

        public void GetTimeRange(out DateTime start, out DateTime end)
        {
            start = DateTime.MaxValue;
            end = DateTime.MaxValue;
            foreach (IItemForMap item in Items)
            {
                if (item is TimedLineForMap ti)
                {
                    if (ti.Item is TimedLineItem trackItem && trackItem.Locations.Any())
                    {
                        start = trackItem.Locations.Min(p => p.Time);
                        end = trackItem.Locations.Max(p => p.Time);
                        return;
                    }
                }
            }
        }

        public void UpdateSelectedDate(DateTime date)
        {
            foreach (IItemForMap item in Items)
            {
                if (item is TimedLineForMap trackItemForMap)
                {
                    trackItemForMap.UpdateByDate(date);
                }
            }
        }

        public void RemoveDateConstraint()
        {
            foreach (IItemForMap item in Items)
            {
                if (item is TimedLineForMap trackItemForMap)
                {
                    trackItemForMap.UpdateAll();
                }
            }
        }
    }
}