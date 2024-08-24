namespace ArgoPantry.WPF.Helpers; 

internal sealed class GreaterThanConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        return double.TryParse(value.ToString(), out double doubleValue) && 
            double.TryParse(parameter.ToString(), out double doubleParameter) &&
            doubleValue > doubleParameter;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}