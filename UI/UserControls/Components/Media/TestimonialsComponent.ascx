<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestimonialsComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Media.TestimonialsComponent" %>

<div class="TestimonialsComponent">
    <div class="Container">
        <div class="TestimonialCarousel">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="TestimonialItem">
                        <asp:Image runat="server" ImageUrl='<%# OWDARO.Util.Utilities.GetImageThumbURL((int?)Eval("ImageID")) %>' />
                        <h3><%# Eval("Name") %></h3>
                        <%#
                string.Format("{0}",
                ((string.IsNullOrWhiteSpace((string)Eval("Company")))?((string.IsNullOrWhiteSpace((string)Eval("Position")))?"": "[ " + Eval("Position") + " ]"):
                ((string.IsNullOrWhiteSpace((string)Eval("Position")))? "[ " + Eval("Company") + " ]" : "[ " + Eval("Position") + ", " + Eval("Company") + " ]")))
                        %>
                        <p>"<%# Eval("Testimonial") %>"</p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>