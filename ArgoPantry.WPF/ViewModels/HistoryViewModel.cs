
namespace ArgoPantry.WPF.ViewModels;

public sealed partial class HistoryViewModel {
    public HistoryViewModel(
        IServiceProvider serviceProvider,
        IDataService<OrderItem> studentDataService
    ) { }

    private bool FilterItem(OrderItem item) {
        throw new NotImplementedException();
    }
}