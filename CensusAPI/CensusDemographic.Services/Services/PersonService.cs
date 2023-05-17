using FluentValidation;
using CensusDemographic.Domain.Interfaces.Repositories;
using CensusDemographic.Domain.Interfaces.Services;
using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.FiltersVM;
using CensusDemographic.Domain.PagedList;
using Microsoft.Extensions.Logging;
using CensusDemographic.Domain.Filters;

namespace CensusDemographic.Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly ILogger _logger;
        private readonly IPersonRepository _personRepository;
        private readonly IValidator<Person> _validator;
        private FamilyTree _familyTree = default!;
            

        public PersonService(ILogger<PersonService> logger, IPersonRepository personRepository, IValidator<Person> validator) 
        {
            _logger = logger;
            _personRepository = personRepository;
            _validator = validator;
        }

        public async Task Add(Person person)
        {
            _logger.LogInformation("[{Method}] - Started", nameof(Add));
            await _validator.ValidateAndThrowAsync(person);
            await _personRepository.Add(person);
            _logger.LogInformation("[{Method}] - Finish", nameof(Add));
        }

        public async Task<List<Person>> GetAll() => await _personRepository.GetAll();
        
        public async Task<Person> GetById(Guid id) => await _personRepository.GetById(id);

        public async Task Remove(Guid id) => await _personRepository.Remove(id);

        public async Task Update(Person person)
        {
            await _validator.ValidateAndThrowAsync(person);
            await _personRepository.Update(person);
        }

        public async Task<List<Person>> GetByFilter(PersonFilterVM personVM)
        {
            var persons = await _personRepository.GetByFilter(personVM);
            return persons;
        }

        public async Task<double> GetPercentualByRegion(PersonFilterVM personVM)
        {
            _logger.LogInformation("[{Method}] - Started", nameof(GetPercentualByRegion));
            var filterRegion = await _personRepository.GetFilterByRegion(personVM);

            var filterBuilder = new FilterDomain<Person>()
            .Where(personVM.Name, p => p.Name.ToLower().Contains(personVM.Name!.ToLower()))
            .Where(personVM.MotherName, p => p.MotherName.ToLower().Contains(personVM.MotherName!.ToLower()))
            .Where(personVM.FatherName, p => p.FatherName.ToLower().Contains(personVM.FatherName!.ToLower()))
            .Where(personVM.Color, p => p.Color.Equals(personVM.Color))
            .Where(personVM.Schooling, p => p.Schooling.Equals(personVM.Schooling))
            .Build();

            var result = filterRegion.Where(filterBuilder).ToList();
            var percent = (double)result.Count / (double)filterRegion.Count * 100;
            _logger.LogInformation("[{Method}] - Finish", nameof(GetPercentualByRegion));
            return percent;
        }

        public async Task<FamilyTree> BuildTree(Guid personId, int level, List<FamilyTree> parents = default!)
        {
           
            var person = await GetById(personId);
            var motherTree = new FamilyTree();
            var fatherTree = new FamilyTree();
            
            if(_familyTree is null) _familyTree = new FamilyTree(person);

            if (level > 0)
            {
                var mother = await _personRepository.GetMotherByDocument(_familyTree.Person.DocumentMother);
                if (mother != null)
                {
                     motherTree = new FamilyTree(mother);
                    motherTree.Parents.Add(motherTree);
                    _familyTree.Parents.Add(motherTree);
                }

                var father = await _personRepository.GetFatherByDocument(_familyTree.Person.DocumentFather);
                if (father != null)
                {
                     fatherTree = new FamilyTree(father);
                    _familyTree.Parents.Add(fatherTree);
                }
            }

            if (!level.Equals(0)) await BuildTree(motherTree.Person.PersonId, level - 1);
            //if(!level.Equals(0)) await BuildTree(fatherTree.Person.PersonId, level - 1);


            return _familyTree;
        }

        public async Task<PagedList<Person>> GetByFilterPaged(PersonFilterVM personVM, int pageNumber, int pageSize)
        {
            var persons = await _personRepository.GetByFilter(personVM);
            return PagedList<Person>.Create(persons, pageNumber, pageSize);
        }

        public async Task<Person> GetPersonByName(string name) => await _personRepository.GetPersonByName(name);

    }
}
