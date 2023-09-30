using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace Project_Luna;

[BepInPlugin(GUID, NAME, VERSION)]
public class Plugin : BasePlugin
{
    public const string GUID = "com.GAPLS.Luna";
    public const string NAME = "Luna";
    public const string VERSION = "0.0";
    public const string AUTHOR = "GAPLS";

    public delegate void TransitionDone();
    public static event TransitionDone OnTransitionDone;


    public override void Load()
    {
        var harmony = new Harmony(GUID);
        harmony.PatchAll(typeof(Player));
        harmony.PatchAll(typeof(LevelTimer));

        Player.Init();
    }

    [HarmonyPatch(typeof(GameManager), "OnTransitionDone")]
    [HarmonyPostfix]
    private static void GameManager_OnTransitionDone()
    {
        OnTransitionDone?.Invoke();
    }
}
