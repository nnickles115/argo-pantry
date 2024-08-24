namespace ArgoPantry.WPF.ViewModels;

/// <summary>
/// Represents a form for item data with validation.
/// </summary>
public sealed partial class ItemForm : ObservableValidator, IFormValidator {
    #region PROPERTIES
    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Item Name is required.")]
    [MinLength(1)]
    private string? _itemName;

    /// <summary>
    /// Gets or sets the barcode of the item.
    /// </summary>
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Barcode is required.")]
    [MinLength(1)]
    private string? _barcode;

    /// <summary>
    /// Gets or sets the quantity of the item.
    /// </summary>
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Quantity is required.")]
    [Range(1, int.MaxValue)]
    private int? _quantity = 1; // Default to 1
    #endregion PROPERTIES

    #region FUNCTIONS
    public void SetEntityInfo(object entity) {
        Item item = (Item)entity;

        ItemName = item.ItemName;
        Barcode = item.Barcode;
        Quantity = (int)item.Quantity!;
    }

    public bool ContainsErrors() {
        ValidateAllProperties();

        return HasErrors;
    }

    #endregion FUNCTIONS
}