using System;

namespace Robochat.Models;

public class Message
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public required DateTime CreatedAt { get; set; }

    public required int ChatId { get; set; }
    public Chat? Chat { get; set; }

    public required Guid UserId { get; set; }
    public User? User { get; set; }
}

public class MessageDto
{
    public required string Content { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required UserDto User { get; set; }
}