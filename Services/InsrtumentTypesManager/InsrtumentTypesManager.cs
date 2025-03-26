using Microsoft.EntityFrameworkCore;
using Strunchic.AdminMode.View;
using Strunchic.AdminMode.ViewModel;
using Strunchik.Model.Item;
using Strunchik.ViewModel.ApplicationContext;
using System.Collections.ObjectModel;
using System.Windows;

namespace Strunchic.AdminMode.Services.InsrtumentTypesManager;

public class InsrtumentTypesManager : BaseManager.BaseManager
{
    public ObservableCollection<ItemsType> Data { get; set; }
    public ItemsType SelectedType { get; set; }

    public InsrtumentTypesManager(ApplicationContext context) : base(context)
    {
        if (context is null)
        {
            throw new NullReferenceException("context is null");
        }

        SelectedType = new ItemsType();

        _context.InstrumentTypes.Load();
        Data = _context.InstrumentTypes.Local.ToObservableCollection();
    }

    public override void Add()
    {
        var viewModel = new AddTypeViewModel(_context);
        var window = new AddTypeWindow(viewModel);

        window.ShowDialog();

        Data = _context.InstrumentTypes.Local.ToObservableCollection();
    }

    public override void Delete()
    {
        if (SelectedType is null)
        {
            MessageBox.Show("Выберите тип,\nкоторый хотите удалить.", "Внимание!");
            return;
        }

        _context.Remove(SelectedType);
        _context.SaveChanges();
    }
}
