using System.ComponentModel.DataAnnotations;

namespace Labb2Web.Shared
{
    public class CreateUser
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Adress { get; set; }
    }
}
