namespace BPJ_U2T.Installers
{
    public class SwaggerInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
