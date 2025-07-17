using GTEs_BE.Datas.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace GTEs_BE.Datas.ModelsEntity
{
    [Table("Abitudini")]
    public class Abitudine
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        // Orario della routine
        [Required]
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        // Giorni in cui è attiva la routine
        [Required]
        public List<DayOfWeek> ActiveDays { get; set; } = new();

        // Coordinate geografiche per attivare la routine        
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int RadiusMeters { get; set; } = 100;
        public string? Location { get; set; } = string.Empty;

        // Tipo di abitudine
        [Required]
        public HabitType HabitType { get; set; }

        // Meteo richiesto per far partire la routine
        public WeatherCondition? RequiredWeather { get; set; }

        // Climatizzazione
        [Required]
        public bool AutoStartClimate { get; set; }
        public int? TargetTemperature { get; set; }

        // Spotify
        [Required]
        public bool PlaySpotify { get; set; }
        public string? SpotifyPlaylistId { get; set; }

        // Pianificazione viaggi
        [Required]
        public bool TriggerTripPlanner { get; set; }
        public TripMode? PreferredTripMode { get; set; }

        [ForeignKey(nameof(Viaggio))]
        public Guid? IdTrip { get; set; }

        public Viaggio? AssociatedTrip { get; set; } = null;

        // RaceChip
        public RaceChipMode? RaceChipSetting { get; set; }

        // Filtro batteria (es. non attivare sotto il 25%)
        public int? MinBatteryPercent { get; set; }

        // Stato attivazione
        public bool IsActive { get; set; } = true;
    }
}
