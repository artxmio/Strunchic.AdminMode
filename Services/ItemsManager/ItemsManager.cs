using Microsoft.EntityFrameworkCore;
using Strunchic.AdminMode.View;
using Strunchic.AdminMode.ViewModel;
using Strunchik.Model.Item;
using Strunchik.Model.User;
using Strunchik.ViewModel.ApplicationContext;
using System.Collections.ObjectModel;
using System.Windows;

namespace Strunchic.AdminMode.Services.ItemsManager;

public class ItemsManager : BaseManager.BaseManager
{
    public ObservableCollection<ItemModel> Data = null!;
    public ItemModel SelectedItem = null!;

    public ItemsManager(ApplicationContext context) : base(context)
    {
        if (context is null)
        {
            throw new NullReferenceException("context is null");
        }

        _context.Items.Load();
        Data = _context.Items.Local.ToObservableCollection();
    }
    public override void Add()
    {
        var viewModel = new AddItemViewModel(_context);
        var window = new AddItemWindow(viewModel);

        window.ShowDialog();

        Data = _context.Items.Local.ToObservableCollection();
    }

    public override void Delete()
    {
        if (SelectedItem is null)
        {
            MessageBox.Show("Выберите товар,\nкоторый хотите удалить.", "Внимание!");
            return;
        }

        _context.Remove(SelectedItem);
        _context.SaveChanges();
    }
}
