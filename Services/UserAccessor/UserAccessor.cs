using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Robochat.Data;
using Robochat.Models;

namespace Robochat.Services.UserAccessor;

public class UserAccessor : IUserAccessor
{
    AppDbContext _db;

    public UserAccessor(AppDbContext db)
    {
        _db = db;
    }

    public Task<User> GetUserAsync()
    {
        return _db.Users.SingleAsync(u => u.Name == Config.UserName);
    }
}