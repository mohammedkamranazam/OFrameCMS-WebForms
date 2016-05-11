<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Media.PostsComponent" %>

<h1 runat="server" id="HeaderTitle" class="PageTitle">
    <asp:Literal runat="server" ID="TitleLiteral"></asp:Literal>
</h1>
<div class="Posts">
    <asp:ListView ID="ListView1" runat="server">
        <ItemTemplate>
            <div class="Post" style='<%# string.Format("direction:{0};", OWDARO.Settings.LanguageHelper.GetLocaleDirection((string)Eval("Locale")) ) %>'>
                <%# OWDARO.Util.Utilities.GetFocusPointImage((int?)Eval("ImageID"), "ImageFrame", (string)Eval("Title"), "PostImage", this.Page) %>
                <h2 class="PostTitle"><%#Eval("Title") %></h2>
                <h3 class="PostSubTitle"><%#Eval("SubTitle") %></h3>
                <div class="InfoDiv">
                    By
                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("#?Username={0}", Eval("Username")) %>' Text='<%# Eval("Username") %>' />
                    | <span>
                        <%#OWDARO.Util.DataParser.GetDateTimeFormattedString((DateTime)Eval("AddedOn")) %></span> | <span>
                            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/Posts.aspx?PostCategoryID={0}", Eval("PostCategoryID")) %>'
                                Text='<%# Eval("ME_PostCategories.Title") %>' /></span>
                </div>
                <div class="PostContent truncate">
                    <%# OWDARO.Util.StringHelper.RemoveKeys(OWDARO.Util.StringHelper.TruncateFromHere((string)Eval("PostContent"))) %>
                    <%--<%# OWDARO.Util.StringHelper.RemoveKeys(OWDARO.Util.StringHelper.TruncateHtml(OWDARO.Util.StringHelper.TruncateFromHere((string)Eval("PostContent")), 300, " ...")) %>--%>
                    <div class="Clear"></div>
                </div>
                <div class="ReadMoreDiv">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="ReadMoreAnchor"
                        NavigateUrl='<%# String.Format("~/Post.aspx?PostID={0}", Eval("PostID")) %>'
                        Text='<%# string.Format("{0}", OWDARO.Settings.LanguageHelper.GetKey("ReadMore", (string)Eval("Locale")) ) %>' />
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>
    <div class="Pager">
        <asp:DataPager ID="DataPager1" runat="server" QueryStringField="Page" PagedControlID="ListView1" PageSize="10">
            <Fields>
                <asp:NextPreviousPagerField ShowNextPageButton="False" ButtonCssClass="ButtonCssClass" />
                <asp:NumericPagerField CurrentPageLabelCssClass="CurrentPageLabelCssClass" NumericButtonCssClass="NumericButtonCssClass"
                    NextPreviousButtonCssClass="NextPreviousButtonCssClass" />
                <asp:NextPreviousPagerField ShowPreviousPageButton="False" ButtonCssClass="ButtonCssClass" />
            </Fields>
        </asp:DataPager>
    </div>
</div>
<asp:HiddenField runat="server" ClientIDMode="Static" ID="_TextTruncateCountHiddenField__" Value="300" />