using System.Linq;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.GettingOverIt.Types;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Oxide.GettingOverIt.Plugins
{
    public class TestPlugin : GOIPlugin
    {
        public TestPlugin()
        {
            Author = "Skipcast";
            Title = "Test plugin";
        }

        [HookMethod("Init")]
        private void OnInit()
        {
            Interface.Oxide.LogDebug("Init done");
        }

        [HookMethod("OnGameQuit")]
        private void OnGameQuit()
        {
            Interface.Oxide.LogDebug("Game quitting");
        }

        [HookMethod("OnSceneChanged")]
        private void OnSceneChanged(SceneType sceneType, Scene scene)
        {
            Interface.Oxide.LogDebug($"Scene changed to {scene.name} ({scene.buildIndex}).");
        }
        
        [HookMethod("OnLoadGame")]
        private void OnLoadGame(Saviour saviour, SaveState saveState)
        {
            Interface.Oxide.LogDebug($"Load game: {saveState}.");
        }
    }
}
