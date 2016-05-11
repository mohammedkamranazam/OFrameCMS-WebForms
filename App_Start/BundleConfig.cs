using OWDARO;
using OWDARO.Settings;
using System.Web.Optimization;

namespace ProjectJKL
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var mainCSSBundle = new StyleBundle(string.Format("~/Theme{0}", AppConfig.MainTheme));
            var ziceCSSBundle = new StyleBundle(string.Format("~/Theme{0}", AppConfig.ZiceTheme));
            var popUpCSSBundle = new StyleBundle(string.Format("~/Theme{0}", AppConfig.PopUpTheme));
            var checkOutCSSBundle = new StyleBundle(string.Format("~/Theme{0}", AppConfig.CheckOutTheme));

            var mainScriptsBundle = new ScriptBundle(string.Format("~/Script{0}", AppConfig.MainTheme));
            var ziceScriptsBundle = new ScriptBundle(string.Format("~/Script{0}", AppConfig.ZiceTheme));
            var popUpScriptsBundle = new ScriptBundle(string.Format("~/Script{0}", AppConfig.PopUpTheme));
            var checkOutScriptsBundle = new ScriptBundle(string.Format("~/Script{0}", AppConfig.CheckOutTheme));

            mainCSSBundle.Include(ThemeStylesheetsHelper.GetPathsFromSettings(AppConfig.MainTheme));
            ziceCSSBundle.Include(ThemeStylesheetsHelper.GetPathsFromSettings(AppConfig.ZiceTheme));
            popUpCSSBundle.Include(ThemeStylesheetsHelper.GetPathsFromSettings(AppConfig.PopUpTheme));
            checkOutCSSBundle.Include(ThemeStylesheetsHelper.GetPathsFromSettings(AppConfig.CheckOutTheme));

            mainScriptsBundle.Include(ThemeScriptsHelper.GetPathsFromSettings(AppConfig.MainTheme));
            ziceScriptsBundle.Include(ThemeScriptsHelper.GetPathsFromSettings(AppConfig.ZiceTheme));
            popUpScriptsBundle.Include(ThemeScriptsHelper.GetPathsFromSettings(AppConfig.PopUpTheme));
            checkOutScriptsBundle.Include(ThemeScriptsHelper.GetPathsFromSettings(AppConfig.CheckOutTheme));

            bundles.Add(mainCSSBundle);
            bundles.Add(ziceCSSBundle);
            bundles.Add(popUpCSSBundle);
            bundles.Add(checkOutCSSBundle);

            bundles.Add(mainScriptsBundle);
            bundles.Add(ziceScriptsBundle);
            bundles.Add(popUpScriptsBundle);
            bundles.Add(checkOutScriptsBundle);

            BundleTable.EnableOptimizations = true;
        }
    }
}