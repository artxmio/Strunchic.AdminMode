using OxyPlot;
using Strunchik.ViewModel.ApplicationContext;

namespace Strunchic.AdminMode.Services.BaseChartService;

public abstract class BaseChartService
{
    protected PlotModel _model;
    protected ApplicationContext _context;

    protected BaseChartService(PlotModel model, ApplicationContext context)
    {
        _model = model;
        _context = context;
    }

    public abstract void SetPlotSettings();
}
