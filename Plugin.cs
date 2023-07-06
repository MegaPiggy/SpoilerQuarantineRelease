using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SpoilerQuarantineRelease
{
    [BepInPlugin("MegaPiggy.SpoilerQuarantineRelease", "Spoiler Quarantine Release", "1.0.0")]
    [BepInProcess("OuterWilds_AlphaDemo_PC.exe")]
    public class SpoilerQuarantineRelease : BaseUnityPlugin
    {

        private static string gamePath;
        public static string DllExecutablePath
        {
            get
            {
                if (string.IsNullOrEmpty(gamePath))
                    gamePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return gamePath;
            }

            private set { }
        }

        private void Awake()
        {
            Debug.Log($"{nameof(SpoilerQuarantineRelease)} was started");
            Debug.Log($"The mod script has been placed in the '{this.gameObject.name}'");
            var harmonyInstance = new Harmony("MegaPiggy.SpoilerQuarantineRelease");
            harmonyInstance.PatchAll();
        }
    }

    [HarmonyPatch(typeof(SettingsMenu), "Awake")]
    public class SceneLoading
    {
        public static void Prefix()
        {
            if (Application.loadedLevel == 1) // Solar System
            {
                GameObject.DestroyImmediate(GameObject.Find("SolarSystemRoot/QuantumMoon_Body/SpoilerQuarantine"));
                GameObject.DestroyImmediate(GameObject.Find("SolarSystemRoot/DarkBramble_Body/SpoilerQuarantine"));
                GameObject.DestroyImmediate(GameObject.Find("SolarSystemRoot/FocalBody/SpoilerQuarantine"));
                GameObject.DestroyImmediate(GameObject.Find("SolarSystemRoot/GiantsDeep_Body/SpoilerQuarantine"));
            }
        }
    }
}
