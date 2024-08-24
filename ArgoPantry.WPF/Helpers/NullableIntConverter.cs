namespace ArgoPantry.WPF.Helpers;

internal sealed class NullableIntConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        // Convert integer to double (NumberBox.Value expects a double)
        if(value is int intValue) {
            return (double)intValue;
        }
        return null!; // Return null for empty state
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        // Convert double to integer
        if(value is double doubleValue) {
            return (int)doubleValue;
        }
        return null!; // Return null for empty state
    }
}