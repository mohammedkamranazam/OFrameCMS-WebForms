<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="UserCategoryManage.aspx.cs" Inherits="OWDARO.UI.Pages.SuperAdmin.UserCategoryManage" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color group "></span>Manage Category </span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="boxtitle">
                <span class="ico color gear "></span>here you can manage the selected category
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" RequiredErrorMessage="title is required"
                        MaxLength="50" />
                    <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" MaxLength="500" />
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
                    <OWD:FormToolbar runat="server" ID="Formtoolbar1" ShowSave="true" ShowCancel="true"
                        ShowDelete="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>