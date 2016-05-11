using OWDARO;
using OWDARO.Helpers;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace ProjectJKL.UI.Pages.Admin.OFrame
{
    public partial class MenuList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }

            LocaleDropDown.LanguageDirectionChanged += LocaleDropDown_LanguageDirectionChanged;
        }

        private async Task LoadData()
        {
            var locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);
            LocaleDropDown.Locale = locale;
            await MenuComponent1.BuildMenuAsync(locale);
        }

        private async void LocaleDropDown_LanguageDirectionChanged(object sender, OWDARO.OEventArgs.LanguageDirectionChangedEventArgs e)
        {
            await MenuComponent1.BuildMenuAsync(LocaleDropDown.Locale);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}