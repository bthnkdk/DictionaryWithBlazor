using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Infrastructure;
using Dictionary.Common.Infrastructure.Exceptions;
using Dictionary.Common.Models.CommandModels.User;
using MediatR;

namespace Dictionary.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
      
        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if(!request.UserId.HasValue)
                throw new ArgumentNullException(nameof(request.UserId));

            var dbUser = await userRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found");

            var encryptPass = PasswordEncryptor.Encyrpt(request.OldPassword);
            if(dbUser.Password != encryptPass)
                throw new DatabaseValidationException("Old password wrong!");

            dbUser.Password = PasswordEncryptor.Encyrpt(request.NewPassword);
            await userRepository.UpdateAsync(dbUser);

            return true;

        }
    }
}
