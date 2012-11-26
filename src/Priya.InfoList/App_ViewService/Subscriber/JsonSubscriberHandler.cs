using System;
using System.Globalization;

using Arshu.Core.Json;
using Arshu.Core.Json.RPC;

using Priya.Security.Utils;
using Priya.InfoList.Entity;
using Priya.InfoList.Model;
using Priya.InfoList.Views;

namespace Priya.InfoList
{
    [JsonRpcHelp("Json Subscriber Handler")]
    public class JsonSubscriberHandler : JsonRpcHandler
    {
        private static JsonSubscriberService service = new JsonSubscriberService();
        public JsonSubscriberHandler()
        {
            if (service == null) service = new JsonSubscriberService();
        }
    }

    /// <summary>
    /// Summary description for Data
    /// </summary>
    [JsonRpcHelp("Json Subscriber Service")]
    public class JsonSubscriberService : JsonRpcService
    {
        #region Save Subscriber

        [JsonRpcMethod("SaveSubscriber")]
        [JsonRpcHelp("Save Subscriber")]
        public JsonObject SaveSubscriber(string subscriberEmail, string subscriberMessage, bool subscriberIsDeleted, long subscriberId)
        {
            var retMessage = new JsonObject();

            bool valid = UtilsSecurity.IsValidEmail(DecodeUrl(subscriberEmail));
            if (valid == true)
            {
                string message;
                long retSubscriberID = DataInfoList.SaveLtdSubscriber(DecodeUrl(subscriberEmail), DecodeUrl(subscriberMessage), subscriberIsDeleted, subscriberId, out message);
                if ((retSubscriberID > 0) && (message.Trim().Length == 0))
                {
                    retMessage.Put("message", subscriberId == 0 ? "Successfully Added Subscriber" : "Successfully Updated Subscriber");
                }
                else
                {
                    if (message.Trim().Length == 0) message = "Error in Saving Subscriber. Return Subscriber ID is 0";
                    retMessage.Put("error", message);
                }
            }
            else
            {
                retMessage.Put("error", "Please enter a Valid Email to Subscribe");
            }

            return retMessage;
        }

        #endregion

        #region Delete Subscriber

        [JsonRpcMethod("DeleteSubscriber")]
        [JsonRpcHelp("Delete Subscriber (Only by Loged in User having Admin Role)")]
        public JsonObject DeleteSubscriber(long subscriberId)
        {
            var retMessage = new JsonObject();

            string message;
            bool success = DataInfoList.DeleteLtdSubscriber(subscriberId, out message);
            if (success)
            {
                retMessage.Put("message", "Successfully Deleted Subscriber");
            }
            else
            {
                if (message.Trim().Length == 0) message = "Error in Deleting Subscriber";
                retMessage.Put("error", message);
            }

            return retMessage;
        }
        #endregion

        #region Get Subscriber Save View

        [JsonRpcMethod("GetSubscriberSaveView")]
        public JsonObject GetSubscriberSaveView(long subscriberId, long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = SubscriberView.GetSaveDetailView(subscriberId, pageNo, itemsPerPage, dataIndex, templateSuffix);
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion

        #region Get Subscriber List View

        [JsonRpcMethod("GetSubscriberListView")]
        public JsonObject GetSubscriberListView(long pageNo, long itemsPerPage, long dataIndex, string templateSuffix)
        {
            JsonObject retMessage = new JsonObject();

            string htmlText = SubscriberView.GetListAllItemView(pageNo, itemsPerPage, dataIndex, templateSuffix);
            if (string.IsNullOrEmpty(htmlText) == true) htmlText = "&nbsp;";
            retMessage.Put("html", htmlText);

            return retMessage;
        }

        #endregion
    }
}