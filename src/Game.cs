using HarmonyLib;

namespace Project_Luna;

public static class Game
{
    public delegate void TransitionDone();
    public static event TransitionDone OnTransitionDone;
    
    public static GameManager Controller { get; private set; }

    public static GameState GameState
    {
        get => Controller.gameState;
        set
        {
            if (Controller != null)
                Controller.gameState = value;
        }
    }

    [HarmonyPatch(typeof(GameManager), "Start")]
    [HarmonyPostfix]
    private static void GameManager_Start() => Controller = GameManager.Instance;

    [HarmonyPatch(typeof(GameManager), "OnTransitionDone")]
    [HarmonyPostfix]
    private static void GameManager_OnTransitionDone() => OnTransitionDone?.Invoke();
}