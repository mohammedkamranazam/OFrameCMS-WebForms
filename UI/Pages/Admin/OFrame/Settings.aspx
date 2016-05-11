<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.OFrame.Settings" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            
                <div class="onecolumn">
                    <div class="header">
                        <span><span class="ico color key"></span>Keywords</span>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="content">
                        <div class="boxtitle">
                            <span class="ico color key"></span>Manage application keywords from here
                        </div>
                        <OWD:CheckBoxAdv runat="server" ID="EnableQACheckBox" LabelText="Enable QA" AutoPostBack="true" />
                        <OWD:CheckBoxAdv runat="server" ID="ShowUserAddPopUpComponentCheckBox" LabelText="Show PopUp For Adding User" AutoPostBack="true" />
                        <OWD:DropDownListAdv runat="server" ID="PerformanceModeDropDownList" LabelText="Performance Mode" AutoPostBack="true" />
                        <OWD:DropDownListAdv runat="server" ID="MemoryCacheItemPriorityDropDownList" LabelText="Memory Cache Item Priority" AutoPostBack="true" />
                        <OWD:CheckBoxAdv runat="server" ID="AllowGuestBuyCheckBox" LabelText="Allow Guest Buy" AutoPostBack="true" />
                        <OWD:CheckBoxAdv runat="server" ID="UseLoginReturnURLCheckBox" LabelText="Use Login Return URL" AutoPostBack="true" />
                        <OWD:CheckBoxAdv runat="server" ID="EnableMailSSLCheckBox" LabelText="Enable Mail SSL" AutoPostBack="true" />
                        <OWD:CheckBoxAdv runat="server" ID="MailSMTPCheckBox" LabelText="Enable SMTP Mail" AutoPostBack="true" />
                        <OWD:DropDownListAdv runat="server" ID="DefaultUserRegistrationCategoryDropDownList" LabelText="Default User Category For Registration" AutoPostBack="true" />
                        <OWD:FormToolbar runat="server" ID="ReloadAppDomainToolbar" ShowCustomPopupButton="true" CustomPopupButtonText="Reload App Domain" CustomPopUpButtonCausesValidation="false" ValidationGroup="ReloadAppDomain" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage1" />
                    </div>
                </div>
            
                <div class="column_left">

                    <div class="onecolumn">
                    <div class="header">
                        <span><span class="ico color key"></span>Keywords</span>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="content">
                        <div class="boxtitle">
                            <span class="ico color key"></span>Manage application keywords from here
                        </div>
                        
                         <OWD:TextBoxAdv runat="server" ID="SiteNameTextBox" LabelText="Site Name" RequiredErrorMessage="required field" ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="HeaderTitleTextBox" LabelText="Header Title" RequiredErrorMessage="required field" ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="HeaderTagLineTextBox" LabelText="Header Tag Line" RequiredErrorMessage="required field" ValidationGroup="SaveKeywordSettings" />
                        <OWD:CKEditor runat="server" ID="ReceiptAddressEditor" LabelText="Receipt Address"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:UnsafeTextBox runat="server" ID="DiscussCodeTextBox" LabelText="Discuss Code" EditorHeight="100px"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:UnsafeTextBox runat="server" ID="GoogleAnalyticsCodeTextBox" LabelText="Google Analytics Code" EditorHeight="100px"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="GoogleWebmasterToolTextBox" LabelText="Google Webmaster"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="BingWebMasterCenterTextBox" LabelText="Bing Webmaster Center"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:CheckBoxAdv runat="server" ID="OAuthLoginCheckBox" LabelText="Enable OAuth Login"  />
                        <owd:TextBoxAdv runat="server" ID="FacebookAPIKeyTextBix" LabelText="Facebook API Key"  ValidationGroup="SaveKeywordSettings" />
                        <owd:TextBoxAdv runat="server" ID="FacebookSecretKeyTextBox" LabelText="Facebook Secret Key"  ValidationGroup="SaveKeywordSettings" />
                        <owd:TextBoxAdv runat="server" ID="GoogleAPIKeyTextBox" LabelText="Google API Key"  ValidationGroup="SaveKeywordSettings" />
                        <owd:TextBoxAdv runat="server" ID="GoogleSecretKeyTextBox" LabelText="Google Secret Key"  ValidationGroup="SaveKeywordSettings" />
                        <owd:TextBoxAdv runat="server" ID="TwitterAPIKeyTextBox" LabelText="Twitter API Key"  ValidationGroup="SaveKeywordSettings" />
                        <owd:TextBoxAdv runat="server" ID="TwitterSecretKeyTextBox" LabelText="Twitter Secret Key"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:UnsafeTextBox runat="server" ID="MAIScriptTextBox" LabelText="Microsoft Application Insight Script" EditorHeight="100px"  ValidationGroup="SaveKeywordSettings" />
                    </div>
                </div>

                    </div>

            <div class="column_right">
                <div class="onecolumn">
                    <div class="header">
                        <span><span class="ico color key"></span>Keywords</span>
                    </div>
                    <div class="Clear">
                    </div>
                    <div class="content">
                        <div class="boxtitle">
                            <span class="ico color key"></span>Manage application keywords from here
                        </div>                       
                        <OWD:TextBoxAdv runat="server" ID="LogoRelativeURLTextBox" LabelText="Logo URL" SmallLabelText="relative url path of the logo" RequiredErrorMessage="required field"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="PerformanceTimeOutMinutesTextBox" LabelText="Performance Time Out Minutes" RequiredErrorMessage="required field" FilterType="Numbers" MaxLength="7"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="CookieTimeOutMinutesTextBox" LabelText="Cookie Time Out Minutes" RequiredErrorMessage="required field" FilterType="Numbers" MaxLength="7"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:DropDownListAdv runat="server" ID="TargetTimeZoneIDDropDownList" LabelText="Time Zone"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:CheckBoxAdv runat="server" ID="IsSiteMultiLingualCheckBox" LabelText="Enable Multilingual" />
                        <OWD:Locales runat="server" ID="LocaleDropDownList" LabelText="Application Default Language"  ValidationGroup="SaveKeywordSettings" />                        
                        <OWD:TextBoxAdv runat="server" ID="AccessManagerSearchPatternsTextBox" LabelText="Access Manager Search Patterns" RequiredErrorMessage="required field"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="FileEditorSearchPatternsTextBox" LabelText="File Editor Search Patterns" RequiredErrorMessage="required field"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="HomePagePostIDTextBox" LabelText="Home Page Post ID" RequiredErrorMessage="required field" EnablePopUp="true" PopUpPosition="Bottom"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="HomePageTopBlockPostIDTextBox" LabelText="Home Page Top Block Post ID" EnablePopUp="true" PopUpPosition="Bottom"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="HomePageBottomBlockPostIDTextBox" LabelText="Home Page Bottom Block Post ID" EnablePopUp="true" PopUpPosition="Bottom"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="AboutUsPostIDTextBox" LabelText="About Us Post ID" RequiredErrorMessage="required field" EnablePopUp="true" PopUpPosition="Bottom"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="ContactUsPostIDTextBox" LabelText="Contact Us Post ID" RequiredErrorMessage="required field" EnablePopUp="true" PopUpPosition="Bottom"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="KeepProductNewForDaysTextBox" LabelText="Keep Product New For Days" RequiredErrorMessage="required field" FilterType="Numbers" MaxLength="7"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="ProductHotItemSoldOutCountTextBox" LabelText="Product Hot Item Sold Count" RequiredErrorMessage="required field" FilterType="Numbers" MaxLength="7"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="MinimumProductRatingToShowTextBox" LabelText="Minimum Product rating To Show" RequiredErrorMessage="required field" FilterType="Numbers" MaxLength="7"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="MinimumCartAmountTextBox" LabelText="Minimum Allowable Cart Amount" RequiredErrorMessage="required field" FilterType="Numbers" MaxLength="7"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="ProductLockTimeOutInMinutesTextBox" LabelText="Product Lock Timeout Minutes" RequiredErrorMessage="required field" FilterType="Numbers" MaxLength="7"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="MailServerTextBox" LabelText="Mail Server" RequiredErrorMessage="required field"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="MailLogonIDTextBox" LabelText="Mail Logon ID" RequiredErrorMessage="required field"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="MailLogonPasswordTextBox" LabelText="Mail Logon Password" TextMode="Password" AutoCompleteType="None"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="MailServerPortTextBox" LabelText="Mail Server Port" RequiredErrorMessage="required field"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="WebsiteMainEmailTextBox" LabelText="Website Main Email" RequiredErrorMessage="required field"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="WebsiteAdminEmailTextBox" LabelText="Website Admin Email" RequiredErrorMessage="required field"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:TextBoxAdv runat="server" ID="ErrorAdminEmailTextBox" LabelText="Error Admin Email" RequiredErrorMessage="required field"  ValidationGroup="SaveKeywordSettings" />
                        
                    </div>
                </div>
            </div>
            <div class="Clear">
            </div>
            <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true"  ValidationGroup="SaveKeywordSettings" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>
