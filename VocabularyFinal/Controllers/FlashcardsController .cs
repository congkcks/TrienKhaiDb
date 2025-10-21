using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VocabularyFinal.Data;

[ApiController]
[Route("api/[controller]")]
public class FlashcardsController : ControllerBase
{
    private readonly LuyenTuVungDbContext _context;
    public FlashcardsController(LuyenTuVungDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroups()
    {
        var groups = await _context.vocab_groups
            .Select(g => new {
                g.id,
                g.name,
                g.type,
                g.created_at,
                FlashcardCount = g.flashcards.Count
            })
            .ToListAsync();

        return Ok(groups);
    }
    [HttpGet("by-group/{groupId}")]
    public async Task<IActionResult> GetFlashcardsByGroup(Guid groupId)
    {
        var flashcards = await _context.flashcards
            .Where(f => f.group_id == groupId)
            .Select(f => new {
                f.id,
                f.english_word,
                f.vietnamese_meaning,
                f.example_sentence_en,
                f.example_sentence_vi,
                f.phonetic
            })
            .ToListAsync();

        if (!flashcards.Any())
            return NotFound(new { message = "Không có flashcard nào trong nhóm này." });

        return Ok(flashcards);
    }
}
