
using System;
using System.Collections.Generic;

using Arshu.Core.Basic.Log;
using Arshu.Core.IO;
using Arshu.Core.Common;
using Arshu.Core.Json;
using Arshu.Data.Common;
using Arshu.Data.DBMigrate;

using PetaPoco;
using Priya.InfoList.Entity;

namespace Priya.InfoList.Data
{
    internal static partial class DataSource 
    {
		#region User Event

        public static event GetUserInfo OnGetUserInfo;

        private static object _eventLock = new object();
        private static UserInfo GetUserInfo()
        {
            UserInfo userInfo = new UserInfo();

            GetUserInfo eh;
            lock (_eventLock) { eh = OnGetUserInfo; }
            if (eh != null) { userInfo = eh(); }

            return userInfo;
        }

        #endregion

        #region Static Constant

        public static bool CheckSetupDb = true;
		public static bool UseSharedConnection = false;
        public static bool UseFileSystem = false;
        public static string DbName = "infolist.db";
        public static string ConnectionString = "AutoFill"; //Data Source=|DataDirectory|" + DbName.ToLower() + "
        public static string ProviderNameFactory = AppCommon.SQLiteProviderNameFactory;
        //private static string ConnectionString = "Server=localhost;Database=Security_dbo;Uid=;Pwd=;";
        //private static string ProviderNameFactory = AppCommon.MySQLProviderNameFactory; //MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data
		//private static string ConnectionString == "Data Source=localhost;Initial Catalog=" + DatabaseName.Replace(".db", "") + ";Integrated Security=True;MultipleActiveResultSets=true";
        //private static string ProviderNameFactory = AppCommon.MSSQLProviderNameFactory; //System.Data.SqlClient.SqlClientFactory, System.Data

        #endregion

        #region Database Related

        public static void BeginTransaction()
        {
			if (_haveDb == true)
			{
				GetDb.BeginTransaction();
			}
        }

        public static void CompleteTransaction()
        {
			if (_haveDb == true)
			{
				GetDb.CompleteTransaction();
			}
        }

        public static void AbortTransaction()
        {
			if (_haveDb == true)
			{
				GetDb.AbortTransaction();
			}
        }

        private static void InitDb()
        {
            //Important : Ensure the DBName without the Extension is contained in the App Name
            if (CheckSetupDb==true)
            {
                List<SetupData> setupDataList = JsonStore<SetupData>.LoadData(true);
                if (setupDataList.Count > 0)
                {
                    foreach (SetupData setupData in setupDataList)
                    {
                        if (setupData != null)
                        {
                            if (setupData.AppName.ToUpper().Contains(DbName.Replace(".db", "").ToUpper()))
                            {
                                if ((string.IsNullOrEmpty(setupData.DbConnectionString) == false) || (string.IsNullOrEmpty(setupData.DbConnectionString) == false))
                                {
                                    ProviderNameFactory = setupData.DbProviderNameFactory;
                                    ConnectionString = setupData.DbConnectionString;
                                }
                                if ((ConnectionString.ToUpper() == "AutoFill".ToUpper()) && (ProviderNameFactory.ToUpper() == AppCommon.SQLiteProviderNameFactory.ToUpper()))
                                {
                                    string appVersionStr = "";
                                    int idxOfVer = setupData.AppName.IndexOf(".v");
                                    if (idxOfVer > -1)
                                    {
                                        appVersionStr = setupData.AppName.Substring(idxOfVer).Trim();
                                    }
									string additionalConfig = "Version=3;FailIfMissing=True;"; //Pooling=True;Synchronous=Off;journal mode=WAL
                                    ConnectionString = "Data Source=|DataDirectory|" + DbName.Replace(".db", "").ToLower() + appVersionStr + ".db;" + additionalConfig;
                                }
                                break;
                            }
                        }
                    }
                }
            }
        }

		private static bool _haveDb = false;
        private static Database _db;
		private static object _dbLock = new object();
        private static bool HaveDb
        {
            get
            {
				string message = "";
				lock (_dbLock)
				{
					if ((UseFileSystem == false) && (_haveDb ==false))
					{
						InitDb();

						string rootPath = IOManager.RootDirectory;
						string dataPath = IOManager.Combine(rootPath, "App_Data");
						if (ConnectionString.Contains("|DataDirectory|") == true)
						{
							ConnectionString = ConnectionString.Replace("|DataDirectory|", dataPath + IOManager.DirectorySeparator);
						}
						bool checkDb = true;
						if ((ProviderNameFactory.ToUpper() == AppCommon.SQLiteProviderNameFactory.ToUpper()) && (ConnectionString.Contains("AutoFill") == false))
						{
							 string sqliteDbPath = "";
							int idxOfSemiColon =ConnectionString.IndexOf(";");
							if (idxOfSemiColon > -1)
							{
								int idxOfEqual = ConnectionString.IndexOf("=");
								sqliteDbPath = ConnectionString.Substring(idxOfEqual + 1, idxOfSemiColon - idxOfEqual -1);
							}
							else
							{
								sqliteDbPath = ConnectionString.Substring(ConnectionString.IndexOf("=") + 1);
							}
							if (IOManager.CachedFileExists(sqliteDbPath, true, true) ==false)
							{
								checkDb =false ;
								LogManager.Log(LogLevel.Critical, "DataSource-HaveDb", "Sqlite Db Not found in Path [" + sqliteDbPath + "]");
							}                       
						}
						if (checkDb == true)
						{
							_db = DataStore.HaveDb(ConnectionString, ProviderNameFactory, out message);
							 if (_db != null) _haveDb = true;
						}
					}
					if (_haveDb == false) {
						if (UseFileSystem ==false)
						{
							LogManager.Log(LogLevel.Critical, "Priya.InfoList.DataSource-HaveDb", "Have Db is false for " + DbName + "[" + message + "]");
						}
						else
						{
							LogManager.Log(LogLevel.Critical, "Priya.InfoList.DataSource-HaveDb", "Have Db is false for " + DbName + ". Use FileSystem is true");
						}
					}
				}
                return _haveDb;
            }
        }

        private static Database GetDb
        {
            get
            {
				if (_haveDb == false) _haveDb = HaveDb;
                if ((_db != null) && (_haveDb ==true))
                {
                    _db.OnDBException -= new PetaPoco.DBException(OnDBException);
					_db.OnDBException += new PetaPoco.DBException(OnDBException);
					if (ProviderNameFactory == AppCommon.SQLiteProviderNameFactory)
					{
						_db.KeepConnectionAlive = true;
						UseSharedConnection =true;
						//_db.Execute("PRAGMA journal_mode=WAL;");
						//_db.Execute("PRAGMA journal_mode=DELETE;");
					}
					if (ProviderNameFactory == AppCommon.MySQLProviderNameFactory)
					{
						_db.KeepConnectionAlive = false;
						UseSharedConnection =false;
						//_db.Execute("set wait_timeout=28800");
						//_db.Execute("set interactive_timeout=28800");
						//_db.Execute("set net_write_timeout=999");
					}
					if (UseSharedConnection ==true)
					{
						_db.OpenSharedConnection();
					}
                };
                if (_db == null) {
					LogManager.Log(LogLevel.Critical, "Priya.InfoList.DataSource-GetDb", "GetDb is Null for " + DbName);
				}
                return _db;
            }
        }

        private static void OnDBException(Exception ex, string lastCommand, string lastSQL, Database db)
        {
            string message = "";
            if (null != ex.InnerException)
            {
                string stackTrace = ex.InnerException.StackTrace;
                message = "DBError:[" + lastSQL + "][" + lastCommand+ "][" + db.SharedConnectionDepth + "]" + Environment.NewLine + "[" + ex.InnerException.Message + "][" + ex.Message + "]" + Environment.NewLine + "[" + stackTrace + "]";
            }
            else
            {
                string stackTrace = ex.StackTrace;
                message = "DBError:[" + lastSQL + "][" + lastCommand+ "][" + db.SharedConnectionDepth + "]" + Environment.NewLine + "[" + ex.Message + "]" + Environment.NewLine + "[" + stackTrace + "]";
            }
            LogManager.Log(LogLevel.Error, "Priya.InfoList.DataSource-OnDBException", "Error:" + message);
        }

        #endregion

        #region Sync Utilities

        public static long GetMaxRevisionNo(string tableName)
        {
            long revisionNo = -1;
            switch (tableName.ToUpper())
            {
     
                case "CNS_DATATYPE":
                    revisionNo = GetMaxCnsDataTypeRevisionNo();
                    break ;
     
                case "LTD_INFOSECTION":
                    revisionNo = GetMaxLtdInfoSectionRevisionNo();
                    break ;
     
                case "CNS_DATAREFTYPE":
                    revisionNo = GetMaxCnsDataRefTypeRevisionNo();
                    break ;
     
                case "CND_DATA":
                    revisionNo = GetMaxCndDataRevisionNo();
                    break ;
     
                case "LTD_INFODETAIL":
                    revisionNo = GetMaxLtdInfoDetailRevisionNo();
                    break ;
     
                case "LTD_INFOPAGE":
                    revisionNo = GetMaxLtdInfoPageRevisionNo();
                    break ;
     
                case "LTD_SUBSCRIBER":
                    revisionNo = GetMaxLtdSubscriberRevisionNo();
                    break ;
     
                case "LTD_INFOCATEGORY":
                    revisionNo = GetMaxLtdInfoCategoryRevisionNo();
                    break ;
  
                default:
                    break;
            }
            return revisionNo;
        }

