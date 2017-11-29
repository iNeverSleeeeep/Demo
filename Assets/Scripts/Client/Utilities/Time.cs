
namespace Demo.Utils
{
    sealed class Time
    {
        #region UnityEngine.Time 
        public static float realtimeSinceStartup
        {
            get { return UnityEngine.Time.realtimeSinceStartup; }
        }
        public static int renderedFrameCount
        {
            get { return UnityEngine.Time.renderedFrameCount; }
        }
        public static int frameCount
        {
            get { return UnityEngine.Time.frameCount; }
        }
        public static float timeScale
        {
            get { return UnityEngine.Time.timeScale; }
            set { UnityEngine.Time.timeScale = value; }
        }
        public static float maximumParticleDeltaTime
        {
            get { return UnityEngine.Time.timeScale; }
            set { UnityEngine.Time.timeScale = value; }
        }
        public static float smoothDeltaTime
        {
            get { return UnityEngine.Time.smoothDeltaTime; }
        }
        public static float maximumDeltaTime
        {
            get { return UnityEngine.Time.maximumDeltaTime; }
            set { UnityEngine.Time.maximumDeltaTime = value; }
        }
        public static int captureFramerate
        {
            get { return UnityEngine.Time.captureFramerate; }
            set { UnityEngine.Time.captureFramerate = value; }
        }
        public static float fixedDeltaTime
        {
            get { return UnityEngine.Time.fixedDeltaTime; }
            set { UnityEngine.Time.fixedDeltaTime = value; }
        }
        public static float unscaledDeltaTime
        {
            get { return UnityEngine.Time.unscaledDeltaTime; }
        }
        public static float fixedUnscaledTime
        {
            get { return UnityEngine.Time.fixedUnscaledTime; }
        }
        public static float unscaledTime
        {
            get { return UnityEngine.Time.unscaledTime; }
        }
        public static float fixedTime
        {
            get { return UnityEngine.Time.fixedTime; }
        }
        public static float deltaTime
        {
            get { return UnityEngine.Time.deltaTime; }
        }
        public static float timeSinceLevelLoad
        {
            get { return UnityEngine.Time.timeSinceLevelLoad; }
        }
        public static float time
        {
            get { return UnityEngine.Time.time; }
        }
        public static float fixedUnscaledDeltaTime
        {
            get { return UnityEngine.Time.fixedUnscaledDeltaTime; }
        }
        public static bool inFixedTimeStep
        {
            get { return UnityEngine.Time.inFixedTimeStep; }
        }
        #endregion

        #region Logic
        public static float logicTime { get; set; }

        public const float kLogicDeltaTime = 0.0333f;
        public static float logicDeltaTime { get { return kLogicDeltaTime; } }

        public static int logicFrameCount { get; set; }
        #endregion
    }
}

