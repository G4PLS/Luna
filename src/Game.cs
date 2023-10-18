using HarmonyLib;
using Rewired.Platforms.Custom;

namespace Luna;

public static class Game
{
    public delegate void TransitionDone();
    /// <summary>
    /// Gets triggered every time the Game Finishes a Transition
    /// </summary>
    public static event TransitionDone OnTransitionDone;
    
    public static GameManager Controller { get; private set; }
    public static GameState GameState
    {
        get => Controller.GetGameState();
        set
        {
            if (Controller != null)
                Controller.SetGameState(value);
        }
    }

    public static void Pause(bool pause) => Controller.Pause(pause);
    public static bool IsPaused() => Controller.IsPaused();
    
    /// <summary>
    /// Closes the Results screen
    /// </summary>
    /// <param name="switchToLevel">If it should switch to a level</param>
    /// <param name="levelID">The level ID that gets loaded when it switches to a level</param>
    /// <param name="toCutscene"></param>
    public static void CloseResults(bool switchToLevel, int levelID = 0, bool toCutscene = false) =>
        Controller.CloseResults(switchToLevel, levelID, toCutscene);
     
    [HarmonyPatch(typeof(GameManager), "Start")]
    [HarmonyPostfix]
    private static void GameManager_Start() => Controller = GameManager.Instance;

    [HarmonyPatch(typeof(GameManager), "OnTransitionDone")]
    [HarmonyPostfix]
    private static void GameManager_OnTransitionDone() => OnTransitionDone?.Invoke();
}