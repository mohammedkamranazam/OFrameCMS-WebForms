using AjaxControlToolkit;
using OWDARO.UI.UserControls.Controls;
using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace OWDARO.Util
{
    public static class Extensions
    {
        public static void AddSelect(this DropDownListAdv dropDownList)
        {
            dropDownList.DropDownList.AddSelect();
        }

        public static void AddSelect(this DropDownList dropDownList)
        {
            dropDownList.Items.Insert(0, new ListItem("Select", "-1"));
        }

        public static void DeleteFile(this string fileName)
        {
            fileName = HttpContext.Current.Server.MapPath(fileName);

            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogError(ex);
                }
            }
        }

        public static Gender GetGender(this DropDownListAdv dropDownList)
        {
            if (dropDownList.SelectedValue == Gender.Male.ToString())
            {
                return Gender.Male;
            }
            else
            {
                if (dropDownList.SelectedValue == Gender.Female.ToString())
                {
                    return Gender.Female;
                }
                else
                {
                    if (dropDownList.SelectedValue == Gender.Unspecified.ToString())
                    {
                        return Gender.Unspecified;
                    }
                    else
                    {
                        return Gender.Unspecified;
                    }
                }
            }
        }

        public static string GetImageFileExtension(this AjaxFileUploadEventArgs ee)
        {
            var extension = ".jpg";

            if (ee.ContentType.NullableContains("jpg"))
            {
                extension = ".jpg";
            }

            if (ee.ContentType.NullableContains("gif"))
            {
                extension = ".gif";
            }

            if (ee.ContentType.NullableContains("png"))
            {
                extension = ".png";
            }

            if (ee.ContentType.NullableContains("jpeg"))
            {
                extension = ".jpeg";
            }

            return extension;
        }

        public static int? GetNullableSelectedValue(this DropDownListAdv dropDownList)
        {
            if (dropDownList.SelectedItem == null || dropDownList.SelectedValue == "-1")
            {
                return null;
            }
            else
            {
                return dropDownList.GetSelectedValue();
            }
        }

        public static string GetProfilePic(this Gender gender)
        {
            if (gender == Gender.Male)
            {
                return AppConfig.MaleAvatar;
            }
            else
            {
                if (gender == Gender.Female)
                {
                    return AppConfig.FemaleAvatar;
                }
                else
                {
                    if (gender == Gender.Unspecified)
                    {
                        return AppConfig.UnspecifiedAvatar;
                    }
                    else
                    {
                        return AppConfig.UnspecifiedAvatar;
                    }
                }
            }
        }

        public static int GetSelectedValue(this DropDownListAdv dropDownList)
        {
            return DataParser.IntParse(dropDownList.SelectedValue);
        }

        public static bool IsNullSelected(this DropDownListAdv dropDownList)
        {
            if (GetNullableSelectedValue(dropDownList) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool NullableContains(this string text, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            return text.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static int NullReverser(this int? value)
        {
            if (value == null)
            {
                return -1;
            }
            else
            {
                return (int)value;
            }
        }

        public static void SetNullableSelectedValue(this DropDownListAdv dropDownList, int? value)
        {
            if (value == null)
            {
                dropDownList.SelectedValue = "-1";
            }
            else
            {
                dropDownList.SelectedValue = value.ToString();
            }
        }

        public static HttpCacheability ToCacheType(this string location)
        {
            switch (location)
            {
                case "Server":
                    return HttpCacheability.Server;

                case "Client":
                    return HttpCacheability.Private;

                case "Both":
                    return HttpCacheability.ServerAndPrivate;

                case "None":
                default:
                    return HttpCacheability.NoCache;
            }
        }
    }
}