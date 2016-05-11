<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="PortfolioManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Media.PortfolioManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Edit Portfolio</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Media/PortfolioList.aspx">
                            <i class="icon-list-ul"></i> List Portfolios
                        </asp:HyperLink>
                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Media/PortfolioAdd.aspx">
                            <i class="icon-plus-sign"></i> Add Portfolio
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static">
                <ContentTemplate>
                    <div class="grid1">
                        <OWD:ImageSelectorComponent runat="server" ID="ImageSelectorComponent1" />
                    </div>
                    <div class="grid3">
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="title of the portfolio" RequiredErrorMessage="portfolio title is required" MaxLength="250" />
                        <OWD:CKEditor runat="server" ID="DescriptionEditor" LabelText="Description" SmallLabelText="description of the portfolio" RequiredErrorMessage="description is required" />
                        <OWD:TextBoxAdv runat="server" ID="DateTextBox" LabelText="Date" SmallLabelText="date of completion of the project" CalendarDefaultView="Days" MaxLength="20" />
                        <OWD:DropDownListAdv runat="server" ID="ClientIDDropDownList" LabelText="Client" SmallLabelText="client of the portfolio" RequiredFieldErrorMessage="please select a client" InitialValue="-1" />
                        <OWD:TextBoxAdv runat="server" ID="URLTextBox" LabelText="URL" SmallLabelText="url of the project" MaxLength="250" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" ShowDelete="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Portfolio Categories</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <OWD:DropDownListAdv runat="server" ID="ProjectCategoriesDropDownList" LabelText="Project Categories" SmallLabelText="the category of the project to which this portfolio belongs" InitialValue="-1" RequiredFieldErrorMessage="project category is required" ValidationGroup="PortfolioCategoriesValidationGroup" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar2" ShowCustom="true" CustomButtonText="Add" ValidationGroup="PortfolioCategoriesValidationGroup" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage2" />
                    <br />
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Project category" SortExpression="Title">
                                <ItemTemplate>
                                    <span class="GridTitleFieldColumn"><%#Eval("Title") %></span>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-mini" Text="Delete" CommandName='<%# Eval("ID") %>' CausesValidation="false"></asp:Button>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <PagerStyle CssClass="GridPagerStyle" />
                        <RowStyle CssClass="GridRowStyle" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>