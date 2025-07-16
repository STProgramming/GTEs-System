using GTEs_BE.Datas.Enums;
using System.ComponentModel.DataAnnotations;

namespace GTEs_BE.Datas.ModelsInput
{
    public class NotificaInputModel
    {
        [Required]
        public NotificationTopic Topic { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        public NotificationGravity Gravity { get; set; }
    }
}
