using Strunchik.Model.User;
using Strunchik.ViewModel.ApplicationContext;
using System.Collections.ObjectModel;

namespace Strunchic.AdminMode.Services.BaseManager;

public abstract class BaseManager(ApplicationContext context)
{
    protected ApplicationContext _context = context;

    public abstract void Add();
    public abstract void Delete();
}
