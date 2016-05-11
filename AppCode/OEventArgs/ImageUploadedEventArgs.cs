using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void ImageUploadedEventHandler(object sender, ImageUploadedEventArgs e);

    public class ImageUploadedEventArgs : EventArgs
    {
        private readonly int imageID;

        public ImageUploadedEventArgs(int imageID)
        {
            this.imageID = imageID;
        }

        public ImageUploadedEventArgs()
        {
        }

        public int ImageID
        {
            get
            {
                return imageID;
            }
        }
    }
}