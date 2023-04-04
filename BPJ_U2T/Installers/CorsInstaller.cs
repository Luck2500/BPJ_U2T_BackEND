namespace BPJ_U2T.Installers
{
    public class CorsInstaller : IInstallers
    {
        public static string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static string MyAllowAnyOrigins = "_myAllowAnyOrigins";
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            // policy.AllowAnyHeader()
            // .AllowAnyMethod()
            // .AllowCredentials()
            // .WithOrigins("http://localhost:3000");
        });
            });
        }
    }
}
