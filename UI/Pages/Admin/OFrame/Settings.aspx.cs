using OWDARO;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.OFrame
{
    public partial class Settings : System.Web.UI.Page
    {
        private void AllowGuestBuyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                KeywordsHelper.SetKeywordValue("AllowGuestBuy", AllowGuestBuyCheckBox.Checked.ToString());

                StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        private void DefaultUserRegistrationCategoryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UserCategoryHelper.SetDefaultCategoryID(DataParser.IntParse(DefaultUserRegistrationCategoryDropDownList.SelectedValue));

                StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        private void EnableMailSSLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                KeywordsHelper.SetKeywordValue("EnableSsl", EnableMailSSLCheckBox.Checked.ToString());

                StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        private void EnableQACheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                KeywordsHelper.SetKeywordValue("EnableQA", EnableQACheckBox.Checked.ToString());

                StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    SymCryptography smc = new SymCryptography();

                    KeywordsHelper.SetKeywordValue("PerformanceTimeOutMinutes", PerformanceTimeOutMinutesTextBox.Text);
                    KeywordsHelper.SetKeywordValue("CookieTimeOutMinutes", CookieTimeOutMinutesTextBox.Text);
                    KeywordsHelper.SetKeywordValue("DefaultLocale", LocaleDropDownList.Locale);
                    KeywordsHelper.SetKeywordValue("LogoRelativeURL", LogoRelativeURLTextBox.Text);
                    KeywordsHelper.SetKeywordValue("AccessManagerSearchPatters", AccessManagerSearchPatternsTextBox.Text);
                    KeywordsHelper.SetKeywordValue("FileEditorSearchPatterns", FileEditorSearchPatternsTextBox.Text);
                    KeywordsHelper.SetKeywordValue("KeepProductNewForDays", KeepProductNewForDaysTextBox.Text);
                    KeywordsHelper.SetKeywordValue("ProductHotItemSoldOutCount", ProductHotItemSoldOutCountTextBox.Text);
                    KeywordsHelper.SetKeywordValue("MinimumProductRatingToShow", MinimumProductRatingToShowTextBox.Text);
                    KeywordsHelper.SetKeywordValue("MinimumCartAmount", MinimumCartAmountTextBox.Text);
                    KeywordsHelper.SetKeywordValue("ProductLockTimeOutInMinutes", ProductLockTimeOutInMinutesTextBox.Text);
                    KeywordsHelper.SetKeywordValue("MailServer", MailServerTextBox.Text);
                    KeywordsHelper.SetKeywordValue("MailServerPort", MailServerPortTextBox.Text);
                    KeywordsHelper.SetKeywordValue("MailLogOnId", MailLogonIDTextBox.Text);
                    KeywordsHelper.SetKeywordValue("SiteName", SiteNameTextBox.Text);
                    KeywordsHelper.SetKeywordValue("HeaderTitle", HeaderTitleTextBox.Text);
                    KeywordsHelper.SetKeywordValue("HeaderTagLine", HeaderTagLineTextBox.Text);
                    KeywordsHelper.SetKeywordValue("ReceiptAddress", ReceiptAddressEditor.Text);
                    KeywordsHelper.SetKeywordValue("WebsiteAdminEmail", WebsiteAdminEmailTextBox.Text);
                    KeywordsHelper.SetKeywordValue("WebsiteMainEmail", WebsiteMainEmailTextBox.Text);
                    KeywordsHelper.SetKeywordValue("ErrorAdminEmail", ErrorAdminEmailTextBox.Text);
                    KeywordsHelper.SetKeywordValue("HomePagePostID", HomePagePostIDTextBox.Text);
                    KeywordsHelper.SetKeywordValue("HomePageTopBlockPostID", HomePageTopBlockPostIDTextBox.Text);
                    KeywordsHelper.SetKeywordValue("HomePageBottomBlockPostID", HomePageBottomBlockPostIDTextBox.Text);
                    KeywordsHelper.SetKeywordValue("AboutUsPostID", AboutUsPostIDTextBox.Text);
                    KeywordsHelper.SetKeywordValue("ContactUsPostID", ContactUsPostIDTextBox.Text);
                    KeywordsHelper.SetKeywordValue("GoogleAnalyticsCode", GoogleAnalyticsCodeTextBox.Text);
                    KeywordsHelper.SetKeywordValue("DiscussCode", DiscussCodeTextBox.Text);
                    KeywordsHelper.SetKeywordValue("GoogleWebmasterTool", GoogleWebmasterToolTextBox.Text);
                    KeywordsHelper.SetKeywordValue("BingWebmasterCenter", BingWebMasterCenterTextBox.Text);
                    KeywordsHelper.SetKeywordValue("TargetTimeZoneID", TargetTimeZoneIDDropDownList.SelectedValue);
                    KeywordsHelper.SetKeywordValue("IsSiteMultiLingual", IsSiteMultiLingualCheckBox.Checked.ToString());
                    KeywordsHelper.SetKeywordValue("FacebookAPIKey", FacebookAPIKeyTextBix.Text);
                    KeywordsHelper.SetKeywordValue("FacebookSecretKey", FacebookSecretKeyTextBox.Text);
                    KeywordsHelper.SetKeywordValue("GoogleAPIKey", GoogleAPIKeyTextBox.Text);
                    KeywordsHelper.SetKeywordValue("GoogleSecretKey", GoogleSecretKeyTextBox.Text);
                    KeywordsHelper.SetKeywordValue("TwitterAPIKey", TwitterAPIKeyTextBox.Text);
                    KeywordsHelper.SetKeywordValue("TwitterSecretKey", TwitterSecretKeyTextBox.Text);
                    KeywordsHelper.SetKeywordValue("EnableOAuthRegistration", OAuthLoginCheckBox.Checked.ToString());
                    KeywordsHelper.SetKeywordValue("MicrosoftApplicationInsightScript", MAIScriptTextBox.Text);

                    if (!string.IsNullOrWhiteSpace(MailLogonPasswordTextBox.Text))
                    {
                        KeywordsHelper.SetKeywordValue("MailLogOnPassword", smc.Encrypt(MailLogonPasswordTextBox.Text));
                    }

                    StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                    StatusMessage.MessageType = StatusMessageType.Success;
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    StatusMessage.MessageType = StatusMessageType.Error;
                }
            }
        }

        private void InitializeFields()
        {
            DefaultUserRegistrationCategoryDropDownList.SelectedValue = UserCategoryHelper.GetDefaultCategoryIDFromSettings().ToString();
            EnableQACheckBox.Checked = KeywordsHelper.GetKeywordValueFromSettings("EnableQA").BoolParse();
            ShowUserAddPopUpComponentCheckBox.Checked = KeywordsHelper.GetKeywordValueFromSettings("ShowUserAddPopUpComponent").BoolParse();
            AllowGuestBuyCheckBox.Checked = KeywordsHelper.GetKeywordValueFromSettings("AllowGuestBuy").BoolParse();
            UseLoginReturnURLCheckBox.Checked = KeywordsHelper.GetKeywordValueFromSettings("UseLoginReturnURL").BoolParse();
            EnableMailSSLCheckBox.Checked = KeywordsHelper.GetKeywordValueFromSettings("EnableSsl").BoolParse();
            MailSMTPCheckBox.Checked = KeywordsHelper.GetKeywordValueFromSettings("MailSmtp").BoolParse();
            PerformanceTimeOutMinutesTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("PerformanceTimeOutMinutes");
            CookieTimeOutMinutesTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("CookieTimeOutMinutes");
            LocaleDropDownList.Locale = KeywordsHelper.GetKeywordValueFromSettings("DefaultLocale");
            IsSiteMultiLingualCheckBox.Checked = KeywordsHelper.GetKeywordValueFromSettings("IsSiteMultiLingual").BoolParse();
            LogoRelativeURLTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("LogoRelativeURL");
            AccessManagerSearchPatternsTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("AccessManagerSearchPatters");
            FileEditorSearchPatternsTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("FileEditorSearchPatterns");
            KeepProductNewForDaysTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("KeepProductNewForDays");
            ProductHotItemSoldOutCountTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("ProductHotItemSoldOutCount");
            MinimumProductRatingToShowTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("MinimumProductRatingToShow");
            MinimumCartAmountTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("MinimumCartAmount");
            ProductLockTimeOutInMinutesTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("ProductLockTimeOutInMinutes");
            MailServerTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("MailServer");
            MailLogonIDTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("MailLogOnId");
            //MailLogonPasswordTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("MailLogOnPassword");
            MailServerPortTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("MailServerPort");
            SiteNameTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("SiteName");
            HeaderTagLineTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("HeaderTagLine");
            HeaderTitleTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("HeaderTitle");
            ReceiptAddressEditor.Text = KeywordsHelper.GetKeywordValueFromSettings("ReceiptAddress");
            WebsiteAdminEmailTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("WebsiteAdminEmail");
            WebsiteMainEmailTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("WebsiteMainEmail");
            ErrorAdminEmailTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("ErrorAdminEmail");
            HomePagePostIDTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("HomePagePostID");
            HomePageTopBlockPostIDTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("HomePageTopBlockPostID");
            HomePageBottomBlockPostIDTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("HomePageBottomBlockPostID");
            AboutUsPostIDTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("AboutUsPostID");
            ContactUsPostIDTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("ContactUsPostID");
            GoogleAnalyticsCodeTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("GoogleAnalyticsCode");
            DiscussCodeTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("DiscussCode");
            GoogleWebmasterToolTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("GoogleWebmasterTool");
            BingWebMasterCenterTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("BingWebmasterCenter");
            TargetTimeZoneIDDropDownList.DataTextField = "DisplayName";
            TargetTimeZoneIDDropDownList.DataValueField = "Id";
            TargetTimeZoneIDDropDownList.DataSource = TimeZoneInfo.GetSystemTimeZones();
            TargetTimeZoneIDDropDownList.SelectedValue = KeywordsHelper.GetKeywordValueFromSettings("TargetTimeZoneID");

            FacebookAPIKeyTextBix.Text = KeywordsHelper.GetKeywordValueFromSettings("FacebookAPIKey");
            FacebookSecretKeyTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("FacebookSecretKey");
            GoogleAPIKeyTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("GoogleAPIKey");
            GoogleSecretKeyTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("GoogleSecretKey");
            TwitterAPIKeyTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("TwitterAPIKey");
            TwitterSecretKeyTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("TwitterSecretKey");
            OAuthLoginCheckBox.Checked = KeywordsHelper.GetKeywordValueFromSettings("EnableOAuthRegistration").BoolParse();
            MAIScriptTextBox.Text = KeywordsHelper.GetKeywordValueFromSettings("MicrosoftApplicationInsightScript");

            using (var context = new MediaEntities())
            {
                AboutUsPostIDTextBox.PopUpListBox.Height = new Unit("200px");
                ContactUsPostIDTextBox.PopUpListBox.Height = new Unit("200px");
                HomePagePostIDTextBox.PopUpListBox.Height = new Unit("200px");
                HomePageTopBlockPostIDTextBox.PopUpListBox.Height = new Unit("200px");
                HomePageBottomBlockPostIDTextBox.PopUpListBox.Height = new Unit("200px");

                var posts = (from set in context.ME_Posts
                             select set);

                foreach (ME_Posts post in posts)
                {
                    AboutUsPostIDTextBox.PopUpListBox.Items.Add(new ListItem(post.Title, post.PostID.ToString()));
                    ContactUsPostIDTextBox.PopUpListBox.Items.Add(new ListItem(post.Title, post.PostID.ToString()));
                    HomePagePostIDTextBox.PopUpListBox.Items.Add(new ListItem(post.Title, post.PostID.ToString()));
                    HomePageTopBlockPostIDTextBox.PopUpListBox.Items.Add(new ListItem(post.Title, post.PostID.ToString()));
                    HomePageBottomBlockPostIDTextBox.PopUpListBox.Items.Add(new ListItem(post.Title, post.PostID.ToString()));
                }
            }

            switch (KeywordsHelper.GetKeywordValueFromSettings("MemoryCacheItemPriority"))
            {
                case "Default":
                    MemoryCacheItemPriorityDropDownList.SelectedValue = "Default";
                    break;

                case "NotRemovable":
                    MemoryCacheItemPriorityDropDownList.SelectedValue = "NotRemovable";
                    break;

                default:
                    MemoryCacheItemPriorityDropDownList.SelectedValue = "Default";
                    break;
            }

            switch (KeywordsHelper.GetKeywordValueFromSettings("PerformanceMode"))
            {
                case "ApplicationState":
                    PerformanceModeDropDownList.SelectedValue = "ApplicationState";
                    break;

                case "Cache":
                    PerformanceModeDropDownList.SelectedValue = "Cache";
                    break;

                case "MemoryCache":
                    PerformanceModeDropDownList.SelectedValue = "MemoryCache";
                    break;

                case "Session":
                    PerformanceModeDropDownList.SelectedValue = "Session";
                    break;

                default:
                    PerformanceModeDropDownList.SelectedValue = "None";
                    break;
            }
        }

        private void MailSMTPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                KeywordsHelper.SetKeywordValue("MailSmtp", MailSMTPCheckBox.Checked.ToString());

                StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        private void MemoryCacheItemPriorityDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                KeywordsHelper.SetKeywordValue("MemoryCacheItemPriority", MemoryCacheItemPriorityDropDownList.SelectedValue);

                StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        private void PerformanceModeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                KeywordsHelper.SetKeywordValue("PerformanceMode", PerformanceModeDropDownList.SelectedValue);

                StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        private void PopulateCacheItemPriority()
        {
            MemoryCacheItemPriorityDropDownList.Items.Add("Default");
            MemoryCacheItemPriorityDropDownList.Items.Add("NotRemovable");
        }

        private void PopulatePerformanceModes()
        {
            PerformanceModeDropDownList.Items.Add("None");
            PerformanceModeDropDownList.Items.Add("ApplicationState");
            PerformanceModeDropDownList.Items.Add("Cache");
            PerformanceModeDropDownList.Items.Add("MemoryCache");
            PerformanceModeDropDownList.Items.Add("Session");
        }

        private void ShowUserAddPopUpComponentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                KeywordsHelper.SetKeywordValue("ShowUserAddPopUpComponent", ShowUserAddPopUpComponentCheckBox.Checked.ToString());

                StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        private void UseLoginReturnURLCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                KeywordsHelper.SetKeywordValue("UseLoginReturnURL", UseLoginReturnURLCheckBox.Checked.ToString());

                StatusMessage1.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatePerformanceModes();

                PopulateCacheItemPriority();

                UserCategoryBL.PopulateUserCategoryList(DefaultUserRegistrationCategoryDropDownList);

                InitializeFields();
            }

            ReloadAppDomainToolbar.CustomPopupCancelButtonText = "Cancel";
            ReloadAppDomainToolbar.CustomPopupMessage = "Reloading the Application Domain will clear all the caches and restart the App Pool.<br /><br />Are you sure you want to reload the Application Domain?";
            ReloadAppDomainToolbar.CustomPopupOKButtonText = "Reload";
            ReloadAppDomainToolbar.CustomPopupTitle = "Application Domain Restart";
            ReloadAppDomainToolbar.CustomPopupButtonCssClass = "btn btn-warning btn-large";

            FormToolbar1.Save += FormToolbar1_Save;
            ReloadAppDomainToolbar.CustomPopupClick += ReloadAppDomainToolbar_CustomPopupClick;
            EnableQACheckBox.CheckedChanged += EnableQACheckBox_CheckedChanged;
            ShowUserAddPopUpComponentCheckBox.CheckedChanged += ShowUserAddPopUpComponentCheckBox_CheckedChanged;
            PerformanceModeDropDownList.SelectedIndexChanged += PerformanceModeDropDownList_SelectedIndexChanged;
            MemoryCacheItemPriorityDropDownList.SelectedIndexChanged += MemoryCacheItemPriorityDropDownList_SelectedIndexChanged;
            AllowGuestBuyCheckBox.CheckedChanged += AllowGuestBuyCheckBox_CheckedChanged;
            UseLoginReturnURLCheckBox.CheckedChanged += UseLoginReturnURLCheckBox_CheckedChanged;
            EnableMailSSLCheckBox.CheckedChanged += EnableMailSSLCheckBox_CheckedChanged;
            MailSMTPCheckBox.CheckedChanged += MailSMTPCheckBox_CheckedChanged;
            DefaultUserRegistrationCategoryDropDownList.SelectedIndexChanged += DefaultUserRegistrationCategoryDropDownList_SelectedIndexChanged;
        }

        private void ReloadAppDomainToolbar_CustomPopupClick(object sender, EventArgs e)
        {
            try
            {
                HttpRuntime.UnloadAppDomain();

                InitializeFields();

                StatusMessage1.Message = "Application Domain Pool Restarted Successfully";
                StatusMessage1.MessageType = StatusMessageType.Success;
            }
            catch (Exception ex)
            {
                StatusMessage1.Message = ExceptionHelper.GetExceptionMessage(ex);
                StatusMessage1.MessageType = StatusMessageType.Error;
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}