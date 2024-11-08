using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared;

namespace HangfireServer.Jobs
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHangfire(configuration =>
            configuration
                .UseSqlServerStorage(@"Server=127.0.0.1,1435; Database=HangfireTest; User Id=sa; Password=yourStrong(!)Password; TrustServerCertificate=True"));

            builder.Services.AddTransient<IProcessSomethingJob, ProcessSomethingJob>();
            builder.Services.AddHangfireServer();

            var app = builder.Build();

            // Optional: Hangfire Dashboard. Not necessary for job execution.
            app.UseHangfireDashboard();

            app.Run();
        }
    }
}