using System.ComponentModel.DataAnnotations;

namespace BPJ_U2T.DTOS.Account
{
    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public string? Address { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
        public int RoleID { get; set; }
    }
}
