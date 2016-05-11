<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="MenuManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.OFrame.MenuManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
        <ContentTemplate>
            <div class="onecolumn">
                <div class="header">
                    <span><span class="ico color window"></span>Edit Menu</span>
                </div>
                <div class="Clear">
                </div>
                <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink runat="server" CssClass="btn btn-primary" NavigateUrl="~/UI/Pages/Admin/OFrame/MenuList.aspx">
                            <i class="icon-arrow-left"></i> Go Back To Menu List
                        </asp:HyperLink>
                </div>
                <div class="content">
                    <OWD:MenuComponent ID="MenuComponent1" runat="server" RootCssClass="ManageHeaderMenu" AllowLinkManagement="True" />
                    <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                    <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Name" SmallLabelText="enter a name for the menu" RequiredErrorMessage="album name is required" MaxLength="100" />
                    <OWD:TextBoxAdv runat="server" ID="PositionTextBox" LabelText="Postition" SmallLabelText="position in the menu" MaxLength="7" FilterType="Numbers" FieldWidth="small" />
                    <OWD:CheckBoxAdv runat="server" ID="IsRootCheckBox" LabelText="Is Root Tab" SmallLabelText="determines if it is the top most tab" AutoPostBack="true" Checked="true" />
                    <OWD:DropDownListAdv runat="server" ID="RootParentMenuIDDropDownList" LabelText="Root Tab" SmallLabelText="the parent root tab of this menu tab" InitialValue="-1" RequiredFieldErrorMessage="please select a parent menu tab" Visible="false" AutoPostBack="true" OnChange="return OnChange(this);" />
                    <OWD:DropDownListAdv runat="server" ID="Level1ParentMenuIDDropDownList" LabelText="Level 1 Tab" SmallLabelText="the level 1 tab of this menu tab" Visible="false" />
                    <OWD:ModulesMenuSelectionComponent runat="server" ID="ModulesMenuSelectionComponent1" />
                    <OWD:TextBoxAdv runat="server" ID="NavigateURLTextBox" LabelText="Navigation URL" SmallLabelText="the url of the tab" RequiredErrorMessage="navigation url is required" EnablePopUp="true" PopUpPosition="Bottom" />
                    <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="check to hide the menu item" HelpLabelText="show/hide the menu item" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:content>