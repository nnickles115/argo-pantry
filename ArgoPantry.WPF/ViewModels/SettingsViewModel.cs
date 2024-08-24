using ArgoPantry.WPF.Controls;

namespace ArgoPantry.WPF.ViewModels; 

public sealed partial class SettingsViewModel : ObservableObject, INavigationAware {
    private readonly IThemeService _themeService;
    private readonly INavigationService _navigationService;

    #region PROPERTIES
    [ObservableProperty]
    private string _appTitle = string.Empty;

    [ObservableProperty]
    private string _appVersion = string.Empty;

    [ObservableProperty]
    private ApplicationTheme _currentTheme = ApplicationTheme.Unknown;

    [ObservableProperty]
    private NavigationViewPaneDisplayMode _currentNavigationStyle = NavigationViewPaneDisplayMode.Left;

    [ObservableProperty]
    private bool _isDeleteEnabled = false;
    #endregion PROPERTIES

    #region FLAGS
    private bool _isInitialized = false;
    #endregion FLAGS

    public SettingsViewModel(
        IThemeService themeService, 
        INavigationService navigationService) {
        _themeService = themeService;
        _navigationService = navigationService;
    }

    #region INavigationAware FUNCTIONS
    public void OnNavigatedTo() {
        if(!_isInitialized) {
            InitializeViewModel();
        }
    }

    public void OnNavigatedFrom() { }
    #endregion INavigationAware FUNCTIONS

    private void InitializeViewModel() {
        // Update the applied theme setting
        CurrentTheme = _themeService.GetTheme();

        // Set the application title and version
        AppTitle = GetAppTitle();
        AppVersion = $"{GetAppTitle()} - {GetAssemblyVersion()}";

        // Finish initializing the view model
        _isInitialized = true;
    }

    private string GetAppTitle() => System.Reflection.Assembly
        .GetExecutingAssembly()
        .GetName().Name!.Split('.')[0];

    private string GetAssemblyVersion() => System.Reflection.Assembly
        .GetExecutingAssembly()
        .GetName().Version?.ToString() ?? string.Empty;


    // FIXME: Throwing an exception here
    partial void OnCurrentNavigationStyleChanged(
        NavigationViewPaneDisplayMode oldValue,
        NavigationViewPaneDisplayMode newValue) {
        //_navigationService.SetPaneDisplayMode(newValue);
    }

    partial void OnCurrentThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue) {
        ApplicationThemeManager.Apply(newValue);

        Settings.Default.Theme = newValue.ToString();
        Settings.Default.Save();
    }
}