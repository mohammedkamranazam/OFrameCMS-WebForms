using OWDARO;
using OWDARO.Util;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace ProjectJKL.UI.Pages.Helpers
{
    public partial class FileBrowser : System.Web.UI.Page
    {
        private void BuildFiles()
        {
            var appDomainLength = HttpRuntime.AppDomainAppPath.Length;

            var path = TreeView1.SelectedNode.Value;

            var extensions = GetExtensions();

            var dir = new DirectoryInfo(path);
            var files = dir.EnumerateFiles().Where(f => extensions.Contains(f.Extension.ToLower())).ToArray();

            FilesLiteral.Text = GetHTML(files, appDomainLength) + "<div class='Clear'></div>";
        }

        private static string[] GetExtensions()
        {
            var type = "All";

            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["Type"]))
            {
                type = HttpContext.Current.Request.QueryString["Type"];
            }

            switch (type)
            {
                case "Flash":
                    var flashExtensions = new[] { ".swf", ".flv" };
                    return flashExtensions;

                default:
                case "All":
                case "Images":
                    var imageExtensions = new[] { ".jpg", ".tiff", ".bmp", ".png", ".gif", ".jpeg" };
                    return imageExtensions;
            }
        }

        private string GetHTML(FileInfo[] files, int appDomainLength)
        {
            var html = string.Empty;
            var tag = string.Empty;
            var type = "All";

            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Request.QueryString["Type"]))
            {
                type = HttpContext.Current.Request.QueryString["Type"];
            }

            switch (type)
            {
                case "Flash":
                    foreach (FileInfo file in files)
                    {
                        tag = "<div class='File'><div onclick='GetURL(this);' path='{0}' class='Flash'></div><span>{1}</span></div>";
                        html += string.Format(tag, string.Format("/{0}", file.FullName.Substring(appDomainLength)).Replace("\\", "/"), file.Name);
                    }
                    break;

                default:
                case "Images":
                case "All":
                    foreach (FileInfo file in files)
                    {
                        tag = "<div class='File'><img src='{0}' onclick='GetURL(this);' path='{1}' class='Image' /><span>{2}</span></div>";
                        html += string.Format(tag, ResolveClientUrl(string.Format("~/{0}", file.FullName.Substring(appDomainLength).Replace("\\", "/"))), string.Format("/{0}", file.FullName.Substring(appDomainLength).Replace("\\", "/")), file.Name);
                    }
                    break;
            }

            return html;
        }

        protected void FileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
        {
            var path = Server.MapPath(LocalStorages.Others);
            var fileName = e.FileName;

            if (TreeView1.SelectedNode != null)
            {
                path = TreeView1.SelectedNode.Value;
            }

            if (e.FileName.NullableContains(":\\"))
            {
                fileName = Path.GetFileName(e.FileName);
            }

            FileUpload1.SaveAs(string.Format("{0}\\{1}", path, fileName));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("FileBrowserPage"));

                Utilities.BuildTree(TreeView1, "/Storage", true);

                TreeView1.Nodes[0].Select();

                BuildFiles();
            }
        }

        protected void RefreshButton_Click(object sender, EventArgs e)
        {
            BuildFiles();
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            BuildFiles();
        }
    }
}