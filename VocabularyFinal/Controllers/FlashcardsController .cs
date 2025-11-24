using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyFinal.Data;
using VocabularyFinal.Models;

[ApiController]
[Route("api/[controller]")]
public class FlashcardsController : ControllerBase
{
    private readonly LuyenTuVungDbContext _context;

    public FlashcardsController(LuyenTuVungDbContext context)
    {
        _context = context;
    }

    [HttpGet("groups")]
    public async Task<IActionResult> GetGroups()
    {
        var groups = await _context.VocabGroups
            .Select(g => new
            {
                g.Id,
                g.Name,
                g.Type,
                g.CreatedAt,
                FlashcardCount = g.Flashcards.Count
            })
            .ToListAsync();

        return Ok(groups);
    }

    [HttpGet("by-group/{groupId:guid}")]
    public async Task<IActionResult> GetFlashcardsByGroup(Guid groupId)
    {
        var group = await _context.VocabGroups
            .Include(g => g.Flashcards)
            .FirstOrDefaultAsync(g => g.Id == groupId);

        if (group == null)
        {
            return NotFound(new { message = "Không tìm thấy nhóm từ vựng." });
        }

        if (!group.Flashcards.Any())
        {
            return Ok(new
            {
                groupId = group.Id,
                groupName = group.Name,
                total = 0,
                data = new List<object>()
            });
        }

        var flashcards = group.Flashcards
            .Select(f => new
            {
                f.Id,
                f.EnglishWord,
                f.Phonetic,
                f.VietnameseMeaning,
                f.ExampleSentenceEn,
                f.ExampleSentenceVi
            });

        return Ok(new
        {
            groupId = group.Id,
            groupName = group.Name,
            total = flashcards.Count(),
            data = flashcards
        });
    }
}