        public static string GetJsonRecord(string tableName, long minRevisionNo, long pageNo, long itemsPerPage, out long retTotalPages, out long retTotalItems)
        {
            string jsonRecord = "";
            long totalPages = 0;
            long totalItems = 0;

            switch (tableName.ToUpper())
            {
     
                case "CNS_DATATYPE":
                    List<CNS_DataType> pagedCnsDataTypeData = GetPagedCnsDataType(pageNo, itemsPerPage, out totalPages, out totalItems, "", "WHERE RevisionNo > @0", minRevisionNo);
                    if (pagedCnsDataTypeData.Count > 0)
                    {
                        jsonRecord = SimpleJson.SerializeObject(pagedCnsDataTypeData);
                    }
                    break ;
     
                case "LTD_INFOSECTION":
                    List<LTD_InfoSection> pagedLtdInfoSectionData = GetPagedLtdInfoSection(pageNo, itemsPerPage, out totalPages, out totalItems, "", "WHERE RevisionNo > @0", minRevisionNo);
                    if (pagedLtdInfoSectionData.Count > 0)
                    {
                        jsonRecord = SimpleJson.SerializeObject(pagedLtdInfoSectionData);
                    }
                    break ;
     
                case "CNS_DATAREFTYPE":
                    List<CNS_DataRefType> pagedCnsDataRefTypeData = GetPagedCnsDataRefType(pageNo, itemsPerPage, out totalPages, out totalItems, "", "WHERE RevisionNo > @0", minRevisionNo);
                    if (pagedCnsDataRefTypeData.Count > 0)
                    {
                        jsonRecord = SimpleJson.SerializeObject(pagedCnsDataRefTypeData);
                    }
                    break ;
     
                case "CND_DATA":
                    List<CND_Data> pagedCndDataData = GetPagedCndData(pageNo, itemsPerPage, out totalPages, out totalItems, "", "WHERE RevisionNo > @0", minRevisionNo);
                    if (pagedCndDataData.Count > 0)
                    {
                        jsonRecord = SimpleJson.SerializeObject(pagedCndDataData);
                    }
                    break ;
     
                case "LTD_INFODETAIL":
                    List<LTD_InfoDetail> pagedLtdInfoDetailData = GetPagedLtdInfoDetail(pageNo, itemsPerPage, out totalPages, out totalItems, "", "WHERE RevisionNo > @0", minRevisionNo);
                    if (pagedLtdInfoDetailData.Count > 0)
                    {
                        jsonRecord = SimpleJson.SerializeObject(pagedLtdInfoDetailData);
                    }
                    break ;
     
                case "LTD_INFOPAGE":
                    List<LTD_InfoPage> pagedLtdInfoPageData = GetPagedLtdInfoPage(pageNo, itemsPerPage, out totalPages, out totalItems, "", "WHERE RevisionNo > @0", minRevisionNo);
                    if (pagedLtdInfoPageData.Count > 0)
                    {
                        jsonRecord = SimpleJson.SerializeObject(pagedLtdInfoPageData);
                    }
                    break ;
     
                case "LTD_SUBSCRIBER":
                    List<LTD_Subscriber> pagedLtdSubscriberData = GetPagedLtdSubscriber(pageNo, itemsPerPage, out totalPages, out totalItems, "", "WHERE RevisionNo > @0", minRevisionNo);
                    if (pagedLtdSubscriberData.Count > 0)
                    {
                        jsonRecord = SimpleJson.SerializeObject(pagedLtdSubscriberData);
                    }
                    break ;
     
                case "LTD_INFOCATEGORY":
                    List<LTD_InfoCategory> pagedLtdInfoCategoryData = GetPagedLtdInfoCategory(pageNo, itemsPerPage, out totalPages, out totalItems, "", "WHERE RevisionNo > @0", minRevisionNo);
                    if (pagedLtdInfoCategoryData.Count > 0)
                    {
                        jsonRecord = SimpleJson.SerializeObject(pagedLtdInfoCategoryData);
                    }
                    break ;
  
                default:
                    break;
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return jsonRecord;
        }

        public static void SaveJsonRecord(string tableName, string jsonRecordList)
        {
            switch (tableName.ToUpper())
            {
     
                case "CNS_DATATYPE":
                    CNS_DataType[] importCnsDataTypeData = SimpleJson.DeserializeObject<CNS_DataType[]>(jsonRecordList);
                    using (ITransaction scope = GetDb.GetTransaction())
                    {
                        foreach (CNS_DataType item in importCnsDataTypeData)
                        {
                            CNS_DataType itemCnsDataTypeExisting = DataSource.GetCnsDataType(0, " WHERE DataTypeGUID=@0", item.DataTypeGUID);
                            if (itemCnsDataTypeExisting == null)
                            {
                                GetDb.Insert(item);
                            }
                            else
                            {
                                item.DataTypeID = itemCnsDataTypeExisting.DataTypeID;
                                GetDb.Update(item) ;
                            }
                        }
                        scope.Complete();
                    }
                    break ;
     
                case "LTD_INFOSECTION":
                    LTD_InfoSection[] importLtdInfoSectionData = SimpleJson.DeserializeObject<LTD_InfoSection[]>(jsonRecordList);
                    using (ITransaction scope = GetDb.GetTransaction())
                    {
                        foreach (LTD_InfoSection item in importLtdInfoSectionData)
                        {
                            LTD_InfoSection itemLtdInfoSectionExisting = DataSource.GetLtdInfoSection(0, " WHERE InfoSectionGUID=@0", item.InfoSectionGUID);
                            if (itemLtdInfoSectionExisting == null)
                            {
                                GetDb.Insert(item);
                            }
                            else
                            {
                                item.InfoSectionID = itemLtdInfoSectionExisting.InfoSectionID;
                                GetDb.Update(item) ;
                            }
                        }
                        scope.Complete();
                    }
                    break ;
     
                case "CNS_DATAREFTYPE":
                    CNS_DataRefType[] importCnsDataRefTypeData = SimpleJson.DeserializeObject<CNS_DataRefType[]>(jsonRecordList);
                    using (ITransaction scope = GetDb.GetTransaction())
                    {
                        foreach (CNS_DataRefType item in importCnsDataRefTypeData)
                        {
                            CNS_DataRefType itemCnsDataRefTypeExisting = DataSource.GetCnsDataRefType(0, " WHERE DataRefTypeGUID=@0", item.DataRefTypeGUID);
                            if (itemCnsDataRefTypeExisting == null)
                            {
                                GetDb.Insert(item);
                            }
                            else
                            {
                                item.DataRefTypeID = itemCnsDataRefTypeExisting.DataRefTypeID;
                                GetDb.Update(item) ;
                            }
                        }
                        scope.Complete();
                    }
                    break ;
     
                case "CND_DATA":
                    CND_Data[] importCndDataData = SimpleJson.DeserializeObject<CND_Data[]>(jsonRecordList);
                    using (ITransaction scope = GetDb.GetTransaction())
                    {
                        foreach (CND_Data item in importCndDataData)
                        {
                            CND_Data itemCndDataExisting = DataSource.GetCndData(0, " WHERE DataGUID=@0", item.DataGUID);
                            if (itemCndDataExisting == null)
                            {
                                GetDb.Insert(item);
                            }
                            else
                            {
                                item.DataID = itemCndDataExisting.DataID;
                                GetDb.Update(item) ;
                            }
                        }
                        scope.Complete();
                    }
                    break ;
     
                case "LTD_INFODETAIL":
                    LTD_InfoDetail[] importLtdInfoDetailData = SimpleJson.DeserializeObject<LTD_InfoDetail[]>(jsonRecordList);
                    using (ITransaction scope = GetDb.GetTransaction())
                    {
                        foreach (LTD_InfoDetail item in importLtdInfoDetailData)
                        {
                            LTD_InfoDetail itemLtdInfoDetailExisting = DataSource.GetLtdInfoDetail(0, " WHERE InfoDetailGUID=@0", item.InfoDetailGUID);
                            if (itemLtdInfoDetailExisting == null)
                            {
                                GetDb.Insert(item);
                            }
                            else
                            {
                                item.InfoDetailID = itemLtdInfoDetailExisting.InfoDetailID;
                                GetDb.Update(item) ;
                            }
                        }
                        scope.Complete();
                    }
                    break ;
     
                case "LTD_INFOPAGE":
                    LTD_InfoPage[] importLtdInfoPageData = SimpleJson.DeserializeObject<LTD_InfoPage[]>(jsonRecordList);
                    using (ITransaction scope = GetDb.GetTransaction())
                    {
                        foreach (LTD_InfoPage item in importLtdInfoPageData)
                        {
                            LTD_InfoPage itemLtdInfoPageExisting = DataSource.GetLtdInfoPage(0, " WHERE InfoPageGUID=@0", item.InfoPageGUID);
                            if (itemLtdInfoPageExisting == null)
                            {
                                GetDb.Insert(item);
                            }
                            else
                            {
                                item.InfoPageID = itemLtdInfoPageExisting.InfoPageID;
                                GetDb.Update(item) ;
                            }
                        }
                        scope.Complete();
                    }
                    break ;
     
                case "LTD_SUBSCRIBER":
                    LTD_Subscriber[] importLtdSubscriberData = SimpleJson.DeserializeObject<LTD_Subscriber[]>(jsonRecordList);
                    using (ITransaction scope = GetDb.GetTransaction())
                    {
                        foreach (LTD_Subscriber item in importLtdSubscriberData)
                        {
                            LTD_Subscriber itemLtdSubscriberExisting = DataSource.GetLtdSubscriber(0, " WHERE SubscriberGUID=@0", item.SubscriberGUID);
                            if (itemLtdSubscriberExisting == null)
                            {
                                GetDb.Insert(item);
                            }
                            else
                            {
                                item.SubscriberID = itemLtdSubscriberExisting.SubscriberID;
                                GetDb.Update(item) ;
                            }
                        }
                        scope.Complete();
                    }
                    break ;
     
                case "LTD_INFOCATEGORY":
                    LTD_InfoCategory[] importLtdInfoCategoryData = SimpleJson.DeserializeObject<LTD_InfoCategory[]>(jsonRecordList);
                    using (ITransaction scope = GetDb.GetTransaction())
                    {
                        foreach (LTD_InfoCategory item in importLtdInfoCategoryData)
                        {
                            LTD_InfoCategory itemLtdInfoCategoryExisting = DataSource.GetLtdInfoCategory(0, " WHERE InfoCategoryGUID=@0", item.InfoCategoryGUID);
                            if (itemLtdInfoCategoryExisting == null)
                            {
                                GetDb.Insert(item);
                            }
                            else
                            {
                                item.InfoCategoryID = itemLtdInfoCategoryExisting.InfoCategoryID;
                                GetDb.Update(item) ;
                            }
                        }
                        scope.Complete();
                    }
                    break ;
  
                default:
                    break;
            }
        }

        #endregion

        #region Utilities

        private static Dictionary<string, string> GetWhereList(string whereClause, params object[] whereArgs)
        {
            Dictionary<string, string> whereList = new Dictionary<string, string>();

            return whereList;
        }

        #endregion


  
        #region CNS_DataType Query Related

        #region Get

        public static CNS_DataType GetCnsDataType(long dataTypeID, string orWhereClause, params object[] orWhereArgs)
        {
            CNS_DataType cnsDataType = null;
            
            if (HaveDb == true)
            {
                if (orWhereClause.Trim().Length == 0)
                {
                    cnsDataType = GetDb.SingleOrDefault<CNS_DataType>("SELECT * FROM CNS_DataType Where DataTypeID=@0", dataTypeID);
                }
                else
                {
                    if ((string.IsNullOrEmpty(orWhereClause) ==false) && (orWhereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) orWhereClause = " WHERE " + orWhereClause;
                    cnsDataType = GetDb.SingleOrDefault<CNS_DataType>("SELECT * FROM CNS_DataType " + orWhereClause, orWhereArgs);
                }
            }
            else
            {
                Dictionary<Guid, CNS_DataType> fileCNSDataTypeList = FileSource.LoadCNSDataTypeData();
                foreach (KeyValuePair<Guid, CNS_DataType> item in fileCNSDataTypeList)
                {
                    bool found = true ;
                    if (orWhereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(orWhereClause, orWhereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }
                            else
                            {
                                found = false;
                            }
                        }
                        if (found ==true)
                        {
                            cnsDataType = item.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Value.DataTypeID == dataTypeID)
                        {
                            cnsDataType = item.Value;
                            break;
                        }
                    }                    
                }
            }

            return cnsDataType;
        }

        public static long GetMaxCnsDataTypeId(string whereClause, params object[] whereArgs)
        {
            long maxCnsDataTypeId = 0;
           
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                maxCnsDataTypeId = GetDb.Single<long>("SELECT MAX(DataTypeID) AS MAX_ID FROM CNS_DataType " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, CNS_DataType> fileCNSDataTypeList = FileSource.LoadCNSDataTypeData();
                foreach (KeyValuePair<Guid, CNS_DataType> item in fileCNSDataTypeList)
                {
                    bool found = true ;
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    
                    if (found ==true)
                    {
                        if (item.Value.DataTypeID >= maxCnsDataTypeId)
                        {
                            maxCnsDataTypeId = item.Value.DataTypeID;
                        }
                    }
                }
            }
            return maxCnsDataTypeId;
        }
          
        private static long GetMaxCnsDataTypeRevisionNo()
        {
            long maxCnsDataTypeRevisionNo = 0;
            if (HaveDb == true)
            {
                maxCnsDataTypeRevisionNo = GetDb.Single<long>("SELECT MAX(RevisionNo) AS MAX_ID FROM CNS_DataType");
            }
            else
            {
                Dictionary<Guid, CNS_DataType> fileCNSDataTypeList = FileSource.LoadCNSDataTypeData();
                foreach (KeyValuePair<Guid, CNS_DataType> item in fileCNSDataTypeList)
                {
                    if (item.Value.RevisionNo >= maxCnsDataTypeRevisionNo)
                    {
                        maxCnsDataTypeRevisionNo = item.Value.RevisionNo;
                    }
                }
            }
            return maxCnsDataTypeRevisionNo;
        }
          

          
		
		/*
        public static long GetCnsDataTypeCount(string whereClause, params object[] whereArgs)
        {
            long cnsDataTypeCount = 0;
            
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                cnsDataTypeCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM CNS_DataType " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, CNS_DataType> fileCNSDataTypeList = FileSource.LoadCNSDataTypeData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, CNS_DataType> filteredCNSDataTypeList = new Dictionary<Guid, CNS_DataType>();
                    foreach (KeyValuePair<Guid, CNS_DataType> item in fileCNSDataTypeList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredCNSDataTypeList.Add(item.Key, item.Value);
                    }
                    cnsDataTypeCount = filteredCNSDataTypeList.Count;
                }
                else
                {
                    cnsDataTypeCount = fileCNSDataTypeList.Count;
                }
            }
            return cnsDataTypeCount;
        }
		*/

        #endregion

        #region Get All

		/*
        public static List<CNS_DataType> GetAllCnsDataType(string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<CNS_DataType> allCnsDataTypeList = new List<CNS_DataType>();
          
            if (HaveDb == true)
            {
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                allCnsDataTypeList = GetDb.Fetch<CNS_DataType>("SELECT * FROM CNS_DataType " + whereClause + " " + orderByClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, CNS_DataType> fileCnsDataTypeList = FileSource.LoadCNSDataTypeData();
                foreach (KeyValuePair<Guid, CNS_DataType> item in fileCnsDataTypeList)
                {
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) allCnsDataTypeList.Add(item.Value);
                    }
                    else
                    {
                        allCnsDataTypeList.Add(item.Value);
                    }
                }
            }

            return allCnsDataTypeList;
        }
		*/

        #endregion

        #region Get Paged

        public static List<CNS_DataType> GetPagedCnsDataType(long pageNo, long itemsPerPage, out long totalPages, out long totalItems, string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<CNS_DataType> pagedCnsDataTypeList = new List<CNS_DataType>();
            if (pageNo <= 0) pageNo = 1;
            if (itemsPerPage <= 0) itemsPerPage = 1;

            long retTotalPages = 0;
            long retTotalItems = 0;
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                Page<CNS_DataType> pagedData = GetDb.Page<CNS_DataType>(pageNo, itemsPerPage, "SELECT * FROM CNS_DataType " + whereClause + " " + orderByClause, whereArgs);
                totalPages = pagedData.TotalPages;
                totalItems = pagedData.TotalItems;
                if ((pagedData.Items.Count == 0) && (totalPages == 1))
                {
                    pagedData = GetDb.Page<CNS_DataType>(1, itemsPerPage, "SELECT * FROM CNS_DataType " + whereClause + " " + orderByClause, whereArgs);
                    totalPages = pagedData.TotalPages;
                    totalItems = pagedData.TotalItems;
                }
                pagedCnsDataTypeList = pagedData.Items;
            }
            else
            {
                Dictionary<Guid, CNS_DataType> fileCnsDataTypeList = FileSource.LoadCNSDataTypeData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, CNS_DataType> filteredCNSDataTypeList = new Dictionary<Guid, CNS_DataType>();
                    foreach (KeyValuePair<Guid, CNS_DataType> item in fileCnsDataTypeList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredCNSDataTypeList.Add(item.Key, item.Value);
                    }
                    pagedCnsDataTypeList = FileSource.GetPagedCNSDataType(filteredCNSDataTypeList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    pagedCnsDataTypeList = FileSource.GetPagedCNSDataType(fileCnsDataTypeList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return pagedCnsDataTypeList;
        }

        #endregion

        #region Insert

        public static long InsertCnsDataType(CNS_DataType cnsDataType)
        {
            long id = 0;
            if (cnsDataType.DataTypeGUID != Guid.Empty) throw new Exception("Cannot Set the GUID for a Insert");
            cnsDataType.DataTypeGUID = Guid.NewGuid();
            cnsDataType.RevisionNo = GetMaxCnsDataTypeRevisionNo() + 1;

            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Insert(cnsDataType);
                    id = cnsDataType.DataTypeID;
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, CNS_DataType> fileCnsDataTypeList = FileSource.LoadCNSDataTypeData();
  
                fileCnsDataTypeList.Add(cnsDataType.DataTypeGUID, cnsDataType);
                FileSource.SaveCNSDataTypeData(fileCnsDataTypeList);

                id = cnsDataType.DataTypeID;
            }

            return id;
        }

        #endregion

        #region Update

        public static bool UpdateCnsDataType(CNS_DataType cnsDataType)
        {
            bool ret = false;
            if (HaveDb == true)
            {
                CNS_DataType cnsDataTypeExisting = GetCnsDataType(cnsDataType.DataTypeID, "");
                if (cnsDataTypeExisting != null)
                {
					cnsDataType.RevisionNo = GetMaxCnsDataTypeRevisionNo() + 1;                      
					using (ITransaction scope = GetDb.GetTransaction())
					{
                        GetDb.Update(cnsDataType);
                        scope.Complete();
                        ret = true;
                    }
                }
            }
            else
            {
                Dictionary<Guid, CNS_DataType> fileCnsDataTypeList = FileSource.LoadCNSDataTypeData();                
                if (fileCnsDataTypeList.ContainsKey(cnsDataType.DataTypeGUID) == true)
                {
                    fileCnsDataTypeList.Remove(cnsDataType.DataTypeGUID);
                    fileCnsDataTypeList.Add(cnsDataType.DataTypeGUID, cnsDataType);
                    FileSource.SaveCNSDataTypeData(fileCnsDataTypeList);
                }
            }

            return ret;
        }

        #endregion

        #region Delete

        public static void DeleteCnsDataType(long cnsDataTypeId)
        {
            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Delete<CNS_DataType>(cnsDataTypeId);
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, CNS_DataType> fileCnsDataTypeList = FileSource.LoadCNSDataTypeData();
                Guid cnsDataTypeGuidToRemove = Guid.Empty;
                foreach (KeyValuePair<Guid, CNS_DataType> item in fileCnsDataTypeList)
                {
                    if (item.Value.DataTypeID == cnsDataTypeId)
                    {
                        cnsDataTypeGuidToRemove = item.Key;
                        break;
                    }
                }
                if (cnsDataTypeGuidToRemove != Guid.Empty)
                {
                    fileCnsDataTypeList.Remove(cnsDataTypeGuidToRemove);
                    FileSource.SaveCNSDataTypeData(fileCnsDataTypeList);
                }
            }

        }

        #endregion

        #endregion
  
        #region LTD_InfoSection Query Related

        #region Get

        public static LTD_InfoSection GetLtdInfoSection(long infoSectionID, string orWhereClause, params object[] orWhereArgs)
        {
            LTD_InfoSection ltdInfoSection = null;
            
            if (HaveDb == true)
            {
                if (orWhereClause.Trim().Length == 0)
                {
                    ltdInfoSection = GetDb.SingleOrDefault<LTD_InfoSection>("SELECT * FROM LTD_InfoSection Where InfoSectionID=@0", infoSectionID);
                }
                else
                {
                    if ((string.IsNullOrEmpty(orWhereClause) ==false) && (orWhereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) orWhereClause = " WHERE " + orWhereClause;
                    ltdInfoSection = GetDb.SingleOrDefault<LTD_InfoSection>("SELECT * FROM LTD_InfoSection " + orWhereClause, orWhereArgs);
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLTDInfoSectionList = FileSource.LoadLTDInfoSectionData();
                foreach (KeyValuePair<Guid, LTD_InfoSection> item in fileLTDInfoSectionList)
                {
                    bool found = true ;
                    if (orWhereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(orWhereClause, orWhereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }
                            else
                            {
                                found = false;
                            }
                        }
                        if (found ==true)
                        {
                            ltdInfoSection = item.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Value.InfoSectionID == infoSectionID)
                        {
                            ltdInfoSection = item.Value;
                            break;
                        }
                    }                    
                }
            }

            return ltdInfoSection;
        }

        public static long GetMaxLtdInfoSectionId(string whereClause, params object[] whereArgs)
        {
            long maxLtdInfoSectionId = 0;
           
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                maxLtdInfoSectionId = GetDb.Single<long>("SELECT MAX(InfoSectionID) AS MAX_ID FROM LTD_InfoSection " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLTDInfoSectionList = FileSource.LoadLTDInfoSectionData();
                foreach (KeyValuePair<Guid, LTD_InfoSection> item in fileLTDInfoSectionList)
                {
                    bool found = true ;
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    
                    if (found ==true)
                    {
                        if (item.Value.InfoSectionID >= maxLtdInfoSectionId)
                        {
                            maxLtdInfoSectionId = item.Value.InfoSectionID;
                        }
                    }
                }
            }
            return maxLtdInfoSectionId;
        }
          
        private static long GetMaxLtdInfoSectionRevisionNo()
        {
            long maxLtdInfoSectionRevisionNo = 0;
            if (HaveDb == true)
            {
                maxLtdInfoSectionRevisionNo = GetDb.Single<long>("SELECT MAX(RevisionNo) AS MAX_ID FROM LTD_InfoSection");
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLTDInfoSectionList = FileSource.LoadLTDInfoSectionData();
                foreach (KeyValuePair<Guid, LTD_InfoSection> item in fileLTDInfoSectionList)
                {
                    if (item.Value.RevisionNo >= maxLtdInfoSectionRevisionNo)
                    {
                        maxLtdInfoSectionRevisionNo = item.Value.RevisionNo;
                    }
                }
            }
            return maxLtdInfoSectionRevisionNo;
        }
          

      
        /*    
        private static DateTime GetLtdInfoSectionLastUpdateDate()
        {
            DateTime LtdInfoSectionLastUpdateDate = DateTime.MinValue;
            if (HaveDb == true)
            {
                LtdInfoSectionLastUpdateDate = GetDb.Single<DateTime>("SELECT MAX(LastUpdateDate) AS MAX_DATE FROM LTD_InfoSection");
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLTDInfoSectionList = FileSource.LoadLTDInfoSectionData();
                foreach (KeyValuePair<Guid, LTD_InfoSection> item in fileLTDInfoSectionList)
                {
                    //Less than zero(-1) - This instance is earlier than passed value.
                    //Zero (0)	- This instance is the same as value. 
                    //Greater than zero(1) - This instance is later than value. 
                    if (item.Value.LastUpdateDate.CompareTo(LtdInfoSectionLastUpdateDate) > 0)
                    {
                        LtdInfoSectionLastUpdateDate = item.Value.LastUpdateDate;
                    }
                }
            }
            return LtdInfoSectionLastUpdateDate;
        }
        */
          
		
		/*
        public static long GetLtdInfoSectionCount(string whereClause, params object[] whereArgs)
        {
            long ltdInfoSectionCount = 0;
            
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                ltdInfoSectionCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM LTD_InfoSection " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLTDInfoSectionList = FileSource.LoadLTDInfoSectionData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_InfoSection> filteredLTDInfoSectionList = new Dictionary<Guid, LTD_InfoSection>();
                    foreach (KeyValuePair<Guid, LTD_InfoSection> item in fileLTDInfoSectionList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDInfoSectionList.Add(item.Key, item.Value);
                    }
                    ltdInfoSectionCount = filteredLTDInfoSectionList.Count;
                }
                else
                {
                    ltdInfoSectionCount = fileLTDInfoSectionList.Count;
                }
            }
            return ltdInfoSectionCount;
        }
		*/

        #endregion

        #region Get All

		/*
        public static List<LTD_InfoSection> GetAllLtdInfoSection(string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_InfoSection> allLtdInfoSectionList = new List<LTD_InfoSection>();
          
            if (HaveDb == true)
            {
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                allLtdInfoSectionList = GetDb.Fetch<LTD_InfoSection>("SELECT * FROM LTD_InfoSection " + whereClause + " " + orderByClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLtdInfoSectionList = FileSource.LoadLTDInfoSectionData();
                foreach (KeyValuePair<Guid, LTD_InfoSection> item in fileLtdInfoSectionList)
                {
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) allLtdInfoSectionList.Add(item.Value);
                    }
                    else
                    {
                        allLtdInfoSectionList.Add(item.Value);
                    }
                }
            }

            return allLtdInfoSectionList;
        }
		*/

        #endregion

        #region Get Paged

        public static List<LTD_InfoSection> GetPagedLtdInfoSection(long pageNo, long itemsPerPage, out long totalPages, out long totalItems, string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_InfoSection> pagedLtdInfoSectionList = new List<LTD_InfoSection>();
            if (pageNo <= 0) pageNo = 1;
            if (itemsPerPage <= 0) itemsPerPage = 1;

            long retTotalPages = 0;
            long retTotalItems = 0;
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                Page<LTD_InfoSection> pagedData = GetDb.Page<LTD_InfoSection>(pageNo, itemsPerPage, "SELECT * FROM LTD_InfoSection " + whereClause + " " + orderByClause, whereArgs);
                totalPages = pagedData.TotalPages;
                totalItems = pagedData.TotalItems;
                if ((pagedData.Items.Count == 0) && (totalPages == 1))
                {
                    pagedData = GetDb.Page<LTD_InfoSection>(1, itemsPerPage, "SELECT * FROM LTD_InfoSection " + whereClause + " " + orderByClause, whereArgs);
                    totalPages = pagedData.TotalPages;
                    totalItems = pagedData.TotalItems;
                }
                pagedLtdInfoSectionList = pagedData.Items;
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLtdInfoSectionList = FileSource.LoadLTDInfoSectionData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_InfoSection> filteredLTDInfoSectionList = new Dictionary<Guid, LTD_InfoSection>();
                    foreach (KeyValuePair<Guid, LTD_InfoSection> item in fileLtdInfoSectionList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDInfoSectionList.Add(item.Key, item.Value);
                    }
                    pagedLtdInfoSectionList = FileSource.GetPagedLTDInfoSection(filteredLTDInfoSectionList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    pagedLtdInfoSectionList = FileSource.GetPagedLTDInfoSection(fileLtdInfoSectionList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return pagedLtdInfoSectionList;
        }

        #endregion

        #region Insert

        public static long InsertLtdInfoSection(LTD_InfoSection ltdInfoSection)
        {
            long id = 0;
            if (ltdInfoSection.InfoSectionGUID != Guid.Empty) throw new Exception("Cannot Set the GUID for a Insert");
            ltdInfoSection.InfoSectionGUID = Guid.NewGuid();
          
			 UserInfo userInfo = OnGetUserInfo();
            ltdInfoSection.UserID = userInfo.Id;
            ltdInfoSection.UserGUID = userInfo.Guid;
            ltdInfoSection.CreatedDate = DateTime.UtcNow;
            ltdInfoSection.LastUpdateDate = DateTime.UtcNow;
            ltdInfoSection.RevisionNo = GetMaxLtdInfoSectionRevisionNo() + 1;

            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Insert(ltdInfoSection);
                    id = ltdInfoSection.InfoSectionID;
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLtdInfoSectionList = FileSource.LoadLTDInfoSectionData();
	  			ltdInfoSection.InfoSectionID = GetMaxLtdInfoSectionId("") + 1;
  
                fileLtdInfoSectionList.Add(ltdInfoSection.InfoSectionGUID, ltdInfoSection);
                FileSource.SaveLTDInfoSectionData(fileLtdInfoSectionList);

                id = ltdInfoSection.InfoSectionID;
            }

            return id;
        }

        #endregion

        #region Update

        public static bool UpdateLtdInfoSection(LTD_InfoSection ltdInfoSection)
        {
            bool ret = false;
          
			 UserInfo userInfo = OnGetUserInfo();
            ltdInfoSection.UserID = userInfo.Id;
            ltdInfoSection.UserGUID = userInfo.Guid;
            ltdInfoSection.LastUpdateDate = DateTime.UtcNow;
            if (HaveDb == true)
            {
                LTD_InfoSection ltdInfoSectionExisting = GetLtdInfoSection(ltdInfoSection.InfoSectionID, "");
                if (ltdInfoSectionExisting != null)
                {
					ltdInfoSection.RevisionNo = GetMaxLtdInfoSectionRevisionNo() + 1;                      
					using (ITransaction scope = GetDb.GetTransaction())
					{
                        GetDb.Update(ltdInfoSection);
                        scope.Complete();
                        ret = true;
                    }
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLtdInfoSectionList = FileSource.LoadLTDInfoSectionData();                
                if (fileLtdInfoSectionList.ContainsKey(ltdInfoSection.InfoSectionGUID) == true)
                {
                    fileLtdInfoSectionList.Remove(ltdInfoSection.InfoSectionGUID);
                    fileLtdInfoSectionList.Add(ltdInfoSection.InfoSectionGUID, ltdInfoSection);
                    FileSource.SaveLTDInfoSectionData(fileLtdInfoSectionList);
                }
            }

            return ret;
        }

        #endregion

        #region Delete

        public static void DeleteLtdInfoSection(long ltdInfoSectionId)
        {
            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Delete<LTD_InfoSection>(ltdInfoSectionId);
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoSection> fileLtdInfoSectionList = FileSource.LoadLTDInfoSectionData();
                Guid ltdInfoSectionGuidToRemove = Guid.Empty;
                foreach (KeyValuePair<Guid, LTD_InfoSection> item in fileLtdInfoSectionList)
                {
                    if (item.Value.InfoSectionID == ltdInfoSectionId)
                    {
                        ltdInfoSectionGuidToRemove = item.Key;
                        break;
                    }
                }
                if (ltdInfoSectionGuidToRemove != Guid.Empty)
                {
                    fileLtdInfoSectionList.Remove(ltdInfoSectionGuidToRemove);
                    FileSource.SaveLTDInfoSectionData(fileLtdInfoSectionList);
                }
            }

        }

        #endregion

        #endregion
  
        #region CNS_DataRefType Query Related

        #region Get

        public static CNS_DataRefType GetCnsDataRefType(long dataRefTypeID, string orWhereClause, params object[] orWhereArgs)
        {
            CNS_DataRefType cnsDataRefType = null;
            
            if (HaveDb == true)
            {
                if (orWhereClause.Trim().Length == 0)
                {
                    cnsDataRefType = GetDb.SingleOrDefault<CNS_DataRefType>("SELECT * FROM CNS_DataRefType Where DataRefTypeID=@0", dataRefTypeID);
                }
                else
                {
                    if ((string.IsNullOrEmpty(orWhereClause) ==false) && (orWhereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) orWhereClause = " WHERE " + orWhereClause;
                    cnsDataRefType = GetDb.SingleOrDefault<CNS_DataRefType>("SELECT * FROM CNS_DataRefType " + orWhereClause, orWhereArgs);
                }
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> fileCNSDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
                foreach (KeyValuePair<Guid, CNS_DataRefType> item in fileCNSDataRefTypeList)
                {
                    bool found = true ;
                    if (orWhereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(orWhereClause, orWhereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }
                            else
                            {
                                found = false;
                            }
                        }
                        if (found ==true)
                        {
                            cnsDataRefType = item.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Value.DataRefTypeID == dataRefTypeID)
                        {
                            cnsDataRefType = item.Value;
                            break;
                        }
                    }                    
                }
            }

            return cnsDataRefType;
        }

        public static long GetMaxCnsDataRefTypeId(string whereClause, params object[] whereArgs)
        {
            long maxCnsDataRefTypeId = 0;
           
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                maxCnsDataRefTypeId = GetDb.Single<long>("SELECT MAX(DataRefTypeID) AS MAX_ID FROM CNS_DataRefType " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> fileCNSDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
                foreach (KeyValuePair<Guid, CNS_DataRefType> item in fileCNSDataRefTypeList)
                {
                    bool found = true ;
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    
                    if (found ==true)
                    {
                        if (item.Value.DataRefTypeID >= maxCnsDataRefTypeId)
                        {
                            maxCnsDataRefTypeId = item.Value.DataRefTypeID;
                        }
                    }
                }
            }
            return maxCnsDataRefTypeId;
        }
          
        private static long GetMaxCnsDataRefTypeRevisionNo()
        {
            long maxCnsDataRefTypeRevisionNo = 0;
            if (HaveDb == true)
            {
                maxCnsDataRefTypeRevisionNo = GetDb.Single<long>("SELECT MAX(RevisionNo) AS MAX_ID FROM CNS_DataRefType");
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> fileCNSDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
                foreach (KeyValuePair<Guid, CNS_DataRefType> item in fileCNSDataRefTypeList)
                {
                    if (item.Value.RevisionNo >= maxCnsDataRefTypeRevisionNo)
                    {
                        maxCnsDataRefTypeRevisionNo = item.Value.RevisionNo;
                    }
                }
            }
            return maxCnsDataRefTypeRevisionNo;
        }
          

          
		
		/*
        public static long GetCnsDataRefTypeCount(string whereClause, params object[] whereArgs)
        {
            long cnsDataRefTypeCount = 0;
            
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                cnsDataRefTypeCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM CNS_DataRefType " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> fileCNSDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, CNS_DataRefType> filteredCNSDataRefTypeList = new Dictionary<Guid, CNS_DataRefType>();
                    foreach (KeyValuePair<Guid, CNS_DataRefType> item in fileCNSDataRefTypeList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredCNSDataRefTypeList.Add(item.Key, item.Value);
                    }
                    cnsDataRefTypeCount = filteredCNSDataRefTypeList.Count;
                }
                else
                {
                    cnsDataRefTypeCount = fileCNSDataRefTypeList.Count;
                }
            }
            return cnsDataRefTypeCount;
        }
		*/

        #endregion

        #region Get All

		/*
        public static List<CNS_DataRefType> GetAllCnsDataRefType(string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<CNS_DataRefType> allCnsDataRefTypeList = new List<CNS_DataRefType>();
          
            if (HaveDb == true)
            {
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                allCnsDataRefTypeList = GetDb.Fetch<CNS_DataRefType>("SELECT * FROM CNS_DataRefType " + whereClause + " " + orderByClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> fileCnsDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
                foreach (KeyValuePair<Guid, CNS_DataRefType> item in fileCnsDataRefTypeList)
                {
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) allCnsDataRefTypeList.Add(item.Value);
                    }
                    else
                    {
                        allCnsDataRefTypeList.Add(item.Value);
                    }
                }
            }

            return allCnsDataRefTypeList;
        }
		*/

        #endregion

        #region Get Paged

        public static List<CNS_DataRefType> GetPagedCnsDataRefType(long pageNo, long itemsPerPage, out long totalPages, out long totalItems, string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<CNS_DataRefType> pagedCnsDataRefTypeList = new List<CNS_DataRefType>();
            if (pageNo <= 0) pageNo = 1;
            if (itemsPerPage <= 0) itemsPerPage = 1;

            long retTotalPages = 0;
            long retTotalItems = 0;
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                Page<CNS_DataRefType> pagedData = GetDb.Page<CNS_DataRefType>(pageNo, itemsPerPage, "SELECT * FROM CNS_DataRefType " + whereClause + " " + orderByClause, whereArgs);
                totalPages = pagedData.TotalPages;
                totalItems = pagedData.TotalItems;
                if ((pagedData.Items.Count == 0) && (totalPages == 1))
                {
                    pagedData = GetDb.Page<CNS_DataRefType>(1, itemsPerPage, "SELECT * FROM CNS_DataRefType " + whereClause + " " + orderByClause, whereArgs);
                    totalPages = pagedData.TotalPages;
                    totalItems = pagedData.TotalItems;
                }
                pagedCnsDataRefTypeList = pagedData.Items;
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> fileCnsDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, CNS_DataRefType> filteredCNSDataRefTypeList = new Dictionary<Guid, CNS_DataRefType>();
                    foreach (KeyValuePair<Guid, CNS_DataRefType> item in fileCnsDataRefTypeList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredCNSDataRefTypeList.Add(item.Key, item.Value);
                    }
                    pagedCnsDataRefTypeList = FileSource.GetPagedCNSDataRefType(filteredCNSDataRefTypeList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    pagedCnsDataRefTypeList = FileSource.GetPagedCNSDataRefType(fileCnsDataRefTypeList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return pagedCnsDataRefTypeList;
        }

        #endregion

        #region Insert

        public static long InsertCnsDataRefType(CNS_DataRefType cnsDataRefType)
        {
            long id = 0;
            if (cnsDataRefType.DataRefTypeGUID != Guid.Empty) throw new Exception("Cannot Set the GUID for a Insert");
            cnsDataRefType.DataRefTypeGUID = Guid.NewGuid();
            cnsDataRefType.RevisionNo = GetMaxCnsDataRefTypeRevisionNo() + 1;

            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Insert(cnsDataRefType);
                    id = cnsDataRefType.DataRefTypeID;
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> fileCnsDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
  
                fileCnsDataRefTypeList.Add(cnsDataRefType.DataRefTypeGUID, cnsDataRefType);
                FileSource.SaveCNSDataRefTypeData(fileCnsDataRefTypeList);

                id = cnsDataRefType.DataRefTypeID;
            }

            return id;
        }

        #endregion

        #region Update

        public static bool UpdateCnsDataRefType(CNS_DataRefType cnsDataRefType)
        {
            bool ret = false;
            if (HaveDb == true)
            {
                CNS_DataRefType cnsDataRefTypeExisting = GetCnsDataRefType(cnsDataRefType.DataRefTypeID, "");
                if (cnsDataRefTypeExisting != null)
                {
					cnsDataRefType.RevisionNo = GetMaxCnsDataRefTypeRevisionNo() + 1;                      
					using (ITransaction scope = GetDb.GetTransaction())
					{
                        GetDb.Update(cnsDataRefType);
                        scope.Complete();
                        ret = true;
                    }
                }
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> fileCnsDataRefTypeList = FileSource.LoadCNSDataRefTypeData();                
                if (fileCnsDataRefTypeList.ContainsKey(cnsDataRefType.DataRefTypeGUID) == true)
                {
                    fileCnsDataRefTypeList.Remove(cnsDataRefType.DataRefTypeGUID);
                    fileCnsDataRefTypeList.Add(cnsDataRefType.DataRefTypeGUID, cnsDataRefType);
                    FileSource.SaveCNSDataRefTypeData(fileCnsDataRefTypeList);
                }
            }

            return ret;
        }

        #endregion

        #region Delete

        public static void DeleteCnsDataRefType(long cnsDataRefTypeId)
        {
            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Delete<CNS_DataRefType>(cnsDataRefTypeId);
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, CNS_DataRefType> fileCnsDataRefTypeList = FileSource.LoadCNSDataRefTypeData();
                Guid cnsDataRefTypeGuidToRemove = Guid.Empty;
                foreach (KeyValuePair<Guid, CNS_DataRefType> item in fileCnsDataRefTypeList)
                {
                    if (item.Value.DataRefTypeID == cnsDataRefTypeId)
                    {
                        cnsDataRefTypeGuidToRemove = item.Key;
                        break;
                    }
                }
                if (cnsDataRefTypeGuidToRemove != Guid.Empty)
                {
                    fileCnsDataRefTypeList.Remove(cnsDataRefTypeGuidToRemove);
                    FileSource.SaveCNSDataRefTypeData(fileCnsDataRefTypeList);
                }
            }

        }

        #endregion

        #endregion
  
        #region CND_Data Query Related

        #region Get

        public static CND_Data GetCndData(long dataID, string orWhereClause, params object[] orWhereArgs)
        {
            CND_Data cndData = null;
            
            if (HaveDb == true)
            {
                if (orWhereClause.Trim().Length == 0)
                {
                    cndData = GetDb.SingleOrDefault<CND_Data>("SELECT * FROM CND_Data Where DataID=@0", dataID);
                }
                else
                {
                    if ((string.IsNullOrEmpty(orWhereClause) ==false) && (orWhereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) orWhereClause = " WHERE " + orWhereClause;
                    cndData = GetDb.SingleOrDefault<CND_Data>("SELECT * FROM CND_Data " + orWhereClause, orWhereArgs);
                }
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCNDDataList = FileSource.LoadCNDDataData();
                foreach (KeyValuePair<Guid, CND_Data> item in fileCNDDataList)
                {
                    bool found = true ;
                    if (orWhereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(orWhereClause, orWhereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }
                            else
                            {
                                found = false;
                            }
                        }
                        if (found ==true)
                        {
                            cndData = item.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Value.DataID == dataID)
                        {
                            cndData = item.Value;
                            break;
                        }
                    }                    
                }
            }

            return cndData;
        }

        public static long GetMaxCndDataId(string whereClause, params object[] whereArgs)
        {
            long maxCndDataId = 0;
           
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                maxCndDataId = GetDb.Single<long>("SELECT MAX(DataID) AS MAX_ID FROM CND_Data " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCNDDataList = FileSource.LoadCNDDataData();
                foreach (KeyValuePair<Guid, CND_Data> item in fileCNDDataList)
                {
                    bool found = true ;
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    
                    if (found ==true)
                    {
                        if (item.Value.DataID >= maxCndDataId)
                        {
                            maxCndDataId = item.Value.DataID;
                        }
                    }
                }
            }
            return maxCndDataId;
        }
          
        private static long GetMaxCndDataRevisionNo()
        {
            long maxCndDataRevisionNo = 0;
            if (HaveDb == true)
            {
                maxCndDataRevisionNo = GetDb.Single<long>("SELECT MAX(RevisionNo) AS MAX_ID FROM CND_Data");
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCNDDataList = FileSource.LoadCNDDataData();
                foreach (KeyValuePair<Guid, CND_Data> item in fileCNDDataList)
                {
                    if (item.Value.RevisionNo >= maxCndDataRevisionNo)
                    {
                        maxCndDataRevisionNo = item.Value.RevisionNo;
                    }
                }
            }
            return maxCndDataRevisionNo;
        }
          

      
        /*    
        private static DateTime GetCndDataLastUpdateDate()
        {
            DateTime CndDataLastUpdateDate = DateTime.MinValue;
            if (HaveDb == true)
            {
                CndDataLastUpdateDate = GetDb.Single<DateTime>("SELECT MAX(LastUpdateDate) AS MAX_DATE FROM CND_Data");
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCNDDataList = FileSource.LoadCNDDataData();
                foreach (KeyValuePair<Guid, CND_Data> item in fileCNDDataList)
                {
                    //Less than zero(-1) - This instance is earlier than passed value.
                    //Zero (0)	- This instance is the same as value. 
                    //Greater than zero(1) - This instance is later than value. 
                    if (item.Value.LastUpdateDate.CompareTo(CndDataLastUpdateDate) > 0)
                    {
                        CndDataLastUpdateDate = item.Value.LastUpdateDate;
                    }
                }
            }
            return CndDataLastUpdateDate;
        }
        */
          
		
		/*
        public static long GetCndDataCount(string whereClause, params object[] whereArgs)
        {
            long cndDataCount = 0;
            
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                cndDataCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM CND_Data " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCNDDataList = FileSource.LoadCNDDataData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, CND_Data> filteredCNDDataList = new Dictionary<Guid, CND_Data>();
                    foreach (KeyValuePair<Guid, CND_Data> item in fileCNDDataList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredCNDDataList.Add(item.Key, item.Value);
                    }
                    cndDataCount = filteredCNDDataList.Count;
                }
                else
                {
                    cndDataCount = fileCNDDataList.Count;
                }
            }
            return cndDataCount;
        }
		*/

        #endregion

        #region Get All

		/*
        public static List<CND_Data> GetAllCndData(string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<CND_Data> allCndDataList = new List<CND_Data>();
          
            if (HaveDb == true)
            {
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                allCndDataList = GetDb.Fetch<CND_Data>("SELECT * FROM CND_Data " + whereClause + " " + orderByClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCndDataList = FileSource.LoadCNDDataData();
                foreach (KeyValuePair<Guid, CND_Data> item in fileCndDataList)
                {
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) allCndDataList.Add(item.Value);
                    }
                    else
                    {
                        allCndDataList.Add(item.Value);
                    }
                }
            }

            return allCndDataList;
        }
		*/

        #endregion

        #region Get Paged

        public static List<CND_Data> GetPagedCndData(long pageNo, long itemsPerPage, out long totalPages, out long totalItems, string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<CND_Data> pagedCndDataList = new List<CND_Data>();
            if (pageNo <= 0) pageNo = 1;
            if (itemsPerPage <= 0) itemsPerPage = 1;

            long retTotalPages = 0;
            long retTotalItems = 0;
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                Page<CND_Data> pagedData = GetDb.Page<CND_Data>(pageNo, itemsPerPage, "SELECT * FROM CND_Data " + whereClause + " " + orderByClause, whereArgs);
                totalPages = pagedData.TotalPages;
                totalItems = pagedData.TotalItems;
                if ((pagedData.Items.Count == 0) && (totalPages == 1))
                {
                    pagedData = GetDb.Page<CND_Data>(1, itemsPerPage, "SELECT * FROM CND_Data " + whereClause + " " + orderByClause, whereArgs);
                    totalPages = pagedData.TotalPages;
                    totalItems = pagedData.TotalItems;
                }
                pagedCndDataList = pagedData.Items;
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCndDataList = FileSource.LoadCNDDataData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, CND_Data> filteredCNDDataList = new Dictionary<Guid, CND_Data>();
                    foreach (KeyValuePair<Guid, CND_Data> item in fileCndDataList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredCNDDataList.Add(item.Key, item.Value);
                    }
                    pagedCndDataList = FileSource.GetPagedCNDData(filteredCNDDataList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    pagedCndDataList = FileSource.GetPagedCNDData(fileCndDataList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return pagedCndDataList;
        }

        #endregion

        #region Insert

        public static long InsertCndData(CND_Data cndData)
        {
            long id = 0;
            if (cndData.DataGUID != Guid.Empty) throw new Exception("Cannot Set the GUID for a Insert");
            cndData.DataGUID = Guid.NewGuid();
          
			 UserInfo userInfo = OnGetUserInfo();
            cndData.UserID = userInfo.Id;
            cndData.UserGUID = userInfo.Guid;
            cndData.CreatedDate = DateTime.UtcNow;
            cndData.LastUpdateDate = DateTime.UtcNow;
            cndData.RevisionNo = GetMaxCndDataRevisionNo() + 1;

            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Insert(cndData);
                    id = cndData.DataID;
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCndDataList = FileSource.LoadCNDDataData();
	  			cndData.DataID = GetMaxCndDataId("") + 1;
  
                fileCndDataList.Add(cndData.DataGUID, cndData);
                FileSource.SaveCNDDataData(fileCndDataList);

                id = cndData.DataID;
            }

            return id;
        }

        #endregion

        #region Update

        public static bool UpdateCndData(CND_Data cndData)
        {
            bool ret = false;
          
			 UserInfo userInfo = OnGetUserInfo();
            cndData.UserID = userInfo.Id;
            cndData.UserGUID = userInfo.Guid;
            cndData.LastUpdateDate = DateTime.UtcNow;
            if (HaveDb == true)
            {
                CND_Data cndDataExisting = GetCndData(cndData.DataID, "");
                if (cndDataExisting != null)
                {
					cndData.RevisionNo = GetMaxCndDataRevisionNo() + 1;                      
					using (ITransaction scope = GetDb.GetTransaction())
					{
                        GetDb.Update(cndData);
                        scope.Complete();
                        ret = true;
                    }
                }
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCndDataList = FileSource.LoadCNDDataData();                
                if (fileCndDataList.ContainsKey(cndData.DataGUID) == true)
                {
                    fileCndDataList.Remove(cndData.DataGUID);
                    fileCndDataList.Add(cndData.DataGUID, cndData);
                    FileSource.SaveCNDDataData(fileCndDataList);
                }
            }

            return ret;
        }

        #endregion

        #region Delete

        public static void DeleteCndData(long cndDataId)
        {
            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Delete<CND_Data>(cndDataId);
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, CND_Data> fileCndDataList = FileSource.LoadCNDDataData();
                Guid cndDataGuidToRemove = Guid.Empty;
                foreach (KeyValuePair<Guid, CND_Data> item in fileCndDataList)
                {
                    if (item.Value.DataID == cndDataId)
                    {
                        cndDataGuidToRemove = item.Key;
                        break;
                    }
                }
                if (cndDataGuidToRemove != Guid.Empty)
                {
                    fileCndDataList.Remove(cndDataGuidToRemove);
                    FileSource.SaveCNDDataData(fileCndDataList);
                }
            }

        }

        #endregion

        #endregion
  
        #region SYS_Version Query Related

        #region Get

        public static SYS_Version GetSysVersion(double versionNo, string orWhereClause, params object[] orWhereArgs)
        {
            SYS_Version sysVersion = null;
            
            if (HaveDb == true)
            {
                if (orWhereClause.Trim().Length == 0)
                {
                    sysVersion = GetDb.SingleOrDefault<SYS_Version>("SELECT * FROM SYS_Version Where VersionNo=@0", versionNo);
                }
                else
                {
                    if ((string.IsNullOrEmpty(orWhereClause) ==false) && (orWhereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) orWhereClause = " WHERE " + orWhereClause;
                    sysVersion = GetDb.SingleOrDefault<SYS_Version>("SELECT * FROM SYS_Version " + orWhereClause, orWhereArgs);
                }
            }
            else
            {
                Dictionary<Guid, SYS_Version> fileSYSVersionList = FileSource.LoadSYSVersionData();
                foreach (KeyValuePair<Guid, SYS_Version> item in fileSYSVersionList)
                {
                    bool found = true ;
                    if (orWhereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(orWhereClause, orWhereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }
                            else
                            {
                                found = false;
                            }
                        }
                        if (found ==true)
                        {
                            sysVersion = item.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Value.VersionNo == versionNo)
                        {
                            sysVersion = item.Value;
                            break;
                        }
                    }                    
                }
            }

            return sysVersion;
        }

        public static double GetMaxSysVersionId(string whereClause, params object[] whereArgs)
        {
            double maxSysVersionId = 0;
           
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                maxSysVersionId = GetDb.Single<long>("SELECT MAX(VersionNo) AS MAX_ID FROM SYS_Version " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, SYS_Version> fileSYSVersionList = FileSource.LoadSYSVersionData();
                foreach (KeyValuePair<Guid, SYS_Version> item in fileSYSVersionList)
                {
                    bool found = true ;
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    
                    if (found ==true)
                    {
                        if (item.Value.VersionNo >= maxSysVersionId)
                        {
                            maxSysVersionId = item.Value.VersionNo;
                        }
                    }
                }
            }
            return maxSysVersionId;
        }
          

          
		
		/*
        public static long GetSysVersionCount(string whereClause, params object[] whereArgs)
        {
            long sysVersionCount = 0;
            
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                sysVersionCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM SYS_Version " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, SYS_Version> fileSYSVersionList = FileSource.LoadSYSVersionData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, SYS_Version> filteredSYSVersionList = new Dictionary<Guid, SYS_Version>();
                    foreach (KeyValuePair<Guid, SYS_Version> item in fileSYSVersionList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredSYSVersionList.Add(item.Key, item.Value);
                    }
                    sysVersionCount = filteredSYSVersionList.Count;
                }
                else
                {
                    sysVersionCount = fileSYSVersionList.Count;
                }
            }
            return sysVersionCount;
        }
		*/

        #endregion

        #region Get All

		/*
        public static List<SYS_Version> GetAllSysVersion(string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<SYS_Version> allSysVersionList = new List<SYS_Version>();
          
            if (HaveDb == true)
            {
                                
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                allSysVersionList = GetDb.Fetch<SYS_Version>("SELECT * FROM SYS_Version " + whereClause + " " + orderByClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, SYS_Version> fileSysVersionList = FileSource.LoadSYSVersionData();
                foreach (KeyValuePair<Guid, SYS_Version> item in fileSysVersionList)
                {
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) allSysVersionList.Add(item.Value);
                    }
                    else
                    {
                        allSysVersionList.Add(item.Value);
                    }
                }
            }

            return allSysVersionList;
        }
		*/

        #endregion

        #region Get Paged

        public static List<SYS_Version> GetPagedSysVersion(long pageNo, long itemsPerPage, out long totalPages, out long totalItems, string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<SYS_Version> pagedSysVersionList = new List<SYS_Version>();
            if (pageNo <= 0) pageNo = 1;
            if (itemsPerPage <= 0) itemsPerPage = 1;

            long retTotalPages = 0;
            long retTotalItems = 0;
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                                
                Page<SYS_Version> pagedData = GetDb.Page<SYS_Version>(pageNo, itemsPerPage, "SELECT * FROM SYS_Version " + whereClause + " " + orderByClause, whereArgs);
                totalPages = pagedData.TotalPages;
                totalItems = pagedData.TotalItems;
                if ((pagedData.Items.Count == 0) && (totalPages == 1))
                {
                    pagedData = GetDb.Page<SYS_Version>(1, itemsPerPage, "SELECT * FROM SYS_Version " + whereClause + " " + orderByClause, whereArgs);
                    totalPages = pagedData.TotalPages;
                    totalItems = pagedData.TotalItems;
                }
                pagedSysVersionList = pagedData.Items;
            }
            else
            {
                Dictionary<Guid, SYS_Version> fileSysVersionList = FileSource.LoadSYSVersionData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, SYS_Version> filteredSYSVersionList = new Dictionary<Guid, SYS_Version>();
                    foreach (KeyValuePair<Guid, SYS_Version> item in fileSysVersionList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredSYSVersionList.Add(item.Key, item.Value);
                    }
                    pagedSysVersionList = FileSource.GetPagedSYSVersion(filteredSYSVersionList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    pagedSysVersionList = FileSource.GetPagedSYSVersion(fileSysVersionList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return pagedSysVersionList;
        }

        #endregion

        #region Insert

        public static double InsertSysVersion(SYS_Version sysVersion)
        {
            double id = 0;
            if (sysVersion.VersionNoGUID != Guid.Empty) throw new Exception("Cannot Set the GUID for a Insert");
            sysVersion.VersionNoGUID = Guid.NewGuid();


            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Insert(sysVersion);
                    id = sysVersion.VersionNo;
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, SYS_Version> fileSysVersionList = FileSource.LoadSYSVersionData();
  
                fileSysVersionList.Add(sysVersion.VersionNoGUID, sysVersion);
                FileSource.SaveSYSVersionData(fileSysVersionList);

                id = sysVersion.VersionNo;
            }

            return id;
        }

        #endregion

        #region Update

        public static bool UpdateSysVersion(SYS_Version sysVersion)
        {
            bool ret = false;
            if (HaveDb == true)
            {
                SYS_Version sysVersionExisting = GetSysVersion(sysVersion.VersionNo, "");
                if (sysVersionExisting != null)
                {
                      
					using (ITransaction scope = GetDb.GetTransaction())
					{
                        GetDb.Update(sysVersion);
                        scope.Complete();
                        ret = true;
                    }
                }
            }
            else
            {
                Dictionary<Guid, SYS_Version> fileSysVersionList = FileSource.LoadSYSVersionData();                
                if (fileSysVersionList.ContainsKey(sysVersion.VersionNoGUID) == true)
                {
                    fileSysVersionList.Remove(sysVersion.VersionNoGUID);
                    fileSysVersionList.Add(sysVersion.VersionNoGUID, sysVersion);
                    FileSource.SaveSYSVersionData(fileSysVersionList);
                }
            }

            return ret;
        }

        #endregion

        #region Delete

        public static void DeleteSysVersion(long sysVersionId)
        {
            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Delete<SYS_Version>(sysVersionId);
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, SYS_Version> fileSysVersionList = FileSource.LoadSYSVersionData();
                Guid sysVersionGuidToRemove = Guid.Empty;
                foreach (KeyValuePair<Guid, SYS_Version> item in fileSysVersionList)
                {
                    if (item.Value.VersionNo == sysVersionId)
                    {
                        sysVersionGuidToRemove = item.Key;
                        break;
                    }
                }
                if (sysVersionGuidToRemove != Guid.Empty)
                {
                    fileSysVersionList.Remove(sysVersionGuidToRemove);
                    FileSource.SaveSYSVersionData(fileSysVersionList);
                }
            }

        }

        #endregion

        #endregion
  
        #region LTD_InfoDetail Query Related

        #region Get

        public static LTD_InfoDetail GetLtdInfoDetail(long infoDetailID, string orWhereClause, params object[] orWhereArgs)
        {
            LTD_InfoDetail ltdInfoDetail = null;
            
            if (HaveDb == true)
            {
                if (orWhereClause.Trim().Length == 0)
                {
                    ltdInfoDetail = GetDb.SingleOrDefault<LTD_InfoDetail>("SELECT * FROM LTD_InfoDetail Where InfoDetailID=@0", infoDetailID);
                }
                else
                {
                    if ((string.IsNullOrEmpty(orWhereClause) ==false) && (orWhereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) orWhereClause = " WHERE " + orWhereClause;
                    ltdInfoDetail = GetDb.SingleOrDefault<LTD_InfoDetail>("SELECT * FROM LTD_InfoDetail " + orWhereClause, orWhereArgs);
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLTDInfoDetailList = FileSource.LoadLTDInfoDetailData();
                foreach (KeyValuePair<Guid, LTD_InfoDetail> item in fileLTDInfoDetailList)
                {
                    bool found = true ;
                    if (orWhereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(orWhereClause, orWhereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }
                            else
                            {
                                found = false;
                            }
                        }
                        if (found ==true)
                        {
                            ltdInfoDetail = item.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Value.InfoDetailID == infoDetailID)
                        {
                            ltdInfoDetail = item.Value;
                            break;
                        }
                    }                    
                }
            }

            return ltdInfoDetail;
        }

        public static long GetMaxLtdInfoDetailId(string whereClause, params object[] whereArgs)
        {
            long maxLtdInfoDetailId = 0;
           
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                maxLtdInfoDetailId = GetDb.Single<long>("SELECT MAX(InfoDetailID) AS MAX_ID FROM LTD_InfoDetail " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLTDInfoDetailList = FileSource.LoadLTDInfoDetailData();
                foreach (KeyValuePair<Guid, LTD_InfoDetail> item in fileLTDInfoDetailList)
                {
                    bool found = true ;
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    
                    if (found ==true)
                    {
                        if (item.Value.InfoDetailID >= maxLtdInfoDetailId)
                        {
                            maxLtdInfoDetailId = item.Value.InfoDetailID;
                        }
                    }
                }
            }
            return maxLtdInfoDetailId;
        }
          
        private static long GetMaxLtdInfoDetailRevisionNo()
        {
            long maxLtdInfoDetailRevisionNo = 0;
            if (HaveDb == true)
            {
                maxLtdInfoDetailRevisionNo = GetDb.Single<long>("SELECT MAX(RevisionNo) AS MAX_ID FROM LTD_InfoDetail");
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLTDInfoDetailList = FileSource.LoadLTDInfoDetailData();
                foreach (KeyValuePair<Guid, LTD_InfoDetail> item in fileLTDInfoDetailList)
                {
                    if (item.Value.RevisionNo >= maxLtdInfoDetailRevisionNo)
                    {
                        maxLtdInfoDetailRevisionNo = item.Value.RevisionNo;
                    }
                }
            }
            return maxLtdInfoDetailRevisionNo;
        }
          

      
        /*    
        private static DateTime GetLtdInfoDetailLastUpdateDate()
        {
            DateTime LtdInfoDetailLastUpdateDate = DateTime.MinValue;
            if (HaveDb == true)
            {
                LtdInfoDetailLastUpdateDate = GetDb.Single<DateTime>("SELECT MAX(LastUpdateDate) AS MAX_DATE FROM LTD_InfoDetail");
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLTDInfoDetailList = FileSource.LoadLTDInfoDetailData();
                foreach (KeyValuePair<Guid, LTD_InfoDetail> item in fileLTDInfoDetailList)
                {
                    //Less than zero(-1) - This instance is earlier than passed value.
                    //Zero (0)	- This instance is the same as value. 
                    //Greater than zero(1) - This instance is later than value. 
                    if (item.Value.LastUpdateDate.CompareTo(LtdInfoDetailLastUpdateDate) > 0)
                    {
                        LtdInfoDetailLastUpdateDate = item.Value.LastUpdateDate;
                    }
                }
            }
            return LtdInfoDetailLastUpdateDate;
        }
        */
          
		
		/*
        public static long GetLtdInfoDetailCount(string whereClause, params object[] whereArgs)
        {
            long ltdInfoDetailCount = 0;
            
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                ltdInfoDetailCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM LTD_InfoDetail " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLTDInfoDetailList = FileSource.LoadLTDInfoDetailData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_InfoDetail> filteredLTDInfoDetailList = new Dictionary<Guid, LTD_InfoDetail>();
                    foreach (KeyValuePair<Guid, LTD_InfoDetail> item in fileLTDInfoDetailList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDInfoDetailList.Add(item.Key, item.Value);
                    }
                    ltdInfoDetailCount = filteredLTDInfoDetailList.Count;
                }
                else
                {
                    ltdInfoDetailCount = fileLTDInfoDetailList.Count;
                }
            }
            return ltdInfoDetailCount;
        }
		*/

        #endregion

        #region Get All

		/*
        public static List<LTD_InfoDetail> GetAllLtdInfoDetail(string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_InfoDetail> allLtdInfoDetailList = new List<LTD_InfoDetail>();
          
            if (HaveDb == true)
            {
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                allLtdInfoDetailList = GetDb.Fetch<LTD_InfoDetail>("SELECT * FROM LTD_InfoDetail " + whereClause + " " + orderByClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLtdInfoDetailList = FileSource.LoadLTDInfoDetailData();
                foreach (KeyValuePair<Guid, LTD_InfoDetail> item in fileLtdInfoDetailList)
                {
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) allLtdInfoDetailList.Add(item.Value);
                    }
                    else
                    {
                        allLtdInfoDetailList.Add(item.Value);
                    }
                }
            }

            return allLtdInfoDetailList;
        }
		*/

        #endregion

        #region Get Paged

        public static List<LTD_InfoDetail> GetPagedLtdInfoDetail(long pageNo, long itemsPerPage, out long totalPages, out long totalItems, string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_InfoDetail> pagedLtdInfoDetailList = new List<LTD_InfoDetail>();
            if (pageNo <= 0) pageNo = 1;
            if (itemsPerPage <= 0) itemsPerPage = 1;

            long retTotalPages = 0;
            long retTotalItems = 0;
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                Page<LTD_InfoDetail> pagedData = GetDb.Page<LTD_InfoDetail>(pageNo, itemsPerPage, "SELECT * FROM LTD_InfoDetail " + whereClause + " " + orderByClause, whereArgs);
                totalPages = pagedData.TotalPages;
                totalItems = pagedData.TotalItems;
                if ((pagedData.Items.Count == 0) && (totalPages == 1))
                {
                    pagedData = GetDb.Page<LTD_InfoDetail>(1, itemsPerPage, "SELECT * FROM LTD_InfoDetail " + whereClause + " " + orderByClause, whereArgs);
                    totalPages = pagedData.TotalPages;
                    totalItems = pagedData.TotalItems;
                }
                pagedLtdInfoDetailList = pagedData.Items;
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLtdInfoDetailList = FileSource.LoadLTDInfoDetailData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_InfoDetail> filteredLTDInfoDetailList = new Dictionary<Guid, LTD_InfoDetail>();
                    foreach (KeyValuePair<Guid, LTD_InfoDetail> item in fileLtdInfoDetailList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDInfoDetailList.Add(item.Key, item.Value);
                    }
                    pagedLtdInfoDetailList = FileSource.GetPagedLTDInfoDetail(filteredLTDInfoDetailList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    pagedLtdInfoDetailList = FileSource.GetPagedLTDInfoDetail(fileLtdInfoDetailList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return pagedLtdInfoDetailList;
        }

        #endregion

        #region Insert

        public static long InsertLtdInfoDetail(LTD_InfoDetail ltdInfoDetail)
        {
            long id = 0;
            if (ltdInfoDetail.InfoDetailGUID != Guid.Empty) throw new Exception("Cannot Set the GUID for a Insert");
            ltdInfoDetail.InfoDetailGUID = Guid.NewGuid();
          
			 UserInfo userInfo = OnGetUserInfo();
            ltdInfoDetail.UserID = userInfo.Id;
            ltdInfoDetail.UserGUID = userInfo.Guid;
            ltdInfoDetail.CreatedDate = DateTime.UtcNow;
            ltdInfoDetail.LastUpdateDate = DateTime.UtcNow;
            ltdInfoDetail.RevisionNo = GetMaxLtdInfoDetailRevisionNo() + 1;

            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Insert(ltdInfoDetail);
                    id = ltdInfoDetail.InfoDetailID;
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLtdInfoDetailList = FileSource.LoadLTDInfoDetailData();
	  			ltdInfoDetail.InfoDetailID = GetMaxLtdInfoDetailId("") + 1;
  
                fileLtdInfoDetailList.Add(ltdInfoDetail.InfoDetailGUID, ltdInfoDetail);
                FileSource.SaveLTDInfoDetailData(fileLtdInfoDetailList);

                id = ltdInfoDetail.InfoDetailID;
            }

            return id;
        }

        #endregion

        #region Update

        public static bool UpdateLtdInfoDetail(LTD_InfoDetail ltdInfoDetail)
        {
            bool ret = false;
          
			 UserInfo userInfo = OnGetUserInfo();
            ltdInfoDetail.UserID = userInfo.Id;
            ltdInfoDetail.UserGUID = userInfo.Guid;
            ltdInfoDetail.LastUpdateDate = DateTime.UtcNow;
            if (HaveDb == true)
            {
                LTD_InfoDetail ltdInfoDetailExisting = GetLtdInfoDetail(ltdInfoDetail.InfoDetailID, "");
                if (ltdInfoDetailExisting != null)
                {
					ltdInfoDetail.RevisionNo = GetMaxLtdInfoDetailRevisionNo() + 1;                      
					using (ITransaction scope = GetDb.GetTransaction())
					{
                        GetDb.Update(ltdInfoDetail);
                        scope.Complete();
                        ret = true;
                    }
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLtdInfoDetailList = FileSource.LoadLTDInfoDetailData();                
                if (fileLtdInfoDetailList.ContainsKey(ltdInfoDetail.InfoDetailGUID) == true)
                {
                    fileLtdInfoDetailList.Remove(ltdInfoDetail.InfoDetailGUID);
                    fileLtdInfoDetailList.Add(ltdInfoDetail.InfoDetailGUID, ltdInfoDetail);
                    FileSource.SaveLTDInfoDetailData(fileLtdInfoDetailList);
                }
            }

            return ret;
        }

        #endregion

        #region Delete

        public static void DeleteLtdInfoDetail(long ltdInfoDetailId)
        {
            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Delete<LTD_InfoDetail>(ltdInfoDetailId);
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoDetail> fileLtdInfoDetailList = FileSource.LoadLTDInfoDetailData();
                Guid ltdInfoDetailGuidToRemove = Guid.Empty;
                foreach (KeyValuePair<Guid, LTD_InfoDetail> item in fileLtdInfoDetailList)
                {
                    if (item.Value.InfoDetailID == ltdInfoDetailId)
                    {
                        ltdInfoDetailGuidToRemove = item.Key;
                        break;
                    }
                }
                if (ltdInfoDetailGuidToRemove != Guid.Empty)
                {
                    fileLtdInfoDetailList.Remove(ltdInfoDetailGuidToRemove);
                    FileSource.SaveLTDInfoDetailData(fileLtdInfoDetailList);
                }
            }

        }

        #endregion

        #endregion
  
        #region LTD_InfoPage Query Related

        #region Get

        public static LTD_InfoPage GetLtdInfoPage(long infoPageID, string orWhereClause, params object[] orWhereArgs)
        {
            LTD_InfoPage ltdInfoPage = null;
            
            if (HaveDb == true)
            {
                if (orWhereClause.Trim().Length == 0)
                {
                    ltdInfoPage = GetDb.SingleOrDefault<LTD_InfoPage>("SELECT * FROM LTD_InfoPage Where InfoPageID=@0", infoPageID);
                }
                else
                {
                    if ((string.IsNullOrEmpty(orWhereClause) ==false) && (orWhereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) orWhereClause = " WHERE " + orWhereClause;
                    ltdInfoPage = GetDb.SingleOrDefault<LTD_InfoPage>("SELECT * FROM LTD_InfoPage " + orWhereClause, orWhereArgs);
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLTDInfoPageList = FileSource.LoadLTDInfoPageData();
                foreach (KeyValuePair<Guid, LTD_InfoPage> item in fileLTDInfoPageList)
                {
                    bool found = true ;
                    if (orWhereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(orWhereClause, orWhereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }
                            else
                            {
                                found = false;
                            }
                        }
                        if (found ==true)
                        {
                            ltdInfoPage = item.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Value.InfoPageID == infoPageID)
                        {
                            ltdInfoPage = item.Value;
                            break;
                        }
                    }                    
                }
            }

            return ltdInfoPage;
        }

        public static long GetMaxLtdInfoPageId(string whereClause, params object[] whereArgs)
        {
            long maxLtdInfoPageId = 0;
           
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                maxLtdInfoPageId = GetDb.Single<long>("SELECT MAX(InfoPageID) AS MAX_ID FROM LTD_InfoPage " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLTDInfoPageList = FileSource.LoadLTDInfoPageData();
                foreach (KeyValuePair<Guid, LTD_InfoPage> item in fileLTDInfoPageList)
                {
                    bool found = true ;
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    
                    if (found ==true)
                    {
                        if (item.Value.InfoPageID >= maxLtdInfoPageId)
                        {
                            maxLtdInfoPageId = item.Value.InfoPageID;
                        }
                    }
                }
            }
            return maxLtdInfoPageId;
        }
          
        private static long GetMaxLtdInfoPageRevisionNo()
        {
            long maxLtdInfoPageRevisionNo = 0;
            if (HaveDb == true)
            {
                maxLtdInfoPageRevisionNo = GetDb.Single<long>("SELECT MAX(RevisionNo) AS MAX_ID FROM LTD_InfoPage");
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLTDInfoPageList = FileSource.LoadLTDInfoPageData();
                foreach (KeyValuePair<Guid, LTD_InfoPage> item in fileLTDInfoPageList)
                {
                    if (item.Value.RevisionNo >= maxLtdInfoPageRevisionNo)
                    {
                        maxLtdInfoPageRevisionNo = item.Value.RevisionNo;
                    }
                }
            }
            return maxLtdInfoPageRevisionNo;
        }
          

      
        /*    
        private static DateTime GetLtdInfoPageLastUpdateDate()
        {
            DateTime LtdInfoPageLastUpdateDate = DateTime.MinValue;
            if (HaveDb == true)
            {
                LtdInfoPageLastUpdateDate = GetDb.Single<DateTime>("SELECT MAX(LastUpdateDate) AS MAX_DATE FROM LTD_InfoPage");
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLTDInfoPageList = FileSource.LoadLTDInfoPageData();
                foreach (KeyValuePair<Guid, LTD_InfoPage> item in fileLTDInfoPageList)
                {
                    //Less than zero(-1) - This instance is earlier than passed value.
                    //Zero (0)	- This instance is the same as value. 
                    //Greater than zero(1) - This instance is later than value. 
                    if (item.Value.LastUpdateDate.CompareTo(LtdInfoPageLastUpdateDate) > 0)
                    {
                        LtdInfoPageLastUpdateDate = item.Value.LastUpdateDate;
                    }
                }
            }
            return LtdInfoPageLastUpdateDate;
        }
        */
          
		
		/*
        public static long GetLtdInfoPageCount(string whereClause, params object[] whereArgs)
        {
            long ltdInfoPageCount = 0;
            
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                ltdInfoPageCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM LTD_InfoPage " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLTDInfoPageList = FileSource.LoadLTDInfoPageData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_InfoPage> filteredLTDInfoPageList = new Dictionary<Guid, LTD_InfoPage>();
                    foreach (KeyValuePair<Guid, LTD_InfoPage> item in fileLTDInfoPageList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDInfoPageList.Add(item.Key, item.Value);
                    }
                    ltdInfoPageCount = filteredLTDInfoPageList.Count;
                }
                else
                {
                    ltdInfoPageCount = fileLTDInfoPageList.Count;
                }
            }
            return ltdInfoPageCount;
        }
		*/

        #endregion

        #region Get All

		/*
        public static List<LTD_InfoPage> GetAllLtdInfoPage(string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_InfoPage> allLtdInfoPageList = new List<LTD_InfoPage>();
          
            if (HaveDb == true)
            {
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                allLtdInfoPageList = GetDb.Fetch<LTD_InfoPage>("SELECT * FROM LTD_InfoPage " + whereClause + " " + orderByClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLtdInfoPageList = FileSource.LoadLTDInfoPageData();
                foreach (KeyValuePair<Guid, LTD_InfoPage> item in fileLtdInfoPageList)
                {
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) allLtdInfoPageList.Add(item.Value);
                    }
                    else
                    {
                        allLtdInfoPageList.Add(item.Value);
                    }
                }
            }

            return allLtdInfoPageList;
        }
		*/

        #endregion

        #region Get Paged

        public static List<LTD_InfoPage> GetPagedLtdInfoPage(long pageNo, long itemsPerPage, out long totalPages, out long totalItems, string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_InfoPage> pagedLtdInfoPageList = new List<LTD_InfoPage>();
            if (pageNo <= 0) pageNo = 1;
            if (itemsPerPage <= 0) itemsPerPage = 1;

            long retTotalPages = 0;
            long retTotalItems = 0;
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                Page<LTD_InfoPage> pagedData = GetDb.Page<LTD_InfoPage>(pageNo, itemsPerPage, "SELECT * FROM LTD_InfoPage " + whereClause + " " + orderByClause, whereArgs);
                totalPages = pagedData.TotalPages;
                totalItems = pagedData.TotalItems;
                if ((pagedData.Items.Count == 0) && (totalPages == 1))
                {
                    pagedData = GetDb.Page<LTD_InfoPage>(1, itemsPerPage, "SELECT * FROM LTD_InfoPage " + whereClause + " " + orderByClause, whereArgs);
                    totalPages = pagedData.TotalPages;
                    totalItems = pagedData.TotalItems;
                }
                pagedLtdInfoPageList = pagedData.Items;
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLtdInfoPageList = FileSource.LoadLTDInfoPageData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_InfoPage> filteredLTDInfoPageList = new Dictionary<Guid, LTD_InfoPage>();
                    foreach (KeyValuePair<Guid, LTD_InfoPage> item in fileLtdInfoPageList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDInfoPageList.Add(item.Key, item.Value);
                    }
                    pagedLtdInfoPageList = FileSource.GetPagedLTDInfoPage(filteredLTDInfoPageList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    pagedLtdInfoPageList = FileSource.GetPagedLTDInfoPage(fileLtdInfoPageList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return pagedLtdInfoPageList;
        }

        #endregion

        #region Insert

        public static long InsertLtdInfoPage(LTD_InfoPage ltdInfoPage)
        {
            long id = 0;
            if (ltdInfoPage.InfoPageGUID != Guid.Empty) throw new Exception("Cannot Set the GUID for a Insert");
            ltdInfoPage.InfoPageGUID = Guid.NewGuid();
          
			 UserInfo userInfo = OnGetUserInfo();
            ltdInfoPage.UserID = userInfo.Id;
            ltdInfoPage.UserGUID = userInfo.Guid;
            ltdInfoPage.CreatedDate = DateTime.UtcNow;
            ltdInfoPage.LastUpdateDate = DateTime.UtcNow;
            ltdInfoPage.RevisionNo = GetMaxLtdInfoPageRevisionNo() + 1;

            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Insert(ltdInfoPage);
                    id = ltdInfoPage.InfoPageID;
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLtdInfoPageList = FileSource.LoadLTDInfoPageData();
	  			ltdInfoPage.InfoPageID = GetMaxLtdInfoPageId("") + 1;
  
                fileLtdInfoPageList.Add(ltdInfoPage.InfoPageGUID, ltdInfoPage);
                FileSource.SaveLTDInfoPageData(fileLtdInfoPageList);

                id = ltdInfoPage.InfoPageID;
            }

            return id;
        }

        #endregion

        #region Update

        public static bool UpdateLtdInfoPage(LTD_InfoPage ltdInfoPage)
        {
            bool ret = false;
          
			 UserInfo userInfo = OnGetUserInfo();
            ltdInfoPage.UserID = userInfo.Id;
            ltdInfoPage.UserGUID = userInfo.Guid;
            ltdInfoPage.LastUpdateDate = DateTime.UtcNow;
            if (HaveDb == true)
            {
                LTD_InfoPage ltdInfoPageExisting = GetLtdInfoPage(ltdInfoPage.InfoPageID, "");
                if (ltdInfoPageExisting != null)
                {
					ltdInfoPage.RevisionNo = GetMaxLtdInfoPageRevisionNo() + 1;                      
					using (ITransaction scope = GetDb.GetTransaction())
					{
                        GetDb.Update(ltdInfoPage);
                        scope.Complete();
                        ret = true;
                    }
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLtdInfoPageList = FileSource.LoadLTDInfoPageData();                
                if (fileLtdInfoPageList.ContainsKey(ltdInfoPage.InfoPageGUID) == true)
                {
                    fileLtdInfoPageList.Remove(ltdInfoPage.InfoPageGUID);
                    fileLtdInfoPageList.Add(ltdInfoPage.InfoPageGUID, ltdInfoPage);
                    FileSource.SaveLTDInfoPageData(fileLtdInfoPageList);
                }
            }

            return ret;
        }

        #endregion

        #region Delete

        public static void DeleteLtdInfoPage(long ltdInfoPageId)
        {
            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Delete<LTD_InfoPage>(ltdInfoPageId);
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoPage> fileLtdInfoPageList = FileSource.LoadLTDInfoPageData();
                Guid ltdInfoPageGuidToRemove = Guid.Empty;
                foreach (KeyValuePair<Guid, LTD_InfoPage> item in fileLtdInfoPageList)
                {
                    if (item.Value.InfoPageID == ltdInfoPageId)
                    {
                        ltdInfoPageGuidToRemove = item.Key;
                        break;
                    }
                }
                if (ltdInfoPageGuidToRemove != Guid.Empty)
                {
                    fileLtdInfoPageList.Remove(ltdInfoPageGuidToRemove);
                    FileSource.SaveLTDInfoPageData(fileLtdInfoPageList);
                }
            }

        }

        #endregion

        #endregion
  
        #region LTD_Subscriber Query Related

        #region Get

        public static LTD_Subscriber GetLtdSubscriber(long subscriberID, string orWhereClause, params object[] orWhereArgs)
        {
            LTD_Subscriber ltdSubscriber = null;
            
            if (HaveDb == true)
            {
                if (orWhereClause.Trim().Length == 0)
                {
                    ltdSubscriber = GetDb.SingleOrDefault<LTD_Subscriber>("SELECT * FROM LTD_Subscriber Where SubscriberID=@0", subscriberID);
                }
                else
                {
                    if ((string.IsNullOrEmpty(orWhereClause) ==false) && (orWhereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) orWhereClause = " WHERE " + orWhereClause;
                    ltdSubscriber = GetDb.SingleOrDefault<LTD_Subscriber>("SELECT * FROM LTD_Subscriber " + orWhereClause, orWhereArgs);
                }
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLTDSubscriberList = FileSource.LoadLTDSubscriberData();
                foreach (KeyValuePair<Guid, LTD_Subscriber> item in fileLTDSubscriberList)
                {
                    bool found = true ;
                    if (orWhereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(orWhereClause, orWhereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }
                            else
                            {
                                found = false;
                            }
                        }
                        if (found ==true)
                        {
                            ltdSubscriber = item.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Value.SubscriberID == subscriberID)
                        {
                            ltdSubscriber = item.Value;
                            break;
                        }
                    }                    
                }
            }

            return ltdSubscriber;
        }

        public static long GetMaxLtdSubscriberId(string whereClause, params object[] whereArgs)
        {
            long maxLtdSubscriberId = 0;
           
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                maxLtdSubscriberId = GetDb.Single<long>("SELECT MAX(SubscriberID) AS MAX_ID FROM LTD_Subscriber " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLTDSubscriberList = FileSource.LoadLTDSubscriberData();
                foreach (KeyValuePair<Guid, LTD_Subscriber> item in fileLTDSubscriberList)
                {
                    bool found = true ;
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    
                    if (found ==true)
                    {
                        if (item.Value.SubscriberID >= maxLtdSubscriberId)
                        {
                            maxLtdSubscriberId = item.Value.SubscriberID;
                        }
                    }
                }
            }
            return maxLtdSubscriberId;
        }
          
        private static long GetMaxLtdSubscriberRevisionNo()
        {
            long maxLtdSubscriberRevisionNo = 0;
            if (HaveDb == true)
            {
                maxLtdSubscriberRevisionNo = GetDb.Single<long>("SELECT MAX(RevisionNo) AS MAX_ID FROM LTD_Subscriber");
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLTDSubscriberList = FileSource.LoadLTDSubscriberData();
                foreach (KeyValuePair<Guid, LTD_Subscriber> item in fileLTDSubscriberList)
                {
                    if (item.Value.RevisionNo >= maxLtdSubscriberRevisionNo)
                    {
                        maxLtdSubscriberRevisionNo = item.Value.RevisionNo;
                    }
                }
            }
            return maxLtdSubscriberRevisionNo;
        }
          

      
        /*    
        private static DateTime GetLtdSubscriberLastUpdateDate()
        {
            DateTime LtdSubscriberLastUpdateDate = DateTime.MinValue;
            if (HaveDb == true)
            {
                LtdSubscriberLastUpdateDate = GetDb.Single<DateTime>("SELECT MAX(LastUpdateDate) AS MAX_DATE FROM LTD_Subscriber");
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLTDSubscriberList = FileSource.LoadLTDSubscriberData();
                foreach (KeyValuePair<Guid, LTD_Subscriber> item in fileLTDSubscriberList)
                {
                    //Less than zero(-1) - This instance is earlier than passed value.
                    //Zero (0)	- This instance is the same as value. 
                    //Greater than zero(1) - This instance is later than value. 
                    if (item.Value.LastUpdateDate.CompareTo(LtdSubscriberLastUpdateDate) > 0)
                    {
                        LtdSubscriberLastUpdateDate = item.Value.LastUpdateDate;
                    }
                }
            }
            return LtdSubscriberLastUpdateDate;
        }
        */
          
		
		/*
        public static long GetLtdSubscriberCount(string whereClause, params object[] whereArgs)
        {
            long ltdSubscriberCount = 0;
            
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                ltdSubscriberCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM LTD_Subscriber " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLTDSubscriberList = FileSource.LoadLTDSubscriberData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_Subscriber> filteredLTDSubscriberList = new Dictionary<Guid, LTD_Subscriber>();
                    foreach (KeyValuePair<Guid, LTD_Subscriber> item in fileLTDSubscriberList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDSubscriberList.Add(item.Key, item.Value);
                    }
                    ltdSubscriberCount = filteredLTDSubscriberList.Count;
                }
                else
                {
                    ltdSubscriberCount = fileLTDSubscriberList.Count;
                }
            }
            return ltdSubscriberCount;
        }
		*/

        #endregion

        #region Get All

		/*
        public static List<LTD_Subscriber> GetAllLtdSubscriber(string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_Subscriber> allLtdSubscriberList = new List<LTD_Subscriber>();
          
            if (HaveDb == true)
            {
                                
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                allLtdSubscriberList = GetDb.Fetch<LTD_Subscriber>("SELECT * FROM LTD_Subscriber " + whereClause + " " + orderByClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLtdSubscriberList = FileSource.LoadLTDSubscriberData();
                foreach (KeyValuePair<Guid, LTD_Subscriber> item in fileLtdSubscriberList)
                {
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) allLtdSubscriberList.Add(item.Value);
                    }
                    else
                    {
                        allLtdSubscriberList.Add(item.Value);
                    }
                }
            }

            return allLtdSubscriberList;
        }
		*/

        #endregion

        #region Get Paged

        public static List<LTD_Subscriber> GetPagedLtdSubscriber(long pageNo, long itemsPerPage, out long totalPages, out long totalItems, string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_Subscriber> pagedLtdSubscriberList = new List<LTD_Subscriber>();
            if (pageNo <= 0) pageNo = 1;
            if (itemsPerPage <= 0) itemsPerPage = 1;

            long retTotalPages = 0;
            long retTotalItems = 0;
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                                
                Page<LTD_Subscriber> pagedData = GetDb.Page<LTD_Subscriber>(pageNo, itemsPerPage, "SELECT * FROM LTD_Subscriber " + whereClause + " " + orderByClause, whereArgs);
                totalPages = pagedData.TotalPages;
                totalItems = pagedData.TotalItems;
                if ((pagedData.Items.Count == 0) && (totalPages == 1))
                {
                    pagedData = GetDb.Page<LTD_Subscriber>(1, itemsPerPage, "SELECT * FROM LTD_Subscriber " + whereClause + " " + orderByClause, whereArgs);
                    totalPages = pagedData.TotalPages;
                    totalItems = pagedData.TotalItems;
                }
                pagedLtdSubscriberList = pagedData.Items;
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLtdSubscriberList = FileSource.LoadLTDSubscriberData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_Subscriber> filteredLTDSubscriberList = new Dictionary<Guid, LTD_Subscriber>();
                    foreach (KeyValuePair<Guid, LTD_Subscriber> item in fileLtdSubscriberList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDSubscriberList.Add(item.Key, item.Value);
                    }
                    pagedLtdSubscriberList = FileSource.GetPagedLTDSubscriber(filteredLTDSubscriberList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    pagedLtdSubscriberList = FileSource.GetPagedLTDSubscriber(fileLtdSubscriberList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return pagedLtdSubscriberList;
        }

        #endregion

        #region Insert

        public static long InsertLtdSubscriber(LTD_Subscriber ltdSubscriber)
        {
            long id = 0;
            if (ltdSubscriber.SubscriberGUID != Guid.Empty) throw new Exception("Cannot Set the GUID for a Insert");
            ltdSubscriber.SubscriberGUID = Guid.NewGuid();
            ltdSubscriber.CreatedDate = DateTime.UtcNow;
            ltdSubscriber.LastUpdateDate = DateTime.UtcNow;
            ltdSubscriber.RevisionNo = GetMaxLtdSubscriberRevisionNo() + 1;

            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Insert(ltdSubscriber);
                    id = ltdSubscriber.SubscriberID;
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLtdSubscriberList = FileSource.LoadLTDSubscriberData();
	  			ltdSubscriber.SubscriberID = GetMaxLtdSubscriberId("") + 1;
  
                fileLtdSubscriberList.Add(ltdSubscriber.SubscriberGUID, ltdSubscriber);
                FileSource.SaveLTDSubscriberData(fileLtdSubscriberList);

                id = ltdSubscriber.SubscriberID;
            }

            return id;
        }

        #endregion

        #region Update

        public static bool UpdateLtdSubscriber(LTD_Subscriber ltdSubscriber)
        {
            bool ret = false;
            ltdSubscriber.LastUpdateDate = DateTime.UtcNow;
            if (HaveDb == true)
            {
                LTD_Subscriber ltdSubscriberExisting = GetLtdSubscriber(ltdSubscriber.SubscriberID, "");
                if (ltdSubscriberExisting != null)
                {
					ltdSubscriber.RevisionNo = GetMaxLtdSubscriberRevisionNo() + 1;                      
					using (ITransaction scope = GetDb.GetTransaction())
					{
                        GetDb.Update(ltdSubscriber);
                        scope.Complete();
                        ret = true;
                    }
                }
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLtdSubscriberList = FileSource.LoadLTDSubscriberData();                
                if (fileLtdSubscriberList.ContainsKey(ltdSubscriber.SubscriberGUID) == true)
                {
                    fileLtdSubscriberList.Remove(ltdSubscriber.SubscriberGUID);
                    fileLtdSubscriberList.Add(ltdSubscriber.SubscriberGUID, ltdSubscriber);
                    FileSource.SaveLTDSubscriberData(fileLtdSubscriberList);
                }
            }

            return ret;
        }

        #endregion

        #region Delete

        public static void DeleteLtdSubscriber(long ltdSubscriberId)
        {
            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Delete<LTD_Subscriber>(ltdSubscriberId);
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_Subscriber> fileLtdSubscriberList = FileSource.LoadLTDSubscriberData();
                Guid ltdSubscriberGuidToRemove = Guid.Empty;
                foreach (KeyValuePair<Guid, LTD_Subscriber> item in fileLtdSubscriberList)
                {
                    if (item.Value.SubscriberID == ltdSubscriberId)
                    {
                        ltdSubscriberGuidToRemove = item.Key;
                        break;
                    }
                }
                if (ltdSubscriberGuidToRemove != Guid.Empty)
                {
                    fileLtdSubscriberList.Remove(ltdSubscriberGuidToRemove);
                    FileSource.SaveLTDSubscriberData(fileLtdSubscriberList);
                }
            }

        }

        #endregion

        #endregion
  
        #region LTD_InfoCategory Query Related

        #region Get

        public static LTD_InfoCategory GetLtdInfoCategory(long infoCategoryID, string orWhereClause, params object[] orWhereArgs)
        {
            LTD_InfoCategory ltdInfoCategory = null;
            
            if (HaveDb == true)
            {
                if (orWhereClause.Trim().Length == 0)
                {
                    ltdInfoCategory = GetDb.SingleOrDefault<LTD_InfoCategory>("SELECT * FROM LTD_InfoCategory Where InfoCategoryID=@0", infoCategoryID);
                }
                else
                {
                    if ((string.IsNullOrEmpty(orWhereClause) ==false) && (orWhereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) orWhereClause = " WHERE " + orWhereClause;
                    ltdInfoCategory = GetDb.SingleOrDefault<LTD_InfoCategory>("SELECT * FROM LTD_InfoCategory " + orWhereClause, orWhereArgs);
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLTDInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                foreach (KeyValuePair<Guid, LTD_InfoCategory> item in fileLTDInfoCategoryList)
                {
                    bool found = true ;
                    if (orWhereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(orWhereClause, orWhereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }
                            else
                            {
                                found = false;
                            }
                        }
                        if (found ==true)
                        {
                            ltdInfoCategory = item.Value;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Value.InfoCategoryID == infoCategoryID)
                        {
                            ltdInfoCategory = item.Value;
                            break;
                        }
                    }                    
                }
            }

            return ltdInfoCategory;
        }

        public static long GetMaxLtdInfoCategoryId(string whereClause, params object[] whereArgs)
        {
            long maxLtdInfoCategoryId = 0;
           
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                maxLtdInfoCategoryId = GetDb.Single<long>("SELECT MAX(InfoCategoryID) AS MAX_ID FROM LTD_InfoCategory " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLTDInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                foreach (KeyValuePair<Guid, LTD_InfoCategory> item in fileLTDInfoCategoryList)
                {
                    bool found = true ;
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    found = false;
                                    break;
                                }
                            }else
                            {
                                found = false;
                            }
                        }
                    }
                    
                    if (found ==true)
                    {
                        if (item.Value.InfoCategoryID >= maxLtdInfoCategoryId)
                        {
                            maxLtdInfoCategoryId = item.Value.InfoCategoryID;
                        }
                    }
                }
            }
            return maxLtdInfoCategoryId;
        }
          
        private static long GetMaxLtdInfoCategoryRevisionNo()
        {
            long maxLtdInfoCategoryRevisionNo = 0;
            if (HaveDb == true)
            {
                maxLtdInfoCategoryRevisionNo = GetDb.Single<long>("SELECT MAX(RevisionNo) AS MAX_ID FROM LTD_InfoCategory");
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLTDInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                foreach (KeyValuePair<Guid, LTD_InfoCategory> item in fileLTDInfoCategoryList)
                {
                    if (item.Value.RevisionNo >= maxLtdInfoCategoryRevisionNo)
                    {
                        maxLtdInfoCategoryRevisionNo = item.Value.RevisionNo;
                    }
                }
            }
            return maxLtdInfoCategoryRevisionNo;
        }
          

      
        /*    
        private static DateTime GetLtdInfoCategoryLastUpdateDate()
        {
            DateTime LtdInfoCategoryLastUpdateDate = DateTime.MinValue;
            if (HaveDb == true)
            {
                LtdInfoCategoryLastUpdateDate = GetDb.Single<DateTime>("SELECT MAX(LastUpdateDate) AS MAX_DATE FROM LTD_InfoCategory");
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLTDInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                foreach (KeyValuePair<Guid, LTD_InfoCategory> item in fileLTDInfoCategoryList)
                {
                    //Less than zero(-1) - This instance is earlier than passed value.
                    //Zero (0)	- This instance is the same as value. 
                    //Greater than zero(1) - This instance is later than value. 
                    if (item.Value.LastUpdateDate.CompareTo(LtdInfoCategoryLastUpdateDate) > 0)
                    {
                        LtdInfoCategoryLastUpdateDate = item.Value.LastUpdateDate;
                    }
                }
            }
            return LtdInfoCategoryLastUpdateDate;
        }
        */
          
		
		/*
        public static long GetLtdInfoCategoryCount(string whereClause, params object[] whereArgs)
        {
            long ltdInfoCategoryCount = 0;
            
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                ltdInfoCategoryCount = GetDb.Single<long>("SELECT COUNT(*) AS TOTAL_COUNT FROM LTD_InfoCategory " + whereClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLTDInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_InfoCategory> filteredLTDInfoCategoryList = new Dictionary<Guid, LTD_InfoCategory>();
                    foreach (KeyValuePair<Guid, LTD_InfoCategory> item in fileLTDInfoCategoryList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDInfoCategoryList.Add(item.Key, item.Value);
                    }
                    ltdInfoCategoryCount = filteredLTDInfoCategoryList.Count;
                }
                else
                {
                    ltdInfoCategoryCount = fileLTDInfoCategoryList.Count;
                }
            }
            return ltdInfoCategoryCount;
        }
		*/

        #endregion

        #region Get All

		/*
        public static List<LTD_InfoCategory> GetAllLtdInfoCategory(string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_InfoCategory> allLtdInfoCategoryList = new List<LTD_InfoCategory>();
          
            if (HaveDb == true)
            {
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                allLtdInfoCategoryList = GetDb.Fetch<LTD_InfoCategory>("SELECT * FROM LTD_InfoCategory " + whereClause + " " + orderByClause, whereArgs);
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLtdInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                foreach (KeyValuePair<Guid, LTD_InfoCategory> item in fileLtdInfoCategoryList)
                {
                    if (whereClause.Trim().Length > 0)
                    {
                        Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) allLtdInfoCategoryList.Add(item.Value);
                    }
                    else
                    {
                        allLtdInfoCategoryList.Add(item.Value);
                    }
                }
            }

            return allLtdInfoCategoryList;
        }
		*/

        #endregion

        #region Get Paged

        public static List<LTD_InfoCategory> GetPagedLtdInfoCategory(long pageNo, long itemsPerPage, out long totalPages, out long totalItems, string orderByClause, string whereClause, params object[] whereArgs)
        {
            List<LTD_InfoCategory> pagedLtdInfoCategoryList = new List<LTD_InfoCategory>();
            if (pageNo <= 0) pageNo = 1;
            if (itemsPerPage <= 0) itemsPerPage = 1;

            long retTotalPages = 0;
            long retTotalItems = 0;
            if (HaveDb == true)
            {
                if ((string.IsNullOrEmpty(whereClause) ==false) && (whereClause.Trim().ToUpper().StartsWith("Where".ToUpper()) == false)) whereClause = " WHERE " + whereClause;
                if (String.IsNullOrEmpty(orderByClause) ==true) {orderByClause = " Order By Sequence Desc";}                 
                Page<LTD_InfoCategory> pagedData = GetDb.Page<LTD_InfoCategory>(pageNo, itemsPerPage, "SELECT * FROM LTD_InfoCategory " + whereClause + " " + orderByClause, whereArgs);
                totalPages = pagedData.TotalPages;
                totalItems = pagedData.TotalItems;
                if ((pagedData.Items.Count == 0) && (totalPages == 1))
                {
                    pagedData = GetDb.Page<LTD_InfoCategory>(1, itemsPerPage, "SELECT * FROM LTD_InfoCategory " + whereClause + " " + orderByClause, whereArgs);
                    totalPages = pagedData.TotalPages;
                    totalItems = pagedData.TotalItems;
                }
                pagedLtdInfoCategoryList = pagedData.Items;
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLtdInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                if (whereClause.Trim().Length > 0)
                {
                    Dictionary<string, string> whereList = GetWhereList(whereClause, whereArgs);
                    Dictionary<Guid, LTD_InfoCategory> filteredLTDInfoCategoryList = new Dictionary<Guid, LTD_InfoCategory>();
                    foreach (KeyValuePair<Guid, LTD_InfoCategory> item in fileLtdInfoCategoryList)
                    {
                        bool add =true ;
                        foreach (KeyValuePair<string, string> whereCol in whereList)
                        {
                            bool match = false;
                            if (item.Value.HaveColumn(whereCol.Key, whereCol.Value, out match) == true)
                            {
                                if (match == false)
                                {
                                    add = false;
                                    break;
                                }
                            }else
                            {
                                add = false;
                            }
                        }
                        if (add == true) filteredLTDInfoCategoryList.Add(item.Key, item.Value);
                    }
                    pagedLtdInfoCategoryList = FileSource.GetPagedLTDInfoCategory(filteredLTDInfoCategoryList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
                else
                {
                    pagedLtdInfoCategoryList = FileSource.GetPagedLTDInfoCategory(fileLtdInfoCategoryList, pageNo, itemsPerPage, out totalPages, out totalItems);
                }
            }

            retTotalPages = totalPages;
            retTotalItems = totalItems;
            return pagedLtdInfoCategoryList;
        }

        #endregion

        #region Insert

        public static long InsertLtdInfoCategory(LTD_InfoCategory ltdInfoCategory)
        {
            long id = 0;
            if (ltdInfoCategory.InfoCategoryGUID != Guid.Empty) throw new Exception("Cannot Set the GUID for a Insert");
            ltdInfoCategory.InfoCategoryGUID = Guid.NewGuid();
          
			 UserInfo userInfo = OnGetUserInfo();
            ltdInfoCategory.UserID = userInfo.Id;
            ltdInfoCategory.UserGUID = userInfo.Guid;
            ltdInfoCategory.CreatedDate = DateTime.UtcNow;
            ltdInfoCategory.LastUpdateDate = DateTime.UtcNow;
            ltdInfoCategory.RevisionNo = GetMaxLtdInfoCategoryRevisionNo() + 1;

            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Insert(ltdInfoCategory);
                    id = ltdInfoCategory.InfoCategoryID;
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLtdInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
	  			ltdInfoCategory.InfoCategoryID = GetMaxLtdInfoCategoryId("") + 1;
  
                fileLtdInfoCategoryList.Add(ltdInfoCategory.InfoCategoryGUID, ltdInfoCategory);
                FileSource.SaveLTDInfoCategoryData(fileLtdInfoCategoryList);

                id = ltdInfoCategory.InfoCategoryID;
            }

            return id;
        }

        #endregion

        #region Update

        public static bool UpdateLtdInfoCategory(LTD_InfoCategory ltdInfoCategory)
        {
            bool ret = false;
          
			 UserInfo userInfo = OnGetUserInfo();
            ltdInfoCategory.UserID = userInfo.Id;
            ltdInfoCategory.UserGUID = userInfo.Guid;
            ltdInfoCategory.LastUpdateDate = DateTime.UtcNow;
            if (HaveDb == true)
            {
                LTD_InfoCategory ltdInfoCategoryExisting = GetLtdInfoCategory(ltdInfoCategory.InfoCategoryID, "");
                if (ltdInfoCategoryExisting != null)
                {
					ltdInfoCategory.RevisionNo = GetMaxLtdInfoCategoryRevisionNo() + 1;                      
					using (ITransaction scope = GetDb.GetTransaction())
					{
                        GetDb.Update(ltdInfoCategory);
                        scope.Complete();
                        ret = true;
                    }
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLtdInfoCategoryList = FileSource.LoadLTDInfoCategoryData();                
                if (fileLtdInfoCategoryList.ContainsKey(ltdInfoCategory.InfoCategoryGUID) == true)
                {
                    fileLtdInfoCategoryList.Remove(ltdInfoCategory.InfoCategoryGUID);
                    fileLtdInfoCategoryList.Add(ltdInfoCategory.InfoCategoryGUID, ltdInfoCategory);
                    FileSource.SaveLTDInfoCategoryData(fileLtdInfoCategoryList);
                }
            }

            return ret;
        }

        #endregion

        #region Delete

        public static void DeleteLtdInfoCategory(long ltdInfoCategoryId)
        {
            if (HaveDb == true)
            {
                using (ITransaction scope = GetDb.GetTransaction())
                {
                    GetDb.Delete<LTD_InfoCategory>(ltdInfoCategoryId);
                    scope.Complete();
                }
            }
            else
            {
                Dictionary<Guid, LTD_InfoCategory> fileLtdInfoCategoryList = FileSource.LoadLTDInfoCategoryData();
                Guid ltdInfoCategoryGuidToRemove = Guid.Empty;
                foreach (KeyValuePair<Guid, LTD_InfoCategory> item in fileLtdInfoCategoryList)
                {
                    if (item.Value.InfoCategoryID == ltdInfoCategoryId)
                    {
                        ltdInfoCategoryGuidToRemove = item.Key;
                        break;
                    }
                }
                if (ltdInfoCategoryGuidToRemove != Guid.Empty)
                {
                    fileLtdInfoCategoryList.Remove(ltdInfoCategoryGuidToRemove);
                    FileSource.SaveLTDInfoCategoryData(fileLtdInfoCategoryList);
                }
            }

        }

        #endregion

        #endregion
  
    }
}
