<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileSelectorComponent.ascx.cs" Inherits="ProjectJKL.UI.UserControls.Components.Others.FileSelectorComponent" %>

<div class="FileSelector">
    <asp:Image runat="server" ID="_FileSelectorImage__" onclick="ShowPopUp()" />
    <div class="Info">
        <div class="Title">
            <asp:Label runat="server" ID="_TitleLabel__"></asp:Label>
        </div>
        <div class="SelectorToolbar">
            <span class="btn" onclick="ClearSelection()">Clear Selection</span>
            <span class="btn btn-primary" onclick="ShowPopUp()">Click To Change</span>
        </div>
    </div>
    <div class="Clear"></div>
</div>
<asp:HiddenField runat="server" ID="_FileIDHiddenField__" />
<asp:HiddenField runat="server" ID="_NoImagePathHiddenField__" />
<asp:HiddenField runat="server" ID="_LocaleHiddenField__" />
<script>
    var popup;

    function ClearSelection() {
        document.getElementById('<%= _FileIDHiddenField__.ClientID %>').value = "";
        document.getElementById('<%= _FileSelectorImage__.ClientID %>').src = document.getElementById('<%= _NoImagePathHiddenField__.ClientID %>').value;
        document.getElementById('<%= _TitleLabel__.ClientID %>').innerHTML = "No File Selected";
    }

    function ShowPopUp() {
        var fileIDHiddenField = document.getElementById('<%= _FileIDHiddenField__.ClientID %>').getAttribute("id");
        var image = document.getElementById('<%= _FileSelectorImage__.ClientID %>').getAttribute("id");
        var title = document.getElementById('<%= _TitleLabel__.ClientID %>').getAttribute("id");

        var url = "/UI/Pages/Helpers/FileListAndUpload.aspx?imgID=" + image +
                                                            "&ID=" + fileIDHiddenField +
                                                            "&titleID=" + title +
                                                            "&FileID=" + document.getElementById('<%= _FileIDHiddenField__.ClientID %>').value +
                                                            "&Locale=" + document.getElementById('<%= _LocaleHiddenField__.ClientID %>').value;

        popup = window.open(url, "Select or Upload File", "width=860,height=700");
        popup.focus();
    }
</script>

<style>
    .FileSelector {
        margin-top: 10px;
        width: 95%;
        padding: 5px;
        border-bottom: 1px solid rgba(0,0,0,0.1);
    }

        .FileSelector:hover {
        }

        .FileSelector img {
            width: 200px;
            height: 200px;
            float: left;
            cursor: pointer;
            margin-right: 10px;
        }

        .FileSelector div.Info {
            float: left;
            display: block;
            width: 60%;
        }

            .FileSelector div.Info div.SelectorToolbar {
                display: block;
                width: 100%;
                margin-top: 10px;
                text-align: center;
            }

                .FileSelector div.Info div.SelectorToolbar span.btn {
                    margin-bottom: 10px;
                }

            .FileSelector div.Info .Title {
                line-height: 20px;
                font-size: 12px;
                font-family: Verdana;
                text-align: justify;
                padding: 0px 5px 0px 5px;
                color: #333333;
            }
</style>