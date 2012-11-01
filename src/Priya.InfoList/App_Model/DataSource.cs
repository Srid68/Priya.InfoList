using System;
using System.Globalization;
using System.Collections.Generic;

using PetaPoco;
using Arshu.Data.Common;

using Priya.Security.Utils;
using Priya.Security.Entity;
using Priya.InfoList.Entity;

namespace Priya.InfoList.Data
{
    internal static partial class DataSource
    {
        #region Constructor

        static DataSource()
        {
            OnGetUserInfo -= UtilsSecurity.UtilsSecurity_OnGetUserInfo;
            OnGetUserInfo += UtilsSecurity.UtilsSecurity_OnGetUserInfo;
        }

        #endregion

        #region CNS_DataType Query Related

        #region Get

        public static CNS_DataType GetCnsDataType(string dataType)
        {
            CNS_DataType cnsDataType = null;

            if (HaveDb == true)
            {
                cnsDataType = GetDb.SingleOrDefault<CNS_DataType>("SELECT * FROM CNS_DataType Where DataType=@0", dataType);
            }
            else
            {
                Dictionary<Guid, CNS_DataType> allCnsDataTypeList = FileSource.LoadCNSDataTypeData();
                foreach (KeyValuePair<Guid, CNS_DataType> item in allCnsDataTypeList)
                {
                    if (item.Value.DataType == dataType)
                    {
                        cnsDataType = item.Value;
                        break;
                    }
                }
            }
            return cnsDataType;
        }

        public static CNS_DataType GetDefaultCnsDataType()
        {
            CNS_DataType defaultCnsDataType = null;

            if (HaveDb == true)
            {
                defaultCnsDataType = GetDb.SingleOrDefault<CNS_DataType>("SELECT * FROM CNS_DataType Where IsDefault=@0", 1);
            }
            else
            {
                Dictionary<Guid, CNS_DataType> allCnsDataTypeList = FileSource.LoadCNSDataTypeData();
                foreach (KeyValuePair<Guid, CNS_DataType> item in allCnsDataTypeList)
                {
                    if (item.Value.IsDefault == true)
                    {
                        defaultCnsDataType = item.Value;
                        break;
                    }
                }

            }

            return defaultCnsDataType;
        }

        public static long GetDefaultCnsDataTypeId()
        {
            long defaultDataTypeId = 1;
            CNS_DataType defaultCnsDataType = GetDefaultCnsDataType();
            if (defaultCnsDataType != null) defaultDataTypeId = defaultCnsDataType.DataTypeID;
            return defaultDataTypeId;
        }

        #endregion

        #endregion

        #region CNS_DataRefType Query Related

        #region Get

        public static CNS_DataRefType GetCnsDataRefType(string dataRefType)
        {
            CNS_DataRefType cnsDataRefType = null;

            if (HaveDb == true)
            {
                cnsDataRefType = GetDb.SingleOrDefault<CNS_DataRefType>("SELECT * FROM CNS_DataRefType Where DataRefType=@0", dataRefType);
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> allCnsDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
                foreach (KeyValuePair<Guid, CNS_DataRefType> item in allCnsDataRefTypeList)
                {
                    if (item.Value.DataRefType == dataRefType)
                    {
                        cnsDataRefType = item.Value;
                        break;
                    }
                }
            }
            return cnsDataRefType;
        }

        public static CNS_DataRefType GetDefaultCnsDataRefType()
        {
            CNS_DataRefType defaultCnsDataRefType = null;

            if (HaveDb == true)
            {
                defaultCnsDataRefType = GetDb.SingleOrDefault<CNS_DataRefType>("SELECT * FROM CNS_DataRefType Where IsDefault=@0", 1);
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> allCnsDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
                foreach (KeyValuePair<Guid, CNS_DataRefType> item in allCnsDataRefTypeList)
                {
                    if (item.Value.IsDefault == true)
                    {
                        defaultCnsDataRefType = item.Value;
                        break;
                    }
                }

            }

            return defaultCnsDataRefType;
        }

