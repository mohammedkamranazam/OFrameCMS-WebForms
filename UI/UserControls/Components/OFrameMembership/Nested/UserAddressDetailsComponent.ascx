<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAddressDetailsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.UserAddressDetailsComponent" %>

<div class="onecolumn">
    <div class="header">
        <span><span class="ico color diary"></span>Address</span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color diary"></span>add your address details over here
        </div>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <OWD:UserAddressComponent runat="server" ID="AddressComponent"
                    StreetLabelText="Street Name" StreetSmallLabelText="complete street address"
                    StreetMaxLength="200" StreetRequiredErrorMessage="street name is required" CityLabelText="City"
                    CitySmallLabelText="city of residence" CityMaxLength="50" CityRequiredErrorMessage="city name is required"
                    ZipCodeLabelText="Zip Code" ZipCodeSmallLabelText="zipcode or pincode of the city"
                    ZipCodeMaxLength="10" ZipCodeRequiredErrorMessage="zip code is required" StateLabelText="State"
                    StateSmallLabelText="state or province name" StateMaxLength="50" StateRequiredErrorMessage="state is required"
                    CountryLabelText="Country" CountrySmallLabelText="country of residence" ValidationGroup="AddressValidationGroup" />
                <OWD:DropDownListAdv runat="server" ID="CategoryDropDownList" LabelText="Address Type"
                    SmallLabelText="add your different addresses based upon type" RequiredFieldErrorMessage="please select the address category"
                    ValidationGroup="AddressValidationGroup" />
                <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCustom="true" CustomButtonText="Add Address"
                    ValidationGroup="AddressValidationGroup" />
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                <div class="GridSection">
                    <asp:GridView runat="server" ID="GridView" GridLines="None" OnRowDeleting="GridView_RowDeleting"
                        AutoGenerateColumns="false" AllowPaging="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Street Name">
                                <ItemTemplate>
                                    <%#Eval("StreetName")%>
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
                            <asp:TemplateField HeaderText="Zipcode">
                                <ItemTemplate>
                                    <%#Eval("ZipCode")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="State">
                                <ItemTemplate>
                                    <%#Eval("State")%>
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
                            <asp:TemplateField HeaderText="Address Category">
                                <ItemTemplate>
                                    <%#Eval("AddressCategory")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="ContactGridHeaderStyle" />
                                <ItemStyle CssClass="ContactGridItemStyle" />
                                <ControlStyle CssClass="ContactGridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="IDHiddenField" Value='<%#Eval("AddressID") %>' />
                                    <asp:ImageButton ID="ImageButton6" ImageAlign="Middle" ImageUrl="~/Themes/Zice/Graphics/icon/icon_delete.png"
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