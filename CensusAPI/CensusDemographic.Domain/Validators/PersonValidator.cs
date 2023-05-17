using FluentValidation;
using CensusDemographic.Domain.Models;

namespace CensusDemographic.Domain.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.MotherName)
                .NotNull()
                .WithMessage("Titulo da categoria deve ser preenchido")
                .Length(2, 50)
                .WithMessage("O titulo da categoria deve conter entre 2 e 50 caracteres");

            RuleFor(p => p.FatherName).
                NotNull();
        }
    }
}
