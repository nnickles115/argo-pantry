namespace ArgoPantry.WPF.Views;


public sealed partial class SettingsPage : INavigableView<SettingsViewModel> {
    #region VARIABLES
    public SettingsViewModel ViewModel { get; }
    #endregion VARIABLES

    #region CONSTRUCTOR
    public SettingsPage(SettingsViewModel viewModel) {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
    #endregion CONSTRUCTOR
}