using Oxide.Core.Plugins;

namespace Oxide.GettingOverIt
{
    public class GOICore : CSPlugin
    {
        private bool initialized;

        public GOICore()
        {
            Title = "Getting Over It with Bennett Foddy";
            Author = "Skipcast";
            IsCorePlugin = true;
        }

        [HookMethod("Init")]
        private void Init()
        {
            if (initialized)
                return;

            initialized = true;
        }

        [HookMethod("OnPluginLoaded")]
        private void OnPluginLoaded(Plugin plugin)
        {
            if (initialized)
            {
                // Call OnGameInitialized on hotloaded plugins.
                plugin.CallHook("OnGameInitialized");
            }
        }
    }
}
