using System;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.GettingOverIt.Types;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oxide.GettingOverIt
{
    public class GOICore : CSPlugin
    {
        private bool initialized;

        /*private GameObject componentObject;
        private GOIComponent component;*/

        public GOICore()
        {
            Title = "Getting Over It with Bennett Foddy";
            Author = GOIExtension.AssemblyAuthors;
            IsCorePlugin = true;
        }

        [HookMethod("IInit")]
        private void Init()
        {
            if (initialized)
                return;

            Application.logMessageReceived += OnLogMessageReceived;

            /*componentObject = new GameObject("Oxide.GettingOverIt");
            component = componentObject.AddComponent<GOIComponent>();*/

            SceneManager.activeSceneChanged += OnNewScene;

            initialized = true;
            Interface.CallHook("Init");
            OnNewScene(default(Scene), SceneManager.GetActiveScene());
            Interface.Oxide.LogDebug("Init done in Core");
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

        [HookMethod("IOnGameQuit")]
        private void OnGameQuit()
        {
            Interface.CallHook("OnGameQuit");

            //GameObject.Destroy(componentObject);
            Interface.Oxide.OnShutdown();
        }

        [HookMethod("IOnLoadGame")]
        private void OnLoadGame(Saviour saviour, SaveState saveState)
        {
            Interface.Call("OnLoadGame", saviour, saveState);
        }

        private void OnNewScene(Scene oldScene, Scene newScene)
        {
            //Interface.Oxide.LogDebug($"Scene changed to {newScene.name}.");

            SceneType newSceneType;
            
            switch (newScene.name)
            {
                case "Loader":
                    newSceneType = SceneType.Menu;
                    break;
                case "Mian":
                    newSceneType = SceneType.Game;
                    break;
                default:
                    Interface.Oxide.LogError($"Unknown scene loaded: {newScene.name} (build index: {newScene.buildIndex})");
                    return;
            }
            
            Interface.Call("OnSceneChanged", newSceneType, newScene);
        }

        private void OnLogMessageReceived(string condition, string stacktrace, LogType type)
        {
            Interface.Oxide.LogInfo(type + "\t" + condition + "\n" + stacktrace);
        }
    }
}
