<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="ImageListAndUpload.aspx.cs" Inherits="OWDARO.UI.Pages.Helpers.ImageListAndUpload" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
    <style>
        .GridItemStyle {
            display: inline-table;
            float: left;
            border: none;
        }

        .GridRowStyle {
            display: inline-table;
            border: none;
        }

        .GridView td {
            border: none;
        }

        .ImageEditBlock {
            float: left;
            margin: 5px;
            width: 100px;
            height: 160px;
            border: 1px solid rgba(0,0,0,0.1);
        }

            .ImageEditBlock h3 {
                font-family: Verdana;
                font-size: 11px;
                line-height: 11px;
                height: 22px;
                overflow: hidden;
                font-weight: normal;
                text-align: justify;
                padding: 0px 2px 0px 2px;
                margin: 0px;
            }
    </style>
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Images</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" OnSorting="GridView1_Sorting" OnPageIndexChanging="GridView1_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField SortExpression="ImageID" HeaderText="Image">
                                <ItemTemplate>
                                    <div class="ImageEditBlock">
                                        <%# OWDARO.Util.Utilities.GetFancyBoxHTML((int)Eval("ImageID"), string.Empty, false, "style='width:100px; height:100px; margin:0px;'") %>
                                        <h3><%# Eval("Title")%></h3>
                                        <div class="btn-group" style="margin: 5px;">
                                            <asp:HyperLink runat="server" CssClass="btn btn-mini" onclick="OnSelect(this)"
                                                data-ImageSrc='<%# ResolveClientUrl(OWDARO.Util.Utilities.GetImageThumbURL((int)Eval("ImageID"))) %>'
                                                data-ImageID='<%# Eval("ImageID") %>'>
                                            <i class="icon-edit tooltip" title="Select"></i>
                                            </asp:HyperLink>
                                            <asp:HyperLink runat="server" CssClass="btn btn-mini"
                                                NavigateUrl='<%# string.Format("~/UI/Pages/Helpers/ImageListAndUpload.aspx?ImageID={0}&StoragePath={1}&imgID={2}&ID={3}",
                                        Eval("ImageID"),
                                        ((!string.IsNullOrWhiteSpace(Request.QueryString["StoragePath"])) ? Request.QueryString["StoragePath"] : string.Empty),
                                        Request.QueryString["imgID"], Request.QueryString["ID"]) %>'>
                                            <i class="icon-cog tooltip" title="Manage"></i>
                                            </asp:HyperLink>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <PagerStyle CssClass="GridPagerStyle" />
                        <RowStyle CssClass="GridRowStyle" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <OWD:ImageUploaderComponent runat="server" ID="ImageUploader1" />
    <script type="text/javascript">
        function OnSelect(element) {
            if (window.opener != null && !window.opener.closed) {
                var imageIDHiddenField = window.opener.document.getElementById(getParameterByName('ID'));
                var image = window.opener.document.getElementById(getParameterByName('imgID'));

                imageIDHiddenField.value = element.getAttribute("data-ImageID");
                image.src = element.getAttribute("data-ImageSrc");
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
</asp:content>