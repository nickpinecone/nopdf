using Microsoft.Extensions.DependencyInjection;
using Robochat.Data;
using Robochat.Services.UserAccessor;
using Robochat.ViewModels;

namespace Robochat.Services;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        collection.AddDbContext<AppDbContext>();

        collection.AddScoped<Mapper>();
        collection.AddScoped<IUserAccessor, UserAccessor.UserAccessor>();
        collection.AddScoped<IChatRepository, ChatRepository>();
        collection.AddScoped<IMessageRepository, MessageRepository>();

        collection.AddTransient<MainWindowViewModel>();
    }
}