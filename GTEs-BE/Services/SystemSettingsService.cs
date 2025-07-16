using GTEs_BE.Datas;
using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;
using GTEs_BE.Interfaces.IService;
using Microsoft.EntityFrameworkCore;

namespace GTEs_BE.Services
{
    
    public class SystemSettingsService : ISystemSettingsService
    {
        private readonly IDbContextFactory<ApplicationContext> _context;

        public SystemSettingsService(IDbContextFactory<ApplicationContext> context)
        {
            _context = context;
        }
     
        public async Task<List<Contatto>> GetContactsAsync()
        {
            using var context = await _context.CreateDbContextAsync();
            return await context.Contatti.ToListAsync();
        }

        public async Task<Contatto?> GetContactAsync(string IdContatto)
        {
            if(Guid.TryParse(IdContatto, out Guid Id))
            {
                using var context = await _context.CreateDbContextAsync();
                return await context.Contatti.FindAsync(Id);                
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task CreateContactAsync(ContattoInputModel inputModel)
        {
            Contatto newContact = new Contatto
            {
                Name = inputModel.Name,
                LastName = inputModel.LastName,
                BirthDate = inputModel.BirthDate,
                PhoneNumber = inputModel.PhoneNumber,
                SosContact = inputModel.SosContact,
            };
            using var context = await _context.CreateDbContextAsync();
            await context.Contatti.AddAsync(newContact);
            await context.SaveChangesAsync();
        }

        public async Task InsertCarOwnerAsync(string idContact)
        {
            var contact = await GetContactAsync(idContact);
            if(contact == null) throw new NullReferenceException();
            using var context = await _context.CreateDbContextAsync();
            if(context.Contatti.Any(c => c.CarOwner)) throw new OperationCanceledException();
            contact.CarOwner = true;
            context.Contatti.Update(contact);
            await context.SaveChangesAsync();
        }

        public async Task RemoveCarOwnerAsync(string idContact)
        {
            var contact = await GetContactAsync(idContact);
            if (contact == null) throw new NullReferenceException();
            using var context = await _context.CreateDbContextAsync();
            if (!contact.CarOwner) throw new OperationCanceledException();
            contact.CarOwner = false;
            context.Contatti.Update(contact);
            await context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(string idContact)
        {
            var contact = await GetContactAsync(idContact);
            if (contact == null) throw new NullReferenceException();
            using var context = await _context.CreateDbContextAsync();
            if (contact.CarOwner) throw new OperationCanceledException();
            context.Contatti.Remove(contact);
            await context.SaveChangesAsync();
        }
    }
}
