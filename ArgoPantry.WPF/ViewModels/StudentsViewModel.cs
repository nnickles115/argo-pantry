namespace ArgoPantry.WPF.ViewModels;

/// <summary>
/// ViewModel for managing student data from a database displayed in a DataGrid.
/// Provides functionality to load, filter, add, update, and delete student records.
/// </summary>
public sealed partial class StudentsViewModel : GenericViewModel<Student> {
    #region PROPERTIES
    [ObservableProperty]
    private bool _isDeleteEnabled = false;
    #endregion PROPERTIES

    #region CONSTRUCTOR
    /// <summary>
    /// Initializes a new instance of the <see cref="StudentsViewModel"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="studentDataService">The student data service.</param>
    public StudentsViewModel(
        IServiceProvider serviceProvider, 
        IDataService<Student> studentDataService
    ) : base(serviceProvider, studentDataService) {

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
    protected override bool FilterItem(Student student) {
        if(string.IsNullOrEmpty(FilterText)) return true;

        return student.FirstName!.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                student.LastName!.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                student.StudentId!.Contains(FilterText, StringComparison.OrdinalIgnoreCase);
    }
    #endregion ACTIONS

    #region COMMANDS
    protected override async Task<Student?> ShowDialog(Student? student) {
        var studentForm = serviceProvider.GetRequiredService<StudentForm>();
        var dialogHost = contentDialogService.GetDialogHost()!;

        StudentDialog dialog = student == null
            ? new(dialogHost, studentForm)
            : new(dialogHost, studentForm, student);
        
        var result = await dialog.ShowAsync();

        switch(result) {
            case ContentDialogResult.Primary:
                student ??= new();
                student.FirstName = studentForm.FirstName!;
                student.LastName = studentForm.LastName!;
                student.StudentId = studentForm.StudentId!;
                return student;
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