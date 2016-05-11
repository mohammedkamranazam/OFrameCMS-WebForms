using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Media
{
    public partial class TestimonialManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Admin/Media/TestimonialList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            using (var context = new MediaEntities())
            {
                var testimonialID = DataParser.IntParse(Request.QueryString["TestimonialID"]);

                try
                {
                    TestimonialsBL.Delete(testimonialID, context);

                    StatusMessage.MessageType = StatusMessageType.Success;
                    StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                }
            }
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new MediaEntities())
                {
                    var testimonialID = DataParser.IntParse(Request.QueryString["TestimonialID"]);

                    var entity = TestimonialsBL.GetObjectByID(testimonialID, context);

                    entity.Testimonial = TestimonialTextBox.Text;
                    entity.Name = NameTextBox.Text;
                    entity.Company = CompanyTextBox.Text;
                    entity.Position = PositionTextBox.Text;
                    entity.ImageID = ImageSelectorComponent1.ImageID;

                    Save(entity, context);
                }
            }
        }

        private bool Save(ME_Testimonials entity, MediaEntities context)
        {
            try
            {
                TestimonialsBL.Save(context);

                StatusMessage.MessageType = StatusMessageType.Success;
                StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                StatusMessage.MessageType = StatusMessageType.Error;
                StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);

                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["TestimonialID"]))
                {
                    Response.Redirect("~/UI/Pages/Admin/Media/TestimonialList.aspx");
                }

                using (var context = new MediaEntities())
                {
                    var testimonialID = DataParser.IntParse(Request.QueryString["TestimonialID"]);

                    var entity = TestimonialsBL.GetObjectByID(testimonialID, context);

                    if (entity != null)
                    {
                        TestimonialTextBox.Text = entity.Testimonial;
                        CompanyTextBox.Text = entity.Company;
                        NameTextBox.Text = entity.Name;
                        PositionTextBox.Text = entity.Position;
                        ImageSelectorComponent1.ImageID = entity.ImageID;
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                    }
                }

                ImageSelectorComponent1.StoragePath = LocalStorages.Others;
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += new EventHandler(FormToolbar1_Cancel);
            FormToolbar1.Delete += new EventHandler(FormToolbar1_Delete);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}