using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;

namespace RunOnStartup
{
    [SupportedOSPlatform("windows")]
    internal class RunOnStartupWindows : IRunOnStartup
	{
		private const string PARTIAL_KEY_PATH = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
		private const string ALL_USERS_KEY_PATH = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";
		private const string CURRENT_USER_KEY_PATH = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run";

		private static string GetKeyPath(bool allUsers)
		{
			return allUsers ? ALL_USERS_KEY_PATH : CURRENT_USER_KEY_PATH;
		}

		public bool IsRegistered(string uniqueName, bool allUsers)
		{
			return Registry.GetValue(GetKeyPath(allUsers), uniqueName, null) != null;
		}

		public void Register(string uniqueName, string programPath, string? arguments, bool allUsers)
		{
			Registry.SetValue(GetKeyPath(allUsers), uniqueName, $"\"{programPath}\" {arguments}");
		}

		public bool Unregister(string uniqueName, bool allUsers)
		{
			using RegistryKey? key = (allUsers ? Registry.LocalMachine : Registry.CurrentUser).OpenSubKey(PARTIAL_KEY_PATH, true);
			if (key == null || key.GetValue(uniqueName) == null)
			{
				return false;
			}
			key.DeleteValue(uniqueName);
			return true;
		}
	}
}
