using System.Linq;
using Microsoft.EntityFrameworkCore;
using Robochat.Models;

namespace Robochat.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseSqlite($"Data Source={nameof(Robochat)}.sqlite")
            .UseSeeding((context, _) =>
            {
                var exists = context.Set<User>().FirstOrDefault(u => u.Name == Config.UserName);

                if (exists is null)
                {
                    var user = new User()
                    {
                        Name = Config.UserName
                    };
                    var bot = new User()
                    {
                        Name = Config.BotName
                    };
                    var chat = new Chat();

                    chat.Users.Add(user);
                    chat.Users.Add(bot);

                    context.Set<User>().Add(user);
                    context.Set<User>().Add(bot);
                    context.Set<Chat>().Add(chat);
                }

                context.SaveChanges();
            });
    }
}