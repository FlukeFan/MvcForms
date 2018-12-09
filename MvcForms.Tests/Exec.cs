using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace MvcForms.Tests
{
    public static class Exec
    {
        public static void Cmd(string fileName, string arguments, string workingDirectory)
        {
            using (Process process = new Process())
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = workingDirectory;

                TestContext.Progress.WriteLine($"Running {process.StartInfo.FileName} {process.StartInfo.Arguments} (in {process.StartInfo.WorkingDirectory})");

                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, eventArgs) =>
                    {
                        if (eventArgs.Data == null)
                            outputWaitHandle.Set();
                        else
                            TestContext.Progress.WriteLine($"{eventArgs.Data}");
                    };

                    process.ErrorDataReceived += (sender, eventArgs) =>
                    {
                        if (eventArgs.Data == null)
                            outputWaitHandle.Set();
                        else
                            TestContext.Progress.WriteLine($"ERROR: {eventArgs.Data}");
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();

                    TestContext.Progress.WriteLine($"Process exited with code {process.ExitCode}");
                    process.ExitCode.Should().Be(0);
                }
            }
        }
    }
}
