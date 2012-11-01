using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using Arshu.Core.Json;
using Arshu.Core.Json.RPC;

using Priya.Security;
using Priya.InfoList.Entity;
using Priya.InfoList.Model;
using Priya.InfoList.Views;

namespace Priya.InfoList
{
    [JsonRpcHelp("Json Data Handler")]
    public class JsonDataHandler : JsonRpcHandler
    {
        private static JsonDataService service = new JsonDataService();
        public JsonDataHandler()
        {
            if (service == null) service = new JsonDataService();
        }
    }

    /// <summary>
    /// Summary description for Data
    /// </summary>
    [JsonRpcHelp("Json Data Service")]
    public class JsonDataService : JsonRpcService
    {
        #region Save Data

        [JsonRpcMethod("SaveData")]
        [JsonRpcHelp("Save the Data (Only by Loged In User)")]
        public JsonObject SaveData(long parentDataId, long dataRefId, long dataRefTypeId, long dataTypeId, string dataValue, bool dataIsActive, string configToken, long dataId)
        {
            var retMessage = new JsonObject();

            string message;

            bool isPublic = DataView.isPublic(configToken);

            long retUserInfoID = DataCommon.SaveCndData(parentDataId, dataRefId, dataRefTypeId, dataTypeId, DecodeUrl(dataValue), dataIsActive, isPublic, dataId, out message);
            if ((retUserInfoID > 0) && (message.Trim().Length == 0))
            {
                retMessage.Put("message",
                                dataId == 0 ? "Successfully Added User Data" : "Successfully Updated User Data");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Saving User Data. Return User Data ID is 0";
                retMessage.Put("error", message);
            }

            return retMessage;
        }

        #endregion

        #region Delete Data

        [JsonRpcMethod("DeleteData")]
        [JsonRpcHelp("Delete the Data of the Logged In User (Only by Loged In User having Admin/Author Role)")]
        public JsonObject DeleteData(long dataId)
        {
            var retMessage = new JsonObject();

            string message;
            bool success = DataCommon.DeleteCndData(dataId, out message);
            if (success)
            {
                retMessage.Put("message", "Successfully Deleted Data");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Deleting Data";
                retMessage.Put("error", message);
            }

            return retMessage;
        }
        #endregion

        #region Get Data Save View

        [JsonRpcMethod("GetDataSaveView")]
        [JsonRpcHelp("Get Data Save View Html (Only by Loged In User)")]
        public JsonObject GetDataSaveView(long dataId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = DataView.GetSaveView(dataId, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);
            retMessage.Put("html", htmlText);

            return retMessage; ;
        }

        #endregion

        #region Get Data Append View

        [JsonRpcMethod("GetDataAppendView")]
        [JsonRpcHelp("Get Data Append View Html (Only by Loged In User)")]
        public JsonObject GetDataAppendView(long dataId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, string configToken)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = DataView.GetAppendView(dataId, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get Data List View

        [JsonRpcMethod("GetDataListView")]
        [JsonRpcHelp("Get Data List View Html (Only by Loged In User)")]
        public JsonObject GetDataListView(long pageNo, long itemsPerPage, int dataIndex, string templateSuffix, string configToken)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = DataView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken, 0, 0);
            if (string.IsNullOrEmpty(htmlText) == true) htmlText = "&nbsp;";
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get Data List View

        [JsonRpcMethod("GetDataListFilterView")]
        [JsonRpcHelp("Get Data List Filter View Html (Only by Loged In User)")]
        public JsonObject GetDataListFilterView(long pageNo, long itemsPerPage, int dataIndex, string templateSuffix, string configToken, long dataRefTypeId, long dataTypeId)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = DataView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken, dataRefTypeId, dataTypeId);
            if (string.IsNullOrEmpty(htmlText) == true) htmlText = "&nbsp;";
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

    }
}