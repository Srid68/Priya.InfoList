using System;
using System.Globalization;
using System.Web;
using System.Collections.Generic;
using Arshu.Core.Common;

using Priya.Generic.Utils;
using Priya.Security.Utils;
using Priya.Security.Model;
using Priya.InfoList.Entity;
using Priya.InfoList.Model;

namespace Priya.InfoList.Views
{
    public static class DataRefTypeView
    {
        public const string ServiceUrl = "/Apps/InfoList/Data/JsonDataRefType.ashx";
        public const string RefreshListFunctionName = "refreshDataRefTypeList";

        #region Get Script

        private static string GetSaveScript()
        {
            string message;
            var templateSave = new TemplateDataRefTypeSave
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
            var templateList = new TemplateDataRefTypeList
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

            string htmlSaveView = GetSaveView(id, pageNo, itemsPerPage, dataIndex, templateSuffix);

            #endregion

            #region List View

            string htmlListView = GetListView(id, pageNo, itemsPerPage, dataIndex, templateSuffix);

            #endregion            

            var templateDataRefType = new TemplateDataRefType
            {
                SaveExpand = (id == 0) ? "true" : "false",
                SaveDetail = htmlSaveView,
                //ListExpand = (id == 0) ? "false" : "true",
                ListDetail = htmlListView,
            };

            string message = "";
            string html = templateDataRefType.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            return html;
        }

        #endregion

        #region Save View

        public static string GetSaveView(long dataRefTypeId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlSaveScript = GetSaveScript();
            string htmlSaveDetail = ""; //GetSaveDetailView(dataRefTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix);

            string message;
            var templateSave = new TemplateDataRefTypeSave
            {
                SaveScript = htmlSaveScript,
                SaveDetail = htmlSaveDetail
            };
            string htmlSave = templateSave.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
          
            return htmlSave;
        }

        public static string GetSaveDetailView(long dataRefTypeId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string message = "";
            string htmlSaveDetail = "";
            long revisionNo = 0;
            string dataRefTypeName = "";
            bool dataRefTypeIsDefault = false;
            bool dataRefTypeIsActive = true;
            bool dataRefTypeIsSystem = false;

            if (UtilsSecurity.HaveAdminRole() == false)
            {
                TemplateDataRefTypeView dataRefTypeView = new TemplateDataRefTypeView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix
                };
                htmlSaveDetail = dataRefTypeView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                #region Get DataRefType Details

                if (dataRefTypeId > 0)
                {
                    CNS_DataRefType cnsDataRefTypeExisting = DataCommon.GetCnsDataRefType(dataRefTypeId);
                    if (cnsDataRefTypeExisting != null)
                    {
                        dataRefTypeName = cnsDataRefTypeExisting.DataRefType;

                        dataRefTypeIsDefault = cnsDataRefTypeExisting.IsDefault;
                        dataRefTypeIsActive = cnsDataRefTypeExisting.IsActive;
                        dataRefTypeIsSystem = cnsDataRefTypeExisting.IsSystem;
                        revisionNo = cnsDataRefTypeExisting.RevisionNo;
                    }
                }

                #endregion

                #region Set Action

                bool showAdminInfo = false;
                bool enableSave = true;
                bool enableDelete = true;
                if (UtilsSecurity.HaveAdminRole() == false)
                {
                    showAdminInfo = true;
                    enableSave = false;
                    enableDelete = false;
                }

                #endregion

                #region Set Template

                string addActionHtml = "";
                string editActionHtml = "";
                if (dataRefTypeId == 0)
                {
                    var templateSaveAdd = new TemplateDataRefTypeSaveDetailAdd
                    {
                        AddActionDisabled = !enableSave,
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                    };
                    addActionHtml = templateSaveAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }
                else
                {

                    var templateSaveEdit = new TemplateDataRefTypeSaveDetailEdit
                    {
                        Id = dataRefTypeId.ToString(),
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                        SaveActionDisabled = !enableSave,
                        DeleteActionDisabled = !enableDelete,
                    };
                    editActionHtml = templateSaveEdit.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                List<TemplateDataRefTypeSaveDetail.DataRefTypeNameEnabled> dataRefTypeNameEnabledList = new List<TemplateDataRefTypeSaveDetail.DataRefTypeNameEnabled>();
                List<TemplateDataRefTypeSaveDetail.DataRefTypeNameDisabled> dataRefTypeNameDisabledList = new List<TemplateDataRefTypeSaveDetail.DataRefTypeNameDisabled>();
                if (dataRefTypeIsSystem == false)
                {
                    dataRefTypeNameEnabledList.Add(new TemplateDataRefTypeSaveDetail.DataRefTypeNameEnabled
                    {
                        DataRefType = dataRefTypeName,
                        AddMode = (dataRefTypeId == 0) ? true : false,
                    });

                }
                else
                {
                    dataRefTypeNameDisabledList.Add(new TemplateDataRefTypeSaveDetail.DataRefTypeNameDisabled
                    {
                        DataRefType = dataRefTypeName,
                        AddMode = (dataRefTypeId == 0) ? true : false,
                    });
                }

                List<TemplateDataRefTypeSaveDetail.IsActiveVisible> isActiveVisibleList = new List<TemplateDataRefTypeSaveDetail.IsActiveVisible>();
                if (dataRefTypeIsActive  == true)
                {
                    isActiveVisibleList.Add(new TemplateDataRefTypeSaveDetail.IsActiveVisible
                    {
                        IsActive = true
                    });
                }

                var templateSaveDetail = new TemplateDataRefTypeSaveDetail
                {
                    //Id = dataRefTypeId.ToString("N0", CultureInfo.InvariantCulture),
                    RevisionNo = revisionNo.ToString(),
                    DataRefTypeNameEnabledList = dataRefTypeNameEnabledList,
                    DataRefTypeNameDisabledList = dataRefTypeNameDisabledList,
                    IsDefault = dataRefTypeIsDefault,
                    IsActiveVisibleList = isActiveVisibleList,

                    AddAction = addActionHtml,
                    EditAction = editActionHtml,
                    ShowAdminInfo = showAdminInfo,
                };

                htmlSaveDetail = templateSaveDetail.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                      out message);
                #endregion
            }

