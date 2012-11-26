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
    [JsonRpcHelp("Json Info Page Handler")]
    public class JsonInfoPageHandler : JsonRpcHandler
    {
        private static JsonInfoPageService service = new JsonInfoPageService();
        public JsonInfoPageHandler()
        {
            if (service == null) service = new JsonInfoPageService();
        }
    }

    /// <summary>
    /// Summary description for Data
    /// </summary>
    [JsonRpcHelp("Json Info Page Service")]
    public class JsonInfoPageService : JsonRpcService
    {
        #region Save Info Page

        [JsonRpcMethod("SaveInfoPage")]
        [JsonRpcHelp("Save Info Page (Only by Loged in User having Admin/Author Role)")]
        public JsonObject SaveInfoPage(string infoPageName, string infoPageDescription, long infoPageCategoryId, long accessGroupId, bool asyncLoading, bool isActive, DateTime expiryDate, bool commentable, string commentorRoleList, bool isPublic, bool isCommon, bool isDeleted, long sequence, long infoPageId)
        {
            var retMessage = new JsonObject();

            string message;
            long retID = DataInfoList.SaveLtdInfoPage(DecodeUrl(infoPageName), DecodeUrl(infoPageDescription), infoPageCategoryId, accessGroupId, asyncLoading, isActive, expiryDate, commentable, commentorRoleList, isPublic, isCommon, isDeleted, sequence, infoPageId, out message);
            if ((retID > 0) && (message.Trim().Length == 0))
            {
                retMessage.Put("message", infoPageId == 0 ? "Successfully Added Info Page" : "Successfully Updated Info Page");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Saving Info Page. Return Info Page ID is 0";
                retMessage.Put("error", message);
            }

            return retMessage;
        }

        #endregion

        #region Delete Info Page

        [JsonRpcMethod("DeleteInfoPage")]
        [JsonRpcHelp("Delete Info Page (Only by Loged in User having Admin Role)")]
        public JsonObject DeleteInfoPage(long infoPageId)
        {
            var retMessage = new JsonObject();

            string message;
            bool success = DataInfoList.DeleteLtdInfoPage(infoPageId, out message);
            if (success)
            {
                retMessage.Put("message", "Successfully Deleted Info Page");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Deleting Info Page";
                retMessage.Put("error", message);
            }

            return retMessage;
        }
        #endregion

        #region Get Info Page Save View

        [JsonRpcMethod("GetInfoPageSaveView")]
        [JsonRpcHelp("Get Info Page Save View(Only by Loged in User)")]
        public JsonObject GetInfoPageSaveView(long infoPageId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoPageView.GetSaveDetailView(infoPageId, pageNo, itemsPerPage, dataIndex, templateSuffix);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get Info Page List View

        [JsonRpcMethod("GetInfoPageListView", Idempotent = true)]
        [JsonRpcHelp("Get Info Page List View(Only by Loged in User)")]
        public JsonObject GetInfoPageListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, bool asyncLoading)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoPageView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, 0, "", false, 0, true);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get Info Page Filter List View

        [JsonRpcMethod("GetInfoPageFilterListView")]
        [JsonRpcHelp("Get Info Page Filter List View(Only by Loged in User)")]
        public JsonObject GetInfoPageFilterListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix, bool asyncLoading, long filterInfoCategoryId, string filterInfoPage, bool filterInfoPagePublic, long filterCreatedUserId)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoPageView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, filterInfoCategoryId, filterInfoPage, filterInfoPagePublic, filterCreatedUserId, false);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get Info Page Category Option List

        [JsonRpcMethod("GetInfoPageCategoryOptionList")]
        [JsonRpcHelp("Get Info Page Category Option List View (Only by Loged in User)")]
        public JsonObject GetInfoPageCategoryOptionList(long infoPageId, string templateSuffix)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoPageView.GetInfoPageCategoryOptionList(infoPageId, templateSuffix);
            if (string.IsNullOrEmpty(htmlText) == true) htmlText = "&nbsp;";
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion
    }
}