using System.ComponentModel.DataAnnotations;

namespace GeoGuess.Model.Entities;

public class PseudoPlayer : BaseEntity
{
    // This is just a demo player

    [Required]
    [StringLength(30)]
    public string Name { get; set; }
}
