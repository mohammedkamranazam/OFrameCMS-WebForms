<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ProjectJKL.Cart" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <div class="SideBarLeft">
            <OWD:ProductSectionsComponent ID="ProductSectionsComponent1" runat="server" />
        </div>
        <div class="ContentMiddle">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <OWD:ProductCartGridComponent runat="server" ID="CartGridComponent1" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="Clear"></div>
    </div>
</asp:content>