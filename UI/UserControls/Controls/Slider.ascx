<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Slider.ascx.cs" Inherits="OWDARO.UI.UserControls.Controls.Slider" %>

<div class="section" runat="server" id="Container">
    <label>
        <asp:Literal runat="server" ID="Label1"></asp:Literal>
        <small>
            <asp:Literal runat="server" ID="SmallLabel1"></asp:Literal>
        </small>
    </label>
    <div>
        <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
        <span class="f_help">
            <asp:Label runat="server" ID="ValueLabel" Style="color: black; font-weight: bold;"></asp:Label>
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>
<asp:SliderExtender ID="TextBox1_SliderExtender" runat="server" Enabled="True" Maximum="100" Minimum="0" TargetControlID="TextBox1" BoundControlID="ValueLabel" EnableHandleAnimation="true"
    EnableKeyboard="true" Length="400">
</asp:SliderExtender>