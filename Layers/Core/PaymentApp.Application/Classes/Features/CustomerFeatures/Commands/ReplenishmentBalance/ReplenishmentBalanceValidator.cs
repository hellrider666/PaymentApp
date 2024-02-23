using FluentValidation;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Commons.Classes.Helpers.CommonObject;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ReplenishmentBalance
{
    public class ReplenishmentBalanceValidator : AbstractValidator<ReplenishmentBalanceRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReplenishmentBalanceValidator(IUnitOfWork unitOfWork)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            _unitOfWork = unitOfWork;

            RuleFor(x => x.AccountNumber)
                .NotNull()
                    .WithMessage("Номер карты не может быть пустым")
                .Length(16)
                    .WithMessage("Длина номера карты должна быть 16 цифр")
                .Must((number) =>
                {
                    return number.IsDigit();
                })
                    .WithMessage("Номер карты должен содержать только целые цифры")
                .MustAsync(async (number, cancellation) =>
                {
                    var exist = await _unitOfWork.CustomerRepository.GetByAccountNumberAsync(number, cancellation);

                    return exist != null;
                })
                    .WithMessage("Счета с таким номер карты не существует")
                .MustAsync(async (number, cancellation) =>
                {
                    var customer = await _unitOfWork.CustomerRepository.GetByAccountNumberAsync(number, cancellation);

                    return customer.IsActive;
                })
                    .WithMessage("Счет заблокирован");

            RuleFor(x => x.Sum)
                .NotNull()
                    .WithMessage("Сумма не может быть пустой")
                .Must((sum) =>
                {
                    return sum.ToRounded() == sum;
                })
                    .WithMessage("Сумма не может содержать больше 2-х знаков после запятой");
        }
    }
}
