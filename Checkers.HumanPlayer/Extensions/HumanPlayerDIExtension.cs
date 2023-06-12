using Checkers.HumanPlayer.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.HumanPlayer.DI;

public static class HumanPlayerDIExtension
{
    public static void AddHumanPlayer(this IServiceCollection services)
    {
        var assemblies = new[]
        {
            typeof(HumanTryMoveFigureUseCase).Assembly,                
        };

        foreach (var assembly in assemblies)
        {
            var classEndNames = new[] { "Service", "UseCase", "Rule" };

            foreach (var type in assembly.GetTypes().Where(t => classEndNames.Any(endName => t.Name.EndsWith(endName)) && !t.IsInterface))
            {
                services.AddTransient(type);

                foreach (var typeInterface in type.GetInterfaces().Where(t => classEndNames.Any(endName => t.Name.EndsWith(endName))))
                {
                    services.AddTransient(typeInterface, type);
                }
            }
        }            
    }
}