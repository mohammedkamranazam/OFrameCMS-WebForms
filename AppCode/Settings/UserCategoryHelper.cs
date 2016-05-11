using OWDARO.Models;
using OWDARO.Util;
using System;
using System.Xml;

namespace OWDARO.Settings
{
    public static class UserCategoryHelper
    {
        private const string uniqueKey = "_UserCategoryHelper_";

        private readonly static string fileName = AppConfig.PageSettingsFile;

        private static string GetXPath(PageSetting pageSetting)
        {
            switch (pageSetting)
            {
                case PageSetting.Add:
                    return "pageSetting/add/pages/page/categories/category";

                case PageSetting.List:
                    return "pageSetting/list/pages/page/categories/category";

                case PageSetting.Manage:
                    return "pageSetting/manage/pages/page/categories/category";

                default:
                    return "";
            }
        }

        private static void SaveXml(XmlDocument xmlDoc)
        {
            var xmlTextWriter = new XmlTextWriter(fileName, null);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlDoc.WriteContentTo(xmlTextWriter);
            xmlTextWriter.Close();
        }

        public static void AddCategorySetting(UserCategorySettings categorySettings, PageSetting pageSetting)
        {
            if (!IsCategorySettingsPresent(categorySettings.CategoryID, pageSetting))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var newSetting = xmlDoc.CreateElement("category");

                newSetting.SetAttribute("id", categorySettings.CategoryID.ToString());
                newSetting.SetAttribute("name", categorySettings.Name);
                newSetting.SetAttribute("locked", categorySettings.Locked.ToString());
                newSetting.SetAttribute("showEducation", categorySettings.ShowEducation.ToString());
                newSetting.SetAttribute("showWork", categorySettings.ShowWork.ToString());
                newSetting.SetAttribute("showAddress", categorySettings.ShowAddress.ToString());
                newSetting.SetAttribute("showBillingAddress", categorySettings.ShowBillingAddress.ToString());
                newSetting.SetAttribute("showDeliveryAddress", categorySettings.ShowDeliveryAddress.ToString());
                newSetting.SetAttribute("showDateOfBirth", categorySettings.ShowDateOfBirth.ToString());
                newSetting.SetAttribute("showFax", categorySettings.ShowFax.ToString());
                newSetting.SetAttribute("showGender", categorySettings.ShowGender.ToString());
                newSetting.SetAttribute("showLandline", categorySettings.ShowLandline.ToString());
                newSetting.SetAttribute("showMobile", categorySettings.ShowMobile.ToString());
                newSetting.SetAttribute("showWebsite", categorySettings.ShowWebsite.ToString());

                xmlDoc.SelectSingleNode(GetXPath(pageSetting)).ParentNode.AppendChild(newSetting);

                SaveXml(xmlDoc);
            }
        }

        public static void DeleteCategorySetting(int categoryID, PageSetting pageSetting)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var categories = xmlDoc.SelectNodes(GetXPath(pageSetting));

