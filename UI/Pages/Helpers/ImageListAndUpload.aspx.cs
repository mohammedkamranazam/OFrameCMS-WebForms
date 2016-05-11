using OWDARO.Util;
using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.UI.WebControls;

namespace OWDARO.UI.Pages.Helpers
{
    public partial class ImageListAndUpload : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetPopUpThemeFile();
        }

        private IQueryable<OW_Images> GetImages(OWDAROEntities context)
        {
            return (from set in context.OW_Images
                    where set.ImageURL.Contains(ImageUploader1.StoragePath) || set.ShowWebImage == true
                    select set).OrderByDescending(c => c.ImageID);
        }

        private string GetSortDirection(string column)
        {
            var sortDirection = "DESC";

            var sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    var lastDirection = ViewState["SortDirection"] as string;

                    if ((lastDirection != null) && (lastDirection == "DESC"))
                    {
                        sortDirection = "ASC";
                    }
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        private void ImageUploader1_ImageUploaded(object sender, OWDARO.OEventArgs.ImageUploadedEventArgs e)
        {
            PopulateImages();
        }

        private void PopulateImages()
        {
            using (var context = new OWDAROEntities())
            {
                var imagesQuery = GetImages(context);

                GridView1.DataSource = imagesQuery.ToList();
                GridView1.DataBind();
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            using (var context = new OWDAROEntities())
            {
                var imagesQuery = GetImages(context);

                GridView1.DataSource = imagesQuery.ToList();
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataBind();
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            using (var context = new OWDAROEntities())
            {
                var imagesQuery = GetImages(context);

                GridView1.DataSource = imagesQuery.OrderBy(e.SortExpression + " " + GetSortDirection(e.SortExpression)).ToList();
                GridView1.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !string.IsNullOrWhiteSpace(Request.QueryString["ImageID"]))
            {
                ImageUploader1.ImageID = Request.QueryString["ImageID"].NullableIntParse();
            }

            if (!IsPostBack && !string.IsNullOrWhiteSpace(Request.QueryString["StoragePath"]))
            {
                ImageUploader1.StoragePath = Request.QueryString["StoragePath"];
            }

            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("ImageListAndUploadPage"));

                PopulateImages();
            }

            ImageUploader1.ImageUploaded += ImageUploader1_ImageUploaded;
        }
    }
}