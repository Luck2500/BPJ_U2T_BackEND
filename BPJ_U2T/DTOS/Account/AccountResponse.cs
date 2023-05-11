using BPJ_U2T.Settings;

namespace BPJ_U2T.DTOS.Account
{
    public class AccountResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }

        static public AccountResponse FromAccount(Models.Account account)
        {

            return new AccountResponse
            {
                ID = account.ID,
                Name = account.Name,
                Image = !string.IsNullOrEmpty(account.Image) ? UrlServer.Url + "images/" + account.Image : "",
                Email = account.Email,
                Password = account.Password,
                PhoneNumber = account.PhoneNumber,
                Address = account.Address,
                RoleName = account.Role.Name
            };
        }
    }
}
