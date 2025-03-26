using Strunchik.Model.Item;
using Strunchik.ViewModel.ApplicationContext;
using Strunchik.ViewModel.Commands;
using System.Windows;
using System.Windows.Input;

namespace Strunchic.AdminMode.ViewModel;

public class AddTypeViewModel
{
    private readonly ApplicationContext _context;

    public ItemsType NewType { get; set; }

    public ICommand AddItemCommand { get; }

    public AddTypeViewModel(ApplicationContext context)
    {
        _context = context;

        NewType = new ItemsType();

        AddItemCommand = new RelayCommand(async _ => await AddType());
    }

    private async Task AddType()
    {
        if (string.IsNullOrEmpty(NewType.Title))
        {
            MessageBox.Show("Заполните все поля.", "Внимание!");
            return;
        }

        await _context.InstrumentTypes.AddAsync(NewType);
        MessageBox.Show("Тип будет добавлен в ближайшее время.", "Внимание!");

        _context.SaveChanges();
    }
}
