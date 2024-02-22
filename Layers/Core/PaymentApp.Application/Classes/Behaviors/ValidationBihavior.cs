using FluentValidation;
using MediatR;

namespace PaymentApp.Application.Classes.Behaviors
{
    public class ValidationBihavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>

    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBihavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.ValidateAsync(context))
                    .SelectMany(result => result.Result.Errors)
                    .Where(failure => failure != null)
                    .ToList();

            if (failures.Any())
            {
                throw new Exception(failures.FirstOrDefault().ErrorMessage);
            }

            return await next();
        }

    }
}
