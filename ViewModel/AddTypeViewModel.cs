using Microsoft.EntityFrameworkCore;
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

        if (await _context.InstrumentTypes.AnyAsync(type => type.Title == NewType.Title))
        {
            MessageBox.Show("Такой тип уже существует, пожалуйста введите другое название!", "Внимание!");
            return;
        }

        await _context.InstrumentTypes.AddAsync(NewType);
        MessageBox.Show("Тип будет добавлен в ближайшее время.", "Внимание!");

        _context.SaveChanges();
    }
}
