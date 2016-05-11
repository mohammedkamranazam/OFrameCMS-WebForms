<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnsafeTextBox.ascx.cs" Inherits="ProjectJKL.UI.UserControls.Controls.UnsafeTextBox" %>

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
            FilebrowserFlashBrowseUrl="/UI/Pages/Helpers/FileBrowser.aspx?Type=Flash" Height="500px" Width="97%" ContentsLangDirection="Ltr"
            ToolbarStartupExpanded="False" StartupMode="Source" PasteFromWordRemoveStyles="True" PasteFromWordPromptCleanup="True"
            PasteFromWordNumberedHeadingToList="False" HtmlEncodeOutput="True" AutoParagraph="False" AutoUpdateElement="False"></OWD:CKEditorControl>
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

    .cke_toolbox_collapser {
        display: none;
    }

    .cke_skin_kama a.cke_toolbox_collapser_min, .cke_skin_kama a:hover.cke_toolbox_collapser_min {
    }
</style>

<script>
    $(document).ready(function () {
        $(".ajax__html_editor_extender_source").trigger('click');
    });
</script>