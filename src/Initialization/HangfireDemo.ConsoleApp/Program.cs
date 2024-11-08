using Hangfire;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireDemo.ConsoleApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hangfire Experiments");
            Console.WriteLine("==========");
            Console.WriteLine();

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(@"Server=127.0.0.1,1435; Database=HangfireTest; User Id=sa; Password=yourStrong(!)Password; TrustServerCertificate=True");

            CreateFireAndForgetJob();

            using var cancellationTokenSource = new CancellationTokenSource();
            var serverTask = StartBackgroundServer(cancellationTokenSource.Token);

            CreateFireAndForgetJob();

            Console.ReadLine();

            await StopBackgroundServer(cancellationTokenSource, serverTask);
        }

        private static void CreateFireAndForgetJob()
        {
            Console.WriteLine("Creating Fire and Forget Job");

            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire and Forget Job Executed"));

            Console.WriteLine($"Fire and Forget Job created with Job Id {jobId}");
        }

        #region Hangfire Server

        private static Task StartBackgroundServer(CancellationToken token)
        {
            return Task.Run(() =>
            {
                using var server = new BackgroundJobServer();
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