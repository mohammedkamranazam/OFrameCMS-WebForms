using OWDARO.Models;
using OWDARO.Util;
using System;
using System.Xml;

namespace OWDARO.Settings
{
    public static class PageCacheHelper
    {
        private const string uniqueKey = "_PageCacheHelper_";
        private const string xPath = "pages/page";

        private readonly static string fileName = AppConfig.PageCacheFile;

        private static void SaveXml(XmlDocument xmlDoc)
        {
            var xmlTextWriter = new XmlTextWriter(fileName, null);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlDoc.WriteContentTo(xmlTextWriter);
            xmlTextWriter.Close();
        }

        public static void AddCache(PageCache entity)
        {
            if (!IsPagePresent(entity.ID))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var newPage = xmlDoc.CreateElement("page");

                newPage.SetAttribute("id", entity.ID);
                newPage.SetAttribute("duration", entity.Minutes.ToString());
                newPage.SetAttribute("location", entity.Location);

                xmlDoc.SelectSingleNode(xPath).ParentNode.AppendChild(newPage);

                SaveXml(xmlDoc);
            }
        }

        public static void DeleteKeyword(string id)
        {
            if (IsPagePresent(id))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var pages = xmlDoc.SelectNodes(xPath);

                foreach (XmlNode page in pages)
                {
                    if (id == page.Attributes["id"].Value)
                    {
                        page.ParentNode.RemoveChild(page);

                        SaveXml(xmlDoc);

                        break;
                    }
                }
            }
        }

        public static PageCache GetCache(string id)
        {
            return GetKeywordValue(id, AppConfig.PerformanceMode);
        }

        public static PageCache GetCacheFromSettings(string id)
        {
            var entity = new PageCache();

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var pages = xmlDoc.SelectNodes(xPath);

            foreach (XmlNode page in pages)
            {
                if (id == page.Attributes["id"].Value)
                {
                    entity.ID = page.Attributes["id"].Value;
                    entity.Location = page.Attributes["location"].Value;
                    entity.Minutes = page.Attributes["duration"].Value.IntParse();
                    break;
                }
            }

            return entity;
        }

        public static PageCache GetKeywordValue(string id, PerformanceMode performanceMode)
        {
            var keyValue = new PageCache();
            var performanceKey = uniqueKey + id;

            Func<string, PageCache> fnc = GetCacheFromSettings;

            var args = new object[] { id };

            Utilities.GetPerformance<PageCache>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static bool IsPagePresent(string id)
        {
            var present = false;

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var pages = xmlDoc.SelectNodes(xPath);

            foreach (XmlNode page in pages)
            {
                if (id == page.Attributes["id"].Value)
                {
                    present = true;
                    break;
                }
            }

            return present;
        }

        public static void SetKeywordValue(PageCache entity)
        {
            if (IsPagePresent(entity.ID))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var pages = xmlDoc.SelectNodes(xPath);

                foreach (XmlNode page in pages)
                {
                    if (entity.ID == page.Attributes["id"].Value)
                    {
                        page.Attributes["location"].Value = entity.Location;
                        page.Attributes["duration"].Value = entity.Minutes.ToString();

                        SaveXml(xmlDoc);

                        break;
                    }
                }
            }
        }
    }
}