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
    public partial class ProductModelsAdd : System.Web.UI.Page
    {
        private void CategoriesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubCategoriesDropDownList.Visible = true;

            var categoryID = CategoriesDropDownList.GetSelectedValue();

            using (var context = new ShoppingCartEntities())
            {
                var subCategoriesQuery = (from subCategories in context.SC_SubCategories
                                          where subCategories.CategoryID == categoryID
                                          select subCategories);
                SubCategoriesDropDownList.DataTextField = "Title";
                SubCategoriesDropDownList.DataValueField = "SubCategoryID";
                SubCategoriesDropDownList.DataSource = subCategoriesQuery.ToList();
                SubCategoriesDropDownList.AddSelect();
            }
        }

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
                    if (ProductModelsBL.Exists(TitleTextBox.Text, (int)SectionsDropDownList.GetNullableSelectedValue(), (int)CategoriesDropDownList.GetNullableSelectedValue(), (int)SubCategoriesDropDownList.GetNullableSelectedValue(), context))
                    {
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                    }
                    else
                    {
                        var entity = new SC_ProductModels();
                        entity.Title = TitleTextBox.Text;
                        entity.Hide = HideCheckBox.Checked;
                        entity.Description = DescriptionTextBox.Text;
                        entity.SectionID = (int)SectionsDropDownList.GetNullableSelectedValue();
                        entity.CategoryID = (int)CategoriesDropDownList.GetNullableSelectedValue();
                        entity.SubCategoryID = (int)SubCategoriesDropDownList.GetNullableSelectedValue();

                        try
                        {
                            ProductModelsBL.Add(entity, context);
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

            SectionsDropDownList.SelectedIndexChanged += SectionsDropDownList_SelectedIndexChanged;
            CategoriesDropDownList.SelectedIndexChanged += CategoriesDropDownList_SelectedIndexChanged;

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