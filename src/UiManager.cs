using HarmonyLib;
using UnityEngine;

namespace Project_Luna;

public static class UiManager
{
    public static UIController Controller { get; private set; }

    public static UIController.UIState UIState
    {
        get => Controller.currentState;
        set
        {
            if (Controller != null)
                Controller.currentState = value;
        }
    }

    [HarmonyPatch(typeof(UIController), "Start")]
    [HarmonyPostfix]
    private static void UIController_Start()
    {
        Controller = UIController.Instance;
        byte[] bytes = Resource.GameLogo;
        var image = new Texture2D(512, 512);
        image.LoadImage(bytes);

        var sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        UIController.Instance.gameLogo.overrideSprite = sprite;
    }
}