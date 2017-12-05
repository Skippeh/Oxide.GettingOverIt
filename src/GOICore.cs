using System;
using System.IO;
using Oxide.Core;
using Oxide.Core.Plugins;
using Oxide.GettingOverIt.Loggers;
using Oxide.GettingOverIt.Types;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Oxide.GettingOverIt
{
    public class GOICore : CSPlugin
    {
        private bool initialized;
        
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
            
            SceneManager.activeSceneChanged += OnNewScene;

            initialized = true;
            Interface.CallHook("Init");
            OnNewScene(default(Scene), SceneManager.GetActiveScene());
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
            Interface.Oxide.OnShutdown();
        }

        [HookMethod("IOnLoadGame")]
        private void OnLoadGame(Saviour saviour, SaveState saveState)
        {
            Interface.CallHook("OnLoadGame", saviour, saveState);
        }

        private void OnNewScene(Scene oldScene, Scene newScene)
        {
            Interface.CallHook("OnSceneChanged", GetSceneType(newScene), newScene);
        }

        private static SceneType GetSceneType(Scene newScene)
        {
            switch (newScene.name)
            {
                case "Loader":
                    return SceneType.Menu;
                case "Mian":
                    return SceneType.Game;
                default:
                    Interface.Oxide.LogError($"Unknown scene loaded: {newScene.name} (build index: {newScene.buildIndex})");
                    return SceneType.Invalid;
            }
        }

        private void OnLogMessageReceived(string condition, string stacktrace, LogType type)
        {
            string logString = condition + (stacktrace != null ? "\n" + stacktrace : "");
            logString = logString.Trim();

            switch (type)
            {
                case LogType.Log:
                    Interface.Oxide.LogInfo(logString);
                    break;
                case LogType.Warning:
                    Interface.Oxide.LogWarning(logString);
                    break;
                case LogType.Error:
                case LogType.Assert:
                case LogType.Exception:
                    Interface.Oxide.LogError(logString);
                    break;
            }
        }
    }
}
