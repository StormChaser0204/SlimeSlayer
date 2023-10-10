using UnityEditor;
using UnityEngine;

namespace Utils
{
    public static class TimeScaler
    {
        [MenuItem("Utils/Time/TimeScale x2 &_UP")]
        public static void SpeedUp()
        {
            if (!Application.isPlaying)
                return;

            Time.timeScale *= 2f;
            Debug.Log($"Time scaled up: {Time.timeScale}");
        }

        [MenuItem("Utils/Time/TimeScale x0.5 &_DOWN")]
        public static void SlowDown()
        {
            if (!Application.isPlaying)
                return;

            Time.timeScale = Mathf.Max(0f, Time.timeScale / 2f);
            Debug.Log($"Time scaled down: {Time.timeScale}");
        }
    }
}