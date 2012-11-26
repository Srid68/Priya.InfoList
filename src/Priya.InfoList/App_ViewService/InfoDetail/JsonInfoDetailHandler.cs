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
    [JsonRpcHelp("Json Info Detail Service")]
    public class JsonInfoDetailHandler : JsonRpcHandler
    {
        private static JsonInfoDetailService service = new JsonInfoDetailService();
        public JsonInfoDetailHandler()
        {
            if (service == null) service = new JsonInfoDetailService();
        }
    }

    /// <summary>
    /// Summary description for Data
    /// </summary>
    [JsonRpcHelp("Json Info Detail Service")]
    public class JsonInfoDetailService : JsonRpcService
    {
        #region Save Info Detail

        [JsonRpcMethod("SaveInfoDetail")]
        [JsonRpcHelp("Save Info Detail (Only by Loged in User having Admin Roole)")]
        public JsonObject SaveInfoDetail(string infoDetailName, string infoDetailDescription, bool isActive, bool isDeleted, long sequence, long infoDetailId, long infoSectionId)
        {
            var retMessage = new JsonObject();

            string message;
            long retDetailID = DataInfoList.SaveLtdInfoDetail(infoSectionId, DecodeUrl(infoDetailName), DecodeUrl(infoDetailDescription), isActive, isDeleted, sequence, infoDetailId, out message);
            if ((retDetailID > 0) && (message.Trim().Length == 0))
            {
                retMessage.Put("message", infoSectionId == 0 ? "Successfully Added Info Detail" : "Successfully Updated Info Detail");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Saving Info Detail. Return Info Detail ID is 0";
                retMessage.Put("error", message);
            }

            return retMessage;
        }

        #endregion

        #region Delete Info Detail

        [JsonRpcMethod("DeleteInfoDetail")]
        [JsonRpcHelp("Delete Info Detail (Only by Loged in User having Admin Role)")]
        public JsonObject DeleteInfoDetail(long infoDetailId)
        {
            var retMessage = new JsonObject();

            string message;
            bool success = DataInfoList.DeleteLtdInfoDetail(infoDetailId, out message);
            if (success)
            {
                retMessage.Put("message", "Successfully Deleted Info Detail");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Deleting Info Detail";
                retMessage.Put("error", message);
            }

            return retMessage;
        }
        #endregion

        #region Get Info Detail Save View

        [JsonRpcMethod("GetInfoDetailSaveView")]
        [JsonRpcHelp("Get Info Detail Save View (Only by Loged in User having Admin Role)")]
        public JsonObject GetInfoDetailSaveView(long infoDetailId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long infoSectionId)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoDetailView.GetSaveDetailView(infoDetailId, pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get Info Detail List View

        [JsonRpcMethod("GetInfoDetailListView")]
        [JsonRpcHelp("Get Info Detail List View ")]
        public JsonObject GetInfoDetailListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long infoSectionId)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoDetailView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId);
            if (string.IsNullOrEmpty(htmlText) == true) htmlText = "&nbsp;";
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion
    }
}