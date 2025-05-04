using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EvaCore.Accounting.Application.Extensions;

/// <summary>
/// Extension methods for adding application services.
/// </summary>
public static class ApplicationExtension
{
    /// <summary>
    /// Adds application services to the specified service collection.
    /// This method registers MediatR services from the current assembly.
    /// It is typically called in the ConfigureServices method of the Startup class.
    /// </summary>
    /// <param name="collection">service collection</param>
    /// <returns>return the collection config</returns>
    public static IServiceCollection AddServicesApplication(this IServiceCollection collection)
    {
        collection.AddMediatR(config=>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return collection;
    }

}
