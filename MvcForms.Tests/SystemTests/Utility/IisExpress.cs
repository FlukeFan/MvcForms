using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using FluentAssertions;

namespace MvcForms.Tests.SystemTests.Utility
{
    public static class IisExpress
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        private const int WM_KEYDOWN = 0x100;

        private static Process _process;

        public static void BeforeTests(string childSearchFolder, string siteFolder, int port)
        {
            var runningWithCoverage = OpenCoverProcesses().Count > 0;

            if (IsRunning(port))
            {
                // IISExpress already started on this port (presumably by the IDE)

                if (runningWithCoverage)
                    KillRunningIisExpress(); // we need to own the child process in order to acheive coverage
                else
                    return; // IISExpress already running, and we're not measuring coverage - nothing else to do
            }

            var exe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "IIS Express\\iisexpress.exe");
            var webPath = WebPath(childSearchFolder, siteFolder);
            var args = $"/path:{webPath} /Port:{port}";

            _process = Process.Start(new ProcessStartInfo()
            {
                FileName = exe,
                Arguments = args,
                CreateNoWindow = false,
                UseShellExecute = true,
            });

            Wait.For(() => IsRunning(port).Should().BeTrue("IIS Express not running"));
        }

        public static void AfterTests()
        {
            if (_process == null)
                return; // leave IISExpress running

            // send keydown 'Q' to window (which stops IIS Express)
            PostMessage(_process.MainWindowHandle, WM_KEYDOWN, (IntPtr)VkKeyScan('Q'), IntPtr.Zero);

            Wait.For(() => _process.HasExited.Should().BeTrue("IISExpress has not exited"));
            _process = null;
        }

        private static void KillRunningIisExpress()
        {
            Wait.For(() =>
            {
                var processes = IisExpressProcesses();

                foreach (var process in processes)
                    process.Kill();

                processes.Count.Should().Be(0);
            });
        }

        private static IList<Process> IisExpressProcesses()
        {
            var processes = Process.GetProcesses()
                .Where(p => p.ProcessName.ToLower().StartsWith("iisexpress"))
                .ToList();

            return processes;
        }

        private static IList<Process> OpenCoverProcesses()
        {
            var processes = Process.GetProcesses()
                .Where(p => p.ProcessName.ToLower().StartsWith("opencover"))
                .ToList();

            return processes;
        }

        private static string WebPath(string searchFolder, string siteFolder)
        {
            var searches = new List<string>();
            var dir = searchFolder;
            var path = Path.GetFullPath(Path.Combine(dir, siteFolder));

            while (!File.Exists(Path.Combine(path, "web.config")))
            {
                if (searches.Contains(path))
                    throw new Exception($"Could not find {siteFolder} starting from {searchFolder} - searched {string.Join(", ", searches)}");

                searches.Add(path);
                dir = Path.Combine(dir, "..");
                path = Path.GetFullPath(Path.Combine(dir, siteFolder));
            }

            return path;
        }

        private static bool IsRunning(int port)
        {
            try
            {
                using (var tcpClient = new TcpClient())
                    tcpClient.Connect("localhost", port);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
