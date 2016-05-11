using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class AvailabilityTypesManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new ShoppingCartEntities())
                {
                    var availabilityTypeID = DataParser.IntParse(Request.QueryString["AvailabilityTypeID"]);

                    var availabilityTypeQuery = AvailabilityTypesBL.GetObjectByID(availabilityTypeID, context);

                    availabilityTypeQuery.AvailabilityTypeID = availabilityTypeID;

                    if (availabilityTypeQuery.Title != TitleTextBox.Text)
                    {
                        if (AvailabilityTypesBL.Exists(TitleTextBox.Text, context))
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                            return;
                        }
                        else
                        {
                            availabilityTypeQuery.Title = TitleTextBox.Text;
                        }
                    }
                    else
                    {
                        availabilityTypeQuery.Description = DescriptionTextBox.Text;
                        availabilityTypeQuery.Hide = HideCheckBox.Checked;
                        availabilityTypeQuery.ColorName = ColorTextBox.Text;
                    }

                    try
                    {
                        AvailabilityTypesBL.Save(availabilityTypeQuery, context);
                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage.MessageType = StatusMessageType.Error;
                        StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["AvailabilityTypeID"]))
                {
                    using (var context = new ShoppingCartEntities())
                    {
                        var availabilityTypeID = DataParser.IntParse(Request.QueryString["AvailabilityTypeID"]);

                        var availabilityTypeQuery = AvailabilityTypesBL.GetObjectByID(availabilityTypeID, context);

                        if (availabilityTypeQuery != null)
                        {
                            TitleTextBox.Text = availabilityTypeQuery.Title;
                            DescriptionTextBox.Text = availabilityTypeQuery.Description;
                            ColorTextBox.Text = availabilityTypeQuery.ColorName;
                            HideCheckBox.Checked = availabilityTypeQuery.Hide;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/ShoppingCart/AvailabilityTypesList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/ShoppingCart/AvailabilityTypesList.aspx");
                }
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}