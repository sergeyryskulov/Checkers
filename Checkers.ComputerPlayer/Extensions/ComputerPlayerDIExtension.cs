﻿using Checkers.ComputerPlayer.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.ComputerPlayer.DI;

public static class ComputerPlayerDIExtension
{
    public static void AddComputerPlayer(this IServiceCollection services)
    {
        var assemblies = new[]
        {                
            typeof(ComputerCalculateNextStepUseCase).Assembly,                
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