<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LabelAdv.ascx.cs" Inherits="ProjectJKL.UI.UserControls.Controls.LabelAdv" %>

<div class="section" runat="server" id="Container">
    <label runat="server" id="LABEL">
        <asp:Literal runat="server" ID="Label1"></asp:Literal>
        <small>
            <asp:Literal runat="server" ID="SmallLabel1"></asp:Literal>
        </small>
    </label>
    <div runat="server" id="DIV">
        <p class="Label" runat="server" id="LiteralParaTag">
            <asp:Literal runat="server" ID="Literal1"></asp:Literal>
        </p>
        <span class="f_label">
            <asp:Literal runat="server" ID="HelpLabel1"></asp:Literal>
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>