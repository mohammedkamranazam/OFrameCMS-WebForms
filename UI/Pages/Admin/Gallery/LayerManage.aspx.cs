using OWDARO;
using OWDARO.BLL.GalleryBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ProjectJKL.UI.Pages.Admin.Gallery
{
    public partial class LayerManage : System.Web.UI.Page
    {
        private string AnimationInClass(string cssClassValue)
        {
            var classItems = AnimateInClassDropDownList.Items;
            foreach (ListItem classItem in classItems)
            {
                if (cssClassValue.NullableContains(classItem.Value))
                {
                    return classItem.Value;
                }
            }

            return "customin";
        }

        private string AnimationOutClass(string cssClassValue)
        {
            var classItems = AnimateOutClassDropDownList.Items;
            foreach (ListItem classItem in classItems)
            {
                if (cssClassValue.NullableContains(classItem.Value))
                {
                    return classItem.Value;
                }
            }

            return "customout";
        }

        private string CaptionStyleClass(string cssClassValue)
        {
            var cssClasses = KeywordsHelper.GetKeywordValue("revolutionSliderCaptionClasses").Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string cssClass in cssClasses)
            {
                if (cssClassValue.NullableContains(cssClass))
                {
                    return cssClass;
                }
            }

            return "-1";
        }

        private List<ListItem> GetCustomAnimationProperties(string custominValue)
        {
            var values = custominValue.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            var valuePairs = new List<ListItem>();

            foreach (string value in values)
            {
                var propertyAndValue = value.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                if (propertyAndValue.Length == 2)
                {
                    var valuePair = new ListItem(propertyAndValue[0], propertyAndValue[1]);
                    valuePairs.Add(valuePair);
                }
            }

            return valuePairs;
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

        private string GetPropertyValue(List<ListItem> collection, string attribute)
        {
            var property = (from set in collection.Cast<ListItem>()
                            where set.Text == attribute
                            select set).FirstOrDefault();

            if (property != null)
            {
                return property.Value;
            }

            return string.Empty;
        }

        private void InitializeUI()
        {
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

        private void LayerToolbar_Delete(object sender, EventArgs e)
        {
            var layerID = Request.QueryString["LayerID"].LongParse();

            using (var context = new GalleryEntities())
            {
                var layerQuery = LayersBL.GetObjectByID(layerID, context);

                var layerProperties = (from set in context.GY_LayerProperties
                                       where set.LayerID == layerID
                                       select set);

                foreach (GY_LayerProperties property in layerProperties)
                {
                    context.GY_LayerProperties.Remove(property);
                }

                try
                {
                    context.SaveChanges();

                    try
                    {
                        LayersBL.Delete(layerQuery, context);
                        LayerStatusMessage.Message = Constants.Messages.DELETE_SUCCESS_MESSAGE;
                        LayerStatusMessage.MessageType = StatusMessageType.Success;
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

        private void LayerToolbar_Save(object sender, EventArgs e)
        {
            using (var context = new GalleryEntities())
            {
                var layerID = Request.QueryString["LayerID"].LongParse();

                var layerEntity = LayersBL.GetObjectByID(layerID, context);

                if (layerEntity.Title != LayerTitleTextBox.Text)
                {
                    if (LayersBL.Exists(LayerTitleTextBox.Text, layerEntity.SlideID, context))
                    {
                        LayerStatusMessage.Message = Constants.Messages.ITEM_ALREADY_PRESENT;
                        LayerStatusMessage.MessageType = StatusMessageType.Info;
                        return;
                    }
                }

                layerEntity.Title = LayerTitleTextBox.Text;
                layerEntity.LayerContent = CaptionContentEditor.Text;
                layerEntity.LayerContentType = CaptionContentTypeDropDownList.SelectedValue;
                layerEntity.Hide = HideCheckbox.Checked;

                try
                {
                    LayersBL.Save(context);

                    try
                    {
                        SaveLayerProperties(context, layerEntity.LayerID);

                        LayerStatusMessage.Message = Constants.Messages.SAVE_SUCCESS_MESSAGE;
                        LayerStatusMessage.MessageType = StatusMessageType.Success;
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

        private void LoadData(GalleryEntities context, GY_Layers layerQuery)
        {
            var cssClassValue = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "class", context);

            LayerTitleTextBox.Text = layerQuery.Title;
            CaptionContentEditor.Text = layerQuery.LayerContent;
            CaptionContentTypeDropDownList.SelectedValue = layerQuery.LayerContentType;
            HideCheckbox.Checked = layerQuery.Hide;

            DataStartSlider.Value = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-start", context).DoubleParse();
            DataSpeedSlider.Value = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-speed", context).DoubleParse();
            DataStartEasingDropDownList.SelectedValue = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-easing", context);
            DataEndSlider.Value = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-end", context).DoubleParse();
            DataSpeedEndSlider.Value = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-endspeed", context).DoubleParse();
            DataEndEasingDropDownList.SelectedValue = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-endeasing", context);
            LayerStyleAttributeTextBox.Text = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "style", context);

            if (layerQuery.LayerContentType == "Video")
            {
                VideoAutoPlayCheckbox.Checked = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-autoplay", context).BoolParse();
                VideoAutoPlayOnFirstTimeCheckBox.Checked = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-autoplayonlyfirsttime", context).BoolParse();
                VideoNextSlideAtEndCheckBox.Checked = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-nextslideatend", context).BoolParse();

                if (cssClassValue.NullableContains("fullscreenvideo"))
                {
                    IsCaptionVideoFullScreenCheckBox.Checked = true;
                }
                else
                {
                    IsCaptionVideoFullScreenCheckBox.Checked = false;
                }
            }
            else
            {
                SplitTypeInDropDownlist.SelectedValue = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-splitin", context);
                ElementInDelaySlider.Value = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-elementdelay", context).DoubleParse() * 1000;
                SplitTypeOutDropDownlist.SelectedValue = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-splitout", context);
                ElementOutDelaySlider.Value = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-endelementdelay", context).DoubleParse() * 1000;
            }

            var data_x = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-x", context);

            if (data_x == "left" || data_x == "center" || data_x == "right")
            {
                DataXDropDownList.SelectedValue = data_x;
            }
            else
            {
                DataXDropDownList.SelectedValue = "custom";
                DataXSlider.Value = data_x.DoubleParse();
            }

            var data_y = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-y", context);

            if (data_y == "top" || data_y == "center" || data_y == "bottom")
            {
                DataYDropDownList.SelectedValue = data_y;
            }
            else
            {
                DataYDropDownList.SelectedValue = "custom";
                DataYSlider.Value = data_y.DoubleParse();
            }

            if (DataXDropDownList.SelectedValue != "custom")
            {
                DataHOffsetSlider.Value = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-hoffset", context).DoubleParse();
            }

            if (DataYDropDownList.SelectedValue != "custom")
            {
                DataVOffsetSlider.Value = LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-voffset", context).DoubleParse();
            }

            AnimateInClassDropDownList.SelectedValue = AnimationInClass(cssClassValue);

            AnimateOutClassDropDownList.SelectedValue = AnimationOutClass(cssClassValue);

            StylingClassDropDownList.SelectedValue = CaptionStyleClass(cssClassValue);

            if (AnimateInClassDropDownList.SelectedValue == "customin")
            {
                var animationInProperties = GetCustomAnimationProperties(LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-customin", context));

                InTransitionXSlider.Value = GetPropertyValue(animationInProperties, "x").DoubleParse();
                InTransitionYSlider.Value = GetPropertyValue(animationInProperties, "y").DoubleParse();
                InTransitionZSlider.Value = GetPropertyValue(animationInProperties, "z").DoubleParse();

                InRotationXSlider.Value = GetPropertyValue(animationInProperties, "rotationX").DoubleParse();
                InRotationYSlider.Value = GetPropertyValue(animationInProperties, "rotationY").DoubleParse();
                InRotationZSlider.Value = GetPropertyValue(animationInProperties, "rotationZ").DoubleParse();

                InScaleXSlider.Value = GetPropertyValue(animationInProperties, "scaleX").DoubleParse();
                InScaleYSlider.Value = GetPropertyValue(animationInProperties, "scaleY").DoubleParse();

                InSkewXSlider.Value = GetPropertyValue(animationInProperties, "skewX").DoubleParse();
                InSkewYSlider.Value = GetPropertyValue(animationInProperties, "skewY").DoubleParse();

                InOpacitySlider.Value = GetPropertyValue(animationInProperties, "opacity").DoubleParse();

                InPerspectiveSlider.Value = GetPropertyValue(animationInProperties, "transformPerspective").DoubleParse();

                var inTransformOrigin = GetPropertyValue(animationInProperties, "transformOrigin");
                InOriginXSlider.Value = GetFirstValue(inTransformOrigin);
                InOriginYSlider.Value = GetLastValue(inTransformOrigin);
            }

            if (AnimateOutClassDropDownList.SelectedValue == "customout")
            {
                var animationOutProperties = GetCustomAnimationProperties(LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-customout", context));

                OutTransitionXSlider.Value = GetPropertyValue(animationOutProperties, "x").DoubleParse();
                OutTransitionYSlider.Value = GetPropertyValue(animationOutProperties, "y").DoubleParse();
                OutTransitionZSlider.Value = GetPropertyValue(animationOutProperties, "z").DoubleParse();

                OutRotationXSlider.Value = GetPropertyValue(animationOutProperties, "rotationX").DoubleParse();
                OutRotationYSlider.Value = GetPropertyValue(animationOutProperties, "rotationY").DoubleParse();
                OutRotationZSlider.Value = GetPropertyValue(animationOutProperties, "rotationZ").DoubleParse();

                OutScaleXSlider.Value = GetPropertyValue(animationOutProperties, "scaleX").DoubleParse();
                OutScaleYSlider.Value = GetPropertyValue(animationOutProperties, "scaleY").DoubleParse();

                OutSkewXSlider.Value = GetPropertyValue(animationOutProperties, "skewX").DoubleParse();
                OutSkewYSlider.Value = GetPropertyValue(animationOutProperties, "skewY").DoubleParse();

                OutOpacitySlider.Value = GetPropertyValue(animationOutProperties, "opacity").DoubleParse();

                OutPerspectiveSlider.Value = GetPropertyValue(animationOutProperties, "transformPerspective").DoubleParse();

                var outTransformOrigin = GetPropertyValue(animationOutProperties, "transformOrigin");
                OutOriginXSlider.Value = GetFirstValue(outTransformOrigin);
                OutOriginYSlider.Value = GetLastValue(outTransformOrigin);
            }

            CaptionHiddenOnCheckBox.Checked = (LayerPropertiesBL.GetAttributeValue(layerQuery.LayerID, "data-captionhidden", context) == "on") ? true : false;
        }

        private void SaveLayerProperties(GalleryEntities context, long layerID)
        {
            if (CaptionContentTypeDropDownList.SelectedValue == "Video")
            {
                LayerPropertiesBL.Add(layerID, "data-autoplay", VideoAutoPlayCheckbox.Checked.ToString().ToLower(), context);
                LayerPropertiesBL.Add(layerID, "data-autoplayonlyfirsttime", VideoAutoPlayOnFirstTimeCheckBox.Checked.ToString().ToLower(), context);
                LayerPropertiesBL.Add(layerID, "data-nextslideatend", VideoNextSlideAtEndCheckBox.Checked.ToString().ToLower(), context);

                LayerPropertiesBL.Delete(layerID, "data-splitin", context);
                LayerPropertiesBL.Delete(layerID, "data-elementdelay", context);
                LayerPropertiesBL.Delete(layerID, "data-splitout", context);
                LayerPropertiesBL.Delete(layerID, "data-endelementdelay", context);
            }
            else
            {
                LayerPropertiesBL.Delete(layerID, "data-autoplay", context);
                LayerPropertiesBL.Delete(layerID, "data-autoplayonlyfirsttime", context);
                LayerPropertiesBL.Delete(layerID, "data-nextslideatend", context);

                if (SplitTypeInDropDownlist.SelectedValue == string.Empty)
                {
                    LayerPropertiesBL.Delete(layerID, "data-splitin", context);
                    LayerPropertiesBL.Delete(layerID, "data-elementdelay", context);

                    LayerPropertiesBL.Delete(layerID, "data-splitout", context);
                    LayerPropertiesBL.Delete(layerID, "data-endelementdelay", context);
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
            cssClass = string.Format(cssClass, AnimateInClassDropDownList.SelectedValue, AnimateOutClassDropDownList.SelectedValue, (StylingClassDropDownList.SelectedValue == "-1") ? string.Empty : StylingClassDropDownList.SelectedValue, (CaptionContentTypeDropDownList.SelectedValue == "Video" && IsCaptionVideoFullScreenCheckBox.Checked) ? "fullscreenvideo" : string.Empty);
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
            else
            {
                LayerPropertiesBL.Delete(layerID, "data-hoffset", context);
            }

            if (DataYDropDownList.SelectedValue != "custom")
            {
                LayerPropertiesBL.Add(layerID, "data-voffset", DataVOffsetSlider.Value.ToString(), context);
            }
            else
            {
                LayerPropertiesBL.Delete(layerID, "data-voffset", context);
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
                    InTransitionXSlider.Value.ToString(), InTransitionYSlider.Value.ToString(), InTransitionZSlider.Value.ToString(), InRotationXSlider.Value.ToString(), InRotationYSlider.Value.ToString(), InRotationZSlider.Value.ToString(),
                    InScaleXSlider.Value.ToString(), InScaleYSlider.Value.ToString(), InSkewXSlider.Value.ToString(), InSkewYSlider.Value.ToString(), InOpacitySlider.Value.ToString(), InPerspectiveSlider.Value.ToString(),
                    InOriginXSlider.Value.ToString(), InOriginYSlider.Value.ToString());

                LayerPropertiesBL.Add(layerID, "data-customin", customAnimation, context);
            }
            else
            {
                LayerPropertiesBL.Delete(layerID, "data-customin", context);
            }

            if (AnimateOutClassDropDownList.SelectedValue == "customout")
            {
                var customAnimation = string.Format("x:{0};y:{1};z:{2};rotationX:{3};rotationY:{4};rotationZ:{5};scaleX:{6};scaleY:{7};skewX:{8};skewY:{9};opacity:{10};transformPerspective:{11};transformOrigin:{12}% {13}%;",
                    OutTransitionXSlider.Value.ToString(), OutTransitionYSlider.Value.ToString(), OutTransitionZSlider.Value.ToString(), OutRotationXSlider.Value.ToString(), OutRotationYSlider.Value.ToString(), OutRotationZSlider.Value.ToString(),
                    OutScaleXSlider.Value.ToString(), OutScaleYSlider.Value.ToString(), OutSkewXSlider.Value.ToString(), OutSkewYSlider.Value.ToString(), OutOpacitySlider.Value.ToString(), OutPerspectiveSlider.Value.ToString(),
                    OutOriginXSlider.Value.ToString(), OutOriginYSlider.Value.ToString());

                LayerPropertiesBL.Add(layerID, "data-customout", customAnimation, context);
            }
            else
            {
                LayerPropertiesBL.Delete(layerID, "data-customout", context);
            }

            LayerPropertiesBL.Add(layerID, "style", LayerStyleAttributeTextBox.Text, context);

            if (CaptionHiddenOnCheckBox.Checked)
            {
                LayerPropertiesBL.Add(layerID, "data-captionhidden", "on", context);
            }

            context.SaveChanges();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(Request.QueryString["LayerID"]) || string.IsNullOrWhiteSpace(Request.QueryString["SlideID"]))
                {
                    Response.Redirect("~/UI/Pages/Admin/Gallery/SlideList.aspx");
                }

                var layerID = Request.QueryString["LayerID"].LongParse();

                using (var context = new GalleryEntities())
                {
                    var layerQuery = LayersBL.GetObjectByID(layerID, context);

                    if (layerQuery != null)
                    {
                        SlideHyperLink.NavigateUrl = string.Format("~/UI/Pages/Admin/Gallery/SlideManage.aspx?SlideID={0}", layerQuery.SlideID);

                        InitializeUI();
                        LoadData(context, layerQuery);
                    }
                    else
                    {
                        Response.Redirect(string.Format("~/UI/Pages/Admin/Gallery/SlideManage.aspx?SlideID={0}", Request.QueryString["SlideID"]));
                    }
                }
            }

            LayerToolbar.Save += LayerToolbar_Save;
            LayerToolbar.Delete += LayerToolbar_Delete;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.UserRoles.MasterPage;
            this.Master.MasterPageFile = OWDARO.Util.Utilities.GetZiceThemeFile();
        }
    }
}