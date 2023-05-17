using Microsoft.AspNetCore.Mvc;
using CensusDemographic.Domain.Interfaces.Services;
using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.FiltersVM;
using Microsoft.AspNetCore.Authorization;

namespace CensusDemographic.WebApi.Controllers
{
    [Route("v1/Person")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
       private readonly IPersonService _personService;
        public PersonController(IPersonService personService) => _personService = personService;

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Person>>> GetAll()
        {
            var opportunities = await _personService.GetAll();
            return Ok(opportunities);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<Person>> GetById(Guid id)
        {
            var person = await _personService.GetById(id);
            return person is null ? NotFound("person not found!") : Ok(person);
        }

        [HttpPost]
        [Route("GetByFilter")]
        public async Task<ActionResult<List<Person>>> GetByFilter(PersonFilterVM personVM, int pageNumber, int pageSize)
        {
            var persons = await _personService.GetByFilterPaged(personVM, pageNumber, pageSize);
            return persons is null ? NotFound("Person not found!") : Ok(persons);
        }

        [HttpPost]
        [Route("GetPercentualByRegion")]
        public async Task<ActionResult<double>> GetPercentualByRegion(PersonFilterVM personVM)
        {
            var percentual = await _personService.GetPercentualByRegion(personVM);
            return Ok(percentual);
        }

        [HttpGet]
        [Route("GetFamilyTree")]
        public async Task<ActionResult<FamilyTree>> GetFamilyTree(Guid id, int level)
        {
            var persons = await _personService.BuildTree(id, level);
            return persons is null ? NotFound("Person not found!") : Ok(persons);
        }


        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create(Person person)
        {
            try
            {
                await _personService.Add(person);
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult> Update(Person person)
        {
            try
            {
                await _personService.Update(person);
                return Ok();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var person = await _personService.GetById(id);
            if (person is null)
                return NotFound("person not found!");
            await _personService.Remove(id);
            return Ok();
        }
    }
}
