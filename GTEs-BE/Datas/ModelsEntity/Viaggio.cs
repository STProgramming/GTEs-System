using GTEs_BE.Datas.Enums;
using System.ComponentModel.DataAnnotations;

namespace GTEs_BE.Datas.ModelsEntity
{
    public class Viaggio
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string TripName { get; set; } = "Nuovo viaggio";

        [Required]
        public string OriginAddress { get; set; } = string.Empty;

        [Required]
        public string DestinationAddress { get; set; } = string.Empty;

        [Required]
        public double EstimatedDistanceKm { get; set; }

        [Required]
        public TimeSpan EstimatedDuration { get; set; }

        [Required]
        public TripMode Mode { get; set; } = TripMode.Fastest;

        // Autonomia e carburante
        [Required]
        public double CurrentFuelRangeKm { get; set; }         // es. 150 km autonomia
        public double EstimatedFuelCostEuro { get; set; }
        public double EstimatedTollCostEuro { get; set; }

        // Elenco delle tappe
        public List<Fermata> Stops { get; set; } = new();

        [Required]
        public DateTime? ScheduledDepartureTime { get; set; }

        [Required]
        public bool IncludeSuggestedStops { get; set; } = true;

        [Required]
        public bool IncludeTouristSuggestions { get; set; } = true;

        [Required]
        public bool IsConfirmed { get; set; } = false;
    }
}
