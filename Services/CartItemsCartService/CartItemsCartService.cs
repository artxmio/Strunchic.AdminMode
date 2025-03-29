using Microsoft.EntityFrameworkCore;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using Strunchik.ViewModel.ApplicationContext;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Strunchic.AdminMode.Services.CartItemsCartService;

public class CartItemsCartService : BaseChartService.BaseChartService, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public PlotModel CartItemsPlotModel
    {
        get => _model;
        set
        {
            _model = value;
            OnPropertyChanged();
        }
    }

    public CartItemsCartService(ApplicationContext context) : base(new OxyPlot.PlotModel(), context)
    {
        SetPlotSettings();
    }

    public override void SetPlotSettings()
    {
        _context.CartItems.Load();
        _context.Items.Load();

        var itemsCountsByTitle = _context.CartItems
            .GroupBy(order => order.Item.Title)
            .Select(g => new CountItemsInOrders
            {
                Title = g.Key,
                Count = g.Count()
            })
            .OrderBy(x => x.Title)
            .ToList();

        if (itemsCountsByTitle.Count == 0)
        {
            if (CartItemsPlotModel != null)
            {
                CartItemsPlotModel.Axes.Clear();
                CartItemsPlotModel.Series.Clear();
                OnPropertyChanged(nameof(CartItemsPlotModel));
            }
            return;
        }

        if (CartItemsPlotModel == null)
        {
            CartItemsPlotModel = new PlotModel { Title = "Популярность товаров" };
        }
        else
        {
            CartItemsPlotModel.Axes.Clear();
            CartItemsPlotModel.Series.Clear();
        }

        var categoryAxis = new CategoryAxis
        {
            Position = AxisPosition.Left,
            Title = "Название товаров",
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            Angle = 30
        };

        foreach (var item in itemsCountsByTitle)
        {
            categoryAxis.Labels.Add(item.Title);
        }

        CartItemsPlotModel.Axes.Add(categoryAxis);

        CartItemsPlotModel.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Bottom,
            Title = "Количество заказов",
            MajorGridlineStyle = LineStyle.Solid,
            MinorGridlineStyle = LineStyle.Dot,
            StringFormat = "0",
            Minimum = 0,
            MajorStep = 1
        });

        var series = new BarSeries
        {
            Title = "Заказы",
            FillColor = OxyColors.Blue
        };

        foreach (var count in itemsCountsByTitle)
        {
            series.Items.Add(new BarItem
            {
                Value = count.Count
            });
        }

        CartItemsPlotModel.Series.Add(series);
        OnPropertyChanged(nameof(CartItemsPlotModel));

    }

    private void OnPropertyChanged([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}