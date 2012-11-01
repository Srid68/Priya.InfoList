using System;
using System.Web;
using Arshu.Core.Web;

using Priya.InfoList.Views;

namespace Priya.InfoList
{
    /// <summary>
    /// Summary description for DefaultHandler
    /// </summary>
    public class DataHandler : IHttpHandler
    {       
        public void ProcessRequest(HttpContext context)
        {
            #region Get Index HtmlPath

            string htmlText = DataView.GetPageView();

            #endregion         
     
            context.Response.ContentType = "text/html";
            //context.Response.Write(htmlText);
            OptimizerHandler.SendResponseCompressed(context, htmlText, "");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}