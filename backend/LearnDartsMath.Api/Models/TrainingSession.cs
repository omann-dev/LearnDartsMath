namespace LearnDartsMath.Api.Models;

public class TrainingSession
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime StartetAt { get; set; } = DateTime.UtcNow;
    public string Mode { get; set; } = string.Empty;

    public ICollection<ThrowEntry> ThrowEntries { get; set; } = new List<ThrowEntry>();

}