using OWDARO.Settings;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class RevolutionBanner : System.Web.UI.UserControl
    {
        private string GetImageHTML(GY_Slides slide)
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(slide.BGImageDummyURL))
            {
                sb.Append(string.Format(" {0}='{1}'", "src", ResolveClientUrl(slide.BGImageURL)));
            }
            else
            {
                sb.Append(string.Format(" {0}='{1}'", "src", ResolveClientUrl(slide.BGImageDummyURL)));
                sb.Append(string.Format(" {0}='{1}'", "data-lazyload", ResolveClientUrl(slide.BGImageURL)));
            }

            foreach (GY_SlideProperties property in slide.GY_SlideProperties)
            {
                if (property.IsImageProperty && !string.IsNullOrWhiteSpace(property.Value))
                {
                    sb.Append(string.Format(" {0}='{1}'", property.Attribute, property.Value));
                }
            }

            return string.Format("<img{0} />", sb);
        }

        private string GetLayerAttributes(GY_Layers layer)
        {
            StringBuilder sb = new StringBuilder();

            foreach (GY_LayerProperties layerProperty in layer.GY_LayerProperties.OrderBy(c => c.Attribute))
            {
                sb.Append(string.Format(" {0}='{1}'", layerProperty.Attribute, layerProperty.Value));
            }

            return sb.ToString();
        }

        private string GetLayerContent(GY_Layers layer)
        {
            return layer.LayerContent;
        }

        private string GetLayersHTML(GY_Slides slide)
        {
            StringBuilder sb = new StringBuilder();

            foreach (GY_Layers layer in slide.GY_Layers)
            {
                if (layer.Hide)
                {
                    continue;
                }

                sb.Append(string.Format("<div{0}>{1}</div>", GetLayerAttributes(layer), GetLayerContent(layer)));
            }

            return sb.ToString();
        }

        private string GetListItemsHTML(GY_Slides slide)
        {
            StringBuilder sb = new StringBuilder();

            foreach (GY_SlideProperties property in slide.GY_SlideProperties)
            {
                if (!property.IsImageProperty && !string.IsNullOrWhiteSpace(property.Value))
                {
                    sb.Append(string.Format(" {0}='{1}'", property.Attribute, property.Value));
                }
            }

            if (!string.IsNullOrWhiteSpace(slide.BGImageThumbURL))
            {
                sb.Append(string.Format(" {0}='{1}'", "data-thumb", ResolveClientUrl(slide.BGImageThumbURL)));
            }

            return sb.ToString();
        }

        private void InitializeBannerProperties()
        {
            TimerDiv.Attributes.Add("class", KeywordsHelper.GetKeywordValue("bannerTimerClass"));

            delay.Value = KeywordsHelper.GetKeywordValue("delay");
            startwidth.Value = KeywordsHelper.GetKeywordValue("startwidth");
            startheight.Value = KeywordsHelper.GetKeywordValue("startheight");
            autoHeight.Value = KeywordsHelper.GetKeywordValue("autoHeight");
            fullScreenAlignForce.Value = KeywordsHelper.GetKeywordValue("fullScreenAlignForce");

            parallaxDisableOnMobile.Value = KeywordsHelper.GetKeywordValue("parallaxDisableOnMobile");

            fullScreenOffset.Value = KeywordsHelper.GetKeywordValue("fullScreenOffset");

            onHoverStop.Value = KeywordsHelper.GetKeywordValue("onHoverStop");

            thumbWidth.Value = KeywordsHelper.GetKeywordValue("thumbWidth");
            thumbHeight.Value = KeywordsHelper.GetKeywordValue("thumbHeight");
            thumbAmount.Value = KeywordsHelper.GetKeywordValue("thumbAmount");

            hideThumbsOnMobile.Value = KeywordsHelper.GetKeywordValue("hideThumbsOnMobile");
            hideBulletsOnMobile.Value = KeywordsHelper.GetKeywordValue("hideBulletsOnMobile");
            hideArrowsOnMobile.Value = KeywordsHelper.GetKeywordValue("hideArrowsOnMobile");
            hideThumbsUnderResoluition.Value = KeywordsHelper.GetKeywordValue("hideThumbsUnderResoluition");

            hideThumbs.Value = KeywordsHelper.GetKeywordValue("hideThumbs");

            navigationType.Value = KeywordsHelper.GetKeywordValue("navigationType");
            navigationArrows.Value = KeywordsHelper.GetKeywordValue("navigationArrows");
            navigationStyle.Value = KeywordsHelper.GetKeywordValue("navigationStyle");

            navigationHAlign.Value = KeywordsHelper.GetKeywordValue("navigationHAlign");
            navigationVAlign.Value = KeywordsHelper.GetKeywordValue("navigationVAlign");
            navigationHOffset.Value = KeywordsHelper.GetKeywordValue("navigationHOffset");
            navigationVOffset.Value = KeywordsHelper.GetKeywordValue("navigationVOffset");

            soloArrowLeftHalign.Value = KeywordsHelper.GetKeywordValue("soloArrowLeftHalign");
            soloArrowLeftValign.Value = KeywordsHelper.GetKeywordValue("soloArrowLeftValign");
            soloArrowLeftHOffset.Value = KeywordsHelper.GetKeywordValue("soloArrowLeftHOffset");
            soloArrowLeftVOffset.Value = KeywordsHelper.GetKeywordValue("soloArrowLeftVOffset");

            soloArrowRightHalign.Value = KeywordsHelper.GetKeywordValue("soloArrowRightHalign");
            soloArrowRightValign.Value = KeywordsHelper.GetKeywordValue("soloArrowRightValign");
            soloArrowRightHOffset.Value = KeywordsHelper.GetKeywordValue("soloArrowRightHOffset");
            soloArrowRightVOffset.Value = KeywordsHelper.GetKeywordValue("soloArrowRightVOffset");

            touchenabled.Value = KeywordsHelper.GetKeywordValue("touchenabled");

            stopAtSlide.Value = KeywordsHelper.GetKeywordValue("stopAtSlide");
            stopAfterLoops.Value = KeywordsHelper.GetKeywordValue("stopAfterLoops");
            hideCaptionAtLimit.Value = KeywordsHelper.GetKeywordValue("hideCaptionAtLimit");
            hideAllCaptionAtLilmit.Value = KeywordsHelper.GetKeywordValue("hideAllCaptionAtLilmit");
            hideSliderAtLimit.Value = KeywordsHelper.GetKeywordValue("hideSliderAtLimit");

            dottedOverlay.Value = string.Format("'{0}'", KeywordsHelper.GetKeywordValue("dottedOverlay"));

            fullWidth.Value = KeywordsHelper.GetKeywordValue("fullWidth");
            forceFullWidth.Value = KeywordsHelper.GetKeywordValue("forceFullWidth");
            fullScreen.Value = KeywordsHelper.GetKeywordValue("fullScreen");
            fullScreenOffsetContainer.Value = KeywordsHelper.GetKeywordValue("fullScreenOffsetContainer");
            shadow.Value = KeywordsHelper.GetKeywordValue("shadow");
            videoJsPath.Value = KeywordsHelper.GetKeywordValue("videoJsPath");

            lazyLoad.Value = KeywordsHelper.GetKeywordValue("lazyLoad");

            shuffle.Value = KeywordsHelper.GetKeywordValue("shuffle");

            spinner.Value = KeywordsHelper.GetKeywordValue("spinner");

            minFullScreenHeight.Value = KeywordsHelper.GetKeywordValue("minFullScreenHeight");

            keyboardNavigation.Value = KeywordsHelper.GetKeywordValue("keyboardNavigation");
        }

        private async Task LoadData()
        {
            using (var context = new GalleryEntities())
            {
                var slidesQuery = await (from set in context.GY_Slides
                                         where set.Hide == false
                                         select set).ToListAsync();

                StringBuilder sb = new StringBuilder();

                foreach (GY_Slides slide in slidesQuery.OrderBy(p => p.Position))
                {
                    if (slide.GY_SlideProperties.Count > 0)
                    {
                        sb.Append(BuildSlide(slide));
                    }
                }

                SlideLiteral.Text = sb.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));

                InitializeBannerProperties();
            }
        }

        public string BuildSlide(GY_Slides slide)
        {
            return string.Format("<li{0}>{1}{2}</li>", GetListItemsHTML(slide), GetImageHTML(slide), GetLayersHTML(slide));
        }
    }
}