using System.ComponentModel.DataAnnotations;

namespace GTEs_BE.Datas.ModelsInput
{
    public class ContattoInputModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public bool SosContact { get; set; } = false;
    }
}
