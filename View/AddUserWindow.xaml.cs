using Strunchic.AdminMode.ViewModel;
using System.Windows;

namespace Strunchic.AdminMode.View;

public partial class AddUserWindow : Window
{
    private AddUserViewModel _viewModel;

    public AddUserWindow(AddUserViewModel viewModel)
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
