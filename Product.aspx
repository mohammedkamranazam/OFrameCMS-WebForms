<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="ProjectJKL.Product" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="Content">
        <div class="SideBarLeft">
            <OWD:ProductSectionsComponent runat="server" ID="ProductSectionsComponent1" />
        </div>
        <div class="ContentMiddle">
            <OWD:ProductDetailsComponent runat="server" ID="ProductDetailsComponent1" />
        </div>
        <div class="Clear"></div>
    </div>
</asp:content>