using System;
using System.Reflection;
using Oxide.Core;
using Oxide.Core.Extensions;

namespace Oxide.GettingOverIt
{
    public class GOIExtension : Extension
    {
        internal static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        internal static readonly AssemblyName AssemblyName = Assembly.GetName();
        internal static readonly VersionNumber AssemblyVersion = new VersionNumber(AssemblyName.Version.Major, AssemblyName.Version.Minor, AssemblyName.Version.Build);
        internal static readonly string AssemblyAuthors = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCompanyAttribute), false)).Company;
        
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
        }
    }
}
