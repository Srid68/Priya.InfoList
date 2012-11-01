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
    public static class SubscriberView
    {
        public const string ServiceUrl = "/Apps/InfoList/Subscriber/JsonSubscriber.ashx";
        public const string RefreshListFunctionName = "refreshSubscriberList";
        //public const string GetSaveFunctionName = "getSubscriberSaveView";

        #region Get Script

        private static string GetSaveScript()
        {
            string message;
            var templateSave = new TemplateSubscriberSave
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
            var templateList = new TemplateSubscriberList
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

            string htmlListView = "";
            if (UtilsSecurity.HaveAdminRole() == true)
            {
                htmlListView = GetListView(pageNo, itemsPerPage, dataIndex, templateSuffix);
            }

            #endregion            

            long listCount = DataInfoList.GetCountLtdSubscriberId();

            var templateSubscriber = new TemplateSubscriber
            {
                //SaveExpand = (id == 0) ? "true" : "false",
                SaveDetail = htmlSaveView,
                //ListExpand = (id == 0) ? "false" : "true",
                ListDetail = htmlListView,
                ListCount = listCount.ToString()
            };

            string message = "";
            string html = templateSubscriber.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            return html;
        }

        #endregion

        #region Save View

        public static string GetSaveView(long subscriberId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlSaveScript = GetSaveScript();

            string htmlSaveDetail = GetSaveDetailView(subscriberId, pageNo, itemsPerPage, dataIndex, templateSuffix);

            string message;
            var templateSave = new TemplateSubscriberSave
            {
                SaveScript = htmlSaveScript,
                SaveDetail = htmlSaveDetail
            };
            string htmlSave = templateSave.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
          
            return htmlSave;
        }

        public static string GetSaveDetailView(long subscriberId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string message;
            string htmlSaveDetail = "";
            long revisionNo = 0;
            string subscriberEmail = "";
            string subscriberMessage = "";
            bool subscriberIsDeleted = false;

            long dataCount = DataInfoList.GetCountLtdSubscriberId();
            if (dataCount > 0)
            {
                revisionNo = dataCount;
            }

            #region Get Subscriber Details

            if (subscriberId > 0)
            {
                LTD_Subscriber ltdSubscriberExisting = DataInfoList.GetLtdSubscriber(subscriberId);
                if (ltdSubscriberExisting != null)
                {
                    subscriberEmail = ltdSubscriberExisting.SubscriberEmail;
                    subscriberMessage = ltdSubscriberExisting.SubscriberMessage;
                    subscriberIsDeleted = ltdSubscriberExisting.IsDeleted;
                    revisionNo = ltdSubscriberExisting.RevisionNo;
                }
            }

            #endregion

            #region Set Action

            bool showUserInfo =false;
            bool enableSave = true;
            bool enableDelete = true;           

            #endregion

            #region Set Template

            string addActionHtml = "";
            string editActionHtml = "";
            if (subscriberId == 0)
            {
                var templateSaveAdd = new TemplateSubscriberSaveDetailAdd
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


                var templateSaveEdit = new TemplateSubscriberSaveDetailEdit
                {
                    Id = subscriberId.ToString(),
                    DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                    PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                    ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                    TemplateSuffix = templateSuffix,
                    SaveActionDisabled = !enableSave,
                    DeleteActionDisabled = !enableDelete,
                };
                editActionHtml = templateSaveEdit.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }

            List<TemplateSubscriberSaveDetail.IsDeletedVisible> isDeletedVisibleList = new List<TemplateSubscriberSaveDetail.IsDeletedVisible>();
            if (subscriberId != 0)
            {
                isDeletedVisibleList.Add(new TemplateSubscriberSaveDetail.IsDeletedVisible
                {
                    IsDeleted = subscriberIsDeleted,                   
                });
            }

            var templateSaveDetail = new TemplateSubscriberSaveDetail
            {
                //Id = subscriberId.ToString("N0", CultureInfo.InvariantCulture),
                RevisionNo = revisionNo.ToString(),
                SubscriberEmailDisable = (subscriberId !=0),
                SubscriberMessage = subscriberMessage,
                IsDeletedVisibleList = isDeletedVisibleList,

                //AddMode = (subscriberId == 0) ? true : false,
                AddAction = addActionHtml,
                EditAction = editActionHtml,
                ShowUserInfo = showUserInfo,  
            };

            htmlSaveDetail = templateSaveDetail.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                    out message);

            #endregion

            return htmlSaveDetail;
        }

        #endregion

        #region List View

        public static string GetListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlListScript = GetListScript();
            string htmlListDetail = GetListDetailView(pageNo, itemsPerPage, dataIndex, templateSuffix);

            string message;
            var templateList = new TemplateSubscriberList
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
            var templateListDetail = new TemplateSubscriberListDetail
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

            if (UtilsSecurity.HaveAdminRole() == false)
            {
                //TemplateSubscriberView subscriberView = new TemplateSubscriberView
                //{
                //    DataIndex = dataIndex.ToString(),
                //    PageNo = pageNo.ToString(),
                //    ItemsPerPage = itemsPerPage.ToString(),
                //    TemplateSuffix = templateSuffix                    
                //};
                //htmlTextItemList = subscriberView.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            else
            {
                #region Get Fill List

                #region Get Paged Data

                List<LTD_Subscriber> ltdSubscriberList = DataInfoList.GetAllLtdSubscriber(pageNo, itemsPerPage, out totalPages, out totalItems);

                #endregion

                if (ltdSubscriberList.Count > 0)
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
                    for (; index < ltdSubscriberList.Count; index++)
                    {
                        LTD_Subscriber ltdSubscriber = ltdSubscriberList[index];
                        string htmlTextItemTemplate = GetListSingleItemView(ltdSubscriber, pageNo, itemsPerPage, dataIndex, templateSuffix);
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

                if ((htmlTextItemList.Length == 0) && (UtilsSecurity.HaveAdminRole() == true))
                {
                    TemplateSubscriberListDetailEmpty subscriberListDetailEmpty = new TemplateSubscriberListDetailEmpty
                    {
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix
                    };
                    htmlTextItemList = subscriberListDetailEmpty.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }
                #endregion
            }

            return htmlTextItemList;
        }

        private static string GetListSingleItemView(LTD_Subscriber ltdSubscriber, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlTextItem = "";
            if (ltdSubscriber != null)
            {
                string message;
                List<TemplateSubscriberListDetailItem.EditAction> editActionList = new List<TemplateSubscriberListDetailItem.EditAction>();
                if (UtilsSecurity.HaveAdminRole() == true)
                {
                    editActionList.Add(new TemplateSubscriberListDetailItem.EditAction
                    {
                        Id = ltdSubscriber.SubscriberID.ToString(),
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix
                    });
                }

                var templateItem = new TemplateSubscriberListDetailItem
                {
                    SubscriberEmail = ltdSubscriber.SubscriberEmail,
                    SubscriberMessage = ltdSubscriber.SubscriberMessage,
                    //IsDeleted = ltdSubscriber.IsDeleted

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