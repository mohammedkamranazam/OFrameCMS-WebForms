<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FoldersSelectComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.FoldersSelectComponent" %>

<OWD:DropDownListAdv runat="server" ID="DrivesDropDownList" LabelText="Drives" SmallLabelText="list of drives" InitialValue="-1" RequiredFieldErrorMessage="please select a drive"
    AutoPostBack="true" ValidationGroup="FolderValidationGroup" OnChange="return OnChange(this)" />
<asp:HiddenField runat="server" ID="DriveIDHiddenField" Value="-1" />
<div style="margin: 10px 0px 10px 0px;">
    <fieldset class="CategorySelector">
        <legend>Folders</legend>
        <div class="uibutton-toolbar btn-group">
            <asp:Button runat="server" ID="HomeButton" Text="Home" CssClass="btn btn-info " Enabled="true" OnClick="HomeButton_Click" CausesValidation="false" />
            <asp:Button runat="server" ID="BackButton" Text="Back" CssClass="btn btn-info " Enabled="true" OnClick="BackButton_Click" CausesValidation="false" />
            <asp:Button runat="server" ID="ResetButton" Text="Reset" CssClass="btn btn-info " Enabled="true" OnClick="ResetButton_Click" CausesValidation="false" Visible="false" />
        </div>
        <asp:Panel runat="server" ID="FoldersPanel" ScrollBars="Vertical" Style="min-height: 150px; max-height: 500px; width: 100%; margin: 5px 0px 5px 0px;">
            <asp:Label runat="server" ID="FolderAddressLabel"></asp:Label>
            <asp:ListView runat="server" ID="FoldersRepeater" OnItemCommand="FoldersRepeater_ItemCommand">
                <ItemTemplate>
                    <div class="FolderStyle">
                        <asp:ImageButton runat="server" ID="FolderButton" CommandName='<%# Eval("FolderID") %>' CausesValidation="false" ImageUrl='<%# OWDARO.Util.Utilities.GetImageThumbURL((int?)Eval("ImageID")) %>' />
                        <asp:HyperLink runat="server" Text="Edit" NavigateUrl='<%# string.Format("~/UI/Pages/Admin/Gallery/FolderManage.aspx?FolderID={0}", Eval("FolderID")) %>' Visible='<%# OWDARO.Util.DataParser.BoolParse(EditModeHiddenField.Value) %>' />
                        <span><span class='<%# string.Format("Info{0}", ((!string.IsNullOrWhiteSpace((string)Eval("Description"))) ? " tooltip" : "" )) %>' title='<%#Eval("Description") %>'>
                            ?</span><%#Eval("Title") %></span>
                    </div>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <p style="text-align: center; line-height: 150px;">NO SUB FOLDERS</p>
                </EmptyDataTemplate>
            </asp:ListView>
        </asp:Panel>
        <div class="CategorySelectorStatusBar">
            /<asp:Literal runat="server" ID="StatusLiteral" />
        </div>
    </fieldset>
</div>
<asp:HiddenField runat="server" ID="EditModeHiddenField" Value="False" Visible="false" />
<asp:HiddenField runat="server" ID="CurrentFolderIDHiddenField" />
<asp:HiddenField runat="server" ID="FolderStackBackHiddenField" />
<asp:HiddenField runat="server" ID="LocaleHiddenField" />
<asp:HiddenField runat="server" ID="InitialValueHiddenField" />