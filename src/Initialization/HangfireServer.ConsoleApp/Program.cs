using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireServer.ConsoleApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hangfire Server");
            Console.WriteLine("==========");
            Console.WriteLine();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IProcessSomethingJob, ProcessSomethingJob>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(@"Server=127.0.0.1,1435; Database=HangfireTest; User Id=sa; Password=yourStrong(!)Password; TrustServerCertificate=True");

            using var cancellationTokenSource = new CancellationTokenSource();
            var serverTask = StartBackgroundServer(serviceProvider, cancellationTokenSource.Token);

            Console.ReadLine();

            await StopBackgroundServer(cancellationTokenSource, serverTask);
        }

        #region Hangfire Server

        private static Task StartBackgroundServer(IServiceProvider serviceProvider, CancellationToken token)
        {
            return Task.Run(() =>
            {
                using var server = new BackgroundJobServer(new BackgroundJobServerOptions
                {
                    Activator = new HangfireActivator(serviceProvider)
                });

                Console.WriteLine("Background Job Server started.");

                // Keep the server running until the cancellation is requested
                while (!token.IsCancellationRequested)
                {
                    Thread.Sleep(100); // Prevents a tight loop
                }

                Console.WriteLine("Background Job Server stopped.");
            }, token);
        }

        [SuppressMessage("Reliability", "S6966:Awaitable method should be used", Justification = "Cancellation is fast and does not require async.")]
        private static async Task StopBackgroundServer(CancellationTokenSource cancellationTokenSource, Task serverTask)
        {
            cancellationTokenSource.Cancel();

            await serverTask;
        }

        #endregion Hangfire Server
    }
}