            foreach (XmlNode category in categories)
            {
                if (categoryID == DataParser.IntParse(category.Attributes["id"].Value))
                {
                    category.ParentNode.RemoveChild(category);

                    SaveXml(xmlDoc);

                    break;
                }
            }
        }

        public static UserCategorySettings GetCategorySetting(int categoryID, PageSetting pageSetting)
        {
            return GetCategorySetting(categoryID, pageSetting, AppConfig.PerformanceMode);
        }

        public static UserCategorySettings GetCategorySetting(int categoryID, PageSetting pageSetting, PerformanceMode performanceMode)
        {
            var keyValue = new UserCategorySettings();
            var performanceKey = String.Format("{0}{1}_{2}", uniqueKey, categoryID, (int)pageSetting);

            Func<int, PageSetting, UserCategorySettings> fnc = GetCategorySettingFromSettings;

            var args = new object[] { categoryID, pageSetting };

            Utilities.GetPerformance<UserCategorySettings>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static UserCategorySettings GetCategorySettingFromSettings(int categoryID, PageSetting pageSetting)
        {
            var categorySettings = new UserCategorySettings();

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var categoryNodes = xmlDoc.SelectNodes(GetXPath(pageSetting));

            foreach (XmlNode categoryNode in categoryNodes)
            {
                if (categoryID == DataParser.IntParse(categoryNode.Attributes["id"].Value))
                {
                    categorySettings.CategoryID = categoryID;
                    categorySettings.Name = categoryNode.Attributes["name"].Value;
                    categorySettings.Locked = DataParser.BoolParse(categoryNode.Attributes["locked"].Value);
                    categorySettings.ShowEducation = DataParser.BoolParse(categoryNode.Attributes["showEducation"].Value);
                    categorySettings.ShowWork = DataParser.BoolParse(categoryNode.Attributes["showWork"].Value);
                    categorySettings.ShowDateOfBirth = DataParser.BoolParse(categoryNode.Attributes["showDateOfBirth"].Value);
                    categorySettings.ShowAddress = DataParser.BoolParse(categoryNode.Attributes["showAddress"].Value);
                    categorySettings.ShowBillingAddress = DataParser.BoolParse(categoryNode.Attributes["showBillingAddress"].Value);
                    categorySettings.ShowDeliveryAddress = DataParser.BoolParse(categoryNode.Attributes["showDeliveryAddress"].Value);
                    categorySettings.ShowFax = DataParser.BoolParse(categoryNode.Attributes["showFax"].Value);
                    categorySettings.ShowGender = DataParser.BoolParse(categoryNode.Attributes["showGender"].Value);
                    categorySettings.ShowLandline = DataParser.BoolParse(categoryNode.Attributes["showLandline"].Value);
                    categorySettings.ShowMobile = DataParser.BoolParse(categoryNode.Attributes["showMobile"].Value);
                    categorySettings.ShowWebsite = DataParser.BoolParse(categoryNode.Attributes["showWebsite"].Value);
                    break;
                }
            }

            return categorySettings;
        }

        public static int? GetDefaultCategoryID()
        {
            var categoryID = GetDefaultCategoryID(AppConfig.PerformanceMode);

            if (categoryID == -1)
            {
                return null;
            }
            else
            {
                return categoryID;
            }
        }

        public static int GetDefaultCategoryID(PerformanceMode performanceMode)
        {
            var keyValue = -1;
            var performanceKey = uniqueKey + "DefaultCategoryID";

            Func<int> fnc = GetDefaultCategoryIDFromSettings;

            var args = new object[] { };

            Utilities.GetPerformance<int>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static int GetDefaultCategoryIDFromSettings()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(GetXPath(PageSetting.Add)).ParentNode;

            return DataParser.IntParse(rootNode.Attributes["default"].Value);
        }

        public static bool IsCategorySettingsPresent(int categoryID, PageSetting pageSetting)
        {
            var present = false;

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var categoryNodes = xmlDoc.SelectNodes(GetXPath(pageSetting));

            foreach (XmlNode categoryNode in categoryNodes)
            {
                if (categoryID == DataParser.IntParse(categoryNode.Attributes["id"].Value))
                {
                    present = true;
                    break;
                }
            }

            return present;
        }

        public static void SetCategorySetting(UserCategorySettings categorySettings, PageSetting pageSetting)
        {
            if (IsCategorySettingsPresent(categorySettings.CategoryID, pageSetting))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var categoryNodes = xmlDoc.SelectNodes(GetXPath(pageSetting));

                foreach (XmlNode categoryNode in categoryNodes)
                {
                    if (categorySettings.CategoryID == DataParser.IntParse(categoryNode.Attributes["id"].Value))
                    {
                        categoryNode.Attributes["name"].Value = categorySettings.Name;
                        categoryNode.Attributes["locked"].Value = categorySettings.Locked.ToString();
                        categoryNode.Attributes["showEducation"].Value = categorySettings.ShowEducation.ToString();
                        categoryNode.Attributes["showWork"].Value = categorySettings.ShowWork.ToString();
                        categoryNode.Attributes["showAddress"].Value = categorySettings.ShowAddress.ToString();
                        categoryNode.Attributes["showBillingAddress"].Value = categorySettings.ShowBillingAddress.ToString();
                        categoryNode.Attributes["showDeliveryAddress"].Value = categorySettings.ShowDeliveryAddress.ToString();
                        categoryNode.Attributes["showDateOfBirth"].Value = categorySettings.ShowDateOfBirth.ToString();
                        categoryNode.Attributes["showFax"].Value = categorySettings.ShowFax.ToString();
                        categoryNode.Attributes["showGender"].Value = categorySettings.ShowGender.ToString();
                        categoryNode.Attributes["showLandline"].Value = categorySettings.ShowLandline.ToString();
                        categoryNode.Attributes["showMobile"].Value = categorySettings.ShowMobile.ToString();
                        categoryNode.Attributes["showWebsite"].Value = categorySettings.ShowWebsite.ToString();

                        SaveXml(xmlDoc);

                        break;
                    }
                }
            }
        }

        public static void SetDefaultCategoryID(int categoryID)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(GetXPath(PageSetting.Add)).ParentNode;

            rootNode.Attributes["default"].Value = categoryID.ToString();

            SaveXml(xmlDoc);
        }
    }
}