using System.Collections.Generic;

namespace Demo.Utils
{
    static class Extention
    {
        public static List<T> Clone<T>(this List<T> list) where T : class, ICloneable<T>
        {
            var clone = new List<T>(list.Count);
            foreach (var item in list)
                clone.Add(item.Clone());
            return clone;
        }
    }
}