using System;

namespace OWDARO.Models
{
    [Serializable]
    public class EventTypes
    {
        public string Description
        {
            get;
            set;
        }

        public int EventTypeID
        {
            get;
            set;
        }

        public string ExternalFormEmbedCode
        {
            get;
            set;
        }

        public int? ExternalFormID
        {
            get;
            set;
        }

        public string ExternalFormURL
        {
            get;
            set;
        }

        public bool Hide
        {
            get;
            set;
        }

        public string ImageThumbURL
        {
            get;
            set;
        }

        public string ImageURL
        {
            get;
            set;
        }

        public bool IsRegisterable
        {
            get;
            set;
        }

        public bool PopUpExternalForm
        {
            get;
            set;
        }

        public string RegistrationPageURL
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public bool UseExternalForm
        {
            get;
            set;
        }
    }
}