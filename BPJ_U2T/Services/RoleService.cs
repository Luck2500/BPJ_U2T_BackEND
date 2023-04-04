using BPJ_U2T.Interfaces;
using BPJ_U2T.Models;

namespace BPJ_U2T.Services
{
    public class RoleService : IRoleService
    {
        private readonly DatabaseContext databaseContext;

        public RoleService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Role> GetRoleByID(int Id)
        {
            return await databaseContext.Role.FindAsync(Id);
        }
    }
}
