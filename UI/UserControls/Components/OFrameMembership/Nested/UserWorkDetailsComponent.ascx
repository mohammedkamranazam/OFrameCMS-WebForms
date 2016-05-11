<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserWorkDetailsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserWorkDetailsComponent" %>

<div class="onecolumn">
    <div class="header">
        <span><span class="ico color pyramid"></span>Work Details</span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color pyramid"></span>add your work details over here
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <OWD:UserWorkComponent runat="server" ID="UserWorkComponent" ValidationGroup="WorkGroup" />
                <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCustom="true" CustomButtonText="Add Work Details" ValidationGroup="WorkGroup" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                <div class="GridSection">
                    <asp:GridView runat="server" ID="GridView" GridLines="None" OnRowDeleting="GridView_RowDeleting"
                        AutoGenerateColumns="false" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Company">
                                <ItemTemplate>
                                    <%#Eval("Organization")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Worked As">
                                <ItemTemplate>
                                    <%#Eval("Position")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="City">
                                <ItemTemplate>
                                    <%#Eval("City")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Country">
                                <ItemTemplate>
                                    <%#Eval("Country")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <%#Eval("Description")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Working Here">
                                <ItemTemplate>
                                    <%#(((bool)Eval("WorkHere"))) ? "Yes" : "No"%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Joined On">
                                <ItemTemplate>
                                    <%# OWDARO.Util.DataParser.GetDateFormattedString((DateTime?)Eval("StartDate"))%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Left On">
                                <ItemTemplate>
                                    <%# OWDARO.Util.DataParser.GetDateFormattedString((DateTime?)Eval("EndDate"))%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="IDHiddenField" Value='<%#Eval("WorkID") %>' />
                                    <asp:ImageButton ID="ImageButton2" ImageAlign="Middle" ImageUrl="~/Themes/Zice/Graphics/icon/icon_delete.png"
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