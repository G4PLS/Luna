using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Project_Luna
{
    public static class LevelTimer
    {
        private static Timer timer;

        /// <summary>
        /// The Character that gets timed
        /// </summary>
        public static Timer.Character Character 
        { 
            get
            {
                if (timer != null) return timer.character;
                return Timer.Character.Null;
            }
            set
            {
                if(timer != null)
                {
                    timer.character = value;
                }
            }
        }
        /// <summary>
        /// The current time of the level
        /// </summary>
        public static float CurrentLevelTime
        {
            get
            {
                if (timer != null) return timer.currentLevelTime;
                return 0f;
            }
            set
            {
                if(timer != null)
                {
                    timer.currentLevelTime = value;
                }
            }
        }
        /// <summary>
        /// The current level time as a Vector
        /// </summary>
        public static Vector3 CurrentLevelTimeVector
        {
            get
            {
                if (timer != null) return timer.currentLevelTimeVector;
                return Vector3.zero;
            }
            set
            {
                if(timer != null)
                {
                    timer.currentLevelTimeVector = (Vector3)value;
                }
            }
        }
        public static bool TimerPaused
        {
            get
            {
                if (timer != null) return timer.timerPaused;
                return false;
            }
            set
            {
                if(timer != null)
                {
                    timer.timerPaused = value;
                }
            }
        }
        public static bool TimerStopped
        {
            get
            {
                if (timer != null) return timer.timerStopped;
                return false;
            }
            set
            {
                if(timer != null)
                {
                    timer.timerStopped = value;
                }
            }
        }

        /// <summary>
        /// Gets the best time of the given level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static float? GetLevelTime(Level level) => timer?.GetLevelTime((int)level);
        public static float GetCurrentLevelTime() => (float)(timer?.GetCurrentLevelTime());
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
        public static Vector4 TimeToTimeVector(float time)
        {
            int totalMilliseconds = (int)(time * 1000); // Convert seconds to total milliseconds
            int milliseconds = totalMilliseconds % 1000; // Extract milliseconds
            totalMilliseconds /= 1000; // Remove milliseconds
            int seconds = totalMilliseconds % 60; // Extract seconds
            totalMilliseconds /= 60; // Remove seconds
            int minutes = totalMilliseconds % 60; // Extract minutes
            int hours = totalMilliseconds / 60; // Calculate hours

            // Create a Vector4 with hours, minutes, seconds, and milliseconds
            Vector4 timeVector = new Vector4(hours, minutes, seconds, milliseconds);

            return timeVector;
        }
        /// <summary>
        /// Original Time Vector Method
        /// </summary>
        /// <param name="time"></param>
        /// <returns>A vector formatted with x=minutes, y=seconds, z=milliseconds with 2 point accuracy</returns>
        public static Vector3 ConstructTimeVector(float time) => timer.ConstructTimeVector(time);

        [HarmonyPatch(typeof(Timer), "Awake")]
        [HarmonyPostfix]
        private static void Timer_Awake() => timer = Timer.Instance;

        [HarmonyPatch(typeof(Timer), "Update")]
        [HarmonyPrefix]
        private static void UPDATE()
        {

        }
    }
}
