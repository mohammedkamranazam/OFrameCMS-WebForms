using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class IconsManage : System.Web.UI.Page
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
                    var iconID = DataParser.IntParse(Request.QueryString["IconID"]);

                    var iconQuery = IconsBL.GetObjectByID(iconID, context);

                    if (iconQuery != null)
                    {
                        iconQuery.IconID = iconID;

                        var newImageURL = string.Empty;

                        var oldImageURL = iconQuery.IconURL;

                        if (iconQuery.Title != TitleTextBox.Text)
                        {
                            if (IconsBL.Exists(TitleTextBox.Text, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                            else
                            {
                                iconQuery.Title = TitleTextBox.Text;
                            }
                        }
                        else
                        {
                            if (FileUpload1.HasFile)
                            {
                                newImageURL = IconsBL.GetUploadedImagePath(FileUpload1);

                                if (FileUpload1.Success)
                                {
                                    iconQuery.IconURL = newImageURL;
                                }
                            }

                            iconQuery.Description = DescriptionTextBox.Text;
                        }

                        try
                        {
                            IconsBL.Save(iconQuery, context);

                            if (FileUpload1.Success)
                            {
                                oldImageURL.DeleteFile();

                                IconImage.ImageUrl = newImageURL;
                            }

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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["IconID"]))
                {
                    using (var context = new ShoppingCartEntities())
                    {
                        var iconID = DataParser.IntParse(Request.QueryString["IconID"]);

                        var iconQuery = IconsBL.GetObjectByID(iconID, context);

                        if (iconQuery != null)
                        {
                            TitleTextBox.Text = iconQuery.Title;
                            DescriptionTextBox.Text = iconQuery.Description;
                            IconImage.ImageUrl = iconQuery.IconURL;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/ShoppingCart/IconsList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/ShoppingCart/IconsList.aspx");
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