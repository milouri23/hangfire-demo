using System.Threading.Tasks;

namespace Shared
{
    public interface IProcessSomethingJob
    {
        public Task ProcessSomething(ProcessSomethingCommand processSomethingCommand);
    }
}