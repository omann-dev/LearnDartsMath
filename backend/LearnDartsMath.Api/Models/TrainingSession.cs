namespace LearnDartsMath.Api.Models;

public class TrainingSession
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public DateTime StartetAt { get; set; } = DateTime.UtcNow;
    public string Mode { get; set; } = string.Empty;

}