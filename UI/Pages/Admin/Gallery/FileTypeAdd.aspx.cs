using OWDARO;
using OWDARO.AppCode.BLL.GalleryBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class FileTypeAdd : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    if (FileTypesBL.Exists(TitleTextBox.Text, context))
                    {
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                    }
                    else
                    {
                        var entity = new GY_FileTypes();
                        entity.Title = TitleTextBox.Text;
                        entity.Description = DescriptionTextBox.Text;
                        entity.Hide = false;

                        try
                        {
                            FileTypesBL.Add(entity, context);
                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;
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
            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}