namespace ArgoPantry.WPF.Helpers;

internal sealed class ButtonStateToIndexConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if(value is bool isEnabled) {
            return isEnabled ? 1 : 0; // 1 for Enabled, 0 for Disabled
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        if(value is int index) {
            return index == 1; // Enabled if index is 1, otherwise Disabled
        }
        return false;
    }
}