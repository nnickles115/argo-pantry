using System.Reflection;

namespace ArgoPantry.WPF.Helpers; 
internal sealed class NameToPageTypeConverter {
    private static readonly Type[] PageTypes = Assembly
        .GetExecutingAssembly()
        .GetTypes()
        .Where(t => t.Namespace?.StartsWith("ArgoPantry.WPF.Views") ?? false)
        .ToArray();

    public static Type? Convert(string pageName) {
        pageName = pageName.Trim() + "Page";

        return PageTypes.FirstOrDefault(singlePageType =>
            singlePageType.Name.Equals(pageName, StringComparison.CurrentCultureIgnoreCase)
        );
    }
}