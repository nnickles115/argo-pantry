namespace ArgoPantry.WPF.Helpers;

internal sealed class ValidationErrorMultiConverter : IMultiValueConverter {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
        if(values[0] is not ReadOnlyObservableCollection<ValidationError> errors || errors.Count == 0) {
            return string.Empty;
        }

        return string.Join(Environment.NewLine, errors.Select(e => e.ErrorContent.ToString()));
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}