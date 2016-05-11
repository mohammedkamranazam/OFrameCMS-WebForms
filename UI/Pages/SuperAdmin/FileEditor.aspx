<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="FileEditor.aspx.cs" Inherits="ProjectJKL.UI.Pages.SuperAdmin.FileEditor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>File Editor</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static">
                <ContentTemplate>
                    <div class="uibutton-toolbar btn-group">
                        <asp:Button runat="server" ID="SaveButton" Text="Save" CssClass="btn btn-primary" OnClick="SaveButton_Click" />
                    </div>
                    <br />
                    <div class="grid1">
                        <asp:TreeView ID="TreeView1" runat="server" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                            LineImagesFolder="~/Themes/Default/Graphics/TreeLineImages" ExpandDepth="0">
                            <HoverNodeStyle CssClass="FileBrowserHoverNodeStyle" />
                            <NodeStyle CssClass="FileBrowserNodeStyle" />
                            <SelectedNodeStyle CssClass="FileBrowserSelectedNodeStyle" />
                        </asp:TreeView>
                        <asp:HiddenField runat="server" ID="FilePathHiddenField" />
                    </div>
                    <div class="grid3">
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                        <br />
                        <br>
                        <OWD:UnsafeTextBox runat="server" ID="FileContentTextBox" EditorHeight="600px" RequiredErrorMessage="Cannot be empty" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>