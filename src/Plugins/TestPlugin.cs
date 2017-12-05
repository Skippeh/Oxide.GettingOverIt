﻿using System.Linq;
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

            Interface.Oxide.LogDebug("TestPlugin constructed");
        }

        [HookMethod("Init")]
        private void OnInit()
        {
            Interface.Oxide.LogInfo("Test from TestPlugin");

            var allObjects = GameObject.FindObjectsOfType<MonoBehaviour>().Select(behaviour => behaviour.gameObject).Where(go => go.activeInHierarchy);

            foreach (GameObject go in allObjects)
            {
                Interface.Oxide.LogDebug(go.name);
            }

            Interface.Oxide.LogDebug("Init done in TestPlugin");
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
