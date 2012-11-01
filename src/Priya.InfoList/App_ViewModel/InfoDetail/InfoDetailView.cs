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
    public static class InfoDetailView
    {
        #region Variables

        public const string ServiceUrl = "/Apps/InfoList/InfoDetail/JsonInfoDetail.ashx";
        public const string RefreshListFunctionName = "refreshInfoDetailList";

        #endregion

        #region Get Script

        private static string GetSaveScript()
        {
            string message;
            var templateSave = new TemplateInfoDetailSave
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
            var templateList = new TemplateInfoDetailList
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

        public static string GetView(long infoSectionId, long dataIndex, string templateSuffix, bool showSave, bool showList, bool loadSaveScript, bool loadListScript)
        {
            long id = 0;
            long pageNo = 1;
            long itemsPerPage = UtilsGeneric.DefaultItemsPerPage;

            #region Save View

            string htmlSaveView = "";
            htmlSaveView = GetSaveView(id, pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId, loadSaveScript, showSave);

            #endregion

            #region List View

            string htmlListView = "";
            htmlListView = GetListView(pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId, loadListScript, showList);

            #endregion            

            var templateView = new TemplateInfoDetail
            {
                //SaveExpand = (id == 0) ? "true" : "false",
                SaveDetail = htmlSaveView,
                //ListExpand = (id == 0) ? "false" : "true",
                ListDetail = htmlListView,
            };

            string message = "";
            string html = templateView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            return html;
        }

        #endregion

        #region Save View

        public static string GetSaveView(long infoDetailId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long infoSectionId, bool loadSaveScript, bool loadSave)
        {
            string htmlSaveScript = "";
            if (loadSaveScript == true)
            {
                htmlSaveScript = GetSaveScript();
            }
            string htmlSaveDetail = "";
            if (loadSave == true)
            {
                htmlSaveDetail = GetSaveDetailView(infoDetailId, pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId);
            }

            string message;
            bool hideDisplay = true;
            var templateSave = new TemplateInfoDetailSave
            {
                SaveScript = htmlSaveScript,
                SaveDetail = htmlSaveDetail,
                SaveViewHidden = hideDisplay,
                InfoSectionId = infoSectionId.ToString(),
            };
            string htmlSave = templateSave.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
          
            return htmlSave;
        }

        public static string GetSaveDetailView(long infoDetailId, long pageNo, long itemsPerDetail, long dataIndex, string templateSuffix, long infoSectionId)
        {
            string message = "";
            string htmlSaveDetail = "";
            long revisionNo = 0;
            string infoSectionName = "";
            string infoDetailName = "";
            string infoDetailDescription = "";
            bool isActive = true;
            bool isDeleted = false;
            long sequence = 0;
            bool showAdditional = (infoDetailId != 0);

            if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRoleEnabled() == false))
            {
                TemplateInfoDetailView view = new TemplateInfoDetailView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerDetail.ToString(),
                    TemplateSuffix = templateSuffix,
                    InfoSectionId = infoSectionId.ToString(),
                };
                htmlSaveDetail = view.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                #region Get Section Details

                if (infoSectionId > 0)
                {
                    LTD_InfoSection ltdSectionExisting = DataInfoList.GetLtdInfoSection(infoSectionId);
                    if (ltdSectionExisting != null)
                    {
                        infoSectionName = ltdSectionExisting.InfoSectionName;
                    }
                }

                #endregion

                #region Get Detail Details

                if (infoDetailId > 0)
                {
                    LTD_InfoDetail ltdDetailExisting = DataInfoList.GetLtdInfoDetail(infoDetailId);
                    if (ltdDetailExisting != null)
                    {
                        infoDetailName = ltdDetailExisting.InfoDetailName;
                        infoDetailDescription = ltdDetailExisting.InfoDetailDescription;
                        isActive = ltdDetailExisting.IsActive;
                        isDeleted = ltdDetailExisting.IsDeleted;
                        sequence = ltdDetailExisting.Sequence;
                        revisionNo = ltdDetailExisting.RevisionNo;
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

                List<TemplateInfoDetailSaveDetail.AdditionalVisible> additionalVisibleList = new List<TemplateInfoDetailSaveDetail.AdditionalVisible>();
                if (showAdditional == true)
                {
                    additionalVisibleList.Add(new TemplateInfoDetailSaveDetail.AdditionalVisible
                    {                        
                        IsActiveHidden = (infoDetailId == 0),
                        IsActive = isActive,                        
                        IsDeletedHidden = !showDeleted,
                        IsDeleted = isDeleted,
                        SequenceHidden = (infoDetailId == 0),
                        Sequence = sequence.ToString(),
                    });
                }

                #endregion

                #region Action

                string addActionHtml = "";
                string editActionHtml = "";
                if (infoDetailId == 0)
                {
                    var saveDetailAdd = new TemplateInfoDetailSaveDetailAdd
                    {
                        AddActionDisabled = !enableSave,
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerDetail.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                        InfoSectionId = infoSectionId.ToString(),
                    };
                    addActionHtml = saveDetailAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }
                else
                {
                    var templateSaveEdit = new TemplateInfoDetailSaveDetailEdit
                    {
                        Id = infoDetailId.ToString(),
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerDetail.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                        InfoSectionId = infoSectionId.ToString(),
                        SaveActionDisabled = !enableSave,
                        DeleteActionDisabled = !enableDelete,
                    };
                    editActionHtml = templateSaveEdit.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                #endregion

                var templateSaveDetail = new TemplateInfoDetailSaveDetail
                {
                    //Id = infoSectionId.ToString("N0", CultureInfo.InvariantCulture),
                    RevisionNo = revisionNo.ToString(),
                    
                    InfoSectionName = infoSectionName,
                    InfoDetailName = infoDetailName,
                    InfoDetailDescription = infoDetailDescription,                    
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

        public static string GetListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long infoSectionId, bool loadListScript, bool loadList)
        {
            string htmlListScript = "";
            if (loadListScript == true)
            {
                htmlListScript = GetListScript();
            }
            string htmlListDetail = GetListDetailView(pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId, loadList);

            string message;
            var templateList = new TemplateInfoDetailList
            {
                ListScript = htmlListScript,
                ListDetail = htmlListDetail,
                InfoSectionId = infoSectionId.ToString()
            };
            string htmlList = templateList.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlList;
        }

        public static string GetListDetailView(long pageNo, long itemsPerDetail, long dataIndex, string templateSuffix, long infoSectionId, bool loadList)
        {
            string htmlListAllItem = "";
            if (loadList == true)
            {
                htmlListAllItem = GetListAllItemView(pageNo, itemsPerDetail, dataIndex, templateSuffix, infoSectionId);
            }

            string message;
            var templateListDetail = new TemplateInfoDetailListDetail
            {
                ListItem = htmlListAllItem,
                InfoSectionId = infoSectionId.ToString()
            };
            string htmlListDetail = templateListDetail.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                            UtilsGeneric.ThrowException, out message);
         
            return htmlListDetail;
        }

        #endregion

        #region Item View

        public static string GetListAllItemView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long infoSectionId)
        {
            string message = "";
            if (itemsPerPage == 0) itemsPerPage = UtilsGeneric.DefaultInnerItemsPerPage;
            long totalDetails;
            long totalItems;
            string htmlTextItemList = "";
            string htmlAddItemList = "";

            #region Add Link

            if (UtilsSecurity.HaveAuthorRoleEnabled() == true)
            {
                TemplateInfoDetailSaveAdd saveAdd = new TemplateInfoDetailSaveAdd
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    InfoSectionId = infoSectionId.ToString(),
                };
                htmlAddItemList = saveAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }

            #endregion

            #region Get Fill List

            #region Get Detaild Data

            List<LTD_InfoDetail> ltdDetailList = new List<LTD_InfoDetail>();
            if ((UtilsSecurity.HaveAdminRole() == true) || (UtilsSecurity.HaveAuthorRoleEnabled() == true))
            {
                ltdDetailList = DataInfoList.GetPagedLtdInfoDetail(infoSectionId, false, pageNo, itemsPerPage, out totalDetails, out totalItems);
            }
            else
            {
                ltdDetailList = DataInfoList.GetPagedLtdInfoDetail(infoSectionId, true, pageNo, itemsPerPage, out totalDetails, out totalItems);
            }

            #endregion

            if (ltdDetailList.Count > 0)
            {
                #region Get Detail Details

                string topDetailrDetails = UtilsGeneric.GetItemPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalDetails, RefreshListFunctionName, infoSectionId.ToString());
                string bottomDetailrDetails = UtilsGeneric.GetLinkPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalDetails, totalItems, RefreshListFunctionName, infoSectionId.ToString(), false);

                #endregion

                #region Append Top Details

                if (topDetailrDetails.Trim().Length > 0)
                {
                    htmlTextItemList += topDetailrDetails;
                }

                #endregion

                #region Append Items

                int index = 0;
                for (; index < ltdDetailList.Count; index++)
                {
                    LTD_InfoDetail ltdDetail = ltdDetailList[index];
                    string htmlTextItemTemplate = GetListSingleItemView(ltdDetail, pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId);
                    htmlTextItemList += htmlTextItemTemplate;
                }

                #endregion

                #region Append Bottom Detailr

                if (bottomDetailrDetails.Trim().Length > 0)
                {
                    htmlTextItemList += bottomDetailrDetails;
                }

                #endregion
            }

            #endregion

            #region Set Fill List

            if ((htmlTextItemList.Length == 0) && (UtilsSecurity.HaveAuthorRoleEnabled() == true))
            {
                //TemplateInfoDetailListDetailEmpty listDetailEmpty = new TemplateInfoDetailListDetailEmpty
                //{
                //    DataIndex = dataIndex.ToString(),
                //    PageNo = pageNo.ToString(),
                //    ItemsPerPage = itemsPerPage.ToString(),
                //    TemplateSuffix = templateSuffix,
                //    InfoSectionId = infoSectionId.ToString(),
                //};
                //htmlTextItemList = listDetailEmpty.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }

            #endregion

            return htmlAddItemList + htmlTextItemList;
        }

        private static string GetListSingleItemView(LTD_InfoDetail ltdDetail, long pageNo, long itemsPerDetail, long dataIndex, string templateSuffix, long infoSectionId)
        {
            string htmlTextItem = "";
            if (ltdDetail != null)
            {
                string message;
                List<TemplateInfoDetailListDetailItem.EditAction> editActionList = new List<TemplateInfoDetailListDetailItem.EditAction>();
                if ((UtilsSecurity.HaveAdminRole() == true) || (UtilsSecurity.HaveAuthorRoleEnabled() == true))
                {
                    editActionList.Add(new TemplateInfoDetailListDetailItem.EditAction
                    {
                        Id = ltdDetail.InfoDetailID.ToString(),
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerDetail.ToString(),
                        TemplateSuffix = templateSuffix,
                        InfoSectionId = infoSectionId.ToString(),
                    });
                }

                var templateItem = new TemplateInfoDetailListDetailItem
                {
                    InfoDetailName = ltdDetail.InfoDetailName,
                    InfoDetailDescription = ltdDetail.InfoDetailDescription,
                    IsInActive = !ltdDetail.IsActive,

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