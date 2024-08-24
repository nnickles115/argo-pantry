namespace ArgoPantry.WPF.ViewModels;

/// <summary>
/// Represents a form for student data with validation.
/// </summary>
public sealed partial class StudentForm : ObservableValidator, IFormValidator {
    #region PROPERTIES
    /// <summary>
    /// Gets or sets the first name of the student.
    /// </summary>
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "First Name is required.")]
    [MinLength(1)]
    private string? _firstName;

    /// <summary>
    /// Gets or sets the last name of the student.
    /// </summary>
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Last Name is required.")]
    [MinLength(1)]
    private string? _lastName;

    /// <summary>
    /// Gets or sets the student ID.
    /// </summary>
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessage = "Student ID is required.")]
    [CustomValidation(typeof(StudentForm), nameof(ValidateStudentId))]
    [MinLength(1)]
    private string? _studentId;
    #endregion PROPERTIES

    #region SERVICES
    private readonly IDataService<Student> _studentDataService;
    #endregion SERVICES
    
    #region FLAGS
    private bool _isInitialized = false;

    [ObservableProperty]
    private bool _isUpdateMode = false;
    #endregion FLAGS

    #region CONSTRUCTOR
    public StudentForm(IDataService<Student> studentDataService) {
        _studentDataService = studentDataService;
    }

    public void Initialize() {
        if(!_isInitialized) {
            _isInitialized = true;
        }
    }
    #endregion CONSTRUCTOR

    #region ANNOTATION
    public static System.ComponentModel.DataAnnotations.ValidationResult ValidateStudentId(
        string studentId, ValidationContext context) {
        if(context.ObjectInstance is StudentForm instance) {
            var student = instance._studentDataService.GetByColumn(studentId, "StudentId").Result;
            if(student != null && !instance.IsUpdateMode) {
                return new System.ComponentModel.DataAnnotations.ValidationResult("Student ID already exists.");
            }
        }
        return System.ComponentModel.DataAnnotations.ValidationResult.Success!;
    }
    #endregion ANNOTATION

    #region FUNCTIONS
    /// <summary>
    /// Initializes the form with the given student data.
    /// </summary>
    /// <param name="student">The student data to initialize the form with.</param>
    public void SetEntityInfo(object entity) {
        IsUpdateMode = true;

        Student student = (Student)entity;

        FirstName = student.FirstName;
        LastName = student.LastName;
        StudentId = student.StudentId;
    }

    public bool ContainsErrors() {
        ValidateAllProperties();
        return HasErrors;
    }
    #endregion FUNCTIONS
}