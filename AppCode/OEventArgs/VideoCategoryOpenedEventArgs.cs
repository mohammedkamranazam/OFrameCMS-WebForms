using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void VideoCategoryOpenedEventHandler(object sender, VideoCategoryOpenedEventArgs e);

    public class VideoCategoryOpenedEventArgs : EventArgs
    {
        private readonly int categoryID;

        public VideoCategoryOpenedEventArgs(int categoryID)
        {
            this.categoryID = categoryID;
        }

        public VideoCategoryOpenedEventArgs()
        {
        }

        public int VideoCategoryID
        {
            get
            {
                return categoryID;
            }
        }
    }
}