using OWDARO;
using OWDARO.AppCode.BLL.GalleryBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class FileTypeManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var fileTypeID = DataParser.IntParse(Request.QueryString["FileTypeID"]);

            using (var context = new GalleryEntities())
            {
                if (FileTypesBL.RelatedRecordsExists(fileTypeID, context))
                {
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Warning;
                }
                else
                {
                    try
                    {
                        FileTypesBL.Delete(fileTypeID, context);
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
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    var fileTypeID = DataParser.IntParse(Request.QueryString["FileTypeID"]);

                    var fileTypeQuery = FileTypesBL.GetObjectByID(fileTypeID, context);

                    if (fileTypeQuery.Title != TitleTextBox.Text)
                    {
                        if (FileTypesBL.Exists(TitleTextBox.Text, context))
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                            return;
                        }
                    }

                    fileTypeQuery.Title = TitleTextBox.Text;
                    fileTypeQuery.Description = DescriptionTextBox.Text;
                    fileTypeQuery.Hide = HideCheckBox.Checked;

                    try
                    {
                        FileTypesBL.Save(context);
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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["FileTypeID"]))
                {
                    using (var context = new GalleryEntities())
                    {
                        var fileTypeID = DataParser.IntParse(Request.QueryString["FileTypeID"]);

                        var fileTypeQuery = FileTypesBL.GetObjectByID(fileTypeID, context);

                        if (fileTypeQuery != null)
                        {
                            TitleTextBox.Text = fileTypeQuery.Title;
                            DescriptionTextBox.Text = fileTypeQuery.Description;
                            HideCheckBox.Checked = fileTypeQuery.Hide;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/Gallery/FileTypeList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/FileTypeList.aspx");
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