<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="RoleAdd.aspx.cs" Inherits="OWDARO.UI.Pages.SuperAdmin.RoleAdd" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color group "></span>New Role</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="boxtitle">
                <span class="ico color messenger "></span>Add New Roles From Here
            </div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <OWD:TextBoxAdv runat="server" ID="RoleNameTextBox" LabelText="Role" SmallLabelText="name for the new role"
                        RequiredErrorMessage="role name is required" />
                    <OWD:RoleSettingsComponent runat="server" ID="RoleSettingsComponent1" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>