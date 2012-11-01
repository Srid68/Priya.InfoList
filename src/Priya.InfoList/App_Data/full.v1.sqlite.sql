
DROP TABLE IF EXISTS [SYS_Version];
CREATE TABLE [SYS_Version] (
	"VersionNo"		float NOT NULL,
	"VersionNoGUID"		guid NOT NULL,
    PRIMARY KEY ([VersionNo])

);
INSERT INTO SYS_Version (VersionNo, VersionNoGUID) VALUES (1, 'aa7b3943-e055-48ab-8857-392be40824fb');


DROP TABLE IF EXISTS [CND_Data];
CREATE TABLE [CND_Data] (
	"DataID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"DataGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"ParentDataID"		integer,
	"ParentDataGUID"		guid,
	"DataTypeID"		integer NOT NULL,
	"DataTypeGUID"		guid NOT NULL,
	"DataValue"		varchar(500) NOT NULL COLLATE NOCASE,
	"DataRefTypeID"		integer NOT NULL,
	"DataRefTypeGUID"		guid NOT NULL,
	"DataRefID"		integer,
	"IsActive"		bit NOT NULL,
	"IsDeleted"		bit NOT NULL,
	"Sequence"		integer NOT NULL
,
    FOREIGN KEY ([ParentDataID] , [ParentDataGUID])
        REFERENCES [CND_Data]([DataID] , [DataGUID]),
    FOREIGN KEY ([DataRefTypeID] , [DataRefTypeGUID])
        REFERENCES [CNS_DataRefType]([DataRefTypeID] , [DataRefTypeGUID]),
    FOREIGN KEY ([DataTypeID] , [DataTypeGUID])
        REFERENCES [CNS_DataType]([DataTypeID] , [DataTypeGUID])
);

CREATE UNIQUE INDEX [CND_Data_GK_CND_Data]
ON [CND_Data]
([DataGUID]);

CREATE UNIQUE INDEX [CND_Data_GK_CND_Data_1]
ON [CND_Data]
([DataID], [DataGUID]);


DROP TABLE IF EXISTS [CNS_DataRefType];
CREATE TABLE [CNS_DataRefType] (
	"DataRefTypeID"		integer NOT NULL,
	"DataRefTypeGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"DataRefType"		varchar(30) NOT NULL COLLATE NOCASE,
	"IsDefault"		bit NOT NULL,
	"IsSystem"		bit NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL,
    PRIMARY KEY ([DataRefTypeID])

);

CREATE UNIQUE INDEX [CNS_DataRefType_GK_CNS_DataRefType]
ON [CNS_DataRefType]
([DataRefTypeGUID]);

CREATE UNIQUE INDEX [CNS_DataRefType_GK_CNS_DataRefType_1]
ON [CNS_DataRefType]
([DataRefTypeID], [DataRefTypeGUID]);


DROP TABLE IF EXISTS [CNS_DataType];
CREATE TABLE [CNS_DataType] (
	"DataTypeID"		integer NOT NULL,
	"DataTypeGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"DataType"		varchar(30) NOT NULL COLLATE NOCASE,
	"IsDefault"		bit NOT NULL,
	"IsSystem"		bit NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL,
    PRIMARY KEY ([DataTypeID])

);

CREATE UNIQUE INDEX [CNS_DataType_GK_CNS_DataType]
ON [CNS_DataType]
([DataTypeGUID]);

CREATE UNIQUE INDEX [CNS_DataType_GK_CNS_DataType_1]
ON [CNS_DataType]
([DataTypeID], [DataTypeGUID]);


DROP TABLE IF EXISTS [LTD_InfoCategory];
CREATE TABLE [LTD_InfoCategory] (
	"InfoCategoryID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"InfoCategoryGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL,
	"InfoCategoryName"		varchar(30) NOT NULL COLLATE NOCASE,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL,
	"IsDefault"		bit NOT NULL,
	"IsSystem"		bit NOT NULL DEFAULT 0

);

CREATE UNIQUE INDEX [LTD_InfoCategory_GK_LTD_InfoCategory]
ON [LTD_InfoCategory]
([InfoCategoryGUID]);

CREATE UNIQUE INDEX [LTD_InfoCategory_GK_LTD_InfoCategory_1]
ON [LTD_InfoCategory]
([InfoCategoryID], [InfoCategoryGUID]);

CREATE UNIQUE INDEX [LTD_InfoCategory_UK_LTD_InfoCategory]
ON [LTD_InfoCategory]
([InfoCategoryName], [UserID]);


DROP TABLE IF EXISTS [LTD_InfoDetail];
CREATE TABLE [LTD_InfoDetail] (
	"InfoDetailID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"InfoDetailGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL,
	"InfoPageID"		integer NOT NULL,
	"InfoPageGUID"		guid NOT NULL,
	"InfoSectionID"		integer NOT NULL,
	"InfoSectionGUID"		guid NOT NULL,
	"InfoDetailName"		varchar(250) NOT NULL COLLATE NOCASE,
	"InfoDetailDescription"		varchar(4000) NOT NULL COLLATE NOCASE,
	"IsDeleted"		bit NOT NULL
,
    FOREIGN KEY ([InfoPageID] , [InfoPageGUID])
        REFERENCES [LTD_InfoPage]([InfoPageID] , [InfoPageGUID]),
    FOREIGN KEY ([InfoSectionID] , [InfoSectionGUID])
        REFERENCES [LTD_InfoSection]([InfoSectionID] , [InfoSectionGUID])
);

CREATE UNIQUE INDEX [LTD_InfoDetail_GK_LTD_InfoDetail]
ON [LTD_InfoDetail]
([InfoDetailGUID]);


DROP TABLE IF EXISTS [LTD_InfoPage];
CREATE TABLE [LTD_InfoPage] (
	"InfoPageID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"InfoPageGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL,
	"CreatedUserID"		integer NOT NULL,
	"CreatedUserGUID"		guid NOT NULL,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL,
	"InfoPageName"		varchar(250) NOT NULL COLLATE NOCASE,
	"InfoPageDescription"		varchar(4000) NOT NULL COLLATE NOCASE,
	"InfoCategoryID"		integer NOT NULL,
	"InfoCategoryGUID"		guid NOT NULL,
	"ExpiryDate"		datetime NOT NULL,
	"IsPublic"		bit NOT NULL,
	"Commentable"		bit NOT NULL DEFAULT 0,
	"CommentorRoleList"		varchar(150) COLLATE NOCASE,
	"AsyncLoading"		bit NOT NULL DEFAULT 0,
	"AccessGroupID"		integer,
	"AccessGroupGUID"		guid,
	"IsDeleted"		bit NOT NULL
,
    FOREIGN KEY ([InfoCategoryID] , [InfoCategoryGUID])
        REFERENCES [LTD_InfoCategory]([InfoCategoryID] , [InfoCategoryGUID])
);

CREATE UNIQUE INDEX [LTD_InfoPage_GK_LTD_InfoPage_1]
ON [LTD_InfoPage]
([InfoPageID], [InfoPageGUID]);

CREATE UNIQUE INDEX [LTD_InfoPage_GK_LTD_Page]
ON [LTD_InfoPage]
([InfoPageGUID]);


