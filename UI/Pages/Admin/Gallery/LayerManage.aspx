<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="LayerManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.LayerManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Layer Properties</span>
        </div>
        <div class="Clear">
        </div>
        <div class="uibutton-toolbar btn-group">
                <asp:HyperLink runat="server" CssClass="btn btn-primary" ID="SlideHyperLink">
                    <i class="icon-arrow-left"></i> Go Back To Slide
                </asp:HyperLink>
                <asp:HyperLink runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/SlideList.aspx">
                    <i class="icon-list-ul"></i> List Slides
                </asp:HyperLink>
            </div>
        <div class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend>Incoming Animation
                        </legend>
                        <OWD:TextBoxAdv runat="server" ID="LayerTitleTextBox" LabelText="Title" RequiredErrorMessage="title is required" MaxLength="250" />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckbox" LabelText="Hide" HelpLabelText="switch on to hide the layer" />
                        <OWD:DropDownListAdv runat="server" ID="AnimateInClassDropDownList" LabelText="Animate In" OnChange="CustomIn();" />
                        <div id="CustomInAnimationPanel">
                            <OWD:Slider runat="server" ID="InTransitionXSlider" LabelText="Transition X" Maximum="2500" Minimum="-2500" Value="0" />
                            <OWD:Slider runat="server" ID="InTransitionYSlider" LabelText="Transition Y" Maximum="2500" Minimum="-2500" Value="0" />
                            <OWD:Slider runat="server" ID="InTransitionZSlider" LabelText="Transition Z" Maximum="2500" Minimum="-2500" Value="0" />

                            <OWD:Slider runat="server" ID="InRotationXSlider" LabelText="Rotation X" Maximum="3600" Minimum="-3600" Value="0" />
                            <OWD:Slider runat="server" ID="InRotationYSlider" LabelText="Rotation Y" Maximum="3500" Minimum="-3600" Value="0" />
                            <OWD:Slider runat="server" ID="InRotationZSlider" LabelText="Rotation Z" Maximum="3600" Minimum="-3600" Value="0" />

                            <OWD:Slider runat="server" ID="InScaleXSlider" LabelText="Scale X" Maximum="10" Minimum="-10" Value="1" />
                            <OWD:Slider runat="server" ID="InScaleYSlider" LabelText="Scale Y" Maximum="10" Minimum="-10" Value="1" />

                            <OWD:Slider runat="server" ID="InSkewXSlider" LabelText="Skew X" Maximum="3600" Minimum="-3600" Value="0" />
                            <OWD:Slider runat="server" ID="InSkewYSlider" LabelText="Skew Y" Maximum="3600" Minimum="-3600" Value="0" />

                            <OWD:Slider runat="server" ID="InOpacitySlider" LabelText="Opacity" Maximum="100" Minimum="0" Value="100" />

                            <OWD:Slider runat="server" ID="InPerspectiveSlider" LabelText="Perspective" Maximum="1600" Minimum="300" Value="600" />

                            <OWD:Slider runat="server" ID="InOriginXSlider" LabelText="Origin X" Maximum="1000" Minimum="-1000" Value="50" />
                            <OWD:Slider runat="server" ID="InOriginYSlider" LabelText="Origin Y" Maximum="1000" Minimum="-1000" Value="50" />
                        </div>
                        <OWD:DropDownListAdv runat="server" ID="DataXDropDownList" LabelText="Data X" OnChange="DataXChange();" />
                        <OWD:Slider runat="server" ID="DataXSlider" LabelText="Data X Pixels" Maximum="2500" Minimum="-2500" Value="0" />
                        <OWD:Slider runat="server" ID="DataHOffsetSlider" LabelText="Data H Offset" Maximum="2000" Value="0" Minimum="-2000" />
                        <OWD:Slider runat="server" ID="DataStartSlider" LabelText="Start" Maximum="600000" Minimum="0" Value="0" />
                        <OWD:Slider runat="server" ID="DataSpeedSlider" LabelText="Start Speed" Maximum="600000" Minimum="0" Value="0" />
                        <OWD:DropDownListAdv runat="server" ID="DataStartEasingDropDownList" LabelText="Start Easing" />
                    </fieldset>
                    <fieldset>
                        <legend>Outgoing Animation
                        </legend>
                        <OWD:DropDownListAdv runat="server" ID="AnimateOutClassDropDownList" LabelText="Animate Out" OnChange="CustomOut();" />
                        <div id="CustomOutAnimationPanel">
                            <OWD:Slider runat="server" ID="OutTransitionXSlider" LabelText="Transition X" Maximum="2500" Minimum="-2500" Value="0" />
                            <OWD:Slider runat="server" ID="OutTransitionYSlider" LabelText="Transition Y" Maximum="2500" Minimum="-2500" Value="0" />
                            <OWD:Slider runat="server" ID="OutTransitionZSlider" LabelText="Transition Z" Maximum="2500" Minimum="-2500" Value="0" />

                            <OWD:Slider runat="server" ID="OutRotationXSlider" LabelText="Rotation X" Maximum="3600" Minimum="-3600" Value="0" />
                            <OWD:Slider runat="server" ID="OutRotationYSlider" LabelText="Rotation Y" Maximum="3500" Minimum="-3600" Value="0" />
                            <OWD:Slider runat="server" ID="OutRotationZSlider" LabelText="Rotation Z" Maximum="3600" Minimum="-3600" Value="0" />

                            <OWD:Slider runat="server" ID="OutScaleXSlider" LabelText="Scale X" Maximum="10" Minimum="-10" Value="1" />
                            <OWD:Slider runat="server" ID="OutScaleYSlider" LabelText="Scale Y" Maximum="10" Minimum="-10" Value="1" />

                            <OWD:Slider runat="server" ID="OutSkewXSlider" LabelText="Skew X" Maximum="3600" Minimum="-3600" Value="0" />
                            <OWD:Slider runat="server" ID="OutSkewYSlider" LabelText="Skew Y" Maximum="3600" Minimum="-3600" Value="0" />

                            <OWD:Slider runat="server" ID="OutOpacitySlider" LabelText="Opacity" Maximum="100" Minimum="0" Value="100" />

                            <OWD:Slider runat="server" ID="OutPerspectiveSlider" LabelText="Perspective" Maximum="1600" Minimum="300" Value="600" />

                            <OWD:Slider runat="server" ID="OutOriginXSlider" LabelText="Origin X" Maximum="1000" Minimum="-1000" Value="50" />
                            <OWD:Slider runat="server" ID="OutOriginYSlider" LabelText="Origin Y" Maximum="1000" Minimum="-1000" Value="50" />
                        </div>
                        <OWD:DropDownListAdv runat="server" ID="DataYDropDownList" LabelText="Data Y" OnChange="DataYChange();" />
                        <OWD:Slider runat="server" ID="DataYSlider" LabelText="Data Y Pixels" Maximum="2500" Minimum="-2500" Value="0" />
                        <OWD:Slider runat="server" ID="DataVOffsetSlider" LabelText="Data V Offset" Maximum="2000" Value="0" Minimum="-2000" />
                        <OWD:Slider runat="server" ID="DataEndSlider" LabelText="End" Maximum="600000" Minimum="0" Value="0" />
                        <OWD:Slider runat="server" ID="DataSpeedEndSlider" LabelText="End Speed" Maximum="600000" Minimum="0" Value="0" />
                        <OWD:DropDownListAdv runat="server" ID="DataEndEasingDropDownList" LabelText="End Easing" />
                    </fieldset>
                    <OWD:DropDownListAdv runat="server" ID="StylingClassDropDownList" LabelText="Styling Class" />
                    <OWD:TextBoxAdv runat="server" ID="LayerStyleAttributeTextBox" LabelText="Style" />
                    <OWD:DropDownListAdv runat="server" ID="CaptionContentTypeDropDownList" LabelText="Caption Type" OnChange="CaptionTypeChange();" />
                    <OWD:CheckBoxAdv runat="server" ID="IsCaptionVideoFullScreenCheckBox" LabelText="Fullscreen Video" />
                    <OWD:CheckBoxAdv runat="server" ID="VideoAutoPlayCheckbox" LabelText="Auto Play" />
                    <OWD:CheckBoxAdv runat="server" ID="VideoAutoPlayOnFirstTimeCheckBox" LabelText="Auto Play On First time" />
                    <OWD:CheckBoxAdv runat="server" ID="VideoNextSlideAtEndCheckBox" LabelText="Next Slide On Video End" />
                    <OWD:DropDownListAdv runat="server" ID="SplitTypeInDropDownlist" LabelText="Text Split Type In" />
                    <OWD:DropDownListAdv runat="server" ID="SplitTypeOutDropDownlist" LabelText="Text Split Type Out" />
                    <OWD:Slider runat="server" ID="ElementInDelaySlider" LabelText="Element Delay In" Maximum="10000" Minimum="0" Value="100" />
                    <OWD:Slider runat="server" ID="ElementOutDelaySlider" LabelText="Element Delay Out" Maximum="10000" Minimum="0" Value="100" />
                    <OWD:CKEditor runat="server" ID="CaptionContentEditor" LabelText="Caption Content" EditorHeight="100px" />
                    <OWD:CheckBoxAdv runat="server" ID="CaptionHiddenOnCheckBox" LabelText="Caption Hidden On" />
                    <OWD:FormToolbar runat="server" ID="LayerToolbar" ShowSave="true" ShowDelete="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="LayerStatusMessage" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <script type="text/javascript">

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            SliderScript();
            ReInitialize();
        });

        function SliderScript() {
            $(document).ready(function () {

                CustomIn();

                CustomOut();

                DataXChange();

                DataYChange();

                CaptionTypeChange();

            });

            function CustomIn() {
                var element = document.getElementById("<%= AnimateInClassDropDownList.DropDownList.ClientID %>");
                var selectedOption = element.options[element.selectedIndex];
                if (selectedOption.text == "Custom") {
                    document.getElementById("CustomInAnimationPanel").style.display = "block";
                }
                else {
                    document.getElementById("CustomInAnimationPanel").style.display = "none";
                }
            }

            function CustomOut() {
                var element = document.getElementById("<%= AnimateOutClassDropDownList.DropDownList.ClientID %>");
                var selectedOption = element.options[element.selectedIndex];
                if (selectedOption.text == "Custom") {
                    document.getElementById("CustomOutAnimationPanel").style.display = "block";
                }
                else {
                    document.getElementById("CustomOutAnimationPanel").style.display = "none";
                }
            }

            function CaptionTypeChange() {
                var element = document.getElementById("<%= CaptionContentTypeDropDownList.DropDownList.ClientID %>");
                var selectedOption = element.options[element.selectedIndex];
                if (selectedOption.text == "Video") {
                    document.getElementById("<%= IsCaptionVideoFullScreenCheckBox.ContainerID %>").style.display = "block";
                    document.getElementById("<%= VideoAutoPlayCheckbox.ContainerID %>").style.display = "block";
                    document.getElementById("<%= VideoAutoPlayOnFirstTimeCheckBox.ContainerID %>").style.display = "block";
                    document.getElementById("<%= VideoNextSlideAtEndCheckBox.ContainerID %>").style.display = "block";

                    document.getElementById("<%= SplitTypeInDropDownlist.ContainerID %>").style.display = "none";
                    document.getElementById("<%= SplitTypeOutDropDownlist.ContainerID %>").style.display = "none";
                    document.getElementById("<%= ElementInDelaySlider.ContainerID %>").style.display = "none";
                    document.getElementById("<%= ElementOutDelaySlider.ContainerID %>").style.display = "none";
                }
                else {
                    document.getElementById("<%= IsCaptionVideoFullScreenCheckBox.ContainerID %>").style.display = "none";
                    document.getElementById("<%= VideoAutoPlayCheckbox.ContainerID %>").style.display = "none";
                    document.getElementById("<%= VideoAutoPlayOnFirstTimeCheckBox.ContainerID %>").style.display = "none";
                    document.getElementById("<%= VideoNextSlideAtEndCheckBox.ContainerID %>").style.display = "none";

                    document.getElementById("<%= SplitTypeInDropDownlist.ContainerID %>").style.display = "block";
                    document.getElementById("<%= SplitTypeOutDropDownlist.ContainerID %>").style.display = "block";
                    document.getElementById("<%= ElementInDelaySlider.ContainerID %>").style.display = "block";
                    document.getElementById("<%= ElementOutDelaySlider.ContainerID %>").style.display = "block";
                }
            }

            function DataXChange() {
                var element = document.getElementById("<%= DataXDropDownList.DropDownList.ClientID %>");
                var selectedOption = element.options[element.selectedIndex];
                if (selectedOption.text == "Custom") {
                    document.getElementById("<%= DataXSlider.ContainerID %>").style.display = "block";
                    document.getElementById("<%= DataHOffsetSlider.ContainerID %>").style.display = "none";
                }
                else {
                    document.getElementById("<%= DataXSlider.ContainerID %>").style.display = "none";
                    document.getElementById("<%= DataHOffsetSlider.ContainerID %>").style.display = "block";
                }
            }

            function DataYChange() {
                var element = document.getElementById("<%= DataYDropDownList.DropDownList.ClientID %>");
                var selectedOption = element.options[element.selectedIndex];
                if (selectedOption.text == "Custom") {
                    document.getElementById("<%= DataYSlider.ContainerID %>").style.display = "block";
                    document.getElementById("<%= DataVOffsetSlider.ContainerID %>").style.display = "none";
                }
                else {
                    document.getElementById("<%= DataYSlider.ContainerID %>").style.display = "none";
                    document.getElementById("<%= DataVOffsetSlider.ContainerID %>").style.display = "block";
                }
            }
        }

        $(document).ready(function () {
            CustomIn();

            CustomOut();

            DataXChange();

            DataYChange();

            CaptionTypeChange();

        });

        function CustomIn() {
            var element = document.getElementById("<%= AnimateInClassDropDownList.DropDownList.ClientID %>");
            var selectedOption = element.options[element.selectedIndex];
            if (selectedOption.text == "Custom") {
                document.getElementById("CustomInAnimationPanel").style.display = "block";
            }
            else {
                document.getElementById("CustomInAnimationPanel").style.display = "none";
            }
        }

        function CustomOut() {
            var element = document.getElementById("<%= AnimateOutClassDropDownList.DropDownList.ClientID %>");
            var selectedOption = element.options[element.selectedIndex];
            if (selectedOption.text == "Custom") {
                document.getElementById("CustomOutAnimationPanel").style.display = "block";
            }
            else {
                document.getElementById("CustomOutAnimationPanel").style.display = "none";
            }
        }

        function CaptionTypeChange() {
            var element = document.getElementById("<%= CaptionContentTypeDropDownList.DropDownList.ClientID %>");
            var selectedOption = element.options[element.selectedIndex];
            if (selectedOption.text == "Video") {
                document.getElementById("<%= IsCaptionVideoFullScreenCheckBox.ContainerID %>").style.display = "block";
                document.getElementById("<%= VideoAutoPlayCheckbox.ContainerID %>").style.display = "block";
                document.getElementById("<%= VideoAutoPlayOnFirstTimeCheckBox.ContainerID %>").style.display = "block";
                document.getElementById("<%= VideoNextSlideAtEndCheckBox.ContainerID %>").style.display = "block";
            }
            else {
                document.getElementById("<%= IsCaptionVideoFullScreenCheckBox.ContainerID %>").style.display = "none";
                document.getElementById("<%= VideoAutoPlayCheckbox.ContainerID %>").style.display = "none";
                document.getElementById("<%= VideoAutoPlayOnFirstTimeCheckBox.ContainerID %>").style.display = "none";
                document.getElementById("<%= VideoNextSlideAtEndCheckBox.ContainerID %>").style.display = "none";
            }
        }

        function DataXChange() {
            var element = document.getElementById("<%= DataXDropDownList.DropDownList.ClientID %>");
            var selectedOption = element.options[element.selectedIndex];
            if (selectedOption.text == "Custom") {
                document.getElementById("<%= DataXSlider.ContainerID %>").style.display = "block";
                document.getElementById("<%= DataHOffsetSlider.ContainerID %>").style.display = "none";
            }
            else {
                document.getElementById("<%= DataXSlider.ContainerID %>").style.display = "none";
                document.getElementById("<%= DataHOffsetSlider.ContainerID %>").style.display = "block";
            }
        }

        function DataYChange() {
            var element = document.getElementById("<%= DataYDropDownList.DropDownList.ClientID %>");
            var selectedOption = element.options[element.selectedIndex];
            if (selectedOption.text == "Custom") {
                document.getElementById("<%= DataYSlider.ContainerID %>").style.display = "block";
                document.getElementById("<%= DataVOffsetSlider.ContainerID %>").style.display = "none";
            }
            else {
                document.getElementById("<%= DataYSlider.ContainerID %>").style.display = "none";
                document.getElementById("<%= DataVOffsetSlider.ContainerID %>").style.display = "block";
            }
        }
    </script>
</asp:content>