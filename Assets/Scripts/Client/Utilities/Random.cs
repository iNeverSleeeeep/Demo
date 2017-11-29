

using UnityEngine;

namespace Demo.Utils
{
    static class Random
    {
        static int m_Seed = 0;
        public static int seed
        {
            get { return m_Seed; }
            set
            {
                m_Seed = value;
                UnityEngine.Random.InitState(m_Seed);
            }
        }
        public static float Range(float min, float max)
        {
            Debug.Assert(m_Seed != 0);
            return UnityEngine.Random.Range(min, max);
        }
    }
}