DROP TABLE IF EXISTS [LTD_InfoSection];
CREATE TABLE [LTD_InfoSection] (
	"InfoSectionID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"InfoSectionGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL,
	"InfoPageID"		integer NOT NULL,
	"InfoPageGUID"		guid NOT NULL,
	"InfoSectionName"		varchar(250) NOT NULL COLLATE NOCASE,
	"InfoSectionDescription"		varchar(4000) NOT NULL COLLATE NOCASE,
	"IsDeleted"		bit NOT NULL
,
    FOREIGN KEY ([InfoPageID] , [InfoPageGUID])
        REFERENCES [LTD_InfoPage]([InfoPageID] , [InfoPageGUID])
);

CREATE UNIQUE INDEX [LTD_InfoSection_GK_LTD_InfoSection]
ON [LTD_InfoSection]
([InfoSectionGUID]);

CREATE UNIQUE INDEX [LTD_InfoSection_GK_LTD_InfoSection_1]
ON [LTD_InfoSection]
([InfoSectionID], [InfoSectionGUID]);


DROP TABLE IF EXISTS [LTD_Subscriber];
CREATE TABLE [LTD_Subscriber] (
	"SubscriberID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"SubscriberGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL,
	"SubscriberMessage"		varchar(500) NOT NULL COLLATE NOCASE,
	"SubscriberEmail"		varchar(100) NOT NULL COLLATE NOCASE,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"IsDeleted"		bit NOT NULL

);





INSERT INTO CNS_DataRefType (DataRefTypeID, DataRefTypeGUID, RevisionNo, DataRefType, IsDefault, IsSystem, IsActive, Sequence) VALUES (1, '095cbfec-51fb-4c23-909c-40798e10890c', 0, 'None', 1, 1, 1, 1);
INSERT INTO CNS_DataRefType (DataRefTypeID, DataRefTypeGUID, RevisionNo, DataRefType, IsDefault, IsSystem, IsActive, Sequence) VALUES (2, '58914899-f329-4793-aed3-c58d0f219b95', 2, 'InfoPage', 0, 1, 1, 2);


INSERT INTO CNS_DataType (DataTypeID, DataTypeGUID, RevisionNo, DataType, IsDefault, IsSystem, IsActive, Sequence) VALUES (1, '871650ca-5cb9-4503-8005-0f1a78f60bc2', 0, 'General Data', 1, 1, 1, 1);
INSERT INTO CNS_DataType (DataTypeID, DataTypeGUID, RevisionNo, DataType, IsDefault, IsSystem, IsActive, Sequence) VALUES (2, 'eca4d9f0-ab37-4dc9-9634-ae157c2978a6', 1, 'Comment Data', 0, 1, 1, 2);











CREATE TRIGGER [fki_CND_Data_ParentDataID-ParentDataGUID_CND_Data_DataID-DataGUID] Before Insert ON [CND_Data] BEGIN SELECT RAISE(ROLLBACK, 'insert on table CND_Data violates foreign key constraint fki_CND_Data_ParentDataID-ParentDataGUID_CND_Data_DataID-DataGUID') WHERE (SELECT DataID FROM CND_Data WHERE DataID = NEW.ParentDataID AND DataGUID = NEW.ParentDataGUID) IS NULL;  END;

CREATE TRIGGER [fku_CND_Data_ParentDataID-ParentDataGUID_CND_Data_DataID-DataGUID] Before Update ON [CND_Data] BEGIN SELECT RAISE(ROLLBACK, 'update on table CND_Data violates foreign key constraint fku_CND_Data_ParentDataID-ParentDataGUID_CND_Data_DataID-DataGUID') WHERE (SELECT DataID FROM CND_Data WHERE DataID = NEW.ParentDataID AND DataGUID = NEW.ParentDataGUID) IS NULL;  END;

CREATE TRIGGER [fkd_CND_Data_ParentDataID-ParentDataGUID_CND_Data_DataID-DataGUID] Before Delete ON [CND_Data] BEGIN SELECT RAISE(ROLLBACK, 'delete on table CND_Data violates foreign key constraint fkd_CND_Data_ParentDataID-ParentDataGUID_CND_Data_DataID-DataGUID') WHERE (SELECT ParentDataID FROM CND_Data WHERE DataID = OLD.ParentDataID AND DataGUID = OLD.ParentDataGUID) IS NOT NULL;  END;

CREATE TRIGGER [fki_CND_Data_DataRefTypeID-DataRefTypeGUID_CNS_DataRefType_DataRefTypeID-DataRefTypeGUID] Before Insert ON [CND_Data] BEGIN SELECT RAISE(ROLLBACK, 'insert on table CND_Data violates foreign key constraint fki_CND_Data_DataRefTypeID-DataRefTypeGUID_CNS_DataRefType_DataRefTypeID-DataRefTypeGUID') WHERE (SELECT DataRefTypeID FROM CNS_DataRefType WHERE DataRefTypeID = NEW.DataRefTypeID AND DataRefTypeGUID = NEW.DataRefTypeGUID) IS NULL;  END;

CREATE TRIGGER [fku_CND_Data_DataRefTypeID-DataRefTypeGUID_CNS_DataRefType_DataRefTypeID-DataRefTypeGUID] Before Update ON [CND_Data] BEGIN SELECT RAISE(ROLLBACK, 'update on table CND_Data violates foreign key constraint fku_CND_Data_DataRefTypeID-DataRefTypeGUID_CNS_DataRefType_DataRefTypeID-DataRefTypeGUID') WHERE (SELECT DataRefTypeID FROM CNS_DataRefType WHERE DataRefTypeID = NEW.DataRefTypeID AND DataRefTypeGUID = NEW.DataRefTypeGUID) IS NULL;  END;

CREATE TRIGGER [fki_CND_Data_DataTypeID-DataTypeGUID_CNS_DataType_DataTypeID-DataTypeGUID] Before Insert ON [CND_Data] BEGIN SELECT RAISE(ROLLBACK, 'insert on table CND_Data violates foreign key constraint fki_CND_Data_DataTypeID-DataTypeGUID_CNS_DataType_DataTypeID-DataTypeGUID') WHERE (SELECT DataTypeID FROM CNS_DataType WHERE DataTypeID = NEW.DataTypeID AND DataTypeGUID = NEW.DataTypeGUID) IS NULL;  END;

CREATE TRIGGER [fku_CND_Data_DataTypeID-DataTypeGUID_CNS_DataType_DataTypeID-DataTypeGUID] Before Update ON [CND_Data] BEGIN SELECT RAISE(ROLLBACK, 'update on table CND_Data violates foreign key constraint fku_CND_Data_DataTypeID-DataTypeGUID_CNS_DataType_DataTypeID-DataTypeGUID') WHERE (SELECT DataTypeID FROM CNS_DataType WHERE DataTypeID = NEW.DataTypeID AND DataTypeGUID = NEW.DataTypeGUID) IS NULL;  END;

CREATE TRIGGER [fkd_CND_Data_DataRefTypeID-DataRefTypeGUID_CNS_DataRefType_DataRefTypeID-DataRefTypeGUID] Before Delete ON [CNS_DataRefType] BEGIN SELECT RAISE(ROLLBACK, 'delete on table CNS_DataRefType violates foreign key constraint fkd_CND_Data_DataRefTypeID-DataRefTypeGUID_CNS_DataRefType_DataRefTypeID-DataRefTypeGUID') WHERE (SELECT DataRefTypeID FROM CND_Data WHERE DataRefTypeID = OLD.DataRefTypeID AND DataRefTypeGUID = OLD.DataRefTypeGUID) IS NOT NULL;  END;

