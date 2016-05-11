<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OWDARO.Install.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel runat="server" ID="FormPanel">
            <asp:Button runat="server" ID="InstallButton" Text="Install" OnClick="InstallButton_Click" />&nbsp;Server<asp:TextBox runat="server" ID="ServerTextBox"></asp:TextBox>&nbsp;
            DataBase<asp:TextBox runat="server" ID="DataBaseTextBox"></asp:TextBox>&nbsp;Username<asp:TextBox runat="server" ID="UserNameTextBox"></asp:TextBox>&nbsp;Password<asp:TextBox
                runat="server" ID="PasswordTextBox"></asp:TextBox>&nbsp;
           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <asp:Panel ID="ProgressDetailsPanel" runat="server" ScrollBars="Auto" Style="margin: 40px auto; padding: 20px; border: 5px solid #008bc9; border-radius: 5px; min-height: 300px;
                max-height: 600px; width: 800px;">
                <asp:Label runat="server" ID="StatusLabel" Style="font-family: 'Lucida Console'; font-size: 14px; line-height: 30px;"></asp:Label>
            </asp:Panel>
            <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="InstallButton" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div style="position: fixed; top: 0; left: 0; width: 100%; height: 50px; background: #333333; color: #008bc9; text-align: center;">
                        <span style="line-height: 50px; font-family: Verdana; font-size: 25px;">Processing...</span>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </asp:Panel>
    </form>
</body>
</html>