using System.ComponentModel.DataAnnotations.Schema;

namespace GeoGuess.Model.Entities;

public class PlayerPanorama : BaseEntity
{
    public int PlayerId { get; set; }
    public int PanoramaId { get; set; }


    [ForeignKey("PlayerId")]
    public virtual PseudoPlayer? Player { get; set; }

    [ForeignKey("PanoramaId")]
    public virtual Panorama? Panorama { get; set; }
}
