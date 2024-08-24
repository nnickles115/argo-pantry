namespace ArgoPantry.WPF.Helpers;

internal sealed class PaneDisplayModeToIndexConverter : IValueConverter {
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return value switch {
            NavigationViewPaneDisplayMode.LeftMinimal => 1,
            NavigationViewPaneDisplayMode.Top => 2,
            NavigationViewPaneDisplayMode.Bottom => 3,
            _ => 0 // Default to Left
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        return value switch {
            1 => NavigationViewPaneDisplayMode.LeftMinimal,
            2 => NavigationViewPaneDisplayMode.Top,
            3 => NavigationViewPaneDisplayMode.Bottom,
            _ => NavigationViewPaneDisplayMode.Left
        };
    }
}