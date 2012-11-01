using System;
using System.Globalization;
using System.Web;
using System.Collections.Generic;
using Arshu.Core.Common;

using Priya.Generic.Utils;
using Priya.Security.Utils;
using Priya.InfoList.Entity;
using Priya.InfoList.Model;

namespace Priya.InfoList.Views
{
    public static class InfoSectionView
    {
        #region Variables

        public const string ServiceUrl = "/Apps/InfoList/InfoSection/JsonInfoSection.ashx";
        public const string RefreshListFunctionName = "refreshInfoSectionList";

        #endregion

        #region Get Script

        private static string GetSaveScript()
        {
            string message;
            var templateSave = new TemplateInfoSectionSave
            {
                ScriptServiceUrl = UtilsGeneric.GetCurrentService(ServiceUrl)
            };

            string htmlScript = templateSave.GetScriptFilled(true, UtilsGeneric.LoadMinJs, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlScript;
        }

        private static string GetListScript()
        {
            string message;
            var templateList = new TemplateInfoSectionList
            {
                ScriptServiceUrl = UtilsGeneric.GetCurrentService(ServiceUrl)
            };

            string htmlScript = templateList.GetScriptFilled(true, UtilsGeneric.LoadMinJs, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlScript;
        }

        public static string GetScript(bool loadSaveScript, bool loadListScript)
        {
            string htmlSaveScript = "";
            if (loadSaveScript == true)
            {
                htmlSaveScript = GetSaveScript();
            }
            string htmlListScript = "";
            if (loadListScript == true)
            {
                htmlListScript = GetListScript();
            }
            return htmlSaveScript + htmlListScript;
        }

        #endregion

        #region Get View

        public static string GetView(long infoPageId, long dataIndex, string templateSuffix, bool showSave, bool showList, bool loadSaveScript, bool loadListScript)
        {
            long id = 0;
            long pageNo = 1;
            long itemsPerPage = UtilsGeneric.DefaultItemsPerPage;

            #region Save View

            string htmlSaveView = "";
            htmlSaveView = GetSaveView(id, pageNo, itemsPerPage, dataIndex, templateSuffix, infoPageId, loadSaveScript, showSave);
            //htmlSaveView += InfoDetailView.GetView(0, dataIndex + 1, templateSuffix, false, false, true, true);
            htmlSaveView += InfoDetailView.GetScript(true, true);

            #endregion

            #region List View

            string htmlListView = "";
            htmlListView = GetListView(pageNo, itemsPerPage, dataIndex, templateSuffix, infoPageId, loadListScript, showList);

            #endregion

            var template = new TemplateInfoSection
            {
                //SaveExpand = (id == 0) ? "true" : "false",
                SaveDetail = htmlSaveView,
                //ListExpand = (id == 0) ? "false" : "true",
                ListDetail = htmlListView,
            };

            string message = "";
            string html = template.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            return html;
        }

        #endregion

        #region Save View

        public static string GetSaveView(long infoSectionId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long infoPageId, bool loadSaveScript, bool loadSave)
        {
            string htmlSaveScript = "";
            if (loadSaveScript == true)
            {
                htmlSaveScript = GetSaveScript();
            }
            string htmlSaveDetail = "";
            if (loadSave == true)
            {
                htmlSaveDetail = GetSaveDetailView(infoSectionId, pageNo, itemsPerPage, dataIndex, templateSuffix, infoPageId);
            }

            string message;
            bool hideDisplay = true;
            var templateSave = new TemplateInfoSectionSave
            {
                SaveScript = htmlSaveScript,
                SaveDetail = htmlSaveDetail,
                SaveViewHidden = hideDisplay,
                InfoPageId = infoPageId.ToString(),
            };
            string htmlSave = templateSave.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);

            return htmlSave;
        }

        public static string GetSaveDetailView(long infoSectionId, long pageNo, long itemsPerSection, long dataIndex, string templateSuffix, long infoPageId)
        {
            string message = "";
            string htmlSaveDetail = "";
            long revisionNo = 0;
            string infoPageName = "";
            string infoSectionName = "";
            string infoSectionDescription = "";
            bool asyncLoading = false;
            bool isActive = true;
            bool isDeleted = false;
            long sequence = 0;
            bool showAdditional = (infoSectionId != 0);

            if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRoleEnabled() == false))
            {
                TemplateInfoSectionView templateView = new TemplateInfoSectionView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerSection.ToString(),
                    TemplateSuffix = templateSuffix,                    
                    InfoPageId = infoPageId.ToString()
                };
                htmlSaveDetail = templateView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                #region Get Page Details

