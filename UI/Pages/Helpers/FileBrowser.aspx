<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="FileBrowser.aspx.cs" Inherits="ProjectJKL.UI.Pages.Helpers.FileBrowser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Browser</title>
    <link rel="stylesheet" type="text/css" runat="server" href="~/Themes/Default/Stylesheets/Components.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" EnableCdn="False" EnableHistory="True" EnablePageMethods="True" EnableScriptGlobalization="True"></asp:ToolkitScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" ClientIDMode="Static">
            <ContentTemplate>
                <div class="FileBrowserContent">
                    <div class="FileBrowserToolbar">
                        <div class="ToolBarButton">
                            <asp:AsyncFileUpload runat="server" ID="FileUpload1" OnUploadedComplete="FileUpload1_UploadedComplete" Width="200px" CompleteBackColor="#00C982" ErrorBackColor="#A10000"
                                UploadingBackColor="#B88709" />
                        </div>
                        <div class="ToolBarButton">
                            <asp:Button runat="server" ID="RefreshButton" OnClick="RefreshButton_Click" Text="Refresh" />
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="FoldersPanel" CssClass="FileBrowserFoldersPanel" ScrollBars="Auto">
                        <asp:TreeView ID="TreeView1" runat="server" ShowLines="True" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged"
                            LineImagesFolder="~/Themes/Default/Graphics/TreeLineImages">
                            <HoverNodeStyle CssClass="FileBrowserHoverNodeStyle" />
                            <NodeStyle CssClass="FileBrowserNodeStyle" />
                            <SelectedNodeStyle CssClass="FileBrowserSelectedNodeStyle" />
                        </asp:TreeView>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="FilesPanel" CssClass="FileBrowserFilesPanel" ScrollBars="Vertical">
                        <asp:Label runat="server" ID="label1"></asp:Label>
                        <asp:Literal runat="server" ID="FilesLiteral"></asp:Literal>
                    </asp:Panel>
                    <div class="Clear"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DynamicLayout="true" DisplayAfter="0">
            <ProgressTemplate>
                <div class="spinner" style="z-index: 99999999; position: absolute;"></div>
                <div style="width: 100%; height: 100%; position: fixed; left: 0; top: 0; overflow: hidden; background: white; opacity: 0.5; z-index: 9999999;">
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:AlwaysVisibleControlExtender ID="UpdateProgress1_AlwaysVisibleControlExtender"
            runat="server" Enabled="True" HorizontalSide="Center" TargetControlID="UpdateProgress1"
            VerticalSide="Middle" UseAnimation="True">
        </asp:AlwaysVisibleControlExtender>
    </form>
    <script type="text/javascript">
        function getUrlParam(paramName) {
            var reParam = new RegExp('(?:[\?&]|&amp;)' + paramName + '=([^&]+)', 'i');
            var match = window.location.search.match(reParam);

            return (match && match.length > 1) ? match[1] : '';
        }

        function GetURL(element) {
            var funcNum = getUrlParam('CKEditorFuncNum');
            var fileUrl = element.getAttribute("path");
            window.opener.CKEDITOR.tools.callFunction(funcNum, fileUrl);
            window.close();
        }
    </script>
</body>
</html>