namespace FM.API.Configurations
{
    public class Auth
    {
        public const string AuthSection = "Auth";
        public string ClientSecret { get; set; }
        public string ClientId { get; set; }
    }
}
