using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace API.Base.Service
{
    using TRequest = ExampleServiceRequest;
    using TResponse = String;

    public class ExampleServiceRequest : IRequest<TResponse>
    {
        public Guid Id { get; set; }
    }

    public class ExampleServiceRequestValidator : AbstractValidator<TRequest>
    {
        public ExampleServiceRequestValidator()
        {
            RuleFor(p => p.Id).Must(p => p != default).WithMessage($"{nameof(TRequest.Id)} parameter not valid");
        }
    }

    public class ExampleServiceRequestHandler : IRequestHandler<TRequest, TResponse>
    {
        public async Task<string> Handle(ExampleServiceRequest request, CancellationToken cancellationToken)
        {
            return $"Example Service Request: {request.Id}";
        }
    }
}