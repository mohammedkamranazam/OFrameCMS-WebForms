using OWDARO.BLL.MembershipBLL;
using OWDARO.Models;
using OWDARO.Util;
using System;
using System.Xml;

namespace OWDARO.Settings
{
    public static class UserRoleHelper
    {
        private const string rolesXPath = "roleSetting/roles";
        private const string roleXPath = "roleSetting/roles/role";
        private const string uniqueKey = "_UserRoleHelper_";

        private readonly static string fileName = AppConfig.RoleSettingsFile;

        private static void SaveXml(XmlDocument xmlDoc)
        {
            var TR = new XmlTextWriter(fileName, null);
            TR.Formatting = Formatting.Indented;
            xmlDoc.WriteContentTo(TR);
            TR.Close();
        }

        public static void AddRoleSetting(UserRoleSettings roleSetting)
        {
            if (!IsRoleSettingsPresent(roleSetting.Name))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var newSetting = xmlDoc.CreateElement("role");

                newSetting.SetAttribute("name", roleSetting.Name);
                newSetting.SetAttribute("path", roleSetting.Path);
                newSetting.SetAttribute("masterPage", roleSetting.MasterPage);
                newSetting.SetAttribute("locked", roleSetting.Locked.ToString());
                newSetting.SetAttribute("showCategory", roleSetting.ShowCategory.ToString());
                newSetting.SetAttribute("showRoles", roleSetting.ShowRoles.ToString());
                newSetting.SetAttribute("hideSuperAdmin", roleSetting.HideSuperAdmin.ToString());
                newSetting.SetAttribute("registrationBlocked", roleSetting.RegistrationBlocked.ToString());
                newSetting.SetAttribute("login", roleSetting.Login.ToString());
                newSetting.SetAttribute("theme", roleSetting.Theme);

                xmlDoc.SelectSingleNode(roleXPath).ParentNode.AppendChild(newSetting);

                SaveXml(xmlDoc);
            }
        }

        public static void DeleteRoleSetting(string role)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var roleNodes = xmlDoc.SelectNodes(roleXPath);

