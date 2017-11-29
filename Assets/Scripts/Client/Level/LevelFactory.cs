using UnityEngine;
using UnityEditor;

namespace Demo.Level
{
    abstract class LevelFactory
    {
        public abstract LevelLoader GetLevelLoader();
        public abstract LevelFinisher GetLevelFinisher();
    }
}