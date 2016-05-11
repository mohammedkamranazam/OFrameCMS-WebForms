using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.BLL.OFrameBLL;
using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.Settings;
using OWDARO.UI.UserControls.Controls;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.OFrame
{
    public partial class MenuManage : Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private async void FormToolbar1_Delete(object sender, EventArgs e)
        {
            using (var context = new OWDAROEntities())
            {
                var menuID = DataParser.IntParse(Request.QueryString["MenuID"]);

                var menuQuery = await MenuBL.GetObjectByIDAsync(menuID, context);

                if (menuQuery != null)
                {
                    string locale = menuQuery.Locale;

                    if (menuQuery.ChildMenus.Any())
                    {
                        StatusMessage.MessageType = StatusMessageType.Warning;
                        StatusMessage.Message = Constants.Messages.RELATED_RECORD_EXISTS_MESSAGE;
                    }
                    else
                    {
                        try
                        {
                            await MenuBL.DeleteAsync(menuQuery, context);

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;

                            await MenuComponent1.BuildMenuAsync(locale);
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

        private async void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new OWDAROEntities())
                {
                    var menuID = DataParser.IntParse(Request.QueryString["MenuID"]);

                    var menuQuery = await MenuBL.GetObjectByIDAsync(menuID, context);

                    if (menuQuery != null)
                    {
                        menuQuery.Title = TitleTextBox.Text;
                        menuQuery.Hide = HideCheckBox.Checked;
                        menuQuery.IsRoot = IsRootCheckBox.Checked;
                        menuQuery.NavigateURL = NavigateURLTextBox.Text;
                        menuQuery.Locale = LocaleDropDown.Locale;

                        if (string.IsNullOrWhiteSpace(PositionTextBox.Text))
                        {
                            menuQuery.Position = await MenuBL.GetPositionAsync(context, LocaleDropDown.Locale);
                        }
                        else
                        {
                            menuQuery.Position = DataParser.IntParse(PositionTextBox.Text);
                        }

                        if (!IsRootCheckBox.Checked)
                        {
                            menuQuery.ParentMenuID = RootParentMenuIDDropDownList.GetNullableSelectedValue();

                            if (Level1ParentMenuIDDropDownList.Visible)
                            {
                                if (Level1ParentMenuIDDropDownList.GetNullableSelectedValue() != null)
                                {
                                    menuQuery.ParentMenuID = Level1ParentMenuIDDropDownList.GetNullableSelectedValue();
                                }
                            }
                        }

                        try
                        {
                            await MenuBL.ShiftPositionsAsync((int)menuQuery.Position, menuQuery.IsRoot, menuQuery.ParentMenuID, LocaleDropDown.Locale, context);
                            await MenuBL.SaveAsync(context);

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;

                            await MenuComponent1.BuildMenuAsync(LocaleDropDown.Locale);

                            if (!menuQuery.IsRoot)
                            {
                                await InitializeLevel1DropDownAsync(context);
                            }
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

        private async Task InitializeLevel1DropDownAsync(OWDAROEntities context)
        {
            if (await PopulateLevel1TabsAsync(context) > 0)
            {
                Level1ParentMenuIDDropDownList.Visible = true;
            }
            else
            {
                Level1ParentMenuIDDropDownList.Visible = false;
            }
        }

        private async void IsRootCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (IsRootCheckBox.Checked)
            {
                Level1ParentMenuIDDropDownList.Visible = false;
                RootParentMenuIDDropDownList.Visible = false;
            }
            else
            {
                RootParentMenuIDDropDownList.Visible = true;
                Level1ParentMenuIDDropDownList.Visible = false;
            }

            if (!IsRootCheckBox.Checked)
            {
                using (var context = new OWDAROEntities())
                {
                    await PopulateRootTabsAsync(context);
                }
            }
        }

        private void ModulesMenuSelectionComponent1_URLGenerated(object sender, URLGeneratedEventArgs e)
        {
            NavigateURLTextBox.Text = e.URL;
        }

        private async Task<int> PopulateLevel1TabsAsync(OWDAROEntities context)
        {
            var parentMenuID = DataParser.IntParse(RootParentMenuIDDropDownList.SelectedValue);

            var level1TabsQuery = await MenuBL.GetLevel1TabsAsync(parentMenuID, LocaleDropDown.Locale, context);

            if (level1TabsQuery.Any())
            {
                Level1ParentMenuIDDropDownList.Visible = true;

                Level1ParentMenuIDDropDownList.DataTextField = "Title";
                Level1ParentMenuIDDropDownList.DataValueField = "MenuID";
                Level1ParentMenuIDDropDownList.DataSource = level1TabsQuery;
                Level1ParentMenuIDDropDownList.AddSelect();

                await RemoveCurrentMenuAsync(Level1ParentMenuIDDropDownList, context);
            }

            return level1TabsQuery.Count();
        }

        private async Task PopulateRootTabsAsync(OWDAROEntities context)
        {
            RootParentMenuIDDropDownList.DataTextField = "Title";
            RootParentMenuIDDropDownList.DataValueField = "MenuID";
            RootParentMenuIDDropDownList.DataSource = await MenuBL.GetRootTabsAsync(context, LocaleDropDown.Locale);
            RootParentMenuIDDropDownList.AddSelect();

            await RemoveCurrentMenuAsync(RootParentMenuIDDropDownList, context);
        }

        private async Task RemoveCurrentMenuAsync(DropDownListAdv dropDown, OWDAROEntities context)
        {
            var menuID = DataParser.IntParse(HttpContext.Current.Request.QueryString["MenuID"]);

            var menuQuery = await (from set in context.OW_Menu
                                   where set.MenuID == menuID
                                   select set).FirstOrDefaultAsync();

            if (menuQuery != null)
            {
                dropDown.Items.Remove(new ListItem(menuQuery.Title, menuQuery.MenuID.ToString()));
            }
        }

        private async void RootParentMenuIDDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new OWDAROEntities())
            {
                await PopulateLevel1TabsAsync(context);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["MenuID"]))
                {
                    this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                    {
                        await LoadData();
                    }));
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/OFrame/MenuList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }

            PositionTextBox.ValidationExpression = Validator.IntegerValidationExpression;
            PositionTextBox.ValidationErrorMessage = Validator.IntegerValidationErrorMessage;

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            IsRootCheckBox.CheckedChanged += IsRootCheckBox_CheckedChanged;
            RootParentMenuIDDropDownList.SelectedIndexChanged += RootParentMenuIDDropDownList_SelectedIndexChanged;
            ModulesMenuSelectionComponent1.URLGenerated += ModulesMenuSelectionComponent1_URLGenerated;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, LanguageDirectionChangedEventArgs e)
        {
            InitializeLocaleFields(e.Direction);

            ModulesMenuSelectionComponent1.Locale = LocaleDropDown.Locale;

            await ModulesMenuSelectionComponent1.LoadData();
            await MenuComponent1.BuildMenuAsync(LocaleDropDown.Locale);

            MenuBL.PopulateNavigateURLs(NavigateURLTextBox, LocaleDropDown.Locale);
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
        }

        private async Task LoadData()
        {
            using (var context = new OWDAROEntities())
            {
                var menuID = DataParser.IntParse(Request.QueryString["MenuID"]);

                var menuQuery = await MenuBL.GetObjectByIDAsync(menuID, context);

                if (menuQuery != null)
                {
                    var locale = menuQuery.Locale;
                    LocaleDropDown.Locale = locale;
                    InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

                    TitleTextBox.Text = menuQuery.Title;
                    PositionTextBox.Text = menuQuery.Position.ToString();
                    IsRootCheckBox.Checked = menuQuery.IsRoot;
                    NavigateURLTextBox.Text = menuQuery.NavigateURL;
                    HideCheckBox.Checked = menuQuery.Hide;
                    MenuComponent1.Locale = locale;

                    NavigateURLTextBox.PopUpListBox.Height = new Unit("200px");
                    MenuBL.PopulateNavigateURLs(NavigateURLTextBox, menuQuery.Locale);

                    ModulesMenuSelectionComponent1.Locale = LocaleDropDown.Locale;

                    await ModulesMenuSelectionComponent1.LoadData();
                    await MenuComponent1.BuildMenuAsync(locale);

                    if (menuQuery.IsRoot)
                    {
                        RootParentMenuIDDropDownList.Visible = false;
                        Level1ParentMenuIDDropDownList.Visible = false;
                    }
                    else
                    {
                        RootParentMenuIDDropDownList.Visible = true;
                    }

                    if (!menuQuery.IsRoot)
                    {
                        await PopulateRootTabsAsync(context);

                        var rootItem = RootParentMenuIDDropDownList.Items.FindByValue(menuQuery.ParentMenuID.ToString());

                        if (rootItem != null)
                        {
                            RootParentMenuIDDropDownList.SelectedValue = menuQuery.ParentMenuID.ToString();

                            await InitializeLevel1DropDownAsync(context);
                        }
                        else
                        {
                            var nonRootParentQuery = await MenuBL.GetObjectByIDAsync((int)menuQuery.ParentMenuID, context);

                            var rootOfParentQuery = nonRootParentQuery.ParentMenu;

                            RootParentMenuIDDropDownList.SelectedValue = rootOfParentQuery.MenuID.ToString();

                            await InitializeLevel1DropDownAsync(context);

                            Level1ParentMenuIDDropDownList.SelectedValue = menuQuery.ParentMenuID.ToString();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/UI/Pages/Admin/OFrame/MenuList.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}