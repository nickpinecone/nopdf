using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Robochat.Data;
using Robochat.ViewModels;

namespace Robochat.Services;

public static class ServiceManager
{
    public static ServiceProvider ServiceProvider { get; }
    private static IServiceCollection Services { get; }

    static ServiceManager()
    {
        Services = new ServiceCollection();
        AddCommonServices();
        ServiceProvider = Services.BuildServiceProvider();
    }

    public static void AddCommonServices()
    {
        Services.AddDbContext<AppDbContext>();

        Services.AddScoped<UserAccessor>();
        Services.AddScoped<ChatService>();
        Services.AddScoped<MessageService>();

        Services.AddTransient<ViewModelActivator>();
        Services.AddSingleton<Mapper>();
        Services.AddSingleton<RoutingState>();

        Services.AddSingleton<MainWindowViewModel>();
        Services.AddTransient<ChatViewModel>();
        Services.AddTransient<AllChatsViewModel>();
    }
}