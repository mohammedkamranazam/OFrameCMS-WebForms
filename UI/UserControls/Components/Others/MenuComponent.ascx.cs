using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class MenuComponent : System.Web.UI.UserControl
    {
        public string Locale
        {
            get;
            set;
        }

        public bool AllowLinkManagement
        {
            get
            {
                return DataParser.BoolParse(AllowLinkManagementHiddenField.Value);
            }

            set
            {
                AllowLinkManagementHiddenField.Value = value.ToString();
            }
        }

        public string LevelCssClass
        {
            get
            {
                return LevelCssClassHiddenField.Value;
            }

            set
            {
                LevelCssClassHiddenField.Value = value;
            }
        }

        public string RootCssClass
        {
            get
            {
                return RootCssClassHiddenField.Value;
            }

            set
            {
                RootCssClassHiddenField.Value = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Page.RegisterAsyncTask(new PageAsyncTask(LoadData));
            }
        }

        private async Task LoadData()
        {
            string locale = (string.IsNullOrWhiteSpace(Locale)) ? CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey) : Locale;

            await BuildMenuAsync(locale);
        }

        public string BuildLevels(IQueryable<OW_Menu> levelTabs, bool isLevel1)
        {
            var levelTabsQuery = (from set in levelTabs
                                  where set.Hide == false
                                  select set).OrderBy(c => c.Position);

            if (levelTabsQuery.Any())
            {
                StringBuilder sb = new StringBuilder();

                foreach (OW_Menu levelTab in levelTabsQuery)
                {
                    var title = levelTab.Title;
                    var navigateURL = levelTab.NavigateURL;
                    var editURL = string.Empty;

                    if (AllowLinkManagement)
                    {
                        editURL = string.Format("<span class='EditDiv'>{1}<a href='{0}' class='LinkEditStyle'>EDIT</a></span>", ResolveClientUrl(string.Format("~/UI/Pages/Admin/OFrame/MenuManage.aspx?MenuID={0}", levelTab.MenuID)), levelTab.Position);
                    }

                    var levelAnchor = string.Format("{2}<a href='{1}'>{0}</a>", title, (navigateURL == "#") ? "#" : ResolveClientUrl(navigateURL), editURL);

                    var levelListItem = string.Format("<li>{0}{1}</li>", levelAnchor, BuildLevels(levelTab.ChildMenus.AsQueryable(), levelTab.IsRoot));

                    sb.Append(levelListItem);
                }

                return string.Format("<ul{1}>{0}</ul>", sb, (!string.IsNullOrWhiteSpace(LevelCssClass) && isLevel1) ? string.Format(" class='{0}'", LevelCssClass) : string.Empty);
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task BuildMenuAsync(string locale)
        {
            if (!string.IsNullOrWhiteSpace(RootCssClass))
            {
                MenuUL.Attributes.Add("class", RootCssClass);
            }

            using (var context = new OWDAROEntities())
            {
                var menuQuery = await (from set in context.OW_Menu
                                       where set.Locale == locale
                                       select set).ToListAsync();

                BuildRoot(menuQuery.AsQueryable().OrderBy(c => c.Position));
            }
        }

        public void BuildRoot(IQueryable<OW_Menu> menuQuery)
        {
            var rootTabs = (from set in menuQuery
                            where set.IsRoot && set.Hide == false
                            select set).OrderBy(c => c.Position);

            if (rootTabs.Any())
            {
                StringBuilder sb = new StringBuilder();

                foreach (OW_Menu rootTab in rootTabs)
                {
                    var title = rootTab.Title;
                    var navigateURL = rootTab.NavigateURL;
                    var editURL = string.Empty;

                    if (AllowLinkManagement)
                    {
                        editURL = string.Format("<span class='EditDiv'>{1}<a href='{0}' class='LinkEditStyle'>EDIT</a></span>", ResolveClientUrl(string.Format("~/UI/Pages/Admin/OFrame/MenuManage.aspx?MenuID={0}", rootTab.MenuID)), rootTab.Position);
                    }

                    var rootAnchor = string.Format("{2}<a href='{1}'>{0}</a>", title, (navigateURL == "#") ? "#" : ResolveClientUrl(navigateURL), editURL);

                    var rootListItem = string.Format("<li>{0}{1}</li>", rootAnchor, BuildLevels(rootTab.ChildMenus.AsQueryable(), rootTab.IsRoot));

                    sb.Append(rootListItem);
                }

                MenuUL.InnerHtml = sb.Append("<div class='Clear'></div>").ToString();
            }
        }
    }
}