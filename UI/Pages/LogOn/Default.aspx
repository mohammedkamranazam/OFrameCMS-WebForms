<%@ Page Async="true" Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OWDARO.UI.Pages.LogOn.Default" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="grid2">
        <asp:Panel runat="server" ID="LoginComponentPanel">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <OWD:LoginComponent runat="server" ID="LoginComponent" UseRedirect="true"></OWD:LoginComponent>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div class="grid2">
        <asp:Panel runat="server" ID="UserAddPopUpComponentPanel">
            <OWD:UserAddPopUpComponent runat="server" ID="UserAddPopUpComponent1" />
        </asp:Panel>
    </div>
    <div class="Clear"></div>
</asp:content>