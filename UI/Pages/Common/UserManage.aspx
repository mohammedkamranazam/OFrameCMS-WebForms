<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Themes/Default/Main.Master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="OWDARO.UI.Pages.Common.UserManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color user"></span>Manage User</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="boxtitle">
                <span class="ico color list "></span>manage user account from here
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <OWD:CheckBoxAdv runat="server" ID="LoginBlockedCheckBox" LabelText="Approved"
                        SmallLabelText="determines whether user is approved or not" HelpLabelText="approve/disapprove user account" AutoPostBack="true" />
                    <OWD:CheckBoxAdv runat="server" ID="UnlockUserCheckBox" LabelText="Is Locked"
                        SmallLabelText="determines whether user is locked or not" HelpLabelText="turn OFF to unlock user" Enabled="false" AutoPostBack="true" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color administrator"></span>Profile Setting</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content" style="min-height: 400px;">
            <div class="boxtitle">
                <span class="ico color webcam "></span>user profile management
            </div>
            <div class="grid1">
                <div class="profileSetting">
                    <div class="avartar">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Image runat="server" ID="ProfilePicImage" Width="100%" Height="100%" />
                                <asp:Button runat="server" ID="DeleteImageButton" CssClass="uibutton special" Text="Delete Image"
                                    Width="100%" CausesValidation="False" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button runat="server" ID="UploadImageButton" CssClass="uibutton" Text="Upload Image"
                            Width="100%" CausesValidation="true" ValidationGroup="ImageUploadGroup" />
                        <br />
                        <br />
                        <asp:FileUpload runat="server" ID="ProfilePicFileUpload" CssClass="fileupload" Style="width: 90%;"
                            ValidationGroup="ImageUploadGroup" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ProfilePicFileUpload"
                            ValidationGroup="ImageUploadGroup" Display="Dynamic" SetFocusOnError="true" ErrorMessage="please select an image"
                            Style="color: red;" />
                        <asp:RegularExpressionValidator runat="server" ID="ImageRegularExpressionValidator"
                            ControlToValidate="ProfilePicFileUpload" ValidationGroup="ImageUploadGroup" Display="Dynamic"
                            SetFocusOnError="true" Style="color: red;" />
                        <p style="text-align: center;">
                            <span>OR </span>Take a picture with <a class="takeWebcam">Webcam</a>
                        </p>
                    </div>
                </div>
            </div>
            <div class="grid3">
                <div class="section webcam">
                    <div id="screen">
                        <div style="height: 100%; text-align: center;">
                            <OWD:WebCamComponent runat="server" ID="WebCamComponent"></OWD:WebCamComponent>
                        </div>
                    </div>
                    <div class="buttonPane" style="margin-left: 0px; border-top: 1px solid #999999; margin-top: 10px; padding-top: 10px;">
                        <a id="closeButton" class="uibutton special">Close</a>
                        <asp:Button runat="server" ID="WebCamUploadButton" Text="Upload" CssClass="uibutton" ValidationGroup="WebCamImageUploadValidationGroup" />
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <OWD:TextBoxAdv runat="server" ID="NameTextBox" RequiredErrorMessage="name is required" LabelText="Name" SmallLabelText="full name of the user"
                            ValidationGroup="ProfileInfoValidationGroup" />
                        <OWD:DropDownListAdv ID="RolesDropDownList" runat="server" LabelText="Role" SmallLabelText="role for site access" RequiredErrorMessage="please select a role for site access"
                            InitialValue="-1" ValidationGroup="ProfileInfoValidationGroup" />
                        <OWD:DropDownListAdv runat="server" ID="CategoryDropDownList" AutoPostBack="true" LabelText="Category" SmallLabelText="category of the user"
                            RequiredFieldErrorMessage="please select a category" ValidationGroup="ProfileInfoValidationGroup" />
                        <OWD:DropDownListAdv runat="server" ID="GenderDropDownList" LabelText="Gender" SmallLabelText="the sex of the user" RequiredErrorMessage="please select a gender"
                            ValidationGroup="ProfileInfoValidationGroup">
                        </OWD:DropDownListAdv>
                        <OWD:TextBoxAdv runat="server" ID="DateOfBirthTextBox" LabelText="Date Of Birth" CalendarDefaultView="Years" RequiredErrorMessage="please enter your date of birth"
                            ValidationGroup="ProfileInfoValidationGroup" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" ValidationGroup="ProfileInfoValidationGroup" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="Clear"></div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div class="onecolumn">
                <div class="header">
                    <span><span class="ico color satellite"></span>Contact Details</span>
                </div>
                <div class="Clear">
                </div>
                <div class="content">

                    <OWD:TextBoxAdv runat="server" ID="EmailTextBox" LabelText="Primary Email" SmallLabelText="email id for all major account operations"
                        MaxLength="100" RequiredErrorMessage="enter the email id" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar2" ShowCancel="true" ShowSave="true" ValidationGroup="ContactDetailsValidationGroup" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage1" />
                </div>
            </div>
            <asp:Panel runat="server" ID="EmailPanel">
                <OWD:UserDataDetailsComponent runat="server" ID="EmailsUserDataDetailsComponent"
                    HeaderIcon="mail" BoxIcon="mail" DataTextBoxMaxLength="200" />
            </asp:Panel>
            <asp:Panel runat="server" ID="MobilePanel">
                <OWD:UserDataDetailsComponent runat="server" ID="MobileUserDataDetailsComponent"
                    HeaderIcon="phone" BoxIcon="phone" DataTextBoxMaxLength="20" />
            </asp:Panel>
            <asp:Panel runat="server" ID="LandlinePanel">
                <OWD:UserDataDetailsComponent runat="server" ID="LandlineUserDataDetailsComponent"
                    HeaderIcon="phone" BoxIcon="phone" DataTextBoxMaxLength="20" />
            </asp:Panel>
            <asp:Panel runat="server" ID="FaxPanel">
                <OWD:UserDataDetailsComponent runat="server" ID="FaxUserDataDetailsComponent" HeaderIcon="print"
                    BoxIcon="print" DataTextBoxMaxLength="20" />
            </asp:Panel>
            <asp:Panel runat="server" ID="WebsitePanel">
                <OWD:UserDataDetailsComponent runat="server" ID="WebsiteUserDataDetailsComponent"
                    HeaderIcon="link" BoxIcon="link" DataTextBoxMaxLength="150" />
            </asp:Panel>
            <asp:Panel runat="server" ID="WorkPanel">
                <OWD:UserWorkDetailsComponent runat="server" ID="WorkDetailsComponent" />
            </asp:Panel>
            <asp:Panel runat="server" ID="AddressPanel">
                <OWD:UserAddressDetailsComponent runat="server" ID="AddressDetailsComponent" />
            </asp:Panel>
            <asp:Panel runat="server" ID="EducationPanel">
                <OWD:UserEducationDetailsComponent runat="server" ID="EducationDetailsComponent" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>