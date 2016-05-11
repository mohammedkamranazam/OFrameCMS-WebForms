<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Media.PortfolioComponent" %>

<h1 class="PageTitle">Portfolio</h1>
<div class="Portfolio">
    <div class="timeline animated">
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="EntityDataSource1">
            <ItemTemplate>
                <div class="timeline-row">
                    <div class="timeline-time">
                        <%# OWDARO.Util.DataParser.GetDateFormattedString((DateTime)Eval("Date")) %>
                    </div>
                    <div class="timeline-icon">
                    </div>
                    <div class="panel timeline-content">
                        <div class="panel-body">
                            <%# OWDARO.Util.Utilities.GetFancyBoxHTML((int?)Eval("ImageID"), string.Empty, false, string.Empty) %>
                            <h2>
                                <%# Eval("Title") %>
                            </h2>
                            <span class="Client"><strong>Client:</strong>
                                <%# Eval("ME_Clients.Title") %>
                            </span>
                            <span class="Categories">
                                <strong>Categories:</strong>
                                <%# GetCategories((int)Eval("PortfolioID")) %>
                            </span>
                            <span class="URL"><strong>Link:</strong>
                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("URL") %>' Text="View Project" />
                            </span>
                            <div class="Description">
                                <%# Eval("Description") %>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="Clear"></div>
</div>
<asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=MediaEntities" DefaultContainerName="MediaEntities" EnableFlattening="False" EntitySetName="ME_Portfolios"
    Include="ME_Clients" OrderBy="it.Date">
</asp:EntityDataSource>