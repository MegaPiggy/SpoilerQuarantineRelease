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
        private void Awake()
        {
            Logger.LogDebug($"{nameof(SpoilerQuarantineRelease)} was started");
            Logger.LogDebug($"The mod script has been placed in the '{this.gameObject.name}'");
            new Harmony("MegaPiggy.SpoilerQuarantineRelease").PatchAll();
            Logger.LogDebug($"Harmony patching complete");
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
                foreach (var referenceFrame in GameObject.FindObjectsOfType(typeof(ReferenceFrameVolume)).Select(rfVolume => ((ReferenceFrameVolume)rfVolume).GetReferenceFrame()))
                {
                    referenceFrame.isAvailable = true;
                }
                foreach (var mrfVolume in GameObject.FindObjectsOfType(typeof(MajorReferenceFrameVolume)).Cast<MajorReferenceFrameVolume>())
                {
                    mrfVolume._isUnavailable = false;
                }
            }
        }
    }
}
