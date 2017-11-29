
using Demo.Utils;
using System.Collections.Generic;

namespace Demo.Frame
{
    public struct FrameData
    {
        public enum OperationType
        {
            Invalid,
            Moveing,
            EndMove,
            Skill
        }

        public struct EntityData
        {
            public int id;
            public OperationType type;

            // type == OperationType.Moveing
            public float angle;

#if UNITY_EDITOR
            private static System.Text.StringBuilder toStringBuilder = new System.Text.StringBuilder(); 
            public override string ToString()
            {
                toStringBuilder.Length = 0;
                toStringBuilder.Append("id:");
                toStringBuilder.Append(id.ToString());
                toStringBuilder.Append(" oper:");
                toStringBuilder.Append(type.ToString());
                return toStringBuilder.ToString();
            }
#endif
        }

        public int id;
        public List<EntityData> data;

        FrameData(int id , List<EntityData> data)
        {
            this.id = id;
            this.data = data;
        }
#if UNITY_EDITOR
        private static System.Text.StringBuilder toStringBuilder = new System.Text.StringBuilder();
        public override string ToString()
        {
            toStringBuilder.Length = 0;
            toStringBuilder.Append("EntityData id:");
            toStringBuilder.Append(id.ToString());
            toStringBuilder.Append(" data:");
            if (data == null)
                toStringBuilder.Append("null");
            else
            {
                toStringBuilder.Append('[');
                foreach (var d in data)
                    toStringBuilder.Append(d.ToString());
                toStringBuilder.Append(']');
            }

            return toStringBuilder.ToString();
        }
#endif
    }
}

