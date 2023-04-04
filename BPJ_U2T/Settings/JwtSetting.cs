namespace BPJ_U2T.Settings
{
    public class JwtSetting
    {
        public string Key { get; set; } = "SecretKey@12345678";
        public string Issuer { get; set; } = "MrSutanat";
        public string Audience { get; set; } = "Sutanat Kampool";
        public string Expire { get; set; } = "1";
    }
}
