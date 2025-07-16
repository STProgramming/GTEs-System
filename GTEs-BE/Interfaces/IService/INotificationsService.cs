using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;

namespace GTEs_BE.Interfaces.IService
{
    public interface INotificationsService
    {
        public Task<List<Notifica>> GetNotificationsAsync(bool All);

        public Task<Notifica?> GetNotificationAsync(string idNotification);

        public Task CreateNotificationAsync(NotificaInputModel inputModel);

        public Task SetReadNotificationsAsync(List<string> listIdNotificationsReaded);

    }
}
