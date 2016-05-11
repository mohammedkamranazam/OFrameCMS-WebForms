using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Media
{
    public partial class TestimonialAdd : System.Web.UI.Page
    {
        private void Add(MediaEntities context, ME_Testimonials entity)
        {
            try
            {
                TestimonialsBL.Add(entity, context);
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

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new MediaEntities())
                {
                    var entity = new ME_Testimonials();
                    entity.Testimonial = TestimonialTextBox.Text;
                    entity.Company = CompanyTextBox.Text;
                    entity.Name = NameTextBox.Text;
                    entity.Position = PositionTextBox.Text;
                    entity.ImageID = ImageSelectorComponent1.ImageID;

                    Add(context, entity);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ImageSelectorComponent1.StoragePath = LocalStorages.Others;
            }

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