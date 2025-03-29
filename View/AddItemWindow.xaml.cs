using Strunchic.AdminMode.ViewModel;
using System.IO;
using System.Windows;
using System.Drawing;

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

    private void SelectImage_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "Изображения (*.jpg;*.png)|*.jpg;*.png",
            Title = "Выберите изображение товара"
        };

        if (dialog.ShowDialog() == true)
        {
            string sourceFilePath = dialog.FileName;

            using (Image image = Image.FromFile(sourceFilePath))
            {
                int width = image.Width;
                int height = image.Height;

                if (width != height)
                {
                    MessageBox.Show("Высота и ширина изображения должны быть одинаковы.", "Внимание!");
                    return;
                }
            }

            string destinationFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/Images/ItemImages");
            Directory.CreateDirectory(destinationFolder);

            string destinationFilePath = Path.Combine("Resources/Images/ItemImages/", Path.GetFileName(sourceFilePath));

            File.Copy(sourceFilePath, destinationFilePath, overwrite: true);

            if (_viewModel != null)
            {
                _viewModel.NewItem.ImagePath = destinationFilePath;
            }

            MessageBox.Show("Изображение было добавлено", "Внимание!");
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }
}
