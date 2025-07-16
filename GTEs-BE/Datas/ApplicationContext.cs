using GTEs_BE.Datas.ModelsEntity;
using Microsoft.EntityFrameworkCore;

namespace GTEs_BE.Datas
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abitudine> Abitudini { get; set; }

        public virtual DbSet<Viaggio> Viaggi { get; set; }

        public virtual DbSet<Fermata> Fermate { get; set; }

        public virtual DbSet<Contatto> Contatti {  get; set; }

        public virtual DbSet<Notifica> Notifiche { get; set; }
    }
}
