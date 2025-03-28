using Microsoft.EntityFrameworkCore;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Strunchik.ViewModel.ApplicationContext;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Strunchic.AdminMode.Services.UserChartService;

public class UserChartService : INotifyPropertyChanged
{
    private PlotModel _userPlotModel;
    private ApplicationContext _context;

    public event PropertyChangedEventHandler? PropertyChanged;

    public PlotModel UserPlotModel
    {
        get => _userPlotModel;
        set
        {
            _userPlotModel = value;
            OnPropertyChanched();
        }
    }

    public UserChartService(ApplicationContext context)
    {
        _userPlotModel = new PlotModel();
        _context = context;

        SetPlotSettings();
    }

    public void SetPlotSettings()
    {
        _context.Users.Load();

        /*
            Таблица Users преобразуется в коллекцию IEnumerable<object>,
            Группируется по датам регистрации RegistrationData,
            Выбираются данные в коллекцию типа IEnumerable<CountUserBy>,
            объекты которой содержат дату регистрации и количество пользователей в эту дату
        */
        var userCountsByDate = _context.Users
         .AsEnumerable()
         .GroupBy(u => u.RegistrationData.Date)
         .Select(g => new CountUserByDate
                     {
                         Date = g.Key,
                         UserCount = g.Count()
                     })
         .OrderBy(x => x.Date)
         .ToList();

        //Ось X с датами регистрации
        UserPlotModel.Axes.Add(new DateTimeAxis
        {
            Position = AxisPosition.Bottom,
            StringFormat = "dd.MM.yyyy",
            Title = "Дата",
            IntervalType = DateTimeIntervalType.Auto,
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            MajorStep = 5
        });

        //Ось Y с количеством пользователей
        UserPlotModel.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Title = "Количество пользователей",
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            StringFormat = "0",
            MajorStep = 1
        });

        // Данные для графика
        var series = new LineSeries
        {
            Title = "Заказы",
            MarkerType = MarkerType.Circle
        };

        foreach (var count in userCountsByDate)
        {
            series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(count.Date), count.UserCount));
        }

        UserPlotModel.Series.Add(series);

        OnPropertyChanched(nameof(UserPlotModel));
    }
    private void OnPropertyChanched([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}


public class CountUserByDate
{
    public int UserCount { get; set; } = 0;
    public DateTime Date { get; set; } = DateTime.Now;
}