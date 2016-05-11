using System;

namespace OWDARO.Performance
{
    [Serializable]
    public class SerializableSession
    {
        public object SessionObject
        {
            get;
            set;
        }
    }
}