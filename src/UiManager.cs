using System.Collections.Generic;
using HarmonyLib;
using Il2CppSystem;
using UnityEngine;

namespace Luna;

public static class UiManager
{
    public enum UIState
    {
        Title,
        Mission,
        Options,
        OptionsGameplay,
        OptionsGraphics,
        OptionsAudio,
        OptionsCamera,
        Profile,
        Results,
        CharacterSelect,
        LevelSelect,
    }
    
    public static UIController Controller { get; private set; }
    private static Dictionary<UIState, object> _uiMenus;

    public static void LockCursor() => Controller.onLockCursor();

    private static UIState _currentUIState; 
    public static UIState CurrentUIState
    {
        get => _currentUIState;
        set
        {
            if (Controller != null)
                _currentUIState = value;
        }
    }

    public static void DisableAllMenus()
    {
        foreach (var menu in _uiMenus)
            ChangeMenuState(menu.Value, false);
    }
    
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
            {UIState.OptionsGameplay, Controller.gameplayMainMenu}, //INGAME MENU
            {UIState.OptionsGraphics, Controller.graphicsMenu}, //INGAME MENU
            {UIState.OptionsAudio, Controller.audioMenu}, //INGAME MENU
            {UIState.OptionsCamera, Controller.gameplayMenu}, //INGAME MENU
            {UIState.Profile, Controller.profileMenu}, //PROFILE MENU
            {UIState.Results, Controller.resultsMenu}, //RESULTS MENU
            {UIState.CharacterSelect, Controller.characterSelectMenu}, //CHARACTER SELECT MENU
            {UIState.LevelSelect, Controller.levelSelectMenu}, //LEVEL SELECT MENU
        };
        
        var image = new Texture2D(512, 512);
        image.LoadImage(Resource.GameLogo);

        var sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        UIController.Instance.gameLogo.overrideSprite = sprite;
    }

    [HarmonyPatch(typeof(IngameMenu), "OnEnable")]
    [HarmonyPostfix]
    private static void IngameMenu_OnEnable(IngameMenu __instance)
    {
        if (__instance == Controller.gameplayMainMenu)
            CurrentUIState = UIState.OptionsGameplay;
        else if (__instance == Controller.graphicsMenu)
            CurrentUIState = UIState.OptionsGraphics;
        else if (__instance == Controller.audioMenu)
            CurrentUIState = UIState.OptionsAudio;
        else if (__instance == Controller.gameplayMenu)
            CurrentUIState = UIState.OptionsCamera;
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
}