CREATE TRIGGER [fkd_CND_Data_DataTypeID-DataTypeGUID_CNS_DataType_DataTypeID-DataTypeGUID] Before Delete ON [CNS_DataType] BEGIN SELECT RAISE(ROLLBACK, 'delete on table CNS_DataType violates foreign key constraint fkd_CND_Data_DataTypeID-DataTypeGUID_CNS_DataType_DataTypeID-DataTypeGUID') WHERE (SELECT DataTypeID FROM CND_Data WHERE DataTypeID = OLD.DataTypeID AND DataTypeGUID = OLD.DataTypeGUID) IS NOT NULL;  END;

CREATE TRIGGER [fkd_LTD_InfoPage_InfoCategoryID-InfoCategoryGUID_LTD_InfoCategory_InfoCategoryID-InfoCategoryGUID] Before Delete ON [LTD_InfoCategory] BEGIN SELECT RAISE(ROLLBACK, 'delete on table LTD_InfoCategory violates foreign key constraint fkd_LTD_InfoPage_InfoCategoryID-InfoCategoryGUID_LTD_InfoCategory_InfoCategoryID-InfoCategoryGUID') WHERE (SELECT InfoCategoryID FROM LTD_InfoPage WHERE InfoCategoryID = OLD.InfoCategoryID AND InfoCategoryGUID = OLD.InfoCategoryGUID) IS NOT NULL;  END;

CREATE TRIGGER [fki_LTD_InfoDetail_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID] Before Insert ON [LTD_InfoDetail] BEGIN SELECT RAISE(ROLLBACK, 'insert on table LTD_InfoDetail violates foreign key constraint fki_LTD_InfoDetail_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID') WHERE (SELECT InfoPageID FROM LTD_InfoPage WHERE InfoPageID = NEW.InfoPageID AND InfoPageGUID = NEW.InfoPageGUID) IS NULL;  END;

CREATE TRIGGER [fku_LTD_InfoDetail_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID] Before Update ON [LTD_InfoDetail] BEGIN SELECT RAISE(ROLLBACK, 'update on table LTD_InfoDetail violates foreign key constraint fku_LTD_InfoDetail_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID') WHERE (SELECT InfoPageID FROM LTD_InfoPage WHERE InfoPageID = NEW.InfoPageID AND InfoPageGUID = NEW.InfoPageGUID) IS NULL;  END;

CREATE TRIGGER [fki_LTD_InfoDetail_InfoSectionID-InfoSectionGUID_LTD_InfoSection_InfoSectionID-InfoSectionGUID] Before Insert ON [LTD_InfoDetail] BEGIN SELECT RAISE(ROLLBACK, 'insert on table LTD_InfoDetail violates foreign key constraint fki_LTD_InfoDetail_InfoSectionID-InfoSectionGUID_LTD_InfoSection_InfoSectionID-InfoSectionGUID') WHERE (SELECT InfoSectionID FROM LTD_InfoSection WHERE InfoSectionID = NEW.InfoSectionID AND InfoSectionGUID = NEW.InfoSectionGUID) IS NULL;  END;

CREATE TRIGGER [fku_LTD_InfoDetail_InfoSectionID-InfoSectionGUID_LTD_InfoSection_InfoSectionID-InfoSectionGUID] Before Update ON [LTD_InfoDetail] BEGIN SELECT RAISE(ROLLBACK, 'update on table LTD_InfoDetail violates foreign key constraint fku_LTD_InfoDetail_InfoSectionID-InfoSectionGUID_LTD_InfoSection_InfoSectionID-InfoSectionGUID') WHERE (SELECT InfoSectionID FROM LTD_InfoSection WHERE InfoSectionID = NEW.InfoSectionID AND InfoSectionGUID = NEW.InfoSectionGUID) IS NULL;  END;

CREATE TRIGGER [fkd_LTD_InfoSection_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID] Before Delete ON [LTD_InfoPage] BEGIN SELECT RAISE(ROLLBACK, 'delete on table LTD_InfoPage violates foreign key constraint fkd_LTD_InfoSection_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID') WHERE (SELECT InfoPageID FROM LTD_InfoSection WHERE InfoPageID = OLD.InfoPageID AND InfoPageGUID = OLD.InfoPageGUID) IS NOT NULL;  END;

CREATE TRIGGER [fkd_LTD_InfoDetail_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID] Before Delete ON [LTD_InfoPage] BEGIN SELECT RAISE(ROLLBACK, 'delete on table LTD_InfoPage violates foreign key constraint fkd_LTD_InfoDetail_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID') WHERE (SELECT InfoPageID FROM LTD_InfoDetail WHERE InfoPageID = OLD.InfoPageID AND InfoPageGUID = OLD.InfoPageGUID) IS NOT NULL;  END;

CREATE TRIGGER [fki_LTD_InfoPage_InfoCategoryID-InfoCategoryGUID_LTD_InfoCategory_InfoCategoryID-InfoCategoryGUID] Before Insert ON [LTD_InfoPage] BEGIN SELECT RAISE(ROLLBACK, 'insert on table LTD_InfoPage violates foreign key constraint fki_LTD_InfoPage_InfoCategoryID-InfoCategoryGUID_LTD_InfoCategory_InfoCategoryID-InfoCategoryGUID') WHERE (SELECT InfoCategoryID FROM LTD_InfoCategory WHERE InfoCategoryID = NEW.InfoCategoryID AND InfoCategoryGUID = NEW.InfoCategoryGUID) IS NULL;  END;

CREATE TRIGGER [fku_LTD_InfoPage_InfoCategoryID-InfoCategoryGUID_LTD_InfoCategory_InfoCategoryID-InfoCategoryGUID] Before Update ON [LTD_InfoPage] BEGIN SELECT RAISE(ROLLBACK, 'update on table LTD_InfoPage violates foreign key constraint fku_LTD_InfoPage_InfoCategoryID-InfoCategoryGUID_LTD_InfoCategory_InfoCategoryID-InfoCategoryGUID') WHERE (SELECT InfoCategoryID FROM LTD_InfoCategory WHERE InfoCategoryID = NEW.InfoCategoryID AND InfoCategoryGUID = NEW.InfoCategoryGUID) IS NULL;  END;

CREATE TRIGGER [fki_LTD_InfoSection_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID] Before Insert ON [LTD_InfoSection] BEGIN SELECT RAISE(ROLLBACK, 'insert on table LTD_InfoSection violates foreign key constraint fki_LTD_InfoSection_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID') WHERE (SELECT InfoPageID FROM LTD_InfoPage WHERE InfoPageID = NEW.InfoPageID AND InfoPageGUID = NEW.InfoPageGUID) IS NULL;  END;

