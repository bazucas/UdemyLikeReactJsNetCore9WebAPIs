// Controllers/VideosController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy.Server.Database;
using Udemy.Server.Dtos;
using Udemy.Server.Entities;

namespace Udemy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideosController : ControllerBase
{
    private readonly AppDbContext _context;

    public VideosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Videos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoDto>>> GetVideos()
    {
        var videos = await _context.Videos.ToListAsync();
        return videos.Select(v => new VideoDto(
            v.VideoId,
            v.Title,
            v.Url,
            v.ThumbnailUrl,
            v.Duration)).ToList();
    }

    // GET: api/Videos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<VideoDto>> GetVideo(int id)
    {
        var video = await _context.Videos.FindAsync(id);

        if (video == null)
        {
            return NotFound();
        }

        return new VideoDto(video.VideoId, video.Title, video.ThumbnailUrl, video.Url, video.Duration);
    }

    // POST: api/Videos
    [HttpPost]
    public async Task<ActionResult<VideoDto>> PostVideo(VideoDto videoDto)
    {
        var video = new Video
        {
            Title = videoDto.Title,
            Url = videoDto.Url,
            Duration = videoDto.Duration
        };

        _context.Videos.Add(video);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVideo), new { id = video.VideoId }, videoDto);
    }

    // PUT: api/Videos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVideo(int id, VideoDto videoDto)
    {
        if (id != videoDto.VideoId)
        {
            return BadRequest();
        }

        var video = await _context.Videos.FindAsync(id);
        if (video == null)
        {
            return NotFound();
        }

        video.Title = videoDto.Title;
        video.Url = videoDto.Url;
        video.Duration = videoDto.Duration;

        _context.Entry(video).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Videos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideo(int id)
    {
        var video = await _context.Videos.FindAsync(id);
        if (video == null)
        {
            return NotFound();
        }

        _context.Videos.Remove(video);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}