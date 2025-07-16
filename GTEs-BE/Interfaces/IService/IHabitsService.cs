using GTEs_BE.Datas.ModelsEntity;
using GTEs_BE.Datas.ModelsInput;

namespace GTEs_BE.Interfaces.IService
{
    public interface IHabitsService
    {
        public Task<List<Abitudine>> GetHabitsAsync();

        public Task<Abitudine?> GetHabitAsync(string IdHabit);

        public Task<Abitudine> CreateHabitAsync(AbitudineInputModel input);

        public Task<Abitudine?> UpdateHabitAsync(Guid id, AbitudineInputModel input);

        public Task<bool> DeleteHabitAsync(Guid id);

    }
}
