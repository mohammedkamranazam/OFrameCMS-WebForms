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
    public partial class SizesManage : System.Web.UI.Page
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
                    var sizeID = DataParser.IntParse(Request.QueryString["SizeID"]);

                    var sizeQuery = SizesBL.GetObjectByID(sizeID, context);

                    sizeQuery.SizeID = sizeID;

                    if (sizeQuery.SubCategoryID.ToString() != SubCategoriesDropDownList.SelectedValue)
                    {
                        if (SizesBL.Exists(TitleTextBox.Text, (int)SectionsDropDownList.GetNullableSelectedValue(), (int)CategoriesDropDownList.GetNullableSelectedValue(), (int)SubCategoriesDropDownList.GetNullableSelectedValue(), context))
                        {
                            StatusMessage.MessageType = StatusMessageType.Info;
                            StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                            return;
                        }
                        else
                        {
                            sizeQuery.SectionID = (int)SectionsDropDownList.GetNullableSelectedValue();
                            sizeQuery.CategoryID = (int)CategoriesDropDownList.GetNullableSelectedValue();
                            sizeQuery.SubCategoryID = (int)SubCategoriesDropDownList.GetNullableSelectedValue();
                        }
                    }
                    else
                    {
                        if (sizeQuery.Title != TitleTextBox.Text)
                        {
                            if (SizesBL.Exists(TitleTextBox.Text, (int)SectionsDropDownList.GetNullableSelectedValue(), (int)CategoriesDropDownList.GetNullableSelectedValue(), (int)SubCategoriesDropDownList.GetNullableSelectedValue(), context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                            else
                            {
                                sizeQuery.Title = TitleTextBox.Text;
                            }
                        }
                        else
                        {
                            sizeQuery.Description = DescriptionTextBox.Text;
                        }
                    }
                    try
                    {
                        SizesBL.Save(sizeQuery, context);
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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["SizeID"]))
                {
                    using (var context = new ShoppingCartEntities())
                    {
                        var sizeID = DataParser.IntParse(Request.QueryString["SizeID"]);

                        var sizeQuery = SizesBL.GetObjectByID(sizeID, context);

                        if (sizeQuery != null)
                        {
                            TitleTextBox.Text = sizeQuery.Title;
                            DescriptionTextBox.Text = sizeQuery.Description;

                            var sectionsQuery = (from sections in context.SC_Sections
                                                 select sections);
                            SectionsDropDownList.DataTextField = "Title";
                            SectionsDropDownList.DataValueField = "SectionID";
                            SectionsDropDownList.DataSource = sectionsQuery.ToList();
                            SectionsDropDownList.AddSelect();
                            SectionsDropDownList.SelectedValue = sizeQuery.SectionID.ToString();

                            var query = (from categories in context.SC_Categories
                                         where categories.SectionID == sizeQuery.SectionID
                                         select categories);
                            CategoriesDropDownList.DataTextField = "Title";
                            CategoriesDropDownList.DataValueField = "CategoryID";
                            CategoriesDropDownList.DataSource = query.ToList();
                            CategoriesDropDownList.AddSelect();
                            CategoriesDropDownList.SelectedValue = sizeQuery.CategoryID.ToString();

                            var subCategoriesQuery = (from subCategories in context.SC_SubCategories
                                                      where subCategories.CategoryID == sizeQuery.CategoryID
                                                      select subCategories);
                            SubCategoriesDropDownList.DataTextField = "Title";
                            SubCategoriesDropDownList.DataValueField = "SubCategoryID";
                            SubCategoriesDropDownList.DataSource = subCategoriesQuery.ToList();
                            SubCategoriesDropDownList.AddSelect();
                            SubCategoriesDropDownList.SelectedValue = sizeQuery.SubCategoryID.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/ShoppingCart/SizesList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/ShoppingCart/SizesList.aspx");
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