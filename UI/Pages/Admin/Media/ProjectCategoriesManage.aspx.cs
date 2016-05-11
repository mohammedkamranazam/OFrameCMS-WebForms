using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;

namespace ProjectJKL.UI.Pages.Admin.Media
{
    public partial class ProjectCategoriesManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Admin/Media/ProjectCategoriesList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            using (var context = new MediaEntities())
            {
                var projectCategoryID = DataParser.IntParse(Request.QueryString["ProjectCategoryID"]);

                if (ProjectCategoriesBL.RelatedRecordsExists(projectCategoryID, context))
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                }
                else
                {
                    try
                    {
                        ProjectCategoriesBL.Delete(projectCategoryID, context);

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
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new MediaEntities())
                {
                    var projectCategoryID = DataParser.IntParse(Request.QueryString["ProjectCategoryID"]);

                    var entity = ProjectCategoriesBL.GetObjectByID(projectCategoryID, context);

                    if (entity.Title != TitleTextBox.Text)
                    {
                        if (ProjectCategoriesBL.Exists(TitleTextBox.Text, context))
                        {
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                            return;
                        }
                    }

                    entity.Title = TitleTextBox.Text;
                    entity.Description = DescriptionTextBox.Text;

                    try
                    {
                        ProjectCategoriesBL.Save(context);

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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["ProjectCategoryID"]))
                {
                    using (var context = new MediaEntities())
                    {
                        var projectCategoryID = DataParser.IntParse(Request.QueryString["ProjectCategoryID"]);

                        var entity = ProjectCategoriesBL.GetObjectByID(projectCategoryID, context);

                        if (entity != null)
                        {
                            TitleTextBox.Text = entity.Title;
                            DescriptionTextBox.Text = entity.Description;
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Media/ProjectCategoriesList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
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