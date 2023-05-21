using System;
using System.Linq;
using Checkers.ComputerPlayer.DI;
using Checkers.ComputerPlayer.UseCases;
using Checkers.HumanPlayer.DI;
using Checkers.HumanPlayer.UseCases;
using Checkers.Rules.Extensions;
using Checkers.Rules.Rules;
using Microsoft.Extensions.DependencyInjection;

namespace Checkers.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCheckers(this IServiceCollection services)
        {
            services.AddRules();           
            services.AddComputerPlayer();
            services.AddHumanPlayer();
        }        
    }
}
