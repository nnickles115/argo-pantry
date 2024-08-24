namespace ArgoPantry.WPF.Controls;

public partial class CrudTableControl : UserControl {
    public string AddButtonContent {
        get { return (string)GetValue(AddButtonContentProperty); }
        set { SetValue(AddButtonContentProperty, value); }
    }

    public static readonly DependencyProperty AddButtonContentProperty =
        DependencyProperty.Register(
            "AddButtonContent",
            typeof(string), 
            typeof(CrudTableControl), 
            new PropertyMetadata(string.Empty)
        );

    public string UpdateButtonContent {
        get { return (string)GetValue(UpdateButtonContentProperty); }
        set { SetValue(UpdateButtonContentProperty, value); }
    }

    public static readonly DependencyProperty UpdateButtonContentProperty =
        DependencyProperty.Register(
            "UpdateButtonContent",
            typeof(string),
            typeof(CrudTableControl),
            new PropertyMetadata(string.Empty)
        );

    public string DeleteButtonContent {
        get { return (string)GetValue(DeleteButtonContentProperty); }
        set { SetValue(DeleteButtonContentProperty, value); }
    }

    public static readonly DependencyProperty DeleteButtonContentProperty =
        DependencyProperty.Register(
            "DeleteButtonContent",
            typeof(string),
            typeof(CrudTableControl),
            new PropertyMetadata(string.Empty)
        );

    public ICommand AddCommand {
        get { return (ICommand)GetValue(AddCommandProperty); }
        set { SetValue(AddCommandProperty, value); }
    }

    public static readonly DependencyProperty AddCommandProperty =
        DependencyProperty.Register(
            "AddCommand",
            typeof(ICommand),
            typeof(CrudTableControl),
            new PropertyMetadata(null)
        );

    public ICommand UpdateCommand {
        get { return (ICommand)GetValue(UpdateCommandProperty); }
        set { SetValue(UpdateCommandProperty, value); }
    }

    public static readonly DependencyProperty UpdateCommandProperty =
        DependencyProperty.Register(
            "UpdateCommand",
            typeof(ICommand),
            typeof(CrudTableControl),
            new PropertyMetadata(null)
        );

    public ICommand DeleteCommand {
        get { return (ICommand)GetValue(DeleteCommandProperty); }
        set { SetValue(DeleteCommandProperty, value); }
    }

    public static readonly DependencyProperty DeleteCommandProperty =
        DependencyProperty.Register(
            "DeleteCommand",
            typeof(ICommand),
            typeof(CrudTableControl),
            new PropertyMetadata(null)
        );

    public bool IsAddButtonEnabled {
        get { return (bool)GetValue(IsAddButtonEnabledProperty); }
        set { SetValue(IsAddButtonEnabledProperty, value); }
    }

    public static readonly DependencyProperty IsAddButtonEnabledProperty =
        DependencyProperty.Register(
            "IsAddButtonEnabled",
            typeof(bool),
            typeof(CrudTableControl),
            new PropertyMetadata(true) // Default to true (enabled)
        );

    public bool IsUpdateButtonEnabled {
        get { return (bool)GetValue(IsUpdateButtonEnabledProperty); }
        set { SetValue(IsUpdateButtonEnabledProperty, value); }
    }

    public static readonly DependencyProperty IsUpdateButtonEnabledProperty =
        DependencyProperty.Register(
            "IsUpdateButtonEnabled",
            typeof(bool),
            typeof(CrudTableControl),
            new PropertyMetadata(true) // Default to true (enabled)
        );

    public bool IsDeleteButtonEnabled {
        get { return (bool)GetValue(IsDeleteButtonEnabledProperty); }
        set { SetValue(IsDeleteButtonEnabledProperty, value); }
    }

    public static readonly DependencyProperty IsDeleteButtonEnabledProperty =
        DependencyProperty.Register(
            "IsDeleteButtonEnabled",
            typeof(bool),
            typeof(CrudTableControl),
            new PropertyMetadata(true) // Default to true (enabled)
        );

    public Visibility AddButtonVisibility {
        get { return (Visibility)GetValue(AddButtonVisibilityProperty); }
        set { SetValue(AddButtonVisibilityProperty, value); }
    }

    public static readonly DependencyProperty AddButtonVisibilityProperty =
        DependencyProperty.Register(
            "AddButtonVisibility",
            typeof(Visibility),
            typeof(CrudTableControl),
            new PropertyMetadata(Visibility.Visible)
        );

    public Visibility UpdateButtonVisibility {
        get { return (Visibility)GetValue(UpdateButtonVisibilityProperty); }
        set { SetValue(UpdateButtonVisibilityProperty, value); }
    }

    public static readonly DependencyProperty UpdateButtonVisibilityProperty =
        DependencyProperty.Register(
            "UpdateButtonVisibility",
            typeof(Visibility),
            typeof(CrudTableControl),
            new PropertyMetadata(Visibility.Visible)
        );

    public Visibility DeleteButtonVisibility {
        get { return (Visibility)GetValue(DeleteButtonVisibilityProperty); }
        set { SetValue(DeleteButtonVisibilityProperty, value); }
    }

    public static readonly DependencyProperty DeleteButtonVisibilityProperty =
        DependencyProperty.Register(
            "DeleteButtonVisibility",
            typeof(Visibility),
            typeof(CrudTableControl),
            new PropertyMetadata(Visibility.Visible)
        );

    public CrudTableControl() {
        InitializeComponent();
    }
}