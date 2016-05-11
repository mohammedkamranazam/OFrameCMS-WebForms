<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="ProjectJKL.ContactUs" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="grid2">
        <OWD:ContactUsDetailsComponent runat="server" />
    </div>
    <div class="grid2">
        <div class="onecolumn">
            <div class="header">
                <span><span class="ico color mail"></span>
                    <%= OWDARO.Settings.LanguageHelper.GetKey("QuickContactForm") %></span>
            </div>
            <div class="content">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <OWD:ContactUsQueryFormComponent runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="Clear">
    </div>
</asp:content>