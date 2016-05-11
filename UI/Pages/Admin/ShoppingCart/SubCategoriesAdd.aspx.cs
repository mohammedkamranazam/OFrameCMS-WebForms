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
    public partial class SubCategoriesAdd : System.Web.UI.Page
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
                    if (SubCategoriesBL.Exists(TitleTextBox.Text, (int)CategoriesDropDownList.GetNullableSelectedValue()))
                    {
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                    }
                    else
                    {
                        var subCategoriesEntity = new SC_SubCategories();
                        subCategoriesEntity.Title = TitleTextBox.Text;
                        subCategoriesEntity.Description = DescriptionTextBox.Text;
                        subCategoriesEntity.Hide = false;
                        subCategoriesEntity.ImageURL = SubCategoriesBL.GetUploadedImagePath(FileUpload1);
                        subCategoriesEntity.ImageThumbURL = SubCategoriesBL.GetUploadedImageThumbnailPath(FileUpload1);
                        subCategoriesEntity.CategoryID = (int)CategoriesDropDownList.GetNullableSelectedValue();

                        if (FileUpload1.Success)
                        {
                            try
                            {
                                SubCategoriesBL.Add(subCategoriesEntity);
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

        private void SectionsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoriesDropDownList.Visible = true;

            var sectionID = SectionsDropDownList.GetSelectedValue();

            using (var context = new ShoppingCartEntities())
            {
                var query = (from categories in context.SC_Categories
                             where categories.SectionID == sectionID
                             select categories);

                CategoriesDropDownList.DataTextField = "Title";
                CategoriesDropDownList.DataValueField = "CategoryID";
                CategoriesDropDownList.DataSource = query.ToList();
                CategoriesDropDownList.AddSelect();
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
                    var sectionsQuery = (from sections in context.SC_Sections
                                         select sections);

                    SectionsDropDownList.DataTextField = "Title";
                    SectionsDropDownList.DataValueField = "SectionID";
                    SectionsDropDownList.DataSource = sectionsQuery.ToList();
                    SectionsDropDownList.AddSelect();
                }
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            SectionsDropDownList.SelectedIndexChanged += SectionsDropDownList_SelectedIndexChanged;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}