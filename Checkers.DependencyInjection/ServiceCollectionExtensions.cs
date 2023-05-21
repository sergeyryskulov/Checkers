using System;
using System.Linq;
using Checkers.ComputerPlayer.DI;
using Checkers.ComputerPlayer.UseCases;
using Checkers.HumanPlayer.UseCases;
using Checkers.Rules.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace Checkers.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCheckers(this IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(HumanTryMoveFigureUseCase).Assembly,                
                typeof(ValidateRule).Assembly,
            };

            foreach (var assembly in assemblies)
            {
                var classEndNames =  new []{ "Service", "UseCase", "Rule" };
                
                foreach (var type in assembly.GetTypes().Where(t => classEndNames.Any(endName=> t.Name.EndsWith(endName))  && !t.IsInterface))
                {
                    services.AddTransient(type);

                    foreach (var typeInterface in type.GetInterfaces().Where(t => classEndNames.Any(endName => t.Name.EndsWith(endName))))
                    {
                        services.AddTransient(typeInterface, type);
                    }
                }
            }

            services.AddComputerPlayer();
        }

        
    }
}
