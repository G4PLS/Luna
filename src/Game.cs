using HarmonyLib;

namespace Luna;

public static class Game
{
    public delegate void TransitionDone();
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
    public static void CloseResults() => Controller.CloseResults(false);
    
    [HarmonyPatch(typeof(GameManager), "Start")]
    [HarmonyPostfix]
    private static void GameManager_Start() => Controller = GameManager.Instance;

    [HarmonyPatch(typeof(GameManager), "OnTransitionDone")]
    [HarmonyPostfix]
    private static void GameManager_OnTransitionDone() => OnTransitionDone?.Invoke();
}