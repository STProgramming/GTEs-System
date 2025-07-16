using GTEs_BE.Datas;
using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;
using GTEs_BE.Interfaces.IService;
using Microsoft.EntityFrameworkCore;

namespace GTEs_BE.Services
{
    public class TripsService : ITripsService
    {
        private readonly IDbContextFactory<ApplicationContext> _context;

        public TripsService(IDbContextFactory<ApplicationContext> context)
        {
            _context = context;
        }

        public async Task<List<Viaggio>> GetTripsAsync()
        {
            await using var context = await _context.CreateDbContextAsync();
            return await context.Viaggi.Include(t => t.Stops).ToListAsync();
        }

        public async Task<Viaggio?> GetTripAsync(Guid id)
        {
            await using var context = await _context.CreateDbContextAsync();
            return await context.Viaggi.Include(t => t.Stops).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Viaggio> CreateTripAsync(ViaggioInputModel model)
        {
            await using var context = await _context.CreateDbContextAsync();
            var trip = new Viaggio
            {
                Id = Guid.NewGuid(),
                TripName = model.TripName,
                OriginAddress = model.OriginAddress,
                DestinationAddress = model.DestinationAddress,
                Mode = model.Mode,
                CurrentFuelRangeKm = model.CurrentFuelRangeKm,
                Stops = model.Stops ?? new List<Fermata>()
            };
            context.Viaggi.Add(trip);
            await context.SaveChangesAsync();
            return trip;
        }

        public async Task<bool> DeleteTripAsync(Guid id)
        {
            await using var context = await _context.CreateDbContextAsync();
            var trip = await context.Viaggi.Include(t => t.Stops).FirstOrDefaultAsync(t => t.Id == id);
            if (trip == null) return false;
            context.Viaggi.Remove(trip);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddStopAsync(Guid tripId, Fermata stop)
        {
            await using var context = await _context.CreateDbContextAsync();
            var trip = await context.Viaggi.Include(t => t.Stops).FirstOrDefaultAsync(t => t.Id == tripId);
            if (trip == null) return false;

            stop.Id = Guid.NewGuid();
            trip.Stops.Add(stop);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveStopAsync(Guid tripId, Guid stopId)
        {
            await using var context = await _context.CreateDbContextAsync();
            var trip = await context.Viaggi.Include(t => t.Stops).FirstOrDefaultAsync(t => t.Id == tripId);
            if (trip == null) return false;

            var stop = trip.Stops.FirstOrDefault(s => s.Id == stopId);
            if (stop == null) return false;

            trip.Stops.Remove(stop);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Viaggio?> UpdateTripAsync(Guid idViaggio, ViaggioInputModel updatedTrip)
        {
            await using var context = await _context.CreateDbContextAsync();
            var existing = await context.Viaggi.Include(t => t.Stops).FirstOrDefaultAsync(t => t.Id == idViaggio);
            if (existing == null) return null;

            context.Entry(existing).CurrentValues.SetValues(updatedTrip);
            existing.Stops = updatedTrip.Stops ?? new List<Fermata>();
            await context.SaveChangesAsync();
            return existing;
        }
    }
}
