using System.ComponentModel.DataAnnotations;

namespace BPJ_U2T.DTOS.Account
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
