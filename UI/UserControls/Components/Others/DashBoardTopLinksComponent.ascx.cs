using System;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class DashBoardTopLinksComponent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GraphList.Visible = false;
            SettingsList.Visible = false;
            MessageList.Visible = false;

            HomeNotification.Visible = false;
        }
    }
}