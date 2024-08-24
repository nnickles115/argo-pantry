namespace ArgoPantry.WPF.ViewModels;

/// <summary>
/// ViewModel for managing item data from a database displayed in a DataGrid.
/// Provides functionality to load, filter, add, update, and delete item records.
/// </summary>
public sealed partial class ItemsViewModel : GenericViewModel<Item> {
    #region PROPERTIES
    [ObservableProperty]
    private bool _isDeleteEnabled = false;
    #endregion PROPERTIES

    #region CONSTRUCTOR
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemsViewModel"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="itemDataService">The item data service.</param>
    public ItemsViewModel(
        IServiceProvider serviceProvider,
        IDataService<Item> itemDataService
    ) : base(serviceProvider, itemDataService) {

        // Check the global override of the delete button
        var settingsViewModel = serviceProvider.GetRequiredService<SettingsViewModel>();
        _isDeleteEnabled = settingsViewModel.IsDeleteEnabled;

        settingsViewModel.PropertyChanged += (sender, e) => {
            if(e.PropertyName == nameof(SettingsViewModel.IsDeleteEnabled)) {
                IsDeleteEnabled = settingsViewModel.IsDeleteEnabled;
            }
        };
    }
    #endregion CONSTRUCTOR

    #region ACTIONS
    protected override bool FilterItem(Item item) {
        if(string.IsNullOrEmpty(FilterText)) return true;

        return item.ItemName!.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                item.Barcode!.Contains(FilterText, StringComparison.OrdinalIgnoreCase);
    }
    #endregion ACTIONS

    #region COMMANDS
    protected override async Task<Item?> ShowDialog(Item? item) {
        var itemForm = serviceProvider.GetRequiredService<ItemForm>();
        var dialogHost = contentDialogService.GetDialogHost()!;

        ItemDialog dialog = item == null
            ? new(dialogHost, itemForm)
            : new(dialogHost, itemForm, item);

        var result = await dialog.ShowAsync();

        switch(result) {
            case ContentDialogResult.Primary:
                item ??= new();
                item.ItemName = itemForm.ItemName!;
                item.Barcode = itemForm.Barcode!;
                item.Quantity = (int)itemForm.Quantity!;
                return item;
            case ContentDialogResult.Secondary:
                break;
            case ContentDialogResult.None:
                break;
            default:
                break;
        }
        return null;
    }
    #endregion COMMANDS
}