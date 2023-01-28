using AutoMapper;
using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common.Infrastructure;
using Dictionary.Common.Infrastructure.Exceptions;
using Dictionary.Common.Models.QueriesModels;
using Dictionary.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dictionary.Api.Application.Features.Commands.User
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public LoginUserCommandHandler(IConfiguration configuration, IMapper mapper, IUserRepository userRepository)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetSingleAsync(s => s.EmailAddress == request.EmailAddress);

            if (user == null)
                throw new DatabaseValidationException("User not found");

            var pass = PasswordEncryptor.Encyrpt(request.Password);
            if (user.Password != pass)
                throw new DatabaseValidationException("Password is wrong");

            if (!user.EmailConfirmed)
                throw new DatabaseValidationException("Email address is not confirmed");

            var result = mapper.Map<LoginUserViewModel>(user);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName)
            };

            result.Token = GenerateToken(claims);

            return result;  
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims,
                                              expires: expire,
                                              signingCredentials: credential,
                                              notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
