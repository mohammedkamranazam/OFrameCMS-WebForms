using OWDARO.BLL.GalleryBLL;
using OWDARO.BLL.MembershipBLL;
using OWDARO.Models;
using OWDARO.Performance;
using OWDARO.Settings;
using OWDARO.UI.UserControls.Controls;
using ProjectJKL.AppCode.DAL.GalleryModel;
using ProjectJKL.AppCode.DAL.OWDAROModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace OWDARO.Util
{
    public static class Utilities
    {
        public static string GetTagsHTML(string tags, Page page)
        {
            var tagsList = tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new StringBuilder();

            foreach (var tag in tagsList)
            {
                sb.Append(string.Format("<strong><a href='{1}'>{0}</a></strong>, ", tag, page.ResolveClientUrl(string.Format("~/Search.aspx?Search={0}", tag))));
            }

            if (sb.Length > 0)
            {
                var lastCommaIndex = sb.ToString().LastIndexOf(',');

                sb = sb.Remove(lastCommaIndex, 1);
            }
            else
            {
                sb.Append("No tags");
            }

            return sb.ToString();
        }

        public static async Task<string> GetHyperHTMLAsync(string content, GalleryEntities context, Page page)
        {
            MatchCollection mc = StringHelper.GetMatchCollection(content);

            if (mc == null)
            {
                return content;
            }

            foreach (Match m in mc)
            {
                content = content.Replace(m.Value, await GetKeyHTMLAsync(StringHelper.GetKey(m), context, page));
            }

            return content;
        }

        public static async Task<string> GetKeyHTMLAsync(ListItem key, GalleryEntities context, Page page)
        {
            switch (key.Text)
            {
                case "EventID":
                    return "EVENT HTML";

                #region ALBUM

                case "AlbumID":
                    var album = await AlbumsBL.GetObjectByIDAsync(key.Value.IntParse(), context);
                    if (album != null)
                    {
                        return AlbumsBL.GetAlbumHTML(album, LanguageHelper.GetLocaleDirection(album.Locale), page, LanguageHelper.GetKey("Photos", album.Locale));
                    }
                    else
                    {
                        return "Album Does Not Exists";
                    }

                #endregion ALBUM

                #region PHOTOS

                case "GalleryID":
                    var gallery = await AlbumsBL.GetObjectByIDAsync(key.Value.IntParse(), context);
                    if (gallery != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (var galleryPhoto in gallery.GY_Photos)
                        {
                            sb.Append(Utilities.GetFancyBoxHTML(galleryPhoto.ImageID, string.Empty, true, string.Empty,
                                string.Format("title='{0}'",
                                String.Format("{0}: {1}",
                                galleryPhoto.Title,
                                galleryPhoto.Description))));
                        }
                        return sb.ToString();
                    }
                    else
                    {
                        return "Gallery Does Not Exists";
                    }

                #endregion PHOTOS

                #region PHOTO

                case "PhotoID":
                    var photo = await PhotosBL.GetObjectByIDAsync(key.Value.IntParse(), context);
                    if (photo != null)
                    {
                        return Utilities.GetFancyBoxHTML(photo.ImageID, string.Empty, true, string.Empty, string.Format("title='{0}'", String.Format("{0}: {1}", photo.Title, photo.Description)));
                    }
                    else
                    {
                        return "Photo Does Not Exists";
                    }

                #endregion PHOTO

                #region FILE

                case "FileID":
                    var file = await FilesBL.GetObjectByIDAsync(key.Value.IntParse(), context);
                    if (file != null)
                    {
                        return FilesBL.GetFileHTML(file, LanguageHelper.GetKey(file.Locale), page);
                    }
                    else
                    {
                        return "File Does Not Exists";
                    }

                #endregion FILE

                #region FOLDER

                case "FolderID":
                    var folder = await FoldersBL.GetObjectByIDAsync(key.Value.IntParse(), context);
                    if (folder != null)
                    {
                        return FoldersBL.GetFolderHTML(folder, LanguageHelper.GetKey(folder.Locale), page);
                    }
                    else
                    {
                        return "Folder Does Not Exists";
                    }

                #endregion FOLDER

                #region DRIVE

                case "DriveID":
                    var drive = await DrivesBL.GetObjectByIDAsync(key.Value.IntParse(), context);
                    if (drive != null)
                    {
                        return DrivesBL.GetDrivesHTML(drive, LanguageHelper.GetKey(drive.Locale), page);
                    }
                    else
                    {
                        return "Drive Does Not Exists";
                    }

                #endregion DRIVE

                case "VideoSectionID":
                    return "VIDEO SECTION HTML";

                case "VideoCategoryID":
                    return "VIDEO CATEGORY HTML";

                case "VideoSetID":
                    return "VIDEO SET HTML";

                #region VIDEO

                case "VideoID":
                    var video = await VideosBL.GetObjectByIDAsync(key.Value.IntParse(), context);
                    if (video != null)
                    {
                        return await VideosBL.GetVideoHTML(video, LanguageHelper.GetKey(video.Locale), context, page);
                    }
                    else
                    {
                        return "Video Does Not Exists";
                    }

                #endregion VIDEO

                case "AudioSectionID":
                    return "AUDIO SECTION HTML";

                case "AudioCategoryID":
                    return "AUDIO CATEGORY HTML";

                case "AudioSetID":
                    return "AUDIO SET HTML";

                #region AUDIO

                case "AudioID":
                    var audio = await AudiosBL.GetObjectByIDAsync(key.Value.IntParse(), context);
                    if (audio != null)
                    {
                        return await AudiosBL.GetAudioHTML(audio, LanguageHelper.GetKey(audio.Locale), page, context);
                    }
                    else
                    {
                        return "Audio Does Not Exists";
                    }

                #endregion AUDIO

                case "Clear":
                    return "<div class='Clear'></div>";

                default:
                    return "NOT DEFINED";
            }
        }

        public static string GetMainThemeFile()
        {
            return string.Format(UserRoleHelper.GetRoleMasterPage(UserRoles.AnonymousRole), AppConfig.MainTheme);
        }

        public static string GetPopUpThemeFile()
        {
            return string.Format(UserRoleHelper.GetRoleMasterPage(UserRoles.AnonymousRole), AppConfig.PopUpTheme);
        }

        public static string GetCheckOutThemeFile()
        {
            return string.Format(UserRoleHelper.GetRoleMasterPage(UserRoles.AnonymousRole), AppConfig.CheckOutTheme);
        }

        public static string GetZiceThemeFile()
        {
            return string.Format("~/Themes/{0}/Main.Master", AppConfig.ZiceTheme);
        }

        public static string GetRoleThemeFile()
        {
            return string.Format("~/Themes/{0}/Main.Master", UserRoleHelper.GetRoleSetting(UserBL.GetUserRole()).Theme);
        }

        private static void ClearControls(Control control)
        {
            for (var i = control.Controls.Count - 1; i >= 0; i--)
            {
                ClearControls(control.Controls[i]);
            }

            if (!(control is TableCell))
            {
                if (control.GetType().GetProperty("SelectedItem") != null)
                {
                    var literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    try
                    {
                        literal.Text =
                            (string)control.GetType().GetProperty("SelectedItem").
                                GetValue(control, null);
                    }
                    catch
                    {
                    }

                    control.Parent.Controls.Remove(control);
                }
                else
                {
                    if (control.GetType().GetProperty("Text") != null)
                    {
                        var literal = new LiteralControl();
                        control.Parent.Controls.Add(literal);
                        literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                        control.Parent.Controls.Remove(control);
                    }
                }
            }
        }

        private static void TraverseFiles(DirectoryInfo currentDir, TreeNode currentNode)
        {
            foreach (var file in currentDir.GetFiles())
            {
                var node = new TreeNode(file.Name, file.FullName);
                currentNode.ChildNodes.Add(node);
            }
        }

        private static void TraverseFiles(DirectoryInfo currentDir, TreeNode currentNode, string[] patterns)
        {
            foreach (var pattern in patterns)
            {
                foreach (var file in currentDir.GetFiles(pattern))
                {
                    var node = new TreeNode(file.Name, file.FullName);
                    currentNode.ChildNodes.Add(node);
                }
            }
        }

        private static void TraverseTree(DirectoryInfo currentDir, TreeNode currentNode, bool skipFiles)
        {
            foreach (var dir in currentDir.GetDirectories())
            {
                var node = new TreeNode(dir.Name, dir.FullName);
                currentNode.ChildNodes.Add(node);
                if (!skipFiles)
                {
                    TraverseFiles(dir, node);
                }
                TraverseTree(dir, node, skipFiles);
            }
        }

        private static void TraverseTree(DirectoryInfo currentDir, TreeNode currentNode, string[] patterns)
        {
            foreach (var dir in currentDir.GetDirectories())
            {
                var add = false;

                foreach (var pattern in patterns)
                {
                    if (dir.GetFiles(pattern, SearchOption.AllDirectories).Length > 0)
                    {
                        add = true;
                    }
                }

                if (add)
                {
                    var node = new TreeNode(dir.Name, dir.FullName);
                    currentNode.ChildNodes.Add(node);
                    TraverseFiles(dir, node, patterns);
                    TraverseTree(dir, node, patterns);
                }
            }
        }

        public static void BuildTree(TreeView TreeView1, string path, bool skipFiles)
        {
            var rootDir = new DirectoryInfo(HttpRuntime.AppDomainAppPath + path);

            TreeView1.Nodes.Clear();

            var rootNode = new TreeNode(rootDir.Name, rootDir.FullName);
            TreeView1.Nodes.Add(rootNode);

            TraverseTree(rootDir, rootNode, skipFiles);
        }

        public static void BuildTree(TreeView TreeView1, string path, string[] patterns)
        {
            var rootDir = new DirectoryInfo(HttpRuntime.AppDomainAppPath + path);

            TreeView1.Nodes.Clear();

            var rootNode = new TreeNode(rootDir.Name, rootDir.FullName);
            TreeView1.Nodes.Add(rootNode);

            TraverseFiles(rootDir, rootNode, patterns);

            TraverseTree(rootDir, rootNode, patterns);
        }

        public static void ClearOldPerformanceKeys()
        {
            ClearPerformance(Constants.Keys.AvatarPathPerformanceKey, PerformanceMode.Session);
        }

        public static void ClearPerformance(string performanceKey)
        {
            ClearPerformance(performanceKey, AppConfig.PerformanceMode);
        }

        public static void ClearPerformance(string performanceKey, PerformanceMode performanceMode)
        {
            switch (performanceMode)
            {
                case PerformanceMode.ApplicationState:
                    if (ApplicationStateHelper.Exists(performanceKey))
                    {
                        ApplicationStateHelper.Clear(performanceKey);
                    }
                    break;

                case PerformanceMode.Cache:
                    if (CacheHelper.Exists(performanceKey))
                    {
                        CacheHelper.Clear(performanceKey);
                    }
                    break;

                case PerformanceMode.MemoryCache:
                    if (MemoryCacheHelper.Exists(performanceKey))
                    {
                        MemoryCacheHelper.Clear(performanceKey);
                    }
                    break;

                case PerformanceMode.Session:
                    if (SessionHelper.Exists(performanceKey))
                    {
                        SessionHelper.Clear(performanceKey);
                    }
                    break;

                case PerformanceMode.None:
                    break;
            }
        }

        public static DateTime DateTimeNow()
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, AppConfig.TargetTimeZoneID);
        }

        public static void ExportExcel(ControlCollection Controls, Object datasource, string filename)
        {
            var gridview = new GridView();
            gridview.DataSource = datasource;
            gridview.DataBind();
            gridview.AllowPaging = false;

            if (gridview.Rows.Count > 65535)
            {
                return;
            }

            filename = String.Format("{0}_{1}.xls", filename, Utilities.DateTimeNow());

            var response = HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            response.Charset = string.Empty;

            response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);

            response.ContentType = "application/vnd.xls";

            var stringWriter = new StringWriter();
            var htmlWriter = new HtmlTextWriter(stringWriter);

            ClearControls(gridview);

            var form = new HtmlForm();
            Controls.Add(form);
            form.Controls.Add(gridview);
            form.RenderControl(htmlWriter);

            response.Write(stringWriter.ToString());
            response.End();
        }

        public static string GetAbsoluteURL(this string relativeURL)
        {
            var url = string.Empty;

            if (!string.IsNullOrWhiteSpace(relativeURL))
            {
                if (relativeURL.Substring(0, 1) == "~")
                {
                    url = relativeURL.Remove(0, 1);
                }
                else
                {
                    url = relativeURL;
                }
            }

            return string.Format("http{0}://{1}{2}{3}",
            (HttpContext.Current.Request.IsSecureConnection) ? "s" : "",
            HttpContext.Current.Request.Url.Host,
            (HttpContext.Current.Request.Url.Port != 80) ? string.Format(":{0}", HttpContext.Current.Request.Url.Port.ToString()) : "",
            url);
        }

        public static string GetFancyBoxHTML(int? imageID, string cssClass, bool isGrouped, string imgExtra)
        {
            return GetFancyBoxHTML(imageID, cssClass, isGrouped, imgExtra, string.Empty);
        }

        public static string GetFancyBoxHTML(int? imageID, string cssClass, bool isGrouped, string imgExtra, string anchorExtra)
        {
            using (var context = new OWDAROEntities())
            {
                if (imageID != null)
                {
                    var entity = (from set in context.OW_Images
                                  where set.ImageID == imageID
                                  select set).FirstOrDefault();

                    if (entity != null)
                    {
                        return GetFancyBoxHTML(entity, cssClass, isGrouped, imgExtra, anchorExtra);
                    }
                }

                var fancyBox = "class='fancybox'";

                if (isGrouped)
                {
                    fancyBox = "rel='fancybox'";
                }

                var anchorTag = "<a {2} href='{0}' {3}>{1}</a>";
                var imgTag = "<img src='{0}' alt='{1}' class='{2}' {3} />";

                var anchorURL = AppConfig.NoImage.Remove(0, 1);
                var imgTagURL = anchorURL;
                var imgAlt = "no image";

                imgTag = string.Format(imgTag, imgTagURL, imgAlt, cssClass, imgExtra);

                anchorTag = string.Format(anchorTag, anchorURL, imgTag, fancyBox, anchorExtra);

                return anchorTag;
            }
        }

        public static string GetFancyBoxHTML(OW_Images entity, string cssClass, bool isGrouped, string imgExtra, string anchorExtra)
        {
            var fancyBox = "class='fancybox'";

            if (isGrouped)
            {
                fancyBox = "rel='fancybox'";
            }

            var anchorTag = "<a {2} href='{0}' {3} >{1}</a>";
            var imgTag = "<img src='{0}' alt='{1}' class='{2}' {3} />";

            var anchorURL = string.Empty;
            var imgTagURL = string.Empty;
            var imgAlt = entity.Title;

            anchorURL = GetImageURL(entity);
            imgTagURL = GetImageThumbURL(entity);

            imgTag = string.Format(imgTag, imgTagURL, imgAlt, cssClass, imgExtra);

            anchorTag = string.Format(anchorTag, anchorURL, imgTag, fancyBox, anchorExtra);

            return anchorTag;
        }

        public static string GetHTML(Control ctrl, string Script)
        {
            HtmlForm frm = new HtmlForm();
            using (Page pg = new Page())
            {
                HttpContext.Current.Response.Clear();

                var stringWrite = new StringWriter();
                var htmlWrite = new HtmlTextWriter(stringWrite);

                if (ctrl is WebControl)
                {
                    var w = new Unit(100, UnitType.Percentage);

                    ((WebControl)ctrl).Width = w;
                }
                pg.EnableEventValidation = false;

                if (!string.IsNullOrWhiteSpace(Script))
                {
                    pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
                }
                pg.Controls.Add(frm);
                frm.Attributes.Add("runat", "server");
                frm.Controls.Add(ctrl);
                pg.DesignerInitialize();
                pg.RenderControl(htmlWrite);
                var strHTML = stringWrite.ToString();

                return strHTML;
            }
        }

        public static string GetImageThumbURL(int? imageID)
        {
            if (imageID == null)
            {
                return AppConfig.NoImage;
            }

            using (var context = new OWDAROEntities())
            {
                var imageQuery = (from set in context.OW_Images
                                  where set.ImageID == imageID
                                  select set).FirstOrDefault();

                if (imageQuery != null)
                {
                    return GetImageThumbURL(imageQuery);
                }
                else
                {
                    return AppConfig.NoImage;
                }
            }
        }

        public static string GetImageThumbURL(OW_Images entity)
        {
            return GetImageThumbURL(entity, null, null);
        }

        public static string GetImageThumbURL(OW_Images entity, int? maxWidth, int? maxHeight)
        {
            if (entity.ShowWebImage)
            {
                if (!string.IsNullOrWhiteSpace(entity.WebImageURL))
                {
                    return GetWebImageThumbURLCodified(entity, maxWidth, maxHeight);
                }
                else
                {
                    return AppConfig.NoImage.Remove(0, 1);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(entity.ImageURL))
                {
                    return GetImageThumbURLCodified(entity, maxWidth, maxHeight).Remove(0, 1);
                }
                else
                {
                    return AppConfig.NoImage.Remove(0, 1);
                }
            }
        }

        public static string GetWebImageThumbURLCodified(OW_Images entity)
        {
            return GetWebImageThumbURLCodified(entity, null, null);
        }

        public static string GetWebImageThumbURLCodified(OW_Images entity, int? maxWidth, int? maxHeight)
        {
            var maxWidthValue = entity.MaxWidth;
            var maxHeightValue = entity.MaxHeight;

            if (maxWidth != null)
            {
                maxWidthValue = (int)maxWidth;
            }

            if (maxHeight != null)
            {
                maxHeightValue = (int)maxHeight;
            }

            return string.Format("{0}?width={1}&height={2}&quality={3}&maxwidth={4}&maxheight={5}&mode={6}&scale={7}&404=default",
                                                            entity.WebImageURL,
                                                            entity.ThumbWidth,
                                                            entity.ThumbHeight,
                                                            entity.ThumbQuality,
                                                            maxWidthValue,
                                                            maxHeightValue,
                                                            entity.ThumbMode,
                                                            entity.ThumbScale);
        }

        public static string GetImageThumbURLCodified(OW_Images entity)
        {
            return GetImageThumbURLCodified(entity, null, null);
        }

        public static string GetImageThumbURLCodified(OW_Images entity, int? maxWidth, int? maxHeight)
        {
            var maxWidthValue = entity.MaxWidth;
            var maxHeightValue = entity.MaxHeight;

            if (maxWidth != null)
            {
                maxWidthValue = (int)maxWidth;
            }

            if (maxHeight != null)
            {
                maxHeightValue = (int)maxHeight;
            }

            return string.Format("{0}?width={1}&height={2}&quality={3}&maxwidth={4}&maxheight={5}&mode={6}&scale={7}&404=default",
                                                            entity.ImageURL,
                                                            entity.ThumbWidth,
                                                            entity.ThumbHeight,
                                                            entity.ThumbQuality,
                                                            maxWidthValue,
                                                            maxHeightValue,
                                                            entity.ThumbMode,
                                                            entity.ThumbScale);
        }

        public static string GetFocusPointImage(int? imageID, string frameClass, string altText, string imageClass, Page page)
        {
            if (imageID == null)
            {
                return string.Empty;
            }

            using (var context = new OWDAROEntities())
            {
                var imageQuery = (from set in context.OW_Images
                                  where set.ImageID == imageID
                                  select set).FirstOrDefault();

                const string tag = "<div class='focuspoint' id='{0}' data-focus-x='{1}' data-focus-y='{2}' data-image-w='{3}' data-image-h='{4}'><img src='{5}' alt='{6}' class='{7}' /></div>";

                if (imageQuery != null)
                {
                    return string.Format(tag,
                        frameClass, imageQuery.FocusPointX, imageQuery.FocusPointY,
                        imageQuery.Width, imageQuery.Height,
                        page.ResolveClientUrl(GetImageURL(imageQuery)),
                        altText, imageClass);
                }
                else
                {
                    return string.Format(tag, frameClass, 0, 0, 300, 300, page.ResolveClientUrl(AppConfig.NoImage), altText, imageClass);
                }
            }
        }

        public static string GetImageURL(int? imageID)
        {
            if (imageID == null)
            {
                return string.Empty;
            }

            using (var context = new OWDAROEntities())
            {
                var imageQuery = (from set in context.OW_Images
                                  where set.ImageID == imageID
                                  select set).FirstOrDefault();

                if (imageQuery != null)
                {
                    return GetImageURL(imageQuery);
                }
                else
                {
                    return AppConfig.NoImage;
                }
            }
        }

        public static string GetImageURL(OW_Images entity)
        {
            return GetImageURL(entity, null, null);
        }

        public static string GetImageURL(OW_Images entity, int? maxWidth, int? maxHeight)
        {
            if (entity.ShowWebImage)
            {
                if (!string.IsNullOrWhiteSpace(entity.WebImageURL))
                {
                    return GetWebImageURLCodified(entity, maxWidth, maxHeight);
                }
                else
                {
                    return AppConfig.NoImage.Remove(0, 1);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(entity.ImageURL))
                {
                    return GetImageURLCodified(entity, maxWidth, maxHeight).Remove(0, 1);
                }
                else
                {
                    return AppConfig.NoImage.Remove(0, 1);
                }
            }
        }

        public static string GetWebImageURLCodified(OW_Images entity)
        {
            return GetWebImageURLCodified(entity, null, null);
        }

        public static string GetWebImageURLCodified(OW_Images entity, int? maxWidth, int? maxHeight)
        {
            var maxWidthValue = entity.MaxWidth;
            var maxHeightValue = entity.MaxHeight;

            if (maxWidth != null)
            {
                maxWidthValue = (int)maxWidth;
            }

            if (maxHeight != null)
            {
                maxHeightValue = (int)maxHeight;
            }

            return string.Format("{0}?width={1}&height={2}&quality={3}&maxwidth={4}&maxheight={5}&cropxunits={6}&cropyunits={7}&crop=({8},{9},{10},{11})&mode={12}&scale={13}&404=default",
                                                            entity.WebImageURL,
                                                            entity.Width,
                                                            entity.Height,
                                                            entity.Quality,
                                                            maxWidthValue,
                                                            maxHeightValue,
                                                            entity.XUnit,
                                                            entity.YUnit,
                                                            entity.X1,
                                                            entity.Y1,
                                                            entity.X2,
                                                            entity.Y2,
                                                            entity.Mode,
                                                            entity.Scale);
        }

        public static string GetImageURLCodified(OW_Images entity)
        {
            return GetImageURLCodified(entity, null, null);
        }

        public static string GetImageURLCodified(OW_Images entity, int? maxWidth, int? maxHeight)
        {
            var maxWidthValue = entity.MaxWidth;
            var maxHeightValue = entity.MaxHeight;

            if (maxWidth != null)
            {
                maxWidthValue = (int)maxWidth;
            }

            if (maxHeight != null)
            {
                maxHeightValue = (int)maxHeight;
            }

            return string.Format("{0}?width={1}&height={2}&quality={3}&maxwidth={4}&maxheight={5}&cropxunits={6}&cropyunits={7}&crop=({8},{9},{10},{11})&mode={12}&scale={13}&404=default",
                                                            entity.ImageURL,
                                                            entity.Width,
                                                            entity.Height,
                                                            entity.Quality,
                                                            maxWidthValue,
                                                            maxHeightValue,
                                                            entity.XUnit,
                                                            entity.YUnit,
                                                            entity.X1,
                                                            entity.Y1,
                                                            entity.X2,
                                                            entity.Y2,
                                                            entity.Mode,
                                                            entity.Scale);
        }

        public static string GetIPAddress()
        {
            return GetIPAddress(HttpContext.Current);
        }

        public static string GetIPAddress(HttpContext context)
        {
            var ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                return ipAddress;
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static void GetPerformance<T>(PerformanceMode performanceMode, string performanceKey, out T keyValue, Delegate func, params object[] args)
        {
            keyValue = default(T);

            switch (performanceMode)
            {
                case PerformanceMode.ApplicationState:
                    if (ApplicationStateHelper.Exists(performanceKey))
                    {
                        ApplicationStateHelper.Get<T>(performanceKey, out keyValue);
                    }
                    else
                    {
                        keyValue = (T)func.DynamicInvoke(args);
                        ApplicationStateHelper.Add<T>(performanceKey, keyValue);
                    }
                    break;

                case PerformanceMode.Cache:
                    if (CacheHelper.Exists(performanceKey))
                    {
                        CacheHelper.Get<T>(performanceKey, out keyValue);
                    }
                    else
                    {
                        keyValue = (T)func.DynamicInvoke(args);
                        CacheHelper.Add<T>(performanceKey, keyValue);
                    }
                    break;

                case PerformanceMode.MemoryCache:
                    if (MemoryCacheHelper.Exists(performanceKey))
                    {
                        MemoryCacheHelper.Get<T>(performanceKey, out keyValue);
                    }
                    else
                    {
                        keyValue = (T)func.DynamicInvoke(args);
                        MemoryCacheHelper.Add<T>(performanceKey, keyValue);
                    }
                    break;

                case PerformanceMode.Session:
                    if (SessionHelper.Exists(performanceKey))
                    {
                        SessionHelper.Get<T>(performanceKey, out keyValue);
                    }
                    else
                    {
                        keyValue = (T)func.DynamicInvoke(args);
                        SessionHelper.Add<T>(performanceKey, keyValue);
                    }
                    break;

                case PerformanceMode.None:
                    keyValue = (T)func.DynamicInvoke(args);
                    break;
            }
        }

        public static string GetRandomString(int length)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();

            char ch;

            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString().ToLower();
        }

        public static int GetRandomNumber(int start, int end)
        {
            int number;

            var rand = new Random();
            number = rand.Next(start, end);

            return number;
        }

        public static void GetTagsSplitted(SortedDictionary<string, int> tagsDictionary, string fullTags)
        {
            var tags = fullTags.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tag in tags)
            {
                if (!string.IsNullOrWhiteSpace(tag))
                {
                    if (tagsDictionary.ContainsKey(tag))
                    {
                        tagsDictionary[tag]++;
                    }
                    else
                    {
                        tagsDictionary.Add(tag, 1);
                    }
                }
            }
        }

        public static bool IsFileSizeOK(Stream content, int maxSizeInMB)
        {
            var maxFileSizeBytes = (maxSizeInMB * 1024) * 1024;

            if (content.Length <= maxFileSizeBytes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void PopulateExternalFormTypes(DropDownListAdv dropdownList)
        {
            dropdownList.Items.Add(new ListItem("Select", "-1"));
            dropdownList.Items.Add(new ListItem("Form Embed Code", "1"));
            dropdownList.Items.Add(new ListItem("Form URL", "2"));
            dropdownList.Items.Add(new ListItem("Pre Built Forms", "3"));
        }

        public static void RegisterAsyncPostBackControl(Page page, Control control)
        {
            var currPageScriptManager = ScriptManager.GetCurrent(page) as ScriptManager;

            if (currPageScriptManager != null)
            {
                currPageScriptManager.RegisterAsyncPostBackControl(control);
            }
        }

        public static void SetPageCache(PageCache entity)
        {
            switch (entity.Location)
            {
                case "Both":
                case "Server":
                    var freshness = new TimeSpan(0, 0, 0, entity.Minutes);
                    var now = DateTime.Now;
                    HttpContext.Current.Response.Cache.SetExpires(now.Add(freshness));
                    HttpContext.Current.Response.Cache.SetMaxAge(freshness);
                    HttpContext.Current.Response.Cache.SetCacheability(entity.Location.ToCacheType());
                    HttpContext.Current.Response.Cache.SetValidUntilExpires(true);
                    break;

                case "Client":
                    HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddMinutes(entity.Minutes));
                    HttpContext.Current.Response.Cache.SetCacheability(entity.Location.ToCacheType());
                    break;

                case "None":
                default:
                    HttpContext.Current.Response.Cache.SetCacheability(entity.Location.ToCacheType());
                    break;
            }
        }

        public static void SetPageSEO(Page page, SEO seo)
        {
            var title = seo.Title;

            if (title.NullableContains("{ONLY-SITENAME}"))
            {
                title = title.Replace("{ONLY-SITENAME}", string.Empty);

                page.Title = string.Format("{0}", AppConfig.SiteName);
            }

            if (title.NullableContains("{ONLY-TITLE}"))
            {
                title = title.Replace("{ONLY-TITLE}", string.Empty);

                page.Title = string.Format("{0}", title);
            }

            if (!seo.Title.NullableContains("{ONLY-SITENAME}") && !seo.Title.NullableContains("{ONLY-TITLE}"))
            {
                page.Title = string.Format("{0} | {1}", seo.Title, AppConfig.SiteName);
            }

            page.MetaDescription = seo.Description;
            page.MetaKeywords = seo.Keywords;
        }
    }
}