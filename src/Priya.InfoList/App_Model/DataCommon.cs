using System;
using System.Globalization;
using System.Collections.Generic;

using Arshu.Core.Basic.Log;
using Arshu.Core.Common;

using Priya.Security.Utils;
using Priya.InfoList.Entity;
using Priya.InfoList.Data;

namespace Priya.InfoList.Model
{
    public enum CommonRefType
    {
        None = 1,
        PageInfo =2,
    }

    public enum CommonDataType
    {
        GeneralData = 1,
        CommentData = 2,
    }

    internal class DataCommon
    {
        #region CNS_DataRefType

        #region Get

        public static CNS_DataRefType GetCnsDataRefType(long dataRefTypeID)
        {
            CNS_DataRefType cnsDataRefType = DataSource.GetCnsDataRefType(dataRefTypeID, "");
            return cnsDataRefType;
        }

        public static CNS_DataRefType GetCnsDataRefType(string refType)
        {
            return DataSource.GetCnsDataRefType(refType);
        }

        public static CNS_DataRefType GetDefaultCnsDataRefType()
        {
            return DataSource.GetDefaultCnsDataRefType();
        }

        public static long GetDefaultCnsDataRefTypeId()
        {
            return DataSource.GetDefaultCnsDataRefTypeId();
        }

        public static long GetMaxCnsDataRefTypeId()
        {
            return DataSource.GetMaxCnsDataRefTypeId("");
        }

        #endregion

        #region Get All

        public static List<CNS_DataRefType> GetAllCnsDataRefType()
        {
            long totalPages = 0;
            long totalItems = 0;
            List<CNS_DataRefType> cnsDataRefTypeList = DataSource.GetPagedCnsDataRefType(0, 100, out totalPages, out totalItems, "", "");

            return cnsDataRefTypeList;
        }

        #endregion

        #region Get Page

        public static List<CNS_DataRefType> GetAllCnsDataRefType(long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;

            List<CNS_DataRefType> cnsDataRefTypeList = DataSource.GetPagedCnsDataRefType(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "");

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return cnsDataRefTypeList;
        }

        #endregion

        #region Save

