namespace Strunchic.AdminMode.Services.OrderChartService;

public class CountOrdersByDate
{
    public int OrderCount { get; set; } = 0;
    public DateTime Date { get; set; } = DateTime.Now;
}