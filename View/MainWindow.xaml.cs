using OxyPlot;
using Strunchic.AdminMode.ViewModel;
using System.Windows;

namespace Strunchic.AdminMode.View;

public partial class MainWindow : Window
{
    private MainWindowViewModel _viewModel;

    public MainWindow()
    {
        InitializeComponent();

        _viewModel = new MainWindowViewModel();

        this.DataContext = new MainWindowViewModel();
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void TabControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        _viewModel.UserChartService.SetPlotSettings();
    }
}