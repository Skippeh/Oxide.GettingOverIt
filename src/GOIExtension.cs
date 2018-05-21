using System;
using System.IO;
using System.Reflection;
using System.Text;
using Oxide.Core;
using Oxide.Core.Extensions;
using Oxide.GettingOverIt.Loggers;

namespace Oxide.GettingOverIt
{
    public class GOIExtension : Extension
    {
        internal static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        internal static readonly AssemblyName AssemblyName = Assembly.GetName();
        internal static readonly VersionNumber AssemblyVersion = new VersionNumber(AssemblyName.Version.Major, AssemblyName.Version.Minor, AssemblyName.Version.Build);
        internal static readonly string AssemblyAuthors = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCompanyAttribute), false)).Company;

        private bool consoleAttached;

        public GOIExtension(ExtensionManager manager) : base(manager)
        {
        }

        public override string Name => "Getting Over It with Bennett Foddy";
        public override string Author => AssemblyAuthors;
        public override VersionNumber Version => AssemblyVersion;

        public override string[] WhitelistAssemblies => new string[]
        {
            "Assembly-CSharp", "Assembly-CSharp-firstpass", "mscorlib", "Oxide.Core", "System", "System.Core", "UnityEngine", "UnityEngine.UI"
        };

        public override string[] WhitelistNamespaces => new string[]
        {
            "Steamworks", "System.Collections", "System.Security.Cryptography", "System.Text", "UnityEngine"
        };

        public override void Load() => Manager.RegisterPluginLoader(new GOIPluginLoader());

        public override void OnModLoad()
        {
            if (Interface.Oxide.CheckConsole())
            {
                if (Environment.OSVersion.Platform != PlatformID.MacOSX && Environment.OSVersion.Platform != PlatformID.Unix)
                {
                    try
                    {
                        if (WinNative.AllocConsole() != 0)
                        {
                            consoleAttached = true;
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                    catch
                    {
                        Interface.Oxide.LogError("Failed to AllocConsole on Windows platform.");
                    }
                }

                try
                {
                    var writer = new StreamWriter(Console.OpenStandardOutput(), Encoding.UTF8) {AutoFlush = true};
                    Console.SetOut(writer);
                    Interface.Oxide.RootLogger.AddLogger(new ConsoleLogger());
                }
                catch (Exception ex)
                {
                    Interface.Oxide.LogError("Failed to initialize console: " + ex);
                }
            }
        }

        public override void OnShutdown()
        {
            if (consoleAttached)
            {
                WinNative.FreeConsole();
            }
        }
    }
}
