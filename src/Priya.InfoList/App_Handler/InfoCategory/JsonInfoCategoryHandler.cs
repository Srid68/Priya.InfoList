using System.Globalization;

using Arshu.Core.Json;
using Arshu.Core.Json.RPC;

using Priya.Security;
using Priya.InfoList.Entity;
using Priya.InfoList.Model;
using Priya.InfoList.Views;

namespace Priya.InfoList
{
    [JsonRpcHelp("Json Info Category Handler")]
    public class JsonInfoCategoryHandler : JsonRpcHandler
    {
        private static JsonInfoCategoryService service = new JsonInfoCategoryService();
        public JsonInfoCategoryHandler()
        {
            if (service == null) service = new JsonInfoCategoryService();
        }
    }

    /// <summary>
    /// Summary description for Data
    /// </summary>
    [JsonRpcHelp("Json Info Category Service")]
    public class JsonInfoCategoryService : JsonRpcService
    {
        #region Save Info Category

        [JsonRpcMethod("SaveInfoCategory")]
        public JsonObject SaveInfoCategory(string infoCategoryName, bool isDefault, bool isActive, long infoCategoryId)
        {
            var retMessage = new JsonObject();

            string message;
            long retCategoryID = DataInfoList.SaveLtdInfoCategory(infoCategoryName, isDefault, isActive, infoCategoryId, out message);
            if ((retCategoryID > 0) && (message.Trim().Length == 0))
            {
                retMessage.Put("message",
                                infoCategoryId == 0 ? "Successfully Added Info Category" : "Successfully Updated Info Category");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Saving Info Category. Return Info Category ID is 0";
                retMessage.Put("error", message);
            }

            return retMessage;
        }

        #endregion

        #region Delete Info Category

        [JsonRpcMethod("DeleteInfoCategory")]
        public JsonObject DeleteInfoCategory(long infoCategoryId)
        {
            var retMessage = new JsonObject();

            string message;
            bool success = DataInfoList.DeleteLtdInfoCategory(infoCategoryId, out message);
            if (success)
            {
                retMessage.Put("message", "Successfully Deleted Info Category");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Deleting Info Category";
                retMessage.Put("error", message);
            }

            return retMessage;
        }
        #endregion

        #region Get Info Category Save View

        [JsonRpcMethod("GetInfoCategorySaveView")]
        public JsonObject GetInfoCategorySaveView(long infoCategoryId, long pageNo, long itemsPerPage, int dataIndex, string templateSuffix, bool hideDisplay)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoCategoryView.GetSaveDetailView(infoCategoryId, pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get Info Category List View

        [JsonRpcMethod("GetInfoCategoryListView")]
        public JsonObject GetInfoCategoryListView(long pageNo, long itemsPerPage, int dataIndex, string templateSuffix, bool hideDisplay)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = InfoCategoryView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay);
            if (string.IsNullOrEmpty(htmlText) == true) htmlText = "&nbsp;";
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion
    }
}