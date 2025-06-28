using System;
using System.Windows;
using WpfMapPlayground.ViewModels;
using WpfMapPlayground.Views;

namespace WpfMapPlayground
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
    public class MainViewModel : ObservableObject
    {
        private bool                m_useTimeLaps;
        private DateSliderViewModel m_dateSliderVm;
        private DateSliderViewModel m_dateSliderVm1;

        public MainViewModel()
        {
            ItemsForMapVm = new ItemsForMapViewModel();
            ExampleGeometriesVm = new ExampleGeometriesViewModel();
            ExampleGeometriesVm.OnAddIem += ItemsForMapVm.Add;
        }

        public bool UseTimeLaps
        {
            get => m_useTimeLaps;
            set
            {
                if (value)
                {
                    ItemsForMapVm.GetTimeRange(out DateTime start, out DateTime end);
                    if (end != DateTime.MaxValue)
                    {
                        DateSliderVm = new DateSliderViewModel(start, end);
                        DateSliderVm.Update();
                        DateSliderVm.SelectedDateChanged += ItemsForMapVm.UpdateSelectedDate;
                    }
                }
                else
                {
                    ItemsForMapVm.RemoveDateConstraint();
                }

                SetProperty(ref m_useTimeLaps, value);
            }
        }

        public ItemsForMapViewModel ItemsForMapVm { get; set; }

        public ExampleGeometriesViewModel ExampleGeometriesVm { get; set; }

        public DateSliderViewModel DateSliderVm
        {
            get => m_dateSliderVm1;
            set => SetProperty(ref m_dateSliderVm1, value);
        }
    }
}