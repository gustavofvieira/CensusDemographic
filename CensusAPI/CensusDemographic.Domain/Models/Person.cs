using MongoDB.Bson.Serialization.Attributes;
using CensusDemographic.Domain.Enums;

namespace CensusDemographic.Domain.Models
{
    public class Person 
    {

        [BsonId]
        public Guid PersonId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public long Document { get; set; }
        public long DocumentFather { get; set; }
        public long DocumentMother { get; set; }
        public Color Color { get; set; }
        public string MotherName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public Schooling Schooling { get; set; }
        public Region Region { get; set; } = new ();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}
