using System.Collections.Generic;
using System.Linq;
using Robochat.Models;

namespace Robochat.Services;

public class Mapper
{
    public MessageDto Map(Message message)
    {
        return new MessageDto()
        {
            Content = message.Content,
            CreatedAt = message.CreatedAt,
            User = new UserDto()
            {
                Name = message.User!.Name
            },
        };
    }

    public IEnumerable<MessageDto> Map(IEnumerable<Message> messages)
    {
        return messages.Select(m => this.Map(m));
    }

    public ChatDto Map(Chat chat)
    {
        return new ChatDto()
        {
            Id = chat.Id,
            Messages = Map(chat.Messages),
            Users = chat.Users.Select(
                u => new UserDto()
                {
                    Name = u.Name
                }
            )
        };
    }

    public IEnumerable<ChatDto> Map(IEnumerable<Chat> chats)
    {
        return chats.Select(ch => this.Map(ch));
    }
}