using GTEs_BE.Datas.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTEs_BE.Datas.ModelsEntity
{
    [Table("Fermate")]
    public class Fermata
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public StopType Type { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public TimeSpan? ExpectedStopDuration { get; set; }

        public bool IsOptional { get; set; } = false; // può essere rimossa
    }
}
