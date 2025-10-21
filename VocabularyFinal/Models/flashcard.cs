using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VocabularyFinal.Models;

public partial class flashcard
{
    [Key]
    public Guid id { get; set; }

    public Guid? group_id { get; set; }

    [StringLength(100)]
    public string english_word { get; set; } = null!;

    public string vietnamese_meaning { get; set; } = null!;

    public string? example_sentence_en { get; set; }

    public string? example_sentence_vi { get; set; }

    [StringLength(100)]
    public string? phonetic { get; set; }

    [ForeignKey("group_id")]
    [InverseProperty("flashcards")]
    public virtual vocab_group? group { get; set; }
}
