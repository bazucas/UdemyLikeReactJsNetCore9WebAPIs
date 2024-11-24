namespace Udemy.Server.Api.Entities;

public class Video
{
    public int VideoId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; }
    public double Duration { get; set; }

    public ICollection<UserVideoProgress> UserVideoProgresses { get; set; }
}

