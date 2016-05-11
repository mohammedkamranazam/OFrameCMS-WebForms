<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="AccessManager.aspx.cs" Inherits="OWDARO.UI.Pages.SuperAdmin.AccessManager" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="margin: 20px auto; width: 1200px;">
                <asp:Panel ID="Panel1" runat="server" Style="margin-left: 20px; border: 2px solid #333; float: left;"
                    Width="200px" Height="500px" ScrollBars="Auto">
                    <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows" ShowLines="True" Height="550px"
                        Width="200px" CssClass="treeview" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                        ExpandDepth="1" ValidateRequestMode="Disabled">
                        <HoverNodeStyle CssClass="hoverNodeStyle" />
                        <ParentNodeStyle CssClass="parentNodeStyle" />
                        <SelectedNodeStyle CssClass="selectedNodeStyle" />
                        <NodeStyle CssClass="nodeStyle" />
                    </asp:TreeView>
                </asp:Panel>
                <div style="width: 900px; border: 2px solid #333; float: left; margin-left: 30px; min-height: 550px;">
                    <div style="height: 15px; padding: 5px 0px 0px 15px;">
                        <asp:Label runat="server" ID="DirectoryLbl"></asp:Label>
                    </div>
                    <div>
                        <hr />
                        <div style="padding: 15px;">
                            <input type="radio" name="Roles" id="RolesRadioButton" runat="server" />Role
                    <asp:DropDownList ID="RolesDropDown" CssClass="DropDownSelect" runat="server">
                    </asp:DropDownList>
                            <br />
                            <br />
                            <input type="radio" name="Roles" id="UserRadioButton" runat="server" />User
                    <asp:DropDownList ID="UserDropDown" CssClass="DropDownSelect" runat="server">
                    </asp:DropDownList>
                            <br />
                            <br />
                            <input type="radio" name="Roles" id="AllUsersRadioButton" runat="server" />All Users
                    <br />
                            <br />
                            <input type="radio" name="Roles" id="AnonymousRadioButton" runat="server" />Anonymous
                    Users
                        </div>
                        <hr />
                        <div style="padding: 15px;">
                            <input type="radio" name="Access" id="AllowRadioButton" runat="server" />Allow
                    <input type="radio" name="Access" id="DenyRadioButton" runat="server" />Deny
                        </div>
                        <hr />
                        <div style="padding: 15px;">
                            <input type="radio" name="Rules" id="AppendRadioButton" runat="server" />Append
                    <input type="radio" name="Rules" id="PrependRadioButton" runat="server" style="margin-left: 50px;" />Prepend
                            <input type="radio" name="Rules" id="PositionRadioButton" runat="server" style="margin-left: 50px;" />Insert
                            at Position
                            <asp:DropDownList runat="server" ID="RuleCountDropDown">
                            </asp:DropDownList>
                        </div>
                        <hr />
                        <div style="padding: 15px;">
                            <asp:Button runat="server" Text="Add Rule" ID="AddRuleButton" CssClass="uibutton"
                                OnClick="AddRuleButton_Click" CausesValidation="False" />
                        </div>
                        <hr />
                        <div style="padding: 15px;">
                            <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False"
                                OnRowDeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Position">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("Position") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="PositionHiddenField" Value='<%#Eval("Position") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeaderStyle" />
                                        <ItemStyle CssClass="GridItemStyle" />
                                        <ControlStyle CssClass="GridControlStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Access Type">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("AccessType") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="AccessTypeHiddenField" Value='<%#Eval("AccessType") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeaderStyle" />
                                        <ItemStyle CssClass="GridItemStyle" />
                                        <ControlStyle CssClass="GridControlStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Access Level">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("AccessLevel") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="AccessLevelHiddenField" Value='<%#Eval("AccessLevel") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeaderStyle" />
                                        <ItemStyle CssClass="GridItemStyle" />
                                        <ControlStyle CssClass="GridControlStyle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Access To">
                                        <ItemTemplate>
                                            <asp:Label runat="server" Text='<%#Eval("AccessTo") %>'></asp:Label>
                                            <asp:HiddenField runat="server" ID="AccessToHiddenField" Value='<%#Eval("AccessTo") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="GridHeaderStyle" />
                                        <ItemStyle CssClass="GridItemStyle" />
                                        <ControlStyle CssClass="GridControlStyle" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" HeaderText="Tools">
                                        <HeaderStyle CssClass="GridHeaderStyle" />
                                        <ItemStyle CssClass="GridItemStyle" />
                                        <ControlStyle CssClass="GridControlStyle" />
                                    </asp:CommandField>
                                </Columns>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <RowStyle CssClass="GridRowStyle" />
                                <PagerStyle CssClass="GridPagerStyle" />
                            </asp:GridView>
                        </div>
                        <hr />
                        <div style="padding: 15px;">
                            <asp:Literal ID="XMLTextBox" runat="server" Mode="Encode"></asp:Literal>
                        </div>
                        <hr />
                        <div style="padding: 15px;">
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TreeView1" EventName="SelectedNodeChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:content>