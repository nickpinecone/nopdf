using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Robochat.Models;
using Robochat.Services.UserAccessor;

namespace Robochat.Data;

public class SendMessageRequest
{
    public required string Content { get; set; }
    public required int ChatId { get; set; }
}

public interface IMessageRepository
{
    public Task<List<Message>> GetMessages(int chatId);
    public Task<Message> SendMessage(SendMessageRequest request);
}

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _db;

    public MessageRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<Message>> GetMessages(int chatId)
    {
        return await _db.Messages
            .Include(m => m.Chat)
            .Include(m => m.User)
            .Where(m => m.ChatId == chatId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }

    public Task<Message> SendMessage(SendMessageRequest request)
    {
        throw new System.NotImplementedException();
    }
}