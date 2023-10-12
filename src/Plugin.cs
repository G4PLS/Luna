using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace Luna;

[BepInPlugin(GUID, NAME, VERSION)]
public class Plugin : BasePlugin
{
    public const string GUID = "com.GAPLS.Luna";
    public const string NAME = "Luna";
    public const string VERSION = "0.3";
    public const string AUTHOR = "GAPLS";

    public override void Load()
    {
        var harmony = new Harmony(GUID);
        harmony.PatchAll(typeof(Plugin));
        harmony.PatchAll(typeof(Player));
        harmony.PatchAll(typeof(LevelTimer));
        harmony.PatchAll(typeof(LevelLoader));
        harmony.PatchAll(typeof(Game));
        harmony.PatchAll(typeof(UiManager));
        Player.Init();
    }
}