CREATE TRIGGER [fku_LTD_InfoSection_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID] Before Update ON [LTD_InfoSection] BEGIN SELECT RAISE(ROLLBACK, 'update on table LTD_InfoSection violates foreign key constraint fku_LTD_InfoSection_InfoPageID-InfoPageGUID_LTD_InfoPage_InfoPageID-InfoPageGUID') WHERE (SELECT InfoPageID FROM LTD_InfoPage WHERE InfoPageID = NEW.InfoPageID AND InfoPageGUID = NEW.InfoPageGUID) IS NULL;  END;

CREATE TRIGGER [fkd_LTD_InfoDetail_InfoSectionID-InfoSectionGUID_LTD_InfoSection_InfoSectionID-InfoSectionGUID] Before Delete ON [LTD_InfoSection] BEGIN SELECT RAISE(ROLLBACK, 'delete on table LTD_InfoSection violates foreign key constraint fkd_LTD_InfoDetail_InfoSectionID-InfoSectionGUID_LTD_InfoSection_InfoSectionID-InfoSectionGUID') WHERE (SELECT InfoSectionID FROM LTD_InfoDetail WHERE InfoSectionID = OLD.InfoSectionID AND InfoSectionGUID = OLD.InfoSectionGUID) IS NOT NULL;  END;


DROP TABLE IF EXISTS [ACD_Request];
CREATE TABLE [ACD_Request] (
	"RequestID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"RequestGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"RequestTypeID"		integer NOT NULL,
	"RequestTypeGUID"		guid NOT NULL,
	"RequestMessage"		nvarchar(4000) NOT NULL COLLATE NOCASE,
	"ResponseMessage"		nvarchar(4000) COLLATE NOCASE,
	"RequestUserID"		integer NOT NULL,
	"RequestUserGUID"		guid NOT NULL,
	"ResponseDate"		datetime,
	"ResponseUserID"		integer,
	"ResponseUserGUID"		guid,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL,
	"IsResponded"		bit NOT NULL,
	"IsDeleted"		bit NOT NULL,
	"IsPublic"		bit NOT NULL
,
    FOREIGN KEY ([RequestTypeID] , [RequestTypeGUID])
        REFERENCES [ACS_RequestType]([RequestTypeID] , [RequestTypeGUID]),
    FOREIGN KEY ([RequestUserID] , [RequestUserGUID])
        REFERENCES [SCD_User]([UserID] , [UserGUID]),
    FOREIGN KEY ([ResponseUserID] , [ResponseUserGUID])
        REFERENCES [SCD_User]([UserID] , [UserGUID])
);

CREATE UNIQUE INDEX [ACD_Request_GK_ACD_Request]
ON [ACD_Request]
([RequestGUID]);


DROP TABLE IF EXISTS [ACS_RequestType];
CREATE TABLE [ACS_RequestType] (
	"RequestTypeID"		integer NOT NULL,
	"RequestTypeGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"RequestType"		varchar(50) NOT NULL COLLATE NOCASE,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL,
    PRIMARY KEY ([RequestTypeID])

);

CREATE UNIQUE INDEX [ACS_RequestType_GK_ACS_RequestType]
ON [ACS_RequestType]
([RequestTypeGUID]);

CREATE UNIQUE INDEX [ACS_RequestType_GK_ACS_RequestType_1]
ON [ACS_RequestType]
([RequestTypeID], [RequestTypeGUID]);


DROP TABLE IF EXISTS [SCD_Group];
CREATE TABLE [SCD_Group] (
	"GroupID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"GroupGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"GroupName"		nvarchar(50) NOT NULL COLLATE NOCASE,
	"IsDefault"		bit NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL DEFAULT 0
,
    FOREIGN KEY ([UserID] , [UserGUID])
        REFERENCES [SCD_User]([UserID] , [UserGUID])
);

CREATE UNIQUE INDEX [SCD_Group_GK_SCD_Group]
ON [SCD_Group]
([GroupGUID]);

CREATE UNIQUE INDEX [SCD_Group_GK_SCD_Group_1]
ON [SCD_Group]
([GroupID], [GroupGUID]);

CREATE UNIQUE INDEX [SCD_Group_UK_SCD_Group]
ON [SCD_Group]
([UserID], [GroupName]);


DROP TABLE IF EXISTS [SCD_GroupUser];
CREATE TABLE [SCD_GroupUser] (
	"GroupUserID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"GroupUserGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"GroupID"		integer NOT NULL,
	"GroupGUID"		guid NOT NULL,
	"MemberID"		integer NOT NULL
,
    FOREIGN KEY ([GroupID] , [GroupGUID])
        REFERENCES [SCD_Group]([GroupID] , [GroupGUID]),
    FOREIGN KEY ([UserID] , [UserGUID])
        REFERENCES [SCD_User]([UserID] , [UserGUID])
);

CREATE UNIQUE INDEX [SCD_GroupUser_GK_SCD_GroupUser]
ON [SCD_GroupUser]
([GroupUserID], [GroupUserGUID]);

CREATE UNIQUE INDEX [SCD_GroupUser_UK_SCD_GroupUser]
ON [SCD_GroupUser]
([GroupID], [MemberID]);


DROP TABLE IF EXISTS [SCD_Invite];
CREATE TABLE [SCD_Invite] (
	"InviteID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"InviteGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"LastUserID"		integer NOT NULL,
	"LastUserGUID"		guid NOT NULL,
	"InviteCode"		varchar(50) NOT NULL COLLATE NOCASE,
	"InviteMaxNo"		integer NOT NULL,
	"InviteConsumed"		integer NOT NULL,
	"InviteRoleList"		varchar(100) NOT NULL COLLATE NOCASE DEFAULT 'GuestRole',
	"InviteCredit"		float NOT NULL DEFAULT 0,
	"IsAutoRegister"		bit NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL DEFAULT 1
,
    FOREIGN KEY ([LastUserID] , [LastUserGUID])
        REFERENCES [SCD_User]([UserID] , [UserGUID])
);

CREATE UNIQUE INDEX [SCD_Invite_GK_SCD_Invite]
ON [SCD_Invite]
([InviteGUID]);


DROP TABLE IF EXISTS [SCD_User];
CREATE TABLE [SCD_User] (
	"UserID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"UserGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"UserLoginID"		nvarchar(50) NOT NULL COLLATE NOCASE,
	"UserEMail"		nvarchar(50) NOT NULL COLLATE NOCASE,
	"UserName"		nvarchar(50) NOT NULL COLLATE NOCASE,
	"DefaultRole"		varchar(50) NOT NULL COLLATE NOCASE,
	"Telephone"		varchar(100) COLLATE NOCASE,
	"GravatorUrl"		varchar(100) COLLATE NOCASE,
	"ProfileImageUrl"		varchar(100) COLLATE NOCASE,
	"EmailConfirmed"		bit NOT NULL,
	"ResetPwdRequest"		bit NOT NULL,
	"ResetPwdRequestDate"		datetime NOT NULL,
	"ResetPwdSent"		bit NOT NULL,
	"ResetPwd"		varchar(50) COLLATE NOCASE,
	"IsActive"		bit NOT NULL DEFAULT 0,
	"Sequence"		integer NOT NULL DEFAULT 0,
	"UserAppKey"		varchar(25) NOT NULL COLLATE NOCASE

);

CREATE UNIQUE INDEX [SCD_User_GK_SCD_User]
ON [SCD_User]
([UserGUID]);

