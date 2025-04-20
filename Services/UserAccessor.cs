using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Robochat.Data;
using Robochat.Models;
using Robochat.Utils;

namespace Robochat.Services;

public class UserAccessor
{
    /// <summary>
    /// Получаем пользователя приложения (симуляция регистрации и авторизации)
    /// </summary>
    public Task<User> GetUserAsync()
    {
        using var db = new AppDbContext();

        return db.Users.SingleAsync(u => u.Name == Config.UserName);
    }
}