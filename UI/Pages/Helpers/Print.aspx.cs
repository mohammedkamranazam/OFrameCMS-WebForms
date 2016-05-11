using OWDARO.Helpers;
using OWDARO.Models;
using OWDARO.Performance;
using System;
using System.Web.UI;

namespace OWDARO.UI.Pages.Helpers
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("PrintPage"));
            }

            if (SessionHelper.Exists(Constants.Keys.PrintSettingsKey))
            {
                var printSettings = new PrintSettings();

                SessionHelper.Get<PrintSettings>(Constants.Keys.PrintSettingsKey, out printSettings);

                var htmlPrintEnabled = false;
                var printCount = 0;
                Control control = null;
                var html = string.Empty;

                printCount = printSettings.PrintCount;

                htmlPrintEnabled = printSettings.HtmlPrintEnabled;

                html = printSettings.HtmlToPrint;

                if (printSettings.ControlToPrint != null)
                {
                    control = printSettings.ControlToPrint;
                }

                if (htmlPrintEnabled)
                {
                    PrintHelper.PrintHtml(html, printCount);
                }
                else
                {
                    if (control != null)
                    {
                        PrintHelper.PrintControl(control, printCount);
                    }
                }
            }
            else
            {
                StatusLabel.Visible = true;
                StatusLabel.Text = "Print Variables Uninitialised";
            }
        }
    }
}