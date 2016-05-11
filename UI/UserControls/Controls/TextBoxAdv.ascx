<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TextBoxAdv.ascx.cs"
    Inherits="OWDARO.UI.UserControls.Controls.TextBoxAdv" %>

<div class="section" runat="server" id="Container">
    <label runat="server" id="LABEL">
        <asp:Literal runat="server" ID="Label1"></asp:Literal>
        <small>
            <asp:Literal runat="server" ID="SmallLabel1"></asp:Literal>
        </small>
    </label>
    <div runat="server" id="DIV">
        <span runat="server" id="TipSpan" clientidmode="Static">
            <asp:TextBox CssClass="full" runat="server" ID="TextBox1" OnTextChanged="TextBox1_TextChanged" />
        </span>
        <span class="f_label" runat="server" id="HelpLabelSpan" visible="false">
            <asp:Literal runat="server" ID="HelpLabelLiteral"></asp:Literal>
        </span>
        <span class="f_help">
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox1" Display="Dynamic" SetFocusOnError="True" Visible="false"
                CultureInvariantValues="true" />
            <asp:CustomValidator ID="CustomValidator1" runat="server" Visible="false" Display="Dynamic" ControlToValidate="TextBox1" SetFocusOnError="True" />
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" SetFocusOnError="true"
                ControlToValidate="TextBox1" Display="Dynamic" Visible="false" />
            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" SetFocusOnError="true"
                ControlToValidate="TextBox1" Display="Dynamic" Visible="false" />
            <asp:RangeValidator ID="RangeValidator1" runat="server" SetFocusOnError="true" ControlToValidate="TextBox1"
                Display="Dynamic" Visible="false" CultureInvariantValues="true" />
            <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlExtender="TextBox1_MaskedEditExtender" ControlToValidate="TextBox1"
                Display="Dynamic" Enabled="true" Visible="false" />
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>

<asp:Panel runat="server" ID="PopUpControlExtenderPanel" Visible="false">
    <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="PopUpPanel" TargetControlID="TextBox1" Enabled="false">
    </asp:PopupControlExtender>
</asp:Panel>
<asp:Panel runat="server" ID="WaterMarkExtenderPanel" Visible="false">
    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" Enabled="True"
        TargetControlID="TextBox1" WatermarkCssClass="watermark">
    </asp:TextBoxWatermarkExtender>
</asp:Panel>
<asp:Panel runat="server" ID="FilteredTextBoxExtenderPanel" Visible="false">
    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
        TargetControlID="TextBox1">
    </asp:FilteredTextBoxExtender>
</asp:Panel>
<asp:Panel runat="server" ID="CalendarExtenderPanel" Visible="false">
    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="TextBox1"
        DefaultView="Years" FirstDayOfWeek="Sunday" TodaysDateFormat="dd-MMM-yyyy" CssClass="CalendarExtender">
    </asp:CalendarExtender>
</asp:Panel>
<asp:Panel runat="server" ID="MaskedEditExtenderPanel" Visible="false">
    <asp:MaskedEditExtender ID="TextBox1_MaskedEditExtender" runat="server" Enabled="True" TargetControlID="TextBox1">
    </asp:MaskedEditExtender>
</asp:Panel>
<asp:Panel runat="server" ID="PopUpPanel" Visible="false">
    <asp:ListBox ID="PopUpListBox1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="PopUpListBox_SelectedIndexChanged"></asp:ListBox>
</asp:Panel>