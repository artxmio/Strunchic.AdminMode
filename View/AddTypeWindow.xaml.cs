using Strunchic.AdminMode.ViewModel;
using System.Windows;

namespace Strunchic.AdminMode.View;

public partial class AddTypeWindow : Window
{
    private AddTypeViewModel _viewModel;

    public AddTypeWindow(AddTypeViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        DataContext = _viewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
