<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DropDownListAdv.ascx.cs" Inherits="OWDARO.UI.UserControls.Controls.DropDownListAdv" %>

<div class="section" runat="server" id="Container">
    <label runat="server" id="LABEL">
        <asp:Literal runat="server" ID="Label1"></asp:Literal>
        <small>
            <asp:Literal runat="server" ID="SmallLabel1"></asp:Literal>
        </small>
    </label>
    <div runat="server" id="DIV">
        <asp:DropDownList runat="server" ID="DropDownList1" CssClass="DropDownSelect" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <span class="f_help">
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DropDownList1" Display="Dynamic" SetFocusOnError="True" Visible="false" CultureInvariantValues="true" />
            <asp:CustomValidator ID="CustomValidator1" runat="server" Visible="false" Display="Dynamic" ControlToValidate="DropDownList1" SetFocusOnError="True" />
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" SetFocusOnError="true" ControlToValidate="DropDownList1" Visible="false" />
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>