                if (infoPageId > 0)
                {
                    LTD_InfoPage ltdInfoPageExisting = DataInfoList.GetLtdInfoPage(infoPageId);
                    if (ltdInfoPageExisting != null)
                    {
                        infoPageName = ltdInfoPageExisting.InfoPageName;
                        asyncLoading = ltdInfoPageExisting.AsyncLoading;
                    }
                }

                #endregion

                #region Get Section Details

                if (infoSectionId > 0)
                {
                    LTD_InfoSection ltdSectionExisting = DataInfoList.GetLtdInfoSection(infoSectionId);
                    if (ltdSectionExisting != null)
                    {
                        infoSectionName = ltdSectionExisting.InfoSectionName;
                        infoSectionDescription = ltdSectionExisting.InfoSectionDescription;
                        isActive = ltdSectionExisting.IsActive;
                        isDeleted = ltdSectionExisting.IsDeleted;
                        sequence = ltdSectionExisting.Sequence;
                        revisionNo = ltdSectionExisting.RevisionNo;
                    }
                }

                #endregion

                #region Set Action

                bool showDeleted = false;
                bool showUserInfo = false;
                bool enableSave = true;
                bool enableDelete = true;
                if (UtilsSecurity.IsAuthenticated() == false)
                {
                    showUserInfo = true;
                    enableSave = false;
                    enableDelete = false;
                }

                showDeleted = (!UtilsSecurity.HaveAdminRole() && (infoSectionId > 0));

                #endregion

                #region Set Template

                #region Additional

                List<TemplateInfoSectionSaveDetail.AdditionalVisible> additionalVisibleList = new List<TemplateInfoSectionSaveDetail.AdditionalVisible>();
                if (showAdditional == true)
                {
                    additionalVisibleList.Add(new TemplateInfoSectionSaveDetail.AdditionalVisible
                    {
                        IsActiveHidden = (infoSectionId == 0),
                        IsActive = isActive,
                        IsDeletedHidden = !showDeleted,
                        IsDeleted = isDeleted,
                        SequenceHidden = (infoSectionId == 0),
                        Sequence = sequence.ToString(),
                    });
                }

                #endregion

                #region Action

                string addActionHtml = "";
                string editActionHtml = "";
                if (infoSectionId == 0)
                {
                    var templateSaveDetailAdd = new TemplateInfoSectionSaveDetailAdd
                    {
                        AddActionDisabled = !enableSave,
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerSection.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                        AsyncLoading = asyncLoading.ToString().ToLower(),
                        InfoPageId = infoPageId.ToString(),
                    };
                    addActionHtml = templateSaveDetailAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }
                else
                {
                    var templateSaveDetailEdit = new TemplateInfoSectionSaveDetailEdit
                    {
                        Id = infoSectionId.ToString(),
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerSection.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                        AsyncLoading = asyncLoading.ToString().ToLower(),
                        InfoPageId = infoPageId.ToString(),

                        SaveActionDisabled = !enableSave,
                        DeleteActionDisabled = !enableDelete,
                    };
                    editActionHtml = templateSaveDetailEdit.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                #endregion

                var templateSaveDetail = new TemplateInfoSectionSaveDetail
                {
                    //Id = infoSectionId.ToString("N0", CultureInfo.InvariantCulture),
                    RevisionNo = revisionNo.ToString(),

                    InfoPageName = infoPageName,
                    InfoSectionName = infoSectionName,
                    InfoSectionDescription = infoSectionDescription,
                    AdditionalActionVisible = showAdditional,
                    AdditionalVisibleList = additionalVisibleList,

                    //AddMode = (levelId == 0) ? true : false,
                    AddAction = addActionHtml,
                    EditAction = editActionHtml,
                    ShowUserInfo = showUserInfo,
                };

                htmlSaveDetail = templateSaveDetail.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                      out message);
                #endregion
            }

