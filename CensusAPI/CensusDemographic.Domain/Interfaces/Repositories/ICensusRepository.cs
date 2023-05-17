using CensusDemographic.Domain.Models;

namespace CensusDemographic.Domain.Interfaces.Repositories
{
    public interface ICensusRepository : IBaseRepository<Models.Census>
    {
        List<Person> GetByFilter();  
    }
}
