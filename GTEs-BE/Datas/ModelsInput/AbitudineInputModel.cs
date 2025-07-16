using GTEs_BE.Datas.Enums;
using System.ComponentModel.DataAnnotations;

namespace GTEs_BE.Datas.ModelsInput
{
    public class AbitudineInputModel
    {
        [Required]
        public string Nome { get; set; } = string.Empty;

        public string? Descrizione { get; set; }

        [Required]
        public TimeSpan TempoInizio { get; set; }

        public TimeSpan? TempoFine { get; set; }


        public List<DayOfWeek> GiorniAttivi { get; set; } = new List<DayOfWeek>();
        
        public double Latitudine { get; set; }

        public double Longitudine { get; set; }

        public int RaggioMetri { get; set; }

        public HabitType TipoAbitudine { get; set; }

        public WeatherCondition CondizioniMeteo { get; set; }

        public bool AutoInizioClima { get; set; }

        public int? ObbiettivoTemperatura { get; set; }

        public bool RiproduciSpotify { get; set; }

        public string? PlaylistSpotifyId { get; set; }

        public bool ScatenaTripPlanner { get; set; }
        
        public TripMode? ModalitaViaggio { get; set; }

        public bool Attivo { get; set; }

        public RaceChipMode? MOdalitaRaceChip { get; set; }
    }
}