            return htmlSaveDetail;
        }

        #endregion

        #region List View

        public static string GetListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long infoPageId, bool loadListScript, bool loadList)
        {
            string htmlListScript = "";
            if (loadListScript == true)
            {
                htmlListScript = GetListScript();
            }
            string htmlListDetail = GetListDetailView(pageNo, itemsPerPage, dataIndex, templateSuffix, infoPageId, loadList);

            string message;
            var templateList = new TemplateInfoSectionList
            {
                ListScript = htmlListScript,
                ListDetail = htmlListDetail,                
                InfoPageId = infoPageId.ToString()
            };
            string htmlList = templateList.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlList;
        }

        public static string GetListDetailView(long pageNo, long itemsPerSection, long dataIndex, string templateSuffix, long infoPageId, bool loadList)
        {
            string htmlListAllItem = "";
            if (loadList == true)
            {
                htmlListAllItem = GetListAllItemView(pageNo, itemsPerSection, dataIndex, templateSuffix, infoPageId, true);
            }

            string message;
            var templateListDetail = new TemplateInfoSectionListDetail
            {
                ListItem = htmlListAllItem,                
                InfoPageId = infoPageId.ToString()
            };
            string htmlListDetail = templateListDetail.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                            UtilsGeneric.ThrowException, out message);

            return htmlListDetail;
        }

        #endregion

        #region Item View

        public static string GetListAllItemView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long infoPageId, bool asyncLoading)
        {
            string message = "";
            if (itemsPerPage == 0) itemsPerPage = UtilsGeneric.DefaultInnerItemsPerPage;
            long totalSections;
            long totalItems;
            string htmlTextItemList = "";
            string htmlAddItemList = "";

            #region Add Link

            if (UtilsSecurity.HaveAuthorRoleEnabled() == true)
            {
                TemplateInfoSectionSaveAdd templateSaveAdd = new TemplateInfoSectionSaveAdd
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    InfoPageId = infoPageId.ToString(),                    
                };
                htmlAddItemList = templateSaveAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }

            #endregion

            #region Get Fill List

            #region Get Section Data

            List<LTD_InfoSection> ltdSectionList = new List<LTD_InfoSection>();
            if ((UtilsSecurity.HaveAdminRole() ==true) || (UtilsSecurity.HaveAuthorRoleEnabled() == true))
            {
                ltdSectionList = DataInfoList.GetPagedLtdInfoSection(infoPageId, false, pageNo, itemsPerPage, out totalSections, out totalItems);
            }
            else
            {
                ltdSectionList = DataInfoList.GetPagedLtdInfoSection(infoPageId, true, pageNo, itemsPerPage, out totalSections, out totalItems);
            }


            #endregion

            if (ltdSectionList.Count > 0)
            {
                #region Get Section Details

                string topSectionrDetails = UtilsGeneric.GetItemPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalSections, RefreshListFunctionName, asyncLoading.ToString().ToLower() + "," + infoPageId.ToString());
                string bottomSectionrDetails = UtilsGeneric.GetLinkPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalSections, totalItems, RefreshListFunctionName, asyncLoading.ToString().ToLower() + "," + infoPageId.ToString(), false);

                #endregion

                #region Append Top Sectionr

                if (topSectionrDetails.Trim().Length > 0)
                {
                    htmlTextItemList += topSectionrDetails;
                }

                #endregion

                #region Append Items

                int index = 0;
                for (; index < ltdSectionList.Count; index++)
                {
                    LTD_InfoSection ltdSection = ltdSectionList[index];
                    string htmlTextItemTemplate = GetListSingleItemView(ltdSection, pageNo, itemsPerPage, dataIndex, templateSuffix, infoPageId, index, asyncLoading);
                    htmlTextItemList += htmlTextItemTemplate;
                }

                #endregion

                #region Append Bottom Sectionr

                if (bottomSectionrDetails.Trim().Length > 0)
                {
                    htmlTextItemList += bottomSectionrDetails;
                }

                #endregion
            }

            #endregion

            #region Set Fill List

            if ((htmlTextItemList.Length == 0) && (UtilsSecurity.HaveAuthorRoleEnabled() == true))
            {
                //TemplateInfoSectionListDetailEmpty templateListDetailEmpty = new TemplateInfoSectionListDetailEmpty
                //{
                //    DataIndex = dataIndex.ToString(),
                //    PageNo = pageNo.ToString(),
                //    ItemsPerPage = itemsPerPage.ToString(),
                //    TemplateSuffix = templateSuffix,
                //    InfoPageId = infoPageId.ToString(),
                //    AsyncLoading = asyncLoading.ToString().ToLower()
                //};
                //htmlTextItemList = templateListDetailEmpty.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }

            #endregion

            return htmlAddItemList + htmlTextItemList;
        }

        private static string GetListSingleItemView(LTD_InfoSection ltdSection, long pageNo, long itemsPerSection, long dataIndex, string templateSuffix, long infoPageId, long recordCount, bool asyncLoading)
        {
            string htmlTextItem = "";
            LTD_InfoPage ltdInfoPage = DataInfoList.GetLtdInfoPage(infoPageId);
            if ((ltdSection != null) && (ltdInfoPage != null))
            {
                string message;
                List<TemplateInfoSectionListDetailItem.EditAction> editActionList = new List<TemplateInfoSectionListDetailItem.EditAction>();
                List<TemplateInfoSectionListDetailItem.AsyncAction> asyncActionList = new List<TemplateInfoSectionListDetailItem.AsyncAction>();

                if ((UtilsSecurity.HaveAdminRole() == true) || (UtilsSecurity.HaveAuthorRoleEnabled() == true))
                {
                    editActionList.Add(new TemplateInfoSectionListDetailItem.EditAction
                    {
                        Id = ltdSection.InfoSectionID.ToString(),
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerSection.ToString(),
                        TemplateSuffix = templateSuffix,                                 
                        InfoPageId = infoPageId.ToString(),
                    });
                }

                string infoDetailListView = "";
                string infoDetailSaveView = "";

                if ((ltdInfoPage.AsyncLoading == false) || ((ltdInfoPage.AsyncLoading == true) && (asyncLoading == false)))
                {
                    infoDetailListView = InfoDetailView.GetView(ltdSection.InfoSectionID, dataIndex + 1, templateSuffix, true, true, false, false);
                }
                else
                {
                    asyncActionList.Add(new TemplateInfoSectionListDetailItem.AsyncAction
                    {
                        Id = ltdSection.InfoSectionID.ToString(),
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerSection.ToString(),
                        TemplateSuffix = templateSuffix,                        
                    });

                    infoDetailSaveView = InfoDetailView.GetView(ltdSection.InfoSectionID, dataIndex + 1, templateSuffix, false, false, false, false);
                }

                var templateListDetailItem = new TemplateInfoSectionListDetailItem
                {
                    InfoSectionName = ltdSection.InfoSectionName,
                    InfoSectionDescription = ltdSection.InfoSectionDescription,
                    IsInActive = !ltdSection.IsActive,

                    EditActionList = editActionList,
                    AsyncActionList = asyncActionList,
                    InfoDetailView = infoDetailSaveView + infoDetailListView
                };
                htmlTextItem = templateListDetailItem.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                        UtilsGeneric.ThrowException,
                                                                        out message);


            }
            return htmlTextItem;
        }

        #endregion
    }
}