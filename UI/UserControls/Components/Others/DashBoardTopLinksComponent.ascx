<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashBoardTopLinksComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Others.DashBoardTopLinksComponent" %>

<div class="topcolumn" id="TopColumn" runat="server">
    <ul id="shortcut">
        <li id="HomeList" runat="server">
            <span class="ntip">
                <asp:HyperLink ID="HomeHyperLink" runat="server" NavigateUrl="~/Default.aspx" title="Back To home">
                <asp:Image runat="server" ImageUrl="~/Themes/Zice/Graphics/icon/shortcut/home.png"
                    alt="home" /><strong>Home</strong>
                </asp:HyperLink>
            </span>
            <div class="notification" id="HomeNotification" runat="server">
                <asp:Literal runat="server" ID="HomeCountLiteral"></asp:Literal>
            </div>
        </li>
        <li id="GraphList" runat="server">
            <span class="ntip">
                <asp:HyperLink ID="GraphHyperLink" runat="server" NavigateUrl="#" title="Graph and Statistics">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Themes/Zice/Graphics/icon/shortcut/graph.png"
                        alt="graph" /><strong>Graph</strong>
                </asp:HyperLink>
            </span>
            <div class="notification" id="GraphNotification" runat="server">
                <asp:Literal runat="server" ID="GraphCountLiteral"></asp:Literal>
            </div>
        </li>
        <li id="SettingsList" runat="server">
            <span class="ntip">
                <asp:HyperLink ID="SettingsHyperLink" runat="server" NavigateUrl="#" title="Manage Site Settings">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Themes/Zice/Graphics/icon/shortcut/setting.png"
                        alt="settings" /><strong>Settings</strong>
                </asp:HyperLink>
            </span>
            <div class="notification" id="SettingsNotification" runat="server">
                <asp:Literal runat="server" ID="SettingsCountLiteral"></asp:Literal>
            </div>
        </li>
        <li id="MessageList" runat="server">
            <span class="ntip">
                <asp:HyperLink ID="MessagesHyperLink" runat="server" NavigateUrl="#" title="View Your Messages">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Themes/Zice/Graphics/icon/shortcut/mail.png"
                        alt="messages" /><strong>Messages</strong>
                </asp:HyperLink>
            </span>
            <div class="notification" id="MessageNotification" runat="server">
                <asp:Literal runat="server" ID="MessagesCountLiteral"></asp:Literal>
            </div>
        </li>
    </ul>
    <div class="Clear"></div>
</div>