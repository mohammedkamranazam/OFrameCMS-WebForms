using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void DriveOpenedEventHandler(object sender, DriveOpenedEventArgs e);

    public class DriveOpenedEventArgs : EventArgs
    {
        private readonly int driveID;

        public DriveOpenedEventArgs(int driveID)
        {
            this.driveID = driveID;
        }

        public DriveOpenedEventArgs()
        {
        }

        public int DriveID
        {
            get
            {
                return driveID;
            }
        }
    }
}