using System;
using System.Linq;
using Checkers.ComputerPlayer.Services;
using Checkers.HumanPlayer.Services;
using Checkers.Rules.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Checkers.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCheckers(this IServiceCollection services)
        {
            var assemblies = new[]
            {
                typeof(HumanPlayerService).Assembly,
                typeof(ComputerPlayerService).Assembly,
                typeof(ValidateRulesService).Assembly,
            };

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes().Where(t => t.Name.EndsWith("Service") && !t.IsInterface))
                {
                    services.AddTransient(type);

                    foreach (var typeInterface in type.GetInterfaces().Where(t => t.Name.StartsWith("I") && t.Name.EndsWith("Service")))
                    {
                        services.AddTransient(typeInterface, type);
                    }
                }
            }
        }

        
    }
}
