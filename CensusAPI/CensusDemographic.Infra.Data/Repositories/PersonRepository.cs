using MongoDB.Driver;
using MongoDB.Driver.Linq;
using CensusDemographic.Domain.Interfaces.Repositories;
using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.FiltersVM;
using CensusDemographic.Infra.Data.Context;
using CensusDemographic.Infra.Data.Repositories.Filters;

namespace CensusDemographic.Infra.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly CensusContext _context;

        public PersonRepository(CensusContext context) => _context = context;

        public async Task Add(Person person) => await _context.Persons.InsertOneAsync(person);

        public async Task<List<Person>> GetAll() => await _context.Persons.AsQueryable().ToListAsync();

        public async Task<Person> GetById(Guid id) => await _context.Persons.AsQueryable().FirstOrDefaultAsync(p => p.PersonId.Equals(id));

        public async Task Remove(Guid id) => await _context.Persons.DeleteOneAsync(p => p.PersonId.Equals(id));


        public async Task<List<Person>> GetFilterByRegion(PersonFilterVM personVM)
        {
            var filterBuilder = new FilterBuilder<Person>()
           .Where(personVM.Region.ZipCode, p => p.Region.ZipCode.Equals(personVM.Region.ZipCode))
           .Where(personVM.Region.Street, p => p.Region.Street.ToLower().Contains(personVM.Region.Street.ToLower()))
           .Where(personVM.Region.Neighborhood, p => p.Region.Neighborhood.ToLower().Contains(personVM.Region.Neighborhood.ToLower()))
           .Where(personVM.Region.City, p => p.Region.City.ToLower().Contains(personVM.Region.City.ToLower()))
           .Where(personVM.Region.State, p => p.Region.State.ToLower().Contains(personVM.Region.State.ToLower()))
           .Where(personVM.Region.Country, p => p.Region.Country.ToLower().Contains(personVM.Region.Country.ToLower()));
            var filter = filterBuilder.Build();

            var result = await _context.Persons.Find(filter).ToListAsync();
            return result;
        }

        public async Task<List<Person>> GetByFilter(PersonFilterVM personVM)
        {
            var filterBuilder = new FilterBuilder<Person>()
            .Where(personVM.Name, p => p.Name.ToLower().Contains(personVM.Name.ToLower()))
            .Where(personVM.Document, p => p.Document.Equals(personVM.Document))
            .Where(personVM.DocumentFather, p => p.DocumentFather.Equals(personVM.DocumentFather))
            .Where(personVM.DocumentMother, p => p.DocumentMother.Equals(personVM.DocumentMother))
            .Where(personVM.Color, p => p.Color.Equals(personVM.Color))
            .Where(personVM.MotherName, p => p.MotherName.ToLower().Contains(personVM.MotherName.ToLower()))
            .Where(personVM.FatherName, p => p.FatherName.ToLower().Contains(personVM.FatherName.ToLower()))
            .Where(personVM.Schooling, p => p.Schooling.Equals(personVM.Schooling))
            .Where(personVM.Region.ZipCode, p => p.Region.ZipCode.Equals(personVM.Region.ZipCode))
            .Where(personVM.Region.Street, p => p.Region.Street.ToLower().Contains(personVM.Region.Street.ToLower()))
            .Where(personVM.Region.Neighborhood, p => p.Region.Neighborhood.ToLower().Contains(personVM.Region.Neighborhood.ToLower()))
            .Where(personVM.Region.City, p => p.Region.City.ToLower().Contains(personVM.Region.City.ToLower()))
            .Where(personVM.Region.State, p => p.Region.State.ToLower().Contains(personVM.Region.State.ToLower()))
            .Where(personVM.Region.Country, p => p.Region.Country.ToLower().Contains(personVM.Region.Country.ToLower()));
            var filter = filterBuilder.Build();
            
            if(filter is not null) return await _context.Persons.Find(filter).ToListAsync();

            return await GetAll();
        }

        public async Task Update(Person person) =>
            await _context.Persons.FindOneAndUpdateAsync(
                p => p.PersonId.Equals(person.PersonId),
                Builders<Person>.Update.Combine(
                    Builders<Person>.Update.Set(p => p.Name, person.Name),
                    Builders<Person>.Update.Set(p => p.Document, person.Document),
                    Builders<Person>.Update.Set(p => p.DocumentFather, person.DocumentFather),
                    Builders<Person>.Update.Set(p => p.DocumentMother, person.DocumentMother),
                    Builders<Person>.Update.Set(p => p.Color, person.Color),
                    Builders<Person>.Update.Set(p => p.MotherName, person.MotherName),
                    Builders<Person>.Update.Set(p => p.FatherName, person.FatherName),
                    Builders<Person>.Update.Set(p => p.Schooling, person.Schooling),
                    Builders<Person>.Update.Set(p => p.Region.ZipCode, person.Region.ZipCode),
                    Builders<Person>.Update.Set(p => p.Region.Street, person.Region.Street),
                    Builders<Person>.Update.Set(p => p.Region.Neighborhood, person.Region.Neighborhood),
                    Builders<Person>.Update.Set(p => p.Region.City, person.Region.City),
                    Builders<Person>.Update.Set(p => p.Region.State, person.Region.State),
                    Builders<Person>.Update.Set(p => p.Region.Country, person.Region.Country)
                ));

        public async Task<Person> GetPersonByName(string name) => await _context.Persons.AsQueryable().FirstOrDefaultAsync(p => p.Name.Equals(name));
        public async Task<Person> GetMotherByDocument(long documentMother) => await _context.Persons.AsQueryable().FirstOrDefaultAsync(p => p.Document.Equals(documentMother));
        public async Task<Person> GetFatherByDocument(long documentFather) => await _context.Persons.AsQueryable().FirstOrDefaultAsync(p => p.Document.Equals(documentFather));
    }
}
