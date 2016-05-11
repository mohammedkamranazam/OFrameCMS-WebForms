<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageUploaderComponent.ascx.cs" Inherits="OWDARO.UI.UserControls.Components.Utility.ImageUploaderComponent" %>

<div class="onecolumn">
    <div class="header">
        <span><span class="ico color window"></span>Edit Image</span>
    </div>
    <div class="Clear">
    </div>
    <div class="content">
        <div class="ImageUploaderComponent">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="DisplayedImage" runat="server" id="ThumbnailDisplayDiv">
                        <asp:Literal runat="server" ID="FancyBoxLiteral"></asp:Literal>
                    </div>
                    <OWD:TextBoxAdv runat="server" ID="TitleTextBox" LabelText="Title" MaxLength="100" />
                    <OWD:CheckBoxAdv runat="server" ID="ShowWebImageCheckBox" LabelText="Show Web Image" SmallLabelText="check this if you want to use web image instead of local image"
                        AutoPostBack="true" />
                    <OWD:TextBoxAdv runat="server" ID="WebImageURLTextBox" LabelText="Web Image URL" SmallLabelText="URL for web image" Visible="false" RequiredErrorMessage="image url is required"
                        ValidationGroup="ImageUploadGroup" />
                    <OWD:TextBoxAdv runat="server" ID="WebImageThumbURLTextBox" LabelText="Web Image Thumb URL" SmallLabelText="URL for web image thumbnail" Visible="false" WatermarkText="Optional"
                        ValidationGroup="ImageUploadGroup" />
                    <OWD:FileUploadAdv ID="ImageURLFileUpload" runat="server" LabelText="Cover Image" SmallLabelText="cover image size should be less than 10MB" MaxFileSizeMB="10" ValidationGroup="ImageUploadGroup" />
                    <OWD:Slider runat="server" ID="ImageQualitySlider" Maximum="100" Minimum="0" Value="70" Length="200" LabelText="Quality" />
                    <OWD:StatusMessageJQuery runat="server" ID="StatusMessage" />
                    <span>
                        <asp:HiddenField runat="server" ID="ImageIDHiddenField" Value="0" />
                        <asp:HiddenField runat="server" ID="UploadedImageWidthHiddenField" />
                        <asp:HiddenField runat="server" ID="UploadedImageHeightHiddenField" />
                        <asp:HiddenField runat="server" ID="StoragePathHiddenField" Value="~/Storage/Resources/Others/" />
                    </span>
                </ContentTemplate>
            </asp:UpdatePanel>
            <OWD:FormToolbar runat="server" ID="FormToolbar1" ShowCustom="true" ShowSave="true" SaveButtonText="Replace Current" CustomButtonText="Upload New"
                SaveButtonCssClass="btn btn-inverse" CustomButtonCssClass="btn btn-primary" ValidationGroup="ImageUploadGroup" />
        </div>
    </div>
</div>

