using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void LanguageDirectionChangedEventHandler(object sender, LanguageDirectionChangedEventArgs e);

    public class LanguageDirectionChangedEventArgs : EventArgs
    {
        private readonly string direction;

        public LanguageDirectionChangedEventArgs(string direction)
        {
            this.direction = direction;
        }

        public LanguageDirectionChangedEventArgs()
        {
        }

        public string Direction
        {
            get
            {
                return direction;
            }
        }
    }
}