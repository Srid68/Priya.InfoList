
using System;
using System.Collections.Generic;
using Arshu.Core.Json;
using Priya.InfoList.Entity;

namespace Priya.InfoList.Data
{
    internal static partial class FileSource
    {

        public static Dictionary<Guid, LTD_InfoSection> LoadLTDInfoSectionData()
        {
            Dictionary<Guid, LTD_InfoSection> keyltdInfoSectionList = new Dictionary<Guid, LTD_InfoSection>();

            List<LTD_InfoSection> ltdInfoSectionList = JsonStore<LTD_InfoSection>.LoadData(false);
            foreach (LTD_InfoSection item in ltdInfoSectionList)
            {
                keyltdInfoSectionList.Add(item.InfoSectionGUID, item);
            }
            return keyltdInfoSectionList;
        }

        public static bool SaveLTDInfoSectionData(Dictionary<Guid, LTD_InfoSection> ltdInfoSectionList)
        {
            bool ret = false;
            if (ltdInfoSectionList.Count > 0)
            {
                List<LTD_InfoSection> ltdInfoSectionValueList = new List<LTD_InfoSection>();
                LTD_InfoSection[] ltdInfoSectionArray = new LTD_InfoSection[ltdInfoSectionList.Values.Count];
                ltdInfoSectionList.Values.CopyTo(ltdInfoSectionArray, 0);
                ltdInfoSectionValueList.AddRange(ltdInfoSectionArray);
                ret = JsonStore<LTD_InfoSection>.SaveData(ltdInfoSectionValueList, true);
            }

            return ret;
        }

        public static List<LTD_InfoSection> GetPagedLTDInfoSection(Dictionary<Guid, LTD_InfoSection> allLTDInfoSectionList, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            List<LTD_InfoSection> pagedLTDInfoSectionList = new List<LTD_InfoSection>();
            totalItems = allLTDInfoSectionList.Count;
            totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemsPerPage);

            if (pageNo > 0) pageNo = pageNo - 1;
            long startIndex = pageNo * itemsPerPage;
            if (startIndex > totalItems) startIndex = 0;
            long endIndex = startIndex + itemsPerPage;
            if (endIndex > totalItems) endIndex = totalItems;

            int i = 0;
            foreach (KeyValuePair<Guid, LTD_InfoSection> item in allLTDInfoSectionList)
            {
                if ((i >= (int)startIndex) && (i < endIndex))
                {
                    pagedLTDInfoSectionList.Add(item.Value);
                }
                i++;
            }

            return pagedLTDInfoSectionList;
        }

        public static Dictionary<Guid, LTD_InfoDetail> LoadLTDInfoDetailData()
        {
            Dictionary<Guid, LTD_InfoDetail> keyltdInfoDetailList = new Dictionary<Guid, LTD_InfoDetail>();

            List<LTD_InfoDetail> ltdInfoDetailList = JsonStore<LTD_InfoDetail>.LoadData(false);
            foreach (LTD_InfoDetail item in ltdInfoDetailList)
            {
                keyltdInfoDetailList.Add(item.InfoDetailGUID, item);
            }
            return keyltdInfoDetailList;
        }

        public static bool SaveLTDInfoDetailData(Dictionary<Guid, LTD_InfoDetail> ltdInfoDetailList)
        {
            bool ret = false;
            if (ltdInfoDetailList.Count > 0)
            {
                List<LTD_InfoDetail> ltdInfoDetailValueList = new List<LTD_InfoDetail>();
                LTD_InfoDetail[] ltdInfoDetailArray = new LTD_InfoDetail[ltdInfoDetailList.Values.Count];
                ltdInfoDetailList.Values.CopyTo(ltdInfoDetailArray, 0);
                ltdInfoDetailValueList.AddRange(ltdInfoDetailArray);
                ret = JsonStore<LTD_InfoDetail>.SaveData(ltdInfoDetailValueList, true);
            }

            return ret;
        }

