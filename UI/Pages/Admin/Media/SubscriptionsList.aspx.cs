using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.Media
{
    public partial class SubscriptionsList : System.Web.UI.Page
    {
        protected void ExportToExcelButton_Click(object sender, EventArgs e)
        {
            using (var context = new MediaEntities())
            {
                var subscriptionsQuery = (from set in context.ME_Subscriptions
                                          select set);
                Utilities.ExportExcel(Controls, subscriptionsQuery.ToList(), "ALL_Subscriptions");
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            using (var context = new MediaEntities())
            {
                var email = e.CommandName;

                try
                {
                    SubscriptionsBL.Delete(email, context);
                    GridView1.DataBind();
                    StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Success;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}