            return htmlSaveDetail;
        }

        #endregion

        #region List View

        public static string GetListView(long dataRefTypeId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlListScript = GetListScript();
            string htmlListDetail = GetListDetailView(pageNo, itemsPerPage, dataIndex, templateSuffix);

            string message;
            var templateList = new TemplateDataRefTypeList
            {
                ListScript = htmlListScript,
                ListDetail = htmlListDetail
            };
            string htmlList = templateList.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlList;
        }

        public static string GetListDetailView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlListAllItem = GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix);

            string message;
            var templateListDetail = new TemplateDataRefTypeListDetail
            {
                ListItem = htmlListAllItem
            };
            string htmlListDetail = templateListDetail.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                            UtilsGeneric.ThrowException, out message);
         
            return htmlListDetail;
        }

        #endregion

        #region Item View

        public static string GetListAllItemView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string message = "";
            if (itemsPerPage == 0) itemsPerPage = UtilsGeneric.DefaultItemsPerPage;
            long totalPages;
            long totalItems;
            string htmlTextItemList = "";
            string htmlAddItemList = "";

            if (UtilsSecurity.HaveAdminRole() == false)
            {
                TemplateDataRefTypeView dataRefTypeView = new TemplateDataRefTypeView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                };
                htmlTextItemList = dataRefTypeView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
             }
            else
            {
                #region Add Link

                if (UtilsSecurity.HaveAdminRole() == true)
                {
                    TemplateDataRefTypeSaveAdd templateSaveAdd = new TemplateDataRefTypeSaveAdd
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

                List<CNS_DataRefType> cnsDataRefTypeList = DataCommon.GetAllCnsDataRefType(pageNo, itemsPerPage, out totalPages, out totalItems);

                #endregion

                if (cnsDataRefTypeList.Count > 0)
                {
                    #region Get Pager Details

                    string topPagerDetails = UtilsGeneric.GetItemPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, RefreshListFunctionName, "");
                    string bottomPagerDetails = UtilsGeneric.GetLinkPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, totalItems, RefreshListFunctionName, "");

                    #endregion

                    #region Append Top Pager

                    if (topPagerDetails.Trim().Length > 0)
                    {
                        htmlTextItemList += topPagerDetails;
                    }

                    #endregion

                    #region Append Items

                    int index = 0;
                    for (; index < cnsDataRefTypeList.Count; index++)
                    {
                        CNS_DataRefType cnsDataRefType = cnsDataRefTypeList[index];
                        string htmlTextItemTemplate = GetListSingleItemView(cnsDataRefType, pageNo, itemsPerPage, dataIndex, templateSuffix);
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
                    TemplateDataRefTypeListDetailEmpty dataRefTypeListDetailEmpty = new TemplateDataRefTypeListDetailEmpty
                    {
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix,
                    };
                    htmlTextItemList = dataRefTypeListDetailEmpty.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }
                #endregion
            }

            return htmlAddItemList + htmlTextItemList;
        }

        private static string GetListSingleItemView(CNS_DataRefType cnsDataRefType, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlTextItem = "";
            if (cnsDataRefType != null)
            {
                string message;
                List<TemplateDataRefTypeListDetailItem.EditAction> editActionList = new List<TemplateDataRefTypeListDetailItem.EditAction>();
                if (UtilsSecurity.HaveAdminRole() == true)
                {
                    editActionList.Add(new TemplateDataRefTypeListDetailItem.EditAction
                    {
                        Id = cnsDataRefType.DataRefTypeID.ToString(),
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix
                    });
                }

                var templateItem = new TemplateDataRefTypeListDetailItem
                {
                    DataRefType = cnsDataRefType.DataRefType,
                    IsDefault = cnsDataRefType.IsDefault,
                    IsInActive = !cnsDataRefType.IsActive,

                    EditActionList = editActionList
                };
                htmlTextItem = templateItem.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                        UtilsGeneric.ThrowException,
                                                                        out message);


            }
            return htmlTextItem;
        }

        #endregion       
    }
}