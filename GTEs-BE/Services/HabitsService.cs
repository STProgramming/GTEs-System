using GTEs_BE.Datas;
using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;
using GTEs_BE.Interfaces.IService;
using Microsoft.EntityFrameworkCore;

namespace GTEs_BE.Services
{    
    public class HabitsService : IHabitsService
    {
        private readonly IDbContextFactory<ApplicationContext> _context;

        public HabitsService(
            IDbContextFactory<ApplicationContext> context)
        {
            _context = context;
        }

        public async Task<List<Abitudine>> GetHabitsAsync()
        {
            using (var context = await _context.CreateDbContextAsync())
            {
                return await context.Abitudini.ToListAsync();
            }
        }

        public async Task<Abitudine?> GetHabitAsync(string IdHabit)
        {
            if(!Guid.TryParse(IdHabit, out Guid Id))
            {
                throw new InvalidCastException();
            }
            using (var context = await _context.CreateDbContextAsync())
            {
                return await context.Abitudini.FindAsync(Id);
            }
        }

        public async Task<Abitudine> CreateHabitAsync(AbitudineInputModel input)
        {
            using var context = await _context.CreateDbContextAsync();
            var abitudine = new Abitudine
            {
                Id = Guid.NewGuid(),
                Name = input.Nome,
                Description = input.Descrizione,
                StartTime = input.TempoInizio,
                EndTime = input.TempoFine,
                ActiveDays = input.GiorniAttivi,
                Latitude = input.Latitudine,
                Longitude = input.Longitudine,
                RadiusMeters = input.RaggioMetri,
                HabitType = input.TipoAbitudine,
                RequiredWeather = input.CondizioniMeteo,
                AutoStartClimate = input.AutoInizioClima,
                TargetTemperature = input.ObbiettivoTemperatura,
                PlaySpotify = input.RiproduciSpotify,
                SpotifyPlaylistId = input.PlaylistSpotifyId,
                TriggerTripPlanner = input.ScatenaTripPlanner,
                PreferredTripMode = input.ModalitaViaggio,
                RaceChipSetting = input.MOdalitaRaceChip,
                IsActive = input.Attivo
            };

            context.Abitudini.Add(abitudine);
            await context.SaveChangesAsync();
            return abitudine;
        }

        public async Task<Abitudine?> UpdateHabitAsync(Guid id, AbitudineInputModel input)
        {
            using var context = await _context.CreateDbContextAsync();
            var abitudine = await context.Abitudini.FindAsync(id);
            if (abitudine == null) return null;

            abitudine.Name = input.Nome;
            abitudine.Description = input.Descrizione;
            abitudine.StartTime = input.TempoInizio;
            abitudine.EndTime = input.TempoFine;
            abitudine.ActiveDays = input.GiorniAttivi;
            abitudine.Latitude = input.Latitudine;
            abitudine.Longitude = input.Longitudine;
            abitudine.RadiusMeters = input.RaggioMetri;
            abitudine.HabitType = input.TipoAbitudine;
            abitudine.RequiredWeather = input.CondizioniMeteo;
            abitudine.AutoStartClimate = input.AutoInizioClima;
            abitudine.TargetTemperature = input.ObbiettivoTemperatura;
            abitudine.PlaySpotify = input.RiproduciSpotify;
            abitudine.SpotifyPlaylistId = input.PlaylistSpotifyId;
            abitudine.TriggerTripPlanner = input.ScatenaTripPlanner;
            abitudine.PreferredTripMode = input.ModalitaViaggio;
            abitudine.RaceChipSetting = input.MOdalitaRaceChip;
            abitudine.IsActive = input.Attivo;

            await context.SaveChangesAsync();
            return abitudine;
        }

        public async Task<bool> DeleteHabitAsync(Guid id)
        {
            using var context = await _context.CreateDbContextAsync();
            var abitudine = await context.Abitudini.FindAsync(id);
            if (abitudine == null) return false;

            context.Abitudini.Remove(abitudine);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
