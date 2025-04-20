using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Robochat.Models;
using Robochat.Services;

namespace Robochat.Data;

public class ChatService
{
    private readonly UserAccessor _userAccessor;

    public ChatService()
    {
        _userAccessor = new UserAccessor();
    }

    public async Task<List<Chat>> GetChats()
    {
        using var db = new AppDbContext();
        var user = await _userAccessor.GetUserAsync();

        var chats = await db.Chats
            .Where(ch => ch.Users.Contains(user))
            .Include(ch => ch.Messages)
            .Include(ch => ch.Users)
            .AsSplitQuery()
            .ToListAsync();

        foreach (var chat in chats)
        {
            chat.Messages = chat.Messages.OrderByDescending(m => m.CreatedAt).Take(1).ToList();
            chat.Users = chat.Users.Where(u => u.Id != user.Id).ToList();
        }

        return chats;
    }

    public async Task<Result<Chat>> GetChatById(int id)
    {
        using var db = new AppDbContext();
        var user = await _userAccessor.GetUserAsync();

        var chat = await db.Chats
            .Include(ch => ch.Messages)
            .Include(ch => ch.Users)
            .AsSplitQuery()
            .SingleOrDefaultAsync(ch => ch.Id == id);

        if (chat is null)
        {
            return Result.Fail("");
        }

        chat.Users = chat.Users.Where(u => u.Id != user.Id).ToList();

        return Result.Ok(chat);
    }
}