using CensusDemographic.Domain.Enums;

namespace CensusDemographic.Domain.Models.FiltersVM
{
    public class PersonFilterVM
    {
        public string? Name { get; set; }
        public int? Document { get; set; }
        public int? DocumentFather { get; set; }
        public int? DocumentMother { get; set; }
        public Color? Color { get; set; }
        public string? MotherName { get; set; }
        public string? FatherName { get; set; }
        public Schooling? Schooling { get; set; }
        public RegionFilterVM? Region { get; set; } = new();
    }
}
