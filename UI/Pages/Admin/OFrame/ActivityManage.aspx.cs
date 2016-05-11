using OWDARO;
using OWDARO.BLL.GalleryBLL;
using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.OFrame
{
    public partial class ActivityManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["ActivityLogID"]))
                {
                    Response.Redirect("~/UI/Pages/Admin/OFrame/ActivityList.aspx");
                }

                var activityLogID = Request.QueryString["ActivityLogID"];

                using (var context = new OWDAROEntities())
                {
                    var activityQuery = ActivityLogsBL.GetObjectByID(new Guid(activityLogID), context);

                    if (activityQuery != null)
                    {
                        ActivityMessageLiteral.Text = activityQuery.ActivityMessage;
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                    }
                }
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}