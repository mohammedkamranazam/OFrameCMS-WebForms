<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RoleSettingsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.RoleSettingsComponent" %>
<div class="grid2">
    <div class="boxtitle">
        <span class="ico color gear"></span>Settings To Apply For This Role
    </div>
    <OWD:CheckBoxAdv runat="server" ID="LoginCheckBox" LabelText="Login After Registration"
        HelpLabelText="check this if you want to automatically login the created user" />
    <OWD:CheckBoxAdv runat="server" ID="RegBlockCheckBox" LabelText="Block Registration"
        HelpLabelText="this will block the registration of new user for the selected role" />
    <OWD:CheckBoxAdv runat="server" ID="HideSuperAdminCheckBox" LabelText="Hide Super Administrator"
        HelpLabelText="hides the super administrator while new user registration" />
    <OWD:CheckBoxAdv runat="server" ID="ShowRolesCheckBox" LabelText="Show Roles"
        HelpLabelText="allows assigning roles to new users" />
    <OWD:CheckBoxAdv runat="server" ID="ShowCategoryCheckBox" LabelText="Show Categories"
        HelpLabelText="allows assigning categories to new users" />
    <OWD:DropDownListAdv runat="server" ID="MasterPagesDropDownList" LabelText="Master Page"
        SmallLabelText="select one to set for the child pages of this role" RequiredFieldErrorMessage="select one master page" />
</div>
<div class="grid2">
    <div class="boxtitle">
        <span class="ico color gear"></span>Root Path For The Role
    </div>
    <div class="section">
        <span class="ManageRolesPathLabel">Current Path:&nbsp;<asp:Label runat="server" ID="PathLabel" /></span>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel runat="server" ScrollBars="Auto" Height="400px" ID="Panel1">
                    <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows" ShowLines="True" CssClass="treeview"
                        Width="100%" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ExpandDepth="1">
                        <HoverNodeStyle CssClass="hoverNodeStyle" />
                        <ParentNodeStyle CssClass="parentNodeStyle" />
                        <SelectedNodeStyle CssClass="selectedNodeStyle" />
                        <NodeStyle CssClass="nodeStyle" />
                    </asp:TreeView>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TreeView1" EventName="SelectedNodeChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
<div class="Clear">
</div>
<style>
    .ManageRolesPathLabel {
        display: block;
        line-height: 30px;
        height: 30px;
        border: 1px solid #808080;
        margin-bottom: 10px;
        font-size: 12px;
        color: black;
        padding-left: 10px;
    }
</style>