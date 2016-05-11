<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="ChangeProfile.aspx.cs" Inherits="OWDARO.UI.Pages.Common.ChangeProfile" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
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
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:Image runat="server" ID="ProfilePicImage" Width="100%" Height="100%" />
                                <asp:Button runat="server" ID="DeleteImageButton" CssClass="uibutton special" Text="Delete Image" Width="100%" OnClick="DeleteImageButton_Click" CausesValidation="False" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Button runat="server" ID="UploadImageButton" CssClass="uibutton" Text="Upload Image" Width="100%" OnClick="UploadImageButton_Click" CausesValidation="true" ValidationGroup="ImageUploadGroup" />
                        <br />
                        <br />
                        <asp:FileUpload runat="server" ID="ProfilePicFileUpload" CssClass="fileupload" Style="width: 90%;" ValidationGroup="ImageUploadGroup" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ProfilePicFileUpload" ValidationGroup="ImageUploadGroup" Display="Dynamic" SetFocusOnError="true" ErrorMessage="please select an image" Style="color: red;" />
                        <asp:RegularExpressionValidator runat="server" ID="ImageRegularExpressionValidator" ControlToValidate="ProfilePicFileUpload" ValidationGroup="ImageUploadGroup" Display="Dynamic" SetFocusOnError="true" Style="color: red;" />
                        <p style="text-align: center;">
                            <span>OR </span>Take a picture with <a class="takeWebcam">Web cam</a>
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
                        <asp:Button runat="server" ID="WebCamUploadButton" Text="Upload" CssClass="uibutton" OnClick="WebCamUploadButton_Click" />
                    </div>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <OWD:TextBoxAdv runat="server" ID="NameTextBox" RequiredErrorMessage="name is required" LabelText="Name" SmallLabelText="full name of the user" AutoCompleteType="DisplayName" />
                        <asp:Panel runat="server" ID="RolesPanel" Visible="false">
                            <OWD:DropDownListAdv ID="RolesDropDownList" runat="server" LabelText="Role" SmallLabelText="role for site access" RequiredErrorMessage="please select a role for site access" InitialValue="-1" />
                        </asp:Panel>
                        <asp:Panel runat="server" ID="CategoryPanel" Visible="false">
                            <OWD:DropDownListAdv runat="server" ID="CategoryDropDownList" AutoPostBack="true" LabelText="Category" SmallLabelText="category of the user" RequiredFieldErrorMessage="please select a category" />
                        </asp:Panel>
                        <asp:Panel runat="server" ID="GenderPanel" Visible="false">
                            <OWD:DropDownListAdv runat="server" ID="GenderDropDownList" LabelText="Gender" SmallLabelText="the sex of the user" RequiredErrorMessage="please select a gender"></OWD:DropDownListAdv>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="DateOfBirthPanel" Visible="false">
                            <OWD:TextBoxAdv runat="server" ID="DateOfBirthTextBox" LabelText="Date Of Birth" CalendarDefaultView="Years" RequiredErrorMessage="please enter your date of birth" />
                        </asp:Panel>
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="Clear"></div>
        </div>
    </div>
</asp:content>