namespace ArgoPantry.WPF.Views;

public partial class ImportPage : INavigableView<ImportViewModel> {
    public ImportViewModel ViewModel { get; }

    public ImportPage(ImportViewModel viewModel) {
        DataContext = ViewModel = viewModel;
        InitializeComponent();
    }
}
