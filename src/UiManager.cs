using System.Collections.Generic;
using System.Runtime.InteropServices;
using HarmonyLib;
using Il2CppSystem;
using Il2CppSystem.Runtime.CompilerServices;
using UnityEngine;

namespace Luna;

public static class UiManager
{
    public enum UIState
    {
        Title,
        Mission,
        Options,
        OptionsSubGameplay,
        OptionsSubGraphics,
        OptionsSubAudio,
        OptionsSubCamera,
        Profile,
        Results,
        CharacterSelect,
        LevelSelect,
    }
    
    public static UIController Controller { get; private set; }
    private static Dictionary<UIState, object> _uiMenus;
    
    private static UIState _currentUIState;
    
    public static UIState CurrentUIState
    {
        get => _currentUIState;
        set
        {
            _currentUIState = value;
            Console.WriteLine(value.ToString());
        }
    }
    public static bool EnableUIInput { get; set; } = true;
    
    public static void LockCursor() => Controller.onLockCursor();
    
    /// <summary>
    /// Refreshes all the Menus.
    /// This needs to be called when the UIState gets changed externally
    /// </summary>
    public static void RefreshMenus()
    {
        foreach (var menu in _uiMenus)
        {
            if (menu.Key == CurrentUIState)
                ChangeMenuState(menu.Value, true);
            else 
                ChangeMenuState(menu.Value, false);
        }
    }
    public static void RefreshMenus(UIState state)
    {
        CurrentUIState = state;
        foreach (var menu in _uiMenus)
        {
            if (menu.Key == state)
                ChangeMenuState(menu.Value, true);
            else 
                ChangeMenuState(menu.Value, false);
        }
    }
    public static void DisableAllMenus()
    {
        foreach (var menu in _uiMenus)
            ChangeMenuState(menu.Value, false);
    }
    
    private static void ChangeMenuState(object menu, bool state)
    {
        switch (menu)
        {
            case IngameMenu ingameMenu:
                ingameMenu.gameObject.SetActive(state);
                break;
            case CharacterSelectMenu characterSelectMenu:
                characterSelectMenu.gameObject.SetActive(state);
                break;
            case LevelSelectMenu levelSelectMenu:
                levelSelectMenu.gameObject.SetActive(state);
                break;
            case MissionUI missionUI:
                missionUI.gameObject.SetActive(state);
                break;
            case OptionsMenu optionsMenu:
                optionsMenu.gameObject.SetActive(state);
                break;
            case PauseMenu pauseMenu:
                pauseMenu.gameObject.SetActive(state);
                break;
            case TitleScreenMenu titleScreenMenu:
                titleScreenMenu.gameObject.SetActive(state);
                titleScreenMenu.Deactivate();
                break;
        }
    }

    [HarmonyPatch(typeof(UIController), "Start")]
    [HarmonyPostfix]
    private static void UIController_Start()
    {
        Controller = UIController.Instance;

        _uiMenus = new Dictionary<UIState, object>()
        {
            {UIState.Title, Controller.titleScreenMenu}, //TITLE SCREEN MENU
            {UIState.Mission, Controller.missionUI}, //MISSION UI
            {UIState.Options, Controller.optionsMenu}, //OPTIONS MENU
            {UIState.OptionsSubGameplay, Controller.gameplayMainMenu}, //INGAME MENU
            {UIState.OptionsSubGraphics, Controller.graphicsMenu}, //INGAME MENU
            {UIState.OptionsSubAudio, Controller.audioMenu}, //INGAME MENU
            {UIState.OptionsSubCamera, Controller.gameplayMenu}, //INGAME MENU
            {UIState.Profile, Controller.profileMenu}, //PROFILE MENU
            {UIState.Results, Controller.resultsMenu}, //RESULTS MENU
            {UIState.CharacterSelect, Controller.characterSelectMenu}, //CHARACTER SELECT MENU
            {UIState.LevelSelect, Controller.levelSelectMenu}, //LEVEL SELECT MENU
        };
        
        Console.WriteLine(Controller.pauseMenu);
        
        var image = new Texture2D(512, 512);
        image.LoadImage(Resource.GameLogo);

        var sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        UIController.Instance.gameLogo.overrideSprite = sprite;
    }

