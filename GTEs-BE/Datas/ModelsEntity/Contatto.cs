using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTEs_BE.Datas.ModelsEntity
{
    [Table("Contatti")]
    public class Contatto
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public bool Passenger { get; set; } = false;

        public bool SosContact { get; set; } = false;

        public bool CarOwner { get; set; } = false;
    }
}