        public static long GetDefaultCnsDataRefTypeId()
        {
            long defaultRefTypeId = 1;
            CNS_DataRefType defaultCnsDataRefType = GetDefaultCnsDataRefType();
            if (defaultCnsDataRefType != null) defaultRefTypeId = defaultCnsDataRefType.DataRefTypeID;
            return defaultRefTypeId;
        }

        #endregion

        #endregion

        #region LTD Subscribe 

        public static LTD_Subscriber GetLtdSubscriber(string subscriberEmail)
        {
            LTD_Subscriber ltdSubscriber = null;

            if (HaveDb == true)
            {
                long totalPages =0;
                long totalItems = 0;
                List<LTD_Subscriber> subscriberList = DataSource.GetPagedLtdSubscriber(1, 1, out totalPages, out totalItems, "", " Where SubscriberEmail=@0", subscriberEmail);
                if (totalItems > 0)
                {
                    ltdSubscriber = subscriberList[0];
                }
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> allLtdSubscriberList = FileSource.LoadLTDSubscriberData();
                foreach (KeyValuePair<Guid, LTD_Subscriber> item in allLtdSubscriberList)
                {
                    if (item.Value.SubscriberEmail == subscriberEmail)
                    {
                        ltdSubscriber = item.Value;
                        break;
                    }
                }

            }

            return ltdSubscriber;
        }

        public static long GetLtdSubscriberCount()
        {
            long ltdSubscriberCount = 0;
            if (HaveDb == true)
            {
                ltdSubscriberCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM LTD_Subscriber ");
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLTDSubscriberList = FileSource.LoadLTDSubscriberData();
                ltdSubscriberCount = fileLTDSubscriberList.Count;
            }
            return ltdSubscriberCount;
        }

        #endregion

        #region LTD Category

        public static LTD_InfoCategory GetLtdInfoCategory(string infoCategoryName)
        {
            LTD_InfoCategory ltdInfoCategory = null;
            long userId = UtilsSecurity.GetUserId();

            if (HaveDb == true)
            {
                long totalPages = 0;
                long totalItems = 0;
                List<LTD_InfoCategory> categoryList = DataSource.GetPagedLtdInfoCategory(1, 1, out totalPages, out totalItems, "", " Where InfoCategoryName=@0 and UserID=@1", infoCategoryName, userId);
                if (totalItems > 0)
                {
                    ltdInfoCategory = categoryList[0];
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> allLtdInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                foreach (KeyValuePair<Guid, LTD_InfoCategory> item in allLtdInfoCategoryList)
                {
                    if ((item.Value.InfoCategoryName == infoCategoryName) && (item.Value.UserID == userId))
                    {
                        ltdInfoCategory = item.Value;
                        break;
                    }
                }
            }

            return ltdInfoCategory;
        }

        public static LTD_InfoCategory GetDefaultLtdInfoCategory()
        {
            LTD_InfoCategory ltdInfoCategory = null;

            if (HaveDb == true)
            {
                long totalPages = 0;
                long totalItems = 0;
                List<LTD_InfoCategory> categoryList = DataSource.GetPagedLtdInfoCategory(1, 1, out totalPages, out totalItems, "", " Where IsDefault=@0", true);
                if (totalItems > 0)
                {
                    ltdInfoCategory = categoryList[0];
                }
                else if (totalItems ==0)
                {
                    categoryList = DataSource.GetPagedLtdInfoCategory(1, 1, out totalPages, out totalItems, "", "");
                    if (totalItems > 0)
                    {
                        ltdInfoCategory = categoryList[0];
                    }
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> allLtdInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                foreach (KeyValuePair<Guid, LTD_InfoCategory> item in allLtdInfoCategoryList)
                {
                    ltdInfoCategory = item.Value;
                    if (item.Value.IsDefault == true)
                    {
                        ltdInfoCategory = item.Value;
                        break;
                    }
                }
            }

            return ltdInfoCategory;
        }

        #endregion 
    }
}
