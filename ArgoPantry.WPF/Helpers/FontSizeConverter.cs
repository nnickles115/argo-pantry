namespace ArgoPantry.WPF.Helpers; 
internal sealed class FontSizeConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        double width = (double)value;
        return width / 25;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}