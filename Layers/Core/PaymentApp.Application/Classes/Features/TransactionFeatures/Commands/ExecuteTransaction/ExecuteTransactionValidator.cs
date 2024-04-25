using FluentValidation;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Commons.Classes.Helpers.CommonObject;
using PaymentApp.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PaymentApp.Application.Classes.Features.TransactionFeatures.Commands.ExecuteTransaction
{
    public class ExecuteTransactionValidator : AbstractValidator<ExecuteTransactionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExecuteTransactionValidator(IUnitOfWork unitOfWork)
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            _unitOfWork = unitOfWork;

            RuleFor(x => x.SenderNumber)
                .NotNull()
                    .WithMessage("Номер карты отправителя не может быть пустым")
                .Length(16)
                    .WithMessage("Длина номера карты отправителя должна быть 16 цифр")
                .Must((number) =>
                {
                    return number.IsDigit();
                })
                    .WithMessage("Номер карты отправителя должен содержать только целые цифры")
                .MustAsync(async (number, cancellation) =>
                {
                    var exist = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>(rep => rep.GetByAccountNumberAsync(number, cancellation));

                    return exist != null;
                })
                    .WithMessage("Отправителя с таким номер карты не существует")
                .MustAsync(async (number, cancellation) =>
                {
                    var exist = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>(rep => rep.GetByAccountNumberAsync(number, cancellation));

                    return exist.IsActive;           
                })
                    .WithMessage("Счет отправителя заблокирован");

            RuleFor(x => x.RecipientNumber)
                .NotNull()
                    .WithMessage("Номер карты получателя не может быть пустым")
                .Length(16)
                    .WithMessage("Длина номера карты получателя должна быть 16 цифр")
                .Must((number) =>
                {
                    return number.IsDigit();
                })
                    .WithMessage("Номер карты получателя должен содержать только целые цифры")
                .MustAsync(async (number, cancellation) =>
                {
                    var exist = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>(rep => rep.GetByAccountNumberAsync(number, cancellation));

                    return exist != null;
                })
                    .WithMessage("Получателя с таким номер карты не существует")
                .MustAsync(async (number, cancellation) =>
                {
                    var exist = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>(rep => rep.GetByAccountNumberAsync(number, cancellation));

                    return exist.IsActive;
                })
                    .WithMessage("Счет получателя заблокирован");

            RuleFor(x => new { x.SenderNumber, x.RecipientNumber })
                .Must(x =>
                {
                    return !Equals(x.SenderNumber, x.RecipientNumber);

                }).WithMessage("Номера отправителя и получателя не могу быть одинаковыми");

            RuleFor(x => x.TransactionNumber)
                .NotNull()
                    .WithMessage("Номер транзакции не может быть пустым")
                .MaximumLength(20)
                    .WithMessage("Длина номера транзакции не может быть больше 20 символов")
                .Must((number) =>
                {
                    return number.IsDigit();

                })
                    .WithMessage("Номер транзакции должен содержать только целочисленные символы")
                .MustAsync(async (number, cancellation) =>
                {
                    var exist = await _unitOfWork.DoWork<ITransactionRepository, TransactionEntity, TransactionEntity>(rep => rep.GetByTransactionNumber(number, cancellation));

                    return exist == null;
                })
                    .WithMessage("Транзакция с таким номером уже существует");

            RuleFor(x => x.Sum)
                .NotNull()
                    .WithMessage("Сумма не может быть пустой")
                .Must((sum) =>
                {
                    return sum.ToRounded() == sum;
                })
                    .WithMessage("Сумма не может содержать больше 2-х знаков после запятой")
                .MustAsync(async (request, sum, cancellation) =>
                {
                    var exist = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>
                        (rep 
                            => rep.GetByAccountNumberAsync(request.SenderNumber, cancellation)
                        );

                    return exist.Balance >= sum;
                })
                .WithMessage("На балансе отправителя недостаточно средств");
        }
    }
}
