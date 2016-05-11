using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;
using System.Linq;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class CategoriesAdd : System.Web.UI.Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new ShoppingCartEntities())
                {
                    if (CategoriesBL.Exists(TitleTextBox.Text, (int)SectionsDropDownList.GetNullableSelectedValue()))
                    {
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                    }
                    else
                    {
                        var categoryEntity = new SC_Categories();
                        categoryEntity.Title = TitleTextBox.Text;
                        categoryEntity.Description = DescriptionTextBox.Text;
                        categoryEntity.Hide = false;
                        categoryEntity.ImageURL = CategoriesBL.GetUploadedImagePath(FileUpload1);
                        categoryEntity.ImageThumbURL = CategoriesBL.GetUploadedImageThumbnailPath(FileUpload1);
                        categoryEntity.SectionID = (int)SectionsDropDownList.GetNullableSelectedValue();

                        if (FileUpload1.Success)
                        {
                            try
                            {
                                CategoriesBL.Add(categoryEntity);
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
                        else
                        {
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = FileUpload1.Message;
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FileUpload1.ValidationExpression = Validator.ImageValidationExpression;
                FileUpload1.ValidationErrorMessage = Validator.ImageValidationErrorMessage;

                using (var context = new ShoppingCartEntities())
                {
                    var query = (from sections in context.SC_Sections
                                 select sections);

                    SectionsDropDownList.DataTextField = "Title";
                    SectionsDropDownList.DataValueField = "SectionID";
                    SectionsDropDownList.DataSource = query.ToList();
                    SectionsDropDownList.AddSelect();
                }
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