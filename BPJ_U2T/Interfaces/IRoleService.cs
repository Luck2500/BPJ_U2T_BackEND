using BPJ_U2T.Models;

namespace BPJ_U2T.Interfaces
{
    public interface IRoleService
    {
        Task<Role> GetRoleByID(int Id);
    }
}
