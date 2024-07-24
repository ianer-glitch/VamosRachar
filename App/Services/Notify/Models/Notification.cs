using Domain.Interfaces;

namespace Notify.Models;

public class Notification : IEntity
{
    public Guid Id { get; set; }
    public DateTime Inclusion { get; set; }
    public DateTime Changed { get; set; }
    public bool Excluded { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}