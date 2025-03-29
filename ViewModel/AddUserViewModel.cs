using Microsoft.EntityFrameworkCore;
using Strunchik.Model.User;
using Strunchik.ViewModel.ApplicationContext;
using Strunchik.ViewModel.Commands;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Strunchic.AdminMode.ViewModel;

public class AddUserViewModel
{
    private readonly ApplicationContext _context;

    public UserModel NewUser { get; set; }

    public ICommand AddUserCommand { get; }

    public AddUserViewModel(ApplicationContext context)
    {
        _context = context;

        NewUser = new UserModel();

        AddUserCommand = new RelayCommand(async _ => await AddUser());
    }

    private async Task AddUser()
    {
        Regex EmailRegex = new Regex("^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$", RegexOptions.Compiled);
        Regex NameRegex = new Regex("^[A-Za-zА-Яа-яЁё\\s]+$", RegexOptions.Compiled);
        Regex PasswordRegex = new Regex("^[A-Za-z\\d@$!%*?&]{8,}$", RegexOptions.Compiled);

        if (string.IsNullOrEmpty(NewUser.Email) || string.IsNullOrEmpty(NewUser.Name) || string.IsNullOrEmpty(NewUser.Password))
        {
            MessageBox.Show("Заполните все поля.", "Внимание!");
            return;
        }

        var isValid = EmailRegex.IsMatch(NewUser.Email) && NameRegex.IsMatch(NewUser.Name) && PasswordRegex.IsMatch(NewUser.Password);

        if (!isValid)
        {
            MessageBox.Show("Корректно заполните все поля.", "Внимание!");
            return;
        }

        if (await _context.Users.AnyAsync(user => user.Email == NewUser.Email))
        {
            MessageBox.Show("Такой пользователь уже существует, пожалуйста, используйте другую почту!", "Внимание!");
            return;
        }

        NewUser.RegistrationData = DateTime.Now;

        await _context.Users.AddAsync(NewUser);
        MessageBox.Show("Пользователь будет добавлен в ближайшее время.", "Внимание!");

        _context.SaveChanges();
    }
}
