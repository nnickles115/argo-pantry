using ArgoPantry.WPF.ViewModels.Contracts;
using System.Diagnostics;

namespace ArgoPantry.WPF.Helpers;

public static partial class DialogHelper {
    [GeneratedRegex("[^0-9]+")]
    private static partial Regex NonNumbers();

    public static async Task ShowErrorDialogAsync(string message) {
        var dialog = new ContentDialog {
            Title = "Validation Error",
            Content = message,
            CloseButtonText = "OK"
        };

        await dialog.ShowAsync();
    }

    public static bool IsTextNumeric(string text) {
        Regex regex = NonNumbers(); // Match any character that is not a digit
        return !regex.IsMatch(text);
    }

    /// <summary>
    /// Handles the PreviewTextInput event for a TextBox/NumberBox.
    /// Ensures that only numeric input is allowed.
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">The event data.</param>
    public static void OnPreviewTextInput(object sender, TextCompositionEventArgs e) {
        if(!string.IsNullOrEmpty(e.Text)) {
            e.Handled = !IsTextNumeric(e.Text);
        }
    }

    /// <summary>
    /// Handles the Pasting event for a TextBox/NumberBox.
    /// Ensures that only numeric input is allowed.
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="e">The event data.</param>
    public static void OnPasting(object sender, DataObjectPastingEventArgs e) {
        if(e.DataObject.GetDataPresent(typeof(string))) {
            string text = (string)e.DataObject.GetData(typeof(string));
            if(!string.IsNullOrEmpty(text) && !IsTextNumeric(text)) {
                e.CancelCommand();
            }
        }
        else {
            e.CancelCommand();
        }
    }

    /// <summary>
    /// Handles the Closing event for a ContentDialog.
    /// Ensures that the dialog only closes if the from data is valid.
    /// </summary>
    /// <param name="sender">The event source.</param>
    /// <param name="args">The event data.</param>
    /// <param name="viewModel">The form to validate.</param>
    public static void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args, IFormValidator viewModel) {
        if(args.Result != ContentDialogResult.None) {
            args.Cancel = viewModel.ContainsErrors(); // If the form contains errors, cancel the close event
        }
    }
}