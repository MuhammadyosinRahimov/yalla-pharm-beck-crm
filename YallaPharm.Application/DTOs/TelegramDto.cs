namespace YallaPharm.Application.DTOs;

public class TelegramUpdateDto
{
    public long UpdateId { get; set; }
    public TelegramMessageDto? Message { get; set; }
}

public class TelegramMessageDto
{
    public long MessageId { get; set; }
    public TelegramUserDto? From { get; set; }
    public TelegramChatDto? Chat { get; set; }
    public string? Text { get; set; }
}

public class TelegramUserDto
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
}

public class TelegramChatDto
{
    public long Id { get; set; }
    public string? Type { get; set; }
}

public class TelegramStatusDto
{
    public bool IsActive { get; set; }
    public string? BotUsername { get; set; }
}
