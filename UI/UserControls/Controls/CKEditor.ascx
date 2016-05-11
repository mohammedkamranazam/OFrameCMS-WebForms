<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CKEditor.ascx.cs" Inherits="OWDARO.UI.UserControls.Controls.CKEditor" %>

<div class="EditorSection" runat="server" id="Container">
    <label>
        <asp:Literal runat="server" ID="Label1"></asp:Literal>
        <small>
            <asp:Literal runat="server" ID="SmallLabel1"></asp:Literal>
        </small>
    </label>
    <div class="EditorHolder">
        <OWD:CKEditorControl runat="server" ID="Editor1" BasePath="~/Scripts/ckeditor"
            ContentsCss="~/Scripts/ckeditor/contents.css"
            FilebrowserBrowseUrl="/UI/Pages/Helpers/FileBrowser.aspx?Type=All"
            FilebrowserImageBrowseUrl="/UI/Pages/Helpers/FileBrowser.aspx?Type=Images"
            FilebrowserFlashBrowseUrl="/UI/Pages/Helpers/FileBrowser.aspx?Type=Flash" Height="500px" Width="97%"></OWD:CKEditorControl>
        <span class="f_help">
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic"
                SetFocusOnError="true" ControlToValidate="Editor1" Visible="false" />
        </span>
    </div>
    <div style="clear: both; height: 0px; padding: 0px; margin: 0px;">
    </div>
</div>

<style>
    #cke_83 {
        display: none;
    }

    .cke_skin_kama .cke_break {
        clear: none;
    }

    .cke_skin_kama input.cke_dialog_ui_input_text, .cke_skin_kama input.cke_dialog_ui_input_password {
        height: 20px;
        line-height: 20px;
    }
</style>