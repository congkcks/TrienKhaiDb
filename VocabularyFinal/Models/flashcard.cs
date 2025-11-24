using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VocabularyFinal.Models;

[Table("flashcards")]
public partial class Flashcard
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("group_id")]
    public Guid GroupId { get; set; }

    [Column("english_word")]
    [StringLength(100)]
    public string EnglishWord { get; set; } = null!;

    [Column("phonetic")]
    [StringLength(100)]
    public string? Phonetic { get; set; }

    [Column("vietnamese_meaning")]
    public string? VietnameseMeaning { get; set; }

    [Column("example_sentence_en")]
    public string? ExampleSentenceEn { get; set; }

    [Column("example_sentence_vi")]
    public string? ExampleSentenceVi { get; set; }

    [ForeignKey("GroupId")]
    [InverseProperty("Flashcards")]
    public virtual VocabGroup Group { get; set; } = null!;
}
