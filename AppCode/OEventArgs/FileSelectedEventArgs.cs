using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void FileSelectedEventHandler(object sender, FileSelectedEventArgs e);

    public class FileSelectedEventArgs : EventArgs
    {
        private readonly long fileID;

        public FileSelectedEventArgs(long fileID)
        {
            this.fileID = fileID;
        }

        public FileSelectedEventArgs()
        {
        }

        public long FileID
        {
            get
            {
                return fileID;
            }
        }
    }
}