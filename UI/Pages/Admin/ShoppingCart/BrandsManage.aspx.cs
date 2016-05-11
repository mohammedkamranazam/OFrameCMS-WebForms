using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class BrandsManage : System.Web.UI.Page
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
                    var brandID = DataParser.IntParse(Request.QueryString["BrandID"]);

                    var brandQuery = BrandsBL.GetObjectByID(brandID, context);

                    if (brandQuery != null)
                    {
                        brandQuery.BrandID = brandID;

                        var newImageURL = string.Empty;

                        var oldImageURL = brandQuery.ImageURL;

                        if (brandQuery.Title != TitleTextBox.Text)
                        {
                            if (BrandsBL.Exists(TitleTextBox.Text, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                            else
                            {
                                brandQuery.Title = TitleTextBox.Text;
                            }
                        }
                        else
                        {
                            if (FileUpload1.HasFile)
                            {
                                newImageURL = CategoriesBL.GetUploadedImagePath(FileUpload1);

                                if (FileUpload1.Success)
                                {
                                    brandQuery.ImageURL = newImageURL;
                                }
                            }

                            brandQuery.Hide = HideCheckBox.Checked;
                            brandQuery.Description = DescriptionTextBox.Text;
                        }

                        try
                        {
                            BrandsBL.Save(brandQuery, context);

                            if (FileUpload1.Success)
                            {
                                oldImageURL.DeleteFile();

                                BrandImage.ImageUrl = newImageURL;
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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["BrandID"]))
                {
                    using (var context = new ShoppingCartEntities())
                    {
                        var brandID = DataParser.IntParse(Request.QueryString["BrandID"]);

                        var brandQuery = BrandsBL.GetObjectByID(brandID, context);

                        if (brandQuery != null)
                        {
                            TitleTextBox.Text = brandQuery.Title;
                            DescriptionTextBox.Text = brandQuery.Description;
                            HideCheckBox.Checked = brandQuery.Hide;
                            BrandImage.ImageUrl = brandQuery.ImageURL;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/ShoppingCart/BrandsList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/ShoppingCart/BrandsList.aspx");
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