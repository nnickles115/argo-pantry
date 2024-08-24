namespace ArgoPantry.WPF.ViewModels;
public sealed partial class MainWindowViewModel : ObservableObject {
    [ObservableProperty]
    private string _applicationTitle = "ArgoPantry Database";

    public ObservableCollection<object> MenuItems { get; } = new();
    public ObservableCollection<object> FooterMenuItems { get; } = new();

    public MainWindowViewModel() {
        InitializeMenuItems();
        InitializeFooterMenuItems();
    }

    private void InitializeMenuItems() {
        MenuItems.Add(new NavigationViewItem() {
            Content = "Dashboard",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Board24 },
            TargetPageType = typeof(DashboardPage)
        });
        MenuItems.Add(new NavigationViewItem() {
            Content = "New Order",
            Icon = new SymbolIcon { Symbol = SymbolRegular.ShoppingBag24 },
            TargetPageType = typeof(OrdersPage)
        });
        MenuItems.Add(new NavigationViewItem() {
            Content = "Manage Students",
            Icon = new SymbolIcon { Symbol = SymbolRegular.People24 },
            TargetPageType = typeof(StudentsPage)
        });
        MenuItems.Add(new NavigationViewItem() {
            Content = "Manage Inventory",
            Icon = new SymbolIcon { Symbol = SymbolRegular.FoodApple24 },
            TargetPageType = typeof(ItemsPage)
        });
        MenuItems.Add(new NavigationViewItem() {
            Content = "Transaction History",
            Icon = new SymbolIcon { Symbol = SymbolRegular.TextBulletListSquare24 },
            TargetPageType = typeof(TransactionsPage)
        });
    }

    private void InitializeFooterMenuItems() {
        /*FooterMenuItems.Add(new NavigationViewItem() {
            Content = "Import Data",
            Icon = new SymbolIcon { Symbol = SymbolRegular.ArrowUpload24 },
            TargetPageType = typeof(ImportPage)
        });*/
        FooterMenuItems.Add(new NavigationViewItem() {
            Content = "Settings",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            TargetPageType = typeof(SettingsPage)
        });
    }

    /*[ObservableProperty]
    private ObservableCollection<Wpf.Ui.Controls.MenuItem> _trayMenuItems = [
        new Wpf.Ui.Controls.MenuItem { Header = "Home", Tag = "tray_home" }
    ];*/
}