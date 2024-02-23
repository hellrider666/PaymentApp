using FluentValidation;
using PaymentApp.Commons.Classes.Helpers.CommonObject;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Queries.GetCustomerByNumber
{
    public class GetCustomerByNumberValidator : AbstractValidator<GetCustomerByNumberRequest>
    {
        public GetCustomerByNumberValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.AccountNumber)
                .NotNull()
                    .WithMessage("Номер карты не может быть пустым")
                .Length(16)
                    .WithMessage("Длина номера карты должна быть 16 цифр")
                .Must((number) =>
                {
                    return number.IsDigit();
                })
                    .WithMessage("Номер карты должен содержать только целые цифры");
        }
    }
}
