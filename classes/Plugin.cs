using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace StickLib
{
    [BepInPlugin("raisin.plugin.sticklib", "StickLib", "0.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        public readonly static string GUID = "raisin.plugin.sticklib";
        public readonly static string NAME = "StickLib";
        public readonly static string VERSION = "0.1.0";

        private void Awake()
        {
            Logger.LogInfo($"Loaded StickLib {VERSION}");
            Harmony.CreateAndPatchAll(typeof(Plugin));

            GameObject console = new GameObject("Console");
            console.AddComponent<Log>();
            DontDestroyOnLoad(console);

            Log.Message($"Loaded StickLib {VERSION}");

            StickLib.Config.ConsoleLimit = Config.Bind("Console Message Limit", "ConsoleLimit", 256, "The maximum number of messages that will be saved in the console.");
        }

        [HarmonyPatch(typeof(GameManager), "Update")]
        [HarmonyPostfix]
        static void UpdatePostfix(GameManager __instance)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Log.Message($"There are currently {Player.GetAll().Length} players");
            }
        }
    }
}