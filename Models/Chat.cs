using System.Collections.Generic;

namespace Robochat.Models;

public class Chat
{
    public int Id { get; set; }

    public ICollection<User> Users { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}

public class ChatDto
{
    public int Id { get; set; }

    public IEnumerable<UserDto> Users { get; set; } = [];
    public IEnumerable<MessageDto> Messages { get; set; } = [];
}