using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.BLL.OFrameBLL;
using OWDARO.Helpers;
using OWDARO.OEventArgs;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.OFrame
{
    public partial class MenuAdd : Page
    {
        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(UserBL.GetRootFolder(), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private async void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new OWDAROEntities())
                {
                    var entity = new OW_Menu();
                    entity.Title = TitleTextBox.Text;
                    entity.Hide = false;
                    entity.IsRoot = IsRootCheckBox.Checked;
                    entity.NavigateURL = NavigateURLTextBox.Text;
                    entity.Locale = LocaleDropDown.Locale;

                    if (string.IsNullOrWhiteSpace(PositionTextBox.Text))
                    {
                        entity.Position = await MenuBL.GetPositionAsync(context, LocaleDropDown.Locale);
                    }
                    else
                    {
                        entity.Position = DataParser.IntParse(PositionTextBox.Text);
                    }

                    if (!IsRootCheckBox.Checked)
                    {
                        entity.ParentMenuID = RootParentMenuIDDropDownList.GetNullableSelectedValue();

                        if (Level1ParentMenuIDDropDownList.Visible)
                        {
                            if (Level1ParentMenuIDDropDownList.GetNullableSelectedValue() != null)
                            {
                                entity.ParentMenuID = Level1ParentMenuIDDropDownList.GetNullableSelectedValue();
                            }
                        }
                    }

                    try
                    {
                        await MenuBL.ShiftPositionsAsync((int)entity.Position, entity.IsRoot, entity.ParentMenuID, LocaleDropDown.Locale, context);
                        await MenuBL.AddAsync(entity, context);

                        StatusMessage.MessageType = StatusMessageType.Success;
                        StatusMessage.Message = Constants.Messages.ADD_SUCCESS_MESSAGE;

                        await MenuComponent1.BuildMenuAsync(LocaleDropDown.Locale);

                        if (!entity.IsRoot)
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
            }

            return level1TabsQuery.Count();
        }

        private async Task PopulateRootTabsAsync(OWDAROEntities context)
        {
            RootParentMenuIDDropDownList.DataTextField = "Title";
            RootParentMenuIDDropDownList.DataValueField = "MenuID";
            RootParentMenuIDDropDownList.DataSource = await MenuBL.GetRootTabsAsync(context, LocaleDropDown.Locale);
            RootParentMenuIDDropDownList.AddSelect();
        }

        private async void RootParentMenuIDDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var context = new OWDAROEntities())
            {
                await InitializeLevel1DropDownAsync(context);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }

            PositionTextBox.ValidationExpression = Validator.IntegerValidationExpression;
            PositionTextBox.ValidationErrorMessage = Validator.IntegerValidationErrorMessage;

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            IsRootCheckBox.CheckedChanged += IsRootCheckBox_CheckedChanged;
            RootParentMenuIDDropDownList.SelectedIndexChanged += RootParentMenuIDDropDownList_SelectedIndexChanged;
            ModulesMenuSelectionComponent1.URLGenerated += ModulesMenuSelectionComponent1_URLGenerated;
            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private async Task LoadData()
        {
            var locale = AppConfig.DefaultLocale;
            InitializeLocaleFields(LanguageHelper.GetLocaleDirection(locale));

            ModulesMenuSelectionComponent1.Locale = locale;
            LocaleDropDown.Locale = locale;

            await MenuComponent1.BuildMenuAsync(locale);
            await ModulesMenuSelectionComponent1.LoadData();

            NavigateURLTextBox.PopUpListBox.Height = new Unit("200px");
            MenuBL.PopulateNavigateURLs(NavigateURLTextBox, locale);
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, LanguageDirectionChangedEventArgs e)
        {
            var locale = LocaleDropDown.Locale;
            InitializeLocaleFields(e.Direction);

            ModulesMenuSelectionComponent1.Locale = locale;

            await ModulesMenuSelectionComponent1.LoadData();
            await MenuComponent1.BuildMenuAsync(locale);

            MenuBL.PopulateNavigateURLs(NavigateURLTextBox, locale);
        }

        private void InitializeLocaleFields(string direction)
        {
            TitleTextBox.Direction = direction;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}