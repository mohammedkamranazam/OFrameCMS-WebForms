<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true"
    CodeBehind="VideoCategoriesManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.VideoCategoriesManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Edit Video Category</span>
        </div>
        <div class="Clear">
        </div>
         <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoCategoriesList.aspx">
                            <i class="icon-list-ul"></i> List Video Categories
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/VideoCategoriesAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Video Category
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static">
                <ContentTemplate>
                    <div class="grid1">
                        <OWD:ImageSelectorComponent runat="server" ID="ImageSelectorComponent1" />
                    </div>
                    <div class="grid3">
                        <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the video category" RequiredErrorMessage="video category name is required" MaxLength="50" />
                        <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the video category" MaxLength="200" TextMode="MultiLine" />
                        <OWD:VideoCategoriesSelectComponent runat="server" id="VideoCategoriesSelectComponent1" ResetVisible="true" />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the video category" HelpLabelText="switch on to hide this video category" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>