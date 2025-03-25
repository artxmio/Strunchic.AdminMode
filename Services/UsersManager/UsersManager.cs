using Microsoft.EntityFrameworkCore;
using Strunchik.Model.User;
using Strunchik.ViewModel.ApplicationContext;
using System.Collections.ObjectModel;

namespace Strunchic.AdminMode.Services.UsersManager;

public class UsersManager : BaseManager.BaseManager
{
    public ObservableCollection<UserModel> Data = null!;
    public UserModel SelectedUser = null!;

    public UsersManager(ApplicationContext context) : base(context)
    {
        if(context is null)
        {
            throw new NullReferenceException("context is null");
        }

        _context.Users.Load();
        Data = _context.Users.Local.ToObservableCollection();
    }
}