            foreach (XmlNode roleNode in roleNodes)
            {
                if (role == roleNode.Attributes["name"].Value)
                {
                    roleNode.ParentNode.RemoveChild(roleNode);

                    SaveXml(xmlDoc);

                    break;
                }
            }
        }

        public static string GetAdminRole()
        {
            return GetAdminRole(AppConfig.PerformanceMode);
        }

        public static string GetAdminRole(PerformanceMode performanceMode)
        {
            var keyValue = string.Empty;
            var performanceKey = uniqueKey + "AdminRole";

            Func<string> fnc = GetAdminRoleFromSettings;

            var args = new object[] { };

            Utilities.GetPerformance<string>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static string GetAdminRoleFromSettings()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            return rootNode.Attributes["admin"].Value;
        }

        public static string GetAnonymousRole()
        {
            return GetAnonymousRole(AppConfig.PerformanceMode);
        }

        public static string GetAnonymousRole(PerformanceMode performanceMode)
        {
            var keyValue = string.Empty;
            var performanceKey = uniqueKey + "AnonymousRole";

            Func<string> fnc = GetAnonymousRoleFromSettings;

            var args = new object[] { };

            Utilities.GetPerformance<string>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static string GetAnonymousRoleFromSettings()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            return rootNode.Attributes["anonymous"].Value;
        }

        public static string GetDefaultRoleName()
        {
            return GetDefaultRoleName(AppConfig.PerformanceMode);
        }

        public static string GetDefaultRoleName(PerformanceMode performanceMode)
        {
            var keyValue = string.Empty;
            var performanceKey = uniqueKey + "DefaultRoleName";

            Func<string> fnc = GetDefaultRoleNameFromSettings;

            var args = new object[] { };

            Utilities.GetPerformance<string>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static string GetDefaultRoleNameFromSettings()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            return rootNode.Attributes["default"].Value;
        }

        public static string GetDefaultRolePath()
        {
            return GetDefaultRolePath(AppConfig.PerformanceMode);
        }

        public static string GetDefaultRolePath(PerformanceMode performanceMode)
        {
            var keyValue = string.Empty;
            var performanceKey = uniqueKey + "DefaultRolePath";

            Func<string> fnc = GetDefaultRolePathFromSettings;

            var args = new object[] { };

            Utilities.GetPerformance<string>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static string GetDefaultRolePathFromSettings()
        {
            return GetRoleSetting(GetDefaultRoleName()).Path;
        }

        public static string GetInactivatedRole()
        {
            return GetInactivatedRole(AppConfig.PerformanceMode);
        }

        public static string GetInactivatedRole(PerformanceMode performanceMode)
        {
            var keyValue = string.Empty;
            var performanceKey = uniqueKey + "InactivatedRole";

            Func<string> fnc = GetInactivatedRoleFromSettings;

            var args = new object[] { };

            Utilities.GetPerformance<string>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static string GetInactivatedRoleFromSettings()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            return rootNode.Attributes["inactivated"].Value;
        }

        public static string GetRoleMasterPage()
        {
            return GetRoleMasterPage(UserBL.GetUserRole());
        }

        public static string GetRoleMasterPage(string role)
        {
            return GetRoleSetting(role).MasterPage;
        }

        public static UserRoleSettings GetRoleSetting(string role)
        {
            return GetRoleSetting(role, AppConfig.PerformanceMode);
        }

        public static UserRoleSettings GetRoleSetting(string role, PerformanceMode performanceMode)
        {
            var keyValue = new UserRoleSettings();
            var performanceKey = uniqueKey + role;

            Func<string, UserRoleSettings> fnc = GetRoleSettingFromFile;

            var args = new object[] { role };

            Utilities.GetPerformance<UserRoleSettings>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static UserRoleSettings GetRoleSettingFromFile(string role)
        {
            var roleSetting = new UserRoleSettings();

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var roleNodes = xmlDoc.SelectNodes(roleXPath);

            foreach (XmlNode roleNode in roleNodes)
            {
                if (role == roleNode.Attributes["name"].Value)
                {
                    roleSetting.Name = role;
                    roleSetting.Path = roleNode.Attributes["path"].Value;
                    roleSetting.Locked = DataParser.BoolParse(roleNode.Attributes["locked"].Value);
                    roleSetting.ShowCategory = DataParser.BoolParse(roleNode.Attributes["showCategory"].Value);
                    roleSetting.ShowRoles = DataParser.BoolParse(roleNode.Attributes["showRoles"].Value);
                    roleSetting.HideSuperAdmin = DataParser.BoolParse(roleNode.Attributes["hideSuperAdmin"].Value);
                    roleSetting.RegistrationBlocked = DataParser.BoolParse(roleNode.Attributes["registrationBlocked"].Value);
                    roleSetting.Login = DataParser.BoolParse(roleNode.Attributes["login"].Value);
                    roleSetting.MasterPage = roleNode.Attributes["masterPage"].Value;
                    roleSetting.Theme = roleNode.Attributes["theme"].Value;

                    break;
                }
            }

            return roleSetting;
        }

        public static string GetSuperAdminRole()
        {
            return GetSuperAdminRole(AppConfig.PerformanceMode);
        }

        public static string GetSuperAdminRole(PerformanceMode performanceMode)
        {
            var keyValue = string.Empty;
            var performanceKey = uniqueKey + "SuperAdminRole";

            Func<string> fnc = GetSuperAdminRoleFromSettings;

            var args = new object[] { };

            Utilities.GetPerformance<string>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static string GetSuperAdminRoleFromSettings()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            return rootNode.Attributes["superAdmin"].Value;
        }

        public static string GetUserRole()
        {
            return GetUserRole(AppConfig.PerformanceMode);
        }

        public static string GetUserRole(PerformanceMode performanceMode)
        {
            var keyValue = string.Empty;
            var performanceKey = uniqueKey + "UserRole";

            Func<string> fnc = GetUserRoleFromSettings;

            var args = new object[] { };

            Utilities.GetPerformance<string>(performanceMode, performanceKey, out keyValue, fnc, args);

            return keyValue;
        }

        public static string GetUserRoleFromSettings()
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            return rootNode.Attributes["user"].Value;
        }

        public static bool IsRoleSettingsPresent(string role)
        {
            var present = false;

            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var roleNodes = xmlDoc.SelectNodes(roleXPath);

            foreach (XmlNode roleNode in roleNodes)
            {
                if (role == roleNode.Attributes["name"].Value)
                {
                    present = true;
                    break;
                }
            }

            return present;
        }

        public static void SetAdminRole(string role)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            rootNode.Attributes["admin"].Value = role;

            SaveXml(xmlDoc);
        }

        public static void SetAnonymousRole(string role)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            rootNode.Attributes["anonymous"].Value = role;

            SaveXml(xmlDoc);
        }

        public static void SetDefaultRoleName(string role)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            rootNode.Attributes["default"].Value = role;

            SaveXml(xmlDoc);
        }

        public static void SetInactivatedRole(string role)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            rootNode.Attributes["inactivated"].Value = role;

            SaveXml(xmlDoc);
        }

        public static void SetRoleSetting(UserRoleSettings roleSetting)
        {
            if (IsRoleSettingsPresent(roleSetting.Name))
            {
                var xmlDoc = new XmlDocument();

                xmlDoc.Load(fileName);

                var roleNodes = xmlDoc.SelectNodes(roleXPath);

                foreach (XmlNode roleNode in roleNodes)
                {
                    if (roleSetting.Name == roleNode.Attributes["name"].Value)
                    {
                        roleNode.Attributes["path"].Value = roleSetting.Path;
                        roleNode.Attributes["locked"].Value = roleSetting.Locked.ToString();
                        roleNode.Attributes["showCategory"].Value = roleSetting.ShowCategory.ToString();
                        roleNode.Attributes["showRoles"].Value = roleSetting.ShowRoles.ToString();
                        roleNode.Attributes["hideSuperAdmin"].Value = roleSetting.HideSuperAdmin.ToString();
                        roleNode.Attributes["registrationBlocked"].Value = roleSetting.RegistrationBlocked.ToString();
                        roleNode.Attributes["login"].Value = roleSetting.Login.ToString();
                        roleNode.Attributes["masterPage"].Value = roleSetting.MasterPage;
                        roleNode.Attributes["theme"].Value = roleSetting.Theme;

                        SaveXml(xmlDoc);

                        break;
                    }
                }
            }
        }

        public static void SetSuperAdminRole(string role)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            rootNode.Attributes["superAdmin"].Value = role;

            SaveXml(xmlDoc);
        }

        public static void SetUserRole(string role)
        {
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(fileName);

            var rootNode = xmlDoc.SelectSingleNode(rolesXPath);

            rootNode.Attributes["user"].Value = role;

            SaveXml(xmlDoc);
        }
    }
}