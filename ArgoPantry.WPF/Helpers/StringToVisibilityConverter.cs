namespace ArgoPantry.WPF.Helpers;

internal sealed class StringToVisibilityConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        return(value is ReadOnlyObservableCollection<ValidationError> errors && errors.Count > 0) 
            ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}