namespace ArgoPantry.WPF.ViewModels.Contracts; 
public interface IDialogForm {
    void InputBox_PreviewTextInput(object sender, TextCompositionEventArgs e);
    void InputBox_Pasting(object sender, DataObjectPastingEventArgs e);
    void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args);
}