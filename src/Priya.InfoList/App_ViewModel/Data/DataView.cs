using System;
using System.Globalization;
using System.Collections.Generic;

using Arshu.Core.Basic.Compress;
using Arshu.Core.Common;

using Priya.Generic.Utils;
using Priya.Generic.Views;
using Priya.JQMobile.Views;
using Priya.Security.Utils;
using Priya.Security.Views;
using Priya.InfoList.Entity;
using Priya.InfoList.Model;
using Priya.InfoList.Data;

namespace Priya.InfoList.Views
{
    public class DataView : IPageView
    {
        #region IView Interface

        public string RawUrl { get; set; }
        public string ThemeName { get; set; }
        public string TemplateSuffix { get; set; }
        public string PageName { get; set; }
        public string PageContent { get; set; }

        public void InitView(bool reInit)
        {
            DataSource.OnGetUserInfo -= UtilsSecurity.UtilsSecurity_OnGetUserInfo;
            DataSource.OnGetUserInfo += UtilsSecurity.UtilsSecurity_OnGetUserInfo;                
        }

        public string GetPageView()
        {
            string pageTitle = UtilsGeneric.GetCurrentText("Data");
            string afterAction = UtilsGeneric.RefreshFunctionWithMessage;
            string helpUrl = UtilsGeneric.HelpUrl;
            return GetPageView(TemplateSuffix, ThemeName, pageTitle, pageTitle, helpUrl, afterAction);
        }

        #endregion

        #region Variables

        public const string ServiceUrl = "/Apps/InfoList/Data/JsonData.ashx";
        public const string RefreshListFunctionName = "refreshDataList";
        public static bool ShowEmptyChildData = false;
        public static int ChildDataIndexSuffix = 100;

        #endregion

        #region Get Script

        private static string GetSaveScript()
        {
            string message;
            var templateSave = new TemplateDataSave
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
            var templateList = new TemplateDataList
            {
                ScriptServiceUrl = UtilsGeneric.GetCurrentService(ServiceUrl)
            };

            string htmlScript = templateList.GetScriptFilled(true, UtilsGeneric.LoadMinJs, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlScript;
        }

        #endregion

        #region Page View

        public static string GetPageView(string templateSuffix, string themeName, string pageTitle, string headerTitle, string helpUrl, string afterAction)
        {
            #region Variables

            bool enablejQMobileAddress = false;
            int dataIndex = 0;

            #endregion

            #region Get JQ Js/Css Resources

            string cssResourcesLink = "";
            string jsResourcesLink = "";
            string currentThemeName = JQueryMobileTheme.Default.ToString();
            if (string.IsNullOrEmpty(themeName) == false) currentThemeName = themeName;
            JQMobileView.GetJQHeaderResource(enablejQMobileAddress, currentThemeName, out cssResourcesLink, out jsResourcesLink);

            #endregion

            #region Set JQPage Header Html

            string htmlJQPageHeader = "";  //"<h2>Data Manager</h2>";

            #endregion

            #region Get JQPage Security Html

            string htmlJQPageSecure = "";

            htmlJQPageSecure = SecurityView.GetView(dataIndex, templateSuffix, headerTitle, helpUrl, afterAction, "");

            #endregion

            #region Get JQPage Content Html

            string htmlJQPageContent = "";

            if ((UtilsGeneric.ForceLogin == false) || (UtilsSecurity.IsAuthenticated() == true))
            {
                string dataRefTypeHtml = "";
                string dataTypeHtml = "";
                string dataHtml = "";
                if (UtilsSecurity.HaveAdminRole())
                {
                    dataRefTypeHtml = DataRefTypeView.GetView(dataIndex + 2, templateSuffix);
                    dataTypeHtml += DataTypeView.GetView(dataIndex + 3, templateSuffix);
                }
                dataHtml = DataView.GetView(false, dataIndex + 4, templateSuffix, false, CommonRefType.None, 0, CommonDataType.GeneralData, "Data", false, false, false, true, true, true, false, true, true, "Add", "Edit", "Append", false);
                
                var templateMain = new TemplateMain
                {
                    ManageDataRefType = dataRefTypeHtml,
                    ManageDataType = dataTypeHtml,
                    ManageData = dataHtml
                };

                string message = "";
                htmlJQPageContent = templateMain.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message) ;
            }

            #endregion

            #region Get JQPage Html

            string htmlJQPage = JQMobileView.GetView(templateSuffix, htmlJQPageHeader, htmlJQPageSecure, htmlJQPageContent, "");

            #endregion

            #region Get IndexPage Content Html

            string htmlPageContent = htmlJQPage;

            #endregion

            #region Get IndexPage Footer Html

            string htmlPageFooter = "";

            #endregion

            #region Get IndexPage Html

            string htmlText = GenericView.GetView(templateSuffix, pageTitle, cssResourcesLink, jsResourcesLink, htmlPageContent, htmlPageFooter, enablejQMobileAddress, true);

            #endregion
            return htmlText;
        }

        #endregion       

        #region Get View

