using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HangfireServer.ConsoleApp
{
    /// <summary>
    /// <see cref="JobActivator"/> implementation for Hangfire"/> to use the <see cref="IServiceProvider"/> to resolve job types.
    /// </summary>
    internal class HangfireActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public HangfireActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type jobType)
        {
            return _serviceProvider.GetRequiredService(jobType);
        }
    }
}