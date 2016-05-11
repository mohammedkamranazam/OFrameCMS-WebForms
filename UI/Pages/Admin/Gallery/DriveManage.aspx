<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="DriveManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.DriveManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Manage Drive</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink runat="server" CssClass="btn btn-primary" NavigateUrl="~/UI/Pages/Admin/Gallery/DriveList.aspx">
                            <i class="icon-arrow-left"></i> Go Back To Drives List
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/DriveAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Drive
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ClientIDMode="Static">
                <ContentTemplate>
                    <div class="grid1">
                        <OWD:ImageSelectorComponent runat="server" ID="ImageSelectorComponent1" />
                    </div>
                    <div class="grid3">
                        <OWD:Locales runat="server" ID="LocaleDropDown" LabelText="Locale" SmallLabelText="select a language" AutoPostBack="true" />
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Name" SmallLabelText="enter a name for drive" RequiredErrorMessage="drive name is required" MaxLength="100" />
                        <OWD:TextBoxAdv MaxLength="250" TextMode="MultiLine" runat="server" ID="DescriptionEditor" LabelText="Description" SmallLabelText="enter description for drive" />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="check this to hide the drive" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>