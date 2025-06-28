using System;
using System.Windows;

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

        public MainViewModel()
        {
            ItemsForMapVm = new ItemsForMapViewModel();
            MasterTracksVm = new MasterTracksViewModel();
            MasterTracksVm.OnAddIem += ItemsForMapVm.Add;
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
                        DateSliderVm = new DateSliderViewModel(start,end);
                        DateSliderVm.Update();
                        DateSliderVm.SelectedDateChanged += ItemsForMapVm.UpdateSelectedDate;
                    }
                }
                
                SetProperty(ref m_useTimeLaps, value);
            }
        }

        public ItemsForMapViewModel ItemsForMapVm { get; set; }

        public MasterTracksViewModel MasterTracksVm { get; set; }

        public DateSliderViewModel DateSliderVm
        {
            get => m_dateSliderVm;
            set
            {
                if (Equals(value, m_dateSliderVm))
                {
                    return;
                }

                m_dateSliderVm = value;
                OnPropertyChanged();
            }
        }
    }
}