<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="UserCategoryAdd.aspx.cs" Inherits="OWDARO.UI.Pages.SuperAdmin.UserCategoryAdd" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color group "></span>New User Category</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="boxtitle">
                <span class="ico color messenger "></span>you can add new user categories from here
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" MaxLength="50"
                        SmallLabelText="user category name" RequiredErrorMessage="title is required" />
                    <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the user category"
                        TextMode="MultiLine" MaxLength="500" />
                    <div class="grid1-3">
                        <OWD:CategorySettingsComponent runat="server" ID="AddPageCategorySettingsComponent"
                            BoxTitle="Fields To Show While Adding User Account" />
                    </div>
                    <div class="grid1-3">
                        <OWD:CategorySettingsComponent runat="server" ID="ManagePageCategorySettingsComponent"
                            BoxTitle="Fields To Show While Managing User Account" />
                    </div>
                    <div class="grid1-3">
                        <OWD:CategorySettingsComponent runat="server" ID="ListPageCategorySettingsComponent"
                            BoxTitle="Fields To Show While Listing User Accounts" />
                    </div>
                    <div class="Clear">
                    </div>
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>