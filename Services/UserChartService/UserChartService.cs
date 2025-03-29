using Microsoft.EntityFrameworkCore;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Strunchik.ViewModel.ApplicationContext;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Strunchic.AdminMode.Services.UserChartService;

public class UserChartService : BaseChartService.BaseChartService, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public PlotModel UserPlotModel
    {
        get => _model;
        set
        {
            _model = value;
            OnPropertyChanged();
        }
    }

    public UserChartService(ApplicationContext context) : base(new PlotModel(), context)
    {
        SetPlotSettings();
    }

    public override void SetPlotSettings()
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
            IntervalType = DateTimeIntervalType.Days,
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

        OnPropertyChanged(nameof(UserPlotModel));
    }

    private void OnPropertyChanged([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
