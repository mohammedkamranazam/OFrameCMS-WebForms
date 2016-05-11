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
    public partial class ProductTypesManage : System.Web.UI.Page
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

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new ShoppingCartEntities())
                {
                    var productTypeID = DataParser.IntParse(Request.QueryString["ProductTypeID"]);

                    var productTypeQuery = ProductTypesBL.GetObjectByID(productTypeID, context);

                    productTypeQuery.ProductTypeID = productTypeID;

                    if (productTypeQuery.SubCategoryID.ToString() != SubCategoriesDropDownList.SelectedValue)
                    {
                        if (ProductTypesBL.Exists(TitleTextBox.Text, (int)SectionsDropDownList.GetNullableSelectedValue(), (int)CategoriesDropDownList.GetNullableSelectedValue(), (int)SubCategoriesDropDownList.GetNullableSelectedValue(), context))
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                            return;
                        }
                        else
                        {
                            productTypeQuery.SectionID = (int)SectionsDropDownList.GetNullableSelectedValue();
                            productTypeQuery.CategoryID = (int)CategoriesDropDownList.GetNullableSelectedValue();
                            productTypeQuery.SubCategoryID = (int)SubCategoriesDropDownList.GetNullableSelectedValue();
                        }
                    }
                    else
                    {
                        if (productTypeQuery.Title != TitleTextBox.Text)
                        {
                            if (ProductTypesBL.Exists(TitleTextBox.Text, (int)SectionsDropDownList.GetNullableSelectedValue(), (int)CategoriesDropDownList.GetNullableSelectedValue(), (int)SubCategoriesDropDownList.GetNullableSelectedValue(), context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                            else
                            {
                                productTypeQuery.Title = TitleTextBox.Text;
                            }
                        }
                        else
                        {
                            productTypeQuery.Description = DescriptionTextBox.Text;
                            productTypeQuery.Hide = HideCheckBox.Checked;
                        }
                    }
                    try
                    {
                        ProductTypesBL.Save(productTypeQuery, context);
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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["ProductTypeID"]))
                {
                    using (var context = new ShoppingCartEntities())
                    {
                        var productTypeID = DataParser.IntParse(Request.QueryString["ProductTypeID"]);

                        var productTypeQuery = ProductTypesBL.GetObjectByID(productTypeID, context);

                        if (productTypeQuery != null)
                        {
                            TitleTextBox.Text = productTypeQuery.Title;
                            DescriptionTextBox.Text = productTypeQuery.Description;
                            HideCheckBox.Checked = productTypeQuery.Hide;

                            var sectionsQuery = (from sections in context.SC_Sections
                                                 select sections);
                            SectionsDropDownList.DataTextField = "Title";
                            SectionsDropDownList.DataValueField = "SectionID";
                            SectionsDropDownList.DataSource = sectionsQuery.ToList();
                            SectionsDropDownList.AddSelect();
                            SectionsDropDownList.SelectedValue = productTypeQuery.SectionID.ToString();

                            var query = (from categories in context.SC_Categories
                                         where categories.SectionID == productTypeQuery.SectionID
                                         select categories);
                            CategoriesDropDownList.DataTextField = "Title";
                            CategoriesDropDownList.DataValueField = "CategoryID";
                            CategoriesDropDownList.DataSource = query.ToList();
                            CategoriesDropDownList.AddSelect();
                            CategoriesDropDownList.SelectedValue = productTypeQuery.CategoryID.ToString();

                            var subCategoriesQuery = (from subCategories in context.SC_SubCategories
                                                      where subCategories.CategoryID == productTypeQuery.CategoryID
                                                      select subCategories);
                            SubCategoriesDropDownList.DataTextField = "Title";
                            SubCategoriesDropDownList.DataValueField = "SubCategoryID";
                            SubCategoriesDropDownList.DataSource = subCategoriesQuery.ToList();
                            SubCategoriesDropDownList.AddSelect();
                            SubCategoriesDropDownList.SelectedValue = productTypeQuery.SubCategoryID.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/ShoppingCart/ProductTypesList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/ShoppingCart/ProductTypesList.aspx");
                }
            }

            SectionsDropDownList.SelectedIndexChanged += SectionsDropDownList_SelectedIndexChanged;
            CategoriesDropDownList.SelectedIndexChanged += CategoriesDropDownList_SelectedIndexChanged;

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