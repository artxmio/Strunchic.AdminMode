using Strunchic.AdminMode.ViewModel;
using System.Windows;

namespace Strunchic.AdminMode.View;

public partial class AddItemWindow : Window
{
    private AddItemViewModel _viewModel = null!;

    public AddItemWindow(AddItemViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;

        this.DataContext = _viewModel;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
