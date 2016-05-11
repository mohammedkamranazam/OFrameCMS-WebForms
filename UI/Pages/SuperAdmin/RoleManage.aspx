<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="RoleManage.aspx.cs" Inherits="OWDARO.UI.Pages.SuperAdmin.RoleManage" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color group "></span>Roles Management</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="boxtitle">
                <span class="ico color messenger "></span>manage role settings from here
            </div>
            <asp:UpdatePanel runat="server" ClientIDMode="Static" ID="UpdatePanel1">
                <ContentTemplate>
                    <OWD:DropDownListAdv runat="server" ID="RolesDropDownList" LabelText="Role" SmallLabelText="the role for site access"
                        RequiredFieldErrorMessage="select a role" InitialValue="-1" AutoPostBack="true"
                        OnChange="return OnChange(this);" />
                    <OWD:RoleSettingsComponent runat="server" ID="RoleSettingsComponent1" />
                    <OWD:FormToolbar runat="server" ID="Formtoolbar1" ShowDelete="true" ShowSave="true"
                        ShowCancel="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>