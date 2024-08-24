namespace ArgoPantry.WPF.Helpers;

public static class PageHelper {
    public static bool ClickedOnDataGridItem(DependencyObject source, DependencyObject root) {
        while(source != null && source != root) {
            if(source is DataGridRow || source is DataGridCell) {
                return true;
            }
            source = VisualTreeHelper.GetParent(source);
        }
        return false;
    }

    public static bool ClickedOnButton(DependencyObject source, DependencyObject root) {
        while(source != null && source != root) {
            if(source is Wpf.Ui.Controls.Button) {
                return true;
            }
            source = VisualTreeHelper.GetParent(source);
        }
        return false;
    }

    public static bool ClickedOnTextBox(DependencyObject source, DependencyObject root) {
        while(source != null && source != root) {
            if(source is Wpf.Ui.Controls.TextBox) {
                return true;
            }
            source = VisualTreeHelper.GetParent(source);
        }
        return false;
    }

    public static void RemoveFocus(UIElement element) {
        FocusManager.SetFocusedElement(FocusManager.GetFocusScope(element), null);
        Keyboard.ClearFocus();
    }
}