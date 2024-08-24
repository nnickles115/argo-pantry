namespace ArgoPantry.WPF.ViewModels;

public partial class DashboardViewModel(INavigationService navigationService) : ObservableObject {
    [RelayCommand]
    private void CardClick(string parameter) {
        if(string.IsNullOrWhiteSpace(parameter)) return;

        Type? pageType = NameToPageTypeConverter.Convert(parameter);

        if(pageType == null) return;

        navigationService.Navigate(pageType);
    }
}