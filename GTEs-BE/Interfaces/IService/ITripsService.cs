using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;

namespace GTEs_BE.Interfaces.IService
{
    public interface ITripsService
    {
        public Task<List<Viaggio>> GetTripsAsync();

        public Task<Viaggio?> GetTripAsync(Guid id);

        public Task<Viaggio> CreateTripAsync(ViaggioInputModel model);

        public Task<bool> DeleteTripAsync(Guid id);

        public Task<bool> AddStopAsync(Guid tripId, Fermata stop);

        public Task<bool> RemoveStopAsync(Guid tripId, Guid stopId);

        public Task<Viaggio?> UpdateTripAsync(Guid idViaggio, ViaggioInputModel updatedTrip);

    }
}
