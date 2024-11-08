using Shared;
using System;
using System.Threading.Tasks;

namespace HangfireServer.ConsoleApp
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