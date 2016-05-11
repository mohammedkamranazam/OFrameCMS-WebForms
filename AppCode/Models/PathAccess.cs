using System;

namespace OWDARO.Models
{
    [Serializable]
    public class PathAccess
    {
        public string AccessLevel
        {
            get;
            set;
        }

        public string AccessTo
        {
            get;
            set;
        }

        public string AccessType
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }
    }
}