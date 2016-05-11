using System;

namespace OWDARO.Models
{
    [Serializable]
    public class Language
    {
        public string Direction
        {
            get;
            set;
        }

        public string Locale
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}