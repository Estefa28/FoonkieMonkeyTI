namespace FM.API.Configurations
{
    /// <summary>
    /// Authentication information based on the "Auth" section within the appsettings.json
    /// </summary>
    public class Auth
    {
        public const string AuthSection = "Auth";
        public string ClientSecret { get; set; }
        public string ClientId { get; set; }
    }
}
