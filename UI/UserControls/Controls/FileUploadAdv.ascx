<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUploadAdv.ascx.cs" Inherits="OWDARO.UI.UserControls.Controls.FileUploadAdv" %>

<div class="section" runat="server" id="Container">
    <label runat="server" id="LABEL">
        <asp:Literal runat="server" ID="Label1"></asp:Literal>
        <small>
            <asp:Literal runat="server" ID="SmallLabel1"></asp:Literal>
        </small>
    </label>
    <div runat="server" id="DIV">
        <asp:FileUpload runat="server" CssClass="fileupload medium" ID="FileUpload1" />
        <span class="f_help">
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" SetFocusOnError="true"
                ControlToValidate="FileUpload1" Display="Dynamic" Visible="false" />
            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="FileUpload1"
                Display="Dynamic" SetFocusOnError="True" Visible="false" />
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>