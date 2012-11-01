

using System;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;

using Arshu.Core.Basic.Log;
using Arshu.Core.Basic.Compress;
using Arshu.Core.Common;
using Arshu.Core.Http;
using Arshu.Core.IO;

namespace Priya.InfoList.Views
{

#region Resource Class

	public class Resource : IResource
	{
	
		public const int ResourceCount = 2;
		
        public const string PngAppbackground = "appbackground.png";
		private const string PngAppbackgroundName = "appbackground.png";
		
        public const string PngApplogo = "applogo.png";
		private const string PngApplogoName = "applogo.png";

        public bool IsValid(string resourceVirtualPath, bool throwException, out string retMessage)
		{
			bool ret =false;
            string message = "File/Resource [" + resourceVirtualPath + "] Not Found";
	
            bool validResourceVirtualPath = false;
            resourceVirtualPath = (resourceVirtualPath.StartsWith("/"))
                                      ? resourceVirtualPath
                                      : "/" + resourceVirtualPath;
            string resourceName = IOManager.GetFileName(resourceVirtualPath);
			switch(resourceName)
            {
		        case PngAppbackgroundName:
                	validResourceVirtualPath = true;
                    break;
		        case PngApplogoName:
                	validResourceVirtualPath = true;
                    break;
		
			}
			if (validResourceVirtualPath)
			{
				ret = true;
				message = "";				
				
			}
			
            if ((ret == false) && (throwException)) throw new Exception(message);
			
			retMessage = message;
			return ret;
		}
		
		public byte[] GetResource(string resourceVirtualPath, bool validate, bool throwException, out string retMessage)
		{
			byte[] resourceBytes = {};
			string message ="";
            resourceVirtualPath = (resourceVirtualPath.StartsWith("/"))
                                      ? resourceVirtualPath
                                      : "/" + resourceVirtualPath;			
            if ((validate ==false) || (IsValid(resourceVirtualPath, throwException, out message)==true))
			{					
			    Assembly resourceAssembly = typeof (Resource).Assembly;
                string resourceName = IOManager.GetFileName(resourceVirtualPath);	
				
				switch(resourceName)
                {
		            case PngAppbackgroundName:
			
						resourceBytes = ResourceUtil.GetBytesFromFile(PngAppbackground, HttpBaseHandler.ResourceCache);;
                        break;
		            case PngApplogoName:
			
						resourceBytes = ResourceUtil.GetBytesFromFile(PngApplogo, HttpBaseHandler.ResourceCache);;
                        break;
							
				}
		
                if ((resourceBytes == null) || (resourceBytes.Length == 0))
                {
                    LogManager.Log(LogLevel.Error, "Priya.InfoList.Resource-GetResource", "Error:Resource File [" + resourceVirtualPath + "] is Zero Bytes in Priya.InfoList.Resource");
                }	
			}
			
			retMessage = message;
			return resourceBytes;
		}
	}

#endregion

}

