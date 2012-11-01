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
    public static class DataTypeView
    {
        public const string ServiceUrl = "/Apps/InfoList/Data/JsonDataType.ashx"; 
        public const string RefreshListFunctionName = "refreshDataTypeList";

        #region Get Script

        private static string GetSaveScript()
        {
            string message;
            var templateSave = new TemplateDataTypeSave
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
            var templateList = new TemplateDataTypeList
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
            long id = 0 ;
            long pageNo = 1;
            long itemsPerPage = UtilsGeneric.DefaultItemsPerPage;

            #region Save View

            string htmlSaveView = GetSaveView(id, pageNo, itemsPerPage, dataIndex, templateSuffix);

            #endregion

            #region List View

            string htmlListView = GetListView(id, pageNo, itemsPerPage, dataIndex, templateSuffix);

            #endregion            

            var templateDataType = new TemplateDataType
            {
                SaveExpand = (id == 0) ? "true" : "false",
                SaveDetail = htmlSaveView,
                //ListExpand = (id == 0) ? "false" : "true",
                ListDetail = htmlListView,
            };

            string message = "";
            string html = templateDataType.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            return html;
        }

        #endregion

        #region Save View

        public static string GetSaveView(long dataTypeId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlSaveScript = GetSaveScript();
            string htmlSaveDetail = ""; //GetSaveDetailView(dataTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix);

            string message;
            var templateSave = new TemplateDataTypeSave
            {
                SaveScript = htmlSaveScript,
                SaveDetail = htmlSaveDetail
            };
            string htmlSave = templateSave.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
          
            return htmlSave;
        }

        public static string GetSaveDetailView(long dataTypeId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string message = "";
            string htmlSaveDetail = "";
            long revisionNo = 0;
            string dataTypeName = "";
            bool dataTypeIsDefault = false;
            bool dataTypeIsActive = true;
            bool dataTypeIsSystem = false;

            if (UtilsSecurity.HaveAdminRole() == false)
            {
                TemplateDataTypeView dataTypeView = new TemplateDataTypeView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix
                };
                htmlSaveDetail = dataTypeView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                #region Get DataType Details

                if (dataTypeId > 0)
                {
                    CNS_DataType cnsDataTypeExisting = DataCommon.GetCnsDataType(dataTypeId);
                    if (cnsDataTypeExisting != null)
                    {
                        dataTypeName = cnsDataTypeExisting.DataType;

                        dataTypeIsDefault = cnsDataTypeExisting.IsDefault;
                        dataTypeIsActive = cnsDataTypeExisting.IsActive;
                        dataTypeIsSystem = cnsDataTypeExisting.IsSystem;
                        revisionNo = cnsDataTypeExisting.RevisionNo;
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
                if (dataTypeId == 0)
                {
                    var templateSaveAdd = new TemplateDataTypeSaveDetailAdd
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
                    var templateSaveEdit = new TemplateDataTypeSaveDetailEdit
                    {
                        Id = dataTypeId.ToString(),
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,

                        SaveActionDisabled = !enableSave,
                        DeleteActionDisabled = !enableDelete,
                    };
                    editActionHtml = templateSaveEdit.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                List<TemplateDataTypeSaveDetail.IsActiveVisible> isActiveVisibleList = new List<TemplateDataTypeSaveDetail.IsActiveVisible>();
                if (dataTypeIsActive ==true)
                {
                    isActiveVisibleList.Add(new TemplateDataTypeSaveDetail.IsActiveVisible
                    {
                        IsActive = dataTypeIsActive
                    });
                }

                var templateSaveDetail = new TemplateDataTypeSaveDetail
                {
                    //Id = dataRefTypeId.ToString("N0", CultureInfo.InvariantCulture),
                    RevisionNo = revisionNo.ToString(),          
                    DataTypeName = dataTypeName,
                    DataTypeNameDisabled = dataTypeIsSystem,       
                    IsDefault = dataTypeIsDefault,
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

        public static string GetListView(long dataTypeId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlListScript = GetListScript();
            string htmlListDetail = GetListDetailView(pageNo, itemsPerPage, dataIndex, templateSuffix);

            string message;
            var templateList = new TemplateDataTypeList
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
            var templateListDetail = new TemplateDataTypeListDetail
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
                TemplateDataTypeView dataTypeView = new TemplateDataTypeView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                };
                htmlTextItemList = dataTypeView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                #region Add Link

                if (UtilsSecurity.HaveAdminRole() == true)
                {
                    TemplateDataTypeSaveAdd templateSaveAdd = new TemplateDataTypeSaveAdd
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

                List<CNS_DataType> cnsDataTypeList = DataCommon.GetAllCnsDataType(pageNo, itemsPerPage, out totalPages, out totalItems);

                #endregion

                if (cnsDataTypeList.Count > 0)
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
                    for (; index < cnsDataTypeList.Count; index++)
                    {
                        CNS_DataType cnsDataType = cnsDataTypeList[index];
                        string htmlTextItemTemplate = GetListSingleItemView(cnsDataType, pageNo, itemsPerPage, dataIndex, templateSuffix);
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
                    TemplateDataTypeListDetailEmpty dataTypeListDetailEmpty = new TemplateDataTypeListDetailEmpty
                    {
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix,
                    };
                    htmlTextItemList = dataTypeListDetailEmpty.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);

                }

                #endregion
            }

            return htmlAddItemList + htmlTextItemList;
        }

        private static string GetListSingleItemView(CNS_DataType cnsDataType, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlTextItem = "";
            if (cnsDataType != null)
            {
                string message;
                List<TemplateDataTypeListDetailItem.EditAction> editActionList = new List<TemplateDataTypeListDetailItem.EditAction>();
                if (UtilsSecurity.HaveAdminRole() == true)
                {
                    editActionList.Add(new TemplateDataTypeListDetailItem.EditAction
                    {
                        Id = cnsDataType.DataTypeID.ToString(),
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix
                    });
                }

                var templateItem = new TemplateDataTypeListDetailItem
                {
                    DataType = cnsDataType.DataType,
                    IsDefault = cnsDataType.IsDefault,
                    IsInActive = !cnsDataType.IsActive,

                    EditActionList = editActionList
                };

                htmlTextItem = templateItem.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);


            }
            return htmlTextItem;
        }

        #endregion       
    }
}