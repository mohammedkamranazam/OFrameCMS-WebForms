<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="TestimonialAdd.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Media.TestimonialAdd" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add New Testimonial</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Media/TestimonialList.aspx">
                            <i class="icon-list-ul"></i> List Testimonials
                        </asp:HyperLink>
                    </div>
        <div class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="grid1">
                        <OWD:ImageSelectorComponent runat="server" ID="ImageSelectorComponent1" />
                    </div>
                    <div class="grid3">
                        <OWD:TextBoxAdv runat="server" ID="TestimonialTextBox" LabelText="Testimonial" SmallLabelText="testimonial text" RequiredErrorMessage="testimonial is required" MaxLength="500" />
                        <OWD:TextBoxAdv runat="server" ID="NameTextBox" LabelText="Name" SmallLabelText="name of the person" MaxLength="100" />
                        <OWD:TextBoxAdv runat="server" ID="PositionTextBox" LabelText="Position" SmallLabelText="position of the person" MaxLength="100" />
                        <OWD:TextBoxAdv runat="server" ID="CompanyTextBox" LabelText="Company" SmallLabelText="company of the person giving testimonial" MaxLength="100" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </div>
                    <div class="Clear"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>