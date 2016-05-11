<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="PostsManage.aspx.cs"
    Inherits="ProjectJKL.UI.Pages.Admin.Media.PostsManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New Post</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Media/PostsList.aspx">
                            <i class="icon-list-ul"></i>List Posts
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Media/PostsAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Post
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
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="title of the post" RequiredErrorMessage="post title is required" MaxLength="250" />
                        <OWD:TextBoxAdv runat="server" ID="SubTitleTextBox" LabelText="Sub Title" SmallLabelText="sub title of the post" RequiredErrorMessage="post sub title is required" MaxLength="250" />
                        <OWD:CKEditor runat="server" ID="DescriptionEditor" LabelText="Content" SmallLabelText="post content or article" RequiredErrorMessage="post content is required" />
                        <OWD:TextBoxAdv runat="server" ID="TagsTextBox" LabelText="Tags" SmallLabelText="comma separated tags for the post" MaxLength="250" FilterMode="InvalidChars" InvalidChars=" " />
                        <OWD:PostCategoriesSelectComponent runat="server" ID="PostCategoriesSelectComponent1" ResetVisible="True" />
                        <OWD:ListPopUpComponent runat="server" ID="PostsListPopUpComponent1" Width="400px" Height="300px" Text="Select Parent Post" />
                        <OWD:LabelAdv runat="server" ID="ParentPostSelectedIDLabel" LabelText="Selected Parent Post" SmallLabelText="the post selected for being the parent post" />
                        <asp:HiddenField runat="server" ID="PostsListPopUpComponentSelectedIDHiddenField" Visible="false" />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the post" HelpLabelText="switch on to hide this post" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>