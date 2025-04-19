using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Robochat.Models;

namespace Robochat.Data;

public class MessageService
{
    public async Task<List<Message>> GetMessages(int chatId)
    {
        using var db = new AppDbContext();

        return await db.Messages
            .Include(m => m.Chat)
            .Include(m => m.User)
            .Where(m => m.ChatId == chatId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync();
    }
}