CREATE UNIQUE INDEX [SCD_User_GK_SCD_User_1]
ON [SCD_User]
([UserID], [UserGUID]);

CREATE UNIQUE INDEX [SCD_User_UK_SCD_User]
ON [SCD_User]
([UserEMail]);

CREATE UNIQUE INDEX [SCD_User_UK_SCD_User_1]
ON [SCD_User]
([UserLoginID]);

CREATE UNIQUE INDEX [SCD_User_UK_SCD_User_2]
ON [SCD_User]
([UserAppKey]);


DROP TABLE IF EXISTS [SCD_UserKey];
CREATE TABLE [SCD_UserKey] (
	"UserKeyID"		integer NOT NULL,
	"UserKeyGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUsedDate"		datetime NOT NULL,
	"RandomKey"		varchar(50) NOT NULL COLLATE NOCASE,
	"PublicKey"		varchar(1024) NOT NULL COLLATE NOCASE,
	"IsValid"		bit NOT NULL DEFAULT 0,
    PRIMARY KEY ([UserKeyID])
,
    FOREIGN KEY ([UserID] , [UserGUID])
        REFERENCES [SCD_User]([UserID] , [UserGUID])
);

CREATE UNIQUE INDEX [SCD_UserKey_GK_SCD_UserKey]
ON [SCD_UserKey]
([UserKeyGUID]);


DROP TABLE IF EXISTS [SYS_ActionRole];
CREATE TABLE [SYS_ActionRole] (
	"ActionRoleID"		integer NOT NULL,
	"ActionRoleGUID"		guid NOT NULL,
	"ActionName"		varchar(30) NOT NULL COLLATE NOCASE,
	"ActionRoleList"		varchar(250) NOT NULL COLLATE NOCASE,
    PRIMARY KEY ([ActionRoleID])

);

CREATE UNIQUE INDEX [SYS_ActionRole_GK_SYS_ActionRole]
ON [SYS_ActionRole]
([ActionRoleGUID]);

CREATE UNIQUE INDEX [SYS_ActionRole_UK_SYS_ActionRole]
ON [SYS_ActionRole]
([ActionName]);





INSERT INTO ACS_RequestType (RequestTypeID, RequestTypeGUID, RevisionNo, RequestType, IsActive, Sequence) VALUES (1, '456a9e86-ee72-4dc3-b22b-b5042560d160', 0, 'General Feedback', 1, 1);
INSERT INTO ACS_RequestType (RequestTypeID, RequestTypeGUID, RevisionNo, RequestType, IsActive, Sequence) VALUES (2, '70f03d0c-19b8-4c5c-a73b-15f2ec115045', 0, 'Account Related', 1, 1);
INSERT INTO ACS_RequestType (RequestTypeID, RequestTypeGUID, RevisionNo, RequestType, IsActive, Sequence) VALUES (3, '6358d0c9-fcd6-4a6d-94cc-cfab481e1ceb', 0, 'Change Request', 1, 1);
INSERT INTO ACS_RequestType (RequestTypeID, RequestTypeGUID, RevisionNo, RequestType, IsActive, Sequence) VALUES (99, '9ff60b89-50f4-4bfa-93d9-857d3f876b3c', 0, 'System Response', 0, 1);













CREATE TRIGGER [fki_ACD_Request_RequestTypeID-RequestTypeGUID_ACS_RequestType_RequestTypeID-RequestTypeGUID] Before Insert ON [ACD_Request] BEGIN SELECT RAISE(ROLLBACK, 'insert on table ACD_Request violates foreign key constraint fki_ACD_Request_RequestTypeID-RequestTypeGUID_ACS_RequestType_RequestTypeID-RequestTypeGUID') WHERE (SELECT RequestTypeID FROM ACS_RequestType WHERE RequestTypeID = NEW.RequestTypeID AND RequestTypeGUID = NEW.RequestTypeGUID) IS NULL;  END;

CREATE TRIGGER [fku_ACD_Request_RequestTypeID-RequestTypeGUID_ACS_RequestType_RequestTypeID-RequestTypeGUID] Before Update ON [ACD_Request] BEGIN SELECT RAISE(ROLLBACK, 'update on table ACD_Request violates foreign key constraint fku_ACD_Request_RequestTypeID-RequestTypeGUID_ACS_RequestType_RequestTypeID-RequestTypeGUID') WHERE (SELECT RequestTypeID FROM ACS_RequestType WHERE RequestTypeID = NEW.RequestTypeID AND RequestTypeGUID = NEW.RequestTypeGUID) IS NULL;  END;

