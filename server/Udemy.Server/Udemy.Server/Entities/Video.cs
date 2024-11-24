namespace Udemy.Server.Entities;

public class Video
{
    public int VideoId { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string ThumbnailUrl { get; set; } // Nova propriedade
    public double Duration { get; set; } // Duração em segundos

    public ICollection<UserVideoProgress> UserVideoProgresses { get; set; }
}

