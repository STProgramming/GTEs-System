using GTEs_BE.Datas.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTEs_BE.Datas.ModelsEntity
{
    [Table("Notifiche")]
    public class Notifica
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public NotificationTopic Topic { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public NotificationGravity Gravity { get; set; }

        [Required]
        public bool Read { get; set; } = false;
    }
}
