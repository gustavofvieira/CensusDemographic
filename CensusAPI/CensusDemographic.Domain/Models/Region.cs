using CensusDemographic.Domain.Enums;

namespace CensusDemographic.Domain.Models
{
    public class Region
    {
        public Continent Continent { get; set; }
        public int ZipCode { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
