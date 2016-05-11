using System;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class PageShareComponent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string facebook = "var sTop = window.screen.height/2-(218); var sLeft = window.screen.width/2-(313);window.open('http://www.facebook.com/sharer.php?u={0}','sharer','toolbar=0,status=0,width=626,height=256,top='+sTop+',left='+sLeft);return false;";
                FacebookHyperlink.Attributes.Add("onclick", string.Format(facebook, Page.Request.Url));

                string twitter = "var sTop = window.screen.height/2-(218); var sLeft = window.screen.width/2-(313);window.open('http://twitter.com/share?url={0}','sharer','toolbar=0,status=0,width=626,height=256,top='+sTop+',left='+sLeft);return false;";
                TwitterHyperlink.Attributes.Add("onclick", string.Format(twitter, Page.Request.Url));

                string gplus = "var sTop = window.screen.height/2-(218); var sLeft = window.screen.width/2-(313);window.open('https://plus.google.com/share?url={0}','sharer','toolbar=0,status=0,width=626,height=256,top='+sTop+',left='+sLeft);return false;";
                GooglePlusHyperlink.Attributes.Add("onclick", string.Format(gplus, Page.Request.Url));
            }
        }
    }
}