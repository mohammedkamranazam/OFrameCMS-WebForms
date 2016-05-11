<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDataDetailsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserDataDetailsComponent" %>

<div class="onecolumn">
    <div class="header">
        <span><span runat="server" id="HeaderTitleIcon" class="ico color"></span>
            <asp:Literal runat="server" ID="HeaderTitleLiteral"></asp:Literal></span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span runat="server" id="BoxTitleIcon" class="ico color"></span>
            <asp:Literal runat="server" ID="BoxTitleLiteral"></asp:Literal>
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <OWD:UserDataComponent runat="server" ID="UserDataComponent" />
                <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCustom="true" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                <div class="GridSection">
                    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" ShowHeader="False"
                        OnRowDeleting="GridView_RowDeleting" GridLines="None" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# this.GridViewLiteralText %>'></asp:Literal>
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#String.Format("{0}{1}", this.GridViewHyperLinkFormatString, Eval("UserData")) %>'
                                        Text='<%#Eval("UserData") %>'></asp:HyperLink>
                                    <asp:HiddenField runat="server" ID="UserDataIDHiddenField" Value='<%#Eval("UserDataID") %>' />
                                    <asp:ImageButton ID="ImageButton1" ImageAlign="Middle" ImageUrl="~/Themes/Zice/Graphics/icon/icon_delete.png"
                                        runat="server" CommandName="Delete" Text="Delete" CausesValidation="False" />
                                </ItemTemplate>
                                <ItemStyle CssClass="ProfileManageGridRowsItemStyle" />
                                <ControlStyle CssClass="ProfileManageGridRowsControlStyle" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>