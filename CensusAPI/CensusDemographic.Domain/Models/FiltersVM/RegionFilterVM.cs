using CensusDemographic.Domain.Enums;

namespace CensusDemographic.Domain.Models.FiltersVM
{
    public class RegionFilterVM
    {
        public Continent? Continent { get; set; }
        public int? ZipCode { get; set; }
        public string? Street { get; set; }
        public string? Neighborhood { get; set; } 
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
    }
}
