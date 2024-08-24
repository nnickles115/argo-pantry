using Microsoft.Win32;

namespace ArgoPantry.WPF.ViewModels; 
public sealed partial class ImportViewModel : ObservableRecipient {
    #region SERVICES
    private readonly IServiceProvider _serviceProvider;
    private readonly CSVParserService _csvParser;
    #endregion SERVICES

    #region CONSTRUCTOR
    public ImportViewModel(
        IServiceProvider serviceProvider,
        CSVParserService csvParser) {
        _serviceProvider = serviceProvider;
        _csvParser = csvParser;
    }
    #endregion CONSTRUCTOR

    #region PROPERTIES
    [ObservableProperty]
    private Visibility _openedFilePathVisibility = Visibility.Collapsed;

    [ObservableProperty]
    private string _openedFilePath = string.Empty;
    #endregion PROPERTIES

    #region FUNCTIONS
    private void ParseFile() {

    }
    #endregion FUNCTIONS

    #region COMMANDS
    [RelayCommand]
    public void OpenFile() {
        OpenedFilePathVisibility = Visibility.Collapsed;

        OpenFileDialog openFileDialog =
            new() {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "CSV Files (*.csv)|*.csv"
            };

        if(openFileDialog.ShowDialog() != true) return;
        if(!File.Exists(openFileDialog.FileName)) return;

        OpenedFilePath = openFileDialog.FileName;
        OpenedFilePathVisibility = Visibility.Visible;

        ParseFile();
    }
    #endregion COMMANDS
}