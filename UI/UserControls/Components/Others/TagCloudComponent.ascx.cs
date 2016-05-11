using OWDARO.Models;
using OWDARO.Settings;
using System;
using System.Collections.Generic;

namespace OWDARO.UI.UserControls.Components.Others
{
    public partial class TagCloudComponent : System.Web.UI.UserControl
    {
        public SortedDictionary<string, int> Tags
        {
            get;
            set;
        }

        public string URL
        {
            get;
            set;
        }

        public void GenerateTags()
        {
            TitleLiteral.Text = string.Format("<h2 class='Title'>{0}</h2>", LanguageHelper.GetKey("TagCloud"));

            TagCloudItem item;

            TagCloudItemCollection items = new TagCloudItemCollection();

            foreach (string tag in Tags.Keys)
            {
                var tagCount = 0;
                Tags.TryGetValue(tag, out tagCount);

                item = new TagCloudItem(tag, ResolveClientUrl(string.Format(URL, tag)), tagCount);
                items.Add(item);
            }

            if (items.Count > 0)
            {
                TagCloud1.GenerateTagCloud(items);
            }
            else
            {
                TagCloud1.Visible = false;
                NoContentLiteral.Text = string.Format("<p>No Tags Found</p>");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}