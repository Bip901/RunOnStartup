using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunOnStartup
{
    public interface IRunOnStartupManager
    {
        /// <summary>
        /// Checks whether the given program was registered to run on startup.
        /// </summary>
        /// <param name="uniqueName">The unique name used at the time of registration. Must be a valid file name on all platforms - do not include slashes or other file-special characters.</param>
        /// <param name="allUsers">Check if it was registered for all users or for the current user only.</param>
        bool IsRegistered(string uniqueName, bool allUsers = false);

        /// <summary>
        /// Registers the given program to run when the user logs in, or updates an existing registered program.
        /// </summary>
        /// <param name="uniqueName">A unique name for this registration. Must be a valid file name on all platforms - do not include slashes or other file-special characters.</param>
        /// <param name="programPath">The path of the native program to run.</param>
        /// <param name="arguments">The shell-escaped arguments to pass to the program.</param>
        /// <param name="allUsers">Whether to register for all users or for the current user only. Passing "true" requires running this function with high privileges.</param>
        /// <exception cref="Exception"/>
        void Register(string uniqueName, string programPath, string? arguments = null, bool allUsers = false);

        /// <summary>
        /// Unregisters a program which was registered with <see cref="Register(string, string, string, bool)"/>.
        /// </summary>
        /// <param name="uniqueName">The unique name used at the time of registration. Must be a valid file name on all platforms - do not include slashes or other file-special characters.</param>
        /// <param name="allUsers">Whether it was registered for all users or for the current user only. Passing "true" requires running this function with high privileges.</param>
        /// <returns>Whether the given program was registered before.</returns>
        /// <exception cref="Exception"/>
        bool Unregister(string uniqueName, bool allUsers = false);
    }
}