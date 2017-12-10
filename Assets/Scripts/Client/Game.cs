using Demo.Input;
using Demo.GameLogic;
using Demo.Frame;
using Demo.Level;
using Demo.Camera;
using Demo.Resources;
using UnityEngine;

namespace Demo
{
    sealed class Game
    {
        #region Instance
        static Game instance = null;
        public static Game Instance
        {
            get
            {
                if (instance == null)
                    instance = new Game();
                return instance;
            }
        }
        #endregion

        #region Managers

        InputManager m_InputManager = null;
        public InputManager inputManager { get { return m_InputManager; } }

        GameLogicManager m_GameLogicManager = null;
        public GameLogicManager gameLogicManager { get { return m_GameLogicManager; } }

        LevelManager m_LevelManager = null;

        CameraManager m_CameraManager = null;
        public CameraManager cameraManager { get { return m_CameraManager; } }

        CoroutineManager m_CoroutineManager = null;
        public CoroutineManager coroutineManager { get { return m_CoroutineManager; } }

        #endregion

        FrameBuffer m_FrameBuffer = null;
        public FrameBuffer frameBuffer { get { return m_FrameBuffer; } }

        FrameDataCollector m_FrameDataCollector = null;
        public FrameDataCollector frameDataCollector { get { return m_FrameDataCollector; } }

        ResourceLoader m_ResourceLoader = null;
        public ResourceLoader resourceLoader { get { return m_ResourceLoader; } }

        public bool gameStart { get { return m_LevelManager == null ? false : m_LevelManager.gameStart; } }

        public void Initialize()
        {
            m_CoroutineManager = new CoroutineManager();
            m_ResourceLoader = new ResourceLoader();
            m_FrameBuffer = new FrameBuffer();
            m_FrameDataCollector = new FrameDataCollector();
            m_InputManager = new InputManager();
            m_GameLogicManager = new GameLogicManager();
            m_LevelManager = new LevelManager();
            m_CameraManager = new CameraManager();

            Utils.Random.seed = 1;
            m_LevelManager.Start();
        }

        float m_CachedTime = 0f;
        public void Update()
        {

            m_CachedTime += Utils.Time.deltaTime;
            if (m_FrameBuffer.empty == false)
            {
                if (m_CachedTime > Utils.Time.kLogicDeltaTime)
                {
                    m_CachedTime -= Utils.Time.kLogicDeltaTime;
                    Utils.Time.logicFrameCount++;

                    m_InputManager.Tick();

                    m_GameLogicManager.HandleFrameData(m_FrameBuffer.GetOneFrame());
                    m_GameLogicManager.Tick();
                    m_FrameDataCollector.Tick();
                    coroutineManager.Tick();
                }
            }
            m_LevelManager.Tick();
        }
    }
}


