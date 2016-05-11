using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class ColorsManage : System.Web.UI.Page
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
                    var colorID = DataParser.IntParse(Request.QueryString["ColorID"]);

                    var colorQuery = ColorsBL.GetObjectByID(colorID, context);

                    if (colorQuery != null)
                    {
                        colorQuery.ColorID = colorID;

                        var newImageURL = string.Empty;

                        var oldImageURL = colorQuery.ImageURL;

                        if (colorQuery.Title != TitleTextBox.Text)
                        {
                            if (ColorsBL.Exists(TitleTextBox.Text, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                            else
                            {
                                colorQuery.Title = TitleTextBox.Text;
                            }
                        }
                        else
                        {
                            if (FileUpload1.HasFile)
                            {
                                newImageURL = CategoriesBL.GetUploadedImagePath(FileUpload1);

                                if (FileUpload1.Success)
                                {
                                    colorQuery.ImageURL = newImageURL;
                                }
                            }

                            colorQuery.Name = NameTextBox.Text;
                            colorQuery.Hex = HexTextBox.Text;
                        }

                        try
                        {
                            ColorsBL.Save(colorQuery, context);

                            if (FileUpload1.Success)
                            {
                                oldImageURL.DeleteFile();

                                ColorImage.ImageUrl = newImageURL;
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
                if (!string.IsNullOrWhiteSpace(Request.QueryString["ColorID"]))
                {
                    using (var context = new ShoppingCartEntities())
                    {
                        var colorID = DataParser.IntParse(Request.QueryString["ColorID"]);

                        var colorQuery = ColorsBL.GetObjectByID(colorID, context);

                        if (colorQuery != null)
                        {
                            TitleTextBox.Text = colorQuery.Title;
                            NameTextBox.Text = colorQuery.Name;
                            HexTextBox.Text = colorQuery.Hex;
                            ColorImage.ImageUrl = colorQuery.ImageURL;
                        }
                        else
                        {
                            Response.Redirect("~/UI/Pages/Admin/ShoppingCart/ColorsList.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/ShoppingCart/ColorsList.aspx");
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