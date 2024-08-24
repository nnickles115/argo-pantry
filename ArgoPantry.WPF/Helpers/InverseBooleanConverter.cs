namespace ArgoPantry.WPF.Helpers; 
internal sealed class InverseBooleanConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        return value is bool booleanValue && !booleanValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        return value is bool booleanValue && !booleanValue;
    }
}