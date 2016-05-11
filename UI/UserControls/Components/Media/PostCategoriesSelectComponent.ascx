<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostCategoriesSelectComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Media.PostCategoriesSelectComponent" %>

<div style="margin: 10px 0px 10px 0px;">
    <fieldset class="CategorySelector">
        <legend>Post Categories</legend>
        <div class="uibutton-toolbar btn-group">
            <asp:Button runat="server" ID="HomeButton" Text="Home" CssClass="btn btn-info " Enabled="true" OnClick="HomeButton_Click"
                CausesValidation="false" />
            <asp:Button runat="server" ID="BackButton" Text="Back" CssClass="btn btn-info " Enabled="true" OnClick="BackButton_Click"
                CausesValidation="false" />
            <asp:Button runat="server" ID="ResetButton" Text="Reset" CssClass="btn btn-info " Enabled="true" OnClick="ResetButton_Click"
                CausesValidation="false" Visible="false" />
        </div>
        <asp:Panel runat="server" ID="PostCategoriesPanel" ScrollBars="Vertical" Style="min-height: 150px; max-height: 500px; width: 100%;">
            <asp:Label runat="server" ID="PostCategoryAddressLabel"></asp:Label>
            <asp:ListView runat="server" ID="PostCategoriesRepeater" OnItemCommand="PostCategoriesRepeater_ItemCommand">
                <ItemTemplate>
                    <div class="PostCategoryStyle">
                        <asp:ImageButton runat="server" ID="PostCategoryButton" CommandName='<%# Eval("PostCategoryID") %>' CausesValidation="false"
                            ImageUrl='<%# OWDARO.Util.Utilities.GetImageThumbURL((int?)Eval("ImageID")) %>' />
                        <asp:HyperLink runat="server" Text="Edit" NavigateUrl='<%# string.Format("~/UI/Pages/Admin/Media/PostCategoriesManage.aspx?PostCategoryID={0}", Eval("PostCategoryID")) %>'
                            Visible='<%# OWDARO.Util.DataParser.BoolParse(EditModeHiddenField.Value) %>' />
                        <span><span class='<%# string.Format("Info{0}", ((!string.IsNullOrWhiteSpace((string)Eval("Description"))) ? " tooltip" : "" )) %>'
                            title='<%#Eval("Description") %>'>?</span><%#Eval("Title") %></span>
                    </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <p style="text-align: center; line-height: 150px;">Selected Category Is Empty</p>
                </EmptyDataTemplate>
            </asp:ListView>
        </asp:Panel>
        <div class="CategorySelectorStatusBar">
            /<asp:Literal runat="server" ID="StatusLiteral" />
        </div>
    </fieldset>
</div>
<asp:HiddenField runat="server" ID="EditModeHiddenField" Value="False" Visible="false" />
<asp:HiddenField runat="server" ID="CurrentPostCategoryIDHiddenField" />
<asp:HiddenField runat="server" ID="PostCategoryStackBackHiddenField" />
<asp:HiddenField runat="server" ID="LocaleHiddenField" />
<asp:HiddenField runat="server" ID="InitialValueHiddenField" />