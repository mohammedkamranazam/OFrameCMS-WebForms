using OWDARO.BLL.GalleryBLL;
using OWDARO.Settings;
using OWDARO.Util;
using System;
using System.Web;

namespace ProjectJKL
{
    public partial class ExternalFormEmbedCode : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("ExternalFormEmbedCodePage"));
                OWDARO.Util.Utilities.SetPageCache(OWDARO.Settings.PageCacheHelper.GetCache("AboutUsPage"));

                if (!string.IsNullOrWhiteSpace(Request.QueryString["EventID"]))
                {
                    var eventID = DataParser.IntParse(Request.QueryString["EventID"]);

                    var eventEntity = EventsBL.GetObjectByID(eventID);

                    if (eventEntity != null)
                    {
                        EmbedCodeLiteral.Text = HttpUtility.HtmlDecode(eventEntity.ExternalFormEmbedCode);
                    }
                    else
                    {
                        EmbedCodeLiteral.Text = "PLEASE SELECT AN EVENT FIRST";
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["EventTypeID"]))
                    {
                        var eventTypeID = DataParser.IntParse(Request.QueryString["EventTypeID"]);

                        var eventType = EventTypesHelper.Get(eventTypeID);

                        if (eventType != null)
                        {
                            EmbedCodeLiteral.Text = HttpUtility.HtmlDecode(eventType.ExternalFormEmbedCode);
                        }
                        else
                        {
                            EmbedCodeLiteral.Text = "PLEASE SELECT AN EVENT FIRST";
                        }
                    }
                    else
                    {
                        EmbedCodeLiteral.Text = "IMPROPER USAGE";
                    }
                }
            }
        }
    }
}