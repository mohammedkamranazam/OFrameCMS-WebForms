using OWDARO.OEventArgs;
using System;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class ListPopUpComponent : System.Web.UI.UserControl
    {
        public event IDSelectedEventHandler IDSelected;

        public object DataSource
        {
            set
            {
                RadioButtonList.DataSource = value;
                RadioButtonList.DataBind();
                RadioButtonList.Items.Insert(0, new ListItem("None", "-1"));
            }
        }

        public string DataTextField
        {
            set
            {
                RadioButtonList.DataTextField = value;
            }
        }

        public string DataValueField
        {
            set
            {
                RadioButtonList.DataValueField = value;
            }
        }

        public bool Enabled
        {
            set
            {
                ComponentPopUpButton.Enabled = value;
                ShowPopUpButton_ConfirmButtonExtender.Enabled = value;
            }
        }

        public Unit Height
        {
            set
            {
                PopUpPanel.Height = value;
            }
        }

        public string Text
        {
            set
            {
                ComponentPopUpButton.Text = value;
                TitleLabel.Text = value;
            }
        }

        public Unit Width
        {
            set
            {
                PopUpPanel.Width = value;
            }
        }

        private void OnIDSelected(string id)
        {
            if (IDSelected != null)
            {
                var args = new IDSelectedEventArgs(id);

                IDSelected(this, args);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnIDSelected(RadioButtonList.SelectedValue);
        }
    }
}