using System.Threading.Tasks;
using Xunit;
using System;

namespace ProcessExecutor
{
    public class ProcessExecutorTest
    {
        
        [Fact]
        public async Task TestConsoleOutput()
        {
            var response = await ProcessExecutor.Execute("dotnet", "--version");
            Console.WriteLine(response);
            response = response.Trim();
            Assert.Equal("1.0.0-preview2-003121", response); // sdk preview 2
        }
    } 
}