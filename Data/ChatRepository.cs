using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Robochat.Models;
using Robochat.Services.UserAccessor;

namespace Robochat.Data;

public interface IChatRepository
{
    public Task<List<Chat>> GetChats();
}

public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _db;
    private readonly IUserAccessor _userAccessor;

    public ChatRepository(AppDbContext db, IUserAccessor userAccessor)
    {
        _db = db;
        _userAccessor = userAccessor;
    }

    public async Task<List<Chat>> GetChats()
    {
        var user = await _userAccessor.GetUserAsync();

        var chats = await _db.Chats
            .Where(ch => ch.Users.Contains(user))
            .Include(ch => ch.Messages)
            .Include(ch => ch.Users)
            .AsSplitQuery()
            .ToListAsync();

        foreach (var chat in chats)
        {
            chat.Users = chat.Users.Where(u => u.Id != user.Id).ToList();
        }

        return chats;
    }
}