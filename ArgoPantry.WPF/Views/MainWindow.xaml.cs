namespace ArgoPantry.WPF.Views; 
public partial class MainWindow : FluentWindow, INavigationWindow {
    private IServiceProvider _serviceProvider;

    public MainWindowViewModel ViewModel { get; }

    public MainWindow(
        MainWindowViewModel viewModel,
        IServiceProvider serviceProvider,
        IPageService pageService,
        INavigationService navigationService,
        IContentDialogService contentDialogService
    ) {
        DataContext = ViewModel = viewModel;
        InitializeComponent();

        SystemThemeWatcher.Watch(this);
        //SystemThemeWatcher.Watch(this, WindowBackdropType.Mica, true);

        SetPageService(pageService);
        SetServiceProvider(serviceProvider);

        navigationService.SetNavigationControl(RootNavigation);
        navigationService.SetPaneDisplayMode(NavigationViewPaneDisplayMode.Left);
        contentDialogService.SetDialogHost(RootContentDialog);
    }

    #region INavigationWindow methods

    public INavigationView GetNavigation() => RootNavigation;

    public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

    public void SetPageService(IPageService pageService) => RootNavigation.SetPageService(pageService);

    public void ShowWindow() => Show();

    public void CloseWindow() => Close();

    #endregion INavigationWindow methods

    protected override void OnClosed(EventArgs e) {
        base.OnClosed(e);

        Application.Current.Shutdown();
    }

    INavigationView INavigationWindow.GetNavigation() => GetNavigation();

    public void SetServiceProvider(IServiceProvider serviceProvider) {
        _serviceProvider = serviceProvider;
    }

    // Collapse the header when the dashboard is selected
    private void RootNavigation_SelectionChanged(NavigationView sender, RoutedEventArgs args) {
        if(sender is not NavigationView navigationView) return;

        RootNavigation.SetCurrentValue(
            NavigationView.HeaderVisibilityProperty,
            navigationView.SelectedItem?.TargetPageType != typeof(DashboardPage)
                ? Visibility.Visible
                : Visibility.Collapsed
        );
    }
}