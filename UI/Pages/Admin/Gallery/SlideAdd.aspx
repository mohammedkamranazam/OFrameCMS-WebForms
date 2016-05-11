<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="SlideAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.SlideAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add Slide</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/SlideList.aspx">
                <i class="icon-list-ul"></i> List Slides
                </asp:HyperLink>
            </div>
        <div class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name to identify the slide" RequiredErrorMessage="title is required" MaxLength="100" />
                    <OWD:TextBoxAdv runat="server" ID="PositionTextBox" LabelText="Position" SmallLabelText="slide position among other slides" MaxLength="7" FilterType="Numbers" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCancel="true" ShowSave="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>