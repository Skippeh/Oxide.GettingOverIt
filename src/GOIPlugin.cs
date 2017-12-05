// Currently not using Oxide.CSharp (CSharpPlugin) because the game targets .NET 2.0 subset (which isn't supported).

using Oxide.Core;
using Oxide.Core.Libraries;
using Oxide.Core.Plugins;

namespace Oxide.GettingOverIt
{
    public abstract class GOIPlugin : CSPlugin
    {
        protected Timer Timer { get; private set; }

        protected GOIPlugin()
        {
            Timer = Interface.Oxide.GetLibrary<Timer>();
            Timer.Repeat(0, 0, Tick, this);
        }

        protected virtual void Tick()
        {
        }
    }
}
