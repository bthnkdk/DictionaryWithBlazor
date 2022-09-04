using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dictionary.Api.Application.Extension
{
    public static class Registration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            var assmbly = Assembly.GetExecutingAssembly();

            services.AddMediatR(assmbly);
            services.AddAutoMapper(assmbly);
            services.AddValidatorsFromAssembly(assmbly);

            return services;
        }
    }
}
