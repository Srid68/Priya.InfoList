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
    [JsonRpcHelp("Json Info Section Handler")]
    public class JsonInfoSectionHandler : JsonRpcHandler
    {
        private static JsonInfoSectionService service = new JsonInfoSectionService();
        public JsonInfoSectionHandler()
        {
            if (service == null) service = new JsonInfoSectionService();
        }
    }

    /// <summary>
    /// Summary description for Data
    /// </summary>
    [JsonRpcHelp("Json Info Section Service")]
    public class JsonInfoSectionService : JsonRpcService
    {
        #region Save Info Section

        [JsonRpcMethod("SaveInfoSection")]
        [JsonRpcHelp("Save Info Section (Only by Loged in User having Admin Role)")]
        public JsonObject SaveInfoSection(string infoSectionName, string infoSectionDescription, bool isActive, bool isDeleted, long sequence, long infoSectionId, long infoPageId)
        {
            var retMessage = new JsonObject();

            string message;
            long retSectionID = DataInfoList.SaveLtdInfoSection(infoPageId, DecodeUrl(infoSectionName), DecodeUrl(infoSectionDescription), isActive, isDeleted, sequence, infoSectionId, out message);
            if ((retSectionID > 0) && (message.Trim().Length == 0))
            {
                retMessage.Put("message", infoSectionId == 0 ? "Successfully Added Info Section" : "Successfully Updated Info Section");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Saving Info Section. Return Info Section ID is 0";
                retMessage.Put("error", message);
            }

            return retMessage;
        }

        #endregion

        #region Delete Info Section

        [JsonRpcMethod("DeleteInfoSection")]
        [JsonRpcHelp("Delete InfoSection (Only by Loged in User having Admin Role)")]
        public JsonObject DeleteInfoSection(long infoSectionId)
        {
            var retMessage = new JsonObject();

            string message;
            bool success = DataInfoList.DeleteLtdInfoSection(infoSectionId, out message);
            if (success)
            {
                retMessage.Put("message", "Successfully Deleted Info Section");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Deleting Section";
                retMessage.Put("error", message);
            }

            return retMessage;
        }
        #endregion

        #region Get Info Section Save View

        [JsonRpcMethod("GetInfoSectionSaveView")]
        [JsonRpcHelp("Get Info Section Save View(Only by Loged in User having Admin Role)")]
        public JsonObject GetInfoSectionSaveView(long infoSectionId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, long infoPageId)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoSectionView.GetSaveDetailView(infoSectionId, pageNo, itemsPerPage, dataIndex, templateSuffix, infoPageId);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get Info Section List View

        [JsonRpcMethod("GetInfoSectionListView")]
        [JsonRpcHelp("Get Info  Section List View (Only by Loged in User having Admin Role)")]
        public JsonObject GetInfoSectionListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, bool asyncLoading, long infoPageId)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoSectionView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, infoPageId, asyncLoading);
            if (string.IsNullOrEmpty(htmlText) == true) htmlText = "&nbsp;";
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion
    }
}