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
    public partial class CategoriesManage : System.Web.UI.Page
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
                    var categoryID = DataParser.IntParse(Request.QueryString["CategoryID"]);

                    var categoryQuery = CategoriesBL.GetObjectByID(categoryID, context);

                    if (categoryQuery != null)
                    {
                        categoryQuery.CategoryID = categoryID;

                        var newImageURL = string.Empty;
                        var newImageThumbURL = string.Empty;
                        var oldImageURL = categoryQuery.ImageURL;
                        var oldImageThumbURL = categoryQuery.ImageThumbURL;

                        if (categoryQuery.SectionID.ToString() != SectionsDropDownList.SelectedValue)
                        {
                            if (CategoriesBL.Exists(TitleTextBox.Text, (int)SectionsDropDownList.GetNullableSelectedValue(), context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                            else
                            {
                                categoryQuery.SectionID = (int)SectionsDropDownList.GetNullableSelectedValue();
                            }
                        }
                        else
                        {
                            if (categoryQuery.Title != TitleTextBox.Text)
                            {
                                if (CategoriesBL.Exists(TitleTextBox.Text, (int)SectionsDropDownList.GetNullableSelectedValue(), context))
                                {
                                    StatusMessage.MessageType = StatusMessageType.Info;
                                    StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                    return;
                                }
                                else
                                {
                                    categoryQuery.Title = TitleTextBox.Text;
                                }
                            }
                            else
                            {
                                if (FileUpload1.HasFile)
                                {
                                    newImageURL = CategoriesBL.GetUploadedImagePath(FileUpload1);
                                    newImageThumbURL = CategoriesBL.GetUploadedImageThumbnailPath(FileUpload1);

                                    if (FileUpload1.Success)
                                    {
                                        categoryQuery.ImageURL = newImageURL;
                                        categoryQuery.ImageThumbURL = newImageThumbURL;
                                    }
                                }

                                categoryQuery.Hide = HideCheckBox.Checked;
                                categoryQuery.Description = DescriptionTextBox.Text;
                            }
                        }
                        try
                        {
                            CategoriesBL.Save(categoryQuery, context);

                            if (FileUpload1.Success)
                            {
                                oldImageURL.DeleteFile();
                                oldImageThumbURL.DeleteFile();
                                CategoryImage.ImageUrl = newImageThumbURL;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["CategoryID"]))
                {
                    using (var context = new ShoppingCartEntities())
                    {
                        var categoryID = DataParser.IntParse(Request.QueryString["CategoryID"]);

                        var categoryQuery = CategoriesBL.GetObjectByID(categoryID, context);

                        if (categoryQuery != null)
                        {
                            TitleTextBox.Text = categoryQuery.Title;
                            DescriptionTextBox.Text = categoryQuery.Description;
                            HideCheckBox.Checked = categoryQuery.Hide;
                            CategoryImage.ImageUrl = categoryQuery.ImageThumbURL;

                            var sectionsQuery = (from sections in context.SC_Sections
                                                 select sections);
                            SectionsDropDownList.DataTextField = "Title";
                            SectionsDropDownList.DataValueField = "SectionID";
                            SectionsDropDownList.DataSource = sectionsQuery.ToList();
                            SectionsDropDownList.AddSelect();
                            SectionsDropDownList.SelectedValue = categoryQuery.SectionID.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/ShoppingCart/CategoriesList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/ShoppingCart/CategoriesList.aspx");
                }
            }

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