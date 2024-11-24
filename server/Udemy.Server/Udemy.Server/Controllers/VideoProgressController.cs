// Controllers/VideoProgressController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy.Server.Database;
using Udemy.Server.Dtos;
using Udemy.Server.Entities;

namespace Udemy.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoProgressController : ControllerBase
{
    private readonly AppDbContext _context;

    public VideoProgressController(AppDbContext context)
    {
        _context = context;
    }

    // Método fictício para obter o ID do usuário autenticado
    private int GetUserId()
    {
        // Em uma implementação real, você obteria o ID do usuário do contexto de autenticação
        return 1; // Por exemplo, usuário com ID 1
    }

    // POST: api/VideoProgress/5
    [HttpPost("{videoId}")]
    public async Task<IActionResult> UpdateProgress(int videoId, [FromBody] ProgressDto progressDto)
    {
        var userId = GetUserId();

        var userProgress = await _context.UserVideoProgresses
            .FirstOrDefaultAsync(up => up.UserId == userId && up.VideoId == videoId);

        if (userProgress == null)
        {
            userProgress = new UserVideoProgress
            {
                UserId = userId,
                VideoId = videoId,
                CurrentTime = progressDto.CurrentTime
            };
            _context.UserVideoProgresses.Add(userProgress);
        }
        else
        {
            userProgress.CurrentTime = progressDto.CurrentTime;
            _context.UserVideoProgresses.Update(userProgress);
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    // POST: api/VideoProgress/5/complete
    [HttpPost("{videoId}/complete")]
    public async Task<IActionResult> ToggleCompletion(int videoId)
    {
        var userId = GetUserId();

        var userProgress = await _context.UserVideoProgresses
            .FirstOrDefaultAsync(up => up.UserId == userId && up.VideoId == videoId);

        var video = await _context.Videos.FindAsync(videoId);
        if (video == null)
        {
            return NotFound("Vídeo não encontrado.");
        }

        if (userProgress == null)
        {
            userProgress = new UserVideoProgress
            {
                UserId = userId,
                VideoId = videoId,
                CurrentTime = video.Duration
            };
            _context.UserVideoProgresses.Add(userProgress);
        }
        else
        {
            // Alterna entre completo e não completo
            userProgress.CurrentTime = userProgress.CurrentTime >= video.Duration ? 0 : video.Duration;
            _context.UserVideoProgresses.Update(userProgress);
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    // GET: api/VideoProgress/5
    [HttpGet("{videoId}")]
    public async Task<ActionResult<ProgressDto>> GetProgress(int videoId)
    {
        var userId = GetUserId();

        var userProgress = await _context.UserVideoProgresses
            .FirstOrDefaultAsync(up => up.UserId == userId && up.VideoId == videoId);

        if (userProgress == null)
        {
            return NotFound();
        }

        return new ProgressDto(userProgress.CurrentTime);
    }
}