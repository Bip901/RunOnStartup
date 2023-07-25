using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunOnStartup
{
    internal class RunOnStartupManagerLinux : IRunOnStartupManager
    {
        private const string ALL_USERS_STARTUP_PATH = "/etc/xdg/autostart/";
        private const string CURRENT_USER_STARTUP_PATH = "%HOME%/.config/autostart/";
        private const string DESKTOP_FILE_EXTENSION = ".desktop";
        private const string DESKTOP_FILE_TEMPLATE =
            "[Desktop Entry]\n" +
            "Type=Application\n" +
            "Name={0}\n" +
            "Exec={1}\n" +
            "NoDisplay=true\n";

        private static string GetStartupFolderPath(bool allUsers)
        {
            return Environment.ExpandEnvironmentVariables(allUsers ? ALL_USERS_STARTUP_PATH : CURRENT_USER_STARTUP_PATH);
        }

        private static string GetDesktopFilePath(string startupFolderPath, string uniqueName)
        {
            return Path.Combine(startupFolderPath, uniqueName + DESKTOP_FILE_EXTENSION);
        }

        private static string BuildExecCommandLine(string programPath, string? escapedArguments)
        {
            string execCommandline = $"\"{programPath}\"";
            if (escapedArguments != null)
            {
                execCommandline += " " + escapedArguments;
            }
            return execCommandline;
        }

        public bool IsRegistered(string uniqueName, bool allUsers)
        {
            string startupFolderPath = GetStartupFolderPath(allUsers);
            return File.Exists(GetDesktopFilePath(startupFolderPath, uniqueName));
        }

        public void Register(string uniqueName, string programPath, string? arguments, bool allUsers)
        {
            string startupFolderPath = GetStartupFolderPath(allUsers);
            Directory.CreateDirectory(startupFolderPath);
            string desktopFilePath = GetDesktopFilePath(startupFolderPath, uniqueName);
            string execCommandline = BuildExecCommandLine(programPath, arguments);
            File.WriteAllText(desktopFilePath, string.Format(DESKTOP_FILE_TEMPLATE, uniqueName, execCommandline));
        }

        public bool Unregister(string uniqueName, bool allUsers)
        {
            string startupFolderPath = GetStartupFolderPath(allUsers);
            string desktopFilePath = GetDesktopFilePath(startupFolderPath, uniqueName);
            if (File.Exists(desktopFilePath))
            {
                File.Delete(desktopFilePath);
                return true;
            }
            return false;
        }
    }
}
