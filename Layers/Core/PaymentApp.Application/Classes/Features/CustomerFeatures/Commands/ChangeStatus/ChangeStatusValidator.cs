using FluentValidation;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Commons.Classes.Helpers.CommonObject;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ChangeStatus
{
    public class ChangeStatusValidator : AbstractValidator<ChangeStatusRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChangeStatusValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

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
                    .WithMessage("Номер карты должен содержать только целые цифры")
                .MustAsync(async (number, cancellation) =>
                {
                        var exist = await _unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>(rep => rep.GetByAccountNumberAsync(number, cancellation));

                        return exist != null;
                })
                    .WithMessage("Счета с таким номером карты не существует");

            RuleFor(x => x.IsActive)
                .NotNull()
                    .WithMessage("Действие не может быть пустым");

        }
    }
}
