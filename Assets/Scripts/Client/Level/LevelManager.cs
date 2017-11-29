
using UnityEngine;

namespace Demo.Level
{
    class LevelManager
    {
        LevelFactory m_LevelFactory = null;
        LevelLoader m_LevelLoader = null;
        LevelFinisher m_LevelFinisher = null;

        bool m_GameStart = false;
        public bool gameStart { get { return m_GameStart; } }

        public LevelManager()
        {
            m_LevelFactory = new WarlockWarsLevelFactory();
            m_LevelLoader = m_LevelFactory.GetLevelLoader();
            m_LevelFinisher = m_LevelFactory.GetLevelFinisher();
        }

        public void Start()
        {
            m_LevelLoader.Load();
            m_GameStart = true;
        }

        public void Tick()
        {
            if (m_LevelFinisher.CheckIfGameIsOver())
                GameOver();
        }

        public void GameOver()
        {
            Debug.Log("Game Over!");
            m_GameStart = false;
        }
    }
}

