using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.FiltersVM;

namespace CensusDemographic.Domain.Interfaces.Repositories
{

    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<List<Person>> GetByFilter(PersonFilterVM personVM);
        Task<Person> GetPersonByName(string name);
        Task<List<Person>> GetFilterByRegion(PersonFilterVM personVM);
        Task<Person> GetFatherByDocument(long documentFather);
        Task<Person> GetMotherByDocument(long documentMother);
    }
}
