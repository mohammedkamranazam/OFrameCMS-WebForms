using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class SlideManage : Page
    {
        private void AddLayerProperties(GalleryEntities context, long layerID)
        {
            if (CaptionContentTypeDropDownList.SelectedValue == "Video")
            {
                LayerPropertiesBL.Add(layerID, "data-autoplay", VideoAutoPlayCheckbox.Checked.ToString().ToLower(), context);
                LayerPropertiesBL.Add(layerID, "data-autoplayonlyfirsttime", VideoAutoPlayOnFirstTimeCheckBox.Checked.ToString().ToLower(), context);
                LayerPropertiesBL.Add(layerID, "data-nextslideatend", VideoNextSlideAtEndCheckBox.Checked.ToString().ToLower(), context);
            }
            else
            {
                if (SplitTypeInDropDownlist.SelectedValue == string.Empty)
                {
                    //LayerPropertiesBL.Delete(layerID, "data-splitin", context);
                    //LayerPropertiesBL.Delete(layerID, "data-elementdelay", context);

                    //LayerPropertiesBL.Delete(layerID, "data-splitout", context);
                    //LayerPropertiesBL.Delete(layerID, "data-endelementdelay", context);
                }
                else
                {
                    LayerPropertiesBL.Add(layerID, "data-splitin", SplitTypeInDropDownlist.SelectedValue, context);
                    LayerPropertiesBL.Add(layerID, "data-elementdelay", (ElementInDelaySlider.Value / 1000).ToString(), context);

                    LayerPropertiesBL.Add(layerID, "data-splitout", SplitTypeOutDropDownlist.SelectedValue, context);
                    LayerPropertiesBL.Add(layerID, "data-endelementdelay", (ElementOutDelaySlider.Value / 1000).ToString(), context);
                }
            }

            var cssClass = "tp-caption {0} {1} {2} {3}";
            cssClass = string.Format(cssClass, AnimateInClassDropDownList.SelectedValue, AnimateOutClassDropDownList.SelectedValue,
                ((StylingClassDropDownList.SelectedValue == "-1") ? string.Empty : StylingClassDropDownList.SelectedValue),
                ((CaptionContentTypeDropDownList.SelectedValue == "Video" && IsCaptionVideoFullScreenCheckBox.Checked) ? "fullscreenvideo" : string.Empty));

            LayerPropertiesBL.Add(layerID, "class", cssClass, context);

            if (DataXDropDownList.SelectedValue == "custom")
            {
                LayerPropertiesBL.Add(layerID, "data-x", DataXSlider.Value.ToString(), context);
            }
            else
            {
                LayerPropertiesBL.Add(layerID, "data-x", DataXDropDownList.SelectedValue, context);
            }

            if (DataYDropDownList.SelectedValue == "custom")
            {
                LayerPropertiesBL.Add(layerID, "data-y", DataYSlider.Value.ToString(), context);
            }
            else
            {
                LayerPropertiesBL.Add(layerID, "data-y", DataYDropDownList.SelectedValue, context);
            }

            if (DataXDropDownList.SelectedValue != "custom")
            {
                LayerPropertiesBL.Add(layerID, "data-hoffset", DataHOffsetSlider.Value.ToString(), context);
            }

            if (DataYDropDownList.SelectedValue != "custom")
            {
                LayerPropertiesBL.Add(layerID, "data-voffset", DataVOffsetSlider.Value.ToString(), context);
            }

            LayerPropertiesBL.Add(layerID, "data-start", DataStartSlider.Value.ToString(), context);
            LayerPropertiesBL.Add(layerID, "data-speed", DataSpeedSlider.Value.ToString(), context);
            LayerPropertiesBL.Add(layerID, "data-easing", DataStartEasingDropDownList.SelectedValue, context);
            LayerPropertiesBL.Add(layerID, "data-end", DataEndSlider.Value.ToString(), context);
            LayerPropertiesBL.Add(layerID, "data-endspeed", DataSpeedEndSlider.Value.ToString(), context);
            LayerPropertiesBL.Add(layerID, "data-endeasing", DataEndEasingDropDownList.SelectedValue, context);

            if (AnimateInClassDropDownList.SelectedValue == "customin")
            {
                var customAnimation = string.Format("x:{0};y:{1};z:{2};rotationX:{3};rotationY:{4};rotationZ:{5};scaleX:{6};scaleY:{7};skewX:{8};skewY:{9};opacity:{10};transformPerspective:{11};transformOrigin:{12}% {13}%;",
                    InTransitionXSlider.Value, InTransitionYSlider.Value, InTransitionZSlider.Value, InRotationXSlider.Value, InRotationYSlider.Value, InRotationZSlider.Value,
                    InScaleXSlider.Value, InScaleYSlider.Value, InSkewXSlider.Value, InSkewYSlider.Value, InOpacitySlider.Value, InPerspectiveSlider.Value,
                    InOriginXSlider.Value, InOriginYSlider.Value);

                LayerPropertiesBL.Add(layerID, "data-customin", customAnimation, context);
            }

            if (AnimateOutClassDropDownList.SelectedValue == "customout")
            {
                var customAnimation = string.Format("x:{0};y:{1};z:{2};rotationX:{3};rotationY:{4};rotationZ:{5};scaleX:{6};scaleY:{7};skewX:{8};skewY:{9};opacity:{10};transformPerspective:{11};transformOrigin:{12}% {13}%;",
                    OutTransitionXSlider.Value, OutTransitionYSlider.Value, OutTransitionZSlider.Value, OutRotationXSlider.Value, OutRotationYSlider.Value, OutRotationZSlider.Value,
                    OutScaleXSlider.Value, OutScaleYSlider.Value, OutSkewXSlider.Value, OutSkewYSlider.Value, OutOpacitySlider.Value, OutPerspectiveSlider.Value,
                    OutOriginXSlider.Value, OutOriginYSlider.Value);

                LayerPropertiesBL.Add(layerID, "data-customout", customAnimation, context);
            }

            LayerPropertiesBL.Add(layerID, "style", LayerStyleAttributeTextBox.Text, context);

            if (CaptionHiddenOnCheckBox.Checked)
            {
                LayerPropertiesBL.Add(layerID, "data-captionhidden", "on", context);
            }

            context.SaveChanges();
        }

        private void CreateCommon(GY_Slides slideEntity, GalleryEntities context)
        {
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-delay", DelaySlider.Value.ToString(), false, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "owd-slideType", SlideTypeDropDownList.SelectedValue, false, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-masterspeed", MasterSpeedSlider.Value.ToString(), false, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-transition", TransitionDropDownList.SelectedValue, false, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-titile", slideEntity.Title, false, context);

            if (!string.IsNullOrWhiteSpace(LinkTextBox.Text))
            {
                SlidePropertiesBL.Add(slideEntity.SlideID, "data-link", LinkTextBox.Text, false, context);
                SlidePropertiesBL.Add(slideEntity.SlideID, "data-target", TargetDropDownList.SelectedValue, false, context);
            }
            else
            {
                SlidePropertiesBL.Add(slideEntity.SlideID, "data-link", string.Empty, false, context);
                SlidePropertiesBL.Add(slideEntity.SlideID, "data-target", string.Empty, false, context);
            }

            switch (SlideIndexDropDownList.SelectedValue)
            {
                case "none":
                    SlidePropertiesBL.Add(slideEntity.SlideID, "data-slideindex", string.Empty, false, context);
                    break;

                case "next":
                case "back":
                    SlidePropertiesBL.Add(slideEntity.SlideID, "data-slideindex", SlideIndexDropDownList.SelectedValue, false, context);
                    SlidePropertiesBL.Add(slideEntity.SlideID, "data-link", string.Empty, false, context);
                    SlidePropertiesBL.Add(slideEntity.SlideID, "data-target", string.Empty, false, context);
                    break;

                case "custom":
                    SlidePropertiesBL.Add(slideEntity.SlideID, "data-slideindex", SlideIndexSlider.Value.ToString(), false, context);
                    SlidePropertiesBL.Add(slideEntity.SlideID, "data-link", string.Empty, false, context);
                    SlidePropertiesBL.Add(slideEntity.SlideID, "data-target", string.Empty, false, context);
                    break;
            }
        }

        private void CreateKenBurnsSlide(GY_Slides slideEntity, GalleryEntities context)
        {
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-kenburns", "on", true, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-bgposition", KenBurnBGPositionDropDownList.SelectedValue, true, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-bgpositionend", KenBurnBGPositionEndDropDownList.SelectedValue, true, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-duration", DurationSlider.Value.ToString(), true, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-ease", EaseDropDownList.SelectedValue, true, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-bgfit", BGFitSlider.Value.ToString(), true, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-bgfitend", BGFitEndSlider.Value.ToString(), true, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "style", string.Empty, true, context);
        }

        private void CreateSimpleSlide(GY_Slides slideEntity, GalleryEntities context)
        {
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-kenburns", "off", true, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-slotamount", SlotAmountSlider.Value.ToString(), false, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-bgrepeat", BGRepeatDropDownList.SelectedValue, true, context);
            SlidePropertiesBL.Add(slideEntity.SlideID, "data-bgposition", BGPositionDropDownList.SelectedValue, true, context);

            switch (BGFitDropDownList.SelectedValue)
            {
                case "cover":
                case "contain":
                case "normal":
                    SlidePropertiesBL.Add(slideEntity.SlideID, "data-bgfit", BGFitDropDownList.SelectedValue, true, context);
                    break;

                case "custom":
                    var value = string.Format("{0}% {1}%", BGFitWidthSlider.Value, BGFitHeightSlider.Value);
                    SlidePropertiesBL.Add(slideEntity.SlideID, "data-bgfit", value, true, context);
                    break;
            }

            if (SlideBackgroundTypeDropDownList.SelectedValue == "Solid Color")
            {
                SlidePropertiesBL.Add(slideEntity.SlideID, "style", string.Format("background-color: {0};", SlideBackgroundColorTextBox.Text), true, context);
            }
            else
            {
                SlidePropertiesBL.Add(slideEntity.SlideID, "style", string.Empty, true, context);
            }
        }

        private void FormToolbar1_Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Pages/Admin/Gallery/SlideList.aspx");
        }

        private void FormToolbar1_Delete(object sender, EventArgs e)
        {
        }

        private void FormToolbar1_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (var context = new GalleryEntities())
                {
                    var slideID = Request.QueryString["SlideID"].IntParse();

                    var slideEntity = SlidesBL.GetObjectByID(slideID, context);

                    if (slideEntity != null)
                    {
                        if (slideEntity.Title != TitleTextBox.Text)
                        {
                            if (SlidesBL.Exists(TitleTextBox.Text, context))
                            {
                                StatusMessage.MessageType = StatusMessageType.Info;
                                StatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                                return;
                            }
                        }

                        slideEntity.Title = TitleTextBox.Text;
                        slideEntity.Hide = HideCheckbox.Checked;

                        if (string.IsNullOrWhiteSpace(PositionTextBox.Text))
                        {
                            slideEntity.Position = SlidesBL.GetPosition(context);
                        }
                        else
                        {
                            slideEntity.Position = PositionTextBox.Text.IntParse();
                        }

                        try
                        {
                            SlidesBL.Save(slideEntity, context);

                            StatusMessage.MessageType = StatusMessageType.Success;
                            StatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                        }
                        catch (Exception ex)
                        {
                            ErrorLogger.LogError(ex);
                            StatusMessage.MessageType = StatusMessageType.Error;
                            StatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        }
                    }
                }
            }
        }

        private string GetColorValue(string text)
        {
            text = text.Replace("background-color: ", string.Empty);
            text = text.Replace(";", string.Empty);

            return text;
        }

        private double GetFirstValue(string text)
        {
            var firstIndex = text.IndexOf("%");

            text = text.Substring(0, firstIndex);

            return text.DoubleParse();
        }

        private double GetLastValue(string text)
        {
            var firstIndex = text.IndexOf("%");

            text = text.Remove(0, firstIndex + 1);

            return GetFirstValue(text);
        }

        private void InitializeUI()
        {
            SlideTypeDropDownList.Items.Add("Simple");
            SlideTypeDropDownList.Items.Add("KenBurns");

            SlideBackgroundTypeDropDownList.Items.Add("Image");
            SlideBackgroundTypeDropDownList.Items.Add("Solid Color");

            SlideIndexDropDownList.Items.Add(new ListItem("None", "none"));
            SlideIndexDropDownList.Items.Add(new ListItem("Next", "next"));
            SlideIndexDropDownList.Items.Add(new ListItem("Back", "back"));
            SlideIndexDropDownList.Items.Add(new ListItem("Custom", "custom"));

            TargetDropDownList.Items.Add(new ListItem("New Tab/Window", "_blank"));
            TargetDropDownList.Items.Add(new ListItem("Same Tab/Window", "_self"));

            BGRepeatDropDownList.Items.Add(new ListItem("No Repeat", "no-repeat"));
            BGRepeatDropDownList.Items.Add(new ListItem("Repeat", "repeat"));
            BGRepeatDropDownList.Items.Add(new ListItem("Repeat Horizontally", "repeat-x"));
            BGRepeatDropDownList.Items.Add(new ListItem("Repeat Vertically", "repeat-y"));

            BGFitDropDownList.Items.Add(new ListItem("Custom", "custom"));
            BGFitDropDownList.Items.Add(new ListItem("Normal", "normal"));
            BGFitDropDownList.Items.Add(new ListItem("Contain", "contain"));
            BGFitDropDownList.Items.Add(new ListItem("Cover", "cover"));

            BGPositionDropDownList.Items.Add("center center");
            BGPositionDropDownList.Items.Add("left top");
            BGPositionDropDownList.Items.Add("left center");
            BGPositionDropDownList.Items.Add("left bottom");
            BGPositionDropDownList.Items.Add("center top");
            BGPositionDropDownList.Items.Add("center bottom");
            BGPositionDropDownList.Items.Add("right top");
            BGPositionDropDownList.Items.Add("right center");
            BGPositionDropDownList.Items.Add("right bottom");

            TransitionDropDownList.Items.Add(new ListItem("Fade", "fade"));
            TransitionDropDownList.Items.Add(new ListItem("Slide To Top", "slideup"));
            TransitionDropDownList.Items.Add(new ListItem("Slide To Bottom", "slidedown"));
            TransitionDropDownList.Items.Add(new ListItem("Slide To Right", "slideright"));
            TransitionDropDownList.Items.Add(new ListItem("Slide To Left", "slideleft"));
            TransitionDropDownList.Items.Add(new ListItem("Slide Horizontal (depending on Next/Previous)", "slidehorizontal"));
            TransitionDropDownList.Items.Add(new ListItem("Slide Vertical (depending on Next/Previous)", "slidevertical"));
            TransitionDropDownList.Items.Add(new ListItem("Slide Boxes", "boxslide"));
            TransitionDropDownList.Items.Add(new ListItem("Slide Slots Horizontal", "slotslide-horizontal"));
            TransitionDropDownList.Items.Add(new ListItem("Slide Slots Vertical", "slotslide-vertical"));
            TransitionDropDownList.Items.Add(new ListItem("Fade Boxes", "boxfade"));
            TransitionDropDownList.Items.Add(new ListItem("Fade Slots Horizontal", "slotfade-horizontal"));
            TransitionDropDownList.Items.Add(new ListItem("Fade Slots Vertical", "slotfade-vertical"));
            TransitionDropDownList.Items.Add(new ListItem("Fade and Slide from Right", "fadefromright"));
            TransitionDropDownList.Items.Add(new ListItem("Fade and Slide from Left", "fadefromleft"));
            TransitionDropDownList.Items.Add(new ListItem("Fade and Slide from Top", "fadefromtop"));
            TransitionDropDownList.Items.Add(new ListItem("Fade and Slide from Bottom", "fadefrombottom"));
            TransitionDropDownList.Items.Add(new ListItem("Fade To Left and Fade From Right", "fadetoleftfadefromright"));
            TransitionDropDownList.Items.Add(new ListItem("Fade To Right and Fade From Left", "fadetorightfadefromleft"));
            TransitionDropDownList.Items.Add(new ListItem("Fade To Top and Fade From Bottom", "fadetotopfadefrombottom"));
            TransitionDropDownList.Items.Add(new ListItem("Fade To Bottom and Fade From Top", "fadetobottomfadefromtop"));
            TransitionDropDownList.Items.Add(new ListItem("Parallax to Right", "parallaxtoright"));
            TransitionDropDownList.Items.Add(new ListItem("Parallax to Left", "parallaxtoleft"));
            TransitionDropDownList.Items.Add(new ListItem("Parallax to Top", "parallaxtotop"));
            TransitionDropDownList.Items.Add(new ListItem("Parallax to Bottom", "parallaxtobottom"));
            TransitionDropDownList.Items.Add(new ListItem("Parallax Vertical", "parallaxvertical"));
            TransitionDropDownList.Items.Add(new ListItem("Parallax Horizontal", "parallaxhorizontal"));
            TransitionDropDownList.Items.Add(new ListItem("Zoom Out and Fade From Right", "scaledownfromright"));
            TransitionDropDownList.Items.Add(new ListItem("Zoom Out and Fade From Left", "scaledownfromleft"));
            TransitionDropDownList.Items.Add(new ListItem("Zoom Out and Fade From Top", "scaledownfromtop"));
            TransitionDropDownList.Items.Add(new ListItem("Zoom Out and Fade From Bottom", "scaledownfrombottom"));
            TransitionDropDownList.Items.Add(new ListItem("ZoomOut", "zoomout"));
            TransitionDropDownList.Items.Add(new ListItem("ZoomIn", "zoomin"));
            TransitionDropDownList.Items.Add(new ListItem("Zoom Slots Horizontal", "slotzoom-horizontal"));
            TransitionDropDownList.Items.Add(new ListItem("Zoom Slots Vertical", "slotzoom-vertical"));
            TransitionDropDownList.Items.Add(new ListItem("Curtain from Left", "curtain-1"));
            TransitionDropDownList.Items.Add(new ListItem("Curtain from Right", "curtain-2"));
            TransitionDropDownList.Items.Add(new ListItem("Curtain from Middle", "curtain-3"));
            TransitionDropDownList.Items.Add(new ListItem("3D Curtain Horizontal", "3dcurtain-horizontal"));
            TransitionDropDownList.Items.Add(new ListItem("3D Curtain Vertical", "3dcurtain-vertical"));
            TransitionDropDownList.Items.Add(new ListItem("Cube Vertical", "cube"));
            TransitionDropDownList.Items.Add(new ListItem("Cube Horizontal", "cube-horizontal"));
            TransitionDropDownList.Items.Add(new ListItem("In Cube Vertical", "incube"));
            TransitionDropDownList.Items.Add(new ListItem("In Cube Horizontal", "incube-horizontal"));
            TransitionDropDownList.Items.Add(new ListItem("TurnOff Horizontal", "turnoff"));
            TransitionDropDownList.Items.Add(new ListItem("TurnOff Vertical", "turnoff-vertical"));
            TransitionDropDownList.Items.Add(new ListItem("Paper Cut", "papercut"));
            TransitionDropDownList.Items.Add(new ListItem("Fly In", "flyin"));
            TransitionDropDownList.Items.Add(new ListItem("Random Flat", "random-static"));
            TransitionDropDownList.Items.Add(new ListItem("Random Premium", "random-premium"));
            TransitionDropDownList.Items.Add(new ListItem("Random Flat and Premium", "random"));

            SlidesBL.PopulateEasing(EaseDropDownList);

            KenBurnBGPositionDropDownList.Items.Add("center center");
            KenBurnBGPositionDropDownList.Items.Add("left top");
            KenBurnBGPositionDropDownList.Items.Add("left center");
            KenBurnBGPositionDropDownList.Items.Add("left bottom");
            KenBurnBGPositionDropDownList.Items.Add("center top");
            KenBurnBGPositionDropDownList.Items.Add("center bottom");
            KenBurnBGPositionDropDownList.Items.Add("right top");
            KenBurnBGPositionDropDownList.Items.Add("right center");
            KenBurnBGPositionDropDownList.Items.Add("right bottom");

            KenBurnBGPositionEndDropDownList.Items.Add("center center");
            KenBurnBGPositionEndDropDownList.Items.Add("left top");
            KenBurnBGPositionEndDropDownList.Items.Add("left center");
            KenBurnBGPositionEndDropDownList.Items.Add("left bottom");
            KenBurnBGPositionEndDropDownList.Items.Add("center top");
            KenBurnBGPositionEndDropDownList.Items.Add("center bottom");
            KenBurnBGPositionEndDropDownList.Items.Add("right top");
            KenBurnBGPositionEndDropDownList.Items.Add("right center");
            KenBurnBGPositionEndDropDownList.Items.Add("right bottom");

            AnimateInClassDropDownList.Items.Add(new ListItem("Short from Top", "sft"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Short from Bottom", "sfb"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Short from Right", "sfr"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Short from Left", "sfl"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Long from Top", "lft"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Long from Bottom", "lfb"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Long from Right", "lfr"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Long from Left", "lfl"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Skew from Left", "skewfromleft"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Skew from Right", "skewfromright"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Skew Short from Left", "skewfromleftshort"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Skew Short from Right", "skewfromrightshort"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Fade In", "fade"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Fade in, Rotate from a Random position and Degree", "randomrotate"));
            AnimateInClassDropDownList.Items.Add(new ListItem("Custom", "customin"));

            AnimateOutClassDropDownList.Items.Add(new ListItem("Short to Top", "stt"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Short to Bottom", "stb"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Short to Right", "str"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Short to Left", "stl"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Long to Top", "ltt"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Long to Bottom", "ltb"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Long to Right", "ltr"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Long to Left", "ltl"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Skew to Left", "skewtoleft"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Skew to Right", "skewtoright"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Skew Short to Left", "skewtoleftshort"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Skew Short to Right", "skewtorightshort"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Fade Out", "fadeout"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Fade Out, Rotate to a Random position and Degree", "randomrotateout"));
            AnimateOutClassDropDownList.Items.Add(new ListItem("Custom", "customout"));

            var CssClasses = KeywordsHelper.GetKeywordValue("revolutionSliderCaptionClasses").Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string CssClass in CssClasses)
            {
                StylingClassDropDownList.Items.Add(CssClass);
            }
            StylingClassDropDownList.AddSelect();

            DataXDropDownList.Items.Add(new ListItem("Left", "left"));
            DataXDropDownList.Items.Add(new ListItem("Center", "center"));
            DataXDropDownList.Items.Add(new ListItem("Right", "right"));
            DataXDropDownList.Items.Add(new ListItem("Custom", "custom"));

            DataYDropDownList.Items.Add(new ListItem("Top", "top"));
            DataYDropDownList.Items.Add(new ListItem("Center", "center"));
            DataYDropDownList.Items.Add(new ListItem("Bottom", "bottom"));
            DataYDropDownList.Items.Add(new ListItem("Custom", "custom"));

            SlidesBL.PopulateEasing(DataStartEasingDropDownList);
            SlidesBL.PopulateEasing(DataEndEasingDropDownList);

            CaptionContentTypeDropDownList.Items.Add("Text/Image");
            CaptionContentTypeDropDownList.Items.Add("Video");

            SplitTypeInDropDownlist.Items.Add(new ListItem("None", string.Empty));
            SplitTypeInDropDownlist.Items.Add(new ListItem("Characters", "chars"));
            SplitTypeInDropDownlist.Items.Add(new ListItem("Words", "words"));
            SplitTypeInDropDownlist.Items.Add(new ListItem("Lines", "lines"));

            SplitTypeOutDropDownlist.Items.Add(new ListItem("None", string.Empty));
            SplitTypeOutDropDownlist.Items.Add(new ListItem("Characters", "chars"));
            SplitTypeOutDropDownlist.Items.Add(new ListItem("Words", "words"));
            SplitTypeOutDropDownlist.Items.Add(new ListItem("Lines", "lines"));
        }

        private void LayerToolbar_Save(object sender, EventArgs e)
        {
            using (var context = new GalleryEntities())
            {
                var layerEntity = new GY_Layers();
                layerEntity.Title = LayerTitleTextBox.Text;
                layerEntity.SlideID = Request.QueryString["SlideID"].LongParse();
                layerEntity.Hide = false;

                layerEntity.LayerContent = CaptionContentEditor.Text;
                layerEntity.LayerContentType = CaptionContentTypeDropDownList.SelectedValue;

                if (LayersBL.Exists(LayerTitleTextBox.Text, layerEntity.SlideID, context))
                {
                    LayerStatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                    LayerStatusMessage.MessageType = StatusMessageType.Info;
                    return;
                }

                try
                {
                    LayersBL.Add(layerEntity, context);

                    try
                    {
                        AddLayerProperties(context, layerEntity.LayerID);

                        LayerStatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                        LayerStatusMessage.MessageType = StatusMessageType.Success;

                        GridView1.DataBind();
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError(ex);
                        LayerStatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                        LayerStatusMessage.MessageType = StatusMessageType.Error;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                    LayerStatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                    LayerStatusMessage.MessageType = StatusMessageType.Error;
                }
            }
        }

        private void LoadData(GalleryEntities context, GY_Slides slideEntity)
        {
            if (slideEntity.GY_SlideProperties.Count == 0)
            {
                SlideStatusMessage.Message = "Slide Not Defined";
                SlideStatusMessage.MessageType = StatusMessageType.Info;
                return;
            }

            SlideTypeDropDownList.SelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "owd-slideType", false, context);
            TransitionDropDownList.SelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-transition", false, context);
            DelaySlider.Value = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-delay", false, context).DoubleParse();
            MasterSpeedSlider.Value = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-masterspeed", false, context).DoubleParse();
            LinkTextBox.Text = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-link", false, context);
            TargetDropDownList.SelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-target", false, context);
            var slideIndexSelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-slideindex", false, context);
            if (slideIndexSelectedValue != "next" && slideIndexSelectedValue != "back" && !string.IsNullOrWhiteSpace(slideIndexSelectedValue))
            {
                SlideIndexDropDownList.SelectedValue = "custom";

                SlideIndexSlider.Value = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-slideindex", false, context).DoubleParse();
            }
            else
            {
                SlideIndexDropDownList.SelectedValue = slideIndexSelectedValue;
            }

            if (SlideTypeDropDownList.SelectedValue == "Simple")
            {
                SlotAmountSlider.Value = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-slotamount", false, context).DoubleParse();

                BGRepeatDropDownList.SelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-bgrepeat", true, context);
                BGPositionDropDownList.SelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-bgposition", true, context);

                var styleAttributeValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "style", true, context);
                if (string.IsNullOrWhiteSpace(styleAttributeValue))
                {
                    SlideBackgroundTypeDropDownList.SelectedValue = "Image";
                }
                else
                {
                    SlideBackgroundTypeDropDownList.SelectedValue = "Solid Color";
                    SlideBackgroundColorTextBox.Text = GetColorValue(styleAttributeValue);
                }

                var bgFitSelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-bgfit", true, context);
                if (bgFitSelectedValue != "cover" && bgFitSelectedValue != "contain" && bgFitSelectedValue != "normal")
                {
                    BGFitWidthSlider.Value = GetFirstValue(bgFitSelectedValue);
                    BGFitHeightSlider.Value = GetLastValue(bgFitSelectedValue);
                    BGFitDropDownList.SelectedValue = "custom";
                }
                else
                {
                    BGFitDropDownList.SelectedValue = bgFitSelectedValue;
                }
            }
            else
            {
                KenBurnBGPositionDropDownList.SelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-bgposition", true, context);
                KenBurnBGPositionEndDropDownList.SelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-bgpositionend", true, context);
                DurationSlider.Value = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-duration", true, context).DoubleParse();
                EaseDropDownList.SelectedValue = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-ease", true, context);
                BGFitSlider.Value = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-bgfit", true, context).DoubleParse();
                BGFitEndSlider.Value = SlidePropertiesBL.GetAttributeValue(slideEntity.SlideID, "data-bgfitend", true, context).DoubleParse();
            }
        }

        private void SaveChanges(GalleryEntities context, string oldBGImageURL, string oldBGImageDummyURL, string oldBGImageThumbURL, string newBGImageURL, string newBGImageDummyURL, string newBGImageThumbURL)
        {
            try
            {
                context.SaveChanges();

                SlideStatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                SlideStatusMessage.MessageType = StatusMessageType.Success;

                oldBGImageDummyURL.DeleteFile();
                oldBGImageThumbURL.DeleteFile();
                oldBGImageURL.DeleteFile();

                if (!string.IsNullOrWhiteSpace(newBGImageURL))
                {
                    fancybox.Attributes.Add("href", ResolveClientUrl(newBGImageURL));
                }

                if (!string.IsNullOrWhiteSpace(newBGImageThumbURL))
                {
                    CoverImage.ImageUrl = newBGImageThumbURL;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                SlideStatusMessage.Message = ExceptionHelper.GetExceptionMessage(ex);
                SlideStatusMessage.MessageType = StatusMessageType.Error;

                newBGImageDummyURL.DeleteFile();
                newBGImageThumbURL.DeleteFile();
                newBGImageURL.DeleteFile();
            }
        }

        private void SlideImageProperties(GY_Slides slideEntity, ref string oldBGImageURL, ref string oldBGImageDummyURL, ref string oldBGImageThumbURL, ref string newBGImageURL, ref string newBGImageDummyURL, ref string newBGImageThumbURL, string transparentImageURL)
        {
            if (SlideBackgroundThumbImageFileUpload.HasFile)
            {
                oldBGImageThumbURL = slideEntity.BGImageThumbURL;
                slideEntity.BGImageThumbURL = newBGImageThumbURL = SlidesBL.GetUploadedImagePath(SlideBackgroundThumbImageFileUpload);
            }

            if (SlideBackgroundImageFileUpload.HasFile)
            {
                oldBGImageURL = (slideEntity.BGImageURL == transparentImageURL) ? string.Empty : slideEntity.BGImageURL;
                slideEntity.BGImageURL = newBGImageURL = SlidesBL.GetUploadedImagePath(SlideBackgroundImageFileUpload);
            }

            if (SlideBackgroundLazyLoadImageFileUpload.HasFile)
            {
                oldBGImageDummyURL = slideEntity.BGImageDummyURL;
                slideEntity.BGImageDummyURL = newBGImageDummyURL = SlidesBL.GetUploadedImagePath(SlideBackgroundLazyLoadImageFileUpload);
            }
        }

        private void SlideToolbar_Cancel(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/UI/Pages/Admin/Gallery/SlideManage.aspx?SildeID={0}", Request.QueryString["SlideID"]));
        }

        private void SlideToolbar_Save(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var oldBGImageURL = string.Empty;
                var oldBGImageDummyURL = string.Empty;
                var oldBGImageThumbURL = string.Empty;

                var newBGImageURL = string.Empty;
                var newBGImageDummyURL = string.Empty;
                var newBGImageThumbURL = string.Empty;

                var transparentImageURL = "~/Scripts/RevolutionSlider/assets/transparent.png";

                var slideID = Request.QueryString["SlideID"].LongParse();

                using (var context = new GalleryEntities())
                {
                    var slideQuery = SlidesBL.GetObjectByID(slideID, context);

                    if (slideQuery != null)
                    {
                        CreateCommon(slideQuery, context);

                        if (SlideTypeDropDownList.SelectedValue == "Simple")
                        {
                            CreateSimpleSlide(slideQuery, context);
                        }
                        else
                        {
                            CreateKenBurnsSlide(slideQuery, context);
                        }

                        if (SlideBackgroundTypeDropDownList.SelectedValue == "Solid Color" && SlideTypeDropDownList.SelectedValue == "Simple")
                        {
                            oldBGImageDummyURL = slideQuery.BGImageDummyURL;
                            oldBGImageURL = (slideQuery.BGImageURL == transparentImageURL) ? string.Empty : slideQuery.BGImageURL;

                            slideQuery.BGImageDummyURL = string.Empty;
                            slideQuery.BGImageURL = transparentImageURL;
                        }
                        else
                        {
                            if (SlideBackgroundTypeDropDownList.SelectedValue == "Image" || SlideTypeDropDownList.SelectedValue == "KenBurns")
                            {
                                SlideImageProperties(slideQuery, ref oldBGImageURL, ref oldBGImageDummyURL, ref oldBGImageThumbURL, ref newBGImageURL, ref newBGImageDummyURL, ref newBGImageThumbURL, transparentImageURL);
                            }
                        }
                        SaveChanges(context, oldBGImageURL, oldBGImageDummyURL, oldBGImageThumbURL, newBGImageURL, newBGImageDummyURL, newBGImageThumbURL);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PositionTextBox.ValidationExpression = Validator.IntegerValidationExpression;
                PositionTextBox.ValidationErrorMessage = Validator.IntegerValidationErrorMessage;

                SlideBackgroundImageFileUpload.ValidationExpression = Validator.ImageValidationExpression;
                SlideBackgroundImageFileUpload.ValidationErrorMessage = Validator.ImageValidationErrorMessage;

                SlideBackgroundLazyLoadImageFileUpload.ValidationExpression = Validator.ImageValidationExpression;
                SlideBackgroundLazyLoadImageFileUpload.ValidationErrorMessage = Validator.ImageValidationErrorMessage;

                SlideBackgroundThumbImageFileUpload.ValidationExpression = Validator.ImageValidationExpression;
                SlideBackgroundThumbImageFileUpload.ValidationErrorMessage = Validator.ImageValidationErrorMessage;

                if (string.IsNullOrWhiteSpace(Request.QueryString["SlideID"]))
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/SlideList.aspx");
                }

                using (var context = new GalleryEntities())
                {
                    var slideID = Request.QueryString["SlideID"].LongParse();

                    var slidesQuery = (from set in context.GY_Slides
                                       select set);
                    SlideIndexSlider.Maximum = slidesQuery.Count();

                    var entity = SlidesBL.GetObjectByID(slideID, context);

                    if (entity != null)
                    {
                        InitializeUI();
                        LoadData(context, entity);

                        TitleTextBox.Text = entity.Title;
                        PositionTextBox.Text = entity.Position.ToString();
                        HideCheckbox.Checked = entity.Hide;

                        if (string.IsNullOrWhiteSpace(entity.BGImageURL))
                        {
                            fancybox.Attributes.Add("href", ResolveClientUrl(AppConfig.NoImage));
                        }
                        else
                        {
                            fancybox.Attributes.Add("href", ResolveClientUrl(entity.BGImageURL));
                        }

                        if (string.IsNullOrWhiteSpace(entity.BGImageThumbURL))
                        {
                            CoverImage.ImageUrl = AppConfig.NoImage;
                        }
                        else
                        {
                            CoverImage.ImageUrl = entity.BGImageThumbURL;
                        }
                    }
                    else
                    {
                        StatusMessage.MessageType = StatusMessageType.Info;
                        StatusMessage.Message = Constants.Messages.ITEM_NOT_EXISTS_MESSAGE;
                    }
                }
            }

            FormToolbar1.Save += FormToolbar1_Save;
            FormToolbar1.Cancel += FormToolbar1_Cancel;
            FormToolbar1.Delete += FormToolbar1_Delete;
            SlideToolbar.Save += SlideToolbar_Save;
            SlideToolbar.Cancel += SlideToolbar_Cancel;
            LayerToolbar.Save += LayerToolbar_Save;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}