    #region Change UI State

    [HarmonyPatch(typeof(IngameMenu), "OnEnable")]
    [HarmonyPostfix]
    private static void IngameMenu_OnEnable(IngameMenu __instance)
    {
        if (__instance == Controller.gameplayMainMenu)
            CurrentUIState = UIState.OptionsSubGameplay;
        else if (__instance == Controller.graphicsMenu)
            CurrentUIState = UIState.OptionsSubGraphics;
        else if (__instance == Controller.audioMenu)
            CurrentUIState = UIState.OptionsSubAudio;
        else if (__instance == Controller.gameplayMenu)
            CurrentUIState = UIState.OptionsSubCamera;
    }

    [HarmonyPatch(typeof(CharacterSelectMenu), "OnEnable")]
    [HarmonyPostfix]
    private static void CharacterSelectMenu_OnEnable() => CurrentUIState = UIState.CharacterSelect;

    [HarmonyPatch(typeof(TitleScreenMenu), "OnEnable")]
    [HarmonyPostfix]
    private static void TitleScreenMenu_OnEnable() => CurrentUIState = UIState.Title;

    [HarmonyPatch(typeof(MissionUI), "OnEnable")]
    [HarmonyPostfix]
    private static void MissionUI_OnEnable() => CurrentUIState = UIState.Mission;

    [HarmonyPatch(typeof(OptionsMenu), "OnEnable")]
    [HarmonyPostfix]
    private static void OptionsMenu_OnEnable() => CurrentUIState = UIState.Options;

    [HarmonyPatch(typeof(ProfileMenu), "OnEnable")]
    [HarmonyPostfix]
    private static void ProfileMenu_OnEnable() => CurrentUIState = UIState.Profile;

    [HarmonyPatch(typeof(ResultsMenu), "OnEnable")]
    [HarmonyPostfix]
    private static void ResultsMenu_OnEnable() => CurrentUIState = UIState.Results;

    [HarmonyPatch(typeof(LevelSelectMenu), "OnEnable")]
    [HarmonyPostfix]
    private static void LevelSelectMenu_OnEnable() => CurrentUIState = UIState.LevelSelect;

    #endregion

    #region Updating UI

    [HarmonyPatch(typeof(IngameMenu), "GetInput")]
    [HarmonyPrefix]
    private static bool IngameMenu_GetInput() => EnableUIInput;

    [HarmonyPatch(typeof(CharacterSelectMenu), "GetInput")]
    [HarmonyPrefix]
    private static bool CharacterSelectMenu_GetInput() => EnableUIInput;

    [HarmonyPatch(typeof(TitleScreenMenu), "GetInput")]
    [HarmonyPrefix]
    private static bool TitleScreenMenu_GetInput() => EnableUIInput;

    [HarmonyPatch(typeof(OptionsMenu), "GetInput")]
    [HarmonyPrefix]
    private static bool OptionsMenu_GetInput() => EnableUIInput;

    [HarmonyPatch(typeof(ProfileMenu), "GetInput")]
    [HarmonyPrefix]
    private static bool ProfileMenu_GetInput() => EnableUIInput;

    [HarmonyPatch(typeof(ResultsMenu), "GetInput")]
    [HarmonyPrefix]
    private static bool ResultsMenu_GetInput() => EnableUIInput;

    [HarmonyPatch(typeof(LevelSelectMenu), "GetInput")]
    [HarmonyPrefix]
    private static bool LevelSelectMenu_GetInput() => EnableUIInput;
    
    [HarmonyPatch(typeof(PauseMenu), "GetInput")]
    [HarmonyPrefix]
    private static bool PauseMenu_GetInput() => EnableUIInput;

    #endregion
}