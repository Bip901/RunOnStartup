using System;

namespace RunOnStartup
{
    /// <summary>
    /// A utility class to get the platform-specific singleton <see cref="IRunOnStartupManager"/>.
    /// </summary>
    public static class RunOnStartupManager
    {
        /// <summary>
        /// Returns an instance of a class that implements <see cref="IRunOnStartupManager"/> for the current platform.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException"/>
        public static IRunOnStartupManager Instance => _instance ??= CreateInstance();
        private static IRunOnStartupManager? _instance;

        private static IRunOnStartupManager CreateInstance()
        {
            if (OperatingSystem.IsWindows())
            {
                return new RunOnStartupManagerWindows();
            }
            else if (OperatingSystem.IsLinux())
            {
                return new RunOnStartupManagerLinux();
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }
    }
}
