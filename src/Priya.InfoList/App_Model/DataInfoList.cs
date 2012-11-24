using System;
using System.Globalization;
using System.Collections.Generic;

using Arshu.Core.Basic.Log;
using Arshu.Core.Common;

using Priya.Security.Entity;
using Priya.Security.Utils;
using Priya.InfoList.Entity;
using Priya.InfoList.Data;

namespace Priya.InfoList.Model
{
    internal static class DataInfoList
    {
        #region LTD Subscribe

        #region Get

        public static LTD_Subscriber GetLtdSubscriber(long subscriberId)
        {
            return DataSource.GetLtdSubscriber(subscriberId, "");
        }

        public static LTD_Subscriber GetLtdSubscriber(string subscriberEmail)
        {
            return DataSource.GetLtdSubscriber(subscriberEmail);
        }

        public static long GetMaxLtdSubscriberId()
        {
            return DataSource.GetMaxLtdSubscriberId("");
        }

        public static long GetCountLtdSubscriberId()
        {
            return DataSource.GetLtdSubscriberCount();
        }

        #endregion

        #region Get Page

        public static List<LTD_Subscriber> GetAllLtdSubscriber(long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;

            List<LTD_Subscriber> ltdSubscriberList = DataSource.GetPagedLtdSubscriber(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "");

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return ltdSubscriberList;
        }

        #endregion

        #region Save

