using FluentValidation;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Commons.Classes.Helpers.CommonObject;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.CreateCustomer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Name)
                .NotNull()
                    .WithMessage("Имя не может быть пустым")
                .MaximumLength(128)
                    .WithMessage("Длина имени не может быть больше 128 символов");

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

                       return exist == null;
                   
                })
                    .WithMessage("Счет с таким номер карты уже существует");

        }
    }
}
