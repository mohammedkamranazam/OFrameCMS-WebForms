using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void URLGeneratedEventHandler(object sender, URLGeneratedEventArgs e);

    public class URLGeneratedEventArgs : EventArgs
    {
        private readonly string url;

        public URLGeneratedEventArgs(string url)
        {
            this.url = url;
        }

        public URLGeneratedEventArgs()
        {
        }

        public string URL
        {
            get
            {
                return url;
            }
        }
    }
}