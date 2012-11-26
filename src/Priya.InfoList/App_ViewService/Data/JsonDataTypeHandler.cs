using System;
using System.Globalization;

using Arshu.Core.Json;
using Arshu.Core.Json.RPC;

using Priya.Security;
using Priya.InfoList.Entity;
using Priya.InfoList.Model;
using Priya.InfoList.Views;

namespace Priya.InfoList
{
    [JsonRpcHelp("Json Data Type Handler")]
    public class JsonDataTypeHandler : JsonRpcHandler
    {
        private static JsonDataTypeService service = new JsonDataTypeService();
        public JsonDataTypeHandler()
        {
            if (service == null) service = new JsonDataTypeService();
        }
    }

    /// <summary>
    /// Summary description for Data
    /// </summary>
    [JsonRpcHelp("Json Data Type Service")]
    public class JsonDataTypeService : JsonRpcService
    {
        #region Save DataType

        [JsonRpcMethod("SaveDataType")]
        [JsonRpcHelp("Save Data Type (Only by Loged In User having Admin/Author Role)")]
        public JsonObject SaveDataType(string dataTypeName, bool dataTypeIsDefault, bool dataTypeIsActive, long dataTypeId)
        {
            var retMessage = new JsonObject();

            string message;
            long retDataTypeID = DataCommon.SaveCnsDataType(DecodeUrl(dataTypeName), dataTypeIsDefault, dataTypeIsActive, dataTypeId, out message);
            if ((retDataTypeID > 0) && (message.Trim().Length == 0))
            {
                retMessage.Put("message", dataTypeId == 0 ? "Successfully Added DataType" : "Successfully Updated DataType");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Saving DataType. Return DataType ID is 0";
                retMessage.Put("error", message);
            }

            return retMessage;
        }

        #endregion

        #region Delete DataType

        [JsonRpcMethod("DeleteDataType")]
        [JsonRpcHelp("Delete Data Type (Only by Loged In User having Admin Role)")]
        public JsonObject DeleteDataType(long dataTypeId)
        {
            var retMessage = new JsonObject();

            string message;
            bool success = DataCommon.DeleteCnsDataType(dataTypeId, out message);
            if (success)
            {
                retMessage.Put("message", "Successfully Deleted DataType");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Deleting DataType";
                retMessage.Put("error", message);
            }

            return retMessage;
        }
        #endregion

        #region Get DataType Save View

        [JsonRpcMethod("GetDataTypeSaveView")]
        [JsonRpcHelp("Get Data Type Save View (Only by Loged In User having Admin Role)")]
        public JsonObject GetDataTypeSaveView(long dataTypeId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = DataTypeView.GetSaveDetailView(dataTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get DataType List View

        [JsonRpcMethod("GetDataTypeListView")]
        [JsonRpcHelp("Get Data Type List View (Only by Loged In User having Admin Role)")]
        public JsonObject GetDataTypeListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = DataTypeView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix);
            if (string.IsNullOrEmpty(htmlText) == true) htmlText = "&nbsp;";
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion
    }
}