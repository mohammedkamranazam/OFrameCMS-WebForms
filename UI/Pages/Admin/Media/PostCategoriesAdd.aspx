<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Themes/Zice/Main.Master" AutoEventWireup="true" CodeBehind="PostCategoriesAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Media.PostCategoriesAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New Post Category</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Media/PostCategoriesList.aspx">
                <i class="icon-list-ul"></i> List Post Categories
            </asp:HyperLink>
        </div>
        <div class="content">
            <div class="grid1">
                <OWD:ImageSelectorComponent runat="server" ID="ImageSelectorComponent1" />
            </div>
            <div class="grid3">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static">
                    <ContentTemplate>
                        <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="title of the post category" RequiredErrorMessage="post category title is required"
                            MaxLength="250" />
                        <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the post category" MaxLength="500" TextMode="MultiLine" />
                        <OWD:PostCategoriesSelectComponent runat="server" ID="PostCategoriesSelectComponent1" />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="check to hide the post category" HelpLabelText="show/hide the post category" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="Clear"></div>
        </div>
    </div>
</asp:Content>