using Shared;
using System;
using System.Threading.Tasks;

namespace HangfireServer.Jobs
{
    public class ProcessSomethingJob : IProcessSomethingJob
    {
        public Task ProcessSomething(ProcessSomethingCommand processSomethingCommand)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"Something processed. Message: {processSomethingCommand.Message}");
            });
        }
    }
}