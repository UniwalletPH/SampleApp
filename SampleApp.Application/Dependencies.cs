using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application.Common;
using SampleApp.Application.Common.Behaviours;
using System;
using System.Reflection;

namespace SampleApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, bool includeValidators = false)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            if (includeValidators)
            {
                services.AddValidators(Assembly.GetExecutingAssembly());
            }

            return services;
        }
    }
}
