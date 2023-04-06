using Application.CQRS.Queries.Users;
using Contracts.Views.User;
using MediatR.Pipeline;
using Services.Abstractions;

namespace Application.CQRS.Pipelines.Users
{
    public class SignInUserQueryPostProcessor : IRequestPostProcessor<SignInUserQuery, UserAuthorizationView>
    {
        private readonly ITfaService _tfaService;

        public SignInUserQueryPostProcessor
        (
            ITfaService tfaService
        )
        {
            _tfaService = tfaService ?? throw new ArgumentNullException(nameof(tfaService));
        }

        public async Task Process(SignInUserQuery request, UserAuthorizationView response, CancellationToken cancellationToken)
        {
            if (response.IsTfaEnabled && response.IsAuthSuccessful)
            {
                await _tfaService.StartAuthenticationAsync(request.Email);
            }
        }
    }
}
