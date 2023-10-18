using System;
using HarmonyLib;

namespace Luna
{
    public static class LevelLoader
    {
        private static LevelSelectMenu _controller;

        public delegate void LevelEnd(Level currentLevel);
        
        /// <summary>
        /// Gets triggered after the player enters a goal
        /// </summary>
        public static LevelEnd OnGoalEntered;
        /// <summary>
        /// Gets triggered when the Moon Boss gets defeated
        /// </summary>
        public static LevelEnd OnSaveRanks;
        /// <summary>
        /// Gets triggered when the Result Screen gets shown
        /// </summary>
        public static LevelEnd OnShowResults;

        public static void LoadLevel(Level level)
        {
            if(Game.GameState != GameState.Load && level != Level.None)
                _controller.StartLevel((int)level);
        }
        public static void LoadLevel(int levelID)
        {
            if(Enum.IsDefined(typeof(Level), levelID) && Game.GameState != GameState.Load && levelID != (int)Level.None)
                _controller.StartLevel(levelID);
        }

        /// <summary>
        /// Loads the next Level in the Level Sequence
        /// </summary>
        public static void LoadNextLevel()
        {
            if (Game.GameState == GameState.Load) return;
            var nextLevel = GameManager.Instance.currentLevel + 1;

            if (Enum.IsDefined(typeof(Level), nextLevel))
                LoadLevel(nextLevel);
        }
        /// <summary>
        /// Loads the Previous level in the level Sequence
        /// </summary>
        public static void LoadPreviousLevel()
        {
            if (Game.GameState == GameState.Load) return;
            var nextLevel = GameManager.Instance.currentLevel - 1;

            if (Enum.IsDefined(typeof(Level), nextLevel))
                LoadLevel(nextLevel);
        }

        /// <summary>
        /// Returns a level with a given id
        /// </summary>
        /// <param name="id">The level id, -1 always uses the current level</param>
        /// <returns>Either the level or null when no level is loaded</returns>
        public static Level GetLevel(int id = -1)
        {
            if (Enum.IsDefined(typeof(Level), id))
                return (Level) GameManager.Instance.currentLevel;
            return Level.None;
            
            /*
            if(id == -1)
            {
                if (GameManager.Instance.gameState == GameState.Mission && Enum.IsDefined(typeof(Level), GameManager.Instance.currentLevel))
                    return (Level)GameManager.Instance.currentLevel;
                return Level.None;
            }
            if (GameManager.Instance.gameState == GameState.Mission && Enum.IsDefined(typeof(Level), id))
                return (Level)GameManager.Instance.currentLevel;
            return Level.None;
            */
        }
        /// <summary>
        /// Returns the Current Level
        /// </summary>
        /// <returns>The Current Level</returns>
        public static Level GetCurrentLevel()
        {
            if (Game.GameState == GameState.Mission &&
                Enum.IsDefined(typeof(Level), GameManager.Instance.currentLevel))
                return (Level) GameManager.Instance.currentLevel;
            return Level.None;
        }

        [HarmonyPatch(typeof(UIController), "Awake")]
        [HarmonyPostfix]
        private static void UIController_Awake() => _controller = UIController.Instance.levelSelectMenu;

        [HarmonyPatch(typeof(LevelGoal), "OnTriggerEnter")]
        [HarmonyPostfix]
        private static void LevelGoal_OnTriggerEnter() => OnGoalEntered?.Invoke(GetCurrentLevel());

        [HarmonyPatch(typeof(MoonBossEnemy), "SaveRanks")]
        [HarmonyPostfix]
        private static void MoonBossEnemy_SaveRanks() => OnSaveRanks?.Invoke(GetCurrentLevel());

        [HarmonyPatch(typeof(GameManager), "ShowResults")]
        [HarmonyPrefix]
        private static void GameManager_ShowResults() => OnShowResults?.Invoke(GetCurrentLevel());
    }
}
