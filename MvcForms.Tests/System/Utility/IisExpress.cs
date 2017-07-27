using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using FluentAssertions;

namespace MvcForms.Tests.System.Utility
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
            var coverageFile = Environment.GetEnvironmentVariable("COVERAGE");
            var iisExpressPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "IIS Express\\iisexpress.exe");
            var webPath = WebPath(childSearchFolder, siteFolder);

            if (!string.IsNullOrWhiteSpace(coverageFile))
                StartWithCoverage(iisExpressPath, webPath, coverageFile, port);
            else
                StartWithoutCoverage(iisExpressPath, webPath, port);
        }

        public static void AfterTests()
        {
            if (_process == null)
                return; // leave IISExpress running

            // send keydown 'Q' to window (which stops IIS Express)
            PostMessage(_process.MainWindowHandle, WM_KEYDOWN, (IntPtr)VkKeyScan('Q'), IntPtr.Zero);

            // wait for child OpenCover.Console to stop
            Wait.For(() => OpenCoverProcesses().Count.Should().Be(1, "there should be a single (parent) OpenCover.Console process"));

            _process = null;
        }

        private static void StartWithoutCoverage(string iisExpressPath, string webPath, int port)
        {
            if (IsRunning(port))
                return;

            var args = $"/path:{webPath} /Port:{port}";
            var exe = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "IIS Express\\iisexpress.exe");

            _process = Start(exe, args);

            Wait.For(() => IsRunning(port).Should().BeTrue("IIS Express not running"));
        }

        private static void StartWithCoverage(string iisExpressPath, string webPath, string coverageFile, int port)
        {
            if (IsRunning(port))
                KillRunningIisExpress();

            Wait.For(() => OpenCoverProcesses().Count.Should().Be(1, "there should be a single (parent) OpenCover.Console process"));

            var binPath = Path.Combine(webPath, "bin");
            var exe = Environment.GetEnvironmentVariable("COVERAGE_EXE");
            var args = $"-targetdir:\"{binPath}\" -target:\"{iisExpressPath}\" -targetargs:\"/path:{webPath} /Port:{port}\" -register:user -output:{coverageFile} -filter:\"+[*]*\"";

            _process = Start(exe, args);
            
            Wait.For(() => IsRunning(port).Should().BeTrue("IIS Express not running"));
        }

        private static Process Start(string exe, string args)
        {
            return Process.Start(new ProcessStartInfo()
            {
                FileName = exe,
                Arguments = args,
                CreateNoWindow = false,
                UseShellExecute = true,
            });
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
                .Where(p => p.ProcessName.ToLower().Contains("opencover.console"))
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
