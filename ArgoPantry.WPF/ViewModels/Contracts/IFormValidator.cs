namespace ArgoPantry.WPF.ViewModels.Contracts;

public interface IFormValidator {
    void SetEntityInfo(object entity);
    bool ContainsErrors();
}