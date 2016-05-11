using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.Media
{
    public partial class PortfolioManage : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Admin/Media/PortfolioList.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
            var portfolioID = DataParser.IntParse(Request.QueryString["PortfolioID"]);

            using (var context = new MediaEntities())
            {
                if (PortfoliosBL.RelatedRecordsExists(portfolioID, context))
                {
                    StatusMessage.MessageType = StatusMessageType.Warning;
                    StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                }
                else
                {
                    try
                    {
                        PortfoliosBL.Delete(portfolioID, context);

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
                    var portfolioID = DataParser.IntParse(Request.QueryString["PortfolioID"]);

                    var portfolioEntity = PortfoliosBL.GetObjectByID(portfolioID, context);

                    if (portfolioEntity != null)
                    {
                        portfolioEntity.Title = TitleTextBox.Text;
                        portfolioEntity.Description = DescriptionEditor.Text;
                        portfolioEntity.Date = DataParser.NullableDateTimeParse(DateTextBox.Text);
                        portfolioEntity.ClientID = ClientIDDropDownList.GetSelectedValue();
                        portfolioEntity.URL = URLTextBox.Text;
                        portfolioEntity.ImageID = ImageSelectorComponent1.ImageID;

                        Save(portfolioEntity, context);
                    }
                }
            }
        }

        private void FormToolbar2_CustomClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var portfolioID = DataParser.IntParse(Request.QueryString["PortfolioID"]);

                using (var context = new MediaEntities())
                {
                    if (PortfolioCategoriesBL.Exists(ProjectCategoriesDropDownList.GetSelectedValue(), portfolioID, context))
                    {
                        StatusMessage2.MessageType = StatusMessageType.Info;
                        StatusMessage2.Message = Constants.Messages.ITEM_ALREADY_PRESENT;

                        return;
                    }

                    var entity = new ME_PortfolioCategories();

                    entity.ProjectCategoryID = ProjectCategoriesDropDownList.GetSelectedValue();
                    entity.PortfolioID = portfolioID;

                    try
                    {
                        PortfolioCategoriesBL.Add(entity, context);
                        StatusMessage2.MessageType = StatusMessageType.Success;
                        StatusMessage2.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;
                        LoadPortfolioProjectCategories(context);
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        StatusMessage2.MessageType = StatusMessageType.Error;
                        StatusMessage2.Message = ExceptionHelper.GetExceptionMessage(ex);
                    }
                }
            }
        }

        private void ImageUploaderComponent1_ImageUploaded(object sender, OWDARO.OEventArgs.ImageUploadedEventArgs e)
        {
            using (var context = new MediaEntities())
            {
                var id = DataParser.IntParse(Request.QueryString["PortfolioID"]);

                var entity = PortfoliosBL.GetObjectByID(id, context);

                if (entity != null)
                {
                    entity.ImageID = e.ImageID;

                    Save(entity, context);
                }
            }
        }

        private void LoadPortfolioProjectCategories()
        {
            using (var context = new MediaEntities())
            {
                LoadPortfolioProjectCategories(context);
            }
        }

        private void LoadPortfolioProjectCategories(MediaEntities context)
        {
            var portfolioID = DataParser.IntParse(Request.QueryString["PortfolioID"]);

            var query = (from set in context.ME_PortfolioCategories
                         where set.PortfolioID == portfolioID
                         select new
                        {
                            Title = set.ME_ProjectCategories.Title,
                            ID = set.PortfolioCategoryID
                        });

            GridView1.DataSource = query.ToList();
            GridView1.DataBind();
        }

        private bool Save(ME_Portfolios entity, MediaEntities context)
        {
            try
            {
                PortfoliosBL.Save(context);

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

        protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            using (var context = new MediaEntities())
            {
                var portfolioCategoryID = DataParser.IntParse(e.CommandName);

                try
                {
                    PortfolioCategoriesBL.Delete(portfolioCategoryID, context);

                    StatusMessage2.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                    StatusMessage2.MessageType = StatusMessageType.Success;

                    LoadPortfolioProjectCategories(context);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage2.Message = ExceptionHelper.GetExceptionMessage(ex);
                    StatusMessage2.MessageType = StatusMessageType.Error;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["PortfolioID"]))
                {
                    var portfolioID = DataParser.IntParse(Request.QueryString["PortfolioID"]);

                    using (var context = new MediaEntities())
                    {
                        var clientsQuery = (from set in context.ME_Clients
                                            select set);

                        ClientIDDropDownList.DataSource = clientsQuery.ToList();
                        ClientIDDropDownList.DataTextField = "Title";
                        ClientIDDropDownList.DataValueField = "ClientID";
                        ClientIDDropDownList.DataBind();
                        ClientIDDropDownList.AddSelect();

                        var projectCategoriesQuery = (from set in context.ME_ProjectCategories
                                                      select set);

                        ProjectCategoriesDropDownList.DataSource = projectCategoriesQuery.ToList();
                        ProjectCategoriesDropDownList.DataTextField = "Title";
                        ProjectCategoriesDropDownList.DataValueField = "ProjectCategoryID";
                        ProjectCategoriesDropDownList.DataBind();
                        ProjectCategoriesDropDownList.AddSelect();

                        var portfolioQuery = PortfoliosBL.GetObjectByID(portfolioID, context);

                        if (portfolioQuery != null)
                        {
                            TitleTextBox.Text = portfolioQuery.Title;
                            DescriptionEditor.Text = portfolioQuery.Description;
                            DateTextBox.Text = DataParser.GetDateFormattedString(portfolioQuery.Date);
                            ClientIDDropDownList.SelectedValue = portfolioQuery.ClientID.ToString();
                            URLTextBox.Text = portfolioQuery.URL;
                            ImageSelectorComponent1.ImageID = portfolioQuery.ImageID;

                            LoadPortfolioProjectCategories(context);
                        }
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                        }
                    }

                    ImageSelectorComponent1.StoragePath = LocalStorages.Others;
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/Media/PortfolioList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }

            DateTextBox.DateFormat = Validator.DateParseExpression;
            DateTextBox.Format = Validator.DateParseExpression;
            DateTextBox.SmallLabelText = "date format is: " + Validator.DateParseExpression;
            DateTextBox.ValidationErrorMessage = Validator.CalendarValidationErrorMessage;
            DateTextBox.ValidationExpression = Validator.CalendarValidationExpression;

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            FormToolbar2.CustomClick += FormToolbar2_CustomClick;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}