CREATE TRIGGER [fki_ACD_Request_RequestUserID-RequestUserGUID_SCD_User_UserID-UserGUID] Before Insert ON [ACD_Request] BEGIN SELECT RAISE(ROLLBACK, 'insert on table ACD_Request violates foreign key constraint fki_ACD_Request_RequestUserID-RequestUserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.RequestUserID AND UserGUID = NEW.RequestUserGUID) IS NULL;  END;

CREATE TRIGGER [fku_ACD_Request_RequestUserID-RequestUserGUID_SCD_User_UserID-UserGUID] Before Update ON [ACD_Request] BEGIN SELECT RAISE(ROLLBACK, 'update on table ACD_Request violates foreign key constraint fku_ACD_Request_RequestUserID-RequestUserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.RequestUserID AND UserGUID = NEW.RequestUserGUID) IS NULL;  END;

CREATE TRIGGER [fki_ACD_Request_ResponseUserID-ResponseUserGUID_SCD_User_UserID-UserGUID] Before Insert ON [ACD_Request] BEGIN SELECT RAISE(ROLLBACK, 'insert on table ACD_Request violates foreign key constraint fki_ACD_Request_ResponseUserID-ResponseUserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.ResponseUserID AND UserGUID = NEW.ResponseUserGUID) IS NULL;  END;

CREATE TRIGGER [fku_ACD_Request_ResponseUserID-ResponseUserGUID_SCD_User_UserID-UserGUID] Before Update ON [ACD_Request] BEGIN SELECT RAISE(ROLLBACK, 'update on table ACD_Request violates foreign key constraint fku_ACD_Request_ResponseUserID-ResponseUserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.ResponseUserID AND UserGUID = NEW.ResponseUserGUID) IS NULL;  END;

CREATE TRIGGER [fkd_ACD_Request_RequestTypeID-RequestTypeGUID_ACS_RequestType_RequestTypeID-RequestTypeGUID] Before Delete ON [ACS_RequestType] BEGIN SELECT RAISE(ROLLBACK, 'delete on table ACS_RequestType violates foreign key constraint fkd_ACD_Request_RequestTypeID-RequestTypeGUID_ACS_RequestType_RequestTypeID-RequestTypeGUID') WHERE (SELECT RequestTypeID FROM ACD_Request WHERE RequestTypeID = OLD.RequestTypeID AND RequestTypeGUID = OLD.RequestTypeGUID) IS NOT NULL;  END;

CREATE TRIGGER [fkd_SCD_GroupUser_GroupID-GroupGUID_SCD_Group_GroupID-GroupGUID] Before Delete ON [SCD_Group] BEGIN SELECT RAISE(ROLLBACK, 'delete on table SCD_Group violates foreign key constraint fkd_SCD_GroupUser_GroupID-GroupGUID_SCD_Group_GroupID-GroupGUID') WHERE (SELECT GroupID FROM SCD_GroupUser WHERE GroupID = OLD.GroupID AND GroupGUID = OLD.GroupGUID) IS NOT NULL;  END;

CREATE TRIGGER [fki_SCD_Group_UserID-UserGUID_SCD_User_UserID-UserGUID] Before Insert ON [SCD_Group] BEGIN SELECT RAISE(ROLLBACK, 'insert on table SCD_Group violates foreign key constraint fki_SCD_Group_UserID-UserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.UserID AND UserGUID = NEW.UserGUID) IS NULL;  END;

CREATE TRIGGER [fku_SCD_Group_UserID-UserGUID_SCD_User_UserID-UserGUID] Before Update ON [SCD_Group] BEGIN SELECT RAISE(ROLLBACK, 'update on table SCD_Group violates foreign key constraint fku_SCD_Group_UserID-UserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.UserID AND UserGUID = NEW.UserGUID) IS NULL;  END;

CREATE TRIGGER [fki_SCD_GroupUser_GroupID-GroupGUID_SCD_Group_GroupID-GroupGUID] Before Insert ON [SCD_GroupUser] BEGIN SELECT RAISE(ROLLBACK, 'insert on table SCD_GroupUser violates foreign key constraint fki_SCD_GroupUser_GroupID-GroupGUID_SCD_Group_GroupID-GroupGUID') WHERE (SELECT GroupID FROM SCD_Group WHERE GroupID = NEW.GroupID AND GroupGUID = NEW.GroupGUID) IS NULL;  END;

CREATE TRIGGER [fku_SCD_GroupUser_GroupID-GroupGUID_SCD_Group_GroupID-GroupGUID] Before Update ON [SCD_GroupUser] BEGIN SELECT RAISE(ROLLBACK, 'update on table SCD_GroupUser violates foreign key constraint fku_SCD_GroupUser_GroupID-GroupGUID_SCD_Group_GroupID-GroupGUID') WHERE (SELECT GroupID FROM SCD_Group WHERE GroupID = NEW.GroupID AND GroupGUID = NEW.GroupGUID) IS NULL;  END;

CREATE TRIGGER [fki_SCD_GroupUser_UserID-UserGUID_SCD_User_UserID-UserGUID] Before Insert ON [SCD_GroupUser] BEGIN SELECT RAISE(ROLLBACK, 'insert on table SCD_GroupUser violates foreign key constraint fki_SCD_GroupUser_UserID-UserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.UserID AND UserGUID = NEW.UserGUID) IS NULL;  END;

CREATE TRIGGER [fku_SCD_GroupUser_UserID-UserGUID_SCD_User_UserID-UserGUID] Before Update ON [SCD_GroupUser] BEGIN SELECT RAISE(ROLLBACK, 'update on table SCD_GroupUser violates foreign key constraint fku_SCD_GroupUser_UserID-UserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.UserID AND UserGUID = NEW.UserGUID) IS NULL;  END;

CREATE TRIGGER [fki_SCD_Invite_LastUserID-LastUserGUID_SCD_User_UserID-UserGUID] Before Insert ON [SCD_Invite] BEGIN SELECT RAISE(ROLLBACK, 'insert on table SCD_Invite violates foreign key constraint fki_SCD_Invite_LastUserID-LastUserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.LastUserID AND UserGUID = NEW.LastUserGUID) IS NULL;  END;

CREATE TRIGGER [fku_SCD_Invite_LastUserID-LastUserGUID_SCD_User_UserID-UserGUID] Before Update ON [SCD_Invite] BEGIN SELECT RAISE(ROLLBACK, 'update on table SCD_Invite violates foreign key constraint fku_SCD_Invite_LastUserID-LastUserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.LastUserID AND UserGUID = NEW.LastUserGUID) IS NULL;  END;

CREATE TRIGGER [fkd_SCD_GroupUser_UserID-UserGUID_SCD_User_UserID-UserGUID] Before Delete ON [SCD_User] BEGIN SELECT RAISE(ROLLBACK, 'delete on table SCD_User violates foreign key constraint fkd_SCD_GroupUser_UserID-UserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_GroupUser WHERE UserID = OLD.UserID AND UserGUID = OLD.UserGUID) IS NOT NULL;  END;

CREATE TRIGGER [fkd_ACD_Request_RequestUserID-RequestUserGUID_SCD_User_UserID-UserGUID] Before Delete ON [SCD_User] BEGIN SELECT RAISE(ROLLBACK, 'delete on table SCD_User violates foreign key constraint fkd_ACD_Request_RequestUserID-RequestUserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT RequestUserID FROM ACD_Request WHERE UserID = OLD.RequestUserID AND UserGUID = OLD.RequestUserGUID) IS NOT NULL;  END;

CREATE TRIGGER [fkd_ACD_Request_ResponseUserID-ResponseUserGUID_SCD_User_UserID-UserGUID] Before Delete ON [SCD_User] BEGIN SELECT RAISE(ROLLBACK, 'delete on table SCD_User violates foreign key constraint fkd_ACD_Request_ResponseUserID-ResponseUserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT ResponseUserID FROM ACD_Request WHERE UserID = OLD.ResponseUserID AND UserGUID = OLD.ResponseUserGUID) IS NOT NULL;  END;

CREATE TRIGGER [fkd_SCD_Invite_LastUserID-LastUserGUID_SCD_User_UserID-UserGUID] Before Delete ON [SCD_User] BEGIN SELECT RAISE(ROLLBACK, 'delete on table SCD_User violates foreign key constraint fkd_SCD_Invite_LastUserID-LastUserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT LastUserID FROM SCD_Invite WHERE UserID = OLD.LastUserID AND UserGUID = OLD.LastUserGUID) IS NOT NULL;  END;

CREATE TRIGGER [fkd_SCD_UserKey_UserID-UserGUID_SCD_User_UserID-UserGUID] Before Delete ON [SCD_User] BEGIN SELECT RAISE(ROLLBACK, 'delete on table SCD_User violates foreign key constraint fkd_SCD_UserKey_UserID-UserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_UserKey WHERE UserID = OLD.UserID AND UserGUID = OLD.UserGUID) IS NOT NULL;  END;

CREATE TRIGGER [fkd_SCD_Group_UserID-UserGUID_SCD_User_UserID-UserGUID] Before Delete ON [SCD_User] BEGIN SELECT RAISE(ROLLBACK, 'delete on table SCD_User violates foreign key constraint fkd_SCD_Group_UserID-UserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_Group WHERE UserID = OLD.UserID AND UserGUID = OLD.UserGUID) IS NOT NULL;  END;

CREATE TRIGGER [fki_SCD_UserKey_UserID-UserGUID_SCD_User_UserID-UserGUID] Before Insert ON [SCD_UserKey] BEGIN SELECT RAISE(ROLLBACK, 'insert on table SCD_UserKey violates foreign key constraint fki_SCD_UserKey_UserID-UserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.UserID AND UserGUID = NEW.UserGUID) IS NULL;  END;

CREATE TRIGGER [fku_SCD_UserKey_UserID-UserGUID_SCD_User_UserID-UserGUID] Before Update ON [SCD_UserKey] BEGIN SELECT RAISE(ROLLBACK, 'update on table SCD_UserKey violates foreign key constraint fku_SCD_UserKey_UserID-UserGUID_SCD_User_UserID-UserGUID') WHERE (SELECT UserID FROM SCD_User WHERE UserID = NEW.UserID AND UserGUID = NEW.UserGUID) IS NULL;  END;


DROP TABLE IF EXISTS [SYC_Config];
CREATE TABLE [SYC_Config] (
	"ConfigID"		integer NOT NULL,
	"ConfigGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"UserID"		integer,
	"UserGUID"		guid,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"ConfigName"		varchar(50) NOT NULL COLLATE NOCASE,
	"ConfigDescription"		varchar(250) NOT NULL COLLATE NOCASE,
	"ConfigValue"		varchar(250) COLLATE NOCASE,
	"ConfigAcceptValue"		varchar(250) COLLATE NOCASE,
	"DataMaxLength"		integer NOT NULL,
	"DataMinLength"		integer NOT NULL,
	"IsUserConfig"		bit NOT NULL,
	"IsActive"		bit NOT NULL,
    PRIMARY KEY ([ConfigID])

);

CREATE UNIQUE INDEX [SYC_Config_GK_SYC_Config]
ON [SYC_Config]
([ConfigGUID]);

CREATE UNIQUE INDEX [SYC_Config_UK_SYC_Config]
ON [SYC_Config]
([ConfigName]);


DROP TABLE IF EXISTS [SYC_Sync];
CREATE TABLE [SYC_Sync] (
	"SyncID"		integer NOT NULL,
	"SyncGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"SyncName"		varchar(30) NOT NULL COLLATE NOCASE,
	"SyncUrl"		varchar(250) NOT NULL COLLATE NOCASE,
	"SyncUserID"		varchar(50) NOT NULL COLLATE NOCASE,
	"SyncUserPwd"		varchar(50) NOT NULL COLLATE NOCASE,
	"SyncLogCount"		integer NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL,
    PRIMARY KEY ([SyncID])

);

CREATE UNIQUE INDEX [SYC_Sync_GK_SYC_Sync]
ON [SYC_Sync]
([SyncGUID]);


DROP TABLE IF EXISTS [SYC_UserProfile];
CREATE TABLE [SYC_UserProfile] (
	"UserProfileID"		integer PRIMARY KEY AUTOINCREMENT NOT NULL,
	"UserProfileGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"LastUpdateDate"		datetime NOT NULL,
	"UserProfileName"		varchar(50) NOT NULL COLLATE NOCASE,
	"StartPage"		varchar(50) COLLATE NOCASE,
	"JQueryMobileTheme"		varchar(50) NOT NULL COLLATE NOCASE,
	"DefaultViewMode"		integer NOT NULL,
	"ItemsPerPage"		integer NOT NULL,
	"InnerItemsPerPage"		integer NOT NULL,
	"ItemsPerRow"		integer NOT NULL,
	"InfoItemsPerPage"		integer NOT NULL,
	"DetailItemsPerPage"		integer NOT NULL,
	"DefaultIconWidth"		integer NOT NULL,
	"DefaultIconHeight"		integer NOT NULL,
	"DefaultImageWidth"		integer NOT NULL,
	"DefaultImageHeight"		integer NOT NULL,
	"DefaultAvatarSize"		integer NOT NULL,
	"IsDefault"		bit NOT NULL,
	"IsActive"		bit NOT NULL,
	"Sequence"		integer NOT NULL

);

CREATE UNIQUE INDEX [SYC_UserProfile_GK_SYC_UserProfile]
ON [SYC_UserProfile]
([UserProfileGUID]);

CREATE UNIQUE INDEX [SYC_UserProfile_UK_SYC_UserProfile]
ON [SYC_UserProfile]
([UserProfileName], [UserID]);


DROP TABLE IF EXISTS [SYD_SyncLog];
CREATE TABLE [SYD_SyncLog] (
	"SyncLogID"		integer NOT NULL,
	"SyncLogGUID"		guid NOT NULL,
	"RevisionNo"		integer NOT NULL DEFAULT 0,
	"UserID"		integer NOT NULL,
	"UserGUID"		guid NOT NULL,
	"CreatedDate"		datetime NOT NULL,
	"SyncID"		integer NOT NULL,
	"SyncLog"		varchar(500) NOT NULL COLLATE NOCASE,
	"Sequence"		integer NOT NULL,
    PRIMARY KEY ([SyncLogID])
,
    FOREIGN KEY ([SyncID])
        REFERENCES [SYC_Sync]([SyncID])
);



INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (1, '1e324b7d-4b8a-4b4e-9023-737f36f0fd22', 566, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'DefaultView', 'DefaultView', 'DOCUMENTVIEW', NULL, 250, 0, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (3, 'bcc52c24-e537-4573-bfa8-a4a1b69a787e', 567, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ForceLogin', 'Force the Login', 'False', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (4, 'bd788965-137a-4465-9bd2-6fe3737930bf', 570, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowAccountInfo', 'Show Account Info', 'False', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (5, 'b6b83e0d-164a-423e-9f32-a3e7c13178b9', 574, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowLogInfo', 'Show Log Info', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (6, '2c2c36bc-f834-4251-8720-6918312759ac', 571, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowAPILink', 'Show the API Link', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (7, 'f5d8558a-3abb-465a-b4c7-dcb1a9bf0dcc', 572, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowSettingLink', 'Show the Setting Link', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (8, 'd95927da-13c5-46ba-953b-f4bdf34736fe', 568, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowAutoRegisterLink', 'Show the Auto Register Link', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (9, '2fb958dc-5a41-435b-99e0-5f4c6f999232', 569, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowRegisterLink', 'Show the Register Link', 'False', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (10, '04748118-feae-45b9-a4b6-029413a11174', 573, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowTrackingLink', 'Show the Tracking Link', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (20, 'b84ab6dd-0657-4467-b91d-e05f924ac2fb', 575, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'DateFormat', 'Date Format', 'dd/MMM/yyyy', NULL, 16, 1, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (21, '8ffbf316-b07b-45db-933f-c0a409890839', 576, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'NumberFormat', 'Number Format', '{0:N3}', NULL, 16, 1, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (50, '12af3a80-9d88-4d30-a438-a290de775f44', 40, 2, 'ff97cb77-77c8-4800-8f3d-3ca7713f78d0', '2012-01-25 06:53:36', '2012-08-29 19:34:05', 'ProductionTestMode', 'Production Test Mode', 'False', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (51, 'edba3118-d712-44e2-a90b-0e092ce4da4e', 327, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'DevelopmentTestMode', 'Development Test Mode', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (52, '70f7c08c-7dd9-4950-ad93-3f501b3609c8', 328, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceVersion', 'Resource Version', '1', NULL, 2, 1, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (53, '7bc1f5c4-d70e-49f9-84fe-c9ca707b695a', 329, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceOptimize', 'Optimize Resources', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (54, '11780585-cddf-4559-a0c7-41f900a84eee', 330, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceCompress', 'Compress Resources', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (55, 'cbe5fa62-3649-4353-8fb1-2bf868b31e3e', 331, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceExpires', 'Expire Resources', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (56, '19663127-9b9b-41e4-b709-8cef861a79c6', 332, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceCache', 'Cache Resources', 'True', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (57, 'd507faf9-9bb8-4a22-aee8-a960f21997a5', 333, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceBase64', 'Base64 Image Resources', 'False', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (58, '0636e0da-c444-43a4-a269-3bca2a0e2281', 334, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceSplit', 'Split Static Resources', 'False', NULL, 5, 4, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (59, '5b282fc7-736f-4f4f-930f-413419ee8de4', 335, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceSplitMax', 'Max Split Static Resources', '3', NULL, 2, 1, 0, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (100, '806c78f9-cb9f-476a-8678-70688f2c7919', 531, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'StartPage', 'StartPage', NULL, NULL, 250, 0, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (101, '7e299edc-34f1-48dc-981a-a0df83b2f5e1', 534, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'ItemsPerPage', 'The Default Number of Items to Display in a Page', '2', NULL, 2, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (102, 'b0acd884-dca2-46c2-b665-7960fbd42c54', 538, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'InnerItemsPerPage', 'The Default Number of Items to Display Inside a Content', '4', NULL, 2, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (103, '24fb823a-d0b6-47ac-9079-0c07cb794188', 536, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'ItemsPerRow', 'The Default Number of Items to Display in a Row', '4', NULL, 2, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (104, 'aa0982cc-bda8-4f9f-9e5a-4308118805ca', 537, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'InfoItemsPerPage', 'The Default Number of Info Items to Display in a Page', '4', NULL, 2, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (105, '339fab2c-9419-4127-9404-358f739fc96c', 0, NULL, NULL, '2012-01-25 06:53:36', '1900-01-01 00:00:06', 'DetailItemsPerPage', 'The Default Number of Detail Items to Display in a Page', '4', NULL, 2, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (106, 'f6f2c402-ecff-470c-b3e0-8a877eba4578', 532, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'JQueryMobileTheme', 'JQuery Theme ', 'Default', NULL, 30, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (107, '18fe43ee-c4cf-4975-aa66-05656c5b5097', 533, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaultViewMode', 'Default View Mode (List, Thumb)', '0', NULL, 1, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (108, '4855c7f5-0c7b-4ca3-a19d-6d2d664eb8a1', 539, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaultIconWidth', 'Default Width of Icon', '70', NULL, 3, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (109, 'cdee4f64-748f-4ba3-9a14-8722c8d5a489', 540, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaultIconHeight', 'Default Height of Icon', '70', NULL, 3, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (110, '9cfd625f-7df5-4ac5-aa77-cebcb0ffe737', 541, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaultImageWidth', 'Default Width of Image', '150', NULL, 3, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (111, 'be51340c-9b35-45a6-ab58-f6ad99ebdd04', 542, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaulImageHeight', 'Default Height of Image', '150', NULL, 3, 1, 1, 1);
INSERT INTO SYC_Config (ConfigID, ConfigGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, ConfigName, ConfigDescription, ConfigValue, ConfigAcceptValue, DataMaxLength, DataMinLength, IsUserConfig, IsActive) VALUES (112, 'e9bd06b1-b817-4b8f-9c36-41efb2d93a1d', 543, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaulAvatarSize', 'Default Size of Square Image', '80', NULL, 3, 1, 1, 1);




INSERT INTO SYC_UserProfile (UserProfileID, UserProfileGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, UserProfileName, StartPage, JQueryMobileTheme, DefaultViewMode, ItemsPerPage, InnerItemsPerPage, ItemsPerRow, InfoItemsPerPage, DetailItemsPerPage, DefaultIconWidth, DefaultIconHeight, DefaultImageWidth, DefaultImageHeight, DefaultAvatarSize, IsDefault, IsActive, Sequence) VALUES (1, '1b67e8c9-703e-426b-89d1-9176c276f5f5', 0, 2, 'ff97cb77-77c8-4800-8f3d-3ca7713f78d0', '2012-08-29 19:34:15', '2012-08-29 19:34:15', 'Default', NULL, 'Default', 0, 2, 4, 4, 4, 4, 70, 70, 150, 150, 80, 0, 0, 1);
INSERT INTO SYC_UserProfile (UserProfileID, UserProfileGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, UserProfileName, StartPage, JQueryMobileTheme, DefaultViewMode, ItemsPerPage, InnerItemsPerPage, ItemsPerRow, InfoItemsPerPage, DetailItemsPerPage, DefaultIconWidth, DefaultIconHeight, DefaultImageWidth, DefaultImageHeight, DefaultAvatarSize, IsDefault, IsActive, Sequence) VALUES (2, '9cf212af-ab5b-4b3e-a3a6-6bc4c3f56cec', 0, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-09-15 10:40:52', '2012-09-17 10:11:35', 'Default', NULL, 'Default', 1, 2, 4, 4, 4, 4, 70, 70, 150, 150, 80, 0, 0, 1);
INSERT INTO SYC_UserProfile (UserProfileID, UserProfileGUID, RevisionNo, UserID, UserGUID, CreatedDate, LastUpdateDate, UserProfileName, StartPage, JQueryMobileTheme, DefaultViewMode, ItemsPerPage, InnerItemsPerPage, ItemsPerRow, InfoItemsPerPage, DetailItemsPerPage, DefaultIconWidth, DefaultIconHeight, DefaultImageWidth, DefaultImageHeight, DefaultAvatarSize, IsDefault, IsActive, Sequence) VALUES (3, 'd4859c02-5c34-4897-a6fa-afb712bc95ab', 0, 10039, '4f3c72fa-00b2-4d7c-ba71-2eb226009158', '2012-09-15 16:24:58', '2012-09-15 16:53:49', 'Default', NULL, 'Default', 0, 2, 4, 4, 4, 4, 70, 70, 150, 150, 80, 0, 0, 1);



CREATE TRIGGER [fkd_SYD_SyncLog_SyncID_SYC_Sync_SyncID] Before Delete ON [SYC_Sync] BEGIN SELECT RAISE(ROLLBACK, 'delete on table SYC_Sync violates foreign key constraint fkd_SYD_SyncLog_SyncID_SYC_Sync_SyncID') WHERE (SELECT SyncID FROM SYD_SyncLog WHERE SyncID = OLD.SyncID) IS NOT NULL;  END;

CREATE TRIGGER [fki_SYD_SyncLog_SyncID_SYC_Sync_SyncID] Before Insert ON [SYD_SyncLog] BEGIN SELECT RAISE(ROLLBACK, 'insert on table SYD_SyncLog violates foreign key constraint fki_SYD_SyncLog_SyncID_SYC_Sync_SyncID') WHERE (SELECT SyncID FROM SYC_Sync WHERE SyncID = NEW.SyncID) IS NULL;  END;

CREATE TRIGGER [fku_SYD_SyncLog_SyncID_SYC_Sync_SyncID] Before Update ON [SYD_SyncLog] BEGIN SELECT RAISE(ROLLBACK, 'update on table SYD_SyncLog violates foreign key constraint fku_SYD_SyncLog_SyncID_SYC_Sync_SyncID') WHERE (SELECT SyncID FROM SYC_Sync WHERE SyncID = NEW.SyncID) IS NULL;  END;

