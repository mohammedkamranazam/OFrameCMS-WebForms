<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebCamComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Utility.WebCamComponent" %>

<script type="text/javascript" src='<%: ResolveClientUrl("~/Scripts/Silverlight/Silverlight.min.js") %>'></script>
<script type="text/javascript" src='<%: ResolveClientUrl("~/Scripts/Silverlight/WebCamApp.min.js") %>'></script>
<object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
    width="100%" height="100%">
    <param name="source" value="../../../ClientBin/WebCamApp.xap" />
    <param name="onError" value="onSilverlightError" />
    <param name="background" value="transparent" />
    <param name="windowless" value="true" />
    <param name="minRuntimeVersion" value="4.0.60310.0" />
    <param name="autoUpgrade" value="true" />
    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.60310.0" style="text-decoration: none">
        <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight"
            style="border-style: none" />
    </a>
</object>
<iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px; border: 0px;"></iframe>
<asp:HiddenField runat="server" ID="WebCamAppImage64StrHiddenField" ClientIDMode="Static"></asp:HiddenField>