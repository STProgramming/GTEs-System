using GTEs_BE.Datas;
using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;
using GTEs_BE.Hubs;
using GTEs_BE.Interfaces.IService;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Specialized;

namespace GTEs_BE.Services
{
    public class NotificationsService : INotificationsService
    {
        private readonly IDbContextFactory<ApplicationContext> _context;

        private readonly IHubContext<NotificationsHub> _hubContext;

        public NotificationsService(IDbContextFactory<ApplicationContext> context,
            IHubContext<NotificationsHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<List<Notifica>> GetNotificationsAsync(bool All)
        {
            using var context = await _context.CreateDbContextAsync();
            if (All)
            {
                return await context.Notifiche.ToListAsync();
            }
            else
            {
                return await context.Notifiche.Where(n => n.Read == false).ToListAsync();
            }
        }

        public async Task<Notifica?> GetNotificationAsync(string idNotification)
        {
            if(Guid.TryParse(idNotification, out Guid Id))
            {
                using var context = await _context.CreateDbContextAsync();
                return await context.Notifiche.FindAsync(idNotification);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public async Task CreateNotificationAsync(NotificaInputModel inputModel)
        {
            using var context = await _context.CreateDbContextAsync();
            if(context.Notifiche.Any(n => n.Created == inputModel.Created)) throw new OperationCanceledException();
            Notifica newNotification = new Notifica
            {
                Topic = inputModel.Topic,
                Title = inputModel.Title,
                Description = inputModel.Description,
                Created = inputModel.Created,
                Gravity = inputModel.Gravity,
                Read = false
            };
            context.Notifiche.Add(newNotification);
            await context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", newNotification);
        }

        public async Task SetReadNotificationsAsync(List<string> listIdNotificationsReaded)
        {
            foreach(var notification in listIdNotificationsReaded)
            {
                var notificationDetail = await GetNotificationAsync(notification);
                using var context = await _context.CreateDbContextAsync();
                bool canSave = false;
                if(notificationDetail != null)
                {
                    notificationDetail.Read = true;
                    context.Notifiche.Update(notificationDetail);
                    canSave = true;
                }
                if (canSave)
                {
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
