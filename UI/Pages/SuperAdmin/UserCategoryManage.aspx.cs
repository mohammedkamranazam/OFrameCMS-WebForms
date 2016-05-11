using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Linq;
using System.Transactions;

namespace OWDARO.UI.Pages.SuperAdmin
{
    public partial class UserCategoryManage : System.Web.UI.Page
    {
        private void Formtoolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/SuperAdmin/UserCategoryList.aspx");
        }

        private void Formtoolbar1_Delete(object sender, EventArgs e)
        {
            using (var context = new MembershipEntities())
            {
                var userCategoryID = DataParser.IntParse(Request.QueryString["UserCategoryID"]);

                var userCategory = UserCategoryBL.GetCategoryByID(userCategoryID, context);

                if (userCategory != null)
                {
                    if (userCategory.MS_Users.Any())
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = "Record cannot be deleted as it has related records";
                    }
                    else
                    {
                        context.MS_UserCategories.Remove(userCategory);

                        try
                        {
                            context.SaveChanges();

                            Formtoolbar1.RedirectToRelativeUrl = ResolveUrl("~/UI/Pages/SuperAdmin/UserCategoryList.aspx");
                            Formtoolbar1.ShowMessagePopup("Delete Status", String.Format("User Category [ {0} ] was successfully deleted", userCategory.Title));
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        }
                    }
                }
                else
                {
                    StatusMessage.MessageType = StatusMessageType.Info;
                    StatusMessage.Message = "record not present";
                }
            }
        }

        private void Formtoolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new MembershipEntities())
                {
                    var userCategoryID = DataParser.IntParse(Request.QueryString["UserCategoryID"]);

                    var userCategory = UserCategoryBL.GetCategoryByID(userCategoryID, context);

                    if (userCategory != null)
                    {
                        userCategory.Description = DescriptionTextBox.Text;
                        userCategory.Title = TitleTextBox.Text;

                        using (var scope = new TransactionScope())
                        {
                            try
                            {
                                context.SaveChanges();

                                AddPageCategorySettingsComponent.CategoryName = ManagePageCategorySettingsComponent.CategoryName = ListPageCategorySettingsComponent.CategoryName = TitleTextBox.Text;
                                AddPageCategorySettingsComponent.CategoryId = ManagePageCategorySettingsComponent.CategoryId = ListPageCategorySettingsComponent.CategoryId = userCategoryID;

                                UserCategoryHelper.SetCategorySetting(AddPageCategorySettingsComponent.CategorySettings, PageSetting.Add);
                                UserCategoryHelper.SetCategorySetting(ManagePageCategorySettingsComponent.CategorySettings, PageSetting.Manage);
                                UserCategoryHelper.SetCategorySetting(ListPageCategorySettingsComponent.CategorySettings, PageSetting.List);

                                StatusMessage.MessageType = StatusMessageType.Success;
                                StatusMessage.Message = "Successfully saved";
                            }
                            catch (Exception ex)
                            {
                                ErrorLogger.LogError(ex);
                                StatusMessage.MessageType = StatusMessageType.Error;
                                StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                            }

                            scope.Complete();
                        }
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = "no such record present";
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = String.Format("Cockpit: {0}: {1}", AppConfig.SiteName, "Manage User Category");

                if (string.IsNullOrWhiteSpace(Request.QueryString["UserCategoryID"]))
                {
                    Response.Redirect(UserBL.GetRootFolder());
                }

                using (var context = new MembershipEntities())
                {
                    var userCategoryID = DataParser.IntParse(Request.QueryString["UserCategoryID"]);

                    var userCategoryQuery = (from userCategories in context.MS_UserCategories
                                             where userCategories.UserCategoryID == userCategoryID
                                             select userCategories);

                    if (userCategoryQuery.Any())
                    {
                        var userCategory = userCategoryQuery.FirstOrDefault();
                        TitleTextBox.Text = userCategory.Title;
                        DescriptionTextBox.Text = userCategory.Description;

                        AddPageCategorySettingsComponent.CategorySettings = UserCategoryHelper.GetCategorySetting(userCategoryID, PageSetting.Add);
                        ManagePageCategorySettingsComponent.CategorySettings = UserCategoryHelper.GetCategorySetting(userCategoryID, PageSetting.Manage);
                        ListPageCategorySettingsComponent.CategorySettings = UserCategoryHelper.GetCategorySetting(userCategoryID, PageSetting.List);
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = "No User Category found";
                    }
                }
            }

            Formtoolbar1.Save += new EventHandler(Formtoolbar1_Save);
            Formtoolbar1.Cancel += new EventHandler(Formtoolbar1_Cancel);
            Formtoolbar1.Delete += new EventHandler(Formtoolbar1_Delete);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}