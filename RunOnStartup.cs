using System;
using System.Runtime.InteropServices;

namespace RunOnStartup
{
    public static class RunOnStartup
    {
        /// <summary>
        /// Returns an instance of a class that implements <see cref="IRunOnStartup"/> for the current platform.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException"/>
        public static IRunOnStartup Instance => _instance ??= CreateInstance();
        private static IRunOnStartup? _instance;

        private static IRunOnStartup CreateInstance()
        {
            if (OperatingSystem.IsWindows())
            {
                return new RunOnStartupWindows();
            }
            else if (OperatingSystem.IsLinux())
            {
                return new RunOnStartupLinux();
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }
    }
}
