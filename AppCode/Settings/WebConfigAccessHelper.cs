using OWDARO.Models;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace OWDARO.Settings
{
    public static class WebConfigAccessHelper
    {
        private readonly static string rootWebConfigFile = HttpRuntime.AppDomainAppPath + "web.config";

        private static XmlNodeList GetAccessTypeRuleNodes(PathAccess access, XmlNode locationNode)
        {
            var accessTypeRuleNodes = locationNode.SelectNodes("system.web/authorization/" + access.AccessType);
            return accessTypeRuleNodes;
        }

        private static XmlNode GetAuthNode(XmlNode locationNode)
        {
            var authNode = locationNode.SelectSingleNode("system.web/authorization");
            return authNode;
        }

        private static string GetLocationNodePathValue(XmlNode locationNode)
        {
            return locationNode.Attributes["path"].Value;
        }

        private static XmlNodeList GetLocationNodes(XmlDocument xmlDoc)
        {
            var locationNodes = xmlDoc.SelectNodes("/configuration/location");
            return locationNodes;
        }

        private static XmlDocument GetXMLDocument()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(rootWebConfigFile);

            return xmlDoc;
        }

        public static void AddPathAccessRules(string path, PathAccess[] accesses)
        {
            var doc = XDocument.Load(rootWebConfigFile);

            var locationElement = new XElement("location");
            locationElement.SetAttributeValue("path", path);

            var systemWebElement = new XElement("system.web");
            var authorizationElement = new XElement("authorization");

            Array.ForEach(accesses, access =>
            {
                var ruleElement = new XElement(access.AccessType);
                ruleElement.SetAttributeValue(access.AccessLevel, access.AccessTo);
                authorizationElement.Add(ruleElement);
            });

            systemWebElement.Add(authorizationElement);
            locationElement.Add(systemWebElement);

            doc.Descendants("configuration").Single().Add(locationElement);

            doc.Save(rootWebConfigFile);
        }

        public static bool AddRule(PathAccess access, bool prepend)
        {
            var success = false;

            if (IsPathLocationPresent(access.Path))
            {
                var xmlDoc = GetXMLDocument();

                var locationNodes = GetLocationNodes(xmlDoc);

                foreach (XmlNode locationNode in locationNodes)
                {
                    if (access.Path == GetLocationNodePathValue(locationNode))
                    {
                        var authNode = GetAuthNode(locationNode);

                        if (!IsRulePresent(access))
                        {
                            var newRule = xmlDoc.CreateElement(access.AccessType);
                            newRule.SetAttribute(access.AccessLevel, access.AccessTo);

                            if (prepend)
                            {
                                authNode.PrependChild(newRule);
                            }
                            else
                            {
                                authNode.AppendChild(newRule);
                            }

                            xmlDoc.Save(rootWebConfigFile);

                            success = true;
                        }
                    }
                }
            }

            return success;
        }

        public static bool AddRuleAt(PathAccess access, int position)
        {
            var success = false;

            if (IsPathLocationPresent(access.Path))
            {
                var xmlDoc = GetXMLDocument();

                var locationNodes = GetLocationNodes(xmlDoc);
                foreach (XmlNode locationNode in locationNodes)
                {
                    if (access.Path == GetLocationNodePathValue(locationNode))
                    {
                        var authNode = GetAuthNode(locationNode);

                        if (!IsRulePresent(access))
                        {
                            var newRule = xmlDoc.CreateElement(access.AccessType);
                            newRule.SetAttribute(access.AccessLevel, access.AccessTo);

                            var ruleNodes = authNode.ChildNodes;

                            var ruleNode = ruleNodes.Item(Math.Min(position, ruleNodes.Count));

                            authNode.InsertBefore(newRule, ruleNode);

                            xmlDoc.Save(rootWebConfigFile);

                            success = true;
                        }
                    }
                }
            }

            return success;
        }

        public static bool CreatePathLocation(string path)
        {
            var success = false;

            if (!IsPathLocationPresent(path))
            {
                var doc = XDocument.Load(rootWebConfigFile);

                var locationElement = new XElement("location");
                locationElement.SetAttributeValue("path", path);

                var systemWebElement = new XElement("system.web");
                var authorizationElement = new XElement("authorization");

                systemWebElement.Add(authorizationElement);
                locationElement.Add(systemWebElement);

                doc.Descendants("configuration").Single().Add(locationElement);

                try
                {
                    doc.Save(rootWebConfigFile);
                }
                catch
                {
                    success = true;
                }
            }

            return success;
        }

        public static string GetPathLocationXML(string path)
        {
            var xml = string.Empty;

            var xmlDoc = GetXMLDocument();

            var locationNodes = GetLocationNodes(xmlDoc);

            foreach (XmlNode locationNode in locationNodes)
            {
                if (path == GetLocationNodePathValue(locationNode))
                {
                    xml = locationNode.OuterXml;

                    var newDoc = new XmlDocument();
                    newDoc.LoadXml(xml);

                    var sb = new StringBuilder();

                    var settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.Encoding = Encoding.UTF8;
                    settings.OmitXmlDeclaration = true;

                    var writer = XmlWriter.Create(sb, settings);

                    newDoc.WriteTo(writer);

                    writer.Close();

                    xml = sb.ToString();

                    break;
                }
            }

            return xml;
        }

        public static DataTable GetRulesForPath(string path)
        {
            var table = new DataTable();

            table.Columns.Add("Position");
            table.Columns.Add("AccessType");
            table.Columns.Add("AccessLevel");
            table.Columns.Add("AccessTo");

            DataRow row;

            if (IsPathLocationPresent(path))
            {
                var xmlDoc = GetXMLDocument();

                var locationNodes = GetLocationNodes(xmlDoc);

                foreach (XmlNode locationNode in locationNodes)
                {
                    if (path == GetLocationNodePathValue(locationNode))
                    {
                        var authNode = GetAuthNode(locationNode);

                        var ruleNodes = authNode.ChildNodes;

                        var position = 1;

                        foreach (XmlNode ruleNode in ruleNodes)
                        {
                            row = table.NewRow();

                            var accessLevel = string.Empty;

                            var attributes = ruleNode.Attributes;

                            foreach (XmlAttribute attribute in attributes)
                            {
                                if (attribute.Name == "roles" || attribute.Name == "users")
                                {
                                    accessLevel = attribute.Name;
                                }
                            }

                            row["Position"] = position++;
                            row["AccessType"] = ruleNode.LocalName;
                            row["AccessLevel"] = accessLevel;
                            row["AccessTo"] = ruleNode.Attributes[accessLevel].Value;

                            table.Rows.Add(row);
                        }
                    }
                }
            }

            return table;
        }

        public static bool IsPathLocationPresent(string path)
        {
            var present = false;

            var xmlDoc = GetXMLDocument();

            var locationNodes = GetLocationNodes(xmlDoc);

            foreach (XmlNode locationNode in locationNodes)
            {
                if (path == GetLocationNodePathValue(locationNode))
                {
                    present = true;
                    break;
                }
            }

            return present;
        }

        public static bool IsRulePresent(PathAccess access)
        {
            var present = false;

            var xmlDoc = GetXMLDocument();

            var locationNodes = GetLocationNodes(xmlDoc);

            foreach (XmlNode locationNode in locationNodes)
            {
                if (access.Path == GetLocationNodePathValue(locationNode))
                {
                    var accessTypeRuleNodes = GetAccessTypeRuleNodes(access, locationNode);

                    foreach (XmlNode accessTypeRuleNode in accessTypeRuleNodes)
                    {
                        var attributes = accessTypeRuleNode.Attributes;

                        foreach (XmlAttribute attribute in attributes)
                        {
                            if (attribute.Name == access.AccessLevel)
                            {
                                if (access.AccessTo == attribute.Value)
                                {
                                    present = true;
                                }
                            }
                        }
                    }
                }
            }

            return present;
        }

        public static bool RemoveRule(PathAccess access)
        {
            var success = false;

            if (IsRulePresent(access))
            {
                var xmlDoc = GetXMLDocument();

                var locationNodes = GetLocationNodes(xmlDoc);

                foreach (XmlNode locationNode in locationNodes)
                {
                    if (access.Path == GetLocationNodePathValue(locationNode))
                    {
                        var accessTypeRuleNodes = GetAccessTypeRuleNodes(access, locationNode);

                        foreach (XmlNode accessTypeRuleNode in accessTypeRuleNodes)
                        {
                            var attributes = accessTypeRuleNode.Attributes;

                            foreach (XmlAttribute attribute in attributes)
                            {
                                if (attribute.Name == access.AccessLevel)
                                {
                                    if (access.AccessTo == attribute.Value)
                                    {
                                        accessTypeRuleNode.ParentNode.RemoveChild(accessTypeRuleNode);

                                        xmlDoc.Save(rootWebConfigFile);
                                        success = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return success;
        }
    }
}