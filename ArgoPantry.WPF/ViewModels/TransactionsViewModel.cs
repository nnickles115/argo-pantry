using ArgoPantry.WPF.Models;

namespace ArgoPantry.WPF.ViewModels;

public sealed partial class TransactionsViewModel : ObservableRecipient {
    #region SERVICES
    private readonly IServiceProvider _serviceProvider;
    private readonly OrderDataService _orderDAO;
    private readonly IContentDialogService _contentDialogService;
    #endregion SERVICES

    #region PROPERTIES
    public ObservableCollection<TransactionEntity> TableData { get; } = new();

    [ObservableProperty]
    private ICollectionView itemsSource = null!;

    [ObservableProperty]
    private string filterText = string.Empty;

    [ObservableProperty]
    /*[NotifyCanExecuteChangedFor(nameof(UpdateItemCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteItemCommand))]*/
    private TransactionEntity selectedItem = null!;
    #endregion PROPERTIES

    private bool isInitialized = false;

    #region CONSTRUCTOR
    public TransactionsViewModel(
        IServiceProvider serviceProvider,
        OrderDataService orderDAO) {
        _serviceProvider = serviceProvider;
        _orderDAO = orderDAO;

        _contentDialogService = serviceProvider.GetRequiredService<IContentDialogService>();

        // Set up the ItemsSource for the DataGrid
        ItemsSource = CollectionViewSource.GetDefaultView(TableData);
        ItemsSource.Filter = FilterRows;
    }

    public async Task InitializeAsync() {
        if(!isInitialized) {
            await UpdateDataGridAsync();
            isInitialized = true;
        }
    }
    #endregion CONSTRUCTOR

    #region ACTIONS
    public async Task UpdateDataGridAsync() {
        var allOrders = await _orderDAO.GetAllOrdersWithItems();

        TableData.Clear(); // Prevent stale data from persisting in the ui
        foreach(Order order in allOrders!) {
            foreach(OrderItem orderItem in order.OrderItems) {
                TableData.Add(new TransactionEntity {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    FullName = $"{order.Student.FirstName} {order.Student.LastName}",
                    StudentId = order.Student.StudentId,
                    ItemName = orderItem.Item.ItemName,
                    Barcode = orderItem.Item.Barcode,
                    QuantityTaken = orderItem.QuantityTaken
                });
            }
        }
        ItemsSource.Refresh();
    }

    partial void OnFilterTextChanged(string value) => ItemsSource.Refresh();

    private bool FilterRows(object entity) {
        if(entity is TransactionEntity transaction) {
            return FilterItem(transaction);
        }
        return false;
    }

    private bool FilterItem(TransactionEntity transaction) {
        if(string.IsNullOrEmpty(FilterText)) return true;

        return transaction.Id.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
               transaction.FullName.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
               transaction.StudentId.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
               transaction.ItemName.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
               transaction.Barcode.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
               transaction.QuantityTaken.ToString().Contains(FilterText, StringComparison.OrdinalIgnoreCase);
    }
    #endregion ACTIONS

    #region COMMANDS
    //private abstract Task<T?> ShowDialog(T? item);

    /*[RelayCommand]
    public async Task AddItem() {
        var newItem = await ShowDialog(null);
        if(newItem != null) {
            await dataService.Create(newItem);
            await UpdateDataGridAsync();
        }
    }*/

    private bool CanExecuteUpdateOrDelete() => SelectedItem is not null;

    /*[RelayCommand(CanExecute = nameof(CanExecuteUpdateOrDelete))]
    private async Task UpdateItem() {
        if(SelectedItem == null) return;

        var updatedItem = await ShowDialog(SelectedItem);
        if(updatedItem != null) {
            await dataService.Update(SelectedItem.Id, SelectedItem);
            await UpdateDataGridAsync();
        }
    }*/

    /*[RelayCommand(CanExecute = nameof(CanExecuteUpdateOrDelete))]
    private async Task DeleteItem() {
        ContentDialogResult result = await contentDialogService
            .ShowSimpleDialogAsync(new() {
                Title = "Delete Record",
                Content = "Are you sure you want to delete this record?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            }
            );

        if(result == ContentDialogResult.Primary && SelectedItem != null) {
            await dataService.Delete(SelectedItem.Id);
            await UpdateDataGridAsync();
        }
    }*/
    #endregion COMMANDS
}
