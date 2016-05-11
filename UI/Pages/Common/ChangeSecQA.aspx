<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="ChangeSecQA.aspx.cs" Inherits="OWDARO.UI.Pages.Common.ChangeSecQA" Async="true" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color lock "></span>Security Question And Answer</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="boxtitle">
                <span class="ico color security "></span>change secondary security settings here
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <OWD:TextBoxAdv runat="server" ID="CurrentPasswordTextBox" LabelText="Current Password"
                        SmallLabelText="password currently in use" RequiredErrorMessage="enter current password"
                        MaxLength="30" TextMode="Password" />
                    <OWD:TextBoxAdv runat="server" ID="NewQuestionTextBox" LabelText="New Question" SmallLabelText="will be asked at the time of password retrieval"
                        RequiredErrorMessage="enter new security question" />
                    <OWD:TextBoxAdv runat="server" ID="NewAnswerTextBox" LabelText="New Answer" SmallLabelText="the answer to your above security question"
                        RequiredErrorMessage="enter new security answer" />
                    <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowSave="true" ShowCancel="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusLabel" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:content>