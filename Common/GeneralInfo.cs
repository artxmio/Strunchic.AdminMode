using Microsoft.EntityFrameworkCore;
using Strunchik.ViewModel.ApplicationContext;

namespace Strunchic.AdminMode.Common;

public class GeneralInfo
{
    private readonly ApplicationContext _context;

    public int TotalUsers => _context.Users.Local.Count;
    public int TotalOrders => _context.Orders.Local.Count;
    public decimal AverageAmout { get => _context.Orders.Local.Average(o => o.TotalAmount); }

    public GeneralInfo(ApplicationContext context)
    {
        _context = context;
        _context.Users.Load();
        _context.Orders.Load();
    }
}