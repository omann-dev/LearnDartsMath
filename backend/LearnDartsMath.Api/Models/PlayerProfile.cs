namespace LearnDartsMath.Api.Models;

public class PlayerProfile
{
    public int Id { get; set; }
    
    public int UserId { get; set;}
    public User User { get; set;} = null!;
    
    public string DisplayName { get; set; } = string.Empty;

}