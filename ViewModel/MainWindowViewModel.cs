using Strunchic.AdminMode.Services.UsersManager;
using Strunchik.ViewModel.ApplicationContext;

namespace Strunchic.AdminMode.ViewModel;

public class MainWindowViewModel
{
    private UsersManager _userManager = null!;
    private ApplicationContext _context = null!;

    public MainWindowViewModel()
    {
        _context = new ApplicationContext();

        _userManager = new UsersManager();
    }
}
