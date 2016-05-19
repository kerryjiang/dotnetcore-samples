using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace ProcessExecutor
{
    public static class ProcessExecutor
    {
        private static async Task ReadOutput(StreamReader reader, StringBuilder output, CancellationToken cancelToken)
        {
            while (!cancelToken.IsCancellationRequested)
            {
                output.AppendLine(await reader.ReadLineAsync());
            }
        }
        
        public static async Task<string> Execute(string program, string arguments)
        {
            var startInfo = new ProcessStartInfo();
            
            startInfo.FileName = program;
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardOutput = true;
            
            var taskCompleteSrc = new TaskCompletionSource<string>();
            
            Process process;
            
            try
            {
                process = Process.Start(startInfo);
                process.EnableRaisingEvents = true;
            }
            catch (System.Exception e)
            {
                taskCompleteSrc.SetException(e);
                return await taskCompleteSrc.Task;
            }
            
            var outputBuilder = new StringBuilder();
            
            var cancelTokenSource = new CancellationTokenSource();

            var readOutputTask = ReadOutput(process.StandardOutput, outputBuilder, cancelTokenSource.Token);
            
            process.Exited += (p, e) => {
                cancelTokenSource.Cancel();
                readOutputTask.Wait();
                taskCompleteSrc.SetResult(outputBuilder.ToString());
            };
            
            return await taskCompleteSrc.Task;
        }
    } 
}