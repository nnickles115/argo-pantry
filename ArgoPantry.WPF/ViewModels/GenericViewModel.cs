namespace ArgoPantry.WPF.ViewModels;

public abstract partial class GenericViewModel<T> : ObservableRecipient where T : DomainObject {
    #region SERVICES
    protected readonly IServiceProvider serviceProvider;
    protected readonly IDataService<T> dataService;
    protected readonly IContentDialogService contentDialogService;
    #endregion SERVICES

    #region PROPERTIES
    public ObservableCollection<T> TableData { get; } = new();

    [ObservableProperty]
    protected ICollectionView itemsSource = null!;

    [ObservableProperty]
    protected string filterText = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(UpdateItemCommand))]
    [NotifyCanExecuteChangedFor(nameof(DeleteItemCommand))]
    protected T selectedItem = null!;
    #endregion PROPERTIES

    protected bool isInitialized = false;

    #region CONSTRUCTOR
    protected GenericViewModel(
        IServiceProvider serviceProvider, 
        IDataService<T> dataService) {
        this.serviceProvider = serviceProvider;
        this.dataService = dataService;

        contentDialogService = serviceProvider.GetRequiredService<IContentDialogService>();

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
    protected virtual async Task UpdateDataGridAsync() {
        var allItems = await dataService.GetAll();

        TableData.Clear(); // Prevent stale data from persisting in the ui
        foreach(T item in allItems!) {
            TableData.Add(item);
        }

        ItemsSource.Refresh();
    }

    partial void OnFilterTextChanged(string value) => ItemsSource.Refresh();

    private bool FilterRows(object entity) {
        if(entity is T item) {
            return FilterItem(item);
        }
        return false;
    }

    protected abstract bool FilterItem(T item);
    #endregion ACTIONS

    #region COMMANDS
    protected abstract Task<T?> ShowDialog(T? item);

    [RelayCommand]
    public async Task AddItem() {
        var newItem = await ShowDialog(null);
        if(newItem != null) {
            await dataService.Create(newItem);
            await UpdateDataGridAsync();
        }
    }

    private bool CanExecuteUpdateOrDelete() => SelectedItem is not null;

    [RelayCommand(CanExecute = nameof(CanExecuteUpdateOrDelete))]
    protected async Task UpdateItem() {
        if(SelectedItem == null) return;

        var updatedItem = await ShowDialog(SelectedItem);
        if(updatedItem != null) {
            await dataService.Update(SelectedItem.Id, SelectedItem);
            await UpdateDataGridAsync();
        }
    }

    [RelayCommand(CanExecute = nameof(CanExecuteUpdateOrDelete))]
    protected async Task DeleteItem() {
        ContentDialogResult result = await contentDialogService
            .ShowSimpleDialogAsync(new() {
                    Title = "Delete Record",
                    Content = "Are you sure you want to delete this record?\n\n" +
                              "*Deleting items from the Database is undoable and\n" +
                              " the ID number will never be able to be used again.",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel"
                }
            );

        if(result == ContentDialogResult.Primary && SelectedItem != null) {
            await dataService.Delete(SelectedItem.Id);
            await UpdateDataGridAsync();
        }
    }
    #endregion COMMANDS
}