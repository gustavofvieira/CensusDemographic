namespace CensusDemographic.Domain.Models.Auth
{
    public sealed class SettingsOptions
    {
        public string Secret { get; set; } = string.Empty;
        public int ExpireToken { get; set; }
    }
}
