using Microsoft.EntityFrameworkCore;
using Strunchic.AdminMode.View;
using Strunchic.AdminMode.ViewModel;
using Strunchik.Model.User;
using Strunchik.ViewModel.ApplicationContext;
using System.Collections.ObjectModel;
using System.Windows;

namespace Strunchic.AdminMode.Services.UsersManager;

public class UsersManager : BaseManager.BaseManager
{
    public ObservableCollection<UserModel> Data = null!;
    public UserModel SelectedUser = null!;

    public UsersManager(ApplicationContext context) : base(context)
    {
        if (context is null)
        {
            throw new NullReferenceException("context is null");
        }

        _context.Users.Load();
        Data = _context.Users.Local.ToObservableCollection();
    }

    public override void Add()
    {
        var viewModel = new AddUserViewModel(_context);
        var window = new AddUserWindow(viewModel);

        window.ShowDialog();

        Data = _context.Users.Local.ToObservableCollection();
    }

    public override void Delete()
    {
        if (SelectedUser is null)
        {
            MessageBox.Show("Выберите пользователя,\nкоторого хотите удалить.", "Внимание!");
            return;
        }

        _context.Remove(SelectedUser);
    }
}
