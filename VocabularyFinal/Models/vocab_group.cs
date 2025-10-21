using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VocabularyFinal.Models;

public partial class vocab_group
{
    [Key]
    public Guid id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = null!;

    [StringLength(50)]
    public string type { get; set; } = null!;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? created_at { get; set; }

    [InverseProperty("group")]
    public virtual ICollection<flashcard> flashcards { get; set; } = new List<flashcard>();
}
