namespace Udemy.Server.Api.Entities;

public class UserVideoProgress
{
    public int UserId { get; set; }
    public int VideoId { get; set; }
    public double CurrentTime { get; set; }

    public User User { get; set; }
    public Video Video { get; set; }
}
