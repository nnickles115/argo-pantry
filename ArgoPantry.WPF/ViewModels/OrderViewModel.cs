namespace ArgoPantry.WPF.ViewModels;

public sealed partial class OrderViewModel : ObservableObject, INavigationAware {
    #region SERVICES
    private readonly IServiceProvider _serviceProvider;
    private readonly IContentDialogService _contentDialogService;
    private readonly ArgoPantryDbContext _context;
    private readonly IDataService<Student> _studentDAO;
    private readonly IDataService<Item> _itemDAO;
    private readonly OrderDataService _orderDAO;
    #endregion SERVICES

    #region VARIABLES
    private bool _isInitialized = false;
    #endregion VARIABLES

    #region Student PROPERTIES
    public ObservableCollection<string> AutoSuggestBoxSuggestions { get; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    [NotifyCanExecuteChangedFor(nameof(ConfirmOrderCommand))]
    private Student _selectedStudent = null!;

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _fullName = string.Empty;
    #endregion Student PROPERTIES

    #region Item PROPERTIES
    public ObservableCollection<Item> ScannedItems { get; } = new();
    public ObservableCollection<Item> SelectedItemsForRemoval { get; } = new();

    [ObservableProperty]
    private bool _isEditModeEnabled = false;

    [ObservableProperty]
    private string _editCartButtonText = "Edit Cart";

    [ObservableProperty]
    private string _scannedBarcode = string.Empty;
    #endregion Student PROPERTIES

    #region ListView PROPERTIES
    [ObservableProperty]
    private double _listViewHeight = 0.0;
    #endregion ListView PROPERTIES

    #region CONSTRUCTOR
    public OrderViewModel(
        IServiceProvider serviceProvider,
        IContentDialogService contentDialogService,
        ArgoPantryDbContext context,
        IDataService<Student> studentDAO,
        IDataService<Item> itemDAO,
        OrderDataService orderDAO) { 
        _serviceProvider = serviceProvider;
        _contentDialogService = contentDialogService;
        _context = context;
        _studentDAO = studentDAO;
        _itemDAO = itemDAO;
        _orderDAO = orderDAO;

        SubscribeToEvents();
        PopulateSuggestionBox().ConfigureAwait(false);
    }

    private void SubscribeToEvents() {
        ScannedItems.CollectionChanged += (s, e)
            => ToggleEditModeCommand.NotifyCanExecuteChanged();

        ScannedItems.CollectionChanged += (s, e)
            => ClearCartCommand.NotifyCanExecuteChanged();

        ScannedItems.CollectionChanged += (s, e)
            => ConfirmOrderCommand.NotifyCanExecuteChanged();

        SelectedItemsForRemoval.CollectionChanged += (s, e)
            => RemoveItemsFromCartCommand.NotifyCanExecuteChanged();
    }
    #endregion CONSTRUCTOR

    #region INavigationAware FUNCTIONS
    public async void OnNavigatedTo() {
        if(!_isInitialized) {
            await PopulateSuggestionBox();
            _isInitialized = true;
        }
    }

    public void OnNavigatedFrom() { }
    #endregion INavigationAware FUNCTIONS

    #region FUNCTIONS
    private async Task PopulateSuggestionBox() {
        AutoSuggestBoxSuggestions.Clear();

        var students = await _studentDAO.GetAll();
        if(students == null) return;

        foreach(Student student in students) {
            AutoSuggestBoxSuggestions.Add(student.StudentId);
        }
    }

    private void ClearSelections() {
        // Clear item collections
        ScannedItems.Clear();
        SelectedItemsForRemoval.Clear();

        // Reset the student selection
        SelectedStudent = null!;
        SearchText = string.Empty;
    }
    #endregion FUNCTIONS

    #region EVENT HANDLERS
    partial void OnSearchTextChanged(string value) {
        if(string.IsNullOrEmpty(value)) {
            SelectedStudent = null!;
            FullName = string.Empty;
            PopulateSuggestionBox().ConfigureAwait(false);
        }
    }

    partial void OnScannedBarcodeChanged(string value) {
        if(!string.IsNullOrEmpty(value)) {
            PerformBarcodeLookup(value);
            ScannedBarcode = string.Empty;
        }
    }

    private async void PerformBarcodeLookup(string barcode) {
        var item = await _itemDAO.GetByColumn(barcode, "Barcode");
        if(item == null) return;

        int quantityInCart = ScannedItems.Count(i => i.Id == item.Id);

        if(quantityInCart + 1 > item.Quantity) {
            await _contentDialogService.ShowSimpleDialogAsync(new() {
                Title = "Out of Stock",
                Content = $"Cannot add more of {item.ItemName} (Barcode: {item.Barcode}). {item.Quantity} left in stock.",
                CloseButtonText = "OK"
            });
            return;
        }
        ScannedItems.Add(item);
    }
    #endregion EVENT HANDLERS

    #region Student COMMANDS
    [RelayCommand]
    private async Task AddNewStudent() {
        var studentsViewModel = _serviceProvider.GetRequiredService<StudentsViewModel>();
        await studentsViewModel.AddItem();
        await PopulateSuggestionBox();
    }

    [RelayCommand]
    private async Task SuggestionChosen(string selectedStudentId) {
        var student = await _studentDAO.GetByColumn(selectedStudentId, "StudentId");
        if(student == null) return;

        SelectedStudent = student;
        FullName = $"{student.FirstName} {student.LastName}";
    }
    #endregion Student COMMANDS

    #region Item COMMANDS
    // TODO: Implement this -> used for background scanning of barcodes
    /*[RelayCommand]
    private void AddScannedItem() {
        //ScannedItems.Add();
    }*/
    #endregion Item COMMANDS

    #region ListView COMMANDS
    private bool CanEditOrClearCart() => ScannedItems.Count > 0;
    private bool CanRemoveItemsFromCart() => IsEditModeEnabled && SelectedItemsForRemoval.Any();

    [RelayCommand(CanExecute = nameof(CanEditOrClearCart))]
    private void ToggleEditMode() {
        IsEditModeEnabled = !IsEditModeEnabled;
        EditCartButtonText = IsEditModeEnabled ? "Save Cart" : "Edit Cart";

        if(!IsEditModeEnabled) RemoveItemsFromCart();
    }

    [RelayCommand] // Called by event handler in code-behind to avoid violating MVVM
    private void SelectionChanged(object parameter) {
        if(parameter is (
            IList<Item> addedItems,
            IList<Item> removedItems)) {

            foreach(Item item in addedItems) {
                SelectedItemsForRemoval.Add(item);
            }

            foreach(Item item in removedItems) {
                SelectedItemsForRemoval.Remove(item);
            }
        }
    }

    [RelayCommand(CanExecute = nameof(CanRemoveItemsFromCart))]
    private void RemoveItemsFromCart() {
        foreach(Item item in SelectedItemsForRemoval.ToList()) {
            ScannedItems.Remove(item);
        }
        SelectedItemsForRemoval.Clear();
    }

    [RelayCommand(CanExecute = nameof(CanEditOrClearCart))]
    private async Task ClearCart() {
        ContentDialogResult result = await _contentDialogService
            .ShowSimpleDialogAsync(new() {
                Title = "Clear Cart",
                Content = "Are you sure you want to clear the cart?",
                PrimaryButtonText = "Clear",
                CloseButtonText = "Cancel"
            }
        );

        if(result == ContentDialogResult.Primary) {
            ScannedItems.Clear();
            SelectedItemsForRemoval.Clear();
        }
    }
    #endregion ListView COMMANDS

    #region Order COMMANDS
    private bool CanPlaceOrder() => SelectedStudent != null && ScannedItems.Count > 0;
    
    [RelayCommand(CanExecute = nameof(CanPlaceOrder))]
    private async Task ConfirmOrder() {
        ContentDialogResult result = await _contentDialogService
            .ShowSimpleDialogAsync(new() {
                Title = "Confirm Order",
                Content = "Are you sure you want to place this order?",
                PrimaryButtonText = "Confirm",
                CloseButtonText = "Cancel"
            }
        );

        if(result == ContentDialogResult.Primary) {
            await PlaceOrder();
        }
    }
    private async Task PlaceOrder() {
        // Format the current date and time
        string orderDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt"); // Sortable date format

        // Create a new order, add the date and selected student
        Order newOrder = new() {
            OrderDate = orderDate,
            Student = SelectedStudent
        };

        // Inform the DB of the selected student existing and not needing to be added
        await _studentDAO.Update(SelectedStudent.Id, SelectedStudent);

        // Group ScannedItems by ItemId to aggregate quantities
        var groupedItems = ScannedItems.GroupBy(item => item.Id)
            .Select(group => new {
                Item = group.First(),
                Quantity = group.Count()
            }).ToList();

        // Add each grouped item to the OrderItems collection
        foreach(var group in groupedItems) {
            // Subtract the quantity taken from the available stock
            group.Item.Quantity -= group.Quantity;

            // Add each item to the OrderItems collection
            newOrder.OrderItems.Add(new OrderItem {
                QuantityTaken = group.Quantity,
                Item = group.Item
            });
        }

        // Create the order and update the stock
        await _orderDAO.Create(newOrder, groupedItems.Select(g => g.Item));

        // Reset selected student and item list
        ClearSelections();

        // Update the data grid in TransactionsPage
        await _serviceProvider.GetRequiredService<TransactionsViewModel>().UpdateDataGridAsync();
    }
    #endregion Order COMMANDS
}