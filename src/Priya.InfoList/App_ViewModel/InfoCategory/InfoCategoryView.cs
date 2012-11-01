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
    public static class InfoCategoryView
    {
        public const string ServiceUrl = "/Apps/InfoList/InfoCategory/JsonInfoCategory.ashx";
        public const string RefreshListFunctionName = "refreshInfoCategoryList";

        #region Get Script

        private static string GetSaveScript(string refreshCallback)
        {
            string message;
            var templateSave = new TemplateInfoCategorySave
            {
                ScriptRefreshCallback = refreshCallback,
                ScriptServiceUrl = UtilsGeneric.GetCurrentService(ServiceUrl)
            };
            string htmlScript = templateSave.GetScriptFilled(true, UtilsGeneric.LoadMinJs, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlScript;
        }

        private static string GetListScript()
        {
            string message;
            var templateList = new TemplateInfoCategoryList
            {
                ScriptServiceUrl = UtilsGeneric.GetCurrentService(ServiceUrl)
            };
            string htmlScript = templateList.GetScriptFilled(true, UtilsGeneric.LoadMinJs, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlScript;
        }

        #endregion

        #region Get View

        public static string GetView(long dataIndex, string templateSuffix)
        {
            long id = 0;
            long pageNo = 1;
            long itemsPerPage = UtilsGeneric.DefaultItemsPerPage;

            #region Save View

            string htmlSaveView = GetSaveView(id, pageNo, itemsPerPage, dataIndex, templateSuffix, false, "");

            #endregion

            #region List View

            string htmlListView = GetListView(id, pageNo, itemsPerPage, dataIndex, templateSuffix);

            #endregion
            
            var template = new TemplateInfoCategory
            {
                SaveExpand = (id == 0) ? "true" : "false",
                SaveDetail = htmlSaveView,
                //ListExpand = (id == 0) ? "false" : "true",
                ListDetail = htmlListView,
                DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),                
            };

            string message = "";
            string html = template.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            return html;
        }

        #endregion

        #region Save View

        public static string GetSaveView(long infoCategoryId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, bool hideDisplay, string refreshCallback)
        {
            string message;

            string htmlSaveScript = GetSaveScript(refreshCallback);
            string htmlSaveDetail = ""; //GetSaveDetailView(infoCategoryId, pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay);

            var templateSave = new TemplateInfoCategorySave
            {
                DataIndex = dataIndex.ToString(),
                SaveScript = htmlSaveScript,
                SaveDetail = htmlSaveDetail,                
                SaveViewHidden= hideDisplay,
            };
            string htmlSave = templateSave.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);

            return htmlSave;
        }

        public static string GetSaveDetailView(long infoCategoryId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, bool hideDisplay)
        {
            string message; 
            string htmlSaveDetail = "";
            long revisionNo = 0;
            string infoCategoryName = "";
            bool infoCategoryIsDefault = false;
            bool infoCategoryIsActive = true;
            bool infoCategoryIsSystem = false;

            if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
            {
                TemplateInfoCategoryView templateView = new TemplateInfoCategoryView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix
                };
                htmlSaveDetail = templateView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                #region Get Category Details

                if (infoCategoryId > 0)
                {
                    LTD_InfoCategory ltdInfoCategoryExisting = DataInfoList.GetLtdInfoCategory(infoCategoryId);
                    if (ltdInfoCategoryExisting != null)
                    {
                        infoCategoryName = ltdInfoCategoryExisting.InfoCategoryName;

                        infoCategoryIsDefault = ltdInfoCategoryExisting.IsDefault;
                        infoCategoryIsActive = ltdInfoCategoryExisting.IsActive;
                        //infoCategoryIsSystem = ltdInfoCategoryExisting.IsSystem;
                        revisionNo = ltdInfoCategoryExisting.RevisionNo;
                    }
                }

                #endregion

                #region Set Action

                bool showUserInfo = false;
                bool enableSave = true;
                bool enableDelete = true;
                if (UtilsSecurity.IsAuthenticated() == false)
                {
                    showUserInfo = true;
                    enableSave = false;
                    enableDelete = false;
                }

                #endregion

                #region Set Template

                string addActionHtml = "";
                string editActionHtml = "";
                if (infoCategoryId == 0)
                {
                    var templateSaveAdd = new TemplateInfoCategorySaveDetailAdd
                    {
                        AddActionDisabled =!enableSave,
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                        HideDisplay = hideDisplay.ToString().ToLower(),
                    };
                    addActionHtml = templateSaveAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }
                else
                {
                    var templateSaveEdit = new TemplateInfoCategorySaveDetailEdit
                    {
                        Id = infoCategoryId.ToString(),
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                        HideDisplay = hideDisplay.ToString().ToLower(),
                        SaveActionDisabled = !enableSave,
                        DeleteActionDisabled = !enableDelete,
                    };
                    editActionHtml = templateSaveEdit.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                List<TemplateInfoCategorySaveDetail.IsDefaultVisible.IsDefault> isDefaultList = new List<TemplateInfoCategorySaveDetail.IsDefaultVisible.IsDefault>();
                List<TemplateInfoCategorySaveDetail.IsDefaultVisible.IsNotDefault> isNotDefaultList = new List<TemplateInfoCategorySaveDetail.IsDefaultVisible.IsNotDefault>();
                if (infoCategoryIsDefault == true)
                {
                    isDefaultList.Add(new TemplateInfoCategorySaveDetail.IsDefaultVisible.IsDefault
                    {
                        DataIndex = dataIndex.ToString(),
                    });

                }
                else
                {
                    isNotDefaultList.Add(new TemplateInfoCategorySaveDetail.IsDefaultVisible.IsNotDefault
                    {
                        DataIndex = dataIndex.ToString(),
                    });
                }

                List<TemplateInfoCategorySaveDetail.IsDefaultVisible> isDefaultVisibleList = new List<TemplateInfoCategorySaveDetail.IsDefaultVisible>();
                if ((infoCategoryId != 0) && (UtilsSecurity.HaveAdminRole() ==true))
                {
                    isDefaultVisibleList.Add(new TemplateInfoCategorySaveDetail.IsDefaultVisible
                    {
                        DataIndex = dataIndex.ToString(),
                        IsDefaultList = isDefaultList,
                        IsNotDefaultList = isNotDefaultList,
                    });
                }
                List<TemplateInfoCategorySaveDetail.IsActiveVisible.IsActive> isActiveList = new List<TemplateInfoCategorySaveDetail.IsActiveVisible.IsActive>();
                List<TemplateInfoCategorySaveDetail.IsActiveVisible.IsInActive> isInActiveList = new List<TemplateInfoCategorySaveDetail.IsActiveVisible.IsInActive>();
                if (infoCategoryIsActive == true)
                {
                    isActiveList.Add(new TemplateInfoCategorySaveDetail.IsActiveVisible.IsActive
                    {
                        DataIndex = dataIndex.ToString(),
                    });

                }
                else
                {
                    isInActiveList.Add(new TemplateInfoCategorySaveDetail.IsActiveVisible.IsInActive
                    {
                        DataIndex = dataIndex.ToString(),
                    });
                }

                List<TemplateInfoCategorySaveDetail.IsActiveVisible> isActiveVisibleList = new List<TemplateInfoCategorySaveDetail.IsActiveVisible>();
                if (infoCategoryId != 0)
                {
                    isActiveVisibleList.Add(new TemplateInfoCategorySaveDetail.IsActiveVisible
                    {
                        DataIndex = dataIndex.ToString(),
                        IsActiveList = isActiveList,
                        IsInActiveList = isInActiveList,
                    });
                }

                var templateSaveDetail = new TemplateInfoCategorySaveDetail
                {
                    //Id = groupId.ToString("N0", CultureInfo.InvariantCulture),
                    DataIndex = dataIndex.ToString(),                    
                    RevisionNo = revisionNo.ToString("N0", CultureInfo.InvariantCulture),    
                    InfoCategoryName =infoCategoryName,
                    InfoCategoryNameDisabled = infoCategoryIsSystem,
                    IsDefaultVisibleList = isDefaultVisibleList,
                    IsActiveVisibleList = isActiveVisibleList,

                    AddAction = addActionHtml,
                    EditAction = editActionHtml,
                    ShowUserInfo = showUserInfo,   
                };

                htmlSaveDetail = templateSaveDetail.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);

                #endregion
            }

            return htmlSaveDetail;
        }

        #endregion

        #region List View

        public static string GetListView(long infoCategoryId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlListScript = GetListScript();

            string htmlListDetail = GetListDetailView(pageNo, itemsPerPage, dataIndex, templateSuffix, false);

            string message;
            var templateList = new TemplateInfoCategoryList
            {
                ListScript = htmlListScript,             
                ListDetail = htmlListDetail,
                DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture)
            };
            string htmlList = templateList.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlList;
        }

        public static string GetListDetailView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, bool hideDisplay)
        {
            string htmlListAllItem = GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay);

            string message;
            var templateListDetail = new TemplateInfoCategoryListDetail
            {
                ListItem = htmlListAllItem,
                DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture)
            };
            string htmlListDetail = templateListDetail.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                            UtilsGeneric.ThrowException, out message);

            return htmlListDetail;
        }

        #endregion

        #region Item View

        public static string GetListAllItemView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, bool hideDisplay)
        {
            string message = "";
            long totalPages;
            long totalItems;
            string htmlTextItemList = "";
            string htmlAddItemList = "";

            if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
            {
                TemplateInfoCategoryView templateView = new TemplateInfoCategoryView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    HideDisplay = hideDisplay.ToString().ToLower(),
                };
                htmlTextItemList = templateView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }else
            {
                #region Add Link

                if (UtilsSecurity.HaveAuthorRoleEnabled() == true)
                {
                    TemplateInfoCategorySaveAdd templateSaveAdd = new TemplateInfoCategorySaveAdd
                    {
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix,
                    };
                    htmlAddItemList = templateSaveAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                #endregion

                #region Get Fill List

                #region Get Paged Data

                List<LTD_InfoCategory> ltdInfoCategoryList = new List<LTD_InfoCategory>();
                ltdInfoCategoryList = DataInfoList.GetPagedLtdInfoCategory(pageNo, itemsPerPage, out totalPages, out totalItems);

                #endregion

                if (ltdInfoCategoryList.Count > 0)
                {
                    #region Get Pager Details

                    string topPagerDetails = UtilsGeneric.GetItemPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, RefreshListFunctionName, hideDisplay.ToString(CultureInfo.InvariantCulture).ToLower(), true, true);
                    string bottomPagerDetails = UtilsGeneric.GetLinkPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, totalItems, RefreshListFunctionName, hideDisplay.ToString(CultureInfo.InvariantCulture).ToLower());

                    #endregion

                    #region Append Top Pager

                    if (topPagerDetails.Trim().Length > 0)
                    {
                        htmlTextItemList += topPagerDetails;
                    }

                    #endregion

                    #region Append Items

                    int index = 0;
                    for (; index < ltdInfoCategoryList.Count; index++)
                    {
                        LTD_InfoCategory ltdInfoCategory = ltdInfoCategoryList[index];
                        string htmlTextItemTemplate = GetListSingleItemView(ltdInfoCategory, pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay);
                        htmlTextItemList += htmlTextItemTemplate;
                    }

                    #endregion

                    #region Append Bottom Pager

                    if (bottomPagerDetails.Trim().Length > 0)
                    {
                        htmlTextItemList += bottomPagerDetails;
                    }

                    #endregion
                }

                #endregion

                #region Set Fill List

                if (htmlTextItemList.Length == 0)
                {
                    TemplateInfoCategoryListDetailEmpty templateListDetailEmpty = new TemplateInfoCategoryListDetailEmpty
                    {
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix,
                        HideDisplay = hideDisplay.ToString().ToLower(),
                    };
                    htmlTextItemList = templateListDetailEmpty.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                #endregion
            }

            return htmlAddItemList + htmlTextItemList;
        }

        private static string GetListSingleItemView(LTD_InfoCategory ltdInfoCategory, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, bool hideDisplay)
        {
            string htmlTextItem = "";
            if (ltdInfoCategory != null)
            {
                string message;
                List<TemplateInfoCategoryListDetailItem.EditAction> editActionList = new List<TemplateInfoCategoryListDetailItem.EditAction>();
                editActionList.Add(new TemplateInfoCategoryListDetailItem.EditAction
                {
                    Id = ltdInfoCategory.InfoCategoryID.ToString(),
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,                    
                    //HideDisplay = hideDisplay.ToString().ToLower(),
                });
                string createdBy = UtilsSecurity.GetUserName(ltdInfoCategory.UserID);
                
                var templateListDetailItem = new TemplateInfoCategoryListDetailItem
                {
                    InfoCategoryName = ltdInfoCategory.InfoCategoryName,
                    IsDefault = ltdInfoCategory.IsDefault,
                    IsInActive = !ltdInfoCategory.IsActive,
                    CreatedBy = createdBy,

                    EditActionList = editActionList
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