        public static long SaveLtdSubscriber(string subscriberEmail, string subscriberMessage, bool isDeleted, long subscriberId, out string retMessage)
        {
            long id = 0;
            string message = "";
            try
            {
                bool validationFail = false;
                if ((subscriberId > 0) && (UtilsSecurity.HaveAuthorRole() == false))
                {
                    message = "Please login with User having Author Role to Update Subscriber Data";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if ((subscriberMessage.Trim().Length == 0) || (subscriberEmail.Trim().Length == 0))
                    {
                        message = "Missing or Empty Subscriber Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    if (isDeleted == true)
                    {
                        if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
                        {
                            message = "Is Deleted Can be Updated Only by Users having Admin/Author Role";
                            validationFail = true;
                        }
                    }
                }

                var ltdSubscriber = new LTD_Subscriber();
                if (validationFail == false)
                {
                    if (subscriberId != 0)
                    {
                        ltdSubscriber = GetLtdSubscriber(subscriberId);
                        if (ltdSubscriber == null)
                        {
                            message = "The Subscriber having ID [" + subscriberId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                    }
                    else
                    {
                        ltdSubscriber = GetLtdSubscriber(subscriberEmail);
                        if (ltdSubscriber != null)
                        {
                            message = "The Subscriber having Email [" + subscriberEmail + "] is already registered";
                            validationFail = true;
                        }
                    }
                }

                if (validationFail == false)
                {
                    if (ltdSubscriber == null) ltdSubscriber = new LTD_Subscriber();
                    ltdSubscriber.SubscriberMessage = subscriberMessage;
                    ltdSubscriber.SubscriberEmail = subscriberEmail;
                    ltdSubscriber.IsDeleted = isDeleted;

                    DataSource.BeginTransaction();

                    if (ltdSubscriber.SubscriberID == 0)
                    {
                        DataSource.InsertLtdSubscriber(ltdSubscriber);
                        id = ltdSubscriber.SubscriberID;
                    }
                    else
                    {
                        DataSource.UpdateLtdSubscriber(ltdSubscriber);
                        id = ltdSubscriber.SubscriberID;
                    }
                    if (id > 0) DataSource.CompleteTransaction();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-SaveLtdSubscriber", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return id;
        }

        #endregion

        #region Delete

        public static bool DeleteLtdSubscriber(long subscriberId, out string retMessage)
        {
            bool ret = false;
            string message = "";
            try
            {
                bool validationFail = false;
                if ((UtilsSecurity.HaveAdminRole() == false))
                {
                    message = "Please login as User having admin Role to Delete Subscriber Data";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (subscriberId == 0)
                    {
                        message = "Missing or Empty Subscriber ID Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    LTD_Subscriber ltdSubscriberExisting = GetLtdSubscriber(subscriberId);
                    if (ltdSubscriberExisting == null)
                    {
                        message = "The Subscriber with ID [" + subscriberId.ToString(CultureInfo.InvariantCulture) + "] does not exist";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    DataSource.BeginTransaction();
                    DataSource.DeleteLtdSubscriber(subscriberId);
                    DataSource.CompleteTransaction();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-DeleteLtdSubscriber", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return ret;
        }

        #endregion

        #endregion

        #region LTD_InfoCategory

        #region Get

        public static LTD_InfoCategory GetLtdInfoCategory(long infoCategoryId)
        {
            return DataSource.GetLtdInfoCategory(infoCategoryId, " Where InfoCategoryId=@0", infoCategoryId);
        }
       
        public static LTD_InfoCategory GetLtdInfoCategory(string infoCategoryName)
        {
            return DataSource.GetLtdInfoCategory(infoCategoryName);
        }

        public static LTD_InfoCategory GetDefaultLtdInfoCategory()
        {
            return DataSource.GetDefaultLtdInfoCategory();
        }

        public static long GetMaxLtdInfoCategoryId()
        {
            return DataSource.GetMaxLtdInfoCategoryId("");
        }

        #endregion

        #region Get All

        public static List<LTD_InfoCategory> GetAllActiveLtdInfoCategory()
        {
            long totalPages = 0;
            long totalItems = 0;
            return DataSource.GetPagedLtdInfoCategory(0, 100, out totalPages, out totalItems, "", "Where IsActive=@0", true);
        }

        #endregion

        #region Get Page


        public static List<LTD_InfoCategory> GetPagedLtdInfoCategory(long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;

            List<LTD_InfoCategory> ltdInfoCategoryList = DataSource.GetPagedLtdInfoCategory(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "");

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return ltdInfoCategoryList;
        }
       
        #endregion

        #region Save

        public static long SaveLtdInfoCategory(string infoCategoryName, bool isDefault, bool isActive, long infoCategoryId, out string retMessage)
        {
            long id = 0;
            string message = "";
            try
            {
                bool validationFail = false;
                if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
                {
                    message = "Please login as User having Admin/Author Role to Save Info Category";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (infoCategoryName.Trim().Length == 0)
                    {
                        message = "Missing or Empty Category Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    LTD_InfoCategory ltdInfoCategoryNameExisting = GetLtdInfoCategory(infoCategoryName);
                    if ((ltdInfoCategoryNameExisting != null) && (ltdInfoCategoryNameExisting.InfoCategoryID != infoCategoryId))
                    {
                        message = "The Info Category [" + infoCategoryName + "] allready exists";
                        validationFail = true;
                    }
                }

                var ltdInfoCategory = new LTD_InfoCategory();
                if (validationFail == false)
                {
                    if (infoCategoryId != 0)
                    {
                        ltdInfoCategory = GetLtdInfoCategory(infoCategoryId);
                        if (ltdInfoCategory == null)
                        {
                            message = "The Info Category having ID [" + infoCategoryId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                    }
                }

                if (validationFail == false)
                {
                    ltdInfoCategory.IsActive = isActive;
                    if (infoCategoryId == 0)
                    {
                        ltdInfoCategory.Sequence = GetMaxLtdInfoCategoryId() + 1;
                        ltdInfoCategory.IsActive = true;
                        ltdInfoCategory.IsSystem = false;
                    }
                    ltdInfoCategory.InfoCategoryName = infoCategoryName;
                    ltdInfoCategory.IsDefault = isDefault;

                    DataSource.BeginTransaction();
                    if (ltdInfoCategory.IsDefault)
                    {
                        LTD_InfoCategory defaultLtdInfoCategory = GetDefaultLtdInfoCategory();
                        if (defaultLtdInfoCategory != null)
                        {
                            if (defaultLtdInfoCategory.InfoCategoryID != ltdInfoCategory.InfoCategoryID)
                            {
                                defaultLtdInfoCategory.IsDefault = false;
                                DataSource.UpdateLtdInfoCategory(defaultLtdInfoCategory);
                            }
                        }
                    }

                    if (ltdInfoCategory.InfoCategoryID == 0)
                    {
                        DataSource.InsertLtdInfoCategory(ltdInfoCategory);
                        id = ltdInfoCategory.InfoCategoryID;
                    }
                    else
                    {
                        DataSource.UpdateLtdInfoCategory(ltdInfoCategory);
                        id = ltdInfoCategory.InfoCategoryID;
                    }
                    DataSource.CompleteTransaction();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-SaveLtdInfoCategory", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return id;
        }

        #endregion

        #region Delete

        public static bool DeleteLtdInfoCategory(long infoCategoryId, out string retMessage)
        {
            bool ret = false;
            string message = "";
            try
            {
                bool validationFail = false;
                if ((UtilsSecurity.HaveAdminRole() == false))
                {
                    message = "Please login as User having admin Role to Delete Category";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (infoCategoryId == 0)
                    {
                        message = "Missing or Empty Category ID Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    LTD_InfoCategory ltdInfoCategoryExisting = GetLtdInfoCategory(infoCategoryId);
                    if (ltdInfoCategoryExisting == null)
                    {
                        message = "The Category with ID [" + infoCategoryId.ToString(CultureInfo.InvariantCulture) + "] does not exist";
                        validationFail = true;
                    }
                    else if (ltdInfoCategoryExisting.IsSystem == true)
                    {
                        message = "The System Category Cannot be Deleted";
                        validationFail = true;
                    }
                    else if (ltdInfoCategoryExisting.IsDefault)
                    {
                        message = "Please set another Category as Default and then Delete this Default Category";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    DataSource.BeginTransaction();
                    DataSource.DeleteLtdInfoCategory(infoCategoryId);
                    DataSource.CompleteTransaction();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-DeleteLtdInfoCategory", message);
            }

            retMessage = message;
            return ret;
        }

        #endregion

        #endregion

        #region LTD Page

        #region Get

        public static LTD_InfoPage GetLtdInfoPage(long infoPageId)
        {
            return DataSource.GetLtdInfoPage(infoPageId, "");
        }
        
        public static long GetMaxLtdInfoPageId()
        {
            return DataSource.GetMaxLtdInfoPageId("");
        }

        #endregion

        #region Get Page

        public static List<LTD_InfoPage> GetCommonPagedLtdInfoPage(long filterInfoCategoryId, string filterInfoPage, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;
            List<LTD_InfoPage> ltdInfoPageList = new List<LTD_InfoPage>();

            long userId = UtilsSecurity.GetUserId();
            filterInfoPage = "%" + filterInfoPage + "%";

            if ((filterInfoCategoryId == 0) && (string.IsNullOrEmpty(filterInfoPage) == true))
            {
                ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsCommon=@1 and IsDeleted=@2 and CreatedUserID!=@3", true, true, false, userId);
            }
            else if ((filterInfoCategoryId == 0) && (string.IsNullOrEmpty(filterInfoPage) == false))
            {
                ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsCommon=@1 and IsDeleted=@2 and CreatedUserID!=@3 and InfoPageName LIKE @4", true, true, false, userId, filterInfoPage);
            }
            else if ((filterInfoCategoryId > 0) && (string.IsNullOrEmpty(filterInfoPage) == true))
            {
                ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsCommon=@1 and IsDeleted=@2 and CreatedUserID!=@3 and InfoCategoryId=@4", true, true, false, userId, filterInfoCategoryId);
            }
            else if ((filterInfoCategoryId > 0) && (string.IsNullOrEmpty(filterInfoPage) == false))
            {
                ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsCommon=@1 and IsDeleted=@2 and InfoCategoryId=@3 and and CreatedUserID!=@4 and InfoPageName LIKE @5", true, true, false, userId, filterInfoCategoryId, filterInfoPage);
            }

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return ltdInfoPageList;
        }

        public static List<LTD_InfoPage> GetPublicPagedLtdInfoPage(long filterInfoCategoryId, string filterInfoPage, long filterCreatedUserId, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;
            List<LTD_InfoPage> ltdInfoPageList = new List<LTD_InfoPage>();

            filterInfoPage = "%" + filterInfoPage + "%";

            if ((filterInfoCategoryId == 0) && (string.IsNullOrEmpty(filterInfoPage) == true))
            {
                if (filterCreatedUserId == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsPublic=@1 and IsDeleted=@2", true, true, false);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsPublic=@1 and IsDeleted=@2 and CreatedUserID=@3", true, true, false, filterCreatedUserId);
                }
            }
            else if ((filterInfoCategoryId == 0) && (string.IsNullOrEmpty(filterInfoPage) == false))
            {
                if (filterCreatedUserId == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsPublic=@1 and IsDeleted=@2 and InfoPageName LIKE @3", true, true, false, filterInfoPage);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsPublic=@1 and IsDeleted=@2 and InfoPageName LIKE @3 and CreatedUserID=@4", true, true, false, filterInfoPage, filterCreatedUserId);
                }
            }
            else if ((filterInfoCategoryId > 0) && (string.IsNullOrEmpty(filterInfoPage) == true))
            {
                if (filterCreatedUserId == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsPublic=@1 and IsDeleted=@2 and InfoCategoryId=@3", true, true, false, filterInfoCategoryId);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsPublic=@1 and IsDeleted=@2 and InfoCategoryId=@3 and CreatedUserID=@4", true, true, false, filterInfoCategoryId, filterCreatedUserId);
                }
            }
            else if ((filterInfoCategoryId > 0) && (string.IsNullOrEmpty(filterInfoPage) == false))
            {
                if (filterCreatedUserId == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsPublic=@1 and IsDeleted=@2 and InfoCategoryId=@3 and InfoPageName LIKE @4", true, true, false, filterInfoCategoryId, filterInfoPage);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where IsActive=@0 and IsPublic=@1 and IsDeleted=@2 and InfoCategoryId=@3 and InfoPageName LIKE @4 and CreatedUserID=@5", true, true, false, filterInfoCategoryId, filterInfoPage, filterCreatedUserId);
                }
            }

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return ltdInfoPageList;
        }

        public static List<LTD_InfoPage> GetUserPagedLtdInfoPage(long filterInfoCategoryId, string filterInfoPage, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;
            List<LTD_InfoPage> ltdInfoPageList = new List<LTD_InfoPage>();

            filterInfoPage = "%" + filterInfoPage + "%";
            long userId = UtilsSecurity.GetUserId();
            List<long> accesssGroupIdList = UtilsSecurity.GetAllUserAccessGroupId();
            string accessGroupIdInList = String.Join(",", accesssGroupIdList.ToArray());

            if ((filterInfoCategoryId == 0) && (string.IsNullOrEmpty(filterInfoPage) == true))
            {
                if (accesssGroupIdList.Count  == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where CreatedUserID=@0", userId);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where CreatedUserID=@0 OR AccessGroupID IN (@1)", userId, accessGroupIdInList);
                }
            }
            else if ((filterInfoCategoryId == 0) && (string.IsNullOrEmpty(filterInfoPage) == false))
            {
                if (accesssGroupIdList.Count == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where CreatedUserID=@0 and InfoPageName LIKE @1", userId, filterInfoPage);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where (CreatedUserID=@0 OR AccessGroupId IN (@1)) and InfoPageName LIKE @2 ", userId, accessGroupIdInList, filterInfoPage);
                }
            }
            else if ((filterInfoCategoryId > 0) && (string.IsNullOrEmpty(filterInfoPage) == true))
            {
                if (accesssGroupIdList.Count == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where CreatedUserID=@0 and InfoCategoryId=@1", userId, filterInfoCategoryId);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where (CreatedUserID=@0 OR AccessGroupId IN (@1)) and InfoCategoryId=@2", userId, accessGroupIdInList, filterInfoCategoryId);
                }
            }
            else if ((filterInfoCategoryId > 0) && (string.IsNullOrEmpty(filterInfoPage) == false))
            {
                if (accesssGroupIdList.Count == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where CreatedUserID=@0 and InfoCategoryId=@1 and InfoPageName LIKE @2", userId, filterInfoCategoryId, filterInfoPage);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where (CreatedUserID=@0 OR AccessGroupId IN (@1)) and InfoCategoryId=@2 and InfoPageName LIKE @3", userId, accessGroupIdInList, filterInfoCategoryId, filterInfoPage);
                }
            }

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return ltdInfoPageList;
        }

        public static List<LTD_InfoPage> GetPagedLtdInfoPage(long filterInfoCategoryId, string filterInfoPage, long filterCreatedUserId, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;

            filterInfoPage = "%" + filterInfoPage + "%";

            List<LTD_InfoPage> ltdInfoPageList = new List<LTD_InfoPage>();

            if ((filterInfoCategoryId == 0) && (string.IsNullOrEmpty(filterInfoPage) == true))
            {
                if (filterCreatedUserId == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where CreatedUserID=@0", filterCreatedUserId);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "");
                }
            }
            else if ((filterInfoCategoryId == 0) && (string.IsNullOrEmpty(filterInfoPage) == false))
            {
                if (filterCreatedUserId == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where InfoPageName LIKE @0", filterInfoPage);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where InfoPageName LIKE @0 and CreatedUserID=@1", filterInfoPage, filterCreatedUserId);
                }
            }
            else if ((filterInfoCategoryId > 0) && (string.IsNullOrEmpty(filterInfoPage) == true))
            {
                if (filterCreatedUserId == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where InfoCategoryId=@0", filterInfoCategoryId);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where InfoCategoryId=@0 and CreatedUserID=@1", filterInfoCategoryId, filterCreatedUserId);
                }
            }
            else if ((filterInfoCategoryId > 0) && (string.IsNullOrEmpty(filterInfoPage) == false))
            {
                if (filterCreatedUserId == 0)
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where InfoCategoryId=@0 and InfoPageName LIKE @1", filterInfoCategoryId, filterInfoPage);
                }
                else
                {
                    ltdInfoPageList = DataSource.GetPagedLtdInfoPage(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where InfoCategoryId=@0 and InfoPageName LIKE @1 and CreatedUserID=@2", filterInfoCategoryId, filterInfoPage, filterCreatedUserId);
                }
            }
            
            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return ltdInfoPageList;
        }

        #endregion

        #region Save

        public static long SaveLtdInfoPage(string infoPageName, string infoPageDescription, long infoPageCategoryId, long accessGroupId, bool asyncLoading, bool isActive, DateTime expiryDate, bool commentable, string commentorRoleList, bool isPublic, bool isCommon, bool isDeleted, long sequence, long infoPageId, out string retMessage)
        {
            long id = 0;
            string message = "";
            try
            {
                bool validationFail = false;
                if (UtilsSecurity.HaveAuthorRole() == false)
                {
                    if (isPublic == true)
                    {
                        if (UtilsSecurity.HaveAdminRole() == false)
                        {
                            message = "Please login as User Having Admin Role to Make Info Page Public";
                            validationFail = true;
                        }
                    }
                    else
                    {
                        message = "Please login as User Having Author Role to Save Info Page Data";
                        validationFail = true;
                    }
                }


                if (validationFail == false)
                {
                    if ((infoPageName.Trim().Length == 0) || (infoPageDescription.Trim().Length == 0) || (infoPageCategoryId == 0))
                    {
                        message = "Missing or Empty Info Page Data Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    if (isDeleted == true)
                    {
                        if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
                        {
                            message = "Is Deleted Can be Updated Only by Users having Admin/Author Role";
                            validationFail = true;
                        }
                    }
                }

                LTD_InfoCategory ltdInfoCategory = null;
                ltdInfoCategory = GetLtdInfoCategory(infoPageCategoryId);
                if (validationFail == false)
                {
                    if (ltdInfoCategory == null)
                    {
                        message = "The Category having ID [" + infoPageCategoryId + "] cannot be Retrieved";
                        validationFail = true;
                    }
                }

                var ltdInfoPage = new LTD_InfoPage();
                if (validationFail == false)
                {
                    if (infoPageId != 0)
                    {
                        ltdInfoPage = GetLtdInfoPage(infoPageId);
                        if (ltdInfoPage == null)
                        {
                            message = "The Page having ID [" + infoPageId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                    }
                }

                if (validationFail == false)
                {                    
                    ltdInfoPage.InfoPageName = infoPageName;
                    ltdInfoPage.InfoPageDescription = infoPageDescription;

                    if (expiryDate == DateTime.MinValue) expiryDate = DateTime.Now.AddYears(1);
                    ltdInfoPage.ExpiryDate = expiryDate;
                    ltdInfoPage.IsActive = isActive;

                    ltdInfoPage.InfoCategoryID = ltdInfoCategory.InfoCategoryID;
                    ltdInfoPage.InfoCategoryGUID = ltdInfoCategory.InfoCategoryGUID;

                    if (UtilsSecurity.HaveAdminRole())
                    {
                        ltdInfoPage.IsPublic = isPublic;
                        ltdInfoPage.IsCommon = isCommon;
                        ltdInfoPage.IsDeleted = isDeleted;
                        ltdInfoPage.Sequence = sequence;
                    }

                    if (accessGroupId > 0)
                    {
                        SCD_Group scdGroup = UtilsSecurity.GetScdGroup(accessGroupId);
                        if (scdGroup != null)
                        {
                            ltdInfoPage.AccessGroupID = scdGroup.GroupID;
                            ltdInfoPage.AccessGroupGUID = scdGroup.GroupGUID;
                        }
                    }

                    ltdInfoPage.AsyncLoading = asyncLoading;
                    ltdInfoPage.Commentable = commentable;
                    ltdInfoPage.CommentorRoleList = commentorRoleList;

                    if (infoPageId == 0)
                    {
                        ltdInfoPage.IsActive = true;
                        ltdInfoPage.Sequence = GetMaxLtdInfoPageId() + 1;
                    }

                    DataSource.BeginTransaction();

                    if (ltdInfoPage.InfoPageID == 0)
                    {
                        ltdInfoPage.CreatedUserID = UtilsSecurity.GetUserId();
                        ltdInfoPage.CreatedUserGUID = UtilsSecurity.GetUserGuid();

                        DataSource.InsertLtdInfoPage(ltdInfoPage);
                        id = ltdInfoPage.InfoPageID;
                    }
                    else
                    {
                        DataSource.UpdateLtdInfoPage(ltdInfoPage);
                        id = ltdInfoPage.InfoPageID;
                    }
                    if (id > 0) DataSource.CompleteTransaction();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-SaveLtdInfoPage", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return id;
        }

        #endregion

        #region Delete

        public static bool DeleteLtdInfoPage(long infoPageId, out string retMessage)
        {
            bool ret = false;
            string message = "";
            try
            {
                bool validationFail = false;
                if (UtilsSecurity.HaveAdminRole() == false)
                {
                    message = "Please login as User having admin Role to Delete Page Data";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (infoPageId == 0)
                    {
                        message = "Missing or Empty Page ID Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    LTD_InfoPage ltdInfoPageExisting = GetLtdInfoPage(infoPageId);
                    if (ltdInfoPageExisting == null)
                    {
                        message = "The Page with ID [" + infoPageId.ToString(CultureInfo.InvariantCulture) + "] does not exist";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    if (HaveLtdInfoSection(infoPageId) ==true)
                    {
                        message = "The Page has Sections. Please delete the Sections before deleting the Page";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    DataSource.BeginTransaction();
                    DataSource.DeleteLtdInfoPage(infoPageId);
                    DataSource.CompleteTransaction();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-DeleteLtdInfoPage", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return ret;
        }

        #endregion

        #endregion

        #region LTD Section

        #region Get

        public static LTD_InfoSection GetLtdInfoSection(long infoSectionId)
        {
            return DataSource.GetLtdInfoSection(infoSectionId, "");
        }

        public static long GetMaxLtdInfoSectionId()
        {
            return DataSource.GetMaxLtdInfoSectionId("");
        }

        public static bool HaveLtdInfoSection(long infoPageId)
        {
            bool ret = true;

            long totalPages = 0;
            long totalItems =0;
            List<LTD_InfoSection> ltdSectionList = GetPagedLtdInfoSection(infoPageId, false, 1, 1, out totalPages, out totalItems);
            if (ltdSectionList.Count == 0) ret = false;

            return ret;
        }

        #endregion

        #region Get Page

        public static List<LTD_InfoSection> GetPagedLtdInfoSection(long infoPageId, bool showActiveOnly, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;

            List<LTD_InfoSection> ltdSectionList = new List<LTD_InfoSection>();
            if (showActiveOnly == true)
            {
                ltdSectionList = DataSource.GetPagedLtdInfoSection(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "Order by Sequence ASC", "Where InfoPageID=@0 and IsActive=@1", infoPageId, true);
            }
            else
            {
                ltdSectionList = DataSource.GetPagedLtdInfoSection(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "Order by Sequence ASC", "Where InfoPageID=@0", infoPageId);
            }
            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return ltdSectionList;
        }

        #endregion

        #region Save

        public static long SaveLtdInfoSection(long infoPageId, string infoSectionName, string infoSectionDescription, bool isActive, bool isDeleted, long sequence, long infoSectionId, out string retMessage)
        {
            long id = 0;
            string message = "";
            try
            {
                bool validationFail = false;
                if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
                {
                    message = "Please login as User Having Admin/Author Role to Save Section Data";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if ((infoPageId == 0) || (infoSectionName.Trim().Length == 0) || (infoSectionDescription.Trim().Length == 0))
                    {
                        message = "Missing or Empty Section Data Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    if (isDeleted == true)
                    {
                        if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
                        {
                            message = "Is Deleted Can be Updated Only by Users having Admin/Author Role";
                            validationFail = true;
                        }
                    }
                }

                var ltdInfoPage = new LTD_InfoPage();
                if (validationFail == false)
                {
                    if (infoPageId != 0)
                    {
                        ltdInfoPage = GetLtdInfoPage(infoPageId);
                        if (ltdInfoPage == null)
                        {
                            message = "The Info Page having ID [" + infoPageId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                    }
                }

                var ltdSection = new LTD_InfoSection();
                if (validationFail == false)
                {
                    if (infoSectionId != 0)
                    {
                        ltdSection = GetLtdInfoSection(infoSectionId);
                        if (ltdSection == null)
                        {
                            message = "The Info Section having ID [" + infoSectionId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                    }
                }

                if (validationFail == false)
                {
                    ltdSection.InfoPageID = ltdInfoPage.InfoPageID;
                    ltdSection.InfoPageGUID = ltdInfoPage.InfoPageGUID;

                    ltdSection.InfoSectionName = infoSectionName;
                    ltdSection.InfoSectionDescription = infoSectionDescription;
                    ltdSection.IsActive = isActive;
                    ltdSection.IsDeleted = isDeleted;
                    ltdSection.Sequence = sequence;

                    if (infoSectionId == 0)
                    {
                        ltdSection.IsActive = true;
                        ltdSection.Sequence = GetMaxLtdInfoSectionId() + 1;
                    }

                    DataSource.BeginTransaction();
                    if (ltdSection.InfoSectionID == 0)
                    {
                        DataSource.InsertLtdInfoSection(ltdSection);
                        id = ltdSection.InfoSectionID;
                    }
                    else
                    {
                        DataSource.UpdateLtdInfoSection(ltdSection);
                        id = ltdSection.InfoSectionID;
                    }
                    if (id > 0) DataSource.CompleteTransaction();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-SaveLtdInfoSection", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return id;
        }

        #endregion

        #region Delete

        public static bool DeleteLtdInfoSection(long infoSectionId, out string retMessage)
        {
            bool ret = false;
            string message = "";
            try
            {
                bool validationFail = false;
                if (UtilsSecurity.HaveAdminRole() == false)
                {
                    message = "Please login as User having admin Role to Delete Section Data";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (infoSectionId == 0)
                    {
                        message = "Missing or Empty Section ID Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    LTD_InfoSection ltdSectionExisting = GetLtdInfoSection(infoSectionId);
                    if (ltdSectionExisting == null)
                    {
                        message = "The Section with ID [" + infoSectionId.ToString(CultureInfo.InvariantCulture) + "] does not exist";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    if (HaveLtdInfoDetail(infoSectionId) == true)
                    {
                        message = "The Section has Details. Please delete the Details before deleting the Section";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    DataSource.BeginTransaction();
                    DataSource.DeleteLtdInfoSection(infoSectionId);
                    DataSource.CompleteTransaction();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-DeleteLtdInfoSection", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return ret;
        }

        #endregion

        #endregion

        #region LTD Detail

        #region Get

        public static LTD_InfoDetail GetLtdInfoDetail(long infoDetailId)
        {
            return DataSource.GetLtdInfoDetail(infoDetailId, "");
        }

        public static long GetMaxLtdInfoDetailId()
        {
            return DataSource.GetMaxLtdInfoDetailId("");
        }

        public static bool HaveLtdInfoDetail(long infoSectionId)
        {
            bool ret = true;

            long totalPages = 0;
            long totalItems = 0;
            List<LTD_InfoDetail> ltdDetailList = GetPagedLtdInfoDetail(infoSectionId, false, 1, 1, out totalPages, out totalItems);
            if (ltdDetailList.Count == 0) ret = false;

            return ret;
        }

        #endregion

        #region Get Page

        public static List<LTD_InfoDetail> GetPagedLtdInfoDetail(long infoSectionId, bool showActiveOnly, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;

            List<LTD_InfoDetail> ltdDetailList = new List<LTD_InfoDetail>();
            if (showActiveOnly == true)
            {
                ltdDetailList = DataSource.GetPagedLtdInfoDetail(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "Order by Sequence ASC", "Where InfoSectionID=@0 and IsActive=@1", infoSectionId, true);
            }
            else
            {
                ltdDetailList = DataSource.GetPagedLtdInfoDetail(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "Order by Sequence ASC", "Where InfoSectionID=@0", infoSectionId);
            }

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return ltdDetailList;
        }

        #endregion

        #region Save

        public static long SaveLtdInfoDetail(long infoSectionId, string infoDetailName, string infoDetailDescription, bool isActive, bool isDeleted, long sequence, long infoDetailId, out string retMessage)
        {
            long id = 0;
            string message = "";
            try
            {
                bool validationFail = false;
                if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
                {
                    message = "Please login as User Having Author Role to Save Detail Data";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if ((infoSectionId == 0) || (infoDetailName.Trim().Length == 0) || (infoDetailDescription.Trim().Length == 0))
                    {
                        message = "Missing or Empty InfoDetail Data Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    if (isDeleted == true)
                    {
                        if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
                        {
                            message = "Is Deleted Can be Updated Only by Users having Admin/Author Role";
                            validationFail = true;
                        }
                    }
                }

                var ltdSection = new LTD_InfoSection();
                if (validationFail == false)
                {
                    if (infoSectionId != 0)
                    {
                        ltdSection = GetLtdInfoSection(infoSectionId);
                        if (ltdSection == null)
                        {
                            message = "The Info Section having ID [" + infoSectionId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                    }
                }

                var ltdDetail = new LTD_InfoDetail();
                if (validationFail == false)
                {
                    if (infoDetailId != 0)
                    {
                        ltdDetail = GetLtdInfoDetail(infoDetailId);
                        if (ltdDetail == null)
                        {
                            message = "The Info Detail having ID [" + infoDetailId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                    }
                }

                if (validationFail == false)
                {
                    ltdDetail.InfoPageID = ltdSection.InfoPageID;
                    ltdDetail.InfoPageGUID = ltdSection.InfoPageGUID;

                    ltdDetail.InfoSectionID = ltdSection.InfoSectionID;
                    ltdDetail.InfoSectionGUID = ltdSection.InfoSectionGUID;

                    ltdDetail.InfoDetailName = infoDetailName;
                    ltdDetail.InfoDetailDescription = infoDetailDescription;
                    ltdDetail.IsActive = isActive;
                    ltdDetail.IsDeleted = isDeleted;
                    ltdDetail.Sequence = sequence;

                    if (infoDetailId == 0)
                    {
                        ltdDetail.IsActive = true;
                        ltdDetail.Sequence = GetMaxLtdInfoDetailId() + 1;
                    }

                    DataSource.BeginTransaction();
                    if (ltdDetail.InfoDetailID == 0)
                    {
                        DataSource.InsertLtdInfoDetail(ltdDetail);
                        id = ltdDetail.InfoDetailID;
                    }
                    else
                    {
                        DataSource.UpdateLtdInfoDetail(ltdDetail);
                        id = ltdDetail.InfoDetailID;
                    }
                    if (id > 0) DataSource.CompleteTransaction();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-SaveLtdInfoDetail", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return id;
        }

        #endregion

        #region Delete

        public static bool DeleteLtdInfoDetail(long infoDetailId, out string retMessage)
        {
            bool ret = false;
            string message = "";
            try
            {
                bool validationFail = false;
                if (UtilsSecurity.HaveAdminRole() == false)
                {
                    message = "Please login as User having admin Role to Delete Detail Data";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (infoDetailId == 0)
                    {
                        message = "Missing or Empty Detail ID Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    LTD_InfoDetail ltdDetailExisting = GetLtdInfoDetail(infoDetailId);
                    if (ltdDetailExisting == null)
                    {
                        message = "The Detail with ID [" + infoDetailId.ToString(CultureInfo.InvariantCulture) + "] does not exist";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    DataSource.BeginTransaction();
                    DataSource.DeleteLtdInfoDetail(infoDetailId);
                    DataSource.CompleteTransaction();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataInfoList-DeleteLtdInfoDetail", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return ret;
        }

        #endregion

        #endregion
    }
}