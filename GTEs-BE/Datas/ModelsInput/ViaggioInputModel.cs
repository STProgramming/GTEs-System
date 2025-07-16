using GTEs_BE.Datas.Enums;
using GTEs_BE.Datas.ModelsEntity;
using System.ComponentModel.DataAnnotations;

namespace GTEs_BE.Datas.ModelsInput
{
    public class ViaggioInputModel
    {
        [Required]
        public string TripName { get; set; } = string.Empty;

        [Required]
        public string OriginAddress { get; set; } = string.Empty;

        [Required]
        public string DestinationAddress { get; set; } = string.Empty;

        public TripMode Mode { get; set; }

        public double CurrentFuelRangeKm { get; set; }

        public List<Fermata>? Stops { get; set; }
    }
}
