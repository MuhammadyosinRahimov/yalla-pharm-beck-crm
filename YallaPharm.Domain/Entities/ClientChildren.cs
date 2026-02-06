using YallaPharm.Domain.Enums;

namespace YallaPharm.Domain.Entities;

public class ClientChildren
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ClientId { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public short? Age { get; set; }
    public Gender? Gender { get; set; }

    public virtual Client? Client { get; set; }
}
