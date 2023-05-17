using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.FiltersVM;
using CensusDemographic.Domain.PagedList;

namespace CensusDemographic.Domain.Interfaces.Services
{
    public interface IPersonService : IBaseService<Person>
    {
        Task<double> GetPercentualByRegion(PersonFilterVM personVM);
        Task<List<Person>> GetByFilter(PersonFilterVM personVM);
        Task<PagedList<Person>> GetByFilterPaged(PersonFilterVM personVM, int pageNumber, int pageSize);
        Task<FamilyTree> BuildTree (Guid personId, int level, List<FamilyTree> parents = default!);
    }
}
