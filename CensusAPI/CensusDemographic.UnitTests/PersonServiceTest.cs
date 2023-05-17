using AutoFixture;
using CensusDemographic.Domain.Interfaces.Repositories;
using CensusDemographic.Domain.Models;
using CensusDemographic.Domain.Models.FiltersVM;
using CensusDemographic.Services.Services;
using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace CensusDemographic.UnitTests
{
    public class PersonServiceTest
    {

        private Mock<IValidator<Person>> _validator = default!;
        private Mock<IPersonRepository> _personRepository = default!;
        private Mock<ILogger<PersonService>> _logger = default!;
        private Fixture _fixture = default!;

        [SetUp]
        public void SetUp()
        {
            _validator = new();
            _logger = new();
            _personRepository = new();
            _fixture = new();
        }

        [Test]
        public async Task ShouldAddPersonWithSuccess()
        {
            //Arrange
            var service = new PersonService(_logger.Object, _personRepository.Object, _validator.Object);
            var person = _fixture.Build<Person>().Create();

            //Act

            await service.Add(person);

            //Assert
            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("[Add] - Started", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);

            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("[Add] - Finish", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);

        }

        [Test]
        public async Task ShouldGetPercentualByRegionWithSuccess()
        {
            //Arrange
            var service = new PersonService(_logger.Object, _personRepository.Object, _validator.Object);
            var personList = _fixture.Build<Person>().CreateMany().ToList();
            var personVM = _fixture.Build<PersonFilterVM>().Create();

            _personRepository.Setup(pr => pr.GetFilterByRegion(It.IsAny<PersonFilterVM>())).ReturnsAsync(personList);
            //Act

            var percentual = await service.GetPercentualByRegion(personVM);

            //Assert

            //percentual.Should().Equals(0);

            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("[GetPercentualByRegion] - Started", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);

            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("[GetPercentualByRegion] - Finish", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);

        }
    }
}