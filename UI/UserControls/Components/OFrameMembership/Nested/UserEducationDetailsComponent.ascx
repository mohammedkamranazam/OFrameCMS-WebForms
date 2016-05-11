<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserEducationDetailsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserEducationDetailsComponent" %>
<div class="onecolumn">
    <div class="header">
        <span><span class="ico color pencil"></span>Educations</span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color pencil"></span>add your education details over here
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanle1">
            <ContentTemplate>
                <OWD:UserEducationComponent runat="server" ID="EducationComponent" ValidationGroup="EducationGroup" />
                <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCustom="true" CustomButtonText="Add Education" ValidationGroup="EducationGroup" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                <div class="GridSection">
                    <asp:GridView runat="server" ID="GridView" GridLines="None" OnRowDeleting="GridView_RowDeleting"
                        AutoGenerateColumns="false" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Institute Name">
                                <ItemTemplate>
                                    <%#Eval("InstituteName")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stream">
                                <ItemTemplate>
                                    <%#Eval("Stream")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate>
                                    <%#OWDARO.Util.DataParser.GetDateFormattedString(((DateTime?)Eval("StartDate")))%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate>
                                    <%#OWDARO.Util.DataParser.GetDateFormattedString(((DateTime?)Eval("EndDate")))%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qualification">
                                <ItemTemplate>
                                    <%#Eval("MS_EducationQualificationTypes.Title")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="IDHiddenField" Value='<%#Eval("EducationID") %>' />
                                    <asp:ImageButton ID="ImageButton1" ImageAlign="Middle" ImageUrl="~/Themes/Zice/Graphics/icon/icon_delete.png"
                                        runat="server" CommandName="Delete" Text="Delete" CausesValidation="False" />
                                </ItemTemplate>
                                <ItemStyle CssClass="ProfileManageGridRowsItemStyle" />
                                <ControlStyle CssClass="ProfileManageGridRowsControlStyle" />
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="ContactGridHeaderStyle" />
                        <RowStyle CssClass="ContactGridRowStyle" />
                        <AlternatingRowStyle CssClass="ContactGridAlternatingRowStyle" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>