using Hangfire;
using Shared;
using System;

namespace HangfireClient.ConsoleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hangfire Client");
            Console.WriteLine("==========");
            Console.WriteLine();

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(@"Server=127.0.0.1,1435; Database=HangfireTest; User Id=sa; Password=yourStrong(!)Password; TrustServerCertificate=True");

            ScheduleProcessSomethingJob();
            ScheduleProcessSomethingJob();

            Console.ReadLine();
        }

        private static void ScheduleProcessSomethingJob()
        {
            var random = new Random();
            int randomNumber = random.Next(100, 1000);

            Console.WriteLine($"Scheduling Process Something Job: {randomNumber}");

            var jobId = BackgroundJob.Schedule<IProcessSomethingJob>(
                x => x.ProcessSomething(new ProcessSomethingCommand { Message = $"Something {randomNumber}" }),
                TimeSpan.FromSeconds(10));

            Console.WriteLine($"Process Something Job Scheduled with Job Id {jobId}. The payload number is: {randomNumber}");
        }
    }
}