using OWDARO.BLL.MediaBLL;
using OWDARO.OEventArgs;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.MediaModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.Media
{
    public partial class PostCategoriesSelectComponent : UserControl
    {
        public event PostCategoryOpenedEventHandler PostCategoryOpened;

        public bool ResetVisible
        {
            set
            {
                ResetButton.Visible = value;
            }
        }

        public string Locale
        {
            set
            {
                if (string.IsNullOrWhiteSpace(LocaleHiddenField.Value))
                {
                    LocaleHiddenField.Value = AppConfig.DefaultLocale;
                }

                if (!string.IsNullOrWhiteSpace(value))
                {
                    LocaleHiddenField.Value = value;
                }
            }

            get
            {
                return LocaleHiddenField.Value;
            }
        }

        public bool EditMode
        {
            get
            {
                return DataParser.BoolParse(EditModeHiddenField.Value);
            }

            set
            {
                EditModeHiddenField.Value = value.ToString();
            }
        }

        public int InitialValue
        {
            set
            {
                InitialValueHiddenField.Value = value.ToString();
            }

            get
            {
                return InitialValueHiddenField.Value.IntParse();
            }
        }

        public int? ParentPostCategoryID
        {
            get
            {
                return DataParser.NullableIntParse(CurrentPostCategoryIDHiddenField.Value);
            }

            set
            {
                CurrentPostCategoryIDHiddenField.Value = value.ToString();
            }
        }

        private void CheckBackStackForEmpty()
        {
            var stackElements = PostCategoryStackBackHiddenField.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            if (stackElements.Length > 0)
            {
                EnableBackButton();
            }
            else
            {
                DisableBackButton();
            }
        }

        private void DisableBackButton()
        {
            BackButton.Enabled = false;
            BackButton.CssClass += " disabled";
        }

        private void EnableBackButton()
        {
            BackButton.Enabled = true;
            BackButton.CssClass = "btn btn-info";
        }

        private async Task GoHome()
        {
            using (var context = new MediaEntities())
            {
                await GoHome(context);
            }
        }

        private async Task GoHome(MediaEntities context)
        {
            CurrentPostCategoryIDHiddenField.Value = string.Empty;

            var postCategories = await (from set in context.ME_PostCategories
                                        where set.ParentPostCategoryID == null && set.Locale == Locale
                                        select set).ToListAsync();

            PostCategoriesRepeater.DataSource = postCategories;
            PostCategoriesRepeater.DataBind();

            StatusLiteral.Text = BuildCategoryPath(context, null);
        }

        public async Task LoadData()
        {
            await InitializePostCategories();

            DisableBackButton();
        }

        private void OnPostCategoryOpened(int postCategoryID)
        {
            if (PostCategoryOpened != null)
            {
                var args = new PostCategoryOpenedEventArgs(postCategoryID);

                PostCategoryOpened(this, args);
            }
        }

        private async Task OpenPostCategory(int postCategoryID, bool pushToStack)
        {
            using (var context = new MediaEntities())
            {
                var postCategoryQuery = await PostCategoriesBL.GetObjectByIDAsync(postCategoryID, context);

                if (postCategoryQuery != null)
                {
                    PostCategoriesRepeater.DataSource = postCategoryQuery.ME_ChildPostCategories.ToList();
                    PostCategoriesRepeater.DataBind();

                    CurrentPostCategoryIDHiddenField.Value = postCategoryID.ToString();

                    StatusLiteral.Text = BuildCategoryPath(context, postCategoryID);

                    if (pushToStack)
                    {
                        PushToBackStack(postCategoryQuery.ParentPostCategoryID);
                    }

                    OnPostCategoryOpened(postCategoryID);
                }
            }
        }

        private int? PopFromBackStack()
        {
            var stackElements = PostCategoryStackBackHiddenField.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            var lastElement = stackElements.LastOrDefault();

            PostCategoryStackBackHiddenField.Value = PostCategoryStackBackHiddenField.Value.Replace(lastElement + ";", string.Empty);

            lastElement = lastElement.Replace("]", string.Empty);
            lastElement = lastElement.Replace("[", string.Empty);

            var postCategoryID = DataParser.NullableIntParse((lastElement == "-1") ? string.Empty : lastElement);

            CheckBackStackForEmpty();

            return postCategoryID;
        }

        private void PushToBackStack(int? postCategoryID)
        {
            PostCategoryStackBackHiddenField.Value += string.Format("[{0}];", ((postCategoryID == null) ? "-1" : postCategoryID.ToString()));

            CheckBackStackForEmpty();
        }

        protected async void BackButton_Click(object sender, EventArgs e)
        {
            var postCategoryID = PopFromBackStack();

            if (postCategoryID == null)
            {
                await GoHome();

                PostCategoryStackBackHiddenField.Value = string.Empty;

                CheckBackStackForEmpty();
            }
            else
            {
                await OpenPostCategory((int)postCategoryID, false);
            }
        }

        protected async void PostCategoriesRepeater_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            await OpenPostCategory(DataParser.IntParse(e.CommandName), true);
        }

        protected async void HomeButton_Click(object sender, EventArgs e)
        {
            await GoHome();

            PostCategoryStackBackHiddenField.Value = string.Empty;

            CheckBackStackForEmpty();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.RegisterAsyncTask(new PageAsyncTask(async cancellationToken =>
                {
                    await LoadData();
                }));
            }
        }

        public async Task InitializePostCategories()
        {
            using (var context = new MediaEntities())
            {
                await InitializePostCategories(context);
            }
        }

        public async Task InitializePostCategories(MediaEntities context)
        {
            var parentPostCategoryID = DataParser.NullableLongParse(CurrentPostCategoryIDHiddenField.Value);

            var postCategories = await (from set in context.ME_PostCategories
                                        where set.ParentPostCategoryID == parentPostCategoryID && set.Locale == Locale
                                        select set).ToListAsync();

            PostCategoriesRepeater.DataSource = postCategories;
            PostCategoriesRepeater.DataBind();

            StatusLiteral.Text = BuildCategoryPath(context, parentPostCategoryID);
        }

        protected async void ResetButton_Click(object sender, EventArgs e)
        {
            ParentPostCategoryID = InitialValue;

            await InitializePostCategories();
        }

        public string BuildCategoryPath(MediaEntities context, long? categoryID)
        {
            StringBuilder path = new StringBuilder();

            if (categoryID == null)
            {
                return string.Empty;
            }

            var category = (from set in context.ME_PostCategories where set.PostCategoryID == (long)categoryID select set).FirstOrDefault();

            if (category != null)
            {
                path.Insert(0, string.Format("{0}/", category.Title));

                if (category.ParentPostCategoryID != null)
                {
                    path.Insert(0, string.Format("{0}", BuildCategoryPath(context, category.ParentPostCategoryID)));
                }
            }

            return path.ToString();
        }
    }
}