namespace ArgoPantry.Views;

/// <summary>
/// Represents a ContentDialog for adding or editing a student.
/// </summary>
public sealed partial class StudentDialog : ContentDialog, IDialogForm {
    #region VARIABLES
    /// <summary>
    /// Gets the ViewModel for the dialog.
    /// </summary>
    public StudentForm ViewModel { get; }
    #endregion VARIABLES

    #region CONSTRUCTORS
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentDialog"/> class for adding a new student.
    /// </summary>
    /// <param name="contentPresenter">The content presenter for the dialog.</param>
    /// <param name="viewModel">The ViewModel for the dialog.</param>
    public StudentDialog(
        ContentPresenter contentPresenter,
        StudentForm viewModel
    ) : base(contentPresenter) {
        DataContext = ViewModel = viewModel;
        Title = "Add Student";
        PrimaryButtonText = "Submit";
        
        InitializeComponent();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StudentDialog"/> class for editing an existing student.
    /// </summary>
    /// <param name="contentPresenter">The content presenter for the dialog.</param>
    /// <param name="viewModel">The ViewModel for the dialog.</param>
    /// <param name="student">The student to be edited.</param>
    public StudentDialog(
        ContentPresenter contentPresenter,
        StudentForm viewModel,
        Student student
    ) : base(contentPresenter) {
        DataContext = ViewModel = viewModel;
        ViewModel.SetEntityInfo(student);
        Title = "Edit Student";
        PrimaryButtonText = "Update";

        InitializeComponent();
    }
    #endregion CONSTRUCTORS

    private void Page_Loaded(object sender, RoutedEventArgs e) {
        ViewModel.Initialize();
    }

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