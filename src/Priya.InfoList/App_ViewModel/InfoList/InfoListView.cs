using System;
using System.Globalization;
using System.Web;
using System.Collections.Generic;

using Arshu.Core.Common;

using Priya.Generic.Utils;
using Priya.Generic.Views;
using Priya.Mobile.Views;
using Priya.Security.Entity;
using Priya.Security.Utils;
using Priya.Security.Views;
using Priya.InfoList.Entity;
using Priya.InfoList.Model;
using Priya.InfoList.Data;

namespace Priya.InfoList.Views
{
    public class InfoListView : IPageView
    {
        #region IView Interface

        public string RawUrl { get; set; }
        public string ThemeName { get; set; }
        public string TemplateSuffix { get; set; }
        public string SiteTitle { get; set; }
        public string PageTitle { get; set; }
        public string PageContent { get; set; }
        public bool IsPageProcessor { get { return false; } }

        public void InitView(bool reInit)
        {
        }

        public string GetPageView()
        {
            string headerTitle = UtilsGeneric.GetCurrentText("Info List");
            if ((UtilsGeneric.ForceLogin == true) && (UtilsSecurity.IsAuthenticated() == false))
            {
                headerTitle = UtilsGeneric.GetCurrentText("Security");
            }
            string pageTitle = SiteTitle + "-" + headerTitle;

            string afterAction = UtilsGeneric.RefreshFunctionWithMessage;
            string helpUrl = UtilsGeneric.HelpUrl;

            if (UtilsSecurity.HaveAdminRole() == true)
            {
                //SecurityHeaderView.AfterLoginHeaderLinks.Clear();
                SecurityHeaderView.AfterLoginHeaderLinks.Add(new ViewInfo
                {
                    ViewName = "DataView",
                    ViewIconName = "comment"
                });
            }

            return GetPageView(TemplateSuffix, ThemeName, pageTitle, headerTitle, helpUrl, afterAction);
        }

        #endregion

        #region Get Page View

        public static string GetPageView(string templateSuffix, string themeName, string pageTitle, string headerTitle, string helpUrl, string afterAction)
        {
            #region Variables

            bool enableMobileAddress = false;
            int dataIndex = 0;

            #endregion

            #region Get JQ Js/Css Resources

            string cssResourcesLink = "";
            string jsResourcesLink = "";
            string currentThemeName = MobileTheme.Default.ToString();
            if (string.IsNullOrEmpty(themeName) == false) currentThemeName = themeName;
            MobileView.GetMobileHeaderResource(enableMobileAddress, currentThemeName, out cssResourcesLink, out jsResourcesLink);

            #endregion

            #region Set Mobile Page Header Html

            string htmlMobilePageHeader = "";  //"<h2>InfoList Manager</h2>";

            #endregion

            #region Get Mobile Page Security Html

            string htmlMobilePageSecure = "";

            htmlMobilePageSecure = SecurityView.GetView(dataIndex, templateSuffix, headerTitle, helpUrl, afterAction, "");

            #endregion

            #region Get Mobile Page Content Html

            string htmlMobilePageContent = "";

            if ((UtilsGeneric.ForceLogin == false) || (UtilsSecurity.IsAuthenticated() == true))
            {
                if (UtilsSecurity.HaveAdminRole() == true)
                {
                    string infoCategoryView = InfoCategoryView.GetView(dataIndex, templateSuffix);
                    htmlMobilePageContent += infoCategoryView;
                }

                string pageView = InfoPageView.GetView(dataIndex + 1, templateSuffix);
                htmlMobilePageContent += pageView;
            }

            #endregion

            #region Get Mobile Page Html

            string htmlMobilePage = MobileView.GetView(templateSuffix, htmlMobilePageHeader, htmlMobilePageSecure, htmlMobilePageContent, "");

            #endregion

            #region Get IndexPage Header Html

            string htmlPageHeader = "";

            #endregion

            #region Get IndexPage Content Html

            string htmlPageContent = htmlMobilePage;

            #endregion

            #region Get IndexPage Footer Html

            string htmlPageFooter = "";

            #endregion

            #region Get IndexPage Html

            string htmlText = GenericView.GetView(templateSuffix, pageTitle, cssResourcesLink, jsResourcesLink, htmlPageHeader, htmlPageContent, htmlPageFooter, enableMobileAddress, true);

            #endregion

            return htmlText;
        }

        #endregion
    }
}