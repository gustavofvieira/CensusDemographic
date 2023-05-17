namespace CensusDemographic.Domain.Models
{
    public class FamilyTree
    {
        public Person Person { get; set; }
        public List<FamilyTree> Parents { get; set; } = new List<FamilyTree>();

        public FamilyTree()
        {
        }
        public FamilyTree(Person person)
        {
            Person = person;
        }
    }
}
