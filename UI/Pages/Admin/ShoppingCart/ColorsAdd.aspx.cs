using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.ShoppingCartModel;
using ProjectJKL.BLL.ShoppingCartBLL;
using System;

namespace ProjectJKL.UI.Pages.Admin.ShoppingCart
{
    public partial class ColorsAdd : System.Web.UI.Page
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
                    if (ColorsBL.Exists(TitleTextBox.Text, context))
                    {
                        StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                    }
                    else
                    {
                        var colorsEntity = new SC_Colors();
                        colorsEntity.Title = TitleTextBox.Text;
                        colorsEntity.Name = NameTextBox.Text;
                        colorsEntity.Hex = (HexTextBox.Text.StartsWith("#")) ? HexTextBox.Text : string.Format("#{0}", HexTextBox.Text);
                        colorsEntity.ImageURL = ColorsBL.GetUploadedImagePath(FileUpload1);

                        if (FileUpload1.Success)
                        {
                            try
                            {
                                ColorsBL.Add(colorsEntity, context);
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