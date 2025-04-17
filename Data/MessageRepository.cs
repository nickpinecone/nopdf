using System.Collections.Generic;
using System.Threading.Tasks;
using Robochat.Models;

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
    public Task<List<Message>> GetMessages(int chatId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Message> SendMessage(SendMessageRequest request)
    {
        throw new System.NotImplementedException();
    }
}