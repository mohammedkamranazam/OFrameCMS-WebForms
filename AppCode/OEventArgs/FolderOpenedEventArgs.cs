using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void FolderOpenedEventHandler(object sender, FolderOpenedEventArgs e);

    public class FolderOpenedEventArgs : EventArgs
    {
        private readonly long? folderID;

        public FolderOpenedEventArgs(long? folderID)
        {
            this.folderID = folderID;
        }

        public FolderOpenedEventArgs()
        {
        }

        public long? FolderID
        {
            get
            {
                return folderID;
            }
        }
    }
}