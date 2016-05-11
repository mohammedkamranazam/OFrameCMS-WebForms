using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace OWDARO.BLL.OFrameBLL
{
    public static class MenuBL
    {
        public static void Add(OW_Menu entity)
        {
            using (var context = new OWDAROEntities())
            {
                Add(entity, context);
            }
        }

        public static void Add(OW_Menu entity, OWDAROEntities context)
        {
            context.OW_Menu.Add(entity);

            context.SaveChanges();
        }

        public static async Task AddAsync(OW_Menu entity)
        {
            using (var context = new OWDAROEntities())
            {
                await AddAsync(entity, context);
            }
        }

        public static async Task AddAsync(OW_Menu entity, OWDAROEntities context)
        {
            context.OW_Menu.Add(entity);

            await context.SaveChangesAsync();
        }

        public static void Delete(int id)
        {
            using (var context = new OWDAROEntities())
            {
                Delete(id, context);
            }
        }

        public static void Delete(int id, OWDAROEntities context)
        {
            var query = GetObjectByID(id, context);

            context.OW_Menu.Remove(query);

            context.SaveChanges();
        }

        public static async Task DeleteAsync(OW_Menu entity)
        {
            using (var context = new OWDAROEntities())
            {
                await DeleteAsync(entity, context);
            }
        }

        public static async Task DeleteAsync(OW_Menu entity, OWDAROEntities context)
        {
            context.OW_Menu.Remove(entity);

            await context.SaveChangesAsync();
        }

        public static bool Exists(string title)
        {
            using (var context = new OWDAROEntities())
            {
                return Exists(title, context);
            }
        }

        public static bool Exists(string title, OWDAROEntities context)
        {
            var query = (from set in context.OW_Menu
                         where set.Title == title
                         select set);
            return query.Any();
        }

        public static IList<OW_Menu> GetLevel1Tabs(int rootTabID, string locale)
        {
            using (var context = new OWDAROEntities())
            {
                return GetLevel1Tabs(rootTabID, locale, context);
            }
        }

        public static IList<OW_Menu> GetLevel1Tabs(int rootTabID, string locale, OWDAROEntities context)
        {
            return (from set in context.OW_Menu
                    where set.Hide == false &&
                    set.IsRoot == false &&
                    set.ParentMenuID == rootTabID &&
                    set.ParentMenu.IsRoot &&
                    set.Locale == locale
                    select set).ToList();
        }

        public static async Task<IList<OW_Menu>> GetLevel1TabsAsync(int rootTabID, string locale)
        {
            using (var context = new OWDAROEntities())
            {
                return await GetLevel1TabsAsync(rootTabID, locale, context);
            }
        }

        public static async Task<IList<OW_Menu>> GetLevel1TabsAsync(int rootTabID, string locale, OWDAROEntities context)
        {
            return await (from set in context.OW_Menu
                          where set.Hide == false &&
                          set.IsRoot == false &&
                          set.ParentMenuID == rootTabID &&
                          set.ParentMenu.IsRoot &&
                          set.Locale == locale
                          select set).ToListAsync();
        }

        public static OW_Menu GetObjectByID(int id)
        {
            using (var context = new OWDAROEntities())
            {
                return GetObjectByID(id, context);
            }
        }

        public static OW_Menu GetObjectByID(int id, OWDAROEntities context)
        {
            var entity = (OW_Menu)(from set in context.OW_Menu
                                   where set.MenuID == id
                                   select set).FirstOrDefault();

            return entity;
        }

        public static async Task<OW_Menu> GetObjectByIDAsync(int id)
        {
            using (var context = new OWDAROEntities())
            {
                return await GetObjectByIDAsync(id, context);
            }
        }

        public static async Task<OW_Menu> GetObjectByIDAsync(int id, OWDAROEntities context)
        {
            return await (from set in context.OW_Menu
                          where set.MenuID == id
                          select set).FirstOrDefaultAsync();
        }

        public static int GetPosition(OWDAROEntities context)
        {
            var positionQuery = (from set in context.OW_Menu
                                 select set).OrderByDescending(c => c.Position).Take(1).FirstOrDefault();

            return positionQuery != null ? (int)positionQuery.Position + 1 : 1;
        }

        public static async Task<int> GetPositionAsync(OWDAROEntities context, string locale)
        {
            var positionQuery = await (from set in context.OW_Menu
                                       where set.Hide == false &&
                                       set.Locale == locale
                                       select set).OrderByDescending(c => c.Position).Take(1).FirstOrDefaultAsync();

            return positionQuery != null ? (int)positionQuery.Position + 1 : 1;
        }

        public static IList<OW_Menu> GetRootTabs()
        {
            using (var context = new OWDAROEntities())
            {
                return GetRootTabs(context);
            }
        }

        public static IList<OW_Menu> GetRootTabs(OWDAROEntities context)
        {
            return (from set in context.OW_Menu
                    where set.Hide == false && set.IsRoot == true
                    select set).ToList();
        }

        public static async Task<IList<OW_Menu>> GetRootTabsAsync(string locale)
        {
            using (var context = new OWDAROEntities())
            {
                return await GetRootTabsAsync(context, locale);
            }
        }

        public static async Task<IList<OW_Menu>> GetRootTabsAsync(OWDAROEntities context, string locale)
        {
            return await (from set in context.OW_Menu
                          where set.Hide == false && set.IsRoot == true && set.Locale == locale
                          select set).ToListAsync();
        }

        public static void PopulateNavigateURLs(TextBoxAdv textBox, string locale)
        {
            textBox.PopUpListBox.Items.Clear();
            textBox.PopUpListBox.Items.Add(new ListItem("Blank", "#"));
            textBox.PopUpListBox.Items.Add(new ListItem("Home", String.Format("~/Default.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("About Us", String.Format("~/AboutUs.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Contact Us", String.Format("~/ContactUs.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Photos", String.Format("~/Albums.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("All Events", String.Format("~/Events.aspx?EventSchedule=All&Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Upcoming Events", String.Format("~/Events.aspx?EventSchedule=UpComing&Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("On Going Events", String.Format("~/Events.aspx?EventSchedule=Continuing&Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Past Events", String.Format("~/Events.aspx?EventSchedule=Past&Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Portfolio", String.Format("~/Portfolio.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Posts", String.Format("~/Posts.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Drives", String.Format("~/Drives.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Videos", String.Format("~/VideoCategories.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Audios", String.Format("~/AudioCategories.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Products", String.Format("~/Products.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Cart", String.Format("~/Cart.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Search", String.Format("~/Search.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Add(new ListItem("Login", String.Format("~/UI/Pages/LogOn/Default.aspx?Lang={0}", locale)));
            textBox.PopUpListBox.Items.Insert(0, new ListItem("Cancel", string.Empty));
        }

        public static bool RelatedRecordsExists(int id)
        {
            using (var context = new OWDAROEntities())
            {
                return RelatedRecordsExists(id, context);
            }
        }

        public static bool RelatedRecordsExists(int id, OWDAROEntities context)
        {
            return false;
        }

        public static void Save(OWDAROEntities context)
        {
            context.SaveChanges();
        }

        public static async Task SaveAsync(OWDAROEntities context)
        {
            await context.SaveChangesAsync();
        }

        public static void ShiftPositions(int position, bool isRoot, int? parentMenuID, OWDAROEntities context)
        {
            if (isRoot)
            {
                var rootTabQuery = (from set in context.OW_Menu
                                    where set.Position == position && set.IsRoot == isRoot
                                    select set).FirstOrDefault();

                if (rootTabQuery != null)
                {
                    rootTabQuery.Position++;
                    ShiftPositions((int)rootTabQuery.Position, rootTabQuery.IsRoot, parentMenuID, context);
                }
                else
                {
                    return;
                }
            }
            else
            {
                var levelTabQuery = (from set in context.OW_Menu
                                     where set.Position == position && set.IsRoot == isRoot && set.ParentMenuID == parentMenuID
                                     select set).FirstOrDefault();

                if (levelTabQuery != null)
                {
                    levelTabQuery.Position++;
                    ShiftPositions((int)levelTabQuery.Position, levelTabQuery.IsRoot, parentMenuID, context);
                }
                else
                {
                    return;
                }
            }
        }

        public static async Task ShiftPositionsAsync(int position, bool isRoot, int? parentMenuID, string locale, OWDAROEntities context)
        {
            if (isRoot)
            {
                var rootTabQuery = await (from set in context.OW_Menu
                                          where set.Position == position && set.IsRoot == isRoot && set.Locale == locale
                                          select set).FirstOrDefaultAsync();

                if (rootTabQuery != null)
                {
                    rootTabQuery.Position++;
                    await ShiftPositionsAsync((int)rootTabQuery.Position, rootTabQuery.IsRoot, parentMenuID, locale, context);
                }
                else
                {
                    return;
                }
            }
            else
            {
                var levelTabQuery = await (from set in context.OW_Menu
                                           where set.Position == position && set.IsRoot == isRoot && set.ParentMenuID == parentMenuID && set.Locale == locale
                                           select set).FirstOrDefaultAsync();

                if (levelTabQuery != null)
                {
                    levelTabQuery.Position++;
                    await ShiftPositionsAsync((int)levelTabQuery.Position, levelTabQuery.IsRoot, parentMenuID, locale, context);
                }
                else
                {
                    return;
                }
            }
        }
    }
}