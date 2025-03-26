using Microsoft.EntityFrameworkCore;
using Strunchik.Model.Order;
using Strunchik.ViewModel.ApplicationContext;
using System.Collections.ObjectModel;
using System.Windows;

namespace Strunchic.AdminMode.Services.OrderManager;

public class OrderManager : BaseManager.BaseManager
{
    public ObservableCollection<OrderModel> Data { get; set; }
    public OrderModel SelectedOrder { get; set; }

    public OrderManager(ApplicationContext context) : base(context)
    {
        if (context is null)
        {
            throw new NullReferenceException("context is null");
        }

        SelectedOrder = new OrderModel();

        _context.Orders.Load();
        Data = _context.Orders.Local.ToObservableCollection();
    }

    public override void Add()
    {}

    public override void Delete()
    {
        if (SelectedOrder is null)
        {
            MessageBox.Show("Выберите заказ,\nкоторый хотите удалить.", "Внимание!");
            return;
        }

        _context.Remove(SelectedOrder);
        _context.SaveChanges();
    }
}
