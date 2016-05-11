using OWDARO.BLL.GalleryBLL;
using OWDARO.OEventArgs;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OWDARO.UI.UserControls.Components.Gallery
{
    public partial class AudioCategoriesSelectComponent : UserControl
    {
        public event AudioCategoryOpenedEventHandler AudioCategoryOpened;

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

        public int? ParentAudioCategoryID
        {
            get
            {
                return DataParser.NullableIntParse(CurrentAudioCategoryIDHiddenField.Value);
            }

            set
            {
                CurrentAudioCategoryIDHiddenField.Value = value.ToString();
            }
        }

        private void CheckBackStackForEmpty()
        {
            var stackElements = AudioCategoryStackBackHiddenField.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

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
            using (var context = new GalleryEntities())
            {
                await GoHome(context);
            }
        }

        private async Task GoHome(GalleryEntities context)
        {
            CurrentAudioCategoryIDHiddenField.Value = string.Empty;

            var audioCategories = await (from set in context.GY_AudioCategories
                                         where set.ParentAudioCategoryID == null && set.Locale == Locale
                                         select set).ToListAsync();

            AudioCategoriesRepeater.DataSource = audioCategories;
            AudioCategoriesRepeater.DataBind();

            StatusLiteral.Text = BuildCategoryPath(context, null);
        }

        public async Task LoadData()
        {
            await InitializeAudioCategories();

            DisableBackButton();
        }

        private void OnAudioCategoryOpened(int audioCategoryID)
        {
            if (AudioCategoryOpened != null)
            {
                var args = new AudioCategoryOpenedEventArgs(audioCategoryID);

                AudioCategoryOpened(this, args);
            }
        }

        private async Task OpenAudioCategory(int audioCategoryID, bool pushToStack)
        {
            using (var context = new GalleryEntities())
            {
                var audioCategoryQuery = await AudioCategoriesBL.GetObjectByIDAsync(audioCategoryID, context);

                if (audioCategoryQuery != null)
                {
                    AudioCategoriesRepeater.DataSource = audioCategoryQuery.GY_ChildAudioCategories.ToList();
                    AudioCategoriesRepeater.DataBind();

                    CurrentAudioCategoryIDHiddenField.Value = audioCategoryID.ToString();

                    StatusLiteral.Text = BuildCategoryPath(context, audioCategoryID);

                    if (pushToStack)
                    {
                        PushToBackStack(audioCategoryQuery.ParentAudioCategoryID);
                    }

                    OnAudioCategoryOpened(audioCategoryID);
                }
            }
        }

        private int? PopFromBackStack()
        {
            var stackElements = AudioCategoryStackBackHiddenField.Value.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            var lastElement = stackElements.LastOrDefault();

            AudioCategoryStackBackHiddenField.Value = AudioCategoryStackBackHiddenField.Value.Replace(lastElement + ";", string.Empty);

            lastElement = lastElement.Replace("]", string.Empty);
            lastElement = lastElement.Replace("[", string.Empty);

            var audioCategoryID = DataParser.NullableIntParse((lastElement == "-1") ? string.Empty : lastElement);

            CheckBackStackForEmpty();

            return audioCategoryID;
        }

        private void PushToBackStack(int? audioCategoryID)
        {
            AudioCategoryStackBackHiddenField.Value += string.Format("[{0}];", ((audioCategoryID == null) ? "-1" : audioCategoryID.ToString()));

            CheckBackStackForEmpty();
        }

        protected async void BackButton_Click(object sender, EventArgs e)
        {
            var audioCategoryID = PopFromBackStack();

            if (audioCategoryID == null)
            {
                await GoHome();

                AudioCategoryStackBackHiddenField.Value = string.Empty;

                CheckBackStackForEmpty();
            }
            else
            {
                await OpenAudioCategory((int)audioCategoryID, false);
            }
        }

        protected async void AudioCategoriesRepeater_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            await OpenAudioCategory(DataParser.IntParse(e.CommandName), true);
        }

        protected async void HomeButton_Click(object sender, EventArgs e)
        {
            await GoHome();

            AudioCategoryStackBackHiddenField.Value = string.Empty;

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

        public async Task InitializeAudioCategories()
        {
            using (var context = new GalleryEntities())
            {
                await InitializeAudioCategories(context);
            }
        }

        public async Task InitializeAudioCategories(GalleryEntities context)
        {
            var parentAudioCategoryID = DataParser.NullableLongParse(CurrentAudioCategoryIDHiddenField.Value);

            var audioCategories = await (from set in context.GY_AudioCategories
                                         where set.ParentAudioCategoryID == parentAudioCategoryID && set.Locale == Locale
                                         select set).ToListAsync();

            AudioCategoriesRepeater.DataSource = audioCategories;
            AudioCategoriesRepeater.DataBind();

            StatusLiteral.Text = BuildCategoryPath(context, parentAudioCategoryID);
        }

        protected async void ResetButton_Click(object sender, EventArgs e)
        {
            ParentAudioCategoryID = InitialValue;

            await InitializeAudioCategories();
        }

        public string BuildCategoryPath(GalleryEntities context, long? categoryID)
        {
            StringBuilder path = new StringBuilder();

            if (categoryID == null)
            {
                return string.Empty;
            }

            var category = (from set in context.GY_AudioCategories where set.AudioCategoryID == (long)categoryID select set).FirstOrDefault();

            if (category != null)
            {
                path.Insert(0, string.Format("{0}/", category.Title));

                if (category.ParentAudioCategoryID != null)
                {
                    path.Insert(0, string.Format("{0}", BuildCategoryPath(context, category.ParentAudioCategoryID)));
                }
            }

            return path.ToString();
        }
    }
}