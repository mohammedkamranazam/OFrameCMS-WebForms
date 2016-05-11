using OWDARO.Models;
using OWDARO.Util;
using System;
using System.Xml;

namespace OWDARO.Settings
{
    public static class UserDataCategoriesHelper
    {
        private const string expressionXPath = "userDataCategories/userDataCategory";
        private const string uniqueKey = "_UserDataCategories__";

        private readonly static string fileName = AppConfig.UserDataCategoriesFile;

        private static void SaveXml(XmlDocument xmlDoc)
        {
            var xmlTextWriter = new XmlTextWriter(fileName, null);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlDoc.WriteContentTo(xmlTextWriter);
            xmlTextWriter.Close();
        }

        public static void Add(UserDataCategory dataCategory)
        {
            if (!Exists(dataCategory.Name))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var newCategory = xmlDoc.CreateElement("userDataCategory");

                newCategory.SetAttribute("name", dataCategory.Name);
                newCategory.SetAttribute("value", dataCategory.Value);

                xmlDoc.SelectSingleNode(expressionXPath).ParentNode.AppendChild(newCategory);

                SaveXml(xmlDoc);
            }
        }

        public static void Delete(string name)
        {
            if (Exists(name))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var expressions = xmlDoc.SelectNodes(expressionXPath);

                foreach (XmlNode expression in expressions)
                {
                    if (name == expression.Attributes["name"].Value)
                    {
                        expression.ParentNode.RemoveChild(expression);

                        SaveXml(xmlDoc);

                        break;
                    }
                }
            }
        }

        public static bool Exists(string name)
        {
            var present = false;

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var expressions = xmlDoc.SelectNodes(expressionXPath);

            foreach (XmlNode expression in expressions)
            {
                if (name == expression.Attributes["name"].Value)
                {
                    present = true;
                    break;
                }
            }

            return present;
        }

        public static UserDataCategory GetDataCategory(string name)
        {
            return GetDataCategory(name, AppConfig.PerformanceMode);
        }

        public static UserDataCategory GetDataCategory(string name, PerformanceMode performanceMode)
        {
            var dataCategory = new UserDataCategory();

            var performanceKey = uniqueKey + name;

            Func<string, UserDataCategory> fnc = GetDataCategoryFromSettings;

            var args = new object[] { name };

            Utilities.GetPerformance<UserDataCategory>(performanceMode, performanceKey, out dataCategory, fnc, args);

            return dataCategory;
        }

        public static UserDataCategory GetDataCategoryFromSettings(string name)
        {
            var dataCategory = new UserDataCategory();

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var expressions = xmlDoc.SelectNodes(expressionXPath);

            foreach (XmlNode expression in expressions)
            {
                if (name == expression.Attributes["name"].Value)
                {
                    dataCategory.Name = expression.Attributes["name"].Value;
                    dataCategory.Value = expression.Attributes["value"].Value;
                    break;
                }
            }

            return dataCategory;
        }

        public static void SetDataCategory(UserDataCategory dataCategory)
        {
            if (Exists(dataCategory.Name))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var expressions = xmlDoc.SelectNodes(expressionXPath);

                foreach (XmlNode expression in expressions)
                {
                    if (dataCategory.Name == expression.Attributes["name"].Value)
                    {
                        expression.Attributes["value"].Value = dataCategory.Value;

                        SaveXml(xmlDoc);

                        break;
                    }
                }
            }
        }
    }
}