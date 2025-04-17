using System.Threading.Tasks;
using Robochat.Models;

namespace Robochat.Services.UserAccessor;

public interface IUserAccessor
{
    /// <summary>
    /// Получаем пользователя приложения (симуляция регистрации и авторизации)
    /// </summary>
    public Task<User> GetUserAsync();
}