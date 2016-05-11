<%@ Page Async="true" Title="" Language="C#" AutoEventWireup="true" CodeBehind="SlideManage.aspx.cs" Inherits="ProjectJKL.UI.Pages.Admin.Gallery.SlideManage" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Edit Slide</span>
        </div>
        <div class="Clear">
        </div>
         <div class="uibutton-toolbar btn-group">
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn" NavigateUrl="~/UI/Pages/Admin/Gallery/SlideList.aspx">
                <i class="icon-list-ul"></i> List Slides
                </asp:HyperLink>
                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btn btn-success" NavigateUrl="~/UI/Pages/Admin/Gallery/SlideAdd.aspx">
                <i class="icon-plus-sign"></i> Add Slide
                </asp:HyperLink>
            </div>
        <div class="content">
            <div class="grid1">
                <div class="center">
                    <asp:HyperLink runat="server" ID="fancybox" ClientIDMode="Static">
                        <asp:Image ID="CoverImage" runat="server" CssClass="ManageImage" />
                    </asp:HyperLink>
                </div>
            </div>
            <div class="grid3">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" SmallLabelText="name to identify the slide" RequiredErrorMessage="title is required" MaxLength="100" ValidationGroup="SlideValidationGroup" />
                        <OWD:TextBoxAdv runat="server" ID="PositionTextBox" LabelText="Position" SmallLabelText="slide position among other slides" MaxLength="7" FilterType="Numbers" ValidationGroup="SlideValidationGroup" />
                        <OWD:CheckBoxAdv runat="server" ID="HideCheckbox" LabelText="Hide" HelpLabelText="switch on to hide the slide" />
                        <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCancel="true" ShowSave="true" ShowDelete="true" ValidationGroup="SlideValidationGroup" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="Clear"></div>
        </div>
    </div>

    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Edit Slide Properties</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <OWD:DropDownListAdv runat="server" ID="SlideTypeDropDownList" LabelText="Slide Type" InitialValue="-1" OnChange="SlideTypeChange();" RequiredFieldErrorMessage="please select slide type" ValidationGroup="SlidePropertiesValidation" />
            <div id="KenBurnSlidePropertiesPanel">
                <OWD:Slider runat="server" ID="DurationSlider" LabelText="Animation Duration" Maximum="600000" Minimum="0" Value="100" ValidationGroup="SlidePropertiesValidation" />
                <OWD:DropDownListAdv runat="server" ID="EaseDropDownList" LabelText="Animation Type" ValidationGroup="SlidePropertiesValidation" />
                <OWD:Slider runat="server" ID="BGFitSlider" LabelText="Background Fit" Maximum="2000" Minimum="0" Value="100" ValidationGroup="SlidePropertiesValidation" />
                <OWD:Slider runat="server" ID="BGFitEndSlider" LabelText="Background Fit End" Maximum="2000" Minimum="0" Value="100" ValidationGroup="SlidePropertiesValidation" />
                <OWD:DropDownListAdv runat="server" ID="KenBurnBGPositionDropDownList" LabelText="Background Position" ValidationGroup="SlidePropertiesValidation" />
                <OWD:DropDownListAdv runat="server" ID="KenBurnBGPositionEndDropDownList" LabelText="Background Position End" ValidationGroup="SlidePropertiesValidation" />
            </div>
            <div id="SimpleSlidePropertiesPanel">
                <OWD:DropDownListAdv runat="server" ID="BGRepeatDropDownList" LabelText="Background Repeat" ValidationGroup="SlidePropertiesValidation" />
                <OWD:DropDownListAdv runat="server" ID="BGFitDropDownList" LabelText="Background Fit" InitialValue="-1" OnChange="BGFitTypeChange();" ValidationGroup="SlidePropertiesValidation" />
                <OWD:Slider runat="server" ID="BGFitWidthSlider" LabelText="Width" Maximum="2000" Minimum="0" Value="100" ValidationGroup="SlidePropertiesValidation" />
                <OWD:Slider runat="server" ID="BGFitHeightSlider" LabelText="Height" Maximum="2000" Minimum="0" Value="100" ValidationGroup="SlidePropertiesValidation" />
                <OWD:DropDownListAdv runat="server" ID="BGPositionDropDownList" LabelText="Background Position" ValidationGroup="SlidePropertiesValidation" />
                <OWD:Slider runat="server" ID="SlotAmountSlider" LabelText="Slot Amount" Maximum="30" Minimum="0" Value="7" ValidationGroup="SlidePropertiesValidation" />
            </div>
            <OWD:DropDownListAdv runat="server" ID="TransitionDropDownList" LabelText="Transition" ValidationGroup="SlidePropertiesValidation" />
            <OWD:Slider runat="server" ID="DelaySlider" LabelText="Slide Display Time" Maximum="600000" Minimum="0" Value="10000" ValidationGroup="SlidePropertiesValidation" />
            <OWD:Slider runat="server" ID="MasterSpeedSlider" LabelText="Master Speed" Maximum="3000" Minimum="0" Value="1200" ValidationGroup="SlidePropertiesValidation" />
            <OWD:TextBoxAdv runat="server" ID="LinkTextBox" LabelText="Link" ValidationGroup="SlidePropertiesValidation" />
            <OWD:DropDownListAdv runat="server" ID="TargetDropDownList" LabelText="Link Target" ValidationGroup="SlidePropertiesValidation" />
            <OWD:DropDownListAdv runat="server" ID="SlideIndexDropDownList" LabelText="Slide Index" InitialValue="-1" OnChange="SlideIndexTypeChange();" ValidationGroup="SlidePropertiesValidation" />
            <OWD:Slider runat="server" ID="SlideIndexSlider" LabelText="Slide Index" Minimum="1" ValidationGroup="SlidePropertiesValidation" />
            <OWD:DropDownListAdv runat="server" ID="SlideBackgroundTypeDropDownList" LabelText="Slide Background" InitialValue="-1" OnChange="SetSlideBackgroundType();" RequiredFieldErrorMessage="please select slide background type" ValidationGroup="SlidePropertiesValidation" />
            <OWD:TextBoxAdv runat="server" ID="SlideBackgroundColorTextBox" LabelText="Color Hex Code" MaxLength="7" ValidationGroup="SlidePropertiesValidation" />
            <OWD:FileUploadAdv runat="server" ID="SlideBackgroundImageFileUpload" LabelText="Background Image" MaxFileSizeMB="5" ValidationGroup="SlidePropertiesValidation" />
            <OWD:FileUploadAdv runat="server" ID="SlideBackgroundThumbImageFileUpload" LabelText="Thumb Image" MaxFileSizeMB="5" ValidationGroup="SlidePropertiesValidation" />
            <OWD:FileUploadAdv runat="server" ID="SlideBackgroundLazyLoadImageFileUpload" LabelText="Lazy Load Image" MaxFileSizeMB="5" ValidationGroup="SlidePropertiesValidation" />
            <OWD:FormToolbar runat="server" ID="SlideToolbar" ShowCancel="true" ShowSave="true" ValidationGroup="SlidePropertiesValidation" />
            <OWD:StatusMessageJQuery runat="server" ID="SlideStatusMessage" />
        </div>
    </div>

    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Add Slide Layers</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend>Incoming Animation
                        </legend>
                        <OWD:TextBoxAdv runat="server" ID="LayerTitleTextBox" LabelText="Title" RequiredErrorMessage="title is required" MaxLength="250" />
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
                        <OWD:Slider runat="server" ID="DataStartSlider" LabelText="Start" Maximum="600000" Minimum="0" Value="1000" />
                        <OWD:Slider runat="server" ID="DataSpeedSlider" LabelText="Start Speed" Maximum="600000" Minimum="0" Value="1000" />
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
                        <OWD:Slider runat="server" ID="DataEndSlider" LabelText="End" Maximum="600000" Minimum="0" Value="9000" />
                        <OWD:Slider runat="server" ID="DataSpeedEndSlider" LabelText="End Speed" Maximum="600000" Minimum="0" Value="1000" />
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
                    <OWD:FormToolbar runat="server" ID="LayerToolbar" ShowSave="true" />
                    <OWD:StatusMessageJQuery runat="server" ID="LayerStatusMessage" />
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" CssClass="GridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" DataSourceID="EntityDataSource1">
                        <Columns>
                            <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                <ItemTemplate>
                                    <%#Eval("Title") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" SortExpression="LayerContentType">
                                <ItemTemplate>
                                    <%#Eval("LayerContentType") %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Hidden" SortExpression="Hide">
                                <ItemTemplate>
                                    <%# ((bool)Eval("Hide")) ? "Yes" : "No" %>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                                <ItemStyle CssClass="GridItemStyle" />
                                <ControlStyle CssClass="GridControlStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="btn-group" style="margin: 5px;">
                                        <asp:HyperLink runat="server" CssClass="btn btn-mini" NavigateUrl='<%# string.Format("LayerManage.aspx?LayerID={0}&SlideID={1}", Eval("LayerID"), Request.QueryString["SlideID"]) %>'>
                                            <i class="icon-cog"></i> Manage
                                        </asp:HyperLink>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <PagerStyle CssClass="GridPagerStyle" />
                        <RowStyle CssClass="GridRowStyle" />
                    </asp:GridView>
                    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=GalleryEntities" DefaultContainerName="GalleryEntities" EnableFlattening="False" EntitySetName="GY_Layers" Where="it.SlideID==@SlideID">
                        <WhereParameters>
                            <asp:QueryStringParameter Name="SlideID" DbType="Int64" QueryStringField="SlideID" />
                        </WhereParameters>
                    </asp:EntityDataSource>
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

                SlideTypeChange();

                BGFitTypeChange();

                SlideIndexTypeChange();

                CustomIn();

                CustomOut();

                DataXChange();

                DataYChange();

                CaptionTypeChange();

            });

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

            function SlideTypeChange() {
                var element = document.getElementById("<%= SlideTypeDropDownList.DropDownList.ClientID %>");
                var selectedOption = element.options[element.selectedIndex];
                if (selectedOption.text == "Simple") {
                    document.getElementById("KenBurnSlidePropertiesPanel").style.display = "none";
                    document.getElementById("SimpleSlidePropertiesPanel").style.display = "block";
                    document.getElementById("<%= SlideBackgroundTypeDropDownList.ContainerID %>").style.display = "block";
                    SetSlideBackgroundType();
                }
                if (selectedOption.text == "KenBurns") {
                    document.getElementById("KenBurnSlidePropertiesPanel").style.display = "block";
                    document.getElementById("SimpleSlidePropertiesPanel").style.display = "none";
                    document.getElementById("<%= SlideBackgroundTypeDropDownList.ContainerID %>").style.display = "none";
                    document.getElementById("<%= SlideBackgroundColorTextBox.ContainerID %>").style.display = "none";
                    document.getElementById("<%= SlideBackgroundImageFileUpload.ContainerID %>").style.display = "block";
                    document.getElementById("<%= SlideBackgroundThumbImageFileUpload.ContainerID %>").style.display = "block";
                    document.getElementById("<%= SlideBackgroundLazyLoadImageFileUpload.ContainerID %>").style.display = "block";
                }
            }

            function BGFitTypeChange() {
                var element = document.getElementById("<%= BGFitDropDownList.DropDownList.ClientID %>");
                var selectedOption = element.options[element.selectedIndex];
                if (selectedOption.text == "Cover" || selectedOption.text == "Contain" || selectedOption.text == "Normal") {
                    document.getElementById("<%= BGFitWidthSlider.ContainerID %>").style.display = "none";
                    document.getElementById("<%= BGFitHeightSlider.ContainerID %>").style.display = "none";
                }
                if (selectedOption.text == "Custom") {
                    document.getElementById("<%= BGFitWidthSlider.ContainerID %>").style.display = "block";
                    document.getElementById("<%= BGFitHeightSlider.ContainerID %>").style.display = "block";
                }
            }

            function SlideIndexTypeChange() {
                var element = document.getElementById("<%= SlideIndexDropDownList.DropDownList.ClientID %>");
                var selectedOption = element.options[element.selectedIndex];
                if (selectedOption.text == "None") {
                    document.getElementById("<%= SlideIndexSlider.ContainerID %>").style.display = "none";
                }
                if (selectedOption.text == "Next" || selectedOption.text == "Back") {
                    document.getElementById("<%= SlideIndexSlider.ContainerID %>").style.display = "none";
                }
                if (selectedOption.text == "Custom") {
                    document.getElementById("<%= SlideIndexSlider.ContainerID %>").style.display = "block";
                }
            }

            function SetSlideBackgroundType() {
                var element = document.getElementById("<%= SlideBackgroundTypeDropDownList.DropDownList.ClientID %>");
                var selectedOption = element.options[element.selectedIndex];
                if (selectedOption.text == "Image") {
                    document.getElementById("<%= SlideBackgroundColorTextBox.ContainerID %>").style.display = "none";
                    document.getElementById("<%= SlideBackgroundImageFileUpload.ContainerID %>").style.display = "block";
                    document.getElementById("<%= SlideBackgroundThumbImageFileUpload.ContainerID %>").style.display = "block";
                    document.getElementById("<%= SlideBackgroundLazyLoadImageFileUpload.ContainerID %>").style.display = "block";
                }
                if (selectedOption.text == "Solid Color") {
                    document.getElementById("<%= SlideBackgroundColorTextBox.ContainerID %>").style.display = "block";
                    document.getElementById("<%= SlideBackgroundImageFileUpload.ContainerID %>").style.display = "none";
                    document.getElementById("<%= SlideBackgroundThumbImageFileUpload.ContainerID %>").style.display = "none";
                    document.getElementById("<%= SlideBackgroundLazyLoadImageFileUpload.ContainerID %>").style.display = "none";
                }
            }
        }

        $(document).ready(function () {

            SlideTypeChange();

            BGFitTypeChange();

            SlideIndexTypeChange();

            CustomIn();

            CustomOut();

            DataXChange();

            DataYChange();

            CaptionTypeChange();

        });

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

        function SlideTypeChange() {
            var element = document.getElementById("<%= SlideTypeDropDownList.DropDownList.ClientID %>");
            var selectedOption = element.options[element.selectedIndex];
            if (selectedOption.text == "Simple") {
                document.getElementById("KenBurnSlidePropertiesPanel").style.display = "none";
                document.getElementById("SimpleSlidePropertiesPanel").style.display = "block";
                document.getElementById("<%= SlideBackgroundTypeDropDownList.ContainerID %>").style.display = "block";
                SetSlideBackgroundType();
            }
            if (selectedOption.text == "KenBurns") {
                document.getElementById("KenBurnSlidePropertiesPanel").style.display = "block";
                document.getElementById("SimpleSlidePropertiesPanel").style.display = "none";
                document.getElementById("<%= SlideBackgroundTypeDropDownList.ContainerID %>").style.display = "none";
                document.getElementById("<%= SlideBackgroundColorTextBox.ContainerID %>").style.display = "none";
                document.getElementById("<%= SlideBackgroundImageFileUpload.ContainerID %>").style.display = "block";
                document.getElementById("<%= SlideBackgroundThumbImageFileUpload.ContainerID %>").style.display = "block";
                document.getElementById("<%= SlideBackgroundLazyLoadImageFileUpload.ContainerID %>").style.display = "block";
            }
        }

        function BGFitTypeChange() {
            var element = document.getElementById("<%= BGFitDropDownList.DropDownList.ClientID %>");
            var selectedOption = element.options[element.selectedIndex];
            if (selectedOption.text == "Cover" || selectedOption.text == "Contain" || selectedOption.text == "Normal") {
                document.getElementById("<%= BGFitWidthSlider.ContainerID %>").style.display = "none";
                document.getElementById("<%= BGFitHeightSlider.ContainerID %>").style.display = "none";
            }
            if (selectedOption.text == "Custom") {
                document.getElementById("<%= BGFitWidthSlider.ContainerID %>").style.display = "block";
                document.getElementById("<%= BGFitHeightSlider.ContainerID %>").style.display = "block";
            }
        }

        function SlideIndexTypeChange() {
            var element = document.getElementById("<%= SlideIndexDropDownList.DropDownList.ClientID %>");
            var selectedOption = element.options[element.selectedIndex];
            if (selectedOption.text == "None") {
                document.getElementById("<%= SlideIndexSlider.ContainerID %>").style.display = "none";
            }
            if (selectedOption.text == "Next" || selectedOption.text == "Back") {
                document.getElementById("<%= SlideIndexSlider.ContainerID %>").style.display = "none";
            }
            if (selectedOption.text == "Custom") {
                document.getElementById("<%= SlideIndexSlider.ContainerID %>").style.display = "block";
            }
        }

        function SetSlideBackgroundType() {
            var element = document.getElementById("<%= SlideBackgroundTypeDropDownList.DropDownList.ClientID %>");
            var selectedOption = element.options[element.selectedIndex];
            if (selectedOption.text == "Image") {
                document.getElementById("<%= SlideBackgroundColorTextBox.ContainerID %>").style.display = "none";
                document.getElementById("<%= SlideBackgroundImageFileUpload.ContainerID %>").style.display = "block";
                document.getElementById("<%= SlideBackgroundThumbImageFileUpload.ContainerID %>").style.display = "block";
                document.getElementById("<%= SlideBackgroundLazyLoadImageFileUpload.ContainerID %>").style.display = "block";
            }
            if (selectedOption.text == "Solid Color") {
                document.getElementById("<%= SlideBackgroundColorTextBox.ContainerID %>").style.display = "block";
                document.getElementById("<%= SlideBackgroundImageFileUpload.ContainerID %>").style.display = "none";
                document.getElementById("<%= SlideBackgroundThumbImageFileUpload.ContainerID %>").style.display = "none";
                document.getElementById("<%= SlideBackgroundLazyLoadImageFileUpload.ContainerID %>").style.display = "none";
            }
        }
    </script>
</asp:content>
