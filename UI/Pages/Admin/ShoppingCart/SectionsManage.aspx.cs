using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class SectionsManage : System.Web.UI.Page
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
                    var sectionID = DataParser.IntParse(Request.QueryString["SectionID"]);

                    var sectionQuery = SectionsBL.GetObjectByID(sectionID, context);

                    if (sectionQuery != null)
                    {
                        sectionQuery.SectionID = sectionID;

                        var newImageURL = string.Empty;
                        var newImageThumbURL = string.Empty;
                        var oldImageURL = sectionQuery.ImageURL;
                        var oldImageThumbURL = sectionQuery.ImageThumbURL;

                        if (sectionQuery.Title != TitleTextBox.Text)
                        {
                            if (SectionsBL.Exists(TitleTextBox.Text, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                            else
                            {
                                sectionQuery.Title = TitleTextBox.Text;
                            }
                        }
                        else
                        {
                            if (FileUpload1.HasFile)
                            {
                                newImageURL = SectionsBL.GetUploadedImagePath(FileUpload1);
                                newImageThumbURL = SectionsBL.GetUploadedImageThumbnailPath(FileUpload1);

                                if (FileUpload1.Success)
                                {
                                    sectionQuery.ImageURL = newImageURL;
                                    sectionQuery.ImageThumbURL = newImageThumbURL;
                                }
                            }

                            sectionQuery.Hide = HideCheckBox.Checked;
                            sectionQuery.Description = DescriptionTextBox.Text;
                        }

                        try
                        {
                            SectionsBL.Save(sectionQuery, context);

                            if (FileUpload1.Success)
                            {
                                oldImageURL.DeleteFile();
                                oldImageThumbURL.DeleteFile();
                                SectionImage.ImageUrl = newImageThumbURL;
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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["SectionID"]))
                {
                    using (var context = new ShoppingCartEntities())
                    {
                        var sectionID = DataParser.IntParse(Request.QueryString["SectionID"]);

                        var sectionQuery = SectionsBL.GetObjectByID(sectionID, context);

                        if (sectionQuery != null)
                        {
                            TitleTextBox.Text = sectionQuery.Title;
                            DescriptionTextBox.Text = sectionQuery.Description;
                            HideCheckBox.Checked = sectionQuery.Hide;
                            SectionImage.ImageUrl = sectionQuery.ImageThumbURL;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/ShoppingCart/SectionsList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/ShoppingCart/SectionsList.aspx");
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