        public static string GetView(bool skipMain, long dataIndex, string templateSuffix, bool showSaveDetail, CommonRefType dataRefTypeId, long dataRefId, CommonDataType dataTypeId, string dataValueName, bool enableRefTypeId, bool enableRefId, bool enableDataTypeId, bool showRefType, bool showRefId, bool showDataType, bool showItemHeader, bool showItemEdit, bool showItemAppend, string itemAddName, string itemEditName, string itemAppendName, bool isPublic)
        {
            long id = 0;
            long pageNo = 1;
            long itemsPerPage = UtilsGeneric.DefaultItemsPerPage;

            #region Save View

            string configToken;
            string htmlSaveView = GetSaveView(showSaveDetail, id, pageNo, itemsPerPage, dataIndex, templateSuffix, (long)dataRefTypeId, dataRefId, (long)dataTypeId, dataValueName, enableRefTypeId, enableRefId, enableDataTypeId, showRefType, showRefId, showDataType, showItemHeader, showItemEdit, showItemAppend, itemAddName, itemEditName, itemAppendName, isPublic, out configToken);

            #endregion

            #region List View

            string htmlListView = GetListView(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);

            #endregion            

            string html = "";
            if (skipMain == false)
            {
                var templateData = new TemplateData
                {
                    SaveExpand = (id == 0) ? "true" : "false",
                    SaveDetail = htmlSaveView,
                    //ListExpand = (id == 0) ? "false" : "true",
                    ListDetail = htmlListView,
                };

                string message = "";
                html = templateData.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                html = htmlSaveView + htmlListView;
            }
            return html;
        }

        #endregion

        #region Save View

        public static string GetSaveView(long dataId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken)
        {
            string message = "";
            string htmlTextList = "";

            #region Retrieve Config Values

            long refTypeId = 0;
            long refId = 0;
            long dataTypeId = 0;
            string dataValueName = "";

            bool enableRefTypeId = false;
            bool enableRefId = false;
            bool enableDataTypeId = false;
            bool showRefType = false;
            bool showRefId = false;
            bool showDataType = false;

            bool showItemHeader = false;
            bool showItemEdit = false;
            bool showItemAppend = false;
            string itemAddName = "Add";
            string itemEditName = "Edit";
            string itemAppendName = "Append";
            bool isPublic = false;

            ProcessConfigToken(configToken, out refTypeId, out refId, out dataTypeId, out dataValueName, out enableRefTypeId, out enableRefId, out enableDataTypeId, out showRefType, out showRefId, out showDataType, out showItemHeader, out showItemEdit, out showItemAppend, out itemAddName, out itemEditName, out itemAppendName, out isPublic);

            #endregion

            if ((UtilsSecurity.IsAuthenticated() == false) && (isPublic ==false))
            {
                TemplateDataView dataView = new TemplateDataView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    DataValueName = dataValueName,
                };
                htmlTextList = dataView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {

                htmlTextList = GetSaveDetailView(refTypeId, refId, dataTypeId, dataValueName, enableRefTypeId, enableRefId, enableDataTypeId, showRefType, showRefId, showDataType, showItemHeader, showItemEdit, showItemAppend, itemAddName, itemEditName, itemAppendName, isPublic, 0, dataId, pageNo, itemsPerPage, dataIndex, templateSuffix, out configToken);
            }

            return htmlTextList;
        }

        public static string GetAppendView(long dataId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken)
        {
            string message = "";
            string htmlTextList = "";

            #region Retrieve Config Values

            long refTypeId = 0;
            long refId = 0;
            long dataTypeId = 0;
            string dataValueName = "";

            bool enableRefTypeId = false;
            bool enableRefId = false;
            bool enableDataTypeId = false;
            bool showRefType = false;
            bool showRefId = false;
            bool showDataType = false;

            bool showItemHeader = false;
            bool showItemEdit = false;
            bool showItemAppend = false;
            string itemAddName = "Add";
            string itemEditName = "Edit";
            string itemAppendName = "Append";
            bool isPublic = false;

            ProcessConfigToken(configToken, out refTypeId, out refId, out dataTypeId, out dataValueName, out enableRefTypeId, out enableRefId, out enableDataTypeId, out showRefType, out showRefId, out showDataType, out showItemHeader, out showItemEdit, out showItemAppend, out itemAddName, out itemEditName, out itemAppendName, out isPublic);

            #endregion

            if ((UtilsSecurity.IsAuthenticated() == false) && (isPublic ==false))
            {
                TemplateDataView dataView = new TemplateDataView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    DataValueName = dataValueName,
                };
                htmlTextList = dataView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                htmlTextList = GetSaveDetailView(refTypeId, refId, dataTypeId, dataValueName, false, false, false, showRefType, showRefId, showDataType, showItemHeader, showItemEdit, showItemAppend, itemAddName, itemEditName, itemAppendName, isPublic, dataId, 0, pageNo, itemsPerPage, dataIndex, templateSuffix, out configToken);
            }

            return htmlTextList;
        }

        public static string GetSaveView(bool showSaveDetail, long dataId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long dataRefTypeId, long dataRefId, long dataTypeId, string dataValueName, bool enableRefTypeId, bool enableRefId, bool enableDataTypeId, bool showRefType, bool showRefId, bool showDataType, bool showItemHeader, bool showItemEdit, bool showItemAppend, string itemAddName, string itemEditName, string itemAppendName, bool isPublic, out string retConfigToken)
        {
            string configToken;
            string htmlSaveScript = GetSaveScript();

            string htmlSaveDetail = GetSaveDetailView(dataRefTypeId, dataRefId, dataTypeId, dataValueName, enableRefTypeId,enableRefId, enableDataTypeId, showRefType, showRefId, showDataType, showItemHeader, showItemEdit, showItemAppend, itemAddName, itemEditName, itemAppendName, isPublic, 0, dataId, pageNo, itemsPerPage, dataIndex, templateSuffix, out configToken);

            string message;
            var templateSave = new TemplateDataSave
            {
                SaveScript = htmlSaveScript,
                SaveViewHidden = !showSaveDetail,

            };
            string htmlSave = templateSave.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);

