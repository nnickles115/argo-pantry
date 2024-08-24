namespace ArgoPantry.WPF.Helpers; 
internal sealed class ScaleConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        double size = (double)value;
        return size / 300; // Example calculation; adjust as needed
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}