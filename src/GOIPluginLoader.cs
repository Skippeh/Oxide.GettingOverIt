using System;
using Oxide.Core.Plugins;

namespace Oxide.GettingOverIt
{
    public class GOIPluginLoader : PluginLoader
    {
        public override Type[] CorePlugins => new [] { typeof(GOICore) };
    }
}
