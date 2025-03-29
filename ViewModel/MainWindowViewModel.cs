using MimeKit.Cryptography;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Strunchic.AdminMode.Services.InsrtumentTypesManager;
using Strunchic.AdminMode.Services.ItemsManager;
using Strunchic.AdminMode.Services.OrderChartService;
using Strunchic.AdminMode.Services.OrderManager;
using Strunchic.AdminMode.Services.UserChartService;
using Strunchic.AdminMode.Services.UsersManager;
using Strunchik.Model.Item;
using Strunchik.Model.Order;
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
    private readonly ItemsManager _itemManager = null!;
    private readonly InsrtumentTypesManager _typesManager = null!;
    private readonly OrderManager _orderManager = null!;
    private readonly ApplicationContext _context = null!;

    private readonly UserChartService _userChartService = null!;
    private readonly OrderChartService _orderChartService = null!;

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

    public ItemModel SelectedItem
    {
        get => _itemManager.SelectedItem;
        set
        {
            _itemManager.SelectedItem = value;
            OnPropertyChanched();
        }
    }
    public ObservableCollection<ItemModel> Items
    {
        get => _itemManager.Data;
        set
        {
            _itemManager.Data = value;
            OnPropertyChanched();
        }
    }

    public ItemsType SelectedType
    {
        get => _typesManager.SelectedType;
        set
        {
            _typesManager.SelectedType = value;
            OnPropertyChanched();
        }
    }
    public ObservableCollection<ItemsType> Types
    {
        get => _typesManager.Data;
        set
        {
            _typesManager.Data = value;
            OnPropertyChanched();
        }
    }

    public OrderModel SelectedOrder
    {
        get => _orderManager.SelectedOrder;
        set
        {
            _orderManager.SelectedOrder = value;
            OnPropertyChanched();
        }
    }
    public ObservableCollection<OrderModel> Orders
    {
        get => _orderManager.Data;
        set
        {
            _orderManager.Data = value;
            OnPropertyChanched();
        }
    }

    public ICommand AddUserCommand { get; }
    public ICommand DeleteUserCommand { get; }

    public ICommand AddItemCommand { get; }
    public ICommand DeleteItemCommand { get; }

    public ICommand AddTypeCommand { get; }
    public ICommand DeleteTypeCommand { get; }

    public ICommand AddOrderCommand { get; }
    public ICommand DeleteOrderCommand { get; }

    public ICommand SaveCommand { get; }

    public UserChartService UserChartService
    {
        get => _userChartService;
    }
    public OrderChartService OrderChartService
    {
        get => _orderChartService;
    }

    public MainWindowViewModel()
    {
        _context = new ApplicationContext();

        _userManager = new UsersManager(_context);
        _itemManager = new ItemsManager(_context);
        _typesManager = new InsrtumentTypesManager(_context);
        _orderManager = new OrderManager(_context);

        _userChartService = new UserChartService(_context);
        _orderChartService = new OrderChartService(_context);

        AddUserCommand = new RelayCommand(_ => { _userManager.Add(); OnPropertyChanched(nameof(Users)); });
        DeleteUserCommand = new RelayCommand(_ => _userManager.Delete());

        AddItemCommand = new RelayCommand(_ => { _itemManager.Add(); OnPropertyChanched(nameof(Items)); });
        DeleteItemCommand = new RelayCommand(_ => _itemManager.Delete());

        AddTypeCommand = new RelayCommand(_ => { _typesManager.Add(); OnPropertyChanched(nameof(Types)); });
        DeleteTypeCommand = new RelayCommand(_ => _typesManager.Delete());

        AddOrderCommand = new RelayCommand(_ => { _orderManager.Add(); OnPropertyChanched(nameof(Orders)); });
        DeleteOrderCommand = new RelayCommand(_ => _orderManager.Delete());

        SaveCommand = new RelayCommand(_ => _context.SaveChanges());
    }

    private void OnPropertyChanched([CallerMemberName] string property = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
