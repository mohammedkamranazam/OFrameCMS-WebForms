<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SecurityQAComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.OFrameMembership.SecurityQAComponent" %>
<div class="onecolumn">
    <div class="header">
        <span><span class="ico color password "></span>Password Retrieval</span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="boxtitle">
            <span class="ico color lock "></span>Retrieve password after answering the security
            question
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Wizard ID="QAWizard" Width="100%" runat="server" BackColor="Transparent" ActiveStepIndex="0"
                    DisplaySideBar="False" FinishCompleteButtonText="Submit" FinishPreviousButtonText="Back"
                    SkipLinkText="" StepPreviousButtonText="Back" OnFinishButtonClick="QAWizard_FinishButtonClick"
                    OnNextButtonClick="QAWizard_NextButtonClick" DisplayCancelButton="True" OnPreviousButtonClick="QAWizard_PreviousButtonClick"
                    OnCancelButtonClick="QAWizard_CancelButtonClick">
                    <FinishCompleteButtonStyle CssClass="uibutton" />
                    <FinishPreviousButtonStyle CssClass="uibutton confirm icon previous" />
                    <StartNextButtonStyle CssClass="uibutton icon confirm next" />
                    <StepNextButtonStyle CssClass="uibutton icon confirm next" />
                    <StepPreviousButtonStyle CssClass="uibutton icon confirm previous" />
                    <CancelButtonStyle CssClass="uibutton icon special cancel" />
                    <WizardSteps>
                        <asp:WizardStep ID="WizardStep1" runat="server">
                            <OWD:TextBoxAdv runat="server" ID="UsernameTextBox" LabelText="Username" MaxLength="50" RequiredErrorMessage="enter the username" />
                        </asp:WizardStep>
                        <asp:WizardStep ID="WizardStep2" runat="server">
                            <OWD:LabelAdv runat="server" ID="SecurityQuestionLiteral" LabelText="Security Question" SmallLabelText="answer this question to get your password" />
                            <OWD:TextBoxAdv runat="server" ID="SecurityAnswerTextBox" LabelText="Security Answer" MaxLength="200" RequiredErrorMessage="enter the security answer" />
                        </asp:WizardStep>
                    </WizardSteps>
                </asp:Wizard>
                <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
<style>
    .previous {
        display: none;
    }
</style>