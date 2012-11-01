
using System;
using System.Collections.Generic;
using PetaPoco;

namespace Priya.InfoList.Entity
{
	
    [TableName("CNS_DataType")]
    [PrimaryKey("DataTypeID", autoIncrement=false)]
    [ExplicitColumns]
    public partial class CNS_DataType  
    {
      public bool HaveColumn(string columnName, string columnValue, out bool retValueMatched)
      {
          bool ret = false;
          bool valueMatched = false;

          if (columnName == "DataTypeID") 
          {
              ret = true;
              if (DataTypeID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataTypeGUID") 
          {
              ret = true;
              if (DataTypeGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "RevisionNo") 
          {
              ret = true;
              if (RevisionNo.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataType") 
          {
              ret = true;
              if (DataType.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsDefault") 
          {
              ret = true;
              if (IsDefault.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsSystem") 
          {
              ret = true;
              if (IsSystem.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsActive") 
          {
              ret = true;
              if (IsActive.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "Sequence") 
          {
              ret = true;
              if (Sequence.ToString() == columnValue) valueMatched = true;
          }
          retValueMatched = valueMatched;
          return ret;
      }
      [Column] public long DataTypeID { get; set; }
      [Column] public Guid DataTypeGUID { get; set; }
      [Column] public long RevisionNo { get; set; }
      [Column] public string DataType { get; set; }
      [Column] public bool IsDefault { get; set; }
      [Column] public bool IsSystem { get; set; }
      [Column] public bool IsActive { get; set; }
      [Column] public long Sequence { get; set; }
    }

    [TableName("LTD_InfoSection")]
    [PrimaryKey("InfoSectionID")]
    [ExplicitColumns]
    public partial class LTD_InfoSection  
    {
      public bool HaveColumn(string columnName, string columnValue, out bool retValueMatched)
      {
          bool ret = false;
          bool valueMatched = false;

          if (columnName == "InfoSectionID") 
          {
              ret = true;
              if (InfoSectionID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoSectionGUID") 
          {
              ret = true;
              if (InfoSectionGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "RevisionNo") 
          {
              ret = true;
              if (RevisionNo.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserID") 
          {
              ret = true;
              if (UserID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserGUID") 
          {
              ret = true;
              if (UserGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "CreatedDate") 
          {
              ret = true;
              if (CreatedDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "LastUpdateDate") 
          {
              ret = true;
              if (LastUpdateDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsActive") 
          {
              ret = true;
              if (IsActive.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "Sequence") 
          {
              ret = true;
              if (Sequence.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoPageID") 
          {
              ret = true;
              if (InfoPageID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoPageGUID") 
          {
              ret = true;
              if (InfoPageGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoSectionName") 
          {
              ret = true;
              if (InfoSectionName.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoSectionDescription") 
          {
              ret = true;
              if (InfoSectionDescription.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsDeleted") 
          {
              ret = true;
              if (IsDeleted.ToString() == columnValue) valueMatched = true;
          }
          retValueMatched = valueMatched;
          return ret;
      }
      [Column] public long InfoSectionID { get; set; }
      [Column] public Guid InfoSectionGUID { get; set; }
      [Column] public long RevisionNo { get; set; }
      [Column] public long UserID { get; set; }
      [Column] public Guid UserGUID { get; set; }
      [Column] public DateTime CreatedDate { get; set; }
      [Column] public DateTime LastUpdateDate { get; set; }
      [Column] public bool IsActive { get; set; }
      [Column] public long Sequence { get; set; }
      [Column] public long InfoPageID { get; set; }
      [Column] public Guid InfoPageGUID { get; set; }
      [Column] public string InfoSectionName { get; set; }
      [Column] public string InfoSectionDescription { get; set; }
      [Column] public bool IsDeleted { get; set; }
    }

    [TableName("CNS_DataRefType")]
    [PrimaryKey("DataRefTypeID", autoIncrement=false)]
    [ExplicitColumns]
    public partial class CNS_DataRefType  
    {
      public bool HaveColumn(string columnName, string columnValue, out bool retValueMatched)
      {
          bool ret = false;
          bool valueMatched = false;

          if (columnName == "DataRefTypeID") 
          {
              ret = true;
              if (DataRefTypeID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataRefTypeGUID") 
          {
              ret = true;
              if (DataRefTypeGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "RevisionNo") 
          {
              ret = true;
              if (RevisionNo.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataRefType") 
          {
              ret = true;
              if (DataRefType.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsDefault") 
          {
              ret = true;
              if (IsDefault.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsSystem") 
          {
              ret = true;
              if (IsSystem.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsActive") 
          {
              ret = true;
              if (IsActive.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "Sequence") 
          {
              ret = true;
              if (Sequence.ToString() == columnValue) valueMatched = true;
          }
          retValueMatched = valueMatched;
          return ret;
      }
      [Column] public long DataRefTypeID { get; set; }
      [Column] public Guid DataRefTypeGUID { get; set; }
      [Column] public long RevisionNo { get; set; }
      [Column] public string DataRefType { get; set; }
      [Column] public bool IsDefault { get; set; }
      [Column] public bool IsSystem { get; set; }
      [Column] public bool IsActive { get; set; }
      [Column] public long Sequence { get; set; }
    }

    [TableName("CND_Data")]
    [PrimaryKey("DataID")]
    [ExplicitColumns]
    public partial class CND_Data  
    {
      public bool HaveColumn(string columnName, string columnValue, out bool retValueMatched)
      {
          bool ret = false;
          bool valueMatched = false;

          if (columnName == "DataID") 
          {
              ret = true;
              if (DataID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataGUID") 
          {
              ret = true;
              if (DataGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "RevisionNo") 
          {
              ret = true;
              if (RevisionNo.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserID") 
          {
              ret = true;
              if (UserID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserGUID") 
          {
              ret = true;
              if (UserGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "CreatedDate") 
          {
              ret = true;
              if (CreatedDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "LastUpdateDate") 
          {
              ret = true;
              if (LastUpdateDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "ParentDataID") 
          {
              ret = true;
              if (ParentDataID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "ParentDataGUID") 
          {
              ret = true;
              if (ParentDataGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataTypeID") 
          {
              ret = true;
              if (DataTypeID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataTypeGUID") 
          {
              ret = true;
              if (DataTypeGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataValue") 
          {
              ret = true;
              if (DataValue.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataRefTypeID") 
          {
              ret = true;
              if (DataRefTypeID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataRefTypeGUID") 
          {
              ret = true;
              if (DataRefTypeGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "DataRefID") 
          {
              ret = true;
              if (DataRefID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsActive") 
          {
              ret = true;
              if (IsActive.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsDeleted") 
          {
              ret = true;
              if (IsDeleted.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "Sequence") 
          {
              ret = true;
              if (Sequence.ToString() == columnValue) valueMatched = true;
          }
          retValueMatched = valueMatched;
          return ret;
      }
      [Column] public long DataID { get; set; }
      [Column] public Guid DataGUID { get; set; }
      [Column] public long RevisionNo { get; set; }
      [Column] public long UserID { get; set; }
      [Column] public Guid UserGUID { get; set; }
      [Column] public DateTime CreatedDate { get; set; }
      [Column] public DateTime LastUpdateDate { get; set; }
      [Column] public long? ParentDataID { get; set; }
      [Column] public Guid? ParentDataGUID { get; set; }
      [Column] public long DataTypeID { get; set; }
      [Column] public Guid DataTypeGUID { get; set; }
      [Column] public string DataValue { get; set; }
      [Column] public long DataRefTypeID { get; set; }
      [Column] public Guid DataRefTypeGUID { get; set; }
      [Column] public long? DataRefID { get; set; }
      [Column] public bool IsActive { get; set; }
      [Column] public bool IsDeleted { get; set; }
      [Column] public long Sequence { get; set; }
    }

    [TableName("SYS_Version")]
    [PrimaryKey("VersionNo", autoIncrement=false)]
    [ExplicitColumns]
    public partial class SYS_Version  
    {
      public bool HaveColumn(string columnName, string columnValue, out bool retValueMatched)
      {
          bool ret = false;
          bool valueMatched = false;

          if (columnName == "VersionNo") 
          {
              ret = true;
              if (VersionNo.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "VersionNoGUID") 
          {
              ret = true;
              if (VersionNoGUID.ToString() == columnValue) valueMatched = true;
          }
          retValueMatched = valueMatched;
          return ret;
      }
      [Column] public double VersionNo { get; set; }
      [Column] public Guid VersionNoGUID { get; set; }
    }

    [TableName("LTD_InfoDetail")]
    [PrimaryKey("InfoDetailID")]
    [ExplicitColumns]
    public partial class LTD_InfoDetail  
    {
      public bool HaveColumn(string columnName, string columnValue, out bool retValueMatched)
      {
          bool ret = false;
          bool valueMatched = false;

          if (columnName == "InfoDetailID") 
          {
              ret = true;
              if (InfoDetailID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoDetailGUID") 
          {
              ret = true;
              if (InfoDetailGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "RevisionNo") 
          {
              ret = true;
              if (RevisionNo.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserID") 
          {
              ret = true;
              if (UserID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserGUID") 
          {
              ret = true;
              if (UserGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "CreatedDate") 
          {
              ret = true;
              if (CreatedDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "LastUpdateDate") 
          {
              ret = true;
              if (LastUpdateDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsActive") 
          {
              ret = true;
              if (IsActive.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "Sequence") 
          {
              ret = true;
              if (Sequence.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoPageID") 
          {
              ret = true;
              if (InfoPageID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoPageGUID") 
          {
              ret = true;
              if (InfoPageGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoSectionID") 
          {
              ret = true;
              if (InfoSectionID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoSectionGUID") 
          {
              ret = true;
              if (InfoSectionGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoDetailName") 
          {
              ret = true;
              if (InfoDetailName.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoDetailDescription") 
          {
              ret = true;
              if (InfoDetailDescription.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsDeleted") 
          {
              ret = true;
              if (IsDeleted.ToString() == columnValue) valueMatched = true;
          }
          retValueMatched = valueMatched;
          return ret;
      }
      [Column] public long InfoDetailID { get; set; }
      [Column] public Guid InfoDetailGUID { get; set; }
      [Column] public long RevisionNo { get; set; }
      [Column] public long UserID { get; set; }
      [Column] public Guid UserGUID { get; set; }
      [Column] public DateTime CreatedDate { get; set; }
      [Column] public DateTime LastUpdateDate { get; set; }
      [Column] public bool IsActive { get; set; }
      [Column] public long Sequence { get; set; }
      [Column] public long InfoPageID { get; set; }
      [Column] public Guid InfoPageGUID { get; set; }
      [Column] public long InfoSectionID { get; set; }
      [Column] public Guid InfoSectionGUID { get; set; }
      [Column] public string InfoDetailName { get; set; }
      [Column] public string InfoDetailDescription { get; set; }
      [Column] public bool IsDeleted { get; set; }
    }

    [TableName("LTD_InfoPage")]
    [PrimaryKey("InfoPageID")]
    [ExplicitColumns]
    public partial class LTD_InfoPage  
    {
      public bool HaveColumn(string columnName, string columnValue, out bool retValueMatched)
      {
          bool ret = false;
          bool valueMatched = false;

          if (columnName == "InfoPageID") 
          {
              ret = true;
              if (InfoPageID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoPageGUID") 
          {
              ret = true;
              if (InfoPageGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "RevisionNo") 
          {
              ret = true;
              if (RevisionNo.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "CreatedUserID") 
          {
              ret = true;
              if (CreatedUserID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "CreatedUserGUID") 
          {
              ret = true;
              if (CreatedUserGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserID") 
          {
              ret = true;
              if (UserID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserGUID") 
          {
              ret = true;
              if (UserGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "CreatedDate") 
          {
              ret = true;
              if (CreatedDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "LastUpdateDate") 
          {
              ret = true;
              if (LastUpdateDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsActive") 
          {
              ret = true;
              if (IsActive.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "Sequence") 
          {
              ret = true;
              if (Sequence.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoPageName") 
          {
              ret = true;
              if (InfoPageName.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoPageDescription") 
          {
              ret = true;
              if (InfoPageDescription.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoCategoryID") 
          {
              ret = true;
              if (InfoCategoryID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoCategoryGUID") 
          {
              ret = true;
              if (InfoCategoryGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "ExpiryDate") 
          {
              ret = true;
              if (ExpiryDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsPublic") 
          {
              ret = true;
              if (IsPublic.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "Commentable") 
          {
              ret = true;
              if (Commentable.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "CommentorRoleList") 
          {
              ret = true;
              if (CommentorRoleList.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "AsyncLoading") 
          {
              ret = true;
              if (AsyncLoading.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "AccessGroupID") 
          {
              ret = true;
              if (AccessGroupID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "AccessGroupGUID") 
          {
              ret = true;
              if (AccessGroupGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsDeleted") 
          {
              ret = true;
              if (IsDeleted.ToString() == columnValue) valueMatched = true;
          }
          retValueMatched = valueMatched;
          return ret;
      }
      [Column] public long InfoPageID { get; set; }
      [Column] public Guid InfoPageGUID { get; set; }
      [Column] public long RevisionNo { get; set; }
      [Column] public long CreatedUserID { get; set; }
      [Column] public Guid CreatedUserGUID { get; set; }
      [Column] public long UserID { get; set; }
      [Column] public Guid UserGUID { get; set; }
      [Column] public DateTime CreatedDate { get; set; }
      [Column] public DateTime LastUpdateDate { get; set; }
      [Column] public bool IsActive { get; set; }
      [Column] public long Sequence { get; set; }
      [Column] public string InfoPageName { get; set; }
      [Column] public string InfoPageDescription { get; set; }
      [Column] public long InfoCategoryID { get; set; }
      [Column] public Guid InfoCategoryGUID { get; set; }
      [Column] public DateTime ExpiryDate { get; set; }
      [Column] public bool IsPublic { get; set; }
      [Column] public bool Commentable { get; set; }
      [Column] public string CommentorRoleList { get; set; }
      [Column] public bool AsyncLoading { get; set; }
      [Column] public long? AccessGroupID { get; set; }
      [Column] public Guid? AccessGroupGUID { get; set; }
      [Column] public bool IsDeleted { get; set; }
    }

    [TableName("LTD_Subscriber")]
    [PrimaryKey("SubscriberID")]
    [ExplicitColumns]
    public partial class LTD_Subscriber  
    {
      public bool HaveColumn(string columnName, string columnValue, out bool retValueMatched)
      {
          bool ret = false;
          bool valueMatched = false;

          if (columnName == "SubscriberID") 
          {
              ret = true;
              if (SubscriberID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "SubscriberGUID") 
          {
              ret = true;
              if (SubscriberGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "RevisionNo") 
          {
              ret = true;
              if (RevisionNo.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "SubscriberMessage") 
          {
              ret = true;
              if (SubscriberMessage.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "SubscriberEmail") 
          {
              ret = true;
              if (SubscriberEmail.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "CreatedDate") 
          {
              ret = true;
              if (CreatedDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "LastUpdateDate") 
          {
              ret = true;
              if (LastUpdateDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsDeleted") 
          {
              ret = true;
              if (IsDeleted.ToString() == columnValue) valueMatched = true;
          }
          retValueMatched = valueMatched;
          return ret;
      }
      [Column] public long SubscriberID { get; set; }
      [Column] public Guid SubscriberGUID { get; set; }
      [Column] public long RevisionNo { get; set; }
      [Column] public string SubscriberMessage { get; set; }
      [Column] public string SubscriberEmail { get; set; }
      [Column] public DateTime CreatedDate { get; set; }
      [Column] public DateTime LastUpdateDate { get; set; }
      [Column] public bool IsDeleted { get; set; }
    }

    [TableName("LTD_InfoCategory")]
    [PrimaryKey("InfoCategoryID")]
    [ExplicitColumns]
    public partial class LTD_InfoCategory  
    {
      public bool HaveColumn(string columnName, string columnValue, out bool retValueMatched)
      {
          bool ret = false;
          bool valueMatched = false;

          if (columnName == "InfoCategoryID") 
          {
              ret = true;
              if (InfoCategoryID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoCategoryGUID") 
          {
              ret = true;
              if (InfoCategoryGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "RevisionNo") 
          {
              ret = true;
              if (RevisionNo.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "InfoCategoryName") 
          {
              ret = true;
              if (InfoCategoryName.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserID") 
          {
              ret = true;
              if (UserID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "UserGUID") 
          {
              ret = true;
              if (UserGUID.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "CreatedDate") 
          {
              ret = true;
              if (CreatedDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "LastUpdateDate") 
          {
              ret = true;
              if (LastUpdateDate.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsActive") 
          {
              ret = true;
              if (IsActive.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "Sequence") 
          {
              ret = true;
              if (Sequence.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsDefault") 
          {
              ret = true;
              if (IsDefault.ToString() == columnValue) valueMatched = true;
          }
          if (columnName == "IsSystem") 
          {
              ret = true;
              if (IsSystem.ToString() == columnValue) valueMatched = true;
          }
          retValueMatched = valueMatched;
          return ret;
      }
      [Column] public long InfoCategoryID { get; set; }
      [Column] public Guid InfoCategoryGUID { get; set; }
      [Column] public long RevisionNo { get; set; }
      [Column] public string InfoCategoryName { get; set; }
      [Column] public long UserID { get; set; }
      [Column] public Guid UserGUID { get; set; }
      [Column] public DateTime CreatedDate { get; set; }
      [Column] public DateTime LastUpdateDate { get; set; }
      [Column] public bool IsActive { get; set; }
      [Column] public long Sequence { get; set; }
      [Column] public bool IsDefault { get; set; }
      [Column] public bool IsSystem { get; set; }
    }

}
