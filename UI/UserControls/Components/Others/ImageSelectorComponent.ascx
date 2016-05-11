<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageSelectorComponent.ascx.cs" Inherits="ProjectJKL.UI.UserControls.Components.Others.ImageSelectorComponent" %>

<div class="ImageSelector">
    <asp:Image runat="server" ID="_ImageSelectorImage__" onclick="ShowPopUp()" />
    <span onclick="ShowPopUp()">Click To Change</span>
</div>
<asp:HiddenField runat="server" ID="_ImageIDHiddenField__" />
<asp:HiddenField runat="server" ID="StoragePathHiddenField" Value="~/Storage/Resources/Others/" />
<script>
    var popup;

    function ShowPopUp() {
        var imageIDHiddenField = document.getElementById('<%= _ImageIDHiddenField__.ClientID %>').getAttribute("id");
        var image = document.getElementById('<%= _ImageSelectorImage__.ClientID %>').getAttribute("id");

        var url = "/UI/Pages/Helpers/ImageListAndUpload.aspx?imgID=" + image +
                                                            "&ID=" + imageIDHiddenField +
                                                            "&ImageID=" + document.getElementById('<%= _ImageIDHiddenField__.ClientID %>').value +
                                                            "&StoragePath=" + document.getElementById('<%= StoragePathHiddenField.ClientID %>').value;

        popup = window.open(url, "Select or Upload Image", "width=860,height=700");
        popup.focus();
    }
</script>

<style>
    .ImageSelector {
        width: 200px;
        height: 200px;
        padding: 5px;
        border: 1px solid rgba(0,0,0,0.1);
        box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.3);
        margin: 10px auto;
    }

        .ImageSelector:hover {
            cursor: pointer;
        }

        .ImageSelector img {
            width: 100%;
            height: 100%;
        }

        .ImageSelector span {
            display: none;
        }

        .ImageSelector:hover span {
            position: absolute;
            display: block;
            line-height: 30px;
            padding: 0px 10px 0px 10px;
            text-align: center;
            font-family: Verdana;
            font-size: 12px;
            color: white;
            background: #008bc9;
            width: 180px;
            margin-top: -30px;
        }
</style>