<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RadioButtonAdv.ascx.cs" Inherits="OWDARO.UI.UserControls.Controls.RadioButtonAdv" %>

<div class="section" runat="server" id="Container">
    <label>
        <asp:Label runat="server" ID="Label1"></asp:Label>
        <small>
            <asp:Label runat="server" ID="SmallLabel1"></asp:Label>
        </small>
    </label>
    <div>
        <asp:RadioButton runat="server" ID="RadioButton1" />
        <span class="f_label">
            <asp:Label runat="server" ID="HelpLabel1"></asp:Label>
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>