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
    public partial class SubCategoriesManage : System.Web.UI.Page
    {
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
                    var subCategoryID = DataParser.IntParse(Request.QueryString["SubCategoryID"]);

                    var subCategoryQuery = SubCategoriesBL.GetObjectByID(subCategoryID, context);

                    if (subCategoryQuery != null)
                    {
                        subCategoryQuery.SubCategoryID = subCategoryID;

                        var newImageURL = string.Empty;
                        var newImageThumbURL = string.Empty;
                        var oldImageURL = subCategoryQuery.ImageURL;
                        var oldImageThumbURL = subCategoryQuery.ImageThumbURL;

                        if (subCategoryQuery.CategoryID.ToString() != CategoriesDropDownList.SelectedValue)
                        {
                            if (SubCategoriesBL.Exists(TitleTextBox.Text, (int)CategoriesDropDownList.GetNullableSelectedValue(), context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                            else
                            {
                                subCategoryQuery.CategoryID = (int)CategoriesDropDownList.GetNullableSelectedValue();
                            }
                        }
                        else
                        {
                            if (subCategoryQuery.Title != TitleTextBox.Text)
                            {
                                if (SubCategoriesBL.Exists(TitleTextBox.Text, (int)CategoriesDropDownList.GetNullableSelectedValue(), context))
                                {
                                    StatusMessage.MessageType = StatusMessageType.Info;
                                    StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                    return;
                                }
                                else
                                {
                                    subCategoryQuery.Title = TitleTextBox.Text;
                                }
                            }
                            else
                            {
                                if (FileUpload1.HasFile)
                                {
                                    newImageURL = SubCategoriesBL.GetUploadedImagePath(FileUpload1);
                                    newImageThumbURL = SubCategoriesBL.GetUploadedImageThumbnailPath(FileUpload1);

                                    if (FileUpload1.Success)
                                    {
                                        subCategoryQuery.ImageURL = newImageURL;
                                        subCategoryQuery.ImageThumbURL = newImageThumbURL;
                                    }
                                }

                                subCategoryQuery.Hide = HideCheckBox.Checked;
                                subCategoryQuery.Description = DescriptionTextBox.Text;
                            }
                        }
                        try
                        {
                            SubCategoriesBL.Save(subCategoryQuery, context);

                            if (FileUpload1.Success)
                            {
                                oldImageURL.DeleteFile();
                                oldImageThumbURL.DeleteFile();
                                SubCategoryImage.ImageUrl = newImageThumbURL;
                            }

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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["SubCategoryID"]))
                {
                    using (var context = new ShoppingCartEntities())
                    {
                        var subCategoryID = DataParser.IntParse(Request.QueryString["SubCategoryID"]);

                        var subCategoryQuery = SubCategoriesBL.GetObjectByID(subCategoryID, context);

                        if (subCategoryQuery != null)
                        {
                            TitleTextBox.Text = subCategoryQuery.Title;
                            DescriptionTextBox.Text = subCategoryQuery.Description;
                            HideCheckBox.Checked = subCategoryQuery.Hide;
                            SubCategoryImage.ImageUrl = subCategoryQuery.ImageThumbURL;

                            var sectionsQuery = (from sections in context.SC_Sections
                                                 select sections);
                            SectionsDropDownList.DataTextField = "Title";
                            SectionsDropDownList.DataValueField = "SectionID";
                            SectionsDropDownList.DataSource = sectionsQuery.ToList();
                            SectionsDropDownList.AddSelect();
                            SectionsDropDownList.SelectedValue = subCategoryQuery.SC_Categories.SectionID.ToString();

                            var query = (from categories in context.SC_Categories
                                         where categories.SectionID == subCategoryQuery.SC_Categories.SectionID
                                         select categories);
                            CategoriesDropDownList.DataTextField = "Title";
                            CategoriesDropDownList.DataValueField = "CategoryID";
                            CategoriesDropDownList.DataSource = query.ToList();
                            CategoriesDropDownList.AddSelect();
                            CategoriesDropDownList.SelectedValue = subCategoryQuery.CategoryID.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/ShoppingCart/SubCategoriesList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/ShoppingCart/SubCategoriesList.aspx");
                }
            }

            SectionsDropDownList.SelectedIndexChanged += SectionsDropDownList_SelectedIndexChanged;

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