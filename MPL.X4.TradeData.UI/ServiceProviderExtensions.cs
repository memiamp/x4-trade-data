using Microsoft.Extensions.DependencyInjection;

namespace MPL.X4.TradeData.UI;

/// <summary>
/// A class that imlements extension methods to a <see cref="IServiceProvider"/>.
/// </summary>
internal static class ServiceProviderExtensions
{
    /// <summary>
    /// Runs the application using the specified <typeparamref name="TForm"/>.
    /// </summary>
    /// <typeparam name="TForm">The type of the form to be run, which must inherit from <see cref="Form"/>.</typeparam>
    /// <param name="serviceProvider">An <see cref="IServiceProvider"/> that is the service provider to use.</param>
    internal static void RunApplication<TForm>(this IServiceProvider serviceProvider)
        where TForm : Form
    {
        var formInstance = serviceProvider.GetRequiredService<TForm>();
        Application.Run(formInstance);
    }
}
