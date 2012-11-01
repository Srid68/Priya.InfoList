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
    [JsonRpcHelp("Json Data Reference Type Handler")]
    public class JsonDataRefTypeHandler : JsonRpcHandler
    {
        private static JsonDataRefTypeService service = new JsonDataRefTypeService();
        public JsonDataRefTypeHandler()
        {
            if (service == null) service = new JsonDataRefTypeService();
        }
    }

    /// <summary>
    /// Summary description for Data
    /// </summary>
    [JsonRpcHelp("Json Data Reference Type Service")]
    public class JsonDataRefTypeService : JsonRpcService
    {
        #region Save DataRefType

        [JsonRpcMethod("SaveDataRefType")]
        [JsonRpcHelp("Save Data Reference Type (Only by Loged In User having Admin/Author Role)")]
        public JsonObject SaveDataRefType(string dataRefType, bool dataRefTypeIsDefault, bool dataRefTypeIsActive, long dataRefTypeId)
        {
            var retMessage = new JsonObject();

            string message;
            long retDataRefTypeID = DataCommon.SaveCnsDataRefType(DecodeUrl(dataRefType), dataRefTypeIsDefault, dataRefTypeIsActive, dataRefTypeId, out message);
            if ((retDataRefTypeID > 0) && (message.Trim().Length == 0))
            {
                retMessage.Put("message", dataRefTypeId == 0 ? "Successfully Added DataRefType" : "Successfully Updated DataRefType");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Saving DataRefType. Return DataRefType ID is 0";
                retMessage.Put("error", message);
            }

            return retMessage;
        }

        #endregion

        #region Delete DataRefType

        [JsonRpcMethod("DeleteDataRefType")]
        [JsonRpcHelp("Delete Data Reference Type (Only by Loged In User having Admin Role)")]
        public JsonObject DeleteDataRefType(long dataRefTypeId)
        {
            var retMessage = new JsonObject();

            string message;
            bool success = DataCommon.DeleteCnsDataRefType(dataRefTypeId, out message);
            if (success)
            {
                retMessage.Put("message", "Successfully Deleted DataRefType");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Deleting DataRefType";
                retMessage.Put("error", message);
            }

            return retMessage;
        }
        #endregion

        #region Get DataRefType Save View

        [JsonRpcMethod("GetDataRefTypeSaveView")]
        [JsonRpcHelp("Get Data Reference Type Save View (Only by Loged In User having Admin/Author Role)")]
        public JsonObject GetDataRefTypeSaveView(long dataRefTypeId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            var retMessage = new JsonObject();

            string htmlText = DataRefTypeView.GetSaveDetailView(dataRefTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get DataRefType List View

        [JsonRpcMethod("GetDataRefTypeListView")]
        [JsonRpcHelp("Get Data Reference Type List View (Only by Loged In User having Admin/Author Role)")]
        public JsonObject GetDataRefTypeListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            var retMessage = new JsonObject();

            string htmlText = DataRefTypeView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix);
            if (string.IsNullOrEmpty(htmlText) == true) htmlText = "&nbsp;";
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion
    }
}