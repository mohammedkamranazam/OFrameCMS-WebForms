using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void IDSelectedEventHandler(object sender, IDSelectedEventArgs e);

    public class IDSelectedEventArgs : EventArgs
    {
        private readonly string id;

        public IDSelectedEventArgs(string id)
        {
            this.id = id;
        }

        public IDSelectedEventArgs()
        {
        }

        public string ID
        {
            get
            {
                return id;
            }
        }
    }
}