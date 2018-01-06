using UnityEngine;
using UnityEditor;

namespace Demo.Utils
{
    interface ICloneable<T>
    {
        T Clone();
    }

    static class Angle
    {
        public static float Between(Vector3 me, Vector3 target)
        {
            var offset = target - me;
            var angle = Vector3.Angle(offset, Vector3.forward);
            if (Vector3.Cross(offset, Vector3.forward).y > 0)
                angle = 360 - angle;
            return angle;
        }
    }
}

