namespace FM.API.Configurations
{
    /// <summary>
    /// Información de autenticación basada en la sección "Auth" dentro del appsettings.json
    /// </summary>
    public class Auth
    {
        public const string AuthSection = "Auth";
        public string ClientSecret { get; set; }
        public string ClientId { get; set; }
    }
}
