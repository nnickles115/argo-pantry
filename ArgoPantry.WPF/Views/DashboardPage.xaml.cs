namespace ArgoPantry.WPF.Views; 
public partial class DashboardPage : INavigableView<DashboardViewModel> {
    public DashboardViewModel ViewModel { get; }

    public DashboardPage(DashboardViewModel viewModel) {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}
