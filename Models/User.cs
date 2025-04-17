using System;
using System.Collections.Generic;

namespace Robochat.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Chat> Chats { get; set; } = [];
}

public class UserDto
{
    public required string Name { get; set; }
}