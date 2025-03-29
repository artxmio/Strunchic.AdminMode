using Microsoft.EntityFrameworkCore;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using Strunchic.AdminMode.Services.UserChartService;
using Strunchik.ViewModel.ApplicationContext;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Strunchic.AdminMode.Services.OrderChartService;

public class OrderChartService : BaseChartService.BaseChartService, INotifyPropertyChanged
{
    public PlotModel OrderPlotModel
    {
        get => _model;
        set
        {
            _model = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public OrderChartService(ApplicationContext context) : base(new OxyPlot.PlotModel(), context)
    {
        SetPlotSettings();
    }


    public override void SetPlotSettings()
    {
        _context.Orders.Load();

        var orderCountByDate = _context.Orders
         .AsEnumerable()
         .GroupBy(order => order.OrderDate.Date)
         .Select(g => new CountOrdersByDate
         {
             Date = g.Key,
             OrderCount = g.Count()
         })
         .OrderBy(x => x.Date)
         .ToList();

        if (orderCountByDate.Count == 0)
        {
            OrderPlotModel.Axes.Clear();
            OrderPlotModel.Series.Clear();
            OnPropertyChanged(nameof(OrderPlotModel));
            return;
        }

        OrderPlotModel.Axes.Add(new DateTimeAxis
        {
            Position = AxisPosition.Bottom,
            StringFormat = "dd.MM.yyyy",
            Title = "Дата",
            IntervalType = DateTimeIntervalType.Days,
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            MajorStep = 7,
            Angle = 45,
            Minimum = DateTimeAxis.ToDouble(orderCountByDate.First().Date),
            Maximum = DateTimeAxis.ToDouble(orderCountByDate.Last().Date)
        });

        OrderPlotModel.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Title = "Количество заказов",
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            StringFormat = "0",
            Minimum = 0,
            MajorStep = 1
        });


        // Данные для графика
        var series = new LineSeries
        {
            Title = "Заказы",
            MarkerType = MarkerType.Circle
        };

        foreach (var count in orderCountByDate)
        {
            series.Points.Add(new DataPoint(DateTimeAxis.ToDouble(count.Date), count.OrderCount));
        }

        OrderPlotModel.Series.Add(series);
        OnPropertyChanged(nameof(OrderPlotModel));
    }

    private void OnPropertyChanged([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}