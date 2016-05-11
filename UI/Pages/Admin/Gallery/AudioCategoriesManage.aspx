<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true"
    CodeBehind="AudioCategoriesManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.AudioCategoriesManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Edit Audio Category</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioCategoriesList.aspx">
                <i class="icon-list-ul"></i> List Audio Categories
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/AudioCategoriesAdd.aspx">
                <i class="icon-plus-sign"></i> Add Audio Category
                </asp:HyperLink>
            </div>
        <div class="content">
            <asp:UpdatePanel runat="server" ClientIDMode="Static" id="UpdatePanel1">
                <ContentTemplate>
                    <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                    <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the audio category" RequiredErrorMessage="audio category name is required" MaxLength="50" />
                    <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the audio category" MaxLength="200" TextMode="MultiLine" />
                    <owd:AudioCategoriesSelectComponent runat="server" id="AudioCategoriesSelectComponent1" />
                    <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the audio category" HelpLabelText="switch on to hide this audio category" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>