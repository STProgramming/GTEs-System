using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;

namespace GTEs_BE.Interfaces.IService
{
    public interface ISystemSettingsService
    {
        public Task<List<Contatto>> GetContactsAsync();
        public Task<Contatto?> GetContactAsync(string IdContatto);
        public Task CreateContactAsync(ContattoInputModel inputModel);
        public Task InsertCarOwnerAsync(string idContact);
        public Task DeleteContactAsync(string idContact);
    }
}
