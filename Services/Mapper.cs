using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Robochat.Models;

namespace Robochat.Services;

public class Mapper
{
    public ChatDto Map(Chat chat)
    {
        return new ChatDto()
        {
            Id = chat.Id,
            Messages = chat.Messages.Select(
                m => new MessageDto()
                {
                    Content = m.Content,
                    CreatedAt = m.CreatedAt,
                    User = new UserDto()
                    {
                        Name = m.User!.Name
                    },
                }
            ),
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