            retConfigToken = configToken;
            return htmlSave;
        }

        public static string GetSaveDetailView(long dataRefTypeId, long dataRefId, long dataTypeId, string dataValueName, bool enableRefTypeId,bool enableRefId, bool enableDataTypeId, bool showRefType, bool showRefId, bool showDataType, bool showItemHeader, bool showItemEdit, bool showItemAppend, string itemAddName, string itemEditName, string itemAppendName, bool isPublic, long parentDataId, long dataId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, out string retConfigToken)
        {
            string message = "";
            string configToken;
            long revisionNo = 0;

            string dataValue = "";
            bool dataIsActive = true;
            bool dataIsDeleted = false;
            //bool showIsDeleted = false;

            #region Get Data Details

            if (dataId > 0)
            {
                CND_Data cndDataExisting = DataCommon.GetCndData(dataId);
                if (cndDataExisting != null)
                {
                    if (cndDataExisting.ParentDataID.HasValue == true)
                    {
                        parentDataId = cndDataExisting.ParentDataID.Value;
                    }                    
                    if (cndDataExisting.DataRefID.HasValue == true)
                    {
                        dataRefId = cndDataExisting.DataRefID.Value;
                    }
                    dataRefTypeId = cndDataExisting.DataRefTypeID;
                    dataTypeId = cndDataExisting.DataTypeID;
                    dataValue = cndDataExisting.DataValue;
                    dataIsActive = cndDataExisting.IsActive;
                    revisionNo = cndDataExisting.RevisionNo;
                }
            }

            #endregion

            #region Get Parent Data Details

            if (parentDataId > 0)
            {
                CND_Data cndDataParentExisting = DataCommon.GetCndData(parentDataId);
                if (cndDataParentExisting != null)
                {
                    dataRefTypeId = cndDataParentExisting.DataRefTypeID;
                    if (cndDataParentExisting.DataRefID.HasValue == true)
                    {
                        dataRefId = cndDataParentExisting.DataRefID.Value;
                    }
                    dataTypeId = cndDataParentExisting.DataTypeID;
                    enableRefId = false;
                    enableRefTypeId = false;
                    enableDataTypeId = false;
                }
            }

            #endregion

            #region Set Action

            bool showUserInfo = false;
            bool enableSave = true;
            bool enableDelete = true;
            if ((UtilsSecurity.IsAuthenticated()==false) && (isPublic ==false))
            {
                showUserInfo = true;
                enableSave = false;
                enableDelete = false;
            }

            if ((UtilsSecurity.HaveAdminRole() == true) && (dataId > 0) && (parentDataId == 0))
            {
                //showIsDeleted = true;
            }

            #endregion

            #region Get Ref Type List

            List<CNS_DataRefType> cnsDataRefTypeList = DataCommon.GetAllCnsDataRefType();

            List<TemplateDataSaveDetail.DataRefTypeItem> dataRefTypeItemList = new List<TemplateDataSaveDetail.DataRefTypeItem>();
            if (cnsDataRefTypeList.Count == 0)
            {
                dataRefTypeItemList.Add(new TemplateDataSaveDetail.DataRefTypeItem
                {
                    DataRefTypeText = "No RefType Found",
                    DataRefTypeValue = "1",
                    DataRefTypeSelected = true
                });
            }
            bool firstRecord = true;
            foreach (CNS_DataRefType cnsDataRefType in cnsDataRefTypeList)
            {
                if ((cnsDataRefType.DataRefTypeID == dataRefTypeId) || ((dataRefTypeId == 0) && (firstRecord == true)))
                {
                    dataRefTypeItemList.Add(new TemplateDataSaveDetail.DataRefTypeItem
                    {
                        DataRefTypeText = cnsDataRefType.DataRefType,
                        DataRefTypeValue = cnsDataRefType.DataRefTypeID.ToString(),
                        DataRefTypeSelected = true
                    });
                }
                else
                {
                    dataRefTypeItemList.Add(new TemplateDataSaveDetail.DataRefTypeItem
                    {
                        DataRefTypeText = cnsDataRefType.DataRefType,
                        DataRefTypeValue = cnsDataRefType.DataRefTypeID.ToString(),
                        DataRefTypeSelected = false
                    });
                }
                firstRecord = false;
            }
            

            #endregion

            #region Get Data Type List

            List<CNS_DataType> cnsDataTypeList = DataCommon.GetAllCnsDataType();
            List<TemplateDataSaveDetail.DataTypeItem> dataTypeItemList = new List<TemplateDataSaveDetail.DataTypeItem>();
            if (cnsDataTypeList.Count == 0)
            {
                dataTypeItemList.Add(new TemplateDataSaveDetail.DataTypeItem
                {
                    DataTypeText = "No DataType Found",
                    DataTypeValue = "1",
                    DataTypeSelected = true
                });
            }
            firstRecord = true;
            foreach (CNS_DataType cnsDataType in cnsDataTypeList)
            {
                if ((cnsDataType.DataTypeID == dataTypeId) || ((dataTypeId == 0) && (firstRecord == true)))
                {
                    dataTypeItemList.Add(new TemplateDataSaveDetail.DataTypeItem
                    {
                        DataTypeText = cnsDataType.DataType,
                        DataTypeValue = cnsDataType.DataTypeID.ToString(),
                        DataTypeSelected = true
                    });
                }
                else
                {
                    dataTypeItemList.Add(new TemplateDataSaveDetail.DataTypeItem
                    {
                        DataTypeText = cnsDataType.DataType,
                        DataTypeValue = cnsDataType.DataTypeID.ToString(),
                        DataTypeSelected = false
                    });
                }
                firstRecord = false;
            }
            
            #endregion

            #region Process Topken

            configToken = GetConfigToken(dataRefTypeId, dataRefId, dataTypeId, dataValueName, enableRefTypeId, enableRefId, enableDataTypeId, showRefType, showRefId, showDataType, showItemHeader, showItemEdit, showItemAppend, itemAddName, itemEditName, itemAppendName, isPublic);

            #endregion
           
            #region Set Template

            #region Is Active

            List<TemplateDataSaveDetail.IsActiveVisible.IsActive> isActiveList = new List<TemplateDataSaveDetail.IsActiveVisible.IsActive>();
            List<TemplateDataSaveDetail.IsActiveVisible.IsInActive> isInActiveList = new List<TemplateDataSaveDetail.IsActiveVisible.IsInActive>();
            if (dataIsActive == true)
            {
                isActiveList.Add(new TemplateDataSaveDetail.IsActiveVisible.IsActive
                {
                    DataIndex = dataIndex.ToString(),
                });

            }
            else
            {
                isInActiveList.Add(new TemplateDataSaveDetail.IsActiveVisible.IsInActive
                {
                    DataIndex = dataIndex.ToString(),
                });
            }

            List<TemplateDataSaveDetail.IsActiveVisible> isActiveVisibleList = new List<TemplateDataSaveDetail.IsActiveVisible>();
            if (dataId != 0)
            {
                isActiveVisibleList.Add(new TemplateDataSaveDetail.IsActiveVisible
                {
                    DataIndex = dataIndex.ToString(),
                    IsActiveList = isActiveList,
                    IsInActiveList = isInActiveList,
                });
            }
            #endregion

            #region Is Delete

            List<TemplateDataSaveDetail.IsDeleteVisible.IsDeleteChecked> isDeleteCheckedList = new List<TemplateDataSaveDetail.IsDeleteVisible.IsDeleteChecked>();
            List<TemplateDataSaveDetail.IsDeleteVisible.IsDelete> isDeleteList = new List<TemplateDataSaveDetail.IsDeleteVisible.IsDelete>();
            if (dataIsDeleted == true)
            {
                isDeleteCheckedList.Add(new TemplateDataSaveDetail.IsDeleteVisible.IsDeleteChecked
                {
                    DataIndex = dataIndex.ToString(),                   
                });

            }
            else
            {
                isDeleteList.Add(new TemplateDataSaveDetail.IsDeleteVisible.IsDelete
                {
                    DataIndex = dataIndex.ToString(),                   
                });
            }

            List<TemplateDataSaveDetail.IsDeleteVisible> isDeleteVisibleList = new List<TemplateDataSaveDetail.IsDeleteVisible>();
            if ((dataId != 0) && (UtilsSecurity.HaveAdminRole() ==true))
            {
                isDeleteVisibleList.Add(new TemplateDataSaveDetail.IsDeleteVisible
                {
                    DataIndex = dataIndex.ToString(),
                    IsDeleteCheckedList = isDeleteCheckedList,
                    IsDeleteList = isDeleteList,
                });
            }

            #endregion

            #region Action

            string addActionHtml = "";
            string editActionHtml = "";
            string appendActionHtml = "";
            if (dataId == 0)
            {
                var templateSaveAdd = new TemplateDataSaveDetailAdd
                {
                    AddActionDisabled = !enableSave,
                    DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                    PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                    ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                    TemplateSuffix = templateSuffix,
                    ConfigToken = configToken,
                };
                addActionHtml = templateSaveAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                var templateSaveEdit = new TemplateDataSaveDetailEdit
                {
                    Id = dataId.ToString(),
                    DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                    PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                    ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                    TemplateSuffix = templateSuffix,
                    ConfigToken = configToken,

                    SaveActionDisabled = !enableSave,
                    DeleteActionDisabled = !enableDelete,
                };
                editActionHtml = templateSaveEdit.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }

            #endregion

            #region Set Access

            if ((UtilsSecurity.HaveAdminRole() == true) && (UtilsSecurity.HaveAuthorRoleEnabled() ==true))
            {
                showRefId = true;
                showRefType = true;
                showDataType = true;
            }
            else
            {
                showRefId = false;
                showRefType = false;
                showDataType = false;
            }

            #endregion

            var templateSaveDetail = new TemplateDataSaveDetail
            {
                //Id = dataId.ToString("N0", CultureInfo.InvariantCulture),
                RevisionNo = revisionNo.ToString(),
                ParentDataId = parentDataId.ToString(),
                ParentDataIdHidden = !showRefId,
                ParentDataIdDisable = true,

                DataRefID = dataRefId.ToString(),
                DataRefIDHidden = !showRefId,
                DataRefIDDisable = true,

                DataRefTypeHidden = !showRefType,
                DataRefTypeDisable = !enableRefTypeId,
                DataRefTypeItemList = dataRefTypeItemList,

                DataTypeHidden = !showDataType,
                DataTypeDisable = !enableDataTypeId,
                DataTypeItemList = dataTypeItemList,

                DataValueName = dataValueName,
                DataValue = dataValue,
                IsActiveVisibleList = isActiveVisibleList,
                IsDeleteVisibleList = isDeleteVisibleList,

                AddAction = addActionHtml,
                EditAction = editActionHtml,
                AppendAction = appendActionHtml,
                ShowUserInfo = showUserInfo,   
            };

            string htmlSaveDetail = templateSaveDetail.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);

            #endregion

            retConfigToken = configToken;
            return htmlSaveDetail;
        }

        #endregion

        #region List View

        public static string GetListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken)
        {
            string htmlListScript = GetListScript();

            string htmlListDetail = GetListDetailView(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);

            string message;
            var templateList = new TemplateDataList
            {
                ListScript = htmlListScript,
                ListDetail = htmlListDetail
            };
            string htmlList = templateList.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlList;
        }

        public static string GetListDetailView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken)
        {
            string htmlListAllItem = GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken, 0, 0);

            string message;
            var templateListDetail = new TemplateDataListDetail
            {
                ListItem = htmlListAllItem,
                DataIndex = dataIndex.ToString()
            };
            string htmlListDetail = templateListDetail.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                            UtilsGeneric.ThrowException, out message);
         
            return htmlListDetail;
        }

        #endregion

        #region Item View

        public static string GetListAllItemView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken, long filterDataRefTypeId, long filterDataTypeId)
        {
            string message = "";
            if (itemsPerPage == 0) itemsPerPage = UtilsGeneric.DefaultItemsPerPage;
            long totalPages;
            long totalItems;
            string htmlTextItemList = "";
            string htmlAddItemList = "";
            string htmlFilterItemList = "";

            #region Retrieve Config Values

            long dataRefTypeId = 0;
            long dataRefId = 0;
            long dataTypeId = 0;
            string dataValueName = "Data";

            bool enableRefTypeId = false;
            bool enableRefId = false;
            bool enableDataTypeId = false;
            bool showRefType = false;
            bool showRefId = false;
            bool showDataType = false;

            bool showItemHeader = false;
            bool showItemEdit = false;
            bool showItemAppend = false;
            string itemAddName = "Add";
            string itemEditName = "Edit";
            string itemAppendName = "Append";
            bool isPublic = false;

            DataView.ProcessConfigToken(configToken, out dataRefTypeId, out dataRefId, out dataTypeId, out dataValueName, out enableRefTypeId, out enableRefId, out enableDataTypeId, out showRefType, out showRefId, out showDataType, out showItemHeader, out showItemEdit, out showItemAppend, out itemAddName, out itemEditName, out itemAppendName, out isPublic);

            #endregion

            if ((UtilsSecurity.IsAuthenticated() == false) && (isPublic ==false))
            {
                TemplateDataView dataView = new TemplateDataView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    DataValueName = dataValueName,
                };
                htmlTextItemList = dataView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                bool hideFilter = true;
                if (filterDataRefTypeId > 0) dataRefTypeId = filterDataRefTypeId;
                if (filterDataTypeId > 0) dataTypeId = filterDataTypeId;
                if ((filterDataRefTypeId > 0) || (filterDataTypeId > 0)) hideFilter = false;

                #region Filter Section

                #region Get Ref Type List

                List<CNS_DataRefType> cnsDataRefTypeList = DataCommon.GetAllCnsDataRefType();

                List<TemplateDataListDetailFilter.DataRefTypeItem> dataRefTypeItemList = new List<TemplateDataListDetailFilter.DataRefTypeItem>();
                if (cnsDataRefTypeList.Count == 0)
                {
                    dataRefTypeItemList.Add(new TemplateDataListDetailFilter.DataRefTypeItem
                    {
                        DataRefTypeText = "No RefType Found",
                        DataRefTypeValue = "1",
                        DataRefTypeSelected = true
                    });
                }
                bool firstRecord = true;
                foreach (CNS_DataRefType cnsDataRefType in cnsDataRefTypeList)
                {
                    if ((cnsDataRefType.DataRefTypeID == dataRefTypeId) || ((dataRefTypeId == 0) && (firstRecord == true)))
                    {
                        dataRefTypeItemList.Add(new TemplateDataListDetailFilter.DataRefTypeItem
                        {
                            DataRefTypeText = cnsDataRefType.DataRefType,
                            DataRefTypeValue = cnsDataRefType.DataRefTypeID.ToString(),
                            DataRefTypeSelected = true
                        });
                    }
                    else
                    {
                        dataRefTypeItemList.Add(new TemplateDataListDetailFilter.DataRefTypeItem
                        {
                            DataRefTypeText = cnsDataRefType.DataRefType,
                            DataRefTypeValue = cnsDataRefType.DataRefTypeID.ToString(),
                            DataRefTypeSelected = false
                        });
                    }
                    firstRecord = false;
                }


                #endregion

                #region Get Data Type List

                List<CNS_DataType> cnsDataTypeList = DataCommon.GetAllCnsDataType();
                List<TemplateDataListDetailFilter.DataTypeItem> dataTypeItemList = new List<TemplateDataListDetailFilter.DataTypeItem>();
                if (cnsDataTypeList.Count == 0)
                {
                    dataTypeItemList.Add(new TemplateDataListDetailFilter.DataTypeItem
                    {
                        DataTypeText = "No DataType Found",
                        DataTypeValue = "1",
                        DataTypeSelected = true
                    });
                }
                firstRecord = true;
                foreach (CNS_DataType cnsDataType in cnsDataTypeList)
                {
                    if ((cnsDataType.DataTypeID == dataTypeId) || ((dataTypeId == 0) && (firstRecord == true)))
                    {
                        dataTypeItemList.Add(new TemplateDataListDetailFilter.DataTypeItem
                        {
                            DataTypeText = cnsDataType.DataType,
                            DataTypeValue = cnsDataType.DataTypeID.ToString(),
                            DataTypeSelected = true
                        });
                    }
                    else
                    {
                        dataTypeItemList.Add(new TemplateDataListDetailFilter.DataTypeItem
                        {
                            DataTypeText = cnsDataType.DataType,
                            DataTypeValue = cnsDataType.DataTypeID.ToString(),
                            DataTypeSelected = false
                        });
                    }
                    firstRecord = false;
                }

                #endregion

                List<TemplateDataSaveAdd.FilterAction> filterActionList = new List<TemplateDataSaveAdd.FilterAction>();

                if (UtilsSecurity.HaveAdminRole() == true)
                {
                    filterActionList.Add(new TemplateDataSaveAdd.FilterAction
                    {
                        DataIndex = dataIndex.ToString(),                       
                    });

                    TemplateDataListDetailFilter listDetailFilter = new TemplateDataListDetailFilter
                    {
                        DataListFilterHidden = hideFilter,
                        DataRefTypeItemList = dataRefTypeItemList,
                        DataTypeItemList = dataTypeItemList,
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix,
                        ConfigToken = configToken,
                    };
                    htmlFilterItemList = listDetailFilter.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                #endregion

                #region Add Link

                TemplateDataSaveAdd dataSaveAdd = new TemplateDataSaveAdd
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    ConfigToken = configToken,
                    AddTitle = itemAddName,
                    FilterActionList = filterActionList,
                };
                htmlAddItemList = dataSaveAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);

                #endregion

                #region Get Fill List

                #region Get Paged Data

                List<CND_Data> cndDataList = DataCommon.GetAllParentCndData(dataRefTypeId, dataRefId, dataTypeId, pageNo, itemsPerPage, out totalPages, out totalItems);

                #endregion

                if (cndDataList.Count > 0)
                {
                    #region Get Pager Details

                    string configTokenParam = "'" + configToken + "'";
                    string topPagerDetails = UtilsGeneric.GetItemPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, RefreshListFunctionName, configTokenParam, false);
                    string bottomPagerDetails = UtilsGeneric.GetLinkPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, totalItems, RefreshListFunctionName, configTokenParam, false);

                    #endregion

                    #region Append Top Pager

                    if (topPagerDetails.Trim().Length > 0)
                    {
                        htmlTextItemList += topPagerDetails;
                    }

                    #endregion

                    #region Append Items

                    int index = 0;
                    for (; index < cndDataList.Count; index++)
                    {
                        CND_Data cndData = cndDataList[index];
                        string htmlTextItemTemplate = GetListSingleItemView(cndData, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);
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
                    htmlAddItemList = "";

                    List<TemplateDataListDetailEmpty.AddAction> addActionList = new List<TemplateDataListDetailEmpty.AddAction>();
                    addActionList.Add(new TemplateDataListDetailEmpty.AddAction
                    {
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix,
                        ConfigToken = configToken,
                    });

                    TemplateDataListDetailEmpty dataListDetailEmpty = new TemplateDataListDetailEmpty
                    {
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix,
                        ConfigToken = configToken,
                        DataValueName = dataValueName,
                        AddActionList = addActionList
                    };
                    htmlTextItemList = dataListDetailEmpty.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                #endregion
            }

            return htmlFilterItemList + htmlAddItemList + htmlTextItemList;
        }

        private static string GetListSingleItemView(CND_Data cndData, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken)
        {
            string htmlTextItem = "";
            if (cndData != null)
            {
                #region Retrieve Config Values

                long refTypeId = 0;
                long refId = 0;
                long dataTypeId = 0;
                string dataValueName = "Data";

                bool enableRefTypeId = false;
                bool enableRefId = false;
                bool enableDataTypeId = false;
                bool showRefType = false;
                bool showRefId = false;
                bool showDataType = false;

                bool showItemHeader = false;
                bool showItemEdit = false;
                bool showItemAppend = false;
                string itemAddName = "Add";
                string itemEditName = "Edit";
                string itemAppendName = "Append";
                bool isPublic = false;

                DataView.ProcessConfigToken(configToken, out refTypeId, out refId, out dataTypeId, out dataValueName, out enableRefTypeId, out enableRefId, out enableDataTypeId, out showRefType, out showRefId, out showDataType, out showItemHeader, out showItemEdit, out showItemAppend, out itemAddName, out itemEditName, out itemAppendName, out isPublic);

                #endregion

                string message;

                #region Edit Action

                List<TemplateDataListDetailItem.EditAction> editActionList = new List<TemplateDataListDetailItem.EditAction>();
                if (showItemEdit == true)
                {
                    if ((UtilsSecurity.IsAuthenticated() == true) || (isPublic == true))
                    {
                        if ((cndData.UserID == UtilsSecurity.GetUserId()) || (UtilsSecurity.HaveAdminRole() == true))
                        {
                            editActionList.Add(new TemplateDataListDetailItem.EditAction
                            {
                                Id = cndData.DataID.ToString(),
                                DataIndex = dataIndex.ToString(),
                                PageNo = pageNo.ToString(),
                                ItemsPerPage = itemsPerPage.ToString(),
                                TemplateSuffix = templateSuffix,
                                ConfigToken = configToken,
                                ItemEditName = itemEditName,
                            });
                        }
                        else
                        {
                            showItemEdit = false;
                        }
                    }
                    else
                    {
                        showItemEdit = false;
                    }
                }

                #endregion

                #region Append Action

                List<TemplateDataListDetailItem.AppendAction> appendActionList = new List<TemplateDataListDetailItem.AppendAction>();
                if (showItemAppend == true)
                {
                    if ((UtilsSecurity.IsAuthenticated() == true) || (isPublic ==true))
                    {
                        //Cannot Reply to Yourself, the User has to edit his comment only.
                        if ((cndData.UserID != UtilsSecurity.GetUserId()) || (UtilsSecurity.HaveAdminRole() == true))
                        {
                            appendActionList.Add(new TemplateDataListDetailItem.AppendAction
                            {
                                Id = cndData.DataID.ToString(),
                                DataIndex = dataIndex.ToString(),
                                PageNo = pageNo.ToString(),
                                ItemsPerPage = itemsPerPage.ToString(),
                                TemplateSuffix = templateSuffix,
                                ConfigToken = configToken,
                                ItemAppendName = itemAppendName,
                            });
                        }
                        else
                        {
                            showItemAppend = false;
                        }
                    }
                    else
                    {
                        showItemAppend = false;
                    }
                }

                #endregion

                #region Item Header

                List<TemplateDataListDetailItem.ItemHeaderVisible> itemHeaderVisibleList = new List<TemplateDataListDetailItem.ItemHeaderVisible>();
                if (showItemHeader == true)
                {
                    CNS_DataType cnsDataType = DataCommon.GetCnsDataType(cndData.DataTypeID);
                    string dataTypeName = cnsDataType.DataType;

                    itemHeaderVisibleList.Add(new TemplateDataListDetailItem.ItemHeaderVisible
                    {
                        DataTypeName = dataTypeName,
                        IsInActive = !cndData.IsActive,
                    });
                }

                #endregion
       
                string childHtmlTextItem = "";
                childHtmlTextItem = GetListChildDetailView(cndData, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);
                
                var templateItem = new TemplateDataListDetailItem
                {
                    ItemHeaderVisibleList = itemHeaderVisibleList,
                    DataValue = cndData.DataValue,
                    CreatedBy = UtilsSecurity.GetUserName(cndData.UserID),
                    CreatedDate = cndData.LastUpdateDate.ToString(UtilsGeneric.DefaultDateFormat),
                    
                    EditActionList = editActionList,
                    AppendActionList = appendActionList,
                    ChildItem = childHtmlTextItem
                };
                htmlTextItem = templateItem.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                        UtilsGeneric.ThrowException,
                                                                        out message);

            }
            return htmlTextItem;
        }

        #endregion       

        #region Child View

        private static long _childDataIndex = 0;
        private static string GetListChildDetailView(CND_Data parentCndData, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken)
        {
            string htmlListAllItem = GetListChildAllItemView(parentCndData, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);

            string message;
            if (_childDataIndex == 0){ _childDataIndex = dataIndex; }else{ _childDataIndex++; }
            var templateListDetail = new TemplateDataListDetail
            {
                ListItem = htmlListAllItem,
                DataIndex = _childDataIndex.ToString()
            };
            string htmlListDetail = templateListDetail.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                            UtilsGeneric.ThrowException, out message);

            return htmlListDetail;
        }

        private static string GetListChildAllItemView(CND_Data parentCndData, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken)
        {
            string message = "";
            long totalPages = 0;
            long totalItems = 0;
            string htmlTextItemList = "";

            #region Retrieve Config Values

            long refTypeId = 0;
            long refId = 0;
            long dataTypeId = 0;
            string dataValueName = "Data";

            bool enableRefTypeId = false;
            bool enableRefId = false;
            bool enableDataTypeId = false;
            bool showRefType = false;
            bool showRefId = false;
            bool showDataType = false;

            bool showItemHeader = false;
            bool showItemEdit = false;
            bool showItemAppend = false;
            string itemAddName = "Add";
            string itemEditName = "Edit";
            string itemAppendName = "Append";
            bool isPublic = false;

            DataView.ProcessConfigToken(configToken, out refTypeId, out refId, out dataTypeId, out dataValueName, out enableRefTypeId, out enableRefId, out enableDataTypeId, out showRefType, out showRefId, out showDataType, out showItemHeader, out showItemEdit, out showItemAppend, out itemAddName, out itemEditName, out itemAppendName, out isPublic);

            #endregion

            #region Get Fill List

            #region Get Paged Data

            List<CND_Data> cndDataList = DataCommon.GetAllChildCndData(refTypeId, refId, dataTypeId, parentCndData.DataID, pageNo, itemsPerPage, out totalPages, out totalItems);

            #endregion

            if (cndDataList.Count > 0)
            {
                #region Get Pager Details

                string configTokenParam = "'" + configToken + "'";
                string topPagerDetails = UtilsGeneric.GetItemPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, RefreshListFunctionName, configTokenParam, false);
                string bottomPagerDetails = UtilsGeneric.GetLinkPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, totalItems, RefreshListFunctionName, configTokenParam, false);

                #endregion

                #region Append Top Pager

                if (topPagerDetails.Trim().Length > 0)
                {
                    htmlTextItemList += topPagerDetails;
                }

                #endregion

                #region Append Items

                int index = 0;
                for (; index < cndDataList.Count; index++)
                {
                    CND_Data cndData = cndDataList[index];
                    string htmlTextItemTemplate = GetListSingleItemView(cndData, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);
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

            if (ShowEmptyChildData ==true)
            {
                TemplateDataListDetailChildEmpty dataListDetailChildEmpty = new TemplateDataListDetailChildEmpty
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    ConfigToken = configToken,
                    DataAppendName = itemAppendName,
                    DataValueName = dataValueName,
                };
                htmlTextItemList = dataListDetailChildEmpty.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            #endregion

            return htmlTextItemList;
        }

        #endregion

        #region Config Token

        public static bool isPublic(string configToken)
        {
            long dataRefTypeId = 0;
            long dataRefId = 0;
            long dataTypeId = 0;
            string dataValueName = "Data";

            bool enableRefTypeId = false;
            bool enableRefId = false;
            bool enableDataTypeId = false;
            bool showRefType = false;
            bool showRefId = false;
            bool showDataType = false;

            bool showItemHeader = false;
            bool showItemEdit = false;
            bool showItemAppend = false;
            string itemAddName = "Add";
            string itemEditName = "Edit";
            string itemAppendName = "Append";
            bool isPublic = false;

            DataView.ProcessConfigToken(configToken, out dataRefTypeId, out dataRefId, out dataTypeId, out dataValueName, out enableRefTypeId, out enableRefId, out enableDataTypeId, out showRefType, out showRefId, out showDataType, out showItemHeader, out showItemEdit, out showItemAppend, out itemAddName, out itemEditName, out itemAppendName, out isPublic);

            return isPublic;
        }

        private static string GetConfigToken(long dataRefTypeId, long dataRefId, long dataTypeId, string dataValueName, bool enableDataRefTypeId, bool enableDataRefId, bool enableDataTypeId, bool showDataRefType, bool showDataRefId, bool showDataType, bool showItemHeader, bool showItemEdit, bool showItemAppend, string itemAddName, string itemEditName, string itemAppendName, bool isPublic)
        {
            string configToken = "";

            configToken += dataRefTypeId.ToString(CultureInfo.InvariantCulture) + ",";
            configToken += dataRefId.ToString(CultureInfo.InvariantCulture) + ",";
            configToken += dataTypeId.ToString(CultureInfo.InvariantCulture) + ",";
            configToken += dataValueName + ",";

            configToken += enableDataRefTypeId.ToString(CultureInfo.InvariantCulture) + ",";
            configToken += enableDataRefId.ToString(CultureInfo.InvariantCulture) + ",";
            configToken += enableDataTypeId.ToString(CultureInfo.InvariantCulture) + ",";
        
            configToken += showDataRefType.ToString(CultureInfo.InvariantCulture) + ",";
            configToken += showDataRefId.ToString(CultureInfo.InvariantCulture) + ",";
            configToken += showDataType.ToString(CultureInfo.InvariantCulture) + ",";

            configToken += showItemHeader.ToString(CultureInfo.InvariantCulture) + ",";
            configToken += showItemEdit.ToString(CultureInfo.InvariantCulture) + ",";
            configToken += showItemAppend.ToString(CultureInfo.InvariantCulture) + ",";

            configToken += itemAddName + ",";
            configToken += itemEditName + ",";
            configToken += itemAppendName + ",";
            configToken += isPublic.ToString(CultureInfo.InvariantCulture) + ",";

            string base64ConfigToken = LZF.CompressToBase64(configToken);

            return base64ConfigToken;
        }

        private static void ProcessConfigToken(string base64ConfigToken, out long retDataRefTypeId, out long retDataRefId, out long retDataTypeId, out string retDataValueName, out bool retEnableDataRefTypeId, out bool retEnableDataRefId, out bool retEnableDataTypeId, out bool retShowDataRefType, out bool retShowDataRefId, out bool retShowDataType, out bool retShowItemHeader, out bool retShowItemEdit, out bool retShowItemAppend, out string retItemAddName, out string retItemEditName, out string retItemAppendName, out bool retIsPublic)
        {
            long dataRefTypeId;
            long dataRefId;
            long dataTypeId;
            string dataValueName;

            bool enableDataRefTypeId;
            bool enableDataRefId;
            bool enableDataTypeId ;
            bool showDataRefType;
            bool showDataRefId;
            bool showDataType;

            bool showItemHeader;
            bool showItemEdit;
            bool showItemAppend;
            string itemAddName;
            string itemEditName;
            string itemAppendName;
            bool isPublic;

            string configToken = LZF.DecompressFromBase64(base64ConfigToken);
            string[] configTokenArray = configToken.Split(',');

            long.TryParse(configTokenArray[0], out dataRefTypeId);
            long.TryParse(configTokenArray[1], out dataRefId);
            long.TryParse(configTokenArray[2], out dataTypeId);
            dataValueName = configTokenArray[3];

            bool.TryParse(configTokenArray[4], out enableDataRefTypeId);
            bool.TryParse(configTokenArray[5], out enableDataRefId);
            bool.TryParse(configTokenArray[6], out enableDataTypeId);
            bool.TryParse(configTokenArray[7], out showDataRefType);
            bool.TryParse(configTokenArray[8], out showDataRefId);
            bool.TryParse(configTokenArray[9], out showDataType);

            bool.TryParse(configTokenArray[10], out showItemHeader);
            bool.TryParse(configTokenArray[11], out showItemEdit);
            bool.TryParse(configTokenArray[12], out showItemAppend);
            itemAddName = configTokenArray[13];
            itemEditName = configTokenArray[14];
            itemAppendName = configTokenArray[15];
            bool.TryParse(configTokenArray[16], out isPublic);

            retDataRefTypeId = dataRefTypeId;
            retDataRefId = dataRefId;
            retDataTypeId = dataTypeId;
            retDataValueName = dataValueName;

            retEnableDataRefTypeId = enableDataRefTypeId;
            retEnableDataRefId = enableDataRefId;
            retEnableDataTypeId = enableDataTypeId;
            retShowDataRefType = showDataRefType;
            retShowDataRefId = showDataRefId;
            retShowDataType= showDataType;

            retShowItemHeader = showItemHeader;
            retShowItemEdit = showItemEdit;
            retShowItemAppend = showItemAppend;
            retItemAddName = itemAddName;
            retItemEditName = itemEditName;
            retItemAppendName = itemAppendName;
            retIsPublic = isPublic;
        }

        #endregion
    }
}