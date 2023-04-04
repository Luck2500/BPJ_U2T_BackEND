using BPJ_U2T.Models;

namespace BPJ_U2T.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAll();
        Task<Account> Login(string email, string password);
        Task<object> Register(Account account);
        Task<Account> GetByID(int id);
        string GenerateToken(Account account);
        Account GetInfo(string accessToken);
        Task Update(Account account);
        Task Delete(Account account);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
    }
}
