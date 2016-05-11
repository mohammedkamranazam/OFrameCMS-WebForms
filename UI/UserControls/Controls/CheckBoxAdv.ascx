<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckBoxAdv.ascx.cs" Inherits="OWDARO.UI.UserControls.Controls.CheckBoxAdv" %>

<div class="section" runat="server" id="Container">
    <label runat="server" id="LABEL">
        <asp:Literal runat="server" ID="Label1"></asp:Literal>
        <small>
            <asp:Literal runat="server" ID="SmallLabel1"></asp:Literal>
        </small>
    </label>
    <div runat="server" id="DIV">
        <asp:CheckBox runat="server" Height="25px" ID="CheckBox1" OnCheckedChanged="CheckBox1_CheckedChanged" />
        <span class="f_label">
            <asp:Literal runat="server" ID="HelpLabel1"></asp:Literal>
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>
<asp:ToggleButtonExtender ID="Extender1" runat="server" TargetControlID="CheckBox1"
    ImageHeight="25" ImageWidth="25" UncheckedImageUrl="~/Themes/Zice/Graphics/unchecked.png"
    CheckedImageUrl="~/Themes/Zice/Graphics/checked.png" UncheckedImageOverUrl="~/Themes/Zice/Graphics/uncheckedHover.png"
    CheckedImageOverUrl="~/Themes/Zice/Graphics/checkedHover.png" />