using Microsoft.EntityFrameworkCore;
using Strunchik.Model.Item;
using Strunchik.ViewModel.ApplicationContext;
using Strunchik.ViewModel.Commands;
using System.Windows;
using System.Windows.Input;

namespace Strunchic.AdminMode.ViewModel;

public class AddItemViewModel
{
    private ApplicationContext _context = null!;

    public ItemModel NewItem { get; set; }
    public ItemsType SelectedType { get; set; } = null!;
    
    public List<ItemsType> Types { get; set; } 

    public ICommand AddItemCommand { get; }

    public AddItemViewModel(ApplicationContext context)
    {
        _context = context;

        _context.InstrumentTypes.Load();

        Types = [.. _context.InstrumentTypes.Local];

        NewItem = new ItemModel();

        AddItemCommand = new RelayCommand(async _ => await AddItem());
    }

    private async Task AddItem()
    {
        if (string.IsNullOrEmpty(NewItem.Title) || NewItem.Price <= 0 || string.IsNullOrEmpty(NewItem.Description) || SelectedType is null)
        {
            MessageBox.Show("Заполните все поля.", "Внимание!");
            return;
        }

        await _context.Items.AddAsync(NewItem);
        NewItem.TypeId = SelectedType.Id;
        MessageBox.Show("Товар будет добавлен в ближайшее время.\nВы можете закрыть это окно или добавть ещё один товар.", "Внимание!");

        _context.SaveChanges();
    }
}
