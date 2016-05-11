using OWDARO.Models;
using OWDARO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace OWDARO.Settings
{
    public static class EventTypesHelper
    {
        private const string xPath = "eventTypes/eventType";

        private readonly static string fileName = AppConfig.EventTypesFile;
        private readonly static string uniqueKey = "_EventTypesKey_";

        private static void SaveXml(XmlDocument xmlDoc)
        {
            var xmlTextWriter = new XmlTextWriter(fileName, null);
            xmlTextWriter.Formatting = Formatting.Indented;
            xmlDoc.WriteContentTo(xmlTextWriter);
            xmlTextWriter.Close();
        }

        public static void Add(EventTypes entity)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var newKey = xmlDoc.CreateElement("key");

            newKey.SetAttribute("id", GetNewPrimaryKey(entity).ToString());
            newKey.SetAttribute("title", entity.Title);
            newKey.SetAttribute("description", entity.Description);
            newKey.SetAttribute("hide", entity.Hide.ToString());
            newKey.SetAttribute("imageUrl", entity.ImageURL);
            newKey.SetAttribute("imageThumbUrl", entity.ImageThumbURL);
            newKey.SetAttribute("isRegisterable", entity.IsRegisterable.ToString());
            newKey.SetAttribute("registrationPageUrl", entity.RegistrationPageURL);
            newKey.SetAttribute("useExternalForm", entity.UseExternalForm.ToString());
            newKey.SetAttribute("popUpExternalForm", entity.PopUpExternalForm.ToString());
            newKey.SetAttribute("externalFormEmbedCode", entity.ExternalFormEmbedCode);
            newKey.SetAttribute("externalFormURL", entity.ExternalFormURL);
            newKey.SetAttribute("externalFormID", entity.ExternalFormID.ToString());

            xmlDoc.SelectSingleNode(xPath).ParentNode.AppendChild(newKey);

            SaveXml(xmlDoc);
        }

        public static void Delete(int id)
        {
            if (Exists(id))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var keys = xmlDoc.SelectNodes(xPath);

                foreach (XmlNode key in keys)
                {
                    if (id == DataParser.IntParse(key.Attributes["id"].Value))
                    {
                        key.ParentNode.RemoveChild(key);

                        SaveXml(xmlDoc);

                        break;
                    }
                }
            }
        }

        public static bool Exists(int id)
        {
            var present = false;

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var keys = xmlDoc.SelectNodes(xPath);

            foreach (XmlNode key in keys)
            {
                if (id == DataParser.IntParse(key.Attributes["id"].Value))
                {
                    present = true;
                    break;
                }
            }

            return present;
        }

        public static EventTypes Get(int id)
        {
            return Get(id, AppConfig.PerformanceMode);
        }

        public static EventTypes Get(int id, PerformanceMode performanceMode)
        {
            var keyValue = new EventTypes();
            var performanceKey = String.Format("{0}_{1}", uniqueKey, id);

            Func<int, EventTypes> fnc = GetFromSettings;

            var args = new object[] { id };

            Utilities.GetPerformance<EventTypes>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static List<EventTypes> GetEvents()
        {
            var eventsList = new List<EventTypes>();

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var keys = xmlDoc.SelectNodes(xPath);

            EventTypes entity;

            foreach (XmlNode key in keys)
            {
                entity = new EventTypes();

                entity.EventTypeID = DataParser.IntParse(key.Attributes["id"].Value);
                entity.Description = key.Attributes["description"].Value;
                entity.Hide = DataParser.BoolParse(key.Attributes["hide"].Value);
                entity.ImageThumbURL = key.Attributes["imageThumbUrl"].Value;
                entity.ImageURL = key.Attributes["imageUrl"].Value;
                entity.IsRegisterable = DataParser.BoolParse(key.Attributes["isRegisterable"].Value);
                entity.RegistrationPageURL = key.Attributes["registrationPageUrl"].Value;
                entity.Title = key.Attributes["title"].Value;
                entity.UseExternalForm = DataParser.BoolParse(key.Attributes["useExternalForm"].Value);
                entity.PopUpExternalForm = DataParser.BoolParse(key.Attributes["popUpExternalForm"].Value);
                entity.ExternalFormEmbedCode = key.Attributes["externalFormEmbedCode"].Value;
                entity.ExternalFormURL = key.Attributes["externalFormURL"].Value;
                entity.ExternalFormID = DataParser.NullableIntParse(key.Attributes["externalFormID"].Value);

                eventsList.Add(entity);
            }

            return eventsList;
        }

        public static EventTypes GetFromSettings(int id)
        {
            var entity = new EventTypes();

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var keys = xmlDoc.SelectNodes(xPath);

            foreach (XmlNode key in keys)
            {
                if (id == DataParser.IntParse(key.Attributes["id"].Value))
                {
                    entity.EventTypeID = id;
                    entity.Description = key.Attributes["description"].Value;
                    entity.Hide = DataParser.BoolParse(key.Attributes["hide"].Value);
                    entity.ImageThumbURL = key.Attributes["imageThumbUrl"].Value;
                    entity.ImageURL = key.Attributes["imageUrl"].Value;
                    entity.IsRegisterable = DataParser.BoolParse(key.Attributes["isRegisterable"].Value);
                    entity.RegistrationPageURL = key.Attributes["registrationPageUrl"].Value;
                    entity.Title = key.Attributes["title"].Value;
                    entity.UseExternalForm = DataParser.BoolParse(key.Attributes["useExternalForm"].Value);
                    entity.PopUpExternalForm = DataParser.BoolParse(key.Attributes["popUpExternalForm"].Value);
                    entity.ExternalFormEmbedCode = key.Attributes["externalFormEmbedCode"].Value;
                    entity.ExternalFormURL = key.Attributes["externalFormURL"].Value;
                    entity.ExternalFormID = DataParser.NullableIntParse(key.Attributes["externalFormID"].Value);

                    break;
                }
            }

            return entity;
        }

        public static int GetNewPrimaryKey(EventTypes entity)
        {
            var id = entity.EventTypeID;

            if (entity.EventTypeID == 0)
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var keys = xmlDoc.SelectNodes(xPath);

                var ids = new int[keys.Count];
                var index = 0;

                foreach (XmlNode key in keys)
                {
                    ids[index] = DataParser.IntParse(key.Attributes["id"].Value);
                    index++;
                }

                id = ids.Max();
                id++;
            }

            return id;
        }

        public static void Set(EventTypes entity)
        {
            if (Exists(entity.EventTypeID))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var keys = xmlDoc.SelectNodes(xPath);

                foreach (XmlNode key in keys)
                {
                    if (entity.EventTypeID == DataParser.IntParse(key.Attributes["id"].Value))
                    {
                        key.Attributes["id"].Value = entity.EventTypeID.ToString();
                        key.Attributes["title"].Value = entity.Title;
                        key.Attributes["description"].Value = entity.Description;
                        key.Attributes["hide"].Value = entity.Hide.ToString();
                        key.Attributes["imageUrl"].Value = entity.ImageURL;
                        key.Attributes["imageThumbUrl"].Value = entity.ImageThumbURL;
                        key.Attributes["isRegisterable"].Value = entity.IsRegisterable.ToString();
                        key.Attributes["registrationPageUrl"].Value = entity.RegistrationPageURL;
                        key.Attributes["useExternalForm"].Value = entity.UseExternalForm.ToString();
                        key.Attributes["popUpExternalForm"].Value = entity.PopUpExternalForm.ToString();
                        key.Attributes["externalFormEmbedCode"].Value = entity.ExternalFormEmbedCode;
                        key.Attributes["externalFormURL"].Value = entity.ExternalFormURL;
                        key.Attributes["externalFormID"].Value = entity.ExternalFormID.ToString();

                        SaveXml(xmlDoc);

                        break;
                    }
                }
            }
        }
    }
}