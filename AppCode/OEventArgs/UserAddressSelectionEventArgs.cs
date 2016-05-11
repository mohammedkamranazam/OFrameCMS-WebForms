using System;

namespace OWDARO.OEventArgs
{
    public delegate void UserAddressSelectionEventHandler(object sender, UserAddressSelectionEventArgs e);

    public class UserAddressSelectionEventArgs : EventArgs
    {
        public UserAddressSelectionEventArgs(string streetName, string city, string zipCode, string state, string country)
        {
            this.StreetName = streetName;
            this.City = city;
            this.ZipCode = zipCode;
            this.State = state;
            this.Country = country;
        }

        public UserAddressSelectionEventArgs()
        {
        }

        public string StreetName
        {
            get;
            set;
        }

        public string City
        {
            get;
            set;
        }

        public string ZipCode
        {
            get;
            set;
        }

        public string State
        {
            get;
            set;
        }

        public string Country
        {
            get;
            set;
        }
    }
}