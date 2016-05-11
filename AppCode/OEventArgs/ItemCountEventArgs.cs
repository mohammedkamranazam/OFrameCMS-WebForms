using System;

namespace OWDARO.OEventArgs
{
    // public delegates...
    public delegate void ItemCountChangedEventHandler(object sender, ItemCountEventArgs e);

    public class ItemCountEventArgs : EventArgs
    {
        private readonly int itemCount;

        public ItemCountEventArgs(int itemCount)
        {
            this.itemCount = itemCount;
        }

        public ItemCountEventArgs()
        {
        }

        public int ItemCount
        {
            get
            {
                return itemCount;
            }
        }
    }
}