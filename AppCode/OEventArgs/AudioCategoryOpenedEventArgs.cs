using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void AudioCategoryOpenedEventHandler(object sender, AudioCategoryOpenedEventArgs e);

    public class AudioCategoryOpenedEventArgs : EventArgs
    {
        private readonly int categoryID;

        public AudioCategoryOpenedEventArgs(int categoryID)
        {
            this.categoryID = categoryID;
        }

        public AudioCategoryOpenedEventArgs()
        {
        }

        public int AudioCategoryID
        {
            get
            {
                return categoryID;
            }
        }
    }
}