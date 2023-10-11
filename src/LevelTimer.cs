using HarmonyLib;
using UnityEngine;

namespace Luna
{
    public static class LevelTimer
    {
        private static Timer timer;

        public delegate void TimerUpdate();
        /// <summary>
        /// Triggers whenever the timer calls its Update Method
        /// </summary>
        public static event TimerUpdate OnTimerUpdate;


        /// <summary>
        /// The Character that gets timed
        /// </summary>
        public static Timer.Character Character
        {
            get => timer != null ? timer.character : Timer.Character.Null;
            set
            {
                if (timer != null)
                    timer.character = value;
            }
        }
        /// <summary>
        /// The current time of the level
        /// </summary>
        public static float CurrentLevelTime
        {
            get => timer != null ? timer.currentLevelTime : 0f;
            set
            {
                if (timer != null)
                    timer.currentLevelTime = value;
            }
        }
        /// <summary>
        /// The current level time as a Vector where x=Minutes, y=Seconds, z=Milliseconds with 2 Point accuracy
        /// </summary>
        public static Vector3 CurrentLevelTimeVector
        {
            get => timer != null ? timer.currentLevelTimeVector : Vector3.zero;
            set
            {
                if (timer != null)
                    timer.currentLevelTimeVector = value;
            }
        }
        public static bool TimerPaused => timer != null && timer.timerPaused;
        public static bool TimerStopped => timer != null && timer.timerStopped;

        /// <summary>
        /// Gets the best time of the given level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static float GetLevelTime(Level level) => (float)timer?.GetLevelTime((int)level);
        public static float GetCurrentLevelTime() => (float)timer?.GetCurrentLevelTime();
        /// <summary>
        /// Updates the best time of a specified level
        /// </summary>
        /// <param name="level"></param>
        /// <param name="time"></param>
        public static void UpdateLevelTime(Level level, float time) => timer?.UpdateLevelTime((int)level, time);
        public static void StartTimer() => timer?.StartTimer();
        public static void StopTimer() => timer?.StopTimer();
        public static void PauseTimer() => timer?.PauseTimer();
        public static void PauseTimer(bool pause) => timer?.PauseTimer(pause);
        public static bool TimerRunning() => !(TimerPaused || TimerStopped);

        /// <summary>
        /// Custom Time Vector Method that also includes hours
        /// </summary>
        /// <param name="time"></param>
        /// <returns>A Vector formatted with x=hours, y=minutes, z=seconds, w=milliseconds with 3 point accuracy</returns>
        public static Vector4 TimeToTimeVector(double time)
        {
            int totalMilliseconds = (int)(time * 1000); // Convert seconds to total milliseconds
            int milliseconds = totalMilliseconds % 1000; // Extract milliseconds
            totalMilliseconds /= 1000; // Remove milliseconds
            int seconds = totalMilliseconds % 60; // Extract seconds
            totalMilliseconds /= 60; // Remove seconds
            int minutes = totalMilliseconds % 60; // Extract minutes
            int hours = totalMilliseconds / 60; // Calculate hours

            // Create a Vector4 with hours, minutes, seconds, and milliseconds
            return new Vector4(hours, minutes, seconds, milliseconds);
        }

        /// <summary>
        /// Original Time Vector Method
        /// </summary>
        /// <param name="time"></param>
        /// <returns>A vector formatted with x=minutes, y=seconds, z=milliseconds with 2 point accuracy</returns>
        public static Vector3 ConstructTimeVector(float time) => timer.ConstructTimeVector(time);

        [HarmonyPatch(typeof(Timer), "Awake")]
        [HarmonyPostfix]
        private static void Timer_Awake()
        {
            timer = Timer.Instance;
            t = new();
        }

        private static T t;

        [HarmonyPatch(typeof(Timer), "Update")]
        [HarmonyPostfix]
        private static void Timer_Update(Timer __instance)
        {
            /*
            if (t.CurrentLevelTime < __instance.currentLevelTime)
            {
                t.CurrentLevelTime = __instance.currentLevelTime;
            }

            var isNewLevel = t.CurrentLevelTime > __instance.currentLevelTime;
            if (isNewLevel)
            {
                t.TimePerLevels.Add(t.CurrentLevelTime);
                t.CurrentLevelTime = __instance.currentLevelTime;
            }
            */

            OnTimerUpdate?.Invoke();
        }
    }
}
