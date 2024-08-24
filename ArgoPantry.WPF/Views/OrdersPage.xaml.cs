namespace ArgoPantry.WPF.Views;

public partial class OrdersPage : INavigableView<OrderViewModel> {
    public OrderViewModel ViewModel { get; }
    private double _normalHeight = 0;
    private double _maximizedHeight = 0;

    public OrdersPage(OrderViewModel viewModel) {
        DataContext = ViewModel = viewModel;
        InitializeComponent();

        Application.Current.MainWindow.StateChanged += (s, e) => OnWindowStateChanged();
    }

    #region Responsive Layout Attempt
    private void OnLoaded(object sender, RoutedEventArgs e) {
        _normalHeight = LayoutRoot.ActualHeight - GetTotalFixedHeight();
        ViewModel.ListViewHeight = _normalHeight;
    }

    private void UpdateMaximizedHeight() {
        _maximizedHeight = LayoutRoot.ActualHeight - GetTotalFixedHeight();
    }

    private void OnWindowStateChanged() {
        Dispatcher.BeginInvoke(new Action(() => AdjustListViewHeight()), DispatcherPriority.Render);
    }

    private void AdjustListViewHeight() {
        WindowState state = Application.Current.MainWindow.WindowState;

        switch(state) {
            case WindowState.Normal:
                ViewModel.ListViewHeight = _normalHeight;
                break;

            case WindowState.Maximized:
                UpdateMaximizedHeight();
                ViewModel.ListViewHeight = _maximizedHeight;
                break;

            case WindowState.Minimized:
                break;

            default:
                break;
        }
    }

    private double GetTotalFixedHeight() {
        // Sum up the heights of all fixed-height elements (headers, footers, etc.)
        double headerHeight = Header.ActualHeight + Header.Margin.Top + Header.Margin.Bottom;
        double footerHeight = Footer.ActualHeight + Footer.Margin.Top + Footer.Margin.Bottom;

        return headerHeight + footerHeight + 16; // 16 is the margin between the ListView and the footer
    }
    #endregion Responsive Layout Attempt

    private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args) {
        string selectedStudentId = args.SelectedItem.ToString()!;
        ViewModel.SuggestionChosenCommand.Execute(selectedStudentId);
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        var addedItems = e.AddedItems.Cast<Item>().ToList();
        var removedItems = e.RemovedItems.Cast<Item>().ToList();

        var parameter = (addedItems, removedItems);
        ViewModel.SelectionChangedCommand.Execute(parameter);
    }

    private void ListView_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
        if(!ViewModel.IsEditModeEnabled) {
            e.Handled = true; // Prevent the item from being selected
        }
    }
}