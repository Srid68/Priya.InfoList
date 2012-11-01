
DROP TABLE IF EXISTS `SYS_Version` ;
CREATE TABLE `SYS_Version` (
	`VersionNo`		FLOAT NOT NULL,
	`VersionNoGUID`		CHAR(36) NOT NULL,
    PRIMARY KEY (`VersionNo`)

)
ENGINE = INNODB;
INSERT INTO `SYS_Version` (`VersionNo`, `VersionNoGUID`) VALUES (1, 'aa7b3943-e055-48ab-8857-392be40824fb');

-- Disable foreign key checks
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;


DROP TABLE IF EXISTS `CND_Data` ;
CREATE TABLE `CND_Data` (
	`DataID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`DataGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`ParentDataID`		BIGINT(19),
	`ParentDataGUID`		CHAR(36),
	`DataTypeID`		BIGINT(19) NOT NULL,
	`DataTypeGUID`		CHAR(36) NOT NULL,
	`DataValue`		VARCHAR(500) NOT NULL  ,
	`DataRefTypeID`		BIGINT(19) NOT NULL,
	`DataRefTypeGUID`		CHAR(36) NOT NULL,
	`DataRefID`		BIGINT(19),
	`IsActive`		TINYINT NOT NULL,
	`IsDeleted`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL
,
    CONSTRAINT `CND_Data_CND_Data` FOREIGN KEY `CND_Data_CND_Data`(`ParentDataID` , `ParentDataGUID`)
        REFERENCES `CND_Data`(`DataID` , `DataGUID`),
    CONSTRAINT `CND_Data_CNS_DataRefType` FOREIGN KEY `CND_Data_CNS_DataRefType`(`DataRefTypeID` , `DataRefTypeGUID`)
        REFERENCES `CNS_DataRefType`(`DataRefTypeID` , `DataRefTypeGUID`),
    CONSTRAINT `CND_Data_CNS_DataType` FOREIGN KEY `CND_Data_CNS_DataType`(`DataTypeID` , `DataTypeGUID`)
        REFERENCES `CNS_DataType`(`DataTypeID` , `DataTypeGUID`),
 PRIMARY KEY (`DataID`),
UNIQUE KEY `CND_Data_GK_CND_Data`(`DataGUID`),
UNIQUE KEY `CND_Data_GK_CND_Data_1`(`DataID`, `DataGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `CNS_DataRefType` ;
CREATE TABLE `CNS_DataRefType` (
	`DataRefTypeID`		BIGINT(19) NOT NULL,
	`DataRefTypeGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`DataRefType`		VARCHAR(30) NOT NULL  ,
	`IsDefault`		TINYINT NOT NULL,
	`IsSystem`		TINYINT NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL,
    PRIMARY KEY (`DataRefTypeID`)

,
UNIQUE KEY `CNS_DataRefType_GK_CNS_DataRefType`(`DataRefTypeGUID`),
UNIQUE KEY `CNS_DataRefType_GK_CNS_DataRefType_1`(`DataRefTypeID`, `DataRefTypeGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `CNS_DataType` ;
CREATE TABLE `CNS_DataType` (
	`DataTypeID`		BIGINT(19) NOT NULL,
	`DataTypeGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`DataType`		VARCHAR(30) NOT NULL  ,
	`IsDefault`		TINYINT NOT NULL,
	`IsSystem`		TINYINT NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL,
    PRIMARY KEY (`DataTypeID`)

,
UNIQUE KEY `CNS_DataType_GK_CNS_DataType`(`DataTypeGUID`),
UNIQUE KEY `CNS_DataType_GK_CNS_DataType_1`(`DataTypeID`, `DataTypeGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `LTD_InfoCategory` ;
CREATE TABLE `LTD_InfoCategory` (
	`InfoCategoryID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`InfoCategoryGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL,
	`InfoCategoryName`		VARCHAR(30) NOT NULL  ,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL,
	`IsDefault`		TINYINT NOT NULL,
	`IsSystem`		TINYINT NOT NULL DEFAULT 0
,
 PRIMARY KEY (`InfoCategoryID`),
UNIQUE KEY `LTD_InfoCategory_GK_LTD_InfoCategory`(`InfoCategoryGUID`),
UNIQUE KEY `LTD_InfoCategory_GK_LTD_InfoCategory_1`(`InfoCategoryID`, `InfoCategoryGUID`),
UNIQUE KEY `LTD_InfoCategory_UK_LTD_InfoCategory`(`InfoCategoryName`, `UserID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `LTD_InfoDetail` ;
CREATE TABLE `LTD_InfoDetail` (
	`InfoDetailID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`InfoDetailGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL,
	`InfoPageID`		BIGINT(19) NOT NULL,
	`InfoPageGUID`		CHAR(36) NOT NULL,
	`InfoSectionID`		BIGINT(19) NOT NULL,
	`InfoSectionGUID`		CHAR(36) NOT NULL,
	`InfoDetailName`		VARCHAR(250) NOT NULL  ,
	`InfoDetailDescription`		VARCHAR(4000) NOT NULL  ,
	`IsDeleted`		TINYINT NOT NULL
,
    CONSTRAINT `LTD_InfoDetail_LTD_InfoPage` FOREIGN KEY `LTD_InfoDetail_LTD_InfoPage`(`InfoPageID` , `InfoPageGUID`)
        REFERENCES `LTD_InfoPage`(`InfoPageID` , `InfoPageGUID`),
    CONSTRAINT `LTD_InfoDetail_LTD_InfoSection` FOREIGN KEY `LTD_InfoDetail_LTD_InfoSection`(`InfoSectionID` , `InfoSectionGUID`)
        REFERENCES `LTD_InfoSection`(`InfoSectionID` , `InfoSectionGUID`),
 PRIMARY KEY (`InfoDetailID`),
UNIQUE KEY `LTD_InfoDetail_GK_LTD_InfoDetail`(`InfoDetailGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `LTD_InfoPage` ;
CREATE TABLE `LTD_InfoPage` (
	`InfoPageID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`InfoPageGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL,
	`CreatedUserID`		BIGINT(19) NOT NULL,
	`CreatedUserGUID`		CHAR(36) NOT NULL,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL,
	`InfoPageName`		VARCHAR(250) NOT NULL  ,
	`InfoPageDescription`		VARCHAR(4000) NOT NULL  ,
	`InfoCategoryID`		BIGINT(19) NOT NULL,
	`InfoCategoryGUID`		CHAR(36) NOT NULL,
	`ExpiryDate`		DATETIME NOT NULL,
	`IsPublic`		TINYINT NOT NULL,
	`Commentable`		TINYINT NOT NULL DEFAULT 0,
	`CommentorRoleList`		VARCHAR(150)  ,
	`AsyncLoading`		TINYINT NOT NULL DEFAULT 0,
	`AccessGroupID`		BIGINT(19),
	`AccessGroupGUID`		CHAR(36),
	`IsDeleted`		TINYINT NOT NULL
,
    CONSTRAINT `LTD_InfoPage_LTD_InfoCategory` FOREIGN KEY `LTD_InfoPage_LTD_InfoCategory`(`InfoCategoryID` , `InfoCategoryGUID`)
        REFERENCES `LTD_InfoCategory`(`InfoCategoryID` , `InfoCategoryGUID`),
 PRIMARY KEY (`InfoPageID`),
UNIQUE KEY `LTD_InfoPage_GK_LTD_InfoPage_1`(`InfoPageID`, `InfoPageGUID`),
UNIQUE KEY `LTD_InfoPage_GK_LTD_Page`(`InfoPageGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `LTD_InfoSection` ;
CREATE TABLE `LTD_InfoSection` (
	`InfoSectionID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`InfoSectionGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL,
	`InfoPageID`		BIGINT(19) NOT NULL,
	`InfoPageGUID`		CHAR(36) NOT NULL,
	`InfoSectionName`		VARCHAR(250) NOT NULL  ,
	`InfoSectionDescription`		VARCHAR(4000) NOT NULL  ,
	`IsDeleted`		TINYINT NOT NULL
,
    CONSTRAINT `LTD_InfoSection_LTD_InfoPage` FOREIGN KEY `LTD_InfoSection_LTD_InfoPage`(`InfoPageID` , `InfoPageGUID`)
        REFERENCES `LTD_InfoPage`(`InfoPageID` , `InfoPageGUID`),
 PRIMARY KEY (`InfoSectionID`),
UNIQUE KEY `LTD_InfoSection_GK_LTD_InfoSection`(`InfoSectionGUID`),
UNIQUE KEY `LTD_InfoSection_GK_LTD_InfoSection_1`(`InfoSectionID`, `InfoSectionGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `LTD_Subscriber` ;
CREATE TABLE `LTD_Subscriber` (
	`SubscriberID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`SubscriberGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL,
	`SubscriberMessage`		VARCHAR(500) NOT NULL  ,
	`SubscriberEmail`		VARCHAR(100) NOT NULL  ,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`IsDeleted`		TINYINT NOT NULL
,
 PRIMARY KEY (`SubscriberID`))
ENGINE = INNODB;




INSERT INTO `CNS_DataRefType` (`DataRefTypeID`, `DataRefTypeGUID`, `RevisionNo`, `DataRefType`, `IsDefault`, `IsSystem`, `IsActive`, `Sequence`) VALUES (1, '095cbfec-51fb-4c23-909c-40798e10890c', 0, 'None', 1, 1, 1, 1);
INSERT INTO `CNS_DataRefType` (`DataRefTypeID`, `DataRefTypeGUID`, `RevisionNo`, `DataRefType`, `IsDefault`, `IsSystem`, `IsActive`, `Sequence`) VALUES (2, '58914899-f329-4793-aed3-c58d0f219b95', 2, 'InfoPage', 0, 1, 1, 2);


INSERT INTO `CNS_DataType` (`DataTypeID`, `DataTypeGUID`, `RevisionNo`, `DataType`, `IsDefault`, `IsSystem`, `IsActive`, `Sequence`) VALUES (1, '871650ca-5cb9-4503-8005-0f1a78f60bc2', 0, 'General Data', 1, 1, 1, 1);
INSERT INTO `CNS_DataType` (`DataTypeID`, `DataTypeGUID`, `RevisionNo`, `DataType`, `IsDefault`, `IsSystem`, `IsActive`, `Sequence`) VALUES (2, 'eca4d9f0-ab37-4dc9-9634-ae157c2978a6', 1, 'Comment Data', 0, 1, 1, 2);












-- Re-enable foreign key checks
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;-- Disable foreign key checks
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;


DROP TABLE IF EXISTS `ACD_Request` ;
CREATE TABLE `ACD_Request` (
	`RequestID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`RequestGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`RequestTypeID`		BIGINT(19) NOT NULL,
	`RequestTypeGUID`		CHAR(36) NOT NULL,
	`RequestMessage`		VARCHAR(4000) CHARACTER SET UTF8  NOT NULL  ,
	`ResponseMessage`		VARCHAR(4000) CHARACTER SET UTF8   ,
	`RequestUserID`		BIGINT(19) NOT NULL,
	`RequestUserGUID`		CHAR(36) NOT NULL,
	`ResponseDate`		DATETIME,
	`ResponseUserID`		BIGINT(19),
	`ResponseUserGUID`		CHAR(36),
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL,
	`IsResponded`		TINYINT NOT NULL,
	`IsDeleted`		TINYINT NOT NULL,
	`IsPublic`		TINYINT NOT NULL
,
    CONSTRAINT `ACD_Request_ACS_RequestType` FOREIGN KEY `ACD_Request_ACS_RequestType`(`RequestTypeID` , `RequestTypeGUID`)
        REFERENCES `ACS_RequestType`(`RequestTypeID` , `RequestTypeGUID`),
    CONSTRAINT `ACD_Request_SCD_User` FOREIGN KEY `ACD_Request_SCD_User`(`RequestUserID` , `RequestUserGUID`)
        REFERENCES `SCD_User`(`UserID` , `UserGUID`),
    CONSTRAINT `ACD_Request_SCD_User1` FOREIGN KEY `ACD_Request_SCD_User1`(`ResponseUserID` , `ResponseUserGUID`)
        REFERENCES `SCD_User`(`UserID` , `UserGUID`),
 PRIMARY KEY (`RequestID`),
UNIQUE KEY `ACD_Request_GK_ACD_Request`(`RequestGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `ACS_RequestType` ;
CREATE TABLE `ACS_RequestType` (
	`RequestTypeID`		BIGINT(19) NOT NULL,
	`RequestTypeGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`RequestType`		VARCHAR(50) NOT NULL  ,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL,
    PRIMARY KEY (`RequestTypeID`)

,
UNIQUE KEY `ACS_RequestType_GK_ACS_RequestType`(`RequestTypeGUID`),
UNIQUE KEY `ACS_RequestType_GK_ACS_RequestType_1`(`RequestTypeID`, `RequestTypeGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `SCD_Group` ;
CREATE TABLE `SCD_Group` (
	`GroupID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`GroupGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`GroupName`		VARCHAR(50) CHARACTER SET UTF8  NOT NULL  ,
	`IsDefault`		TINYINT NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL DEFAULT 0
,
    CONSTRAINT `SCD_Group_SCD_User` FOREIGN KEY `SCD_Group_SCD_User`(`UserID` , `UserGUID`)
        REFERENCES `SCD_User`(`UserID` , `UserGUID`),
 PRIMARY KEY (`GroupID`),
UNIQUE KEY `SCD_Group_GK_SCD_Group`(`GroupGUID`),
UNIQUE KEY `SCD_Group_GK_SCD_Group_1`(`GroupID`, `GroupGUID`),
UNIQUE KEY `SCD_Group_UK_SCD_Group`(`UserID`, `GroupName`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `SCD_GroupUser` ;
CREATE TABLE `SCD_GroupUser` (
	`GroupUserID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`GroupUserGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`GroupID`		BIGINT(19) NOT NULL,
	`GroupGUID`		CHAR(36) NOT NULL,
	`MemberID`		BIGINT(19) NOT NULL
,
    CONSTRAINT `SCD_GroupUser_SCD_Group` FOREIGN KEY `SCD_GroupUser_SCD_Group`(`GroupID` , `GroupGUID`)
        REFERENCES `SCD_Group`(`GroupID` , `GroupGUID`),
    CONSTRAINT `SCD_GroupUser_SCD_User` FOREIGN KEY `SCD_GroupUser_SCD_User`(`UserID` , `UserGUID`)
        REFERENCES `SCD_User`(`UserID` , `UserGUID`),
 PRIMARY KEY (`GroupUserID`),
UNIQUE KEY `SCD_GroupUser_GK_SCD_GroupUser`(`GroupUserID`, `GroupUserGUID`),
UNIQUE KEY `SCD_GroupUser_UK_SCD_GroupUser`(`GroupID`, `MemberID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `SCD_Invite` ;
CREATE TABLE `SCD_Invite` (
	`InviteID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`InviteGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`LastUserID`		BIGINT(19) NOT NULL,
	`LastUserGUID`		CHAR(36) NOT NULL,
	`InviteCode`		VARCHAR(50) NOT NULL  ,
	`InviteMaxNo`		BIGINT(19) NOT NULL,
	`InviteConsumed`		BIGINT(19) NOT NULL,
	`InviteRoleList`		VARCHAR(100) NOT NULL   DEFAULT 'GuestRole',
	`InviteCredit`		FLOAT NOT NULL DEFAULT 0,
	`IsAutoRegister`		TINYINT NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL DEFAULT 1
,
    CONSTRAINT `SCD_Invite_SCD_User` FOREIGN KEY `SCD_Invite_SCD_User`(`LastUserID` , `LastUserGUID`)
        REFERENCES `SCD_User`(`UserID` , `UserGUID`),
 PRIMARY KEY (`InviteID`),
UNIQUE KEY `SCD_Invite_GK_SCD_Invite`(`InviteGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `SCD_User` ;
CREATE TABLE `SCD_User` (
	`UserID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`UserLoginID`		VARCHAR(50) CHARACTER SET UTF8  NOT NULL  ,
	`UserEMail`		VARCHAR(50) CHARACTER SET UTF8  NOT NULL  ,
	`UserName`		VARCHAR(50) CHARACTER SET UTF8  NOT NULL  ,
	`DefaultRole`		VARCHAR(50) NOT NULL  ,
	`Telephone`		VARCHAR(100)  ,
	`GravatorUrl`		VARCHAR(100)  ,
	`ProfileImageUrl`		VARCHAR(100)  ,
	`EmailConfirmed`		TINYINT NOT NULL,
	`ResetPwdRequest`		TINYINT NOT NULL,
	`ResetPwdRequestDate`		DATETIME NOT NULL,
	`ResetPwdSent`		TINYINT NOT NULL,
	`ResetPwd`		VARCHAR(50)  ,
	`IsActive`		TINYINT NOT NULL DEFAULT 0,
	`Sequence`		BIGINT(19) NOT NULL DEFAULT 0,
	`UserAppKey`		VARCHAR(25) NOT NULL  
,
 PRIMARY KEY (`UserID`),
UNIQUE KEY `SCD_User_GK_SCD_User`(`UserGUID`),
UNIQUE KEY `SCD_User_GK_SCD_User_1`(`UserID`, `UserGUID`),
UNIQUE KEY `SCD_User_UK_SCD_User`(`UserEMail`),
UNIQUE KEY `SCD_User_UK_SCD_User_1`(`UserLoginID`),
UNIQUE KEY `SCD_User_UK_SCD_User_2`(`UserAppKey`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `SCD_UserKey` ;
CREATE TABLE `SCD_UserKey` (
	`UserKeyID`		BIGINT(19) NOT NULL,
	`UserKeyGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUsedDate`		DATETIME NOT NULL,
	`RandomKey`		VARCHAR(50) NOT NULL  ,
	`PublicKey`		VARCHAR(1024) NOT NULL  ,
	`IsValid`		TINYINT NOT NULL DEFAULT 0,
    PRIMARY KEY (`UserKeyID`)
,
    CONSTRAINT `SCD_UserKey_SCD_User` FOREIGN KEY `SCD_UserKey_SCD_User`(`UserID` , `UserGUID`)
        REFERENCES `SCD_User`(`UserID` , `UserGUID`)
,
UNIQUE KEY `SCD_UserKey_GK_SCD_UserKey`(`UserKeyGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `SYS_ActionRole` ;
CREATE TABLE `SYS_ActionRole` (
	`ActionRoleID`		BIGINT(19) NOT NULL,
	`ActionRoleGUID`		CHAR(36) NOT NULL,
	`ActionName`		VARCHAR(30) NOT NULL  ,
	`ActionRoleList`		VARCHAR(250) NOT NULL  ,
    PRIMARY KEY (`ActionRoleID`)

,
UNIQUE KEY `SYS_ActionRole_GK_SYS_ActionRole`(`ActionRoleGUID`),
UNIQUE KEY `SYS_ActionRole_UK_SYS_ActionRole`(`ActionName`))
ENGINE = INNODB;




INSERT INTO `ACS_RequestType` (`RequestTypeID`, `RequestTypeGUID`, `RevisionNo`, `RequestType`, `IsActive`, `Sequence`) VALUES (1, '456a9e86-ee72-4dc3-b22b-b5042560d160', 0, 'General Feedback', 1, 1);
INSERT INTO `ACS_RequestType` (`RequestTypeID`, `RequestTypeGUID`, `RevisionNo`, `RequestType`, `IsActive`, `Sequence`) VALUES (2, '70f03d0c-19b8-4c5c-a73b-15f2ec115045', 0, 'Account Related', 1, 1);
INSERT INTO `ACS_RequestType` (`RequestTypeID`, `RequestTypeGUID`, `RevisionNo`, `RequestType`, `IsActive`, `Sequence`) VALUES (3, '6358d0c9-fcd6-4a6d-94cc-cfab481e1ceb', 0, 'Change Request', 1, 1);
INSERT INTO `ACS_RequestType` (`RequestTypeID`, `RequestTypeGUID`, `RevisionNo`, `RequestType`, `IsActive`, `Sequence`) VALUES (99, '9ff60b89-50f4-4bfa-93d9-857d3f876b3c', 0, 'System Response', 0, 1);














-- Re-enable foreign key checks
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;-- Disable foreign key checks
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;


DROP TABLE IF EXISTS `SYC_Config` ;
CREATE TABLE `SYC_Config` (
	`ConfigID`		BIGINT(19) NOT NULL,
	`ConfigGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`UserID`		BIGINT(19),
	`UserGUID`		CHAR(36),
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`ConfigName`		VARCHAR(50) NOT NULL  ,
	`ConfigDescription`		VARCHAR(250) NOT NULL  ,
	`ConfigValue`		VARCHAR(250)  ,
	`ConfigAcceptValue`		VARCHAR(250)  ,
	`DataMaxLength`		BIGINT(19) NOT NULL,
	`DataMinLength`		BIGINT(19) NOT NULL,
	`IsUserConfig`		TINYINT NOT NULL,
	`IsActive`		TINYINT NOT NULL,
    PRIMARY KEY (`ConfigID`)

,
UNIQUE KEY `SYC_Config_GK_SYC_Config`(`ConfigGUID`),
UNIQUE KEY `SYC_Config_UK_SYC_Config`(`ConfigName`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `SYC_Sync` ;
CREATE TABLE `SYC_Sync` (
	`SyncID`		BIGINT(19) NOT NULL,
	`SyncGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`SyncName`		VARCHAR(30) NOT NULL  ,
	`SyncUrl`		VARCHAR(250) NOT NULL  ,
	`SyncUserID`		VARCHAR(50) NOT NULL  ,
	`SyncUserPwd`		VARCHAR(50) NOT NULL  ,
	`SyncLogCount`		BIGINT(19) NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL,
    PRIMARY KEY (`SyncID`)

,
UNIQUE KEY `SYC_Sync_GK_SYC_Sync`(`SyncGUID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `SYC_UserProfile` ;
CREATE TABLE `SYC_UserProfile` (
	`UserProfileID`		BIGINT(19)   AUTO_INCREMENT NOT NULL,
	`UserProfileGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`LastUpdateDate`		DATETIME NOT NULL,
	`UserProfileName`		VARCHAR(50) NOT NULL  ,
	`StartPage`		VARCHAR(50)  ,
	`JQueryMobileTheme`		VARCHAR(50) NOT NULL  ,
	`DefaultViewMode`		BIGINT(19) NOT NULL,
	`ItemsPerPage`		BIGINT(19) NOT NULL,
	`InnerItemsPerPage`		BIGINT(19) NOT NULL,
	`ItemsPerRow`		BIGINT(19) NOT NULL,
	`InfoItemsPerPage`		BIGINT(19) NOT NULL,
	`DetailItemsPerPage`		BIGINT(19) NOT NULL,
	`DefaultIconWidth`		BIGINT(19) NOT NULL,
	`DefaultIconHeight`		BIGINT(19) NOT NULL,
	`DefaultImageWidth`		BIGINT(19) NOT NULL,
	`DefaultImageHeight`		BIGINT(19) NOT NULL,
	`DefaultAvatarSize`		BIGINT(19) NOT NULL,
	`IsDefault`		TINYINT NOT NULL,
	`IsActive`		TINYINT NOT NULL,
	`Sequence`		BIGINT(19) NOT NULL
,
 PRIMARY KEY (`UserProfileID`),
UNIQUE KEY `SYC_UserProfile_GK_SYC_UserProfile`(`UserProfileGUID`),
UNIQUE KEY `SYC_UserProfile_UK_SYC_UserProfile`(`UserProfileName`, `UserID`))
ENGINE = INNODB;


DROP TABLE IF EXISTS `SYD_SyncLog` ;
CREATE TABLE `SYD_SyncLog` (
	`SyncLogID`		BIGINT(19) NOT NULL,
	`SyncLogGUID`		CHAR(36) NOT NULL,
	`RevisionNo`		BIGINT(19) NOT NULL DEFAULT 0,
	`UserID`		BIGINT(19) NOT NULL,
	`UserGUID`		CHAR(36) NOT NULL,
	`CreatedDate`		DATETIME NOT NULL,
	`SyncID`		BIGINT(19) NOT NULL,
	`SyncLog`		VARCHAR(500) NOT NULL  ,
	`Sequence`		BIGINT(19) NOT NULL,
    PRIMARY KEY (`SyncLogID`)
,
    CONSTRAINT `SYD_SyncLog_SYC_Sync` FOREIGN KEY `SYD_SyncLog_SYC_Sync`(`SyncID`)
        REFERENCES `SYC_Sync`(`SyncID`)
)
ENGINE = INNODB;


INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (1, '1e324b7d-4b8a-4b4e-9023-737f36f0fd22', 566, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'DefaultView', 'DefaultView', 'DOCUMENTVIEW', NULL, 250, 0, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (3, 'bcc52c24-e537-4573-bfa8-a4a1b69a787e', 567, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ForceLogin', 'Force the Login', 'False', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (4, 'bd788965-137a-4465-9bd2-6fe3737930bf', 570, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowAccountInfo', 'Show Account Info', 'False', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (5, 'b6b83e0d-164a-423e-9f32-a3e7c13178b9', 574, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowLogInfo', 'Show Log Info', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (6, '2c2c36bc-f834-4251-8720-6918312759ac', 571, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowAPILink', 'Show the API Link', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (7, 'f5d8558a-3abb-465a-b4c7-dcb1a9bf0dcc', 572, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowSettingLink', 'Show the Setting Link', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (8, 'd95927da-13c5-46ba-953b-f4bdf34736fe', 568, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowAutoRegisterLink', 'Show the Auto Register Link', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (9, '2fb958dc-5a41-435b-99e0-5f4c6f999232', 569, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowRegisterLink', 'Show the Register Link', 'False', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (10, '04748118-feae-45b9-a4b6-029413a11174', 573, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'ShowTrackingLink', 'Show the Tracking Link', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (20, 'b84ab6dd-0657-4467-b91d-e05f924ac2fb', 575, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'DateFormat', 'Date Format', 'dd/MMM/yyyy', NULL, 16, 1, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (21, '8ffbf316-b07b-45db-933f-c0a409890839', 576, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-19 21:12:50', 'NumberFormat', 'Number Format', '{0:N3}', NULL, 16, 1, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (50, '12af3a80-9d88-4d30-a438-a290de775f44', 40, 2, 'ff97cb77-77c8-4800-8f3d-3ca7713f78d0', '2012-01-25 06:53:36', '2012-08-29 19:34:05', 'ProductionTestMode', 'Production Test Mode', 'False', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (51, 'edba3118-d712-44e2-a90b-0e092ce4da4e', 327, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'DevelopmentTestMode', 'Development Test Mode', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (52, '70f7c08c-7dd9-4950-ad93-3f501b3609c8', 328, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceVersion', 'Resource Version', '1', NULL, 2, 1, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (53, '7bc1f5c4-d70e-49f9-84fe-c9ca707b695a', 329, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceOptimize', 'Optimize Resources', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (54, '11780585-cddf-4559-a0c7-41f900a84eee', 330, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceCompress', 'Compress Resources', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (55, 'cbe5fa62-3649-4353-8fb1-2bf868b31e3e', 331, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceExpires', 'Expire Resources', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (56, '19663127-9b9b-41e4-b709-8cef861a79c6', 332, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceCache', 'Cache Resources', 'True', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (57, 'd507faf9-9bb8-4a22-aee8-a960f21997a5', 333, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceBase64', 'Base64 Image Resources', 'False', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (58, '0636e0da-c444-43a4-a269-3bca2a0e2281', 334, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceSplit', 'Split Static Resources', 'False', NULL, 5, 4, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (59, '5b282fc7-736f-4f4f-930f-413419ee8de4', 335, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-01-25 06:53:36', '2012-09-15 16:47:17', 'ResourceSplitMax', 'Max Split Static Resources', '3', NULL, 2, 1, 0, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (100, '806c78f9-cb9f-476a-8678-70688f2c7919', 531, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'StartPage', 'StartPage', NULL, NULL, 250, 0, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (101, '7e299edc-34f1-48dc-981a-a0df83b2f5e1', 534, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'ItemsPerPage', 'The Default Number of Items to Display in a Page', '2', NULL, 2, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (102, 'b0acd884-dca2-46c2-b665-7960fbd42c54', 538, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'InnerItemsPerPage', 'The Default Number of Items to Display Inside a Content', '4', NULL, 2, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (103, '24fb823a-d0b6-47ac-9079-0c07cb794188', 536, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'ItemsPerRow', 'The Default Number of Items to Display in a Row', '4', NULL, 2, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (104, 'aa0982cc-bda8-4f9f-9e5a-4308118805ca', 537, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'InfoItemsPerPage', 'The Default Number of Info Items to Display in a Page', '4', NULL, 2, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (105, '339fab2c-9419-4127-9404-358f739fc96c', 0, NULL, NULL, '2012-01-25 06:53:36', '1900-01-01 00:00:06', 'DetailItemsPerPage', 'The Default Number of Detail Items to Display in a Page', '4', NULL, 2, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (106, 'f6f2c402-ecff-470c-b3e0-8a877eba4578', 532, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'JQueryMobileTheme', 'JQuery Theme ', 'Default', NULL, 30, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (107, '18fe43ee-c4cf-4975-aa66-05656c5b5097', 533, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaultViewMode', 'Default View Mode (List, Thumb)', '0', NULL, 1, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (108, '4855c7f5-0c7b-4ca3-a19d-6d2d664eb8a1', 539, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaultIconWidth', 'Default Width of Icon', '70', NULL, 3, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (109, 'cdee4f64-748f-4ba3-9a14-8722c8d5a489', 540, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaultIconHeight', 'Default Height of Icon', '70', NULL, 3, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (110, '9cfd625f-7df5-4ac5-aa77-cebcb0ffe737', 541, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaultImageWidth', 'Default Width of Image', '150', NULL, 3, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (111, 'be51340c-9b35-45a6-ab58-f6ad99ebdd04', 542, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaulImageHeight', 'Default Height of Image', '150', NULL, 3, 1, 1, 1);
INSERT INTO `SYC_Config` (`ConfigID`, `ConfigGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `ConfigName`, `ConfigDescription`, `ConfigValue`, `ConfigAcceptValue`, `DataMaxLength`, `DataMinLength`, `IsUserConfig`, `IsActive`) VALUES (112, 'e9bd06b1-b817-4b8f-9c36-41efb2d93a1d', 543, 10087, 'fda18837-ff81-4bb3-b331-d8b5977a003a', '2012-01-25 06:53:36', '2012-10-11 08:12:33', 'DefaulAvatarSize', 'Default Size of Square Image', '80', NULL, 3, 1, 1, 1);




INSERT INTO `SYC_UserProfile` (`UserProfileID`, `UserProfileGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `UserProfileName`, `StartPage`, `JQueryMobileTheme`, `DefaultViewMode`, `ItemsPerPage`, `InnerItemsPerPage`, `ItemsPerRow`, `InfoItemsPerPage`, `DetailItemsPerPage`, `DefaultIconWidth`, `DefaultIconHeight`, `DefaultImageWidth`, `DefaultImageHeight`, `DefaultAvatarSize`, `IsDefault`, `IsActive`, `Sequence`) VALUES (1, '1b67e8c9-703e-426b-89d1-9176c276f5f5', 0, 2, 'ff97cb77-77c8-4800-8f3d-3ca7713f78d0', '2012-08-29 19:34:15', '2012-08-29 19:34:15', 'Default', NULL, 'Default', 0, 2, 4, 4, 4, 4, 70, 70, 150, 150, 80, 0, 0, 1);
INSERT INTO `SYC_UserProfile` (`UserProfileID`, `UserProfileGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `UserProfileName`, `StartPage`, `JQueryMobileTheme`, `DefaultViewMode`, `ItemsPerPage`, `InnerItemsPerPage`, `ItemsPerRow`, `InfoItemsPerPage`, `DetailItemsPerPage`, `DefaultIconWidth`, `DefaultIconHeight`, `DefaultImageWidth`, `DefaultImageHeight`, `DefaultAvatarSize`, `IsDefault`, `IsActive`, `Sequence`) VALUES (2, '9cf212af-ab5b-4b3e-a3a6-6bc4c3f56cec', 0, 10040, '09aba903-1dbf-41c0-80ed-a70d181f20d3', '2012-09-15 10:40:52', '2012-09-17 10:11:35', 'Default', NULL, 'Default', 1, 2, 4, 4, 4, 4, 70, 70, 150, 150, 80, 0, 0, 1);
INSERT INTO `SYC_UserProfile` (`UserProfileID`, `UserProfileGUID`, `RevisionNo`, `UserID`, `UserGUID`, `CreatedDate`, `LastUpdateDate`, `UserProfileName`, `StartPage`, `JQueryMobileTheme`, `DefaultViewMode`, `ItemsPerPage`, `InnerItemsPerPage`, `ItemsPerRow`, `InfoItemsPerPage`, `DetailItemsPerPage`, `DefaultIconWidth`, `DefaultIconHeight`, `DefaultImageWidth`, `DefaultImageHeight`, `DefaultAvatarSize`, `IsDefault`, `IsActive`, `Sequence`) VALUES (3, 'd4859c02-5c34-4897-a6fa-afb712bc95ab', 0, 10039, '4f3c72fa-00b2-4d7c-ba71-2eb226009158', '2012-09-15 16:24:58', '2012-09-15 16:53:49', 'Default', NULL, 'Default', 0, 2, 4, 4, 4, 4, 70, 70, 150, 150, 80, 0, 0, 1);




-- Re-enable foreign key checks
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;