        public static List<LTD_InfoDetail> GetPagedLTDInfoDetail(Dictionary<Guid, LTD_InfoDetail> allLTDInfoDetailList, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            List<LTD_InfoDetail> pagedLTDInfoDetailList = new List<LTD_InfoDetail>();
            totalItems = allLTDInfoDetailList.Count;
            totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemsPerPage);

            if (pageNo > 0) pageNo = pageNo - 1;
            long startIndex = pageNo * itemsPerPage;
            if (startIndex > totalItems) startIndex = 0;
            long endIndex = startIndex + itemsPerPage;
            if (endIndex > totalItems) endIndex = totalItems;

            int i = 0;
            foreach (KeyValuePair<Guid, LTD_InfoDetail> item in allLTDInfoDetailList)
            {
                if ((i >= (int)startIndex) && (i < endIndex))
                {
                    pagedLTDInfoDetailList.Add(item.Value);
                }
                i++;
            }

            return pagedLTDInfoDetailList;
        }

        public static Dictionary<Guid, CNS_DataType> LoadCNSDataTypeData()
        {
            Dictionary<Guid, CNS_DataType> keycnsDataTypeList = new Dictionary<Guid, CNS_DataType>();

            List<CNS_DataType> cnsDataTypeList = JsonStore<CNS_DataType>.LoadData(false);
            foreach (CNS_DataType item in cnsDataTypeList)
            {
                keycnsDataTypeList.Add(item.DataTypeGUID, item);
            }
            return keycnsDataTypeList;
        }

        public static bool SaveCNSDataTypeData(Dictionary<Guid, CNS_DataType> cnsDataTypeList)
        {
            bool ret = false;
            if (cnsDataTypeList.Count > 0)
            {
                List<CNS_DataType> cnsDataTypeValueList = new List<CNS_DataType>();
                CNS_DataType[] cnsDataTypeArray = new CNS_DataType[cnsDataTypeList.Values.Count];
                cnsDataTypeList.Values.CopyTo(cnsDataTypeArray, 0);
                cnsDataTypeValueList.AddRange(cnsDataTypeArray);
                ret = JsonStore<CNS_DataType>.SaveData(cnsDataTypeValueList, true);
            }

            return ret;
        }

        public static List<CNS_DataType> GetPagedCNSDataType(Dictionary<Guid, CNS_DataType> allCNSDataTypeList, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            List<CNS_DataType> pagedCNSDataTypeList = new List<CNS_DataType>();
            totalItems = allCNSDataTypeList.Count;
            totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemsPerPage);

            if (pageNo > 0) pageNo = pageNo - 1;
            long startIndex = pageNo * itemsPerPage;
            if (startIndex > totalItems) startIndex = 0;
            long endIndex = startIndex + itemsPerPage;
            if (endIndex > totalItems) endIndex = totalItems;

            int i = 0;
            foreach (KeyValuePair<Guid, CNS_DataType> item in allCNSDataTypeList)
            {
                if ((i >= (int)startIndex) && (i < endIndex))
                {
                    pagedCNSDataTypeList.Add(item.Value);
                }
                i++;
            }

            return pagedCNSDataTypeList;
        }

        public static Dictionary<Guid, CNS_DataRefType> LoadCNSDataRefTypeData()
        {
            Dictionary<Guid, CNS_DataRefType> keycnsDataRefTypeList = new Dictionary<Guid, CNS_DataRefType>();

            List<CNS_DataRefType> cnsDataRefTypeList = JsonStore<CNS_DataRefType>.LoadData(false);
            foreach (CNS_DataRefType item in cnsDataRefTypeList)
            {
                keycnsDataRefTypeList.Add(item.DataRefTypeGUID, item);
            }
            return keycnsDataRefTypeList;
        }

        public static bool SaveCNSDataRefTypeData(Dictionary<Guid, CNS_DataRefType> cnsDataRefTypeList)
        {
            bool ret = false;
            if (cnsDataRefTypeList.Count > 0)
            {
                List<CNS_DataRefType> cnsDataRefTypeValueList = new List<CNS_DataRefType>();
                CNS_DataRefType[] cnsDataRefTypeArray = new CNS_DataRefType[cnsDataRefTypeList.Values.Count];
                cnsDataRefTypeList.Values.CopyTo(cnsDataRefTypeArray, 0);
                cnsDataRefTypeValueList.AddRange(cnsDataRefTypeArray);
                ret = JsonStore<CNS_DataRefType>.SaveData(cnsDataRefTypeValueList, true);
            }

            return ret;
        }

        public static List<CNS_DataRefType> GetPagedCNSDataRefType(Dictionary<Guid, CNS_DataRefType> allCNSDataRefTypeList, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            List<CNS_DataRefType> pagedCNSDataRefTypeList = new List<CNS_DataRefType>();
            totalItems = allCNSDataRefTypeList.Count;
            totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemsPerPage);

            if (pageNo > 0) pageNo = pageNo - 1;
            long startIndex = pageNo * itemsPerPage;
            if (startIndex > totalItems) startIndex = 0;
            long endIndex = startIndex + itemsPerPage;
            if (endIndex > totalItems) endIndex = totalItems;

            int i = 0;
            foreach (KeyValuePair<Guid, CNS_DataRefType> item in allCNSDataRefTypeList)
            {
                if ((i >= (int)startIndex) && (i < endIndex))
                {
                    pagedCNSDataRefTypeList.Add(item.Value);
                }
                i++;
            }

            return pagedCNSDataRefTypeList;
        }

        public static Dictionary<Guid, CND_Data> LoadCNDDataData()
        {
            Dictionary<Guid, CND_Data> keycndDataList = new Dictionary<Guid, CND_Data>();

            List<CND_Data> cndDataList = JsonStore<CND_Data>.LoadData(false);
            foreach (CND_Data item in cndDataList)
            {
                keycndDataList.Add(item.DataGUID, item);
            }
            return keycndDataList;
        }

        public static bool SaveCNDDataData(Dictionary<Guid, CND_Data> cndDataList)
        {
            bool ret = false;
            if (cndDataList.Count > 0)
            {
                List<CND_Data> cndDataValueList = new List<CND_Data>();
                CND_Data[] cndDataArray = new CND_Data[cndDataList.Values.Count];
                cndDataList.Values.CopyTo(cndDataArray, 0);
                cndDataValueList.AddRange(cndDataArray);
                ret = JsonStore<CND_Data>.SaveData(cndDataValueList, true);
            }

            return ret;
        }

        public static List<CND_Data> GetPagedCNDData(Dictionary<Guid, CND_Data> allCNDDataList, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            List<CND_Data> pagedCNDDataList = new List<CND_Data>();
            totalItems = allCNDDataList.Count;
            totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemsPerPage);

            if (pageNo > 0) pageNo = pageNo - 1;
            long startIndex = pageNo * itemsPerPage;
            if (startIndex > totalItems) startIndex = 0;
            long endIndex = startIndex + itemsPerPage;
            if (endIndex > totalItems) endIndex = totalItems;

            int i = 0;
            foreach (KeyValuePair<Guid, CND_Data> item in allCNDDataList)
            {
                if ((i >= (int)startIndex) && (i < endIndex))
                {
                    pagedCNDDataList.Add(item.Value);
                }
                i++;
            }

            return pagedCNDDataList;
        }

        public static Dictionary<Guid, LTD_InfoPage> LoadLTDInfoPageData()
        {
            Dictionary<Guid, LTD_InfoPage> keyltdInfoPageList = new Dictionary<Guid, LTD_InfoPage>();

            List<LTD_InfoPage> ltdInfoPageList = JsonStore<LTD_InfoPage>.LoadData(false);
            foreach (LTD_InfoPage item in ltdInfoPageList)
            {
                keyltdInfoPageList.Add(item.InfoPageGUID, item);
            }
            return keyltdInfoPageList;
        }

        public static bool SaveLTDInfoPageData(Dictionary<Guid, LTD_InfoPage> ltdInfoPageList)
        {
            bool ret = false;
            if (ltdInfoPageList.Count > 0)
            {
                List<LTD_InfoPage> ltdInfoPageValueList = new List<LTD_InfoPage>();
                LTD_InfoPage[] ltdInfoPageArray = new LTD_InfoPage[ltdInfoPageList.Values.Count];
                ltdInfoPageList.Values.CopyTo(ltdInfoPageArray, 0);
                ltdInfoPageValueList.AddRange(ltdInfoPageArray);
                ret = JsonStore<LTD_InfoPage>.SaveData(ltdInfoPageValueList, true);
            }

            return ret;
        }

        public static List<LTD_InfoPage> GetPagedLTDInfoPage(Dictionary<Guid, LTD_InfoPage> allLTDInfoPageList, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            List<LTD_InfoPage> pagedLTDInfoPageList = new List<LTD_InfoPage>();
            totalItems = allLTDInfoPageList.Count;
            totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemsPerPage);

            if (pageNo > 0) pageNo = pageNo - 1;
            long startIndex = pageNo * itemsPerPage;
            if (startIndex > totalItems) startIndex = 0;
            long endIndex = startIndex + itemsPerPage;
            if (endIndex > totalItems) endIndex = totalItems;

            int i = 0;
            foreach (KeyValuePair<Guid, LTD_InfoPage> item in allLTDInfoPageList)
            {
                if ((i >= (int)startIndex) && (i < endIndex))
                {
                    pagedLTDInfoPageList.Add(item.Value);
                }
                i++;
            }

            return pagedLTDInfoPageList;
        }

        public static Dictionary<Guid, SYS_Version> LoadSYSVersionData()
        {
            Dictionary<Guid, SYS_Version> keysysVersionList = new Dictionary<Guid, SYS_Version>();

            List<SYS_Version> sysVersionList = JsonStore<SYS_Version>.LoadData(false);
            foreach (SYS_Version item in sysVersionList)
            {
                keysysVersionList.Add(item.VersionNoGUID, item);
            }
            return keysysVersionList;
        }

        public static bool SaveSYSVersionData(Dictionary<Guid, SYS_Version> sysVersionList)
        {
            bool ret = false;
            if (sysVersionList.Count > 0)
            {
                List<SYS_Version> sysVersionValueList = new List<SYS_Version>();
                SYS_Version[] sysVersionArray = new SYS_Version[sysVersionList.Values.Count];
                sysVersionList.Values.CopyTo(sysVersionArray, 0);
                sysVersionValueList.AddRange(sysVersionArray);
                ret = JsonStore<SYS_Version>.SaveData(sysVersionValueList, true);
            }

            return ret;
        }

        public static List<SYS_Version> GetPagedSYSVersion(Dictionary<Guid, SYS_Version> allSYSVersionList, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            List<SYS_Version> pagedSYSVersionList = new List<SYS_Version>();
            totalItems = allSYSVersionList.Count;
            totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemsPerPage);

            if (pageNo > 0) pageNo = pageNo - 1;
            long startIndex = pageNo * itemsPerPage;
            if (startIndex > totalItems) startIndex = 0;
            long endIndex = startIndex + itemsPerPage;
            if (endIndex > totalItems) endIndex = totalItems;

            int i = 0;
            foreach (KeyValuePair<Guid, SYS_Version> item in allSYSVersionList)
            {
                if ((i >= (int)startIndex) && (i < endIndex))
                {
                    pagedSYSVersionList.Add(item.Value);
                }
                i++;
            }

            return pagedSYSVersionList;
        }

        public static Dictionary<Guid, LTD_Subscriber> LoadLTDSubscriberData()
        {
            Dictionary<Guid, LTD_Subscriber> keyltdSubscriberList = new Dictionary<Guid, LTD_Subscriber>();

            List<LTD_Subscriber> ltdSubscriberList = JsonStore<LTD_Subscriber>.LoadData(false);
            foreach (LTD_Subscriber item in ltdSubscriberList)
            {
                keyltdSubscriberList.Add(item.SubscriberGUID, item);
            }
            return keyltdSubscriberList;
        }

        public static bool SaveLTDSubscriberData(Dictionary<Guid, LTD_Subscriber> ltdSubscriberList)
        {
            bool ret = false;
            if (ltdSubscriberList.Count > 0)
            {
                List<LTD_Subscriber> ltdSubscriberValueList = new List<LTD_Subscriber>();
                LTD_Subscriber[] ltdSubscriberArray = new LTD_Subscriber[ltdSubscriberList.Values.Count];
                ltdSubscriberList.Values.CopyTo(ltdSubscriberArray, 0);
                ltdSubscriberValueList.AddRange(ltdSubscriberArray);
                ret = JsonStore<LTD_Subscriber>.SaveData(ltdSubscriberValueList, true);
            }

            return ret;
        }

        public static List<LTD_Subscriber> GetPagedLTDSubscriber(Dictionary<Guid, LTD_Subscriber> allLTDSubscriberList, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            List<LTD_Subscriber> pagedLTDSubscriberList = new List<LTD_Subscriber>();
            totalItems = allLTDSubscriberList.Count;
            totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemsPerPage);

            if (pageNo > 0) pageNo = pageNo - 1;
            long startIndex = pageNo * itemsPerPage;
            if (startIndex > totalItems) startIndex = 0;
            long endIndex = startIndex + itemsPerPage;
            if (endIndex > totalItems) endIndex = totalItems;

            int i = 0;
            foreach (KeyValuePair<Guid, LTD_Subscriber> item in allLTDSubscriberList)
            {
                if ((i >= (int)startIndex) && (i < endIndex))
                {
                    pagedLTDSubscriberList.Add(item.Value);
                }
                i++;
            }

            return pagedLTDSubscriberList;
        }

        public static Dictionary<Guid, LTD_InfoCategory> LoadLTDInfoCategoryData()
        {
            Dictionary<Guid, LTD_InfoCategory> keyltdInfoCategoryList = new Dictionary<Guid, LTD_InfoCategory>();

            List<LTD_InfoCategory> ltdInfoCategoryList = JsonStore<LTD_InfoCategory>.LoadData(false);
            foreach (LTD_InfoCategory item in ltdInfoCategoryList)
            {
                keyltdInfoCategoryList.Add(item.InfoCategoryGUID, item);
            }
            return keyltdInfoCategoryList;
        }

        public static bool SaveLTDInfoCategoryData(Dictionary<Guid, LTD_InfoCategory> ltdInfoCategoryList)
        {
            bool ret = false;
            if (ltdInfoCategoryList.Count > 0)
            {
                List<LTD_InfoCategory> ltdInfoCategoryValueList = new List<LTD_InfoCategory>();
                LTD_InfoCategory[] ltdInfoCategoryArray = new LTD_InfoCategory[ltdInfoCategoryList.Values.Count];
                ltdInfoCategoryList.Values.CopyTo(ltdInfoCategoryArray, 0);
                ltdInfoCategoryValueList.AddRange(ltdInfoCategoryArray);
                ret = JsonStore<LTD_InfoCategory>.SaveData(ltdInfoCategoryValueList, true);
            }

            return ret;
        }

        public static List<LTD_InfoCategory> GetPagedLTDInfoCategory(Dictionary<Guid, LTD_InfoCategory> allLTDInfoCategoryList, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            List<LTD_InfoCategory> pagedLTDInfoCategoryList = new List<LTD_InfoCategory>();
            totalItems = allLTDInfoCategoryList.Count;
            totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)itemsPerPage);

            if (pageNo > 0) pageNo = pageNo - 1;
            long startIndex = pageNo * itemsPerPage;
            if (startIndex > totalItems) startIndex = 0;
            long endIndex = startIndex + itemsPerPage;
            if (endIndex > totalItems) endIndex = totalItems;

            int i = 0;
            foreach (KeyValuePair<Guid, LTD_InfoCategory> item in allLTDInfoCategoryList)
            {
                if ((i >= (int)startIndex) && (i < endIndex))
                {
                    pagedLTDInfoCategoryList.Add(item.Value);
                }
                i++;
            }

            return pagedLTDInfoCategoryList;
        }
    }
}
