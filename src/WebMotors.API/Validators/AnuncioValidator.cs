using FluentValidation;
using WebMotors.Domain.Models;

namespace WebMotors.API.Validators
{
    public class AnuncioValidator : AbstractValidator<AnuncioRequest>
    {
        public AnuncioValidator()
        {
            RuleFor(anuncio => anuncio.Marca)
                  .NotNull()
                  .WithMessage("{PropertyName} campo obrigatório.")
                  .NotEmpty()
                  .WithMessage("{PropertyName} campo obrigatório.");

            RuleFor(anuncio => anuncio.Versao)
                 .NotNull()
                 .WithMessage("{PropertyName} campo obrigatório.")
                 .NotEmpty()
                 .WithMessage("{PropertyName} campo obrigatório.");

            RuleFor(anuncio => anuncio.Modelo)
                .NotNull()
                .WithMessage("{PropertyName} campo obrigatório.")
                .NotEmpty()
                .WithMessage("{PropertyName} campo obrigatório.");

            RuleFor(anuncio => anuncio.Observacao)
               .NotNull()
               .WithMessage("{PropertyName} campo obrigatório.")
               .NotEmpty()
               .WithMessage("{PropertyName} campo obrigatório.");

            RuleFor(storage => storage.Ano)
                .GreaterThan(0)
                .WithMessage("{PropertyName} campo deve ser maior que 0.");

            RuleFor(storage => storage.Quilometragem)
               .GreaterThan(0)
               .WithMessage("{PropertyName} campo deve ser maior que 0.");

        }
    }
}
