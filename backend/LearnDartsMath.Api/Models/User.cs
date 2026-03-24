namespace LearnDartsMath.Api.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    public PlayerProfile? PlayerProfile { get; set; } 
    public ICollection<TrainingSession> TrainingSessions { get; set; }  = new List<TrainingSession>();

}