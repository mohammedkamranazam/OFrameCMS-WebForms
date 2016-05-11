using OWDARO.Helpers;
using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class ResponsiveMenuComponent : System.Web.UI.UserControl
    {
        private string BuildLevels(IQueryable<OW_Menu> levelTabs, bool isLevel1)
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

                    var levelAnchor = string.Format("<a href='{1}'>{0}</a>", title, (navigateURL == "#") ? "#" : ResolveClientUrl(navigateURL));

                    var levelListItem = string.Format("<li>{0}{1}</li>", levelAnchor, BuildLevels(levelTab.ChildMenus.AsQueryable(), levelTab.IsRoot));

                    sb.Append(levelListItem);
                }

                return string.Format("<ul>{0}</ul>", sb);
            }
            else
            {
                return string.Empty;
            }
        }

        private void BuildRoot(IQueryable<OW_Menu> menuQuery)
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

                    var rootAnchor = string.Format("<a href='{1}'>{0}</a>", title, (navigateURL == "#") ? "#" : ResolveClientUrl(navigateURL));

                    var rootListItem = string.Format("<li>{0}{1}</li>", rootAnchor, BuildLevels(rootTab.ChildMenus.AsQueryable(), rootTab.IsRoot));

                    sb.Append(rootListItem);
                }

                MenuULItemsLiteral.Text = sb.ToString();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await BuildMenu();
                }));
            }
        }

        public async Task BuildMenu()
        {
            using (var context = new OWDAROEntities())
            {
                string locale = CookiesHelper.GetCookie(Constants.Keys.CurrentCultureCookieKey);

                var menuQuery = await (from set in context.OW_Menu
                                       where set.Hide == false &&
                                       set.Locale == locale
                                       select set).ToListAsync();

                BuildRoot(menuQuery.AsQueryable().OrderBy(c => c.Position));
            }
        }
    }
}