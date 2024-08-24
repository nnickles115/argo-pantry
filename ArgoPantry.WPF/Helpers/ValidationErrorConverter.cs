namespace ArgoPantry.WPF.Helpers;

internal sealed class ValidationErrorConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if(value is System.Collections.IEnumerable errors) {
            var error = errors.Cast<ValidationError>().FirstOrDefault();
            if(error != null) {
                return error.ErrorContent.ToString()!;
            }
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}