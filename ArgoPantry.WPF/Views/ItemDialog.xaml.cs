namespace ArgoPantry.Views;

/// <summary>
/// Represents a ContentDialog for adding or editing a item.
/// </summary>
public sealed partial class ItemDialog : ContentDialog, IDialogForm {
    #region VARIABLES
    /// <summary>
    /// Gets the ViewModel for the dialog.
    /// </summary>
    public ItemForm ViewModel { get; }
    #endregion VARIABLES

    #region CONSTRUCTORS
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemDialog"/> class for adding a new item.
    /// </summary>
    /// <param name="contentPresenter">The content presenter for the dialog.</param>
    /// <param name="viewModel">The ViewModel for the dialog.</param>
    public ItemDialog(
        ContentPresenter contentPresenter, 
        ItemForm viewModel
    ) : base(contentPresenter) {
        DataContext = ViewModel = viewModel;
        InitializeComponent();

        Title = "Add Item";
        PrimaryButtonText = "Submit";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ItemDialog"/> class for editing an existing item.
    /// </summary>
    /// <param name="contentPresenter">The content presenter for the dialog.</param>
    /// <param name="viewModel">The ViewModel for the dialog.</param>
    /// <param name="item">The item to be edited.</param>
    public ItemDialog(
        ContentPresenter contentPresenter,
        ItemForm viewModel,
        Item item
    ) : base(contentPresenter) {
        DataContext = ViewModel = viewModel;
        InitializeComponent();

        ViewModel.SetEntityInfo(item);

        Title = "Edit Item";
        PrimaryButtonText = "Update";
    }
    #endregion CONSTRUCTORS

    #region IDialogForm EVENT HANDLERS
    public void InputBox_PreviewTextInput(object sender, TextCompositionEventArgs e) {
        DialogHelper.OnPreviewTextInput(sender, e);
    }
    public void InputBox_Pasting(object sender, DataObjectPastingEventArgs e) {
        DialogHelper.OnPasting(sender, e);
    }
    public void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args) {
        DialogHelper.ContentDialog_Closing(sender, args, ViewModel);
    }
    #endregion IDialogForm EVENT HANDLERS
}