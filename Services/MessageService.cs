using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Robochat.Models;
using Robochat.Services;

namespace Robochat.Data;

public class MessageService
{
    private readonly UserAccessor _userAccessor;
    private readonly Random _rng;

    public MessageService()
    {
        _userAccessor = new UserAccessor();
        _rng = new Random();
    }

    public async Task<List<Message>> GetMessages(int chatId)
    {
        using var db = new AppDbContext();

        return await db.Messages
            .Include(m => m.Chat)
            .Include(m => m.User)
            .Where(m => m.ChatId == chatId)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();
    }

    public async Task<Result> SendMessage(int chatId, string content)
    {
        using var db = new AppDbContext();
        var user = await _userAccessor.GetUserAsync();

        var chat = await db.Chats
            .Include(ch => ch.Users)
            .Include(ch => ch.Messages)
            .SingleOrDefaultAsync(ch => ch.Id == chatId);

        if (chat is null)
        {
            return Result.Fail("");
        }

        var message = new Message()
        {
            Content = content,
            ChatId = chat.Id,
            CreatedAt = DateTime.UtcNow,
            UserId = user.Id,
        };

        chat.Messages.Add(message);

        await db.SaveChangesAsync();

        return Result.Ok();
    }

    public async Task<Result> GetReplyFromBot(int chatId)
    {
        await Task.Delay(1000);

        using var db = new AppDbContext();

        var chat = await db.Chats
            .Include(ch => ch.Users)
            .Include(ch => ch.Messages)
            .SingleOrDefaultAsync(ch => ch.Id == chatId);

        if (chat is null)
        {
            return Result.Fail("");
        }

        var bot = await db.Users.SingleAsync(u => u.Name == Config.BotName);

        var reply = new Message()
        {
            Content = Config.Replies[_rng.Next(3)],
            ChatId = chat.Id,
            CreatedAt = DateTime.UtcNow,
            UserId = bot.Id,
        };

        chat.Messages.Add(reply);

        await db.SaveChangesAsync();

        return Result.Ok();
    }
}