        public static long SaveCnsDataRefType(string dataRefType, bool dataRefTypeIsDefault, bool dataRefTypeIsActive, long dataRefTypeId, out string retMessage)
        {
            long id = 0;
            string message = "";
            try
            {
                bool validationFail = false;
                if (UtilsSecurity.HaveAdminRole() == false)
                {
                    message = "Please login as User Having Admin to Save RefType";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (string.IsNullOrEmpty(dataRefType) == true)
                    {
                        message = "Missing or Empty DataRefType Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    CNS_DataRefType cnsDataRefTypeExisting = GetCnsDataRefType(dataRefType);
                    if ((cnsDataRefTypeExisting != null) && (cnsDataRefTypeExisting.DataRefTypeID != dataRefTypeId))
                    {
                        message = "The DataRefType [" + dataRefType + "] allready exists";
                        validationFail = true;
                    }
                }

                var cnsDataRefType = new CNS_DataRefType();
                if (validationFail == false)
                {
                    if (dataRefTypeId != 0)
                    {
                        cnsDataRefType = GetCnsDataRefType(dataRefTypeId);
                        if (cnsDataRefType == null)
                        {
                            message = "The DataRefType having ID [" + dataRefTypeId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                    }
                }

                if (validationFail == false)
                {
                    long maxRefTypeId = GetMaxCnsDataRefTypeId() + 1;
                    cnsDataRefType.IsActive = dataRefTypeIsActive;
                    if (dataRefTypeId == 0)
                    {
                        cnsDataRefType.Sequence = maxRefTypeId;
                        cnsDataRefType.IsActive = true;
                        cnsDataRefType.IsSystem = false;
                    }
                    cnsDataRefType.DataRefType = dataRefType;
                    cnsDataRefType.IsDefault = dataRefTypeIsDefault;


                    DataSource.BeginTransaction();
                    if (cnsDataRefType.IsDefault)
                    {
                        CNS_DataRefType defaultCnsDataRefType = GetDefaultCnsDataRefType();
                        if (defaultCnsDataRefType != null)
                        {
                            if (defaultCnsDataRefType.DataRefTypeID != cnsDataRefType.DataRefTypeID)
                            {
                                defaultCnsDataRefType.IsDefault = false;
                                DataSource.UpdateCnsDataRefType(defaultCnsDataRefType);
                            }
                        }
                    }

                    if (cnsDataRefType.DataRefTypeID == 0)
                    {
                        cnsDataRefType.DataRefTypeID = maxRefTypeId;
                        DataSource.InsertCnsDataRefType(cnsDataRefType);
                        id = cnsDataRefType.DataRefTypeID;
                    }
                    else
                    {
                        DataSource.UpdateCnsDataRefType(cnsDataRefType);
                        id = cnsDataRefType.DataRefTypeID;
                    }
                    DataSource.CompleteTransaction();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataCommon-SaveCnsDataRefType", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return id;
        }

        #endregion

        #region Delete

        public static bool DeleteCnsDataRefType(long dataRefTypeId, out string retMessage)
        {
            bool ret = false;
            string message = "";
            try
            {
                bool validationFail = false;
                if (UtilsSecurity.HaveAdminRole() == false)
                {
                    message = "Please login as User having admin Role to Delete RefType";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (dataRefTypeId == 0)
                    {
                        message = "Missing or Empty DataRefType ID Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    CNS_DataRefType cnsDataRefTypeExisting = GetCnsDataRefType(dataRefTypeId);
                    if (cnsDataRefTypeExisting == null)
                    {
                        message = "The DataRefType with ID [" + dataRefTypeId.ToString(CultureInfo.InvariantCulture) + "] does not exist";
                        validationFail = true;
                    }
                    else if (cnsDataRefTypeExisting.IsSystem)
                    {
                        message = "The System Data RefType Cannot be Deleted";
                        validationFail = true;
                    }
                    else if (cnsDataRefTypeExisting.IsDefault)
                    {
                        message = "Please set another DataRefType as Default and then Delete this RefType";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {

                    DataSource.BeginTransaction();
                    DataSource.DeleteCnsDataRefType(dataRefTypeId);
                    DataSource.CompleteTransaction();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataCommon-DeleteCnsDataRefType", message);
            }

            retMessage = message;
            return ret;
        }

        #endregion

        #endregion

        #region CNS_DataType

        #region Get

        public static CNS_DataType GetCnsDataType(long dataTypeID)
        {
            CNS_DataType cnsDataType = DataSource.GetCnsDataType(dataTypeID, "");
            return cnsDataType;
        }

        public static CNS_DataType GetCnsDataType(string dataType)
        {
            return DataSource.GetCnsDataType(dataType);
        }

        public static CNS_DataType GetDefaultCnsDataType()
        {
            return DataSource.GetDefaultCnsDataType();
        }

        public static long GetDefaultCnsDataTypeId()
        {
            return DataSource.GetDefaultCnsDataTypeId();
        }

        public static long GetMaxCnsDataTypeId()
        {
            return DataSource.GetMaxCnsDataTypeId("");
        }

        #endregion

        #region Get All

        public static List<CNS_DataType> GetAllCnsDataType()
        {
            long totalPages = 0;
            long totalItems = 0;
            List<CNS_DataType> cnsDataTypeList = DataSource.GetPagedCnsDataType(1, 100, out totalPages, out totalItems, "", "");

            return cnsDataTypeList;
        }

        #endregion

        #region Get Page

        public static List<CNS_DataType> GetAllCnsDataType(long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;

            List<CNS_DataType> cncDataTypeList = DataSource.GetPagedCnsDataType(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "");

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return cncDataTypeList;
        }

        #endregion

        #region Save

        public static long SaveCnsDataType(string dataTypeName, bool dataTypeIsDefault, bool dataTypeIsActive, long dataTypeId, out string retMessage)
        {
            long id = 0;
            string message = "";
            try
            {
                bool validationFail = false;
                if (UtilsSecurity.HaveAdminRole() == false)
                {
                    message = "Please login as User Having Admin Role to Save RefType";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (string.IsNullOrEmpty(dataTypeName) == true)
                    {
                        message = "Missing or Empty DataType Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    CNS_DataType cnsDataTypeExisting = GetCnsDataType(dataTypeName);
                    if ((cnsDataTypeExisting != null) && (cnsDataTypeExisting.DataTypeID != dataTypeId))
                    {
                        message = "The DataType [" + dataTypeName + "] allready exists";
                        validationFail = true;
                    }
                }

                var cnsDataType = new CNS_DataType();
                if (validationFail == false)
                {
                    if (dataTypeId != 0)
                    {
                        cnsDataType = GetCnsDataType(dataTypeId);
                        if (cnsDataType == null)
                        {
                            message = "The DataType having ID [" + dataTypeId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                    }
                }

                if (validationFail == false)
                {
                    long maxDataTypeId = GetMaxCnsDataTypeId() + 1;
                    cnsDataType.IsActive = dataTypeIsActive;
                    if (dataTypeId == 0)
                    {
                        cnsDataType.Sequence = maxDataTypeId;
                        cnsDataType.IsActive = true;
                        cnsDataType.IsSystem = false;
                    }
                    cnsDataType.DataType = dataTypeName;
                    cnsDataType.IsDefault = dataTypeIsDefault;

                    DataSource.BeginTransaction();
                    if (cnsDataType.IsDefault)
                    {
                        CNS_DataType defaultCnsDataType = GetDefaultCnsDataType();
                        if (defaultCnsDataType != null)
                        {
                            if (defaultCnsDataType.DataTypeID != cnsDataType.DataTypeID)
                            {
                                defaultCnsDataType.IsDefault = false;
                                DataSource.UpdateCnsDataType(defaultCnsDataType);
                            }
                        }
                    }

                    if (cnsDataType.DataTypeID == 0)
                    {
                        cnsDataType.DataTypeID = maxDataTypeId;
                        DataSource.InsertCnsDataType(cnsDataType);
                        id = cnsDataType.DataTypeID;
                    }
                    else
                    {
                        DataSource.UpdateCnsDataType(cnsDataType);
                        id = cnsDataType.DataTypeID;
                    }
                    DataSource.CompleteTransaction();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataCommon-SaveCnsDataType", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return id;
        }

        #endregion

        #region Delete

        public static bool DeleteCnsDataType(long dataTypeId, out string retMessage)
        {
            bool ret = false;
            string message = "";
            try
            {
                bool validationFail = false;
                if (UtilsSecurity.HaveAdminRole() == false)
                {
                    message = "Please login as User having admin Role to Delete DataType";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (dataTypeId == 0)
                    {
                        message = "Missing or Empty DataType ID Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    CNS_DataType cnsDataTypeExisting = GetCnsDataType(dataTypeId);
                    if (cnsDataTypeExisting == null)
                    {
                        message = "The DataType with ID [" + dataTypeId.ToString(CultureInfo.InvariantCulture) + "] does not exist";
                        validationFail = true;
                    }
                    else if (cnsDataTypeExisting.IsSystem)
                    {
                        message = "The System DataType Cannot be Deleted";
                        validationFail = true;
                    }
                    else if (cnsDataTypeExisting.IsDefault)
                    {
                        message = "Please set another DataType as Default and then Delete this DataType";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    DataSource.BeginTransaction();
                    DataSource.DeleteCnsDataType(dataTypeId);
                    DataSource.CompleteTransaction();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataCommon-DeleteCnsDataType", message);
            }

            retMessage = message;
            return ret;
        }

        #endregion

        #endregion

        #region CND_Data

        #region Get

        public static CND_Data GetCndData(long userDataId)
        {
            return DataSource.GetCndData(userDataId, "");
        }

        public static long GetMaxCndDataId()
        {
            return DataSource.GetMaxCndDataId("");
        }
      
        #endregion
      
        #region Get Page

        public static List<CND_Data> GetAllParentCndData(long dataRefTypeId, long dataRefId, long dataTypeId, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;

            List<CND_Data> cndDataList = DataSource.GetPagedCndData(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where DataRefTypeID=@0 AND DataRefID=@1 AND DataTypeID=@2 AND ParentDataID is null", dataRefTypeId, dataRefId, dataTypeId);

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return cndDataList;
        }

        public static List<CND_Data> GetAllChildCndData(long dataRefTypeId, long dataRefId, long dataTypeId, long parentDataId, long pageNo, long itemsPerPage, out long totalPages, out long totalItems)
        {
            long retTotalPages = 0;
            long retTotalItems = 0;

            List<CND_Data> cndDataList = DataSource.GetPagedCndData(pageNo, itemsPerPage, out retTotalPages, out retTotalItems, "", "Where DataRefTypeID=@0 AND DataRefID=@1 AND DataTypeID=@2 and ParentDataID=@3", dataRefTypeId, dataRefId, dataTypeId, parentDataId);

            totalPages = retTotalPages;
            totalItems = retTotalItems;
            return cndDataList;
        }

        #endregion


        #region Save

        public static long SaveCndData(long parentDataId, long dataRefId, long dataRefTypeId, long dataTypeId, string dataValue, bool dataIsActive,  bool isPublic, long dataId, out string retMessage)
        {
            long id = 0;
            string message = "";
            try
            {
                bool validationFail = false;
                if (isPublic == false)
                {
                    if ((UtilsSecurity.HaveAdminRole() == false) && (UtilsSecurity.HaveAuthorRole() == false))
                    {
                        message = "Please login as User Having Admin/Author Role to Save Data";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    if ((dataRefTypeId == 0) || (dataTypeId == 0) || (string.IsNullOrEmpty(dataValue) == true))
                    {
                        message = "Missing or Empty Data Argument";
                        validationFail = true;
                    }
                }

                CND_Data cndData = new CND_Data();
                if (validationFail == false)
                {
                    if (dataId != 0)
                    {
                        cndData = GetCndData(dataId);
                        if (cndData == null)
                        {
                            message = "The Data having ID [" + dataId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                        else
                        {
                            if ((cndData.UserID != UtilsSecurity.GetUserId()) && (UtilsSecurity.HaveAdminRole() ==false))
                            {
                                message = "The Data having ID [" + dataId + "] can be edited only the User who created it";
                                validationFail = true;
                            }
                        }
                    }
                }

                Guid parentDataGuid = Guid.Empty;
                if (validationFail == false)
                {
                    if (parentDataId != 0)
                    {
                        CND_Data cndDataParent = GetCndData(parentDataId);
                        if (cndDataParent == null)
                        {
                            message = "The Parent Data having ID [" + parentDataId + "] cannot be Retrieved";
                            validationFail = true;
                        }
                        else
                        {
                            parentDataGuid = cndDataParent.DataGUID;
                        }
                    }
                }

                CNS_DataType cnsDataType = GetCnsDataType(dataTypeId);
                if (validationFail == false)
                {
                    if (cnsDataType == null)
                    {
                        message = "The DataType [" + dataTypeId + "] does not exist";
                        validationFail = true;
                    }
                }

                CNS_DataRefType cnsDataRefType = GetCnsDataRefType(dataRefTypeId);
                if (validationFail == false)
                {
                    if (cnsDataRefType == null)
                    {
                        message = "The DataRefType [" + dataRefTypeId + "] does not exist";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    cndData.IsActive = dataIsActive;
                    if (dataId == 0)
                    {
                        cndData.Sequence = GetMaxCndDataId() + 1;
                        cndData.IsActive = true;
                    }

                    cndData.DataTypeID = cnsDataType.DataTypeID;
                    cndData.DataTypeGUID = cnsDataType.DataTypeGUID;

                    cndData.DataRefTypeID = cnsDataRefType.DataRefTypeID;
                    cndData.DataRefTypeGUID = cnsDataRefType.DataRefTypeGUID;

                    cndData.DataRefID = dataRefId;
                    cndData.DataValue = dataValue;

                    if (parentDataId > 0)
                    {
                        cndData.ParentDataID = parentDataId;
                        cndData.ParentDataGUID = parentDataGuid;
                    }

                    DataSource.BeginTransaction();

                    if (cndData.DataID == 0)
                    {
                        DataSource.InsertCndData(cndData);
                        id = cndData.DataID;
                    }
                    else
                    {
                        DataSource.UpdateCndData(cndData);
                        id = cndData.DataID;
                    }
                    DataSource.CompleteTransaction();
                }
            }
            catch (Exception ex)
            {
                id = 0;
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataCommon-SaveCndData", message);
                DataSource.AbortTransaction();
            }

            retMessage = message;
            return id;
        }

        #endregion

        #region Delete

        public static bool DeleteCndData(long dataId, out string retMessage)
        {
            bool ret = false;
            string message = "";
            try
            {
                bool validationFail = false;
                if ((UtilsSecurity.HaveAuthorRole() == false) && (UtilsSecurity.HaveAdminRole() == false))
                {
                    message = "Please login as User having Author Role to Delete Data";
                    validationFail = true;
                }

                if (validationFail == false)
                {
                    if (dataId == 0)
                    {
                        message = "Missing or Empty Data ID Argument";
                        validationFail = true;
                    }
                }

                if (validationFail == false)
                {
                    CND_Data cndDataExisting = GetCndData(dataId);
                    if (cndDataExisting == null)
                    {
                        message = "The Data with ID [" + dataId.ToString(CultureInfo.InvariantCulture) + "] does not exist";
                        validationFail = true;
                    }
                    else if (cndDataExisting.UserID != UtilsSecurity.GetUserId())
                    {
                        if (UtilsSecurity.HaveAdminRole() == false)
                        {
                            message = "You cannot Delete Data of Another User Unless you have Admin Role";
                            validationFail = true;
                        }
                    }

                    if (cndDataExisting != null)
                    {
                        if (HaveChildCndData(cndDataExisting.DataID) ==true)
                        {
                            message = "You cannot Delete this Data Unless you delete all the Child Data of this Data";
                            validationFail = true;
                        }
                    }
                }

                if (validationFail == false)
                {
                    DataSource.BeginTransaction();
                    DataSource.DeleteCndData(dataId);
                    DataSource.CompleteTransaction();
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                message = "DBError:" + ex.Message;
                LogManager.Log(LogLevel.Error, "DataCommon-DeleteCndData", message);
            }

            retMessage = message;
            return ret;
        }

        #endregion

        #region Validation

        public static bool HaveChildCndData(long dataId)
        {
            bool haveChildren = false;
            long totalPages = 0;
            long totalItems = 0;
            List<CND_Data> cndDataList = DataSource.GetPagedCndData(1, 1, out totalPages, out totalItems, "", "Where ParentDataID = @0", dataId);
            if (cndDataList.Count > 0) haveChildren = true;
            return haveChildren;
        }
        #endregion

        #endregion
    }
}