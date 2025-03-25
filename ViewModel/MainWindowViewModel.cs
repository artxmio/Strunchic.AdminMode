using Strunchic.AdminMode.Services.UsersManager;
using Strunchik.Model.User;
using Strunchik.ViewModel.ApplicationContext;
using Strunchik.ViewModel.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Strunchic.AdminMode.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly UsersManager _userManager = null!;
    private readonly ApplicationContext _context = null!;

    public event PropertyChangedEventHandler? PropertyChanged;

    public UserModel SelectedUser
    {
        get => _userManager.SelectedUser;
        set
        {
            _userManager.SelectedUser = value;
            OnPropertyChanched();
        }
    }
    public ObservableCollection<UserModel> Users
    {
        get => _userManager.Data;
        set
        {
            _userManager.Data = value;
            OnPropertyChanched();
        }
    }

    public ICommand AddUserCommand { get; }
    public ICommand DeleteUserCommand { get; }

    public MainWindowViewModel()
    {
        _context = new ApplicationContext();

        _userManager = new UsersManager(_context);

        AddUserCommand = new RelayCommand(_ => { _userManager.Add(); OnPropertyChanched(nameof(Users)); });
        DeleteUserCommand = new RelayCommand(_ => _userManager.Delete());
    }

    private void OnPropertyChanched([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
