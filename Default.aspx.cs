using OWDARO;
using OWDARO.BLL.MediaBLL;
using OWDARO.Helpers;
using OWDARO.Settings;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace ProjectJKL
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            MasterPageFile = OWDARO.Util.Utilities.GetMainThemeFile();
        }

        //private async Task LoadData()
        //{
        //    var postQuery = PostsBL.GetObjectByID(DataParser.IntParse(KeywordsHelper.GetKeywordValue("HomePagePostID")));

        //    if (postQuery != null)
        //    {
        //        using (var contextGal = new GalleryEntities())
        //        {
        //            HomePostLiteral.Text = await Utilities.GetHyperHTMLAsync(StringHelper.RemoveTruncator(postQuery.PostContent), contextGal, Page);
        //        }
        //    }
        //    else
        //    {
        //        HomePagePostDIV.Visible = false;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OWDARO.Util.Utilities.SetPageSEO(Page, OWDARO.Settings.SEOHelper.GetPageSEO("HomePage"));
                OWDARO.Util.Utilities.SetPageCache(OWDARO.Settings.PageCacheHelper.GetCache("HomePage"));

                HomePagePostEmbedComponent.PostID = DataParser.IntParse(KeywordsHelper.GetKeywordValue("HomePagePostID"));
                HomePageTopBlockPostEmbedComponent.PostID = DataParser.IntParse(KeywordsHelper.GetKeywordValue("HomePageTopBlockPostID"));
                HomePageBottomBlockPostEmbedComponent.PostID = DataParser.IntParse(KeywordsHelper.GetKeywordValue("HomePageBottomBlockPostID"));

                //  PdfHelper.GetPDFFromHTML("<h1>test pdf header1</h1>", Server.MapPath(LocalStorages.Storage), "test.pdf");

                //this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                //{
                //    await LoadData();
                //}));


            }
        }

    }
}