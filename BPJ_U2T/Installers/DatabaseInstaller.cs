using Microsoft.EntityFrameworkCore;
using BPJ_U2T.Models;
using BPJ_U2T.Installers;

namespace BPJ_U2T.Installers
{
    public class DatabaseInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {

            var connectionString = builder.Configuration.GetConnectionString("BPJU2T65");
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(connectionString)
            );

        }
    }
}