<div runat="server" id="EditorLaunchToolbar">
    <div class="onecolumn">
        <div class="header">
            <span><span class="ico color window"></span>Edit Image</span>
        </div>
        <div class="Clear">
        </div>
        <div class="content">
            <div class="ImageUploaderComponent">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="grid4">
                            <div class="BigImageDisplayPanel" runat="server" id="BigImageDisplayPanel">
                                <asp:Image runat="server" ID="UploadedBigImageEdit" ClientIDMode="Static" CssClass="EditImage" />
                            </div>
                            <OWD:CheckBoxAdv runat="server" ID="BigImageAspectRatioDisableCheckBox" OnClick="BigImageEnableOrDisableAspectRatio(this)" LabelText="Free Crop" />
                        </div>

                        <div class="grid2">
                            <fieldset>
                                <legend>Big Image
                                </legend>
                                <OWD:LabelAdv runat="server" ID="ImageFocusPointHelperLinkLabel" LabelText="Focus Point Helper Tool Link" />
                                <OWD:TextBoxAdv runat="server" ID="FocusPointXTextBox" LabelText="Focus Point X" FilterMode="ValidChars" ValidChars="0123456789.-" RangeType="Double" MaxValue="1"
                                    MinValue="-1" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:TextBoxAdv runat="server" ID="FocusPointYTextBox" LabelText="Focus Point Y" FilterMode="ValidChars" ValidChars="0123456789.-" RangeType="Double" MaxValue="1"
                                    MinValue="-1" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:TextBoxAdv runat="server" ID="EditWidthTextBox" LabelText="Width" FilterMode="ValidChars" FilterType="Numbers" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:TextBoxAdv runat="server" ID="EditHeightTextBox" LabelText="Height" FilterMode="ValidChars" FilterType="Numbers" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:TextBoxAdv runat="server" ID="EditMaxWidthTextBox" LabelText="Max Width" FilterMode="ValidChars" FilterType="Numbers" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:TextBoxAdv runat="server" ID="EditMaxHeightTextBox" LabelText="Max Height" FilterMode="ValidChars" FilterType="Numbers" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:Slider runat="server" ID="EditQualitySlider" LabelText="Quality" Maximum="100" Minimum="0" Value="70" Length="200" />
                                <span>
                                    <asp:HiddenField runat="server" ID="BigImageCropAspectRatioHiddenField" ClientIDMode="Static" Value="0" />
                                    <asp:HiddenField runat="server" ID="BigImageOriginalAspectRatioHiddenField" ClientIDMode="Static" Value="0" />
                                    <asp:HiddenField runat="server" ID="BigImageCropWidth" ClientIDMode="Static" Value="0" />
                                    <asp:HiddenField runat="server" ID="BigImageCropHeight" ClientIDMode="Static" Value="0" />
                                    <asp:HiddenField runat="server" ID="BigImageCropX1" ClientIDMode="Static" Value="0" />
                                    <asp:HiddenField runat="server" ID="BigImageCropX2" ClientIDMode="Static" Value="0" />
                                    <asp:HiddenField runat="server" ID="BigImageCropY1" ClientIDMode="Static" Value="0" />
                                    <asp:HiddenField runat="server" ID="BigImageCropY2" ClientIDMode="Static" Value="0" />
                                    <asp:HiddenField runat="server" ID="BigImageCropXUnit" ClientIDMode="Static" Value="0" />
                                    <asp:HiddenField runat="server" ID="BigImageCropYUnit" ClientIDMode="Static" Value="0" />
                                </span>
                            </fieldset>
                        </div>

                        <%-- <div class="grid4">
                            <div class="ThumbImageDisplayPanel" runat="server" id="ThumbImageDisplayPanel">
                                <asp:Image runat="server" ID="UploadedThumbImageEdit" ClientIDMode="Static" CssClass="EditImage" />
                            </div>
                            <OWD:CheckBoxAdv runat="server" ID="ThumbImageAspectRatioDisableCheckBox" LabelText="Free Crop" />
                        </div>--%>

                        <div class="grid2">
                            <fieldset>
                                <legend>Thumbnail Image
                                </legend>
                                <OWD:TextBoxAdv runat="server" ID="EditThumbWidthTextBox" LabelText="Width" FilterMode="ValidChars" FilterType="Numbers" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:TextBoxAdv runat="server" ID="EditThumbHeightTextBox" LabelText="Height" FilterMode="ValidChars" FilterType="Numbers" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:TextBoxAdv runat="server" ID="EditThumbMaxWidthTextBox" LabelText="Max Width" FilterMode="ValidChars" FilterType="Numbers" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:TextBoxAdv runat="server" ID="EditThumbMaxHeightTextBox" LabelText="Max Height" FilterMode="ValidChars" FilterType="Numbers" ValidationGroup="ResizeImageValidationGroup" />
                                <OWD:Slider runat="server" ID="EditThumbQualitySlider" LabelText="Quality" Maximum="100" Minimum="0" Value="70" Length="200" />
                                <%--<span>
                                <asp:HiddenField runat="server" ID="ThumbImageCropAspectRatioHiddenField" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField runat="server" ID="ThumbImageOriginalAspectRatioHiddenField" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField runat="server" ID="ThumbImageCropWidth" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField runat="server" ID="ThumbImageCropHeight" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField runat="server" ID="ThumbImageCropX1" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField runat="server" ID="ThumbImageCropX2" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField runat="server" ID="ThumbImageCropY1" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField runat="server" ID="ThumbImageCropY2" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField runat="server" ID="ThumbImageCropXUnit" ClientIDMode="Static" Value="0" />
                                <asp:HiddenField runat="server" ID="ThumbImageCropYUnit" ClientIDMode="Static" Value="0" />
                            </span>--%>
                            </fieldset>
                        </div>
                        <div class="Clear"></div>
                        <OWD:FormToolbar runat="server" ID="SaveToolbar" ShowSave="true" ShowCustom="true" ShowUpdate="true" CenterButtons="true"
                            SaveButtonText="Save Crop" SaveButtonCssClass="btn btn-primary"
                            UpdateButtonText="Update Dimensions" UpdateButtonCssClass="btn btn-inverse"
                            ShowCustomPopupButton="true" CustomPopupButtonText="Reset" CustomPopupButtonCssClass="btn btn-danger"
                            CustomButtonText="Update Focus Point" CustomButtonCssClass="btn btn-info"
                            ValidationGroup="ResizeImageValidationGroup" />
                        <OWD:StatusMessageJQuery runat="server" ID="StatusMessage2" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</div>

