<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilesSelectComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Gallery.Nested.FilesSelectComponent" %>

<OWD:FoldersSelectComponent runat="server" ID="FoldersSelectComponent1" />
<asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging"
    OnSorting="GridView1_Sorting">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <%# OWDARO.Util.Utilities.GetFancyBoxHTML((int?)Eval("ImageID"), string.Empty, false, "Style='margin: 5px; width:100px; height:100px;'") %>
            </ItemTemplate>
            <HeaderStyle CssClass="GridHeaderStyle" />
            <ItemStyle CssClass="GridItemStyle" />
            <ControlStyle CssClass="GridControlStyle" />
        </asp:TemplateField>
        <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" SortExpression="Title">
            <HeaderStyle CssClass="GridHeaderStyle" />
            <ItemStyle CssClass="GridItemStyle" />
            <ControlStyle CssClass="GridControlStyle" />
        </asp:BoundField>
        <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description">
            <HeaderStyle CssClass="GridHeaderStyle" />
            <ItemStyle CssClass="GridItemStyle" />
            <ControlStyle CssClass="GridControlStyle" />
        </asp:BoundField>
        <asp:BoundField DataField="Hide" HeaderText="Hidden" ReadOnly="True" SortExpression="Hide">
            <HeaderStyle CssClass="GridHeaderStyle" />
            <ItemStyle CssClass="GridItemStyle" />
            <ControlStyle CssClass="GridControlStyle" />
        </asp:BoundField>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="btn-group" style="margin: 5px;">
                    <asp:HyperLink runat="server" CssClass="btn btn-mini" NavigateUrl='<%#String.Format("~/UI/Pages/Admin/Gallery/FileManage.aspx?FileID={0}", Eval("FileID")) %>' Visible='<%# (OWDARO.Util.DataParser.BoolParse(IsEditModeHiddenField.Value)) ? true : false %>'>
                        <i class="icon-cog"></i> Manage
                    </asp:HyperLink>
                    <asp:HyperLink runat="server" CssClass="btn btn-mini" data-FileID='<%# Eval("FileID") %>'
                        data-ImageSrc='<%# ResolveClientUrl(OWDARO.Util.Utilities.GetImageThumbURL((int?)Eval("ImageID"))) %>'
                        data-Title='<%# string.Format("<strong>{0}</strong>{1}<br /><strong>File Type:</strong> {2}", Eval("Title"), ((string.IsNullOrWhiteSpace((string)Eval("SubTitle"))) ? "" : string.Format("<br />{0}", Eval("SubTitle"))), Eval("GY_FileTypes.Title") ) %>'
                        Visible='<%# (OWDARO.Util.DataParser.BoolParse(IsEditModeHiddenField.Value)) ? false : true %>'
                        onclick="OnSelect(this)">
                        <i class="icon-cog"></i> Select
                    </asp:HyperLink>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle CssClass="GridHeaderStyle" />
    <PagerStyle CssClass="GridPagerStyle" />
    <RowStyle CssClass="GridRowStyle" />
</asp:GridView>
<br />
<asp:HiddenField runat="server" ID="IsEditModeHiddenField" Value="true" />
<asp:HiddenField runat="server" ID="FileIDHiddenField" />
<script type="text/javascript">
    function OnSelect(element) {
        if (window.opener != null && !window.opener.closed) {
            var fileIDHiddenField = window.opener.document.getElementById(getParameterByName('ID'));
            var image = window.opener.document.getElementById(getParameterByName('imgID'));
            var title = window.opener.document.getElementById(getParameterByName('titleID'));

            fileIDHiddenField.value = element.getAttribute("data-FileID");
            image.src = element.getAttribute("data-ImageSrc");
            title.innerHTML = element.getAttribute("data-Title");
        }
        window.close();
    }

    function getParameterByName(name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }
</script>