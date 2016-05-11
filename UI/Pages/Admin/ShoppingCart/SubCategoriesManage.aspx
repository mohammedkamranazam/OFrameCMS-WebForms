<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="SubCategoriesManage.aspx.cs"
    Inherits="ProjectJKL.UI.Pages.Admin.ShoppingCart.SubCategoriesManage" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Manage Sub Category</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SubCategoriesList.aspx">
                    <i class="icon-list-ul"></i>List Sub Categories
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/ShoppingCart/SubCategoriesAdd.aspx">
                    <i class="icon-plus-sign"></i> Add Sub Categories
                </asp:HyperLink>
            </div>
            <div class="grid1">
                <div class="center">
                    <asp:Image ID="SubCategoryImage" runat="server" CssClass="ManageImage" />
                </div>
            </div>
            <div class="grid3">
                <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name of the sub category"
                    RequiredErrorMessage="sub category name is required" MaxLength="50" />
                <OWD:TextBoxAdv runat="server" ID="DescriptionTextBox" LabelText="Description" SmallLabelText="description of the sub category"
                    MaxLength="200" TextMode="MultiLine" />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <OWD:DropDownListAdv runat="server" ID="SectionsDropDownList" LabelText="Section"
                            SmallLabelText="section to filter the categories below" RequiredFieldErrorMessage="please select a section"
                            InitialValue="-1" AutoPostBack="true" />
                        <OWD:DropDownListAdv runat="server" ID="CategoriesDropDownList" LabelText="Category"
                            SmallLabelText="category to which the sub category belongs" RequiredFieldErrorMessage="please select a category"
                            InitialValue="-1" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <OWD:CheckBoxAdv runat="server" ID="HideCheckBox" LabelText="Hide" SmallLabelText="show/hide the sub category"
                    HelpLabelText="switch on to hide this sub category" />
                <OWD:FileUploadAdv runat="server" ID="FileUpload1" LabelText="Sub Category Image"
                    SmallLabelText="image assigned to this sub category" MaxFileSizeMB="5" />
                <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
            </div>
            <div class="Clear"></div>
        </div>
    </div>
</asp:content>