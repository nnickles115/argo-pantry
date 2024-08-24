namespace ArgoPantry.WPF.Views;

public partial class TransactionsPage : INavigableView<TransactionsViewModel> {
    public TransactionsViewModel ViewModel { get; }

    public TransactionsPage(TransactionsViewModel viewModel) {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }

    private async void Page_Loaded(object sender, RoutedEventArgs e) {
        await ViewModel.InitializeAsync();
    }

    private void OnPreviewMouseDown(object sender, MouseButtonEventArgs e) {
        if(e.OriginalSource is DependencyObject source) {
            bool clickedOnDataGridItem = PageHelper.ClickedOnDataGridItem(source, this);
            bool clickedOnButton = PageHelper.ClickedOnButton(source, this);

            if(!clickedOnDataGridItem && !clickedOnButton) {
                DataGrid.SelectedItem = null;
                PageHelper.RemoveFocus(this);
            }
        }
    }
}