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
    public class InfoPageView
    {
        #region Variables

        public const string ServiceUrl = "/Apps/InfoList/InfoPage/JsonInfoPage.ashx";
        public const string RefreshListFunctionName = "refreshInfoPageList";

        #endregion

        #region Get Script

        private static string GetSaveScript()
        {
            string message;
            var templateSave = new TemplateInfoPageSave
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
            var templateList = new TemplateInfoPageList
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
            //htmlSaveView += InfoSectionView.GetView(0, dataIndex + 1, templateSuffix, false, false, true, true);
            htmlSaveView += InfoSectionView.GetScript(true, true);

            #endregion

            #region List View

            string htmlListView = GetListView(pageNo, itemsPerPage, dataIndex, templateSuffix);

            #endregion

            var templateMain = new TemplateInfoPage
            {
                //SaveExpand = (id == 0) ? "true" : "false",
                SaveDetail = htmlSaveView,
                //ListExpand = (id == 0) ? "false" : "true",
                ListDetail = htmlListView,
            };

            string message = "";
            string html = templateMain.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            return html;
        }

        #endregion

        #region Save View

        public static string GetSaveView(long infoPageId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlSaveScript = GetSaveScript();
            string htmlSaveDetail = ""; //GetSaveDetailView(infoPageId, pageNo, itemsPerPage, dataIndex);

            string message;
            bool hideDisplay = true;
            var templateSave = new TemplateInfoPageSave
            {
                DataIndex = dataIndex.ToString(),
                SaveScript = htmlSaveScript,
                SaveDetail = htmlSaveDetail,
                SaveViewHidden = hideDisplay,
            };
            string htmlSave = templateSave.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);

            return htmlSave;
        }

        public static string GetSaveDetailView(long infoPageId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string message = "";
            string htmlSaveDetail = "";
            bool firstRecord = true;
            bool showAdditional = (infoPageId != 0);

            long revisionNo = 0;

            string infoPageName = "";
            string infoPageDescription = "";

            long accessGroupId = 0;
            bool asyncLoading = false;
            bool commentable = false;
            string commentorRoleList = UtilsSecurity.AdminRole;

            bool isActive = true;
            DateTime expiryDate = DateTime.Now.AddYears(3);

            bool isPublic = false;
            bool isCommon = false;
            bool isDeleted = false;
            long sequence = 0;


            if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRoleEnabled() == false))
            {
                TemplateInfoPageView templateView = new TemplateInfoPageView
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    AsyncLoading = asyncLoading.ToString().ToLower()
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
                        infoPageDescription = ltdInfoPageExisting.InfoPageDescription;
                        expiryDate = ltdInfoPageExisting.ExpiryDate;
                        commentable = ltdInfoPageExisting.Commentable;
                        commentorRoleList = ltdInfoPageExisting.CommentorRoleList;
                        asyncLoading = ltdInfoPageExisting.AsyncLoading;
                        isPublic = ltdInfoPageExisting.IsPublic;
                        isCommon = ltdInfoPageExisting.IsCommon;
                        isActive = ltdInfoPageExisting.IsActive;
                        isDeleted = ltdInfoPageExisting.IsDeleted;
                        sequence = ltdInfoPageExisting.Sequence;
                        if (ltdInfoPageExisting.AccessGroupID.HasValue == true)
                        {
                            accessGroupId = ltdInfoPageExisting.AccessGroupID.Value;
                        }
                        revisionNo = ltdInfoPageExisting.RevisionNo;
                    }
                }

                #endregion

                #region Get Category Option List

                List<LTD_InfoCategory> ltdInfoCategoryList = DataInfoList.GetAllActiveLtdInfoCategory();
                long infoCategoryId = 0;
                if (infoPageId > 0)
                {
                    LTD_InfoPage ltdInfoPage = DataInfoList.GetLtdInfoPage(infoPageId);
                    if (ltdInfoPage != null)
                    {
                        infoCategoryId = ltdInfoPage.InfoCategoryID;
                    }
                }


                firstRecord = true;
                List<TemplateInfoPageSaveDetail.InfoCategoryItem> infoCategoryItemList = new List<TemplateInfoPageSaveDetail.InfoCategoryItem>();
                if (ltdInfoCategoryList.Count == 0)
                {
                    infoCategoryItemList.Add(new TemplateInfoPageSaveDetail.InfoCategoryItem
                    {
                        InfoCategoryText = "Add Info Category",
                        InfoCategoryValue = "0",
                        InfoCategorySelected = true,
                        InfoCategoryDisable = false,
                    });
                }

                if (infoCategoryId != 0)
                {
                    LTD_InfoCategory ltdInfoCategory = DataInfoList.GetLtdInfoCategory(infoCategoryId);
                    if (ltdInfoCategory != null)
                    {
                        infoCategoryItemList.Add(new TemplateInfoPageSaveDetail.InfoCategoryItem
                        {
                            InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                            InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                            InfoCategorySelected = true,
                            InfoCategoryDisable = !ltdInfoCategory.IsActive
                        });
                        firstRecord = false;
                    }
                }

                foreach (LTD_InfoCategory ltdInfoCategory in ltdInfoCategoryList)
                {
                    if (ltdInfoCategory.IsDefault == true)
                    {
                        infoCategoryItemList.Add(new TemplateInfoPageSaveDetail.InfoCategoryItem
                        {
                            InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                            InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                            InfoCategorySelected = firstRecord,
                            InfoCategoryDisable = !ltdInfoCategory.IsActive
                        });
                        firstRecord = false;
                        break;
                    }
                }

                foreach (LTD_InfoCategory ltdInfoCategory in ltdInfoCategoryList)
                {
                    if (ltdInfoCategory.IsDefault == false)
                    {
                        if (ltdInfoCategory.InfoCategoryID != infoCategoryId)
                        {
                            if ((infoCategoryId == 0) && (firstRecord == true))
                            {
                                infoCategoryItemList.Add(new TemplateInfoPageSaveDetail.InfoCategoryItem
                                {
                                    InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                                    InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                                    InfoCategorySelected = true,
                                    InfoCategoryDisable = !ltdInfoCategory.IsActive
                                });
                            }
                            else
                            {
                                infoCategoryItemList.Add(new TemplateInfoPageSaveDetail.InfoCategoryItem
                                {
                                    InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                                    InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                                    InfoCategorySelected = false,
                                    InfoCategoryDisable = !ltdInfoCategory.IsActive
                                });
                            }
                        }
                    }
                    firstRecord = false;
                }

                #endregion

                #region Get Add Info Category List

                List<TemplateInfoPageSaveDetail.AddInfoCategory> addInfoCategoryList = new List<TemplateInfoPageSaveDetail.AddInfoCategory>();
                string htmlInfoCategorySaveDetail = "";
                if (UtilsSecurity.HaveAdminRole() == true)
                {
                    long infoCategoryDataIndex = dataIndex + 10;

                    #region Add Info Category

                    addInfoCategoryList.Add(new TemplateInfoPageSaveDetail.AddInfoCategory
                    {
                        DataIndex = infoCategoryDataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix,
                    });

                    #endregion

                    #region Set Info Category Save Details

                    string refreshCallback = "refreshInfoPageCategoryOptionList(" + infoPageId + ", '" + templateSuffix + "')";
                    htmlInfoCategorySaveDetail = InfoCategoryView.GetSaveView(infoCategoryId, pageNo, itemsPerPage, infoCategoryDataIndex, templateSuffix, true, refreshCallback);

                    #endregion
                }

                #endregion

                #region Set Action

                bool showAdmin = false;
                bool showUserInfo = false;
                bool enableSave = true;
                bool enableDelete = true;
                if (UtilsSecurity.IsAuthenticated() == false)
                {
                    showUserInfo = true;
                    enableSave = false;
                    enableDelete = false;
                }

                showAdmin = (UtilsSecurity.HaveAdminRole() && (infoPageId > 0));

                #endregion

                #region Set Template

                #region More Details

                string saveDetailMore = "";
                if (showAdditional == true)
                {
                    #region Get Group Option List

                    List<SCD_Group> scdGroupList = UtilsSecurity.GetAllUserActiveScdGroup();
                    firstRecord = true;
                    List<TemplateInfoPageSaveDetailMore.AccessGroupItem> groupItemList = new List<TemplateInfoPageSaveDetailMore.AccessGroupItem>();
                    if (scdGroupList.Count == 0)
                    {
                        groupItemList.Add(new TemplateInfoPageSaveDetailMore.AccessGroupItem
                        {
                            AccessGroupText = "No Group Found",
                            AccessGroupValue = "0",
                            AccessGroupSelected = true
                        });
                    }
                    else
                    {
                        groupItemList.Add(new TemplateInfoPageSaveDetailMore.AccessGroupItem
                        {
                            AccessGroupText = "Select Access Group",
                            AccessGroupValue = "0",
                            AccessGroupSelected = (accessGroupId == 0)
                        });
                        firstRecord = false;
                    }

                    foreach (SCD_Group scdGroup in scdGroupList)
                    {
                        if ((scdGroup.GroupID == accessGroupId) || ((accessGroupId == 0) && (firstRecord == true)))
                        {
                            groupItemList.Add(new TemplateInfoPageSaveDetailMore.AccessGroupItem
                            {
                                AccessGroupText = scdGroup.GroupName,
                                AccessGroupValue = scdGroup.GroupID.ToString(CultureInfo.InvariantCulture),
                                AccessGroupSelected = true
                            });
                        }
                        else
                        {
                            groupItemList.Add(new TemplateInfoPageSaveDetailMore.AccessGroupItem
                            {
                                AccessGroupText = scdGroup.GroupName,
                                AccessGroupValue = scdGroup.GroupID.ToString(CultureInfo.InvariantCulture),
                                AccessGroupSelected = false
                            });
                        }
                        firstRecord = false;
                    }

                    #endregion

                    #region Get Role List

                    string[] roleList = UtilsSecurity.GetAllRoleList();
                    if (string.IsNullOrEmpty(commentorRoleList) == true) commentorRoleList = "";

                    firstRecord = true;
                    List<TemplateInfoPageSaveDetailMore.CommentorRoleItem> commentorlRoleItemList = new List<TemplateInfoPageSaveDetailMore.CommentorRoleItem>();
                    foreach (string role in roleList)
                    {
                        if ((commentorRoleList.Contains(role)) || ((commentorRoleList.Trim().Length == 0) && (firstRecord == true)))
                        {
                            commentorlRoleItemList.Add(new TemplateInfoPageSaveDetailMore.CommentorRoleItem
                            {
                                CommentorRoleText = role,
                                CommentorRoleValue = role,
                                CommentorRoleSelected = true
                            });
                        }
                        else
                        {
                            commentorlRoleItemList.Add(new TemplateInfoPageSaveDetailMore.CommentorRoleItem
                            {
                                CommentorRoleText = role,
                                CommentorRoleValue = role,
                                CommentorRoleSelected = false
                            });
                        }
                        firstRecord = false;
                    }


                    #endregion

                    var templateSaveDetailMore = new TemplateInfoPageSaveDetailMore
                    {
                        AccessGroupItemList = groupItemList,
                        AsyncLoading = asyncLoading,
                        Commentable = commentable,
                        CommentorRoleItemList = commentorlRoleItemList,

                        IsActiveHidden = (infoPageId == 0),
                        IsActive = isActive,
                        ExpiryDate = expiryDate.ToString(UtilsGeneric.DefaultDateFormat),

                        IsPublicHidden = !showAdmin,
                        IsPublic = isPublic,
                        IsCommonHidden = !showAdmin,
                        IsCommon = isCommon,
                        IsDeletedHidden = !showAdmin,
                        IsDeleted = isDeleted,
                        SequenceHidden = (infoPageId == 0),
                        Sequence = sequence.ToString(),
                    };

                    saveDetailMore = templateSaveDetailMore.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                          out message);

                }

                #endregion

                #region Action

                string addActionHtml = "";
                string editActionHtml = "";
                if (infoPageId == 0)
                {
                    var templateSaveDetailAdd = new TemplateInfoPageSaveDetailAdd
                    {
                        AddActionDisabled = !enableSave,
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                        AsyncLoading = asyncLoading.ToString().ToLower()
                    };
                    addActionHtml = templateSaveDetailAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }
                else
                {

                    var templateSaveDetailEdit = new TemplateInfoPageSaveDetailEdit
                    {
                        Id = infoPageId.ToString(),
                        DataIndex = dataIndex.ToString("N0", CultureInfo.InvariantCulture),
                        PageNo = pageNo.ToString("N0", CultureInfo.InvariantCulture),
                        ItemsPerPage = itemsPerPage.ToString("N0", CultureInfo.InvariantCulture),
                        TemplateSuffix = templateSuffix,
                        AsyncLoading = asyncLoading.ToString().ToLower(),

                        SaveActionDisabled = !enableSave,
                        DeleteActionDisabled = !enableDelete,
                    };
                    editActionHtml = templateSaveDetailEdit.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
                }

                #endregion

                var templateSaveDetail = new TemplateInfoPageSaveDetail
                {
                    AddInfoCategoryList = addInfoCategoryList,
                    InfoCategorySaveDetail = htmlInfoCategorySaveDetail,

                    //Id = infoPageId.ToString("N0", CultureInfo.InvariantCulture),
                    RevisionNo = revisionNo.ToString(),

                    InfoPageName = infoPageName,
                    InfoPageDescription = infoPageDescription,
                    InfoCategoryItemList = infoCategoryItemList,
                    //InfoCategorySelectDisable = (infoPageId > 0),

                    //AddMode = (infoPageId == 0) ? true : false,
                    AddAction = addActionHtml,
                    EditAction = editActionHtml,
                    ShowUserInfo = showUserInfo,

                    AdditionalActionVisible = showAdditional,
                    MoreDetails = saveDetailMore,
                };

                htmlSaveDetail = templateSaveDetail.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                      out message);

                #endregion
            }

            return htmlSaveDetail;
        }

        #endregion

        #region List View

        public static string GetListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlListScript = GetListScript();
            string htmlListDetail = GetListDetailView(pageNo, itemsPerPage, dataIndex, templateSuffix);

            string message;
            var templateList = new TemplateInfoPageList
            {
                DataIndex = dataIndex.ToString(),
                ListScript = htmlListScript,
                ListDetail = htmlListDetail
            };
            string htmlList = templateList.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                  out message);
            return htmlList;
        }

        public static string GetListDetailView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            string htmlListAllItem = GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, true, 0, "", false, 0, true);

            string message;
            var templateListDetail = new TemplateInfoPageListDetail
            {
                DataIndex = dataIndex.ToString(),
                ListItem = htmlListAllItem
            };
            string htmlListDetail = templateListDetail.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                            UtilsGeneric.ThrowException, out message);

            return htmlListDetail;
        }

        #endregion

        #region Item View

        public static string GetListAllItemView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, bool asyncLoading, long filterInfoCategoryId, string filterInfoPage, bool filterInfoPagePublic, long filterCreatedUserId, bool hideFilter)
        {
            string message = "";
            if (itemsPerPage == 0) itemsPerPage = UtilsGeneric.DefaultItemsPerPage;
            long totalPages;
            long totalItems;
            string htmlTextItemList = "";
            string htmlAddItemList = "";
            string htmlFilterItemList = "";

            //if ((filterInfoCategoryId > 0) || (filterInfoPage.Trim().Length > 0) || (filterInfoPagePublic == true)) hideFilter = false;

            #region Filter Section

            List<TemplateInfoPageListDetailFilter.ShowPublicChecked> showPublicCheckedList = new List<TemplateInfoPageListDetailFilter.ShowPublicChecked>();
            List<TemplateInfoPageListDetailFilter.ShowPublicUnChecked> showPublicUnCheckedList = new List<TemplateInfoPageListDetailFilter.ShowPublicUnChecked>();

            if (filterInfoPagePublic == true)
            {
                showPublicCheckedList.Add(new TemplateInfoPageListDetailFilter.ShowPublicChecked
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    AsyncLoading = asyncLoading.ToString().ToLower()
                });
            }
            else
            {
                showPublicUnCheckedList.Add(new TemplateInfoPageListDetailFilter.ShowPublicUnChecked
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    AsyncLoading = asyncLoading.ToString().ToLower()
                });
            }

            #region Get Info Category List

            List<LTD_InfoCategory> ltdInfoCategoryList = DataInfoList.GetAllActiveLtdInfoCategory();

            bool firstRecord = true;
            List<TemplateInfoPageListDetailFilter.InfoCategoryItem> infoCategoryItemList = new List<TemplateInfoPageListDetailFilter.InfoCategoryItem>();
            if (ltdInfoCategoryList.Count == 0)
            {
                infoCategoryItemList.Add(new TemplateInfoPageListDetailFilter.InfoCategoryItem
                {
                    InfoCategoryText = "No Info Category Found",
                    InfoCategoryValue = "1",
                    InfoCategorySelected = true
                });
            }
            else
            {
                infoCategoryItemList.Add(new TemplateInfoPageListDetailFilter.InfoCategoryItem
                {
                    InfoCategoryText = "Select Category Filter",
                    InfoCategoryValue = "0",
                    InfoCategorySelected = (filterInfoCategoryId == 0)
                });
                if (filterInfoCategoryId == 0) firstRecord = false;
            }

            if (filterInfoCategoryId != 0)
            {
                LTD_InfoCategory ltdInfoCategory = DataInfoList.GetLtdInfoCategory(filterInfoCategoryId);
                if (ltdInfoCategory != null)
                {
                    infoCategoryItemList.Add(new TemplateInfoPageListDetailFilter.InfoCategoryItem
                    {
                        InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                        InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                        InfoCategorySelected = true,
                    });
                    firstRecord = false;
                }
            }

            foreach (LTD_InfoCategory ltdInfoCategory in ltdInfoCategoryList)
            {
                if (ltdInfoCategory.InfoCategoryID != filterInfoCategoryId)
                {
                    if ((filterInfoCategoryId == 0) && (firstRecord == true))
                    {
                        infoCategoryItemList.Add(new TemplateInfoPageListDetailFilter.InfoCategoryItem
                        {
                            InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                            InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                            InfoCategorySelected = true
                        });
                    }
                    else
                    {
                        infoCategoryItemList.Add(new TemplateInfoPageListDetailFilter.InfoCategoryItem
                        {
                            InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                            InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                            InfoCategorySelected = false,
                        });
                    }
                }
                firstRecord = false;
            }

            #endregion

            long selectedUserId = 0;
            string selectUserDetailsHtml = "";
            string userSelectNameId = "infoPageUserFilter";

            if (UtilsSecurity.HaveAdminRole() == true)
            {
                selectedUserId = UtilsSecurity.GetUserId();
                if (filterCreatedUserId > 0) selectedUserId = filterCreatedUserId;

                #region User Select View

                long userViewDataIndex = dataIndex + 5;
                string showFunctionScript = "";
                string selectChangeCallback = " filterInfoPageList(" + pageNo + "," + itemsPerPage + "," + dataIndex + ",'" + templateSuffix + "'," + asyncLoading.ToString().ToLower() + ") ";
                selectUserDetailsHtml = UserAdminView.GetUserSelectView(userViewDataIndex, 1, 25, templateSuffix, selectedUserId, false, showFunctionScript, "", userSelectNameId, selectChangeCallback, out selectedUserId);

                #endregion
            }

            TemplateInfoPageListDetailFilter listDetailFilter = new TemplateInfoPageListDetailFilter
            {
                InfoPageListFilterHidden = hideFilter,

                ShowPublicCheckedList = showPublicCheckedList,
                ShowPublicUnCheckedList = showPublicUnCheckedList,
                InfoCategoryItemList = infoCategoryItemList,
                InfoPageFilter = filterInfoPage,

                DataIndex = dataIndex.ToString(),
                PageNo = pageNo.ToString(),
                ItemsPerPage = itemsPerPage.ToString(),
                TemplateSuffix = templateSuffix,
                AsyncLoading = asyncLoading.ToString().ToLower(),

                UserSelect = selectUserDetailsHtml,
            };
            htmlFilterItemList = listDetailFilter.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);

            #endregion

            #region Add Link

            List<TemplateInfoPageSaveAdd.AddAction> addActionList = new List<TemplateInfoPageSaveAdd.AddAction>();

            if (UtilsSecurity.HaveAuthorRoleEnabled() == true)
            {
                addActionList.Add(new TemplateInfoPageSaveAdd.AddAction
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                });
            }

            TemplateInfoPageSaveAdd templateSaveAdd = new TemplateInfoPageSaveAdd
            {
                AddActionList = addActionList,
            };
            htmlAddItemList = templateSaveAdd.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);

            #endregion

            #region Get Fill List

            #region Get Paged Data

            List<LTD_InfoPage> ltdInfoPageList = new List<LTD_InfoPage>();

            if (UtilsSecurity.IsAuthenticated() == true)
            {
                if (filterInfoPagePublic == true)
                {
                    ltdInfoPageList = DataInfoList.GetPublicPagedLtdInfoPage(filterInfoCategoryId, filterInfoPage, selectedUserId, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    if (UtilsSecurity.HaveAdminRole() == true)
                    {
                        ltdInfoPageList = DataInfoList.GetPagedLtdInfoPage(filterInfoCategoryId, filterInfoPage, selectedUserId, pageNo, itemsPerPage, out totalPages, out totalItems);
                    }
                    else
                    {
                        ltdInfoPageList = DataInfoList.GetUserPagedLtdInfoPage(filterInfoCategoryId, filterInfoPage, pageNo, itemsPerPage, out totalPages, out totalItems);
                    }

                    long commonTotalPages = 0;
                    long commonTotalItems = 0;
                    List<LTD_InfoPage> commonltdInfoPageList = DataInfoList.GetCommonPagedLtdInfoPage(filterInfoCategoryId, filterInfoPage, pageNo, itemsPerPage, out commonTotalPages, out commonTotalItems);
                    if (commonltdInfoPageList.Count > 0)
                    {
                        ltdInfoPageList.AddRange(commonltdInfoPageList);
                        totalItems = totalItems + commonTotalItems;
                        if (commonTotalPages > 1) totalPages = totalPages + commonTotalPages - 1;
                    }
                }
            }
            else
            {
                ltdInfoPageList = DataInfoList.GetPublicPagedLtdInfoPage(filterInfoCategoryId, filterInfoPage, selectedUserId, pageNo, itemsPerPage, out totalPages, out totalItems);
            }

            #endregion

            if (ltdInfoPageList.Count > 0)
            {
                #region Get Pager Details

                string topPagerDetails = UtilsGeneric.GetItemPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, RefreshListFunctionName, asyncLoading.ToString().ToLower());
                string bottomPagerDetails = UtilsGeneric.GetLinkPagerView(pageNo, itemsPerPage, dataIndex, templateSuffix, totalPages, totalItems, RefreshListFunctionName, asyncLoading.ToString().ToLower(), false);

                #endregion

                #region Append Top Pager

                if (topPagerDetails.Trim().Length > 0)
                {
                    htmlTextItemList += topPagerDetails;
                }

                #endregion

                #region Append Items

                int index = 0;
                for (; index < ltdInfoPageList.Count; index++)
                {
                    LTD_InfoPage ltdInfoPage = ltdInfoPageList[index];
                    string htmlTextItemTemplate = GetListSingleItemView(ltdInfoPage, pageNo, itemsPerPage, dataIndex, templateSuffix, index, asyncLoading);
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
                TemplateInfoPageListDetailEmpty templateListDetailEmpty = new TemplateInfoPageListDetailEmpty
                {
                    DataIndex = dataIndex.ToString(),
                    PageNo = pageNo.ToString(),
                    ItemsPerPage = itemsPerPage.ToString(),
                    TemplateSuffix = templateSuffix,
                    AsyncLoading = asyncLoading.ToString().ToLower()
                };
                htmlTextItemList = templateListDetailEmpty.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException, out message);
            }
            #endregion

            return htmlFilterItemList + htmlAddItemList + htmlTextItemList;
        }

        private static string GetListSingleItemView(LTD_InfoPage ltdInfoPage, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long recordIndex, bool asyncLoading)
        {
            string htmlTextItem = "";
            if (ltdInfoPage != null)
            {
                string message;
                List<TemplateInfoPageListDetailItem.EditAction> editActionList = new List<TemplateInfoPageListDetailItem.EditAction>();
                List<TemplateInfoPageListDetailItem.AsyncAction> asyncActionList = new List<TemplateInfoPageListDetailItem.AsyncAction>();

                if (((UtilsSecurity.HaveAdminRole() == true) && (UtilsSecurity.HaveAuthorRoleEnabled() == true)) || ((UtilsSecurity.HaveAuthorRoleEnabled() == true) && (ltdInfoPage.UserID == UtilsSecurity.GetUserId())))
                {
                    editActionList.Add(new TemplateInfoPageListDetailItem.EditAction
                    {
                        Id = ltdInfoPage.InfoPageID.ToString(),
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix
                    });
                }

                string infoCategoryName = "";
                LTD_InfoCategory ltdInfoCategory = DataInfoList.GetLtdInfoCategory(ltdInfoPage.InfoCategoryID);
                if (ltdInfoCategory != null)
                {
                    infoCategoryName = ltdInfoCategory.InfoCategoryName;
                }

                string infoSectionListView = "";
                string infoSectionSaveView = "";

                if ((ltdInfoPage.AsyncLoading == false) || ((ltdInfoPage.AsyncLoading == true) && (asyncLoading == false)))
                {
                    infoSectionListView = InfoSectionView.GetView(ltdInfoPage.InfoPageID, dataIndex + 1, templateSuffix, true, true, false, false);
                }
                else
                {
                    asyncActionList.Add(new TemplateInfoPageListDetailItem.AsyncAction
                    {
                        DataIndex = dataIndex.ToString(),
                        PageNo = pageNo.ToString(),
                        ItemsPerPage = itemsPerPage.ToString(),
                        TemplateSuffix = templateSuffix,
                        AsyncLoading = asyncLoading.ToString().ToLower(),
                        InfoPageId = ltdInfoPage.InfoPageID.ToString(),
                    });

                    infoSectionSaveView = InfoSectionView.GetView(ltdInfoPage.InfoPageID, dataIndex + 1, templateSuffix, false, false, false, false);
                }

                List<TemplateInfoPageListDetailItem.CommentView> commentViewList = new List<TemplateInfoPageListDetailItem.CommentView>();
                if ((ltdInfoPage.Commentable == true) && (UtilsSecurity.IsAuthenticated() == true))
                {
                    if ((UtilsSecurity.HaveRole(ltdInfoPage.CommentorRoleList, false) == true)) //|| ((ltdInfoPage.IsPublic == true) && (ltdInfoPage.CommentorRoleList.Contains(UtilsSecurity.GuestRole) == true)))
                    {
                        bool allowPublicComments = false;
                        string commentViewHtml = DataView.GetView(true, ltdInfoPage.InfoPageID, templateSuffix, false, CommonRefType.PageInfo, ltdInfoPage.InfoPageID, CommonDataType.CommentData, "Comment", false, false, false, false, false, false, false, true, true, "Add Comment", "Edit Comment", "Reply", allowPublicComments);
                        commentViewList.Add(new TemplateInfoPageListDetailItem.CommentView
                        {
                            CommentViewDetail = commentViewHtml,
                        });
                    }
                }

                var templateListDetailItem = new TemplateInfoPageListDetailItem
                {
                    InfoPageName = ltdInfoPage.InfoPageName,
                    InfoPageDescription = ltdInfoPage.InfoPageDescription,
                    InfoCategoryName = infoCategoryName,
                    CreatedBy = UtilsSecurity.GetUserName(ltdInfoPage.CreatedUserID),
                    LastUpdateDate = ltdInfoPage.LastUpdateDate.ToString(UtilsGeneric.DefaultDateFormat),
                    //ExpiryDate = ltdInfoPage.ExpiryDate.ToString(AppCommon.DateFormat),
                    IsInActive = !ltdInfoPage.IsActive,
                    IsDeleted = ltdInfoPage.IsDeleted,

                    EditActionList = editActionList,
                    AsyncActionList = asyncActionList,
                    InfoSectionView = infoSectionSaveView + infoSectionListView,
                    CommentViewList = commentViewList,
                };
                htmlTextItem = templateListDetailItem.GetFilled(templateSuffix, UtilsGeneric.Validate,
                                                                        UtilsGeneric.ThrowException,
                                                                        out message);


            }
            return htmlTextItem;
        }

        #endregion

        #region Info Page Category Option List

        public static string GetInfoPageCategoryOptionList(long infoPageId, string templateSuffix)
        {
            #region Get Category Option List

            List<LTD_InfoCategory> ltdInfoCategoryList = DataInfoList.GetAllActiveLtdInfoCategory();
            long infoCategoryId = 0;
            if (infoPageId > 0)
            {
                LTD_InfoPage ltdInfoPage = DataInfoList.GetLtdInfoPage(infoPageId);
                if (ltdInfoPage != null)
                {
                    infoCategoryId = ltdInfoPage.InfoCategoryID;
                }
            }


            bool firstRecord = true;
            List<TemplateInfoPageCategoryOption.InfoCategoryItem> infoCategoryItemList = new List<TemplateInfoPageCategoryOption.InfoCategoryItem>();
            if (ltdInfoCategoryList.Count == 0)
            {
                infoCategoryItemList.Add(new TemplateInfoPageCategoryOption.InfoCategoryItem
                {
                    InfoCategoryText = "Add Info Category",
                    InfoCategoryValue = "0",
                    InfoCategorySelected = true,
                    InfoCategoryDisable = false,
                });
            }

            if (infoCategoryId != 0)
            {
                LTD_InfoCategory ltdInfoCategory = DataInfoList.GetLtdInfoCategory(infoCategoryId);
                if (ltdInfoCategory != null)
                {
                    infoCategoryItemList.Add(new TemplateInfoPageCategoryOption.InfoCategoryItem
                    {
                        InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                        InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                        InfoCategorySelected = true,
                        InfoCategoryDisable = !ltdInfoCategory.IsActive
                    });
                    firstRecord = false;
                }
            }

            foreach (LTD_InfoCategory ltdInfoCategory in ltdInfoCategoryList)
            {
                if (ltdInfoCategory.IsDefault == true)
                {
                    infoCategoryItemList.Add(new TemplateInfoPageCategoryOption.InfoCategoryItem
                    {
                        InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                        InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                        InfoCategorySelected = firstRecord,
                        InfoCategoryDisable = !ltdInfoCategory.IsActive
                    });
                    firstRecord = false;
                    break;
                }
            }

            foreach (LTD_InfoCategory ltdInfoCategory in ltdInfoCategoryList)
            {
                if (ltdInfoCategory.IsDefault == false)
                {
                    if (ltdInfoCategory.InfoCategoryID != infoCategoryId)
                    {
                        if ((infoCategoryId == 0) && (firstRecord == true))
                        {
                            infoCategoryItemList.Add(new TemplateInfoPageCategoryOption.InfoCategoryItem
                            {
                                InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                                InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                                InfoCategorySelected = true,
                                InfoCategoryDisable = !ltdInfoCategory.IsActive
                            });
                        }
                        else
                        {
                            infoCategoryItemList.Add(new TemplateInfoPageCategoryOption.InfoCategoryItem
                            {
                                InfoCategoryText = ltdInfoCategory.InfoCategoryName,
                                InfoCategoryValue = ltdInfoCategory.InfoCategoryID.ToString(),
                                InfoCategorySelected = false,
                                InfoCategoryDisable = !ltdInfoCategory.IsActive
                            });
                        }
                    }
                }
                firstRecord = false;
            }

            #endregion

            var templateOptioin = new TemplateInfoPageCategoryOption
            {
                InfoCategoryItemList = infoCategoryItemList,
            };

            string message = "";
            string htmlOption = templateOptioin.GetFilled(templateSuffix, UtilsGeneric.Validate, UtilsGeneric.ThrowException,
                                                                      out message);

            return htmlOption;
        }

        #endregion
    }
}