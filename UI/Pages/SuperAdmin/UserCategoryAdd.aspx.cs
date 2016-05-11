using OWDARO.BLL.MembershipBLL;
using OWDARO.Settings;
using ProjectJKL.AppCode.DAL.MembershipModel;
using System;
using System.Linq;
using System.Transactions;

namespace OWDARO.UI.Pages.SuperAdmin
{
    public partial class UserCategoryAdd : System.Web.UI.Page
    {
        private void Formtoolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder());
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new MembershipEntities())
                {
                    var title = TitleTextBox.Text;
                    var description = DescriptionTextBox.Text;

                    var userCategoryQuery = (from userCategories in context.MS_UserCategories
                                             where userCategories.Title == title
                                             select userCategories);

                    if (userCategoryQuery.Any())
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = "User Category already present";
                    }
                    else
                    {
                        var userCategoryNew = new MS_UserCategories();
                        userCategoryNew.Title = title;
                        userCategoryNew.Description = description;

                        using (var scope = new TransactionScope())
                        {
                            context.MS_UserCategories.Add(userCategoryNew);
                            context.SaveChanges();

                            AddPageCategorySettingsComponent.CategoryName = ManagePageCategorySettingsComponent.CategoryName = ListPageCategorySettingsComponent.CategoryName = TitleTextBox.Text;
                            AddPageCategorySettingsComponent.CategoryId = ManagePageCategorySettingsComponent.CategoryId = ListPageCategorySettingsComponent.CategoryId = userCategoryNew.UserCategoryID;

                            UserCategoryHelper.AddCategorySetting(AddPageCategorySettingsComponent.CategorySettings, PageSetting.Add);
                            UserCategoryHelper.AddCategorySetting(ManagePageCategorySettingsComponent.CategorySettings, PageSetting.Manage);
                            UserCategoryHelper.AddCategorySetting(ListPageCategorySettingsComponent.CategorySettings, PageSetting.List);

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = "Record added successfully";

                            scope.Complete();
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Title = String.Format("Cockpit: {0}: {1}", AppConfig.SiteName, "Add User Category");
            }

            FormToolbar1.Save += new EventHandler(FormToolbar1_Save);
            FormToolbar1.Cancel += Formtoolbar1_Cancel;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}