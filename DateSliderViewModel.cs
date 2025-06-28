using System;

using WpfMapPlayground.Views;

namespace WpfMapPlayground;

public class DateSliderViewModel : ObservableObject
{
    private DateTime _selectedDate;
    private double   m_maxTicks;
    private double   m_minTicks;
    private double   m_maxTicks1;
    private string   m_selectedTimeString;

    public event Action<DateTime> SelectedDateChanged;

    public DateTime MinDate { get; } = new DateTime(2020, 1, 1);
    public DateTime MaxDate { get; } = new DateTime(2030, 1, 1);

    public double MinTicks
    {
        get => m_minTicks;
        set => SetProperty(ref m_minTicks, value);
    }

    public double MaxTicks
    {
        get => m_maxTicks1;
        set => SetProperty(ref m_maxTicks1, value);
    }

    public double SelectedTicks
    {
        get => SelectedDate.Ticks;
        set
        {
            SelectedDate = new DateTime((long)value);
            SelectedTimeString = SelectedDate.ToString("HH:mm:ss");
            OnPropertyChanged(nameof(SelectedTicks));
            SelectedDateChanged?.Invoke(SelectedDate);
        }
    }

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set => SetProperty(ref _selectedDate, value);
    }

    public string SelectedTimeString
    {
        get => m_selectedTimeString;
        set => SetProperty(ref m_selectedTimeString, value);
    }

    public void Update() 
    {
        OnPropertyChanged(nameof(MinDate));
        OnPropertyChanged(nameof(MaxDate));
        OnPropertyChanged(nameof(MinTicks));
        OnPropertyChanged(nameof(MaxTicks));
    }

    public DateSliderViewModel(DateTime start, DateTime end)
    {
        MinDate = start;
        MaxDate = end;
        MinTicks = start.Ticks;
        MaxTicks = end.Ticks;
        SelectedDate = start;        
    }
}