<style>
    .ImageUploaderComponent {
    }

        .ImageUploaderComponent .DisplayedImage {
            margin: 10px auto;
            border: 1px rgba(0,0,0,.4) solid;
            background-color: white;
            -webkit-border-radius: 6px;
            -moz-border-radius: 6px;
            border-radius: 6px;
            -webkit-box-shadow: 1px 1px 5px 2px rgba(0, 0, 0, 0.2);
            -moz-box-shadow: 1px 1px 5px 2px rgba(0, 0, 0, 0.2);
            box-shadow: 1px 1px 5px 2px rgba(0, 0, 0, 0.2);
            max-width: 300px;
            display: table;
            padding: 5px;
        }

        .ImageUploaderComponent .BigImageDisplayPanel {
            max-width: 50px;
            /*display: table;*/
            margin: 10px auto;
            border: 1px dotted red;
        }
</style>

<script type="text/javascript">

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function (s, e) {
        ReInitialize();
        BigImageReCropSet();
    });

    function OnImageWebLocalToggleChanged() {
        var element = document.getElementById("<%= ShowWebImageCheckBox.CheckBox.ClientID  %>");

        if (element.checked) {
            document.getElementById("<%= EditorLaunchToolbar.ClientID %>").style.display = "none";
        }
        else {
            document.getElementById("<%= EditorLaunchToolbar.ClientID %>").style.display = "block";
        }
    }

    function BigImageEnableOrDisableAspectRatio(element) {

        if (element.checked) {
            document.getElementById("BigImageCropAspectRatioHiddenField").value = 0;

            BigImageCropScript();
        }
        else {
            document.getElementById("BigImageCropAspectRatioHiddenField").value = document.getElementById("BigImageOriginalAspectRatioHiddenField").value;

            BigImageCropScript();
        }
    };

    function BigImageUpdateCoOrds(c) {
        $('#BigImageCropX1').val(c.x);
        $('#BigImageCropY1').val(c.y);
        $('#BigImageCropX2').val(c.x2);
        $('#BigImageCropY2').val(c.y2);
        $('#BigImageCropWidth').val(c.w);
        $('#BigImageCropHeight').val(c.h);
    };

    $(document).ready(function () {
        BigImageCropScript();
    });

    function BigImageCropScript() {
        var jcrop_apiBigImage;

        var aspect = parseFloat(document.getElementById("BigImageCropAspectRatioHiddenField").value);

        $('#UploadedBigImageEdit').Jcrop({
            onChange: BigImageUpdateCoOrds,
            onSelect: BigImageUpdateCoOrds,
            aspectRatio: aspect
        }, function () {
            jcrop_apiBigImage = this;
        });
    };

    function BigImageReCropSet() {
        function OnImageWebLocalToggleChanged() {
            var element = document.getElementById("<%= ShowWebImageCheckBox.CheckBox.ClientID  %>");

            if (element.checked) {
                document.getElementById("<%= EditorLaunchToolbar.ClientID %>").style.display = "none";
            }
            else {
                document.getElementById("<%= EditorLaunchToolbar.ClientID %>").style.display = "block";
            }
        }

        function BigImageEnableOrDisableAspectRatio(element) {

            if (element.checked) {
                document.getElementById("BigImageCropAspectRatioHiddenField").value = 0;

                BigImageCropScript();
            }
            else {
                document.getElementById("BigImageCropAspectRatioHiddenField").value = document.getElementById("BigImageOriginalAspectRatioHiddenField").value;

                BigImageCropScript();
            }
        };

        function BigImageUpdateCoOrds(c) {
            $('#BigImageCropX1').val(c.x);
            $('#BigImageCropY1').val(c.y);
            $('#BigImageCropX2').val(c.x2);
            $('#BigImageCropY2').val(c.y2);
            $('#BigImageCropWidth').val(c.w);
            $('#BigImageCropHeight').val(c.h);
        };

        $(document).ready(function () {
            BigImageCropScript();
        });

        function BigImageCropScript() {
            var jcrop_apiBigImage;

            var aspect = parseFloat(document.getElementById("BigImageCropAspectRatioHiddenField").value);

            $('#UploadedBigImageEdit').Jcrop({
                onChange: BigImageUpdateCoOrds,
                onSelect: BigImageUpdateCoOrds,
                aspectRatio: aspect
            }, function () {
                jcrop_apiBigImage = this;
            });
        };
    };
</script>