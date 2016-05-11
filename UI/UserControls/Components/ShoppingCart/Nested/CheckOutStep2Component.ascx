<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckOutStep2Component.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.ShoppingCart.CheckOutStep2Component" %>

<div class="Clear"></div>
<asp:Wizard ID="Wizard1" CssClass="Wizard" runat="server" OnActiveStepChanged="Wizard1_ActiveStepChanged" DisplaySideBar="False" Width="100%"
    BackColor="Transparent" ActiveStepIndex="0" FinishCompleteButtonText="Confirm Order" FinishPreviousButtonText="Back"
    SkipLinkText="" StepPreviousButtonText="Back" CancelButtonText="Back To Cart" OnFinishButtonClick="Wizard1_FinishButtonClick"
    DisplayCancelButton="True" OnCancelButtonClick="Wizard1_CancelButtonClick">
    <FinishCompleteButtonStyle CssClass="btn btn-success" />
    <FinishPreviousButtonStyle CssClass="btn" />
    <StartNextButtonStyle CssClass="btn btn-primary" />
    <StepNextButtonStyle CssClass="btn btn-primary" />
    <StepPreviousButtonStyle CssClass="btn" />
    <CancelButtonStyle CssClass="btn btn-inverse" />
    <WizardSteps>
        <asp:WizardStep ID="WizardStep1" runat="server" Title="Delivery Details">
            <h1>Delivery Details</h1>
            <OWD:UserAddressSelectionComponent runat="server" ID="DeliveryAddressSelectionComponent" />
            <OWD:UserAddressComponent runat="server" ID="DeliveryAddressComponent" StreetLabelText="Street Name" StreetSmallLabelText="complete street address"
                StreetMaxLength="200" StreetRequiredErrorMessage="street name is required" CityLabelText="City" CitySmallLabelText="city of residence" CityMaxLength="50"
                CityRequiredErrorMessage="city name is required" ZipCodeLabelText="Zip Code" ZipCodeSmallLabelText="zipcode or pincode of the city" ZipCodeMaxLength="10"
                ZipCodeRequiredErrorMessage="zip code is required" StateLabelText="State" StateSmallLabelText="state or province name" StateMaxLength="50"
                StateRequiredErrorMessage="state is required" CountryLabelText="Country" CountrySmallLabelText="country of residence" />
            <OWD:CheckBoxAdv runat="server" ID="SaveThisDeliveryAddressCheckBox" LabelText="Save This" SmallLabelText="save this address for future use"
                HelpLabelText="check this to save this address for future use" />
            <OWD:CheckBoxAdv runat="server" ID="UseForBillingCheckBox" LabelText="Billing Details Same As Delivery Details" SmallLabelText="use information for billing details"
                HelpLabelText="check this to use the information for billing details" />
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStep2" runat="server" Title="Billing Details">
            <h1>Billing Details</h1>
            <OWD:UserAddressSelectionComponent runat="server" ID="BillingAddressSelectionComponent" />
            <OWD:UserAddressComponent runat="server" ID="BillingAddressComponent" StreetLabelText="Street Name" StreetSmallLabelText="complete street address"
                StreetMaxLength="200" StreetRequiredErrorMessage="street name is required" CityLabelText="City" CitySmallLabelText="city of residence" CityMaxLength="50"
                CityRequiredErrorMessage="city name is required" ZipCodeLabelText="Zip Code" ZipCodeSmallLabelText="zipcode or pincode of the city" ZipCodeMaxLength="10"
                ZipCodeRequiredErrorMessage="zip code is required" StateLabelText="State" StateSmallLabelText="state or province name" StateMaxLength="50"
                StateRequiredErrorMessage="state is required" CountryLabelText="Country" CountrySmallLabelText="country of residence" />
            <OWD:CheckBoxAdv runat="server" ID="SaveThisBillingAddressCheckBox" LabelText="Save This" SmallLabelText="save this address for future use"
                HelpLabelText="check this to save this address for future use" />
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStep3" runat="server" Title="Contact Details">
            <h1>Contact Details</h1>
            <OWD:TextBoxAdv runat="server" ID="EmailIDTextBox" MaxLength="150" EnablePopUp="true" LabelText="Email ID" SmallLabelText="email id to confirm the order"
                RequiredErrorMessage="email id is required" PopUpPosition="Bottom" AutoCompleteType="Email" />
            <OWD:TextBoxAdv runat="server" ID="MobileTextBox" MaxLength="20" EnablePopUp="true" LabelText="Mobile" SmallLabelText="mobile number to get notifications"
                RequiredErrorMessage="mobile number is required" PopUpPosition="Bottom" AutoCompleteType="Cellular" />
        </asp:WizardStep>
        <asp:WizardStep ID="WizardStep4" runat="server" Title="Confirm Order">
            <asp:Panel runat="server" ID="RecieptPanel" Style="width: 1000px; border: 1px dashed black; margin: 20px auto; box-sizing: border-box;">
                <div style="background: #4F81BD; line-height: 30px; color: white; font-family: Verdana; font-size: 12px; padding-left: 20px; padding-right: 20px;">
                    <span style="float: left;">Order Number:
                                    <asp:Label ID="OrderNumberLabel" runat="server" /></span>
                    <span style="float: right;">Date & Time:
                                    <asp:Label ID="DateTimeLabel" runat="server" /></span>
                    <div style="clear: both; height: 0px; padding: 0px; margin: 0px; line-height: 0px;"></div>
                </div>
                <div style="min-height: 100px; padding: 20px; font-family: Verdana; font-size: 12px; color: black; border-bottom: 1px solid #eeeeee; margin-bottom: 20px;">
                    <img src='<%: OWDARO.Util.Utilities.GetAbsoluteURL(OWDARO.AppConfig.LogoRelativeURL) %>' style="float: left; max-width: 300px; max-height: 100px;" />
                    <div style="float: right; width: 300px;">
                        <h1 style="font-family: Verdana; font-size: 15px; color: black; font-weight: bold; line-height: 20px; display: block; border: none; text-decoration: underline; padding: 0px;
                            margin: 0px;">
                            <%: OWDARO.AppConfig.SiteName %>
                        </h1>
                        <p style="font-family: Verdana; font-size: 12px; color: black; line-height: 20px; margin: 0px;">
                            <%= OWDARO.AppConfig.ReceiptAddress %>
                        </p>
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <div style="float: left; width: 400px;">
                    <h2 style="display: block; font-family: Verdana; font-size: 13px; font-weight: bold; color: black; text-decoration: underline; margin-bottom: 10px; padding-left: 20px;">
                        Billing Address
                    </h2>
                    <span style="padding-left: 25px; width: 350px; display: block; font-family: Verdana; font-size: 12px; color: black; line-height: 20px;">
                        <asp:Label ID="BillingAddressLabel" runat="server" /></span>
                </div>
                <div style="float: right; width: 400px;">
                    <h2 style="display: block; font-family: Verdana; font-size: 13px; font-weight: bold; color: black; text-decoration: underline; margin-bottom: 10px; padding-left: 20px;">
                        Shipping Address
                    </h2>
                    <span style="padding-left: 25px; width: 350px; display: block; font-family: Verdana; font-size: 12px; color: black; line-height: 20px;">
                        <asp:Label ID="ShippingAddressLabel" runat="server" />
                    </span>
                </div>
                <div style="clear: both; height: 0px; padding: 0px; margin: 0px; line-height: 0px;"></div>
                <div style="line-height: 20px; font-family: Verdana; font-size: 12px; color: black; padding-left: 20px; margin-top: 10px;">
                    <span style="font-weight: bold;">Email ID:</span>
                    <asp:Label ID="EmailIDLabel" runat="server" />
                </div>
                <div style="line-height: 20px; font-family: Verdana; font-size: 12px; color: black; padding-left: 20px;">
                    <span style="font-weight: bold;">Mobile:</span>
                    <asp:Label ID="MobileLabel" runat="server" />
                </div>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Style="width: 860px; margin: 20px auto; margin-bottom: 0px;"
                    ShowFooter="False" GridLines="None" BorderStyle="None">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <div style="width: 50px; background: #4F81BD; height: 20px; float: left;">
                                </div>
                                <div style="width: 295px; background: #4F81BD; height: 20px; color: white; line-height: 20px; padding-left: 5px; font-family: Verdana; font-size: 11px; font-weight: bold;
                                    float: left;">
                                    Product
                                </div>
                                <div style="width: 105px; background: #4F81BD; height: 20px; color: white; line-height: 20px; padding-left: 5px; font-family: Verdana; font-size: 11px; font-weight: bold;
                                    float: left;">
                                    Details
                                </div>
                                <div style="width: 105px; background: #4F81BD; height: 20px; color: white; line-height: 20px; padding-left: 5px; font-family: Verdana; font-size: 11px; font-weight: bold;
                                    float: left;">
                                    Quantity
                                </div>
                                <div style="width: 105px; background: #4F81BD; height: 20px; color: white; line-height: 20px; padding-left: 5px; font-family: Verdana; font-size: 11px; font-weight: bold;
                                    float: left;">
                                    Price
                                </div>
                                <div style="width: 173px; background: #4F81BD; height: 20px; color: white; line-height: 20px; padding-left: 5px; font-family: Verdana; font-size: 11px; font-weight: bold;
                                    float: left;">
                                    Sub Total
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="border-bottom: 1px solid #eeeeee;">
                                    <img style="width: 50px; height: 50px; margin: 0px; float: left" width="50" height="50" src='<%# string.Format("http{0}://{1}{2}", (Request.IsSecureConnection) ? "s" : "", Request.Url.Host, Page.ResolveUrl(ProjectJKL.BLL.ShoppingCartBLL.ProductImagesBL.GetProductFirstImageThumb((int)Eval("ProductID"))))  %>' />
                                    <div style="width: 290px; float: left; margin: 0px; padding: 5px;">
                                        <span style="font-weight: bold; font-family: Verdana; font-size: 10px; display: block; color: black;"><%#Eval("SC_Products.Title") %></span>
                                        <small style="font-family: Verdana; font-size: 9px; color: black;"><%#Eval("SC_Products.SubTitle") %></small>
                                    </div>
                                    <div style="width: 100px; float: left; margin: 0px; padding: 5px;">
                                        <span style="font-family: Verdana; font-size: 10px; display: block; color: black; line-height: 13px;">
                                            <strong>Itm. No.: </strong><%#Eval("SC_Products.ItemNumber") %>
                                        </span>
                                        <span style="font-family: Verdana; font-size: 10px; display: block; color: black; line-height: 13px;">
                                            <%#ProjectJKL.BLL.ShoppingCartBLL.ColorsBL.GetColor((int)Eval("ProductID")) %>
                                        </span>
                                        <span style="font-family: Verdana; font-size: 10px; display: block; color: black; line-height: 13px;">
                                            <%#ProjectJKL.BLL.ShoppingCartBLL.SizesBL.GetSize((int)Eval("ProductID")) %>
                                        </span>
                                    </div>
                                    <div style="width: 100px; float: left; margin: 0px; padding: 5px;">
                                        <span style="font-family: Verdana; font-size: 10px; display: block; color: black;">
                                            <%#Eval("Quantity") %>
                                            <%#(ProjectJKL.BLL.ShoppingCartBLL.CartProcessingBL.IsProductOutOfStock((ProjectJKL.AppCode.DAL.ShoppingCartModel.SC_Products)Eval("SC_Products"))) ? "<span style='color:red; font-weight:bold; font-size:9px;'> [Out of Stock]</span>": "" %>
                                        </span>
                                    </div>
                                    <div style="width: 100px; float: left; margin: 0px; padding: 5px;">
                                        <span style="font-family: Verdana; font-size: 10px; display: block; color: black;">
                                            <%# ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.GetPrice((int)Eval("ProductID")) %>
                                        </span>
                                    </div>
                                    <div style="width: 170px; float: left; margin: 0px; padding: 5px 0px 5px 5px;">
                                        <span style="font-family: Verdana; font-size: 10px; display: block; color: black;">
                                            <%#String.Format("{0:0.00}", Convert.ToDouble(Eval("Quantity")) * ProjectJKL.BLL.ShoppingCartBLL.ProductsBL.GetPrice((int)Eval("ProductID")) ) %>
                                        </span>
                                    </div>
                                    <div style="clear: both;"></div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div style="width: 860px; margin: 0 auto; height: 66px;">
                    <div style="width: 570px; float: left; height: 66px;">
                    </div>
                    <div style="width: 105px; background: #F79646; height: 23px; color: white; line-height: 23px; padding-left: 5px; font-family: Verdana; font-size: 10px; font-weight: bold;
                        float: left;">
                        Total:
                    </div>
                    <div style="width: 175px; background: #F79646; height: 23px; color: white; line-height: 23px; padding-left: 5px; font-family: Verdana; font-size: 10px; font-weight: bold;
                        float: right;">
                        <asp:Label runat="server" ID="OrderTotalLabel"></asp:Label>
                    </div>
                    <div style="width: 105px; background: #F79646; height: 23px; color: white; line-height: 23px; padding-left: 5px; font-family: Verdana; font-size: 10px; font-weight: bold;
                        float: left;">
                        Shipment Cost:
                    </div>
                    <div style="width: 175px; background: #F79646; height: 23px; color: white; line-height: 23px; padding-left: 5px; font-family: Verdana; font-size: 10px; font-weight: bold;
                        float: right;">
                        <asp:Label runat="server" ID="ShippingCostLabel"></asp:Label>
                    </div>
                    <div style="width: 105px; background: #F79646; height: 23px; color: white; line-height: 23px; padding-left: 5px; font-family: Verdana; font-size: 10px; font-weight: bold;
                        float: left;">
                        Amount To Pay:
                    </div>
                    <div style="width: 175px; background: #F79646; height: 23px; color: white; line-height: 23px; padding-left: 5px; font-family: Verdana; font-size: 10px; font-weight: bold;
                        float: right;">
                        <asp:Label runat="server" ID="TotalAmountLabel"></asp:Label>
                    </div>
                </div>
                <br />
                <br />
            </asp:Panel>
        </asp:WizardStep>
    </WizardSteps>
</asp:Wizard>
<OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
<asp:HiddenField runat="server" Visible="false" ID="OrderNumberHiddenField" />

<style>
    .Wizard .uibutton {
        margin: 5px;
    }
</style>