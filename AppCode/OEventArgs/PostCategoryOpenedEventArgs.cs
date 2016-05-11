using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void PostCategoryOpenedEventHandler(object sender, PostCategoryOpenedEventArgs e);

    public class PostCategoryOpenedEventArgs : EventArgs
    {
        private readonly int categoryID;

        public PostCategoryOpenedEventArgs(int categoryID)
        {
            this.categoryID = categoryID;
        }

        public PostCategoryOpenedEventArgs()
        {
        }

        public int PostCategoryID
        {
            get
            {
                return categoryID;
            }
        }
    }
}