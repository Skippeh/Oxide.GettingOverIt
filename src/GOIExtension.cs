using System;
using System.IO;
using System.Reflection;
using System.Text;
using Oxide.Core;
using Oxide.Core.Extensions;
using Oxide.GettingOverIt.Loggers;
using Oxide.Plugins;

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

        public override string[] DefaultReferences => new[]
        {
            "ArabicSupport",
            "Assembly-CSharp-firstpass",
            "Assembly-CSharp",
            "Oxide.Core",
            "Oxide.GettingOverIt",
            "netstandard",
            "Purchasing.Common",
            "Rewired_Core",
            "Rewired_Windows",
            "System.Drawing",
            "Unity.Analytics.DataPrivacy",
            "Unity.Postprocessing.Runtime",
            "Unity.TextMeshPro",
            "Unity.Timeline",
            "UnityEngine.AccessibilityModule",
            "UnityEngine.Advertisements",
            "UnityEngine.AIModule",
            "UnityEngine.AndroidJNIModule",
            "UnityEngine.AnimationModule",
            "UnityEngine.ARModule",
            "UnityEngine.AssetBundleModule",
            "UnityEngine.AudioModule",
            "UnityEngine.ClothModule",
            "UnityEngine.ClusterInputModule",
            "UnityEngine.ClusterRendererModule",
            "UnityEngine.CoreModule",
            "UnityEngine.CrashReportingModule",
            "UnityEngine.DirectorModule",
            "UnityEngine",
            "UnityEngine.DSPGraphModule",
            "UnityEngine.GameCenterModule",
            "UnityEngine.GIModule",
            "UnityEngine.GridModule",
            "UnityEngine.HotReloadModule",
            "UnityEngine.ImageConversionModule",
            "UnityEngine.IMGUIModule",
            "UnityEngine.InputLegacyModule",
            "UnityEngine.InputModule",
            "UnityEngine.JSONSerializeModule",
            "UnityEngine.LocalizationModule",
            "UnityEngine.Monetization",
            "UnityEngine.ParticleSystemModule",
            "UnityEngine.PerformanceReportingModule",
            "UnityEngine.Physics2DModule",
            "UnityEngine.PhysicsModule",
            "UnityEngine.ProfilerModule",
            "UnityEngine.Purchasing.AppleCore",
            "UnityEngine.Purchasing.AppleMacosStub",
            "UnityEngine.Purchasing.AppleStub",
            "UnityEngine.Purchasing.Codeless",
            "UnityEngine.Purchasing",
            "UnityEngine.Purchasing.SecurityCore",
            "UnityEngine.Purchasing.SecurityStub",
            "UnityEngine.Purchasing.Stores",
            "UnityEngine.Purchasing.WinRTCore",
            "UnityEngine.Purchasing.WinRTStub",
            "UnityEngine.RuntimeInitializeOnLoadManagerInitializerModule",
            "UnityEngine.ScreenCaptureModule",
            "UnityEngine.SharedInternalsModule",
            "UnityEngine.SpatialTracking",
            "UnityEngine.SpriteMaskModule",
            "UnityEngine.SpriteShapeModule",
            "UnityEngine.StreamingModule",
            "UnityEngine.SubstanceModule",
            "UnityEngine.SubsystemsModule",
            "UnityEngine.TerrainModule",
            "UnityEngine.TerrainPhysicsModule",
            "UnityEngine.TextCoreModule",
            "UnityEngine.TextRenderingModule",
            "UnityEngine.TilemapModule",
            "UnityEngine.TLSModule",
            "UnityEngine.UI",
            "UnityEngine.UIElementsModule",
            "UnityEngine.UIElementsNativeModule",
            "UnityEngine.UIModule",
            "UnityEngine.UmbraModule",
            "UnityEngine.UNETModule",
            "UnityEngine.UnityAnalyticsModule",
            "UnityEngine.UnityConnectModule",
            "UnityEngine.UnityCurlModule",
            "UnityEngine.UnityTestProtocolModule",
            "UnityEngine.UnityWebRequestAssetBundleModule",
            "UnityEngine.UnityWebRequestAudioModule",
            "UnityEngine.UnityWebRequestModule",
            "UnityEngine.UnityWebRequestTextureModule",
            "UnityEngine.UnityWebRequestWWWModule",
            "UnityEngine.VehiclesModule",
            "UnityEngine.VFXModule",
            "UnityEngine.VideoModule",
            "UnityEngine.VirtualTexturingModule",
            "UnityEngine.VRModule",
            "UnityEngine.WindModule",
            "UnityEngine.XR.LegacyInputHelpers",
            "UnityEngine.XRModule",
            "ZFBrowser"
        };

        public override string[] WhitelistAssemblies => new[]
        {
            "Assembly-CSharp", "Assembly-CSharp-firstpass",
            "mscorlib",
            "Oxide.Core",
            "System.Core", "System.Collections", "System.Linq",
            "UnityEngine",
            "Oxide.GettingOverIt"
        };

        public override string[] WhitelistNamespaces => new[]
        {
            "System.Collections", "System.Security.Cryptography", "System.Text",
            "UnityEngine",
            "Oxide", "Oxide.GettingOverIt"
        };

        public override void Load() => Manager.RegisterPluginLoader(new GOIPluginLoader());

        public override void OnModLoad()
        {
            CSharpPluginLoader.PluginReferences.UnionWith(DefaultReferences);

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
