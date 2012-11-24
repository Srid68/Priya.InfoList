

using System;
using System.Text;
using System.Collections.Generic;

using Arshu.Core.Basic.Compress;
using Arshu.Core.Common;
using Arshu.Core.Http;
using Arshu.Core.IO;

namespace Priya.InfoList.Views
{


	#region Html Template Classes	

    public class TemplateData	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/Data.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_172="GTxkaXYgaWQ9ImRhdGFTYXZlU2VjdGlvbiIgQBASLXJvbGU9ImNvbGxhcHNpYmxlIoAXAGOAEQZlZD0ie3tTIDYKRXhwYW5kfX0iPiAgAAs8aDM+TWFuYWdlIEQgVwM8L2gzYBeAKwdEZXRhaWx9fSAOA0xpc3TADgU8L2Rpdj4=";

		#endregion		

        #region PlaceHolder Properties

		public string SaveExpand { get; set; }	
		public string SaveDetail { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_172);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_172);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveExpand}}", string.IsNullOrEmpty(SaveExpand)==false ? SaveExpand : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataList	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataList.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlData/DataListScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlData/DataListScript.min.js";										
		private const string TemplateSource_96="G3t7TGlzdFNjcmlwdH19IDxkaXYgaWQ9ImRhdGFAGQZWaWV3e3tEIA0KSW5kZXh9fSI+e3tAGA1EZXRhaWx9fTwvZGl2Pg==";
		private const string MinScriptSource_1404="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiBmaWx0HWVyRGF0YUxpc3QoYixkLGssbSxpKXt2YXIgaj1kO0AHH2c9Iml0ZW1zUGVyUGFnZSIraztpZigkKCIjIitnKS5sCmVuZ3RoPjApe2o9oBQFKyIgb3B0IGESOnNlbGVjdGVkIikudmFsKCl9diBYAGZgJwBm4AB8CFJlZlR5cGVTZUArACDgDjlgigBh4AY5AFTgHTYYaD0neyJwYWdlTm8iIDogJytiO2grPScsIOAF2GAaAWo7oBoAZCEWBEluZGV4gBcBazugFyEKCXBsYXRlU3VmZmlgHAgiJyttKyciJzugIQpjb25maWdUb2tlbkA7IB4AaeADHkBYoPIBSWRAIAMnK2Y7oDpAG0DUwBgBYTsgGAIifSJg/QZlPSJHZXREIJdBrgBGYT8EVmlldyJgHQJjPXfhBuoBKytgGABsQP0HanNvbnJwYyIgjhEyLjAiLCJtZXRob2QiOiInK2UgnQAsIR8NcmFtcyI6JytoKycsImkgGx8nK2MrIn0iOyQuYWpheCh7dHlwZToiUE9TVCIsY29udANlbnRUIYEJOiJhcHBsaWNhdCHyHy9qc29uOyBjaGFyc2V0PXV0Zi04Iix1cmw6Ii8qIUBTDmVydmljZVVybEAqLyIsZCDMATpsYAZA9gI6ImogPQkiLHN1Y2Nlc3M6wqkDKG4pe0KKA289bjsiex9uLmhhc093blByb3BlcnR5KCJlcnJvciIpPT1mYWxzZQEpe+AMJARkIikpeyBCBi5kfWVsc2XgDSQGcmVzdWx0IqApgA0BfX0gTwBv4Ah0BGh0bWwiICgDJCgiI0DIQZUEIitrKS5AGEAvIB8BKTvgCB0AbCNrBHZpZXcoIGsAZiBgAmgiKeAJJBJ0cmlnZ2VyKCJjcmVhdGUiKX19YLMFc2hvd0VyIOwhBABlQAcKLm5hbWUrIjoiK26gEABtITkQYWdlKX19fSk7cmV0dXJuIGZBFgB9wU4AIKB4AEQhfUC0CihhLGMsaSxrLGcpYWYDaD1jO0FuAWU94wUgAitpOyEIACQg7gMiK2Up4wL5AWg9oBTjGPnjBogCYTtm4w+IAWg7oBoAZCCl4wKIAWk7oBfjDIgAayLLASc7oCECY29u4waIAGfAHgAiItZA4gBk4wRTAFbjAE0BYj3jDk0AakDCAWpz4xFNAGQgYuMDTQNmKycso00CYisiIHbjVk0AauMZTQJsKXtA+wNtPWw7Ic8AbOII2OMKTeAJJAhkIikpe209bC7jAU3gCSQi3qNNQCmADQB9Q00AbeAIdEMtACIgUkJJQfJCmAMiK2kpY0ZAL+MKTSAd4xlNICQAdOMYTQFsLuMGTQBsoBDjEE0BfTs=";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ListScript { get; set; }	
		public string DataIndex { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1404);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1404);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1404);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_96);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_96);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListScript}}", string.IsNullOrEmpty(ListScript)==false ? ListScript : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataListDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataListDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_204="BTxkaXY+ICAAEjx1bCBpZD0iZGF0YUxpc3R7e0QgCQlJbmRleH19IiBkIAwHLXJvbGU9ImwgHQR2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgFiIoAWCWRpdmlkZXJ0aGVALAFlImCFQAABe3tAfwVJdGVtfX1ADwo8L3VsPjwvZGl2Pg==";

		#endregion		

        #region PlaceHolder Properties

		public string DataIndex { get; set; }	
		public string ListItem { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_204);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_204);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{ListItem}}", string.IsNullOrEmpty(ListItem)==false ? ListItem : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataListDetailChildEmpty	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataListDetailChildEmpty.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_440="BDxsaT4gIAAfPHNwYW4gc3R5bGU9InBvc2l0aW9uOmFic29sdXRlO3QOb3A6NXB4O3JpZ2h0OjEwIAoAImA3QAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXH3Jlc2hEYXRhTGlzdCh7e1BhZ2VOb319LCB7e0l0ZW1zAlBlckATAH1gEUAoBUluZGV4fSAOEyd7e1RlbXBsYXRlU3VmZml4fX0nQDYRQ29uZmlnVG9rZW59fSk7IiBkIF4NLXJvbGU9ImJ1dHRvbiKAEgRpbmxpbiAUBHRydWUigBIDbWluaeAEEARpY29uPSCuYKcAIoAkQBMhBAs9Im5vdGV4dCI+UmVgHgI8L2Fg9QE8L0EuYAoBTm8gxwBEIH8LQXBwZW5kTmFtZX19oBIGVmFsdWVOYUARDShzKSBGb3VuZDwvbGk+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string ConfigToken { get; set; }	
		public string DataAppendName { get; set; }	
		public string DataValueName { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_440);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_440);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{ConfigToken}}", string.IsNullOrEmpty(ConfigToken)==false ? ConfigToken : "");
			template = template.Replace("{{DataAppendName}}", string.IsNullOrEmpty(DataAppendName)==false ? DataAppendName : "");
			template = template.Replace("{{DataValueName}}", string.IsNullOrEmpty(DataValueName)==false ? DataValueName : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataListDetailEmpty	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataListDetailEmpty.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_616="BDxsaT4gIAAfPHNwYW4gc3R5bGU9InBvc2l0aW9uOmFic29sdXRlO3QOb3A6NXB4O3JpZ2h0OjEwIAoAImA3QAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXH3Jlc2hEYXRhTGlzdCh7e1BhZ2VOb319LCB7e0l0ZW1zAlBlckATAH1gEUAoBUluZGV4fSAOEyd7e1RlbXBsYXRlU3VmZml4fX0nQDYRQ29uZmlnVG9rZW59fSk7IiBkIF4NLXJvbGU9ImJ1dHRvbiKAEgRpbmxpbiAUBHRydWUigBIDbWluaeAEEARpY29uPSCuYKcAIoAkQBMhBAs9Im5vdGV4dCI+UmVgHgI8L2HgAPUIe3sjQWRkQWN0ISgBfX1BB0AAIQsAb+EGAgNnZXREIJ8KU2F2ZVZpZXcoMCwgO0Dx4TQFIRsCQ29u4QEGIRhA5UEN4QCMgMwJYWpheD0iZmFsc8EDBHRoZW1lISoAIoAg4Qo44QklAG7hCEkBaWNBOARwbHVzIoBHAGkgEEE1ACeBNQQnPkFkZOEFMQEvQeEFMQE8L0J2YVIBTm8hFUEnElZhbHVlTmFtZX19KHMpIEZvdW4gRQJsaT4=";

		#endregion		

        #region List Section Properties

		public class AddAction	
		{
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
			public string ConfigToken { get; set; }	
		}		
		public List<AddAction> AddActionList { get; set; }	

        #endregion

        #region Bool Section Properties


        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string ConfigToken { get; set; }	
		public string DataValueName { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_616);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_616);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#AddAction}}";
			sectionEndTag = "{{/AddAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (AddActionList !=null))
			{
				foreach (var addaction in AddActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(addaction.PageNo)==false ? addaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(addaction.ItemsPerPage)==false ? addaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(addaction.DataIndex)==false ? addaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(addaction.TemplateSuffix)==false ? addaction.TemplateSuffix : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ConfigToken}}", string.IsNullOrEmpty(addaction.ConfigToken)==false ? addaction.ConfigToken : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(AddActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{ConfigToken}}", string.IsNullOrEmpty(ConfigToken)==false ? ConfigToken : "");
			template = template.Replace("{{DataValueName}}", string.IsNullOrEmpty(DataValueName)==false ? DataValueName : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataListDetailFilter	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataListDetailFilter.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_836="GDxsaSBpZD0iZGF0YUxpc3RGaWx0ZXJ7e0QgDwtJbmRleH19IiB7eyNAD+ABHx9IaWRkZW59fXN0eWxlPSJkaXNwbGF5Om5vbmU7Int7L+ANLQE+ICAAAzxkaXZgCEAABDx1bCBkIGkDLXJvbCBHAGwggAR2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgFiIoAWCWRpdmlkZXJ0aGVALAFlIuAAbkAAADwg8OAEEEAADDxsYWJlbCBmb3I9ImZhAQBEIJUMUmVmVHlwZVNlbGVjdOAFRcAAQCYFIFJlZiBUICgBPC9gRuAJXQFzZUBBBSBuYW1lPeAQXwAgQYbgD3yA2ApuYXRpdmUtbWVudSBHAmFscyEXEW9uY2hhbmdlPSJyZXR1cm4gZuAAv0GvFih7e1BhZ2VOb319LCB7e0l0ZW1zUGVyQBMAfWARQMGh6BUsICd7e1RlbXBsYXRlU3VmZml4fX0nYBUKQ29uZmlnVG9rZW4gEgAp4Q0MAnt7I0BNoTYASSBqAX19wSjgBwALPG9wdGlvbiB2YWx1ILsBe3vgAjkAVkATAH3iAXegUIGHAWVkInZhTwRlZD0ic6AJIEBidOAIKgE+e+ADWQdUZXh0fX08L4B84QioQADgBUjgDcQCPC9zYHfgDEADPC9saeANGABs4AkX4g14AFQiTIDt4Q1oQSsAIOITccAA4g954AJg4gZ24AMZAGQjA+Kec0C24Q2rwADiDHBANuIHbUATgXwAZeIUauAFJwA+gmdAFOIhZEAt4A214iFhAjwvdYRxAjwvZCVZBDwvbGk+";

		#endregion		

        #region List Section Properties

		public class DataRefTypeItem	
		{
			public string DataRefTypeValue { get; set; }	
			public string DataRefTypeText { get; set; }	
			public bool DataRefTypeSelected { get; set; }	
		}		
		public List<DataRefTypeItem> DataRefTypeItemList { get; set; }	

		public class DataTypeItem	
		{
			public string DataTypeValue { get; set; }	
			public string DataTypeText { get; set; }	
			public bool DataTypeSelected { get; set; }	
		}		
		public List<DataTypeItem> DataTypeItemList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool DataListFilterHidden { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string DataIndex { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string ConfigToken { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_836);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_836);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#DataRefTypeItem}}";
			sectionEndTag = "{{/DataRefTypeItem}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (DataRefTypeItemList !=null))
			{
				foreach (var datareftypeitem in DataRefTypeItemList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolDataRefTypeItemSection(datareftypeitem , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataRefTypeValue}}", string.IsNullOrEmpty(datareftypeitem.DataRefTypeValue)==false ? datareftypeitem.DataRefTypeValue : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataRefTypeText}}", string.IsNullOrEmpty(datareftypeitem.DataRefTypeText)==false ? datareftypeitem.DataRefTypeText : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(DataRefTypeItemList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#DataTypeItem}}";
			sectionEndTag = "{{/DataTypeItem}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (DataTypeItemList !=null))
			{
				foreach (var datatypeitem in DataTypeItemList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolDataTypeItemSection(datatypeitem , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataTypeValue}}", string.IsNullOrEmpty(datatypeitem.DataTypeValue)==false ? datatypeitem.DataTypeValue : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataTypeText}}", string.IsNullOrEmpty(datatypeitem.DataTypeText)==false ? datatypeitem.DataTypeText : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(DataTypeItemList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataListFilterHidden}}";
			sectionEndTag = "{{/DataListFilterHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DataListFilterHidden ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{ConfigToken}}", string.IsNullOrEmpty(ConfigToken)==false ? ConfigToken : "");
			return template;
		}

		protected string ProcessListDataRefTypeItemSection(DataRefTypeItem datareftypeitem, string template)
		{

			return template;
		}

		protected string ProcessListDataTypeItemSection(DataTypeItem datatypeitem, string template)
		{

			return template;
		}

		protected string ProcessBoolDataRefTypeItemSection(DataRefTypeItem datareftypeitem, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataRefTypeSelected}}";
			sectionEndTag = "{{/DataRefTypeSelected}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, datareftypeitem.DataRefTypeSelected ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessBoolDataTypeItemSection(DataTypeItem datatypeitem, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataTypeSelected}}";
			sectionEndTag = "{{/DataTypeSelected}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, datatypeitem.DataTypeSelected ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataListDetailItem	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataListDetailItem.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_792="BDxsaT4gIAAfPGRpdiBzdHlsZT0icG9zaXRpb246YWJzb2x1dGU7cmkLZ2h0OjVweDt0b3A6QAcAImA1QAAJe3sjRWRpdEFjdCAyAX19QBJAABE8YnV0dG9uIGRhdGEtaW5saW4gWQR0cnVlIoASCWFqYXg9ImZhbHPAEQNtaW5p4AQiA2ljb24giwg9J25vdGV4dCeAOABpIBUCPSJlIHIVIiBvbmNsaWNrPSJyZXR1cm4gZ2V0RCBvGVNhdmVWaWV3KHt7SWR9fSwge3tQYWdlTm99YAsHSXRlbXNQZXJAEwB9YBFANAVJbmRleH0gDhMne3tUZW1wbGF0ZVN1ZmZpeH19J2AVCkNvbmZpZ1Rva2VuYBJAzQUpOyIgPntgUgBFIJIHTmFtZX19PC+A/OECIwAvQB3hByMIe3sjQXBwZW5k4AcYADyAP6DwAG7hRzwFY29tbWVu4RE/gJQAVuFgQQE+e2FAAEFhCwFOYeEPQoAf4QMrBDwvZGl2YWUDe3sjSSHWDEhlYWRlclZpc2libGWCjAE8cGAgQAAIPHN0cm9uZz57YfAFVHlwZU5hgGwAc4AYACBARgJzSW5AYwF2ZSHIA3NwYW7DFhBjb2xvcjpyZWQ7Zm9udC13ZWMUB2JvbGQ7Ij4owDQCKTwvQDQEPnt7L0ngAklgfgAvgIoDe3svSeAQq4CbB1ZhbHVlfX08oDEEPGJyIC9gxiDNCnt7Q3JlYXRlZEJ5IFciJQB7wBAixgBl4AE0QAAgGgRoaWxkSSBuICugAAQ8L2xpPg==";

		#endregion		

        #region List Section Properties

		public class EditAction	
		{
			public string Id { get; set; }	
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
			public string ConfigToken { get; set; }	
			public string ItemEditName { get; set; }	
		}		
		public List<EditAction> EditActionList { get; set; }	

		public class AppendAction	
		{
			public string Id { get; set; }	
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
			public string ConfigToken { get; set; }	
			public string ItemAppendName { get; set; }	
		}		
		public List<AppendAction> AppendActionList { get; set; }	

		public class ItemHeaderVisible	
		{
			public string DataTypeName { get; set; }	
			public bool IsInActive { get; set; }	
		}		
		public List<ItemHeaderVisible> ItemHeaderVisibleList { get; set; }	

        #endregion

        #region Bool Section Properties


        #endregion		

        #region PlaceHolder Properties

		public string DataValue { get; set; }	
		public string CreatedBy { get; set; }	
		public string CreatedDate { get; set; }	
		public string ChildItem { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_792);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_792);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#EditAction}}";
			sectionEndTag = "{{/EditAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (EditActionList !=null))
			{
				foreach (var editaction in EditActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(editaction.Id)==false ? editaction.Id : "");
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(editaction.PageNo)==false ? editaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(editaction.ItemsPerPage)==false ? editaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(editaction.DataIndex)==false ? editaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(editaction.TemplateSuffix)==false ? editaction.TemplateSuffix : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ConfigToken}}", string.IsNullOrEmpty(editaction.ConfigToken)==false ? editaction.ConfigToken : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemEditName}}", string.IsNullOrEmpty(editaction.ItemEditName)==false ? editaction.ItemEditName : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(EditActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#AppendAction}}";
			sectionEndTag = "{{/AppendAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (AppendActionList !=null))
			{
				foreach (var appendaction in AppendActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(appendaction.Id)==false ? appendaction.Id : "");
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(appendaction.PageNo)==false ? appendaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(appendaction.ItemsPerPage)==false ? appendaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(appendaction.DataIndex)==false ? appendaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(appendaction.TemplateSuffix)==false ? appendaction.TemplateSuffix : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ConfigToken}}", string.IsNullOrEmpty(appendaction.ConfigToken)==false ? appendaction.ConfigToken : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemAppendName}}", string.IsNullOrEmpty(appendaction.ItemAppendName)==false ? appendaction.ItemAppendName : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(AppendActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#ItemHeaderVisible}}";
			sectionEndTag = "{{/ItemHeaderVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (ItemHeaderVisibleList !=null))
			{
				foreach (var itemheadervisible in ItemHeaderVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolItemHeaderVisibleSection(itemheadervisible , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataTypeName}}", string.IsNullOrEmpty(itemheadervisible.DataTypeName)==false ? itemheadervisible.DataTypeName : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(ItemHeaderVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{DataValue}}", string.IsNullOrEmpty(DataValue)==false ? DataValue : "");
			template = template.Replace("{{CreatedBy}}", string.IsNullOrEmpty(CreatedBy)==false ? CreatedBy : "");
			template = template.Replace("{{CreatedDate}}", string.IsNullOrEmpty(CreatedDate)==false ? CreatedDate : "");
			template = template.Replace("{{ChildItem}}", string.IsNullOrEmpty(ChildItem)==false ? ChildItem : "");
			return template;
		}

		protected string ProcessListItemHeaderVisibleSection(ItemHeaderVisible itemheadervisible, string template)
		{

			return template;
		}

		protected string ProcessBoolItemHeaderVisibleSection(ItemHeaderVisible itemheadervisible, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsInActive}}";
			sectionEndTag = "{{/IsInActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, itemheadervisible.IsInActive ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataSave	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataSave.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlData/DataSaveScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlData/DataSaveScript.min.js";										
		private const string TemplateSource_148="HHt7U2F2ZVNjcmlwdH19IDxkaXYgaWQ9ImRhdGFTIBkIVmlldyIge3sjwAwfSGlkZGVufX1zdHlsZT0iZGlzcGxheTpub25lOyJ7ey8AU6A0wCcBPiAgAAJ7e1MgFg1EZXRhaWx9fTwvZGl2Pg==";
		private const string MinScriptSource_2288="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaERhdGFGb3JtKGIsZCxjLGEsZSl7aWYodHlwZW9mAyhnZXRAIAtTYXZlVmlldyk9PSLAQAIiKXvgBh0DKDAsYsBECCxmYWxzZSl9aeAASuACbwRMaXN0KeAFSgBy4AGNQB0AKOABjQV9JCgiI2QgpUAXA1NlY3QgvBQiKS50cmlnZ2VyKCJleHBhbmQiKX3AmAAg4AeWF2QsYSxjLGgsayxnLGkpe3ZhciBmPSd7IkBUFklkIiA6ICcrZDtmKz0nLCAicGFnZU5vgBQBYTugFAtpdGVtc1BlclBhZ2WAGgFjO6AaYEQDbmRleIAXAWg7oBcgMQlwbGF0ZVN1ZmZpYBwIIicraysnIic7oCEKY29uZmlnVG9rZW5AOyAeAGfAHgMifSI7QKYEZT0iR2XhBIEAImAXAmI9d+EG2QErK2AYAGpA1wdqc29ucnBjIiBTETIuMCIsIm1ldGhvZCI6IicrZSBiACwg5A1yYW1zIjonK2YrJywiaSAbECcrYisifSI7JC5hamF4KHt0IgAQOiJQT1NUIixjb250ZW50VHlAEgdhcHBsaWNhdCGGHy9qc29uOyBjaGFyc2V0PXV0Zi04Iix1cmw6Ii8qIUBTDWVydmljZVVybEAqLyIsQSkBOmpgBgBUYEkAaiA9CSIsc3VjY2VzczrBvgMobCl7QPsDbT1sOyI9G2wuaGFzT3duUHJvcGVydHkoImVycm9yIik9PWZiYkKt4AkkDmQiKSl7bT1sLmR9ZWxzZeANJCL/A3VsdCKgKYANAH1CsgBt4Ah0BGh0bWwiICgBJCiCjABToxEiiUAaQDEgIQEpOyCxAHQhOQlvZihiaW5kQWRkIFUFc01hcCk94wQ54AUcAigpfeAKVwB0wuEHY3JlYXRlIilAXAhpPT10cnVlKXvgCjAJc2hvdygic2xvdyArASQo4AGoAFPjEjVhHeADLEPnQNUCaWRlwEwCfX19YCYAcyBgAUVyIX0hcABlQAcKLm5hbWUrIjoiK2ygEABtIcoDYWdlKSAyCSk7cmV0dXJuIGZBpwB9wd/DngZBcHBlbmRWJFXjvKDgAc4BIjtCvuP/okYF4w19AmQiKeP/ogB24xSi40R9BHNhdmVEJ00NKGcsZCxmLG4scSxtKXtCsQFjPURZADtACghwPSQoIiNwYXIl9UAsAUlkJBEEdmFsKClEgxFwLmxlbmd0aD09MCl7cD0wfXYnUQBrYDIAZCBZA1JlZlQl3wJTZWwkdgMgb3B0JikCOnNlQA0BZWTgAEdAZgBo4AMzAUlk4AAbBGlmKGgu4AJjAWg9gGMAYuAALwBU4B1gAGHgADAEVmFsdWXgA2ABYS7gAmACc2hvpNgHIlBsZWFzZSAg+AdlciB0aGUgRCDSASBWgD4BO2OGdGDxAGqAC0CtACQhLQBkICcHSXNBY3RpdmUgaBFpcygiOmNoZWNrZWQiKSl7aj1BXUZcAGPFwiDmAHSGKwBzIH8EUHJvZ3ImLAAp5gUpAHPgAhoBKClgeABsR+QAcOEDnGjCAnA7bIjCQIkhhABJoBcBaDvgBRcAVCE7wBsCazts4AEzAFTgAhgBYjvgAhiBPCBkKKMOZW5jb2RlVVJJQ29tcG9uIScCKGEpKFQBJzvgAjDhAAcgMwMnK2o7oBoCY29u6AbxAG3gCDmgtAFnOyAzAiJ9ImInAWU96A7uA2k9IlMjPABEIXoAImApAG9BGgFqc+gR/wBpIHAALEE8AGGI/wNsKycsqP8myAB96Fj/AG/oGf8Ccil7QxwDcz1yOyIhAHLoCIroCv/gCSQBZCIicwNzPXIu6AH/4AkkKJuo/0ApgA0AfUKiAHPgCHSn1wAiQFUikQFTdWm0ICigGAApJ+zsBU/kAV2IYyDL4AlWoMsAKWBUiFoBcy5oWgEpfeQI9gFyLmAZ6AF0AHKgEMCDQLqjUQBoKL4AUOMNUQBo4AIaBigpfX19KX3oDagEZGVsZXRiXAAozEIDaixnKUDJA2Q+MClABwB04yu/7A18ACIifEH36BA4BWU9IkRlbKCRACJgKwBp4hfu63fuAGniGe4Cayl7QPYDbD1rOyIiAGviCHmiIusD7uAJJAhkIikpe2w9ay7iAe7gCSSC4AAioCmADQB9QjMAbOAIdKJRACIgKwBz4gLuAWwuoBjiCe4AYW5iQh/iAO7gCVagy+ID7gBsizgBKX3iCO4Bay7iBu4Aa6AQwINAuqKA4jHuAX07";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveViewHidden { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string SaveScript { get; set; }	
		public string SaveDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2288);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2288);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2288);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_148);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_148);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveViewHidden}}";
			sectionEndTag = "{{/SaveViewHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveViewHidden ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveScript}}", string.IsNullOrEmpty(SaveScript)==false ? SaveScript : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataSaveAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataSaveAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_604="FzxsaSBzdHlsZT0iY29sb3I6Z3JheSI+ICAAEzxoNj57e0FkZFRpdGxlfX08L2g2YBgVIDxwIGNsYXNzPSJ1aS1saS1hc2lkZYA0QAAfPGEgb25jbGljaz0icmV0dXJuIGdldERhdGFTYXZlVmkPZXcoMCwge3tQYWdlTm99fUALB0l0ZW1zUGVyQBMAfWARQC8FSW5kZXh9IA4TJ3t7VGVtcGxhdGVTdWZmaXh9fSdgFQpDb25maWdUb2tlbmASEnRydWUpOyIgaHJlZj0iIyIgIGQgdwwtYWpheD0iZmFsc2UigBEIdGhlbWU9ImIigA4Ccm9sQA0FdXR0b24igBIFbWluaT0iQFCgMgVpbmxpbmXgBRIMY29uPSJjb21tZW50IoA3AGkgExJwb3M9J25vdGV4dCc+QWRkPC9hgTogABB7eyNGaWx0ZXJBY3Rpb259fSATYADhAzcDJCgnI0C5BExpc3RGYC0Ae+EDEQ8nKS50b2dnbGUoJ3Nsb3cn4ATyQDdghQI9ImZgOgAigBLhEAQBYSKAIARtaW5pPeAW8eEKKABp4AfwAEZgdeAF8wEvRmAU4APzCDwvcD48L2xpPg==";

		#endregion		

        #region List Section Properties

		public class FilterAction	
		{
			public string DataIndex { get; set; }	
		}		
		public List<FilterAction> FilterActionList { get; set; }	

        #endregion

        #region Bool Section Properties


        #endregion		

        #region PlaceHolder Properties

		public string AddTitle { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string ConfigToken { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_604);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_604);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#FilterAction}}";
			sectionEndTag = "{{/FilterAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (FilterActionList !=null))
			{
				foreach (var filteraction in FilterActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(filteraction.DataIndex)==false ? filteraction.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(FilterActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{AddTitle}}", string.IsNullOrEmpty(AddTitle)==false ? AddTitle : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{ConfigToken}}", string.IsNullOrEmpty(ConfigToken)==false ? ConfigToken : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataSaveDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataSaveDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1804="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGOAAAA08bGkge3sjUGFyZW50RCB+DUlkSGlkZGVufX1zdHlsIGARZGlzcGxheTpub25lOyJ7ey9Q4AorACDgAFQNPGxhYmVsIGZvcj0icGHgAVXgBXYAUGBDACBAcAQgSUQ8L2A04ABDwAAKPGlucHV0IHR5cGUg+gdleHQiIG5hbSAL4ARYAyBpZD3gBhEFdmFsdWU9IKwAUGBnQGYISWR9fSIge3sj4APqCURpc2FibGV9fWSACAFkPUDuQBIDZCJ7e+AE6eAAKhggcGxhY2Vob2xkZXI9IiYjMTg3O0VudGVy4AXUAmQiL+AEzwM8L2xp4AQQIAAAPIF8QJ4FUmVmSURI4RZ54Ago4ABQ4QN1AGQhxUBO4QZyAEQgFgYgUmVmIElE4ShvAGQgO4BSYWzgAg7hAGkARKAiAUR9IWbhECJAJaCEYYxAD0BYAEThAF7hDYngCSfhJ10EVHlwZUjhHV/gAyriDdgAZIDsQCgFU2VsZWN04gXdAEQgHmDlAFQgdAY8c3BhbiBzg0wOY29sb3I6UmVkOyI+KjwvQBoBPjziBv0CPHNlQFKi6gBkIEshIeACauEDguADFkApES1uYXRpdmUtbWVudT0iZmFscyQuAXt7wWdALgBEguDiD/MAROABb+AAKeIAZkAA4AVNA0l0ZW0hJeAGAAY8b3B0aW9u4ggdQH8AVkObAH0iJOAFSIFHAWVkJHdg/gRlZD0ic6AJAiJ7e+ADl+ABKgI+e3vgAq8AVCQhA319PC+AfOAGtOADQOAFtCGGYGfgBDDjCK4APOMBqwBUIdXiG0rgAyfgAFzjB6lAJIDQ4gpD4iw/wADiCUfgAmviAETgAxMAZCH74hNBQCviABTjEajgBCbgAPhAAKHyQCDhBYPAAOIMOEAu4gc1QBOBNgBl4hQy4AUnAD6CL0AU4hksQCXgBaXiFSkiIcDsFDxwIGNsYXNzPSJ1aS1saS1hc2lkZeEF3wZ7e1JldmlzIyUBTm/jAUACPC9wwEMgTwBh5wBJQZkAVkM44AdEAEQhsmAYBk5hbWV9fTxEUeQkbAB0IzQDYXJlYeQCbmBPACLiACGgDhZtYXhsZW5ndGg9IjUwMCIgcm93cz0iMSAJpNsOaGVpZ2h0OjE1MHB4OyI+gWpgPAR9fTwvdKBn4AHeAC+nEAd7eyNJc0FjdCS3BFZpc2liR48g/iD/4QBP4AMjgBzAAOgEIgdjaGVja2JveMgmAGQhHMBaAHthKgVJbmRleH0kfAFpZCfq4BEeAGNASQBlICNgUiSBACDmAW8Ce3svwFTgAYsBe3sgxAFJboDG4AEWQADgTaLgBo/gCXriBz7As+EFCAM+SXMggLQAPOYChuECm+AA6+EEmwF7eyDuBURlbGV0ZeAEFwA84QezgCMBQ2ghmABlI6hBD8AA4RoXgD0BZHvhD7vgDR/hGLyANuAIlAF7e+AA1OBdqeEGw4CW4AF64QnBgCEAZOEFD0HCAERhigBk6QPY4QXDgDnhE6sLU2hvd1VzZXJJbmZv4AKD5ANQAGYsYwEtd4PmASBsQ+0FZXI7IGNvSNMBIHJo1BVQbGVhc2UgbG9naW4gdG8gU2F2ZTwvQETjAPgDe3svU+AMa8AABHt7QWRkQpQBb27gAYgFe3tFZGl04AkVBUFwcGVuZOADF2D4BDwvdWw+";

		#endregion		

        #region List Section Properties

		public class DataRefTypeItem	
		{
			public string DataRefTypeValue { get; set; }	
			public string DataRefTypeText { get; set; }	
			public bool DataRefTypeSelected { get; set; }	
		}		
		public List<DataRefTypeItem> DataRefTypeItemList { get; set; }	

		public class DataTypeItem	
		{
			public string DataTypeValue { get; set; }	
			public string DataTypeText { get; set; }	
			public bool DataTypeSelected { get; set; }	
		}		
		public List<DataTypeItem> DataTypeItemList { get; set; }	

		public class IsActiveVisible	
		{
			public string DataIndex { get; set; }	

			public class IsActive	
			{
			public string DataIndex { get; set; }	
			}
			public List<IsActive> IsActiveList { get; set; }	

			public class IsInActive	
			{
			public string DataIndex { get; set; }	
			}
			public List<IsInActive> IsInActiveList { get; set; }	
		}		
		public List<IsActiveVisible> IsActiveVisibleList { get; set; }	

		public class IsDeleteVisible	
		{
			public string DataIndex { get; set; }	

			public class IsDeleteChecked	
			{
			public string DataIndex { get; set; }	
			}
			public List<IsDeleteChecked> IsDeleteCheckedList { get; set; }	

			public class IsDelete	
			{
			public string DataIndex { get; set; }	
			}
			public List<IsDelete> IsDeleteList { get; set; }	
		}		
		public List<IsDeleteVisible> IsDeleteVisibleList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool ParentDataIdHidden { get; set; }	
		public bool ParentDataIdDisable { get; set; }	
		public bool DataRefIDHidden { get; set; }	
		public bool DataRefIDDisable { get; set; }	
		public bool DataRefTypeHidden { get; set; }	
		public bool DataRefTypeDisable { get; set; }	
		public bool DataTypeHidden { get; set; }	
		public bool DataTypeDisable { get; set; }	
		public bool ShowUserInfo { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ParentDataId { get; set; }	
		public string DataRefID { get; set; }	
		public string RevisionNo { get; set; }	
		public string DataValueName { get; set; }	
		public string DataValue { get; set; }	
		public string AddAction { get; set; }	
		public string EditAction { get; set; }	
		public string AppendAction { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1804);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1804);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#DataRefTypeItem}}";
			sectionEndTag = "{{/DataRefTypeItem}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (DataRefTypeItemList !=null))
			{
				foreach (var datareftypeitem in DataRefTypeItemList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolDataRefTypeItemSection(datareftypeitem , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataRefTypeValue}}", string.IsNullOrEmpty(datareftypeitem.DataRefTypeValue)==false ? datareftypeitem.DataRefTypeValue : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataRefTypeText}}", string.IsNullOrEmpty(datareftypeitem.DataRefTypeText)==false ? datareftypeitem.DataRefTypeText : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(DataRefTypeItemList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#DataTypeItem}}";
			sectionEndTag = "{{/DataTypeItem}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (DataTypeItemList !=null))
			{
				foreach (var datatypeitem in DataTypeItemList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolDataTypeItemSection(datatypeitem , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataTypeValue}}", string.IsNullOrEmpty(datatypeitem.DataTypeValue)==false ? datatypeitem.DataTypeValue : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataTypeText}}", string.IsNullOrEmpty(datatypeitem.DataTypeText)==false ? datatypeitem.DataTypeText : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(DataTypeItemList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsActiveVisible}}";
			sectionEndTag = "{{/IsActiveVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (IsActiveVisibleList !=null))
			{
				foreach (var isactivevisible in IsActiveVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessListIsActiveVisibleSection(isactivevisible , sectionValueInstance);
					sectionValueInstance = ProcessListIsActiveVisibleSection(isactivevisible , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isactivevisible.DataIndex)==false ? isactivevisible.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(IsActiveVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsDeleteVisible}}";
			sectionEndTag = "{{/IsDeleteVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (IsDeleteVisibleList !=null))
			{
				foreach (var isdeletevisible in IsDeleteVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessListIsDeleteVisibleSection(isdeletevisible , sectionValueInstance);
					sectionValueInstance = ProcessListIsDeleteVisibleSection(isdeletevisible , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isdeletevisible.DataIndex)==false ? isdeletevisible.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(IsDeleteVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ParentDataIdHidden}}";
			sectionEndTag = "{{/ParentDataIdHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ParentDataIdHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ParentDataIdDisable}}";
			sectionEndTag = "{{/ParentDataIdDisable}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ParentDataIdDisable ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataRefIDHidden}}";
			sectionEndTag = "{{/DataRefIDHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DataRefIDHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataRefIDDisable}}";
			sectionEndTag = "{{/DataRefIDDisable}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DataRefIDDisable ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataRefTypeHidden}}";
			sectionEndTag = "{{/DataRefTypeHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DataRefTypeHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataRefTypeDisable}}";
			sectionEndTag = "{{/DataRefTypeDisable}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DataRefTypeDisable ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataTypeHidden}}";
			sectionEndTag = "{{/DataTypeHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DataTypeHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataTypeDisable}}";
			sectionEndTag = "{{/DataTypeDisable}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DataTypeDisable ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ShowUserInfo}}";
			sectionEndTag = "{{/ShowUserInfo}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ShowUserInfo ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ParentDataId}}", string.IsNullOrEmpty(ParentDataId)==false ? ParentDataId : "");
			template = template.Replace("{{DataRefID}}", string.IsNullOrEmpty(DataRefID)==false ? DataRefID : "");
			template = template.Replace("{{RevisionNo}}", string.IsNullOrEmpty(RevisionNo)==false ? RevisionNo : "");
			template = template.Replace("{{DataValueName}}", string.IsNullOrEmpty(DataValueName)==false ? DataValueName : "");
			template = template.Replace("{{DataValue}}", string.IsNullOrEmpty(DataValue)==false ? DataValue : "");
			template = template.Replace("{{AddAction}}", string.IsNullOrEmpty(AddAction)==false ? AddAction : "");
			template = template.Replace("{{EditAction}}", string.IsNullOrEmpty(EditAction)==false ? EditAction : "");
			template = template.Replace("{{AppendAction}}", string.IsNullOrEmpty(AppendAction)==false ? AppendAction : "");
			return template;
		}

		protected string ProcessListDataRefTypeItemSection(DataRefTypeItem datareftypeitem, string template)
		{

			return template;
		}

		protected string ProcessListDataTypeItemSection(DataTypeItem datatypeitem, string template)
		{

			return template;
		}

		protected string ProcessListIsActiveVisibleSection(IsActiveVisible isactivevisible, string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsActive}}";
			sectionEndTag = "{{/IsActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (isactivevisible.IsActiveList !=null))
			{
				foreach (var isactive in isactivevisible.IsActiveList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isactive.DataIndex)==false ? isactive.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(isactivevisible.IsActiveList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsInActive}}";
			sectionEndTag = "{{/IsInActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (isactivevisible.IsInActiveList !=null))
			{
				foreach (var isinactive in isactivevisible.IsInActiveList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isinactive.DataIndex)==false ? isinactive.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(isactivevisible.IsInActiveList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessListIsDeleteVisibleSection(IsDeleteVisible isdeletevisible, string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsDeleteChecked}}";
			sectionEndTag = "{{/IsDeleteChecked}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (isdeletevisible.IsDeleteCheckedList !=null))
			{
				foreach (var isdeletechecked in isdeletevisible.IsDeleteCheckedList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isdeletechecked.DataIndex)==false ? isdeletechecked.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(isdeletevisible.IsDeleteCheckedList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsDelete}}";
			sectionEndTag = "{{/IsDelete}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (isdeletevisible.IsDeleteList !=null))
			{
				foreach (var isdelete in isdeletevisible.IsDeleteList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isdelete.DataIndex)==false ? isdelete.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(isdeletevisible.IsDeleteList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolDataRefTypeItemSection(DataRefTypeItem datareftypeitem, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataRefTypeSelected}}";
			sectionEndTag = "{{/DataRefTypeSelected}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, datareftypeitem.DataRefTypeSelected ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessBoolDataTypeItemSection(DataTypeItem datatypeitem, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataTypeSelected}}";
			sectionEndTag = "{{/DataTypeSelected}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, datatypeitem.DataTypeSelected ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessBoolIsActiveVisibleSection(IsActiveVisible isactivevisible, string template)
		{
			
			return template;
		}

		protected string ProcessBoolIsDeleteVisibleSection(IsDeleteVisible isdeletevisible, string template)
		{
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataSaveDetailAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataSaveDetailAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_484="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoOQWRkSW52aXRlIiBuYW1l4AoWAnR5cCAWEHN1Ym1pdCIgZGF0YS10aGVtIBMXYiIgb25jbGljaz0icmV0dXJuIHNhdmVEICINKDAsIHt7UGFnZU5vfX1ACwdJdGVtc1BlckATAH1gEUAnBUluZGV4fSAOEyd7e1RlbXBsYXRlU3VmZml4fX0nYBUKQ29uZmlnVG9rZW4gEgEpIiBWFCNBZGRBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9B4AkoBT5BZGQ8L4D84QA74AMAAjwvZKFwQAABPGThBHQBYmxBVQBi4QF1QAAAPIBNASB04RApAWMi4QgpBnJlZnJlc2hBBAVGb3JtKHvhTC3gCOEFQ2FuY2Vs4QgA4AH0gAkFPC9kaXY+";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool AddActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string ConfigToken { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_484);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_484);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddActionDisabled}}";
			sectionEndTag = "{{/AddActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{ConfigToken}}", string.IsNullOrEmpty(ConfigToken)==false ? ConfigToken : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataSaveDetailAppend	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataSaveDetailAppend.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_492="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMf4AcACzxidXR0b24gaWQ9IoAKD0FwcGVuZERhdGEiIG5hbWXgCxcCdHlwIBcIc3VibWl0IiBkICsELXRoZW0gExdiIiBvbmNsaWNrPSJyZXR1cm4gc2F2ZUQgIg0oMCwge3tQYWdlTm99fUALB0l0ZW1zUGVyQBMAfWARQCcFSW5kZXh9IA4TJ3t7VGVtcGxhdGVTdWZmaXh9fSdgFQpDb25maWdUb2tlbiASASkiIFYVI1NhdmVBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9T4AopAT5TIBQBPC+BAeEATOADAAI8L2ShgUAAATxk4QSFAWJsQWYAYuEBhkAAADyATQEgdOEQLAFjIuEILAZyZWZyZXNoQQcFRm9ybSh74Uww4AjhBUNhbmNlbOEIAOAB9IAJBTwvZGl2Pg==";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string ConfigToken { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_492);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_492);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveActionDisabled}}";
			sectionEndTag = "{{/SaveActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{ConfigToken}}", string.IsNullOrEmpty(ConfigToken)==false ? ConfigToken : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataSaveDetailEdit	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataSaveDetailEdit.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_560="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoNU2F2ZURhdGEiIG5hbWXgCRUCdHlwIBUIc3VibWl0IiBkICkELXRoZW0gExRiIiBvbmNsaWNrPSJyZXR1cm4gc2GATBEoe3tJZH19LCB7e1BhZ2VOb31gCwdJdGVtc1BlckATAH1gEQBEIE8FSW5kZXh9IA4TJ3t7VGVtcGxhdGVTdWZmaXh9fSdgFQpDb25maWdUb2tlbiASASkiIFYBI1MgtxBBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9T4AopAT5TIBQBPC+BAuEAQeADAAI8L2ShdkAAATxk4QR6AWJsQVsAYuEBe0AAADyATQEgdOEQMQFjIuEIMQZyZWZyZXNoQQcFRm9ybSh74Uww4AjhBUNhbmNlbOEIAOAB9OACCQBk4QIHwADgLOsEZGVsZXRibCDmAUlk4lUfAkRlbCBs4h0hAETgDCsBPkRgFuEIJEAAgR4FPC9kaXY+";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveActionDisabled { get; set; }	
		public bool DeleteActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string Id { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string ConfigToken { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_560);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_560);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveActionDisabled}}";
			sectionEndTag = "{{/SaveActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DeleteActionDisabled}}";
			sectionEndTag = "{{/DeleteActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DeleteActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{Id}}", string.IsNullOrEmpty(Id)==false ? Id : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{ConfigToken}}", string.IsNullOrEmpty(ConfigToken)==false ? ConfigToken : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataView	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/DataView.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_528="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAAk8c3BhbiBzdHlsIE8fcG9zaXRpb246YWJzb2x1dGU7dG9wOjVweDtyaWdodDoBMTAgCoBDwAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXBHJlc2hEINgATCDRFih7e1BhZ2VOb319LCB7e0l0ZW1zUGVyQBMAfWARQCgFSW5kZXh9IA4WJ3t7VGVtcGxhdGVTdWZmaXh9fScpOyKA1oEmBmJ1dHRvbiKAEgZpbmxpbmU94QMlA21pbmngBBAAaSEoAD0gnWCWACKgNyATIPcLPSJub3RleHQiPlJlYB4CPC9h4QEkAC9BJeAADhNQbGVhc2UgbG9naW4gdG8gVmlldyDQQL4NVmFsdWVOYW1lfX0ocylBJAk8L2xpPjwvdWw+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string DataValueName { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_528);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_528);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{DataValueName}}", string.IsNullOrEmpty(DataValueName)==false ? DataValueName : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateMain	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlData/Main.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_108="HzxkaXYgZGF0YS1yb2xlPSJjb2xsYXBzaWJsZS1zZXQiAT4gIAAIe3tNYW5hZ2VEICcIUmVmVHlwZX19IBfgBBgAVOAMFQd9fTwvZGl2Pg==";

		#endregion		

        #region PlaceHolder Properties

		public string ManageDataRefType { get; set; }	
		public string ManageDataType { get; set; }	
		public string ManageData { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_108);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_108);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ManageDataRefType}}", string.IsNullOrEmpty(ManageDataRefType)==false ? ManageDataRefType : "");
			template = template.Replace("{{ManageDataType}}", string.IsNullOrEmpty(ManageDataType)==false ? ManageDataType : "");
			template = template.Replace("{{ManageData}}", string.IsNullOrEmpty(ManageData)==false ? ManageData : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefType	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefType.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_192="HzxkaXYgaWQ9ImRhdGFSZWZUeXBlU2F2ZVNlY3Rpb24iACBAFxItcm9sZT0iY29sbGFwc2libGUigBcAY4ARBmVkPSJ7e1MgNgpFeHBhbmR9fSI+ICAAAjxoM2AHQAAHTWFuYWdlIETgAWYCPC9ogB6AOgdEZXRhaWx9fUAsBXt7TGlzdMARBTwvZGl2Pg==";

		#endregion		

        #region PlaceHolder Properties

		public string SaveExpand { get; set; }	
		public string SaveDetail { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_192);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_192);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveExpand}}", string.IsNullOrEmpty(SaveExpand)==false ? SaveExpand : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeList	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeList.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeListScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeListScript.min.js";										
		private const string TemplateSource_92="H3t7TGlzdFNjcmlwdH19PGRpdiBpZD0iZGF0YVJlZlR5AXBlQB8GVmlldyI+ICAAAXt7QA8NRGV0YWlsfX08L2Rpdj4=";
		private const string MinScriptSource_904="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaERhdGFSZWZUeXBlTGlzdChhLGMsaCxqKXt2YXIgA2c9YztABx9lPSJpdGVtc1BlclBhZ2UiK2g7aWYoJCgiIyIrZSkubAplbmd0aD4wKXtnPaAUBSsiIG9wdCBnEjpzZWxlY3RlZCIpLnZhbCgpfXYgWBhmPSd7InBhZ2VObyIgOiAnK2E7Zis9Jywg4AVnYBoBZzugGgBkIKoESW5kZXiAFwFoO6AXIJkVcGxhdGVTdWZmaXgiOiInK2orJyInOyAfAiJ9ImDBBmQ9IkdldEQgQeAC7ARWaWV3ImAeAmI9d+EGKgErK2AYAGlAqBZqc29ucnBjIjogIjIuMCIsIm1ldGhvZGBpAGQgaQAsIMoNcmFtcyI6JytmKycsImkgGx8nK2IrIn0iOyQuYWpheCh7dHlwZToiUE9TVCIsY29udANlbnRUIXQJOiJhcHBsaWNhdCEsHy9qc29uOyBjaGFyc2V0PXV0Zi04Iix1cmw6Ii8qIUBTDmVydmljZVVybEAqLyIsZCDNATppYAYAVGBJAGogPQkiLHN1Y2Nlc3M6wekDKGspe0HEA2w9azshtR9rLmhhc093blByb3BlcnR5KCJlcnJvciIpPT1mYWxzZQEpe+AMJA5kIikpe2w9ay5kfWVsc2XgDSQiUAN1bHQioCmADQF9fSBPAGzgCHQEaHRtbCIgKAMkKCIjQMjhApYiKUAdQDQgJAEpO+ANIgBsIq0EdmlldyggdWLJASIp4A4pEnRyaWdnZXIoImNyZWF0ZSIpfX1gwgRzaG93RUD7IRMAZUAHCi5uYW1lKyI6IitroBAAbSFIEGFnZSl9fX0pO3JldHVybiBmQSUBfTs=";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ListScript { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_904);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_904);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_904);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_92);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_92);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListScript}}", string.IsNullOrEmpty(ListScript)==false ? ListScript : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeListDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeListDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_192="GDx1bCBpZD0iZGF0YVJlZlR5cGVMaXN0IiBAEActcm9sZT0ibCAQBHZpZXcigBQLaW5zZXQ9InRydWUigBEIdGhlbWU9ImEigA4GY29udGVudMAWAmIiICAAYE8JZGl2aWRlcnRoZUAvwD4OZmlsdGVyPSJmYWxzZSI+ICwCIHt7QIUKSXRlbX19PC91bD4=";

		#endregion		

        #region PlaceHolder Properties

		public string ListItem { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_192);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_192);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListItem}}", string.IsNullOrEmpty(ListItem)==false ? ListItem : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeListDetailEmpty	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeListDetailEmpty.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_404="BDxsaT4gIAAfPHNwYW4gc3R5bGU9InBvc2l0aW9uOmFic29sdXRlO3QOb3A6NXB4O3JpZ2h0OjEwIAoAImA3QAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXH3Jlc2hEYXRhUmVmVHlwZUxpc3Qoe3tQYWdlTm99fSwgCXt7SXRlbXNQZXJAEwB9YBFALwVJbmRleH0gDhgne3tUZW1wbGF0ZVN1ZmZpeH19Jyk7IiBkIFQNLXJvbGU9ImJ1dHRvbiKAEgRpbmxpbiAUBHRydWUigBIDbWluaeAEEARpY29uPSCkYJ0AIoAkQBMg+gs9Im5vdGV4dCI+UmVgHgI8L2Fg6wE8L0EkYAoDTm8gRCB9BSBSZWYgVCDUDShzKSBGb3VuZDwvbGk+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_404);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_404);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeListDetailItem	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeListDetailItem.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_560="BDxsaT4gIAACPGgzYAdAABk8c3Ryb25nPnt7RGF0YVJlZlR5cGV9fTwvc4AXQCNgAB97eyNJc0luQWN0aXZlfX08c3BhbiBzdHlsZT0iY29sbxlyOnJlZDtmb250LXdlaWdodDpib2xkOyI+KMA0ACkgVCA0BD57ey9J4AJJYF0gAGBgB0RlZmF1bHR94AtfBWJsYWNrO+ALYQBEgDUAKeADYOAASSBaBCAgPC9ogPMGe3sjRWRpdEDGAW9uoBwgABU8cCBjbGFzcz0idWktbGktYXNpZGUi4AH9IAAIPGJ1dHRvbiBkIScNLWlubGluZT0idHJ1ZSKAEglhamF4PSJmYWxzwBEDbWluaeAEIgZpY29uPSJlIH8VIiBvbmNsaWNrPSJyZXR1cm4gZ2V0RCBZoYEZU2F2ZVZpZXcoe3tJZH19LCB7e1BhZ2VOb31gCwdJdGVtc1BlckATAH1gEUA7BUluZGV4fSAOGCd7e1RlbXBsYXRlU3VmZml4fX0nKTsiPkUgeQE8L4DH4ADbAjwvcGALA3t7L0UgH8EZBDwvbGk+";

		#endregion		

        #region List Section Properties

		public class EditAction	
		{
			public string Id { get; set; }	
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
		}		
		public List<EditAction> EditActionList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool IsInActive { get; set; }	
		public bool IsDefault { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string DataRefType { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_560);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_560);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#EditAction}}";
			sectionEndTag = "{{/EditAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (EditActionList !=null))
			{
				foreach (var editaction in EditActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(editaction.Id)==false ? editaction.Id : "");
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(editaction.PageNo)==false ? editaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(editaction.ItemsPerPage)==false ? editaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(editaction.DataIndex)==false ? editaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(editaction.TemplateSuffix)==false ? editaction.TemplateSuffix : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(EditActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsInActive}}";
			sectionEndTag = "{{/IsInActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsInActive ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDefault}}";
			sectionEndTag = "{{/IsDefault}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsDefault ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{DataRefType}}", string.IsNullOrEmpty(DataRefType)==false ? DataRefType : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeSave	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeSave.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeSaveScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeSaveScript.min.js";										
		private const string TemplateSource_96="H3t7U2F2ZVNjcmlwdH19IDxkaXYgaWQ9ImRhdGFSZWZUA3lwZVMgIAdWaWV3IiA+ICAAAHtgMQ1EZXRhaWx9fTwvZGl2Pg==";
		private const string MinScriptSource_1928="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaERhdGFSZWZUeXBlRm9ybShiLGQsYyxhKXtpZih0BXlwZW9mKOAJKQdMaXN0KT09IsBMAyIpe3LgCE5AJAAowE4FfSQoIiNk4AFrFFNhdmVWaWV3IikuaHRtbCgiIik7JOAFIkA/A1NlY3QgrCAlEXRyaWdnZXIoImV4cGFuZCIpfcB8BCBnZXRE4AlZFyhlLGEsYyxnLGosaCl7dmFyIGY9J3siZOABKRZJZCIgOiAnK2U7Zis9JywgInBhZ2VOb4AUAWE7oBQLaXRlbXNQZXJQYWdlgBoBYzugGkBLBEluZGV4gBcBZzugFyAxFXBsYXRlU3VmZml4IjoiJytqKyciJzsgHwMifSI7QIwEZD0iR2XgC7cAImAeAmI9d+EGuwErK2AYAGlAxBZqc29ucnBjIjogIjIuMCIsIm1ldGhvZGBpAGQgaQAsIMoNcmFtcyI6JytmKycsImkgGxAnK2IrIn0iOyQuYWpheCh7dCHdDzoiUE9TVCIsY29udGVudFQiBQk6ImFwcGxpY2F0IXgfL2pzb247IGNoYXJzZXQ9dXRmLTgiLHVybDoiLyohQFMNZXJ2aWNlVXJsQCovIixBDwE6aWAGAFRgSQBqID0JIixzdWNjZXNzOsGwAyhrKXtBAh9sPWs7aWYoay5oYXNPd25Qcm9wZXJ0eSgiZXJyb3IiKQc9PWZhbHNlKUKK4AkkDmQiKSl7bD1rLmR9ZWxzZeANJCLhA3VsdCKgKYANAX19IHQAbOAIdEKGACIgKAEkKOIUqCA4Iq8BKTvgESYAdMKpBmNyZWF0ZSLiCNjiAvsJc2hvdygic2xvdyAmIJYJaD09dHJ1ZSl7JOAJhQBT4xILAX19YQMAcyBOAUVyITwhLwBlQAcKLm5hbWUrIjoiK2ugEABtIYkDYWdlKSAyCSk7cmV0dXJuIGZBZgB9wZ4FIHNhdmVE4wEmAyhmLGIkFANqLG0pYbYBYT1AogA7gcHgBqcjqAR2YWwoKUDLC2wubGVuZ3RoPT0wKeACmQciUGxlYXNlICJuBmVyIHRoZSDgAmsDIik7YYH1AX12I7EAaIALQE0AJOEFEAVJc0RlZmFh2xIuaXMoIjpjaGVja2VkIikpe2g9QKBgQAFpPWJC4AxABUFjdGl2ZeAKPwFpPWA/IY4AYcGOIAsAdCMuA29mKHMhYglQcm9ncmVzcyk95QQC4AMaASgpYH8AZ0OtAGThAUtEKBEiJytlbmNvZGVVUklDb21wb24hCgIobCkjtwInO2eEieACMuAB6WSsAWg74AsioMtgIQFpO+AKIaTqAWY7IBsAIiQEQb0BYz3kDmMDZT0iUyWfAETgArdgMABrQM0AaiPTAHLkDnsAZSDDACzkAnsDZysnLKR7A2MrIn3kWHsAa+QZewJuKXtA+wdvPW47aWYobuQIBuQKe+AJJAFkIiJmIEIALuQBe+AJJIRtACKgKYANAH1CVQBv4Ah0o5QAIiArBXNob3dTdWUwICigGAApI6nnEK1DmAB9Y/4g0OAJW6DQAClgWYQcAW8uZBwBKX3kCDYBbi5gGeQBNgBuoBDAiEC/owkEaGlkZVDjDQkAaOACGgEoKSBmASl95A1qBGRlbGV04gNoACjHtQFpKUDOA2U+MClABwB04yt85xTvACIijUIIAWI94w4EBWQ9IkRlbOAFnQAiYDIAaOMXBud3ggBo4xkGAmope0D9Ams9akMGAGriCJGiNecDguAJJAhkIikpe2s9ai7jAQbgCSSC+AAioCmADQB9QkYAa+AIdKJkACIgKwBz4wIGAWsuoBjjEAYAYWnoAWkp4wAG4AlboNDjAwYAa4cSASl94wgGAWou4wYGAGqgEMCIQL+ik+MxBgF9Ow==";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string SaveScript { get; set; }	
		public string SaveDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1928);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1928);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1928);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_96);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_96);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveScript}}", string.IsNullOrEmpty(SaveScript)==false ? SaveScript : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeSaveAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeSaveAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_424="FzxsaSBzdHlsZT0iY29sb3I6Z3JheSI+ICAAHjxoNj5BZGQgRGF0YSBSZWZlcmVuY2UgVHlwZTwvaDZgIxUgPHAgY2xhc3M9InVpLWxpLWFzaWRlgD9AABU8YSBvbmNsaWNrPSJyZXR1cm4gZ2V0QFEDUmVmVCBJFVNhdmVWaWV3KDAsIHt7UGFnZU5vfX1ACwdJdGVtc1BlckATAH1gEUA2BUluZGV4fSAOHyd7e1RlbXBsYXRlU3VmZml4fX0nLCB0cnVlKTsiIGhyCGVmPSIjIiAgZCC9DC1hamF4PSJmYWxzZSKAEQh0aGVtZT0iYiKADgJyb2xADQV1dHRvbiKAEgVtaW5pPSJAUKAyBWlubGluZeAFEgpjb249InBsdXMiIIEPoAAAQSE3oAACPC9hYBoIPC9wPjwvbGk+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_424);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_424);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeSaveDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeSaveDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1120="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAABQ8cCBjbGFzcz0idWktbGktYXNpZGWAJsAADXt7UmV2aXNpb25Ob319wBUCPC9w4AFECmxhYmVsIGZvcj0iQHYGUmVmVHlwZeAFRgBEIN+gGBw8c3BhbiBzdHlsZT0iY29sb3I6IFJlZDsiPio8L0AbAj48L2BS4ABhAnt7I+ACPwpOYW1lRW5hYmxlZOACkAhpbnB1dCB0eXAgVAh0ZXh0IiBuYW0gCwBk4AF4BCIgaWQ94AUQCXZhbHVlPSJ7e0TgASYBfX1hQcAAH21heGxlbmd0aD0iNTAiIHBsYWNlaG9sZGVyPSImIzE4Bjc7RW50ZXJg4QBUIPcfKiIgb25rZXlwcmVzcz0icmV0dXJuIGNsaWNrQnV0dG8VbihldmVudCwne3sjQWRkTW9kZX19YmAZAkFkZOAChwN7ey9BwB8De3teQcALgCsDU2F2ZeAOLAUnKTsiIC/hAj0AL+ACKAFOYeEKPeEJYANEaXNh4f9hgs/hA47hGGHhBT4CPC9sg3MAPCN7C3t7I0lzRGVmYXVsdOIOvwdjaGVja2JveOIKwwBJwDoAIuIHzOACGQBjQD8DZWQ9ImBIAmVkIuIFDQBJwEPgAX4De3teSeAJFeMEVGBL4DKU4gSQ4AqC5A5G4AHWBD5JcyBEgTcAPOQCIOEAXAF7eyFYDEFjdGl2ZVZpc2libGWA3wA8wXSAG4AUwADgIeKAPQAi4Ql2wBjhGHWAKOABf2F04AcUQADgQpThBnXgB23hEHSAKAAiQXOACgA84Q1yIcmAH+ELcgxTaG93QWRtaW5JbmZv4AH3ADxF5MYAAGYmvQktd2VpZ2h0OiBsQAYFZXI7IGNvZhYAcmYWEVBsZWFzZSBsb2dpbiBhcyBVcyVuBmhhdmluZyBgXgkgUm9sZSB0byBTJRQAPIZDQUdAAAN7ey9T4A2GwAAEe3tBZGRAxAFvbuABowV7e0VkaXTgAxViZwQ8L3VsPg==";

		#endregion		

        #region List Section Properties

		public class DataRefTypeNameEnabled	
		{
			public string DataRefType { get; set; }	
			public bool AddMode { get; set; }	
		}		
		public List<DataRefTypeNameEnabled> DataRefTypeNameEnabledList { get; set; }	

		public class DataRefTypeNameDisabled	
		{
			public string DataRefType { get; set; }	
			public bool AddMode { get; set; }	
		}		
		public List<DataRefTypeNameDisabled> DataRefTypeNameDisabledList { get; set; }	

		public class IsActiveVisible	
		{
			public bool IsActive { get; set; }	
		}		
		public List<IsActiveVisible> IsActiveVisibleList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool IsDefault { get; set; }	
		public bool ShowAdminInfo { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string RevisionNo { get; set; }	
		public string AddAction { get; set; }	
		public string EditAction { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1120);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1120);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#DataRefTypeNameEnabled}}";
			sectionEndTag = "{{/DataRefTypeNameEnabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (DataRefTypeNameEnabledList !=null))
			{
				foreach (var datareftypenameenabled in DataRefTypeNameEnabledList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolDataRefTypeNameEnabledSection(datareftypenameenabled , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataRefType}}", string.IsNullOrEmpty(datareftypenameenabled.DataRefType)==false ? datareftypenameenabled.DataRefType : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(DataRefTypeNameEnabledList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#DataRefTypeNameDisabled}}";
			sectionEndTag = "{{/DataRefTypeNameDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (DataRefTypeNameDisabledList !=null))
			{
				foreach (var datareftypenamedisabled in DataRefTypeNameDisabledList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolDataRefTypeNameDisabledSection(datareftypenamedisabled , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataRefType}}", string.IsNullOrEmpty(datareftypenamedisabled.DataRefType)==false ? datareftypenamedisabled.DataRefType : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(DataRefTypeNameDisabledList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsActiveVisible}}";
			sectionEndTag = "{{/IsActiveVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (IsActiveVisibleList !=null))
			{
				foreach (var isactivevisible in IsActiveVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolIsActiveVisibleSection(isactivevisible , sectionValueInstance);
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(IsActiveVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDefault}}";
			sectionEndTag = "{{/IsDefault}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsDefault}}";
            invertedSectionEndTag = "{{/IsDefault}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsDefault ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, IsDefault ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ShowAdminInfo}}";
			sectionEndTag = "{{/ShowAdminInfo}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ShowAdminInfo ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{RevisionNo}}", string.IsNullOrEmpty(RevisionNo)==false ? RevisionNo : "");
			template = template.Replace("{{AddAction}}", string.IsNullOrEmpty(AddAction)==false ? AddAction : "");
			template = template.Replace("{{EditAction}}", string.IsNullOrEmpty(EditAction)==false ? EditAction : "");
			return template;
		}

		protected string ProcessListDataRefTypeNameEnabledSection(DataRefTypeNameEnabled datareftypenameenabled, string template)
		{

			return template;
		}

		protected string ProcessListDataRefTypeNameDisabledSection(DataRefTypeNameDisabled datareftypenamedisabled, string template)
		{

			return template;
		}

		protected string ProcessListIsActiveVisibleSection(IsActiveVisible isactivevisible, string template)
		{

			return template;
		}

		protected string ProcessBoolDataRefTypeNameEnabledSection(DataRefTypeNameEnabled datareftypenameenabled, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddMode}}";
			sectionEndTag = "{{/AddMode}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^AddMode}}";
            invertedSectionEndTag = "{{/AddMode}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, datareftypenameenabled.AddMode ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, datareftypenameenabled.AddMode ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessBoolDataRefTypeNameDisabledSection(DataRefTypeNameDisabled datareftypenamedisabled, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddMode}}";
			sectionEndTag = "{{/AddMode}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^AddMode}}";
            invertedSectionEndTag = "{{/AddMode}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, datareftypenamedisabled.AddMode ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, datareftypenamedisabled.AddMode ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessBoolIsActiveVisibleSection(IsActiveVisible isactivevisible, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsActive}}";
			sectionEndTag = "{{/IsActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsActive}}";
            invertedSectionEndTag = "{{/IsActive}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, isactivevisible.IsActive ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, isactivevisible.IsActive ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeSaveDetailAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeSaveDetailAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_476="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMf4AcACzxidXR0b24gaWQ9IoAKE0FkZERhdGFSZWZUeXBlIiBuYW1l4A8bAnR5cCAbCHN1Ym1pdCIgZCA2BC10aGVtIBMXYiIgb25jbGljaz0icmV0dXJuIHNhdmVEICKgWQ0oMCwge3tQYWdlTm99fUALB0l0ZW1zUGVyQBMAfWARQC4FSW5kZXh9IA4VJ3t7VGVtcGxhdGVTdWZmaXh9fScpIiBDFCNBZGRBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9B4AkoBT5BZGQ8L4D64QBF4AMAAjwvZKF6QAABPGThBH4BYmxBXwBi4QF/QAAAPIBNASB04RAdAWMi4QgdBnJlZnJlc2hA8aEgBUZvcm0oe+E5IeAI1QVDYW5jZWzgCPTgAeiACQU8L2Rpdj4=";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool AddActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_476);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_476);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddActionDisabled}}";
			sectionEndTag = "{{/AddActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeSaveDetailEdit	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeSaveDetailEdit.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_552="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoUU2F2ZURhdGFSZWZUeXBlIiBuYW1l4BAcAnR5cCAcCHN1Ym1pdCIgZCA3BC10aGVtIBMUYiIgb25jbGljaz0icmV0dXJuIHNh4ARaESh7e0lkfX0sIHt7UGFnZU5vfWALB0l0ZW1zUGVyQBMAfWARAEQgVgVJbmRleH0gDhUne3tUZW1wbGF0ZVN1ZmZpeH19JykiIEMBI1MguRBBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9T4AopAT5TIBQBPC+BBOEAQ+ADAAI8L2SheEAAATxk4QR8AWJsQV0AYuEBfUAAADyATQEgdOEQJQFjIuEIJQZyZWZyZXNoQPShgwVGb3JtKHvhOSTgCNUFQ2FuY2Vs4Aj04AHo4AIJAGTgAvvAAOAs3wRkZWxldOIDYiDaAUlk4kIHAkRlbCBg4h0JAETgDCsBPkRgFuEIGEAAgRIFPC9kaXY+";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveActionDisabled { get; set; }	
		public bool DeleteActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string Id { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_552);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_552);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveActionDisabled}}";
			sectionEndTag = "{{/SaveActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DeleteActionDisabled}}";
			sectionEndTag = "{{/DeleteActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DeleteActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{Id}}", string.IsNullOrEmpty(Id)==false ? Id : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataRefTypeView	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataRefType/DataRefTypeView.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_568="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAAk8c3BhbiBzdHlsIE8fcG9zaXRpb246YWJzb2x1dGU7dG9wOjVweDtyaWdodDoBMTAgCoBDwAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXBHJlc2hEINgHUmVmVHlwZUwg2BYoe3tQYWdlTm99fSwge3tJdGVtc1BlckATAH1gEUAvBUluZGV4fSAOFid7e1RlbXBsYXRlU3VmZml4fX0nKTsigN2BLQZidXR0b24igBIGaW5saW5lPeEDLANtaW5p4AQQAGkhLwA9IKRgnQAioDcgEyD+Cz0ibm90ZXh0Ij5SZWAeAjwvYeEBKwAvQSzgAA4dUGxlYXNlIGxvZ2luIGFzIFVzZXIgaGF2aW5nIEFkIHUNIFJvbGUgdG8gVmlldyBA1gUgUmVmIFQhCAIocylBQQk8L2xpPjwvdWw+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_568);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_568);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataType	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataType.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_184="HTxkaXYgaWQ9ImRhdGFUeXBlU2F2ZVNlY3Rpb24iIEAUEi1yb2xlPSJjb2xsYXBzaWJsZSKAFwBjgBEGZWQ9Int7UyA2CkV4cGFuZH19Ij4gIAACPGgzYAdAAAdNYW5hZ2UgRKBjAjwvaIAbgDcHRGV0YWlsfX1AKQV7e0xpc3TAEQU8L2Rpdj4=";

		#endregion		

        #region PlaceHolder Properties

		public string SaveExpand { get; set; }	
		public string SaveDetail { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_184);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_184);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveExpand}}", string.IsNullOrEmpty(SaveExpand)==false ? SaveExpand : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeList	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeList.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlDataType/DataTypeListScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlDataType/DataTypeListScript.min.js";										
		private const string TemplateSource_88="Hnt7TGlzdFNjcmlwdH19PGRpdiBpZD0iZGF0YVR5cGVAHAZWaWV3Ij4gIAABe3tADw1EZXRhaWx9fTwvZGl2Pg==";
		private const string MinScriptSource_900="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaERhdGFUeXBlTGlzdChhLGMsaCxqKXt2YXIgZz1jADtABx9lPSJpdGVtc1BlclBhZ2UiK2g7aWYoJCgiIyIrZSkubAplbmd0aD4wKXtnPaAUBSsiIG9wdCBkEjpzZWxlY3RlZCIpLnZhbCgpfXYgWBhmPSd7InBhZ2VObyIgOiAnK2E7Zis9Jywg4AVnYBoBZzugGgBkIKcESW5kZXiAFwFoO6AXIJkVcGxhdGVTdWZmaXgiOiInK2orJyInOyAfAiJ9ImDBBmQ9IkdldEQgQcDpBFZpZXciYBsCYj134QYkASsrYBgAaUClFmpzb25ycGMiOiAiMi4wIiwibWV0aG9kYGYAZCBmACwgxw1yYW1zIjonK2YrJywiaSAbHycrYisifSI7JC5hamF4KHt0eXBlOiJQT1NUIixjb250A2VudFQhcQk6ImFwcGxpY2F0ISkfL2pzb247IGNoYXJzZXQ9dXRmLTgiLHVybDoiLyohQFMOZXJ2aWNlVXJsQCovIixkIMoBOmlgBkDRAjoiaiA9CSIsc3VjY2VzczrB4wMoayl7QcEDbD1rOyGyH2suaGFzT3duUHJvcGVydHkoImVycm9yIik9PWZhbHNlASl74AwkDmQiKSl7bD1rLmR9ZWxzZeANJCJKA3VsdCKgKYANAX19IE8AbOAIdARodG1sIiAoAyQoIiNAyEDBQn0iI0AaQDEgIQEpO+AKHwBsIqQEdmlldyggb2K9ASIp4AsmEnRyaWdnZXIoImNyZWF0ZSIpfX1guQRzaG93RUDyIQoAZUAHCi5uYW1lKyI6IitroBAAbSE/EGFnZSl9fX0pO3JldHVybiBmQRwBfTs=";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ListScript { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_900);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_900);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_900);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_88);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_88);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListScript}}", string.IsNullOrEmpty(ListScript)==false ? ListScript : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeListDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeListDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_188="FTx1bCBpZD0iZGF0YVR5cGVMaXN0IiBADQctcm9sZT0ibCAQBHZpZXcigBQLaW5zZXQ9InRydWUigBEIdGhlbWU9ImEigA4GY29udGVudMAWAmIiICAAYE8JZGl2aWRlcnRoZUAvwD4OZmlsdGVyPSJmYWxzZSI+ICwCIHt7QIUKSXRlbX19PC91bD4=";

		#endregion		

        #region PlaceHolder Properties

		public string ListItem { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_188);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_188);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListItem}}", string.IsNullOrEmpty(ListItem)==false ? ListItem : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeListDetailEmpty	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeListDetailEmpty.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_396="BDxsaT4gIAAfPHNwYW4gc3R5bGU9InBvc2l0aW9uOmFic29sdXRlO3QOb3A6NXB4O3JpZ2h0OjEwIAoAImA3QAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXH3Jlc2hEYXRhVHlwZUxpc3Qoe3tQYWdlTm99fSwge3tJBnRlbXNQZXJAEwB9YBFALAVJbmRleH0gDhgne3tUZW1wbGF0ZVN1ZmZpeH19Jyk7IiBkIFENLXJvbGU9ImJ1dHRvbiKAEgRpbmxpbiAUBHRydWUigBIDbWluaeAEEARpY29uPSChYJoAIoAkQBMg9ws9Im5vdGV4dCI+UmVgHgI8L2Fg6AE8L0EhYAoDTm8gRCB9ASBUINANKHMpIEZvdW5kPC9saT4=";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_396);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_396);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeListDetailItem	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeListDetailItem.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_564="BDxsaT4gIAACPGgzYAdAABY8c3Ryb25nPnt7RGF0YVR5cGV9fTwvc4AUQCDgAAAfe3sjSXNJbkFjdGl2ZX19PHNwYW4gc3R5bGU9ImNvbG8ccjogcmVkOyBmb250LXdlaWdodDogYm9sZDsiPijANwApIFsgNwQ+e3svSeACTOAAZCAAYGcHRGVmYXVsdH3gDGYGYmxhY2s7IOAMaABEgDgAKeADZ+AATCBdAyA8L2jgAf0Ge3sjRWRpdEDTAW9ugB9AABU8cCBjbGFzcz0idWktbGktYXNpZGUi4QQOCDxidXR0b24gZCE1DS1pbmxpbmU9InRydWUigBIJYWpheD0iZmFsc8ARA21pbmngBCIGaWNvbj0iZSB/FSIgb25jbGljaz0icmV0dXJuIGdldEQgWUGPGVNhdmVWaWV3KHt7SWR9fSwge3tQYWdlTm99YAsHSXRlbXNQZXJAEwB9YBFAOAVJbmRleH0gDhgne3tUZW1wbGF0ZVN1ZmZpeH19Jyk7Ij5FIHYBPC+AxOAA2AI8L3BgCwN7ey9FIB/BFgQ8L2xpPg==";

		#endregion		

        #region List Section Properties

		public class EditAction	
		{
			public string Id { get; set; }	
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
		}		
		public List<EditAction> EditActionList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool IsInActive { get; set; }	
		public bool IsDefault { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string DataType { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_564);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_564);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#EditAction}}";
			sectionEndTag = "{{/EditAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (EditActionList !=null))
			{
				foreach (var editaction in EditActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(editaction.Id)==false ? editaction.Id : "");
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(editaction.PageNo)==false ? editaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(editaction.ItemsPerPage)==false ? editaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(editaction.DataIndex)==false ? editaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(editaction.TemplateSuffix)==false ? editaction.TemplateSuffix : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(EditActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsInActive}}";
			sectionEndTag = "{{/IsInActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsInActive ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDefault}}";
			sectionEndTag = "{{/IsDefault}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsDefault ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{DataType}}", string.IsNullOrEmpty(DataType)==false ? DataType : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeSave	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeSave.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlDataType/DataTypeSaveScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlDataType/DataTypeSaveScript.min.js";										
		private const string TemplateSource_92="H3t7U2F2ZVNjcmlwdH19IDxkaXYgaWQ9ImRhdGFUeXBlAFMgHQdWaWV3IiA+ICAAAHtgLg1EZXRhaWx9fTwvZGl2Pg==";
		private const string MinScriptSource_1908="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaERhdGFUeXBlRm9ybShiLGQsYyxhKXtpZih0eXBlAm9mKOAGJgdMaXN0KT09IsBGAyIpe3LgBUhAIQAowEgFfSQoIiNkoGIUU2F2ZVZpZXciKS5odG1sKCIiKTsk4AIfQDkDU2VjdCCdICIRdHJpZ2dlcigiZXhwYW5kIil9wHMEIGdldETgBlMDKGEsYiC8EGcsaixoKXt2YXIgZj0neyJkoCYWSWQiIDogJythO2YrPScsICJwYWdlTm+AFAFiO6AUC2l0ZW1zUGVyUGFnZYAaAWQ7oBpASARJbmRleIAXAWc7oBcgMRVwbGF0ZVN1ZmZpeCI6IicraisnIic7IB8DIn0iO0CJBGU9Ikdl4AixACJgGwJjPXfhBqMBKytgGABpQL4WanNvbnJwYyI6ICIyLjAiLCJtZXRob2RgZgBlIGYALCDHDXJhbXMiOicrZisnLCJpIBsQJytjKyJ9IjskLmFqYXgoe3QhyA86IlBPU1QiLGNvbnRlbnRUIfAJOiJhcHBsaWNhdCFvHy9qc29uOyBjaGFyc2V0PXV0Zi04Iix1cmw6Ii8qIUBTDWVydmljZVVybEAqLyIsQQwBOmlgBkI6AjoiaiA9CSIsc3VjY2VzczrBpwMoayl7QP8fbD1rO2lmKGsuaGFzT3duUHJvcGVydHkoImVycm9yIikHPT1mYWxzZSlCdeAJJA5kIikpe2w9ay5kfWVsc2XgDSQiyQN1bHQioCmADQF9fSB0AGzgCHRCegAiICgBJCjiEZkgNSKgASk74A4jAHTCmgZjcmVhdGUi4gXG4gLmCXNob3coInNsb3cgIyCNCWg9PXRydWUpeyTgBnwAU+IS9gF9fWD3AHMgSwFFciEwISMAZUAHCi5uYW1lKyI6IitroBAAbSF9A2FnZSkgMgkpO3JldHVybiBmQVoAfcGSBSBzYXZlRKMUAShiIzEFZixqLG0pYacBYz1AnAA7QbIBZz3gA6EDTmFtZSORBHZhbCgpQMYLZy5sZW5ndGg9PTAp4AKXByJQbGVhc2UgImAGZXIgdGhlIMBpAyIpO2OB5AF9diOaAGyAC0BKACThAggFSXNEZWZhYccOLmlzKCI6Y2hlY2tlZCIpQgBAm2A9AWE9Yi7gCT0FQWN0aXZl4Ao8AWE9YDwhgABjwYAgCwB0IxcDb2YocyFXCVByb2dyZXNzKT3kBNngAxoBKClgfGOWAGShQAJOYW1kJxEiJytlbmNvZGVVUklDb21wb24hAgIoZykjoQInO2mEcMAz4AHkZJABbDvgCB+gxmAeAWE74AcepMgBYjsgGAAiI+VBrQFlPeQORANoPSJTJXQARKCvACJgLQBr5BdZAGggtwAs5AJZA2krJyykWSJvAH3kWFkAa+QZWQJuKXtA+AdvPW47aWYobuMI5OQKWeAJJAFkIiJYIEIALuQBWeAJJIRLACKgKYANAH1CSgBv4Ah0o34AIiArBXNob3dTdWUOICigGAApI5PnCXPDggB9Y+UgzeAJWKDNAClgVoQDAW8uZAMBKX3kCB0Bbi5gGeQBHQBuoBDAhUC8ovsEaGlkZVDiDfsAaOACGgEoKSBmASl95A1RBGRlbGV04gBiACjHhwFpKUDLA2E+MClABwB04ytr5xHBACIihEH/5xA9BWU9IkRlbOACl4L6AGjiF/rnd1QAaOIZ+gJqKXtA+gJrPWpC+gBq4giFoiznA1TgCSQIZCIpKXtrPWou4gH64AkkguwAIqApgA0AfUI9AGvgCHSiWwAiICsAc+IC+gFrLqAY4g36Rn8DZyxpKeIA+uAJWKDN4gP6AGuG7QEpfeII+gFqLuIG+gBqoBDAhUC8ooriMfoBfTs=";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string SaveScript { get; set; }	
		public string SaveDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1908);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1908);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1908);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_92);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_92);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveScript}}", string.IsNullOrEmpty(SaveScript)==false ? SaveScript : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeSaveAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeSaveAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_408="FzxsaSBzdHlsZT0iY29sb3I6Z3JheSI+ICAAFDxoNj5BZGQgRGF0YSBUeXBlPC9oNmAZFSA8cCBjbGFzcz0idWktbGktYXNpZGWANUAAFTxhIG9uY2xpY2s9InJldHVybiBnZXRARwBUIEYVU2F2ZVZpZXcoMCwge3tQYWdlTm99fUALB0l0ZW1zUGVyQBMAfWARQDMFSW5kZXh9IA4fJ3t7VGVtcGxhdGVTdWZmaXh9fScsIHRydWUpOyIgaHIIZWY9IiMiICBkILAMLWFqYXg9ImZhbHNlIoARCHRoZW1lPSJiIoAOAnJvbEANBXV0dG9uIoASBW1pbmk9IkBQoDIFaW5saW5l4AUSCmNvbj0icGx1cyIggQygAABBISqgAAI8L2FgGgg8L3A+PC9saT4=";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_408);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_408);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeSaveDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeSaveDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1104="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAABQ8cCBjbGFzcz0idWktbGktYXNpZGWAJsAADXt7UmV2aXNpb25Ob319wBUCPC9w4AFECmxhYmVsIGZvcj0iQHYHVHlwZU5hbWXgBUcARCDgASBUIBocPHNwYW4gc3R5bGU9ImNvbG9yOiBSZWQ7Ij4qPC9AGwI+PC9gUeABYAhpbnB1dCB0eXAgMQh0ZXh0IiBuYW0gCwBkIFPgAG0DIGlkPeAGEQl2YWx1ZT0ie3tE4AIoBn19IiB7eyPgAxIKRGlzYWJsZWR9fWSgCSBJoAkDInt7L+ANK8ELQAAfbWF4bGVuZ3RoPSI1MCIgcGxhY2Vob2xkZXI9IiYjMTgGNztFbnRlcuEBBR8gTmFtZSoiIG9ua2V5cHJlc3M9InJldHVybiBjbGljaxpCdXR0b24oZXZlbnQsJ3t7I0FkZE1vZGV9fWJgGQJBZGTAlQN7ey9BwBwDe3teQcALgCgDU2F2ZeALKQUnKTsiIC/hAGNAAAI8L2yCGgA8IiILe3sjSXNEZWZhdWx04AHr4QSKB2NoZWNrYm944QeOAEnANwAi4QST4AIWAGNAOQNlZD0iYEIhZOACiQN7ey9JwEDgAXgDe3teSeAJFeA+juEBBgF7e+ALfOIL4eABzQQ+SXMgRIEoADziArnhAE0Be3shSQxBY3RpdmVWaXNpYmxlgNYAPMFlgBuAFMAA4B7ZgDoAIuEGZ8AV4RhmgCjgAXlhZeAHFEAA4DyO4QZm4Adn4Q1lgCUAIkFkgAoAPOENYyG3gB/hC2MMU2hvd0FkbWluSW5mb+AB7gA8RG7EigBmJUYJLXdlaWdodDogbEAGBWVyOyBjb2SgAHJkoBFQbGVhc2UgbG9naW4gYXMgVXMj0gZoYXZpbmcgYF4JIFJvbGUgdG8gUyN1ADyEzUE+QAADe3svU+ANhsAABHt7QWRkQMQBb27gAaMFe3tFZGl04AMVYlgEPC91bD4=";

		#endregion		

        #region List Section Properties

		public class IsActiveVisible	
		{
			public bool IsActive { get; set; }	
		}		
		public List<IsActiveVisible> IsActiveVisibleList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool DataTypeNameDisabled { get; set; }	
		public bool AddMode { get; set; }	
		public bool IsDefault { get; set; }	
		public bool ShowAdminInfo { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string RevisionNo { get; set; }	
		public string DataTypeName { get; set; }	
		public string AddAction { get; set; }	
		public string EditAction { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1104);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1104);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsActiveVisible}}";
			sectionEndTag = "{{/IsActiveVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (IsActiveVisibleList !=null))
			{
				foreach (var isactivevisible in IsActiveVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolIsActiveVisibleSection(isactivevisible , sectionValueInstance);
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(IsActiveVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DataTypeNameDisabled}}";
			sectionEndTag = "{{/DataTypeNameDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DataTypeNameDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddMode}}";
			sectionEndTag = "{{/AddMode}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^AddMode}}";
            invertedSectionEndTag = "{{/AddMode}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddMode ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, AddMode ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDefault}}";
			sectionEndTag = "{{/IsDefault}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsDefault}}";
            invertedSectionEndTag = "{{/IsDefault}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsDefault ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, IsDefault ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ShowAdminInfo}}";
			sectionEndTag = "{{/ShowAdminInfo}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ShowAdminInfo ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{RevisionNo}}", string.IsNullOrEmpty(RevisionNo)==false ? RevisionNo : "");
			template = template.Replace("{{DataTypeName}}", string.IsNullOrEmpty(DataTypeName)==false ? DataTypeName : "");
			template = template.Replace("{{AddAction}}", string.IsNullOrEmpty(AddAction)==false ? AddAction : "");
			template = template.Replace("{{EditAction}}", string.IsNullOrEmpty(EditAction)==false ? EditAction : "");
			return template;
		}

		protected string ProcessListIsActiveVisibleSection(IsActiveVisible isactivevisible, string template)
		{

			return template;
		}

		protected string ProcessBoolIsActiveVisibleSection(IsActiveVisible isactivevisible, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsActive}}";
			sectionEndTag = "{{/IsActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsActive}}";
            invertedSectionEndTag = "{{/IsActive}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, isactivevisible.IsActive ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, isactivevisible.IsActive ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeSaveDetailAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeSaveDetailAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_472="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoQQWRkRGF0YVR5cGUiIG5hbWXgDBgCdHlwIBgIc3VibWl0IiBkIDAELXRoZW0gExdiIiBvbmNsaWNrPSJyZXR1cm4gc2F2ZUQgIkBTDSgwLCB7e1BhZ2VOb319QAsHSXRlbXNQZXJAEwB9YBFAKwVJbmRleH0gDhUne3tUZW1wbGF0ZVN1ZmZpeH19JykiIEMUI0FkZEFjdGlvbkRpc2FibGVkfX1koAkCPSJkoAkEInt7L0HgCSgFPkFkZDwvgPHhADDgAwACPC9koWVAAAE8ZOEEaQFibEFKAGLhAWpAAAA8gE0BIHThEBoBYyLhCBoGcmVmcmVzaEDxQR0FRm9ybSh74Tke4AjSBUNhbmNlbOAI8eAB5YAJBTwvZGl2Pg==";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool AddActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_472);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_472);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddActionDisabled}}";
			sectionEndTag = "{{/AddActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeSaveDetailEdit	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeSaveDetailEdit.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_548="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoRU2F2ZURhdGFUeXBlIiBuYW1l4A0ZAnR5cCAZCHN1Ym1pdCIgZCAxBC10aGVtIBMUYiIgb25jbGljaz0icmV0dXJuIHNh4AFUESh7e0lkfX0sIHt7UGFnZU5vfWALB0l0ZW1zUGVyQBMAfWARAEQgUwVJbmRleH0gDhUne3tUZW1wbGF0ZVN1ZmZpeH19JykiIEMBI1MgsBBBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9T4AopAT5TIBQBPC+A++EAOuADAAI8L2Shb0AAATxk4QRzAWJsQVQAYuEBdEAAADyATQEgdOEQIgFjIuEIIgZyZWZyZXNoQPRBegVGb3JtKHvhOSHgCNIFQ2FuY2Vs4Ajx4AHl4AIJAGTgA/jgK9QEZGVsZXTiAE4gzwFJZOFC+QJEZWwgXeEd+wBE4AwrAT5EYBbhCA1AAIEHBTwvZGl2Pg==";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveActionDisabled { get; set; }	
		public bool DeleteActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string Id { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_548);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_548);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveActionDisabled}}";
			sectionEndTag = "{{/SaveActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DeleteActionDisabled}}";
			sectionEndTag = "{{/DeleteActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DeleteActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{Id}}", string.IsNullOrEmpty(Id)==false ? Id : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateDataTypeView	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlDataType/DataTypeView.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_560="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAAk8c3BhbiBzdHlsIE8fcG9zaXRpb246YWJzb2x1dGU7dG9wOjVweDtyaWdodDoBMTAgCoBDwAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXBHJlc2hEINgEVHlwZUwg1RYoe3tQYWdlTm99fSwge3tJdGVtc1BlckATAH1gEUAsBUluZGV4fSAOFid7e1RlbXBsYXRlU3VmZml4fX0nKTsigNqBKgZidXR0b24igBIGaW5saW5lPeEDKQNtaW5p4AQQAGkhLAA9IKFgmgAioDcgEyD7Cz0ibm90ZXh0Ij5SZWAeAjwvYeEBKAAvQSngAA4dUGxlYXNlIGxvZ2luIGFzIFVzZXIgSGF2aW5nIEFkIHUNIFJvbGUgdG8gVmlldyBA1gEgVCEEAihzKUE6CTwvbGk+PC91bD4=";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_560);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_560);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategory	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategory.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_216="HzxkaXYgaWQ9ImluZm9DYXRlZ29yeVNhdmVTZWN0aW9uD3t7RGF0YUluZGV4fX0iIGQgDBItcm9sZT0iY29sbGFwc2libGUigBcAY4ARBmVkPSJ7e1MgQwpFeHBhbmR9fSI+ICAAAjxoM2AHQAALTWFuYWdlIEluZm8gwHUCPC9ogCCAPAdEZXRhaWx9fUAuBXt7TGlzdMARBTwvZGl2Pg==";

		#endregion		

        #region PlaceHolder Properties

		public string DataIndex { get; set; }	
		public string SaveExpand { get; set; }	
		public string SaveDetail { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_216);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_216);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{SaveExpand}}", string.IsNullOrEmpty(SaveExpand)==false ? SaveExpand : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategoryList	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategoryList.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlInfoCategory/InfoCategoryListScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlInfoCategory/InfoCategoryListScript.min.js";										
		private const string TemplateSource_112="H3t7TGlzdFNjcmlwdH19PGRpdiBpZD0iaW5mb0NhdGVnAm9yeUAgE1ZpZXd7e0RhdGFJbmRleH19Ij4gIAABe3tAHAVEZXRhaWwgPQQvZGl2Pg==";
		private const string MinScriptSource_916="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaEluZm9DYXRlZ29yeUxpc3QoYSxjLGksayxkKXt2A2FyIGggM0AHH2Y9Iml0ZW1zUGVyUGFnZSIraTtpZigkKCIjIitmKS5sCmVuZ3RoPjApe2g9oBQFKyIgb3B0IGoSOnNlbGVjdGVkIikudmFsKCl9diBYGGc9J3sicGFnZU5vIiA6ICcrYTtnKz0nLCDgBWdgGgFoO6AaCGRhdGFJbmRleIAXAWk7oBcgmRVwbGF0ZVN1ZmZpeCI6IicraysnIic7oB8GaGlkZURpcyAjBnkiOicrZDsgFwIifSJg2QhlPSJHZXRJbmbhBAcEVmlldyJgHwJiPXfhBkYBKytgGABqQMEBaWRAUAliKycsIm1ldGhvIA4gfABlIHwALCDdA3JhbXNAHx9nKyJ9IjskLmFqYXgoe3R5cGU6IlBPU1QiLGNvbnRlbgJ0VHlAEgdhcHBsaWNhdCE0Hy9qc29uOyBjaGFyc2V0PXV0Zi04Iix1cmw6Ii8qIUBTDWVydmljZVVybEAqLyIsQRcBOmpgBgBUYEkAaiA9CSIsc3VjY2VzczrB9AMobCl7QcwDbT1sOyG9H2wuaGFzT3duUHJvcGVydHkoImVycm9yIik9PWZhbHNlASl74AwkDmQiKSl7bT1sLmR9ZWxzZeANJCJbA3VsdCKgKYANAX19IE8AbeAIdARodG1sIiAoBiQoIiNpbmbhBIYEIitpKS5AIEA3ICcBKTvgECUAbCK9BHZpZXcoIHti2gEiKeARLBJ0cmlnZ2VyKCJjcmVhdGUiKX19YMsFc2hvd0VyIQQhHABlQAcKLm5hbWUrIjoiK2ygEABtIVEQYWdlKX19fSk7cmV0dXJuIGZBLgF9Ow==";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ListScript { get; set; }	
		public string DataIndex { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_916);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_916);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_916);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_112);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_112);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListScript}}", string.IsNullOrEmpty(ListScript)==false ? ListScript : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategoryListDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategoryListDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_212="Hzx1bCBpZD0iaW5mb0NhdGVnb3J5TGlzdHt7RGF0YUluB2RleH19IiBkIAwHLXJvbGU9ImwgHQR2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVAL8A+DmZpbHRlcj0iZmFsc2UiPiAsAiB7e0CSCkl0ZW19fTwvdWw+";

		#endregion		

        #region PlaceHolder Properties

		public string DataIndex { get; set; }	
		public string ListItem { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_212);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_212);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{ListItem}}", string.IsNullOrEmpty(ListItem)==false ? ListItem : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategoryListDetailEmpty	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategoryListDetailEmpty.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_424="BDxsaT4gIAAfPHNwYW4gc3R5bGU9InBvc2l0aW9uOmFic29sdXRlO3QOb3A6NXB4O3JpZ2h0OjEwIAoAImA3QAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXH3Jlc2hJbmZvQ2F0ZWdvcnlMaXN0KHt7UGFnZU5vfX0sCiB7e0l0ZW1zUGVyQBMAfWARCURhdGFJbmRleH0gDhMne3tUZW1wbGF0ZVN1ZmZpeH19J0A2BkhpZGVEaXMgGAd5fX0pOyIgZCA1DS1yb2xlPSJidXR0b24igBIEaW5saW4gFAR0cnVlIoASA21pbmngBBAEaWNvbj0gtmCvACKAJEATIQwLPSJub3RleHQiPlJlYB4CPC9hYP0BPC9BNmAKB05vIEluZm8gwOUNKHMpIEZvdW5kPC9saT4=";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string HideDisplay { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_424);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_424);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{HideDisplay}}", string.IsNullOrEmpty(HideDisplay)==false ? HideDisplay : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategoryListDetailItem	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategoryListDetailItem.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_600="BDxsaT4gIAACPGgzYAdAAB88c3Ryb25nPnt7SW5mb0NhdGVnb3J5TmFtZX19PC9zdGAcQCjAAAt7eyNJc0RlZmF1bHQgIh9zcGFuIHN0eWxlPSJjb2xvcjpibHVlO2ZvbnQtd2VpZwtodDpib2xkOyI+KESANAApIFYgNAQ+e3svSeABSMBeYABgYwlJbkFjdGl2ZX194ApkA3JlZDvgC2PANAAp4ANkwBSAZQI8L2iA/hI8cD57e0NyZWF0ZWRCeX19PC9wYOkgfQNFZGl0QH0Bb26AM0AAFTxwIGNsYXNzPSJ1aS1saS1hc2lkZSJgMcAAGTxidXR0b24gZGF0YS1pbmxpbmU9InRydWUigBIJYWpheD0iZmFsc8ARA21pbmngBCIGaWNvbj0iZSB/FyIgIG9uY2xpY2s9InJldHVybiBnZXRJbuEBpRlTYXZlVmlldyh7e0lkfX0sIHt7UGFnZU5vfWALB0l0ZW1zUGVyQBMAfWARAEQglwVJbmRleH0gDhUne3tUZW1wbGF0ZVN1ZmZpeH19JywgQK0FKTsiID5FIIIBPC+A0OAA5AA84QAiAS9FIB/BIgQ8L2xpPg==";

		#endregion		

        #region List Section Properties

		public class EditAction	
		{
			public string Id { get; set; }	
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
		}		
		public List<EditAction> EditActionList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool IsDefault { get; set; }	
		public bool IsInActive { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string InfoCategoryName { get; set; }	
		public string CreatedBy { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_600);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_600);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#EditAction}}";
			sectionEndTag = "{{/EditAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (EditActionList !=null))
			{
				foreach (var editaction in EditActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(editaction.Id)==false ? editaction.Id : "");
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(editaction.PageNo)==false ? editaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(editaction.ItemsPerPage)==false ? editaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(editaction.DataIndex)==false ? editaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(editaction.TemplateSuffix)==false ? editaction.TemplateSuffix : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(EditActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDefault}}";
			sectionEndTag = "{{/IsDefault}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsDefault ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsInActive}}";
			sectionEndTag = "{{/IsInActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsInActive ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{InfoCategoryName}}", string.IsNullOrEmpty(InfoCategoryName)==false ? InfoCategoryName : "");
			template = template.Replace("{{CreatedBy}}", string.IsNullOrEmpty(CreatedBy)==false ? CreatedBy : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategorySave	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategorySave.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlInfoCategory/InfoCategorySaveScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlInfoCategory/InfoCategorySaveScript.min.js";										
		private const string TemplateSource_176="H3t7U2F2ZVNjcmlwdH19IDxkaXYgaWQ9ImluZm9DYXRlBGdvcnlTICEWVmlld3t7RGF0YUluZGV4fX0iIHt7I1OgGR9IaWRkZW59fXN0eWxlPSJkaXNwbGF5OiBub25lOyJ7ewEvU+AGKAE+ICAAAnt7UyAWDURldGFpbH19PC9kaXY+";
		private const string MinScriptSource_2060="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaEluZm9DYXRlZ29yeUZvcm0oYyxlLGQsYixhKXtpCGYodHlwZW9mKOAKLAdMaXN0KT09IsBQAyIpe3LgCVJAJQAo4AFSBjsvKiFAUmVgeRJDYWxsYmFja0AqL30kKCIjaW5m4ACJGlNhdmVWaWV3IitkKS5odG1sKCIiKTskKCIjaeACJUBcA1NlY3QgzgAiQCgRdHJpZ2dlcigiZXhwYW5kIil9wJwGIGdldEluZuAIXxkoaixhLGMsZyxrLGQsaCl7dmFyIGY9J3siaeACLBZJZCIgOiAnK2o7Zis9JywgInBhZ2VOb4AUAWE7oBQRaXRlbXNQZXJQYWdlIjonK2M7oBgIZGF0YUluZGV4gDABZzugFyAvFXBsYXRlU3VmZml4IjoiJytrKyciJzugHwZoaWRlRGlzICMAeWAcAGTAHAMifSI7QKgIZT0iR2V0SW5m4AjWACJgHwJiPXfhBv8BKytgGABpYOEAZEClCWIrJywibWV0aG8gDiCBAGUgZAAsIOADcmFtc0AfDmYrIn0iOyQuYWpheCh7dCINEDoiUE9TVCIsY29udGVudFR5QBIHYXBwbGljYXQhiRovanNvbjsgY2hhcnNldD11dGYtOCIsdXJsOiJB/A5TZXJ2aWNlVXJsQCovIixBHAE6aWAGAFRgSQBqID0JIixzdWNjZXNzOsG/AyhsKXtA8h9tPWw7aWYobC5oYXNPd25Qcm9wZXJ0eSgiZXJyb3IiKQc9PWZhbHNlKUK64AkkDmQiKSl7bT1sLmR9ZWxzZeANJCMUA3VsdCKgKYANAX19IHQAbeAIdEKYACIgKAYkKCIjaW5m4QmGAitnKYK9AG1gBgEpO+AUKQB0wr4GY3JlYXRlIuIJ8ABT4wAWIFgJc2hvdygic2xvdyApIJ8PaD09dHJ1ZSl7JCgiI2luZuAEjgBTwyYgOAB0wGcAZcMmAX19YQ8AcyBRAUVyIUghOwBlQAcKLm5hbWUrIjoiK2ygEABtIZUDYWdlKSAyCSk7cmV0dXJuIGZBcgB9waoGIHNhdmVJbuABfgwobSxiLGUsaSxuLGopYcUBYT1AqAA7QdABYz3gB60NTmFtZSIraSkudmFsKClA2AtjLmxlbmd0aD09MCngAqMHIlBsZWFzZSAihAdlciB0aGUgQ6THAyIpO2GCCAF9diPQAGyAC0BKBiQoIiNpbmbhABoFSXNEZWZhQe9AcRFpcygiOmNoZWNrZWQiKSl7bD1Ap2BDAWY9YljgDUMHQWN0aXZlIisgtOAHQgFmPWBCIZ4AYcGeIAsAdCNHA29mKHMhbwlQcm9ncmVzcyk95QRK4AMaASgpYIIAaGO1AG7gAbwDTmFtZURTAyInK2MjtgInO2iEngFpc8DUZLYBbDvgABegqGAWAWY7wBYAbuABUgBJpOoBbTsgHAAiI/lBowJkPXfkDUcDZz0iUyKRAUlu4AFAACJgMQBrYKokUQEnKyS1ASwi5AJgAGcgqQAs5AJgA2grIn3kWGAAa+QZYAJvKXtA6wdwPW87aWYob+MI6+QKYOAJJAhkIikpe3A9by7kAWDgCSSEUgAioCmADQB9QiEAcOAIdKNtACIgKwBzIisBU3VlFSAooBgAKSOCZ0sCSW5m4gDTZ8UAYuMAcQB9Y9og0+AJXqDTAClgXIP4AXAuY/gBKX3kCBIBby5gGeQBEgBvoBDAi0DCotgAaCa64g7YAGjgAhoi2AN9fSl95A1GB2RlbGV0ZUlu4AHYAyhpLGFnrANqLGQpQNEDaT4wKUAHAHTjK07nC+YirwNpO2Yr4gD55xBBBGU9IkRl4AefASI7QkGDpmBL53xcAGjiGfsCayl7QLkCbD1rQvsAa+IIhqIn5wNc4AkkAmQiKUUxAWsu4gH74Akkgu0AIqApgA0AfUI4AGzgCHSiVgAiICsAc+IC+wFsLqAY4hH7AGHiACTiAPvgCV6g0+ID+wBshuMBKX3iCPsBay7iBvsAa6AQwItAwqKF4jH7AX07";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptRefreshCallback { get; set; }	
		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveViewHidden { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string SaveScript { get; set; }	
		public string DataIndex { get; set; }	
		public string SaveDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2060);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2060);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2060);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@RefreshCallback@*/", string.IsNullOrEmpty(ScriptRefreshCallback)==false ? ScriptRefreshCallback : "");
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_176);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_176);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveViewHidden}}";
			sectionEndTag = "{{/SaveViewHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveViewHidden ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveScript}}", string.IsNullOrEmpty(SaveScript)==false ? SaveScript : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategorySaveAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategorySaveAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_416="FzxsaSBzdHlsZT0iY29sb3I6Z3JheSI+ICAAGDxoNj5BZGQgSW5mbyBDYXRlZ29yeTwvaDZgHRUgPHAgY2xhc3M9InVpLWxpLWFzaWRlgDlAABk8YSBvbmNsaWNrPSJyZXR1cm4gZ2V0SW5mb8BKFVNhdmVWaWV3KDAsIHt7UGFnZU5vfX1ACwdJdGVtc1BlckATAH1gEQlEYXRhSW5kZXh9IA4fJ3t7VGVtcGxhdGVTdWZmaXh9fScsIHRydWUpOyIgaHIIZWY9IiMiICBkIDQMLWFqYXg9ImZhbHNlIoARCHRoZW1lPSJiIoAOAnJvbEANBXV0dG9uIoASBW1pbmk9IkBQoDIFaW5saW5l4AUSCmNvbj0icGx1cyIggRCgAABBITKgAAI8L2FgGgg8L3A+PC9saT4=";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_416);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_416);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategorySaveDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategorySaveDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1160="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAABQ8cCBjbGFzcz0idWktbGktYXNpZGWAJsAADXt7UmV2aXNpb25Ob319wBUCPC9w4AFEHWxhYmVsIGZvcj0iaW5mb0NhdGVnb3J5TmFtZXt7RCDYBkluZGV4fX3gBVgESW5mbyDAKxs8c3BhbiBzdHlsZT0iY29sb3I6UmVkOyI+KjwvQBoCPjwvYGXgAXQIaW5wdXQgdHlwIDAIdGV4dCIgbmFtIAsBaW7gE4EFIGlkPSJp4BUiCnZhbHVlPSJ7e0lu4AVKAH0gPwUge3sjSW7gBRcKRGlzYWJsZWR9fWSgCQI9ImSgCQUie3svSW7gDy/BTkAAH21heGxlbmd0aD0iNTAiIHBsYWNlaG9sZGVyPSImIzE4CTc7RW50ZXIgSW7hAjcfIE5hbWUqIiBvbmtleXByZXNzPSJyZXR1cm4gY2xpY2saQnV0dG9uKGV2ZW50LCd7eyNBZGRNb2RlfX1iYBkEQWRkSW7gAZ0De3svQcAgA3t7XkHAC4AsBVNhdmVJbuAPLeEC6AQnKTsiL+EAqkAAAjwvbIJ1Ent7I0lzRGVmYXVsdFZpc2libGWBBgA84gKW4AMkgB1AAOEE8gdjaGVja2JveOEL9gBJwGAAe+ADlAAi4Qj74A8nAGNAWwNlZD0iYGQhw+ABywN7ey9JwF3gAZlgrwNOb3REgNfgARjgX7LgBqDgDIoBbGHjDKXhDgQDPklzIKDOADzjAnrhAsfhAQjhBMdhCwdBY3RpdmVWaeET34Aj4QErQADhIi+APuQFX+EK4eAMJuEI4AAg4QZAgDXgAZsBe3sitQFJbuAHFkAA4F2y4Aaf4AmK4RHegLThBShB3YAXADzhENyAH+ETwwtTaG93VXNlckluZm/hASsAPEWvxcoAZiabCS13ZWlnaHQ6IGxABgVlcjsgY29F4AEgcmXhEFBsZWFzZSBsb2dpbiB0byBTJGUAPIX0QV9AAAN7ey9T4AxrwAAEe3tBZGRAsAFvbuABiAV7e0VkaXTgAxVivQQ8L3VsPg==";

		#endregion		

        #region List Section Properties

		public class IsDefaultVisible	
		{
			public string DataIndex { get; set; }	

			public class IsDefault	
			{
			public string DataIndex { get; set; }	
			}
			public List<IsDefault> IsDefaultList { get; set; }	

			public class IsNotDefault	
			{
			public string DataIndex { get; set; }	
			}
			public List<IsNotDefault> IsNotDefaultList { get; set; }	
		}		
		public List<IsDefaultVisible> IsDefaultVisibleList { get; set; }	

		public class IsActiveVisible	
		{
			public string DataIndex { get; set; }	

			public class IsActive	
			{
			public string DataIndex { get; set; }	
			}
			public List<IsActive> IsActiveList { get; set; }	

			public class IsInActive	
			{
			public string DataIndex { get; set; }	
			}
			public List<IsInActive> IsInActiveList { get; set; }	
		}		
		public List<IsActiveVisible> IsActiveVisibleList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool InfoCategoryNameDisabled { get; set; }	
		public bool AddMode { get; set; }	
		public bool ShowUserInfo { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string RevisionNo { get; set; }	
		public string DataIndex { get; set; }	
		public string InfoCategoryName { get; set; }	
		public string AddAction { get; set; }	
		public string EditAction { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1160);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1160);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsDefaultVisible}}";
			sectionEndTag = "{{/IsDefaultVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (IsDefaultVisibleList !=null))
			{
				foreach (var isdefaultvisible in IsDefaultVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessListIsDefaultVisibleSection(isdefaultvisible , sectionValueInstance);
					sectionValueInstance = ProcessListIsDefaultVisibleSection(isdefaultvisible , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isdefaultvisible.DataIndex)==false ? isdefaultvisible.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(IsDefaultVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsActiveVisible}}";
			sectionEndTag = "{{/IsActiveVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (IsActiveVisibleList !=null))
			{
				foreach (var isactivevisible in IsActiveVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessListIsActiveVisibleSection(isactivevisible , sectionValueInstance);
					sectionValueInstance = ProcessListIsActiveVisibleSection(isactivevisible , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isactivevisible.DataIndex)==false ? isactivevisible.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(IsActiveVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategoryNameDisabled}}";
			sectionEndTag = "{{/InfoCategoryNameDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, InfoCategoryNameDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddMode}}";
			sectionEndTag = "{{/AddMode}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^AddMode}}";
            invertedSectionEndTag = "{{/AddMode}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddMode ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, AddMode ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ShowUserInfo}}";
			sectionEndTag = "{{/ShowUserInfo}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ShowUserInfo ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{RevisionNo}}", string.IsNullOrEmpty(RevisionNo)==false ? RevisionNo : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{InfoCategoryName}}", string.IsNullOrEmpty(InfoCategoryName)==false ? InfoCategoryName : "");
			template = template.Replace("{{AddAction}}", string.IsNullOrEmpty(AddAction)==false ? AddAction : "");
			template = template.Replace("{{EditAction}}", string.IsNullOrEmpty(EditAction)==false ? EditAction : "");
			return template;
		}

		protected string ProcessListIsDefaultVisibleSection(IsDefaultVisible isdefaultvisible, string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsDefault}}";
			sectionEndTag = "{{/IsDefault}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (isdefaultvisible.IsDefaultList !=null))
			{
				foreach (var isdefault in isdefaultvisible.IsDefaultList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isdefault.DataIndex)==false ? isdefault.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(isdefaultvisible.IsDefaultList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsNotDefault}}";
			sectionEndTag = "{{/IsNotDefault}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (isdefaultvisible.IsNotDefaultList !=null))
			{
				foreach (var isnotdefault in isdefaultvisible.IsNotDefaultList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isnotdefault.DataIndex)==false ? isnotdefault.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(isdefaultvisible.IsNotDefaultList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessListIsActiveVisibleSection(IsActiveVisible isactivevisible, string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsActive}}";
			sectionEndTag = "{{/IsActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (isactivevisible.IsActiveList !=null))
			{
				foreach (var isactive in isactivevisible.IsActiveList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isactive.DataIndex)==false ? isactive.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(isactivevisible.IsActiveList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsInActive}}";
			sectionEndTag = "{{/IsInActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (isactivevisible.IsInActiveList !=null))
			{
				foreach (var isinactive in isactivevisible.IsInActiveList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(isinactive.DataIndex)==false ? isinactive.DataIndex : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(isactivevisible.IsInActiveList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolIsDefaultVisibleSection(IsDefaultVisible isdefaultvisible, string template)
		{
			
			return template;
		}

		protected string ProcessBoolIsActiveVisibleSection(IsActiveVisible isactivevisible, string template)
		{
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategorySaveDetailAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategorySaveDetailAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_500="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoUQWRkSW5mb0NhdGVnb3J5IiBuYW1l4BAcAnR5cCAcEHN1Ym1pdCIgZGF0YS10aGVtIBMYYiIgb25jbGljaz0icmV0dXJuIHNhdmVJbuABWw0oMCwge3tQYWdlTm99fUALB0l0ZW1zUGVyQBMAfWARAEQgUgVJbmRleH0gDhMne3tUZW1wbGF0ZVN1ZmZpeH19J0A2BkhpZGVEaXMgGAR5fX0pIiBUFCNBZGRBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9B4AkoBT5BZGQ8L4EO4QBN4AMAAjwvZKGCQAABPGThBIYBYmxBZwBi4QGHQAAAPIBNASB04RAvAWMi4QgvCHJlZnJlc2hJbuEBMgVGb3JtKHvhSjPgCOcFQ2FuY2Vs4QgG4AH6gAkFPC9kaXY+";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool AddActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string HideDisplay { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_500);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_500);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddActionDisabled}}";
			sectionEndTag = "{{/AddActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{HideDisplay}}", string.IsNullOrEmpty(HideDisplay)==false ? HideDisplay : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategorySaveDetailEdit	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategorySaveDetailEdit.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_584="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoVU2F2ZUluZm9DYXRlZ29yeSIgbmFtZeARHQJ0eXAgHRBzdWJtaXQiIGRhdGEtdGhlbSATGGIiIG9uY2xpY2s9InJldHVybiBzYXZlSW7gAVwRKHt7SWR9fSwge3tQYWdlTm99YAsHSXRlbXNQZXJAEwB9YBEARCBXBUluZGV4fSAOEyd7e1RlbXBsYXRlU3VmZml4fX0nQEIGSGlkZURpcyAYBHl9fSkiIFQBI1MgzRBBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9T4AopAT5TIBQBPC+BGOEAV+ADAAI8L2ShjEAAATxk4QSQAWJsQXEAYuEBkUAAADyATQEgdOEQNwFjIuEINwhyZWZyZXNoSW7hAToFRm9ybSh74Uo24AjnBUNhbmNlbOEIBuAB+uACCQBk4QMN4CvpB2RlbGV0ZUlu4AHoIOQBSWTiUyMBRGVAcuIdJQBE4AwrAT5EYBbhCCJAAIEcBTwvZGl2Pg==";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveActionDisabled { get; set; }	
		public bool DeleteActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string Id { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string HideDisplay { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_584);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_584);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveActionDisabled}}";
			sectionEndTag = "{{/SaveActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DeleteActionDisabled}}";
			sectionEndTag = "{{/DeleteActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DeleteActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{Id}}", string.IsNullOrEmpty(Id)==false ? Id : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{HideDisplay}}", string.IsNullOrEmpty(HideDisplay)==false ? HideDisplay : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoCategoryView	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoCategory/InfoCategoryView.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_556="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAAk8c3BhbiBzdHlsIE8fcG9zaXRpb246YWJzb2x1dGU7dG9wOjVweDtyaWdodDoBMTAgCoBDwAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXEHJlc2hJbmZvQ2F0ZWdvcnlMINkWKHt7UGFnZU5vfX0sIHt7SXRlbXNQZXJAEwB9YBEARCEJBUluZGV4fSAOEyd7e1RlbXBsYXRlU3VmZml4fX0nQDYGSGlkZURpcyAYBXl9fSk7IoDvgT8GYnV0dG9uIoASBmlubGluZT3hAz4DbWluaeAEEABpIUEAPSC2YK8AIqA3IBMhEAs9Im5vdGV4dCI+UmVgHgI8L2HhAT0AL0E+4AAOGVBsZWFzZSBsb2dpbiB0byBWaWV3IEluZm8gwP8CKHMpQTkJPC9saT48L3VsPg==";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string HideDisplay { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_556);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_556);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{HideDisplay}}", string.IsNullOrEmpty(HideDisplay)==false ? HideDisplay : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_36="FHt7U2F2ZURldGFpbH19IHt7TGlzdKAOAX0g";

		#endregion		

        #region PlaceHolder Properties

		public string SaveDetail { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_36);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_36);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailList	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailList.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailListScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailListScript.min.js";										
		private const string TemplateSource_108="H3t7TGlzdFNjcmlwdH19PGRpdiBpZD0iaW5mb0RldGFpAWxMIB4XVmlld3t7SW5mb1NlY3Rpb25JZH19Ij4gIAABe3tAP4AqID8EL2Rpdj4=";
		private const string MinScriptSource_1024="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZXRyH2lldmVJbmZvRGV0YWlsTGlzdChmLGMsZSxkLGIsYSl7FiQoZikuYnV0dG9uKCJkaXNhYmxlIik7IDoCdXJuIEEFZnJlc2hJ4AVAAGPgAD4AfeACa+ALKRJhLGMsaCxrLGope3ZhciBnPWM7QAcfZT0iaXRlbXNQZXJQYWdlIitoO2lmKCQoIiMiK2UpLmwKZW5ndGg+MCl7Zz2gFAUrIiBvcHQg1BI6c2VsZWN0ZWQiKS52YWwoKX12IFgYZj0neyJwYWdlTm8iIDogJythO2YrPScsIOAFZ2AaAWc7oBoIZGF0YUluZGV4gBcBaDugFyCZCXBsYXRlU3VmZmlgHAgiJytrKyciJzugIQRpbmZvUyCFII8BSWSAPQFqOyAbAiJ9ImDfB2Q9IkdldElu4QN2BFZpZXciYB0CYj134Qa0ASsrYBgAaUDFB2pzb25ycGMiIHURMi4wIiwibWV0aG9kIjoiJytkIIQALCDnDXJhbXMiOicrZisnLCJpIBsfJytiKyJ9IjskLmFqYXgoe3R5cGU6IlBPU1QiLGNvbnQEZW50VHlAEgdhcHBsaWNhdCC5Hy9qc29uOyBjaGFyc2V0PXV0Zi04Iix1cmw6Ii8qIUBTDWVydmljZVVybEAqLyIsQSwBOmlgBgBUYEkAaiA9CSIsc3VjY2VzczrCBwMobCl7QeECbT1sQdIfbC5oYXNPd25Qcm9wZXJ0eSgiZXJyb3IiKT09ZmFsc2UBKXsh9+AJJA5kIikpe209bC5kfWVsc2XgDSQimAN1bHQioCmADQF9fSBPAG3gCHQEaHRtbCIgKAUkKCIjaW7hA5UEIitqKS5AHkA1ICUBKTvgDiMAbCM5BHZpZXcoIHdi6QEiKeAPKhJ0cmlnZ2VyKCJjcmVhdGUiKX19YMUFc2hvd0VyIP4g8QBlQAcKLm5hbWUrIjoiK2ygEABtIUsHYWdlKX19fSnDgwBmQSgBfTs=";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ListScript { get; set; }	
		public string InfoSectionId { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1024);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1024);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1024);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_108);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_108);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListScript}}", string.IsNullOrEmpty(ListScript)==false ? ListScript : "");
			template = template.Replace("{{InfoSectionId}}", string.IsNullOrEmpty(InfoSectionId)==false ? InfoSectionId : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailListDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailListDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_216="GDx1bCBpZD0iaW5mb0RldGFpbExpc3R7e0kgDxhTZWN0aW9uSWR9fSIgZGF0YS1yb2xlPSJsICEEdmlldyKAFAtpbnNldD0idHJ1ZSKAEQh0aGVtZT0iYyKADgZjb250ZW50wBYCYiIgIABgTwlkaXZpZGVydGhlQC/APg5maWx0ZXI9ImZhbHNlIj4gLAMge3tMIHQKSXRlbX19PC91bD4=";

		#endregion		

        #region PlaceHolder Properties

		public string InfoSectionId { get; set; }	
		public string ListItem { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_216);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_216);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{InfoSectionId}}", string.IsNullOrEmpty(InfoSectionId)==false ? InfoSectionId : "");
			template = template.Replace("{{ListItem}}", string.IsNullOrEmpty(ListItem)==false ? ListItem : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailListDetailEmpty	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailListDetailEmpty.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_420="BDxsaT4gIAAfPHNwYW4gc3R5bGU9InBvc2l0aW9uOmFic29sdXRlO3QOb3A6NXB4O3JpZ2h0OjEwIAoAImA3QAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXH3Jlc2hJbmZvRGV0YWlsTGlzdCh7e1BhZ2VOb319LCB7CHtJdGVtc1BlckATAH1gEQlEYXRhSW5kZXh9IA4TJ3t7VGVtcGxhdGVTdWZmaXh9fSdgNiBTA1NlY3QgqAhJZH19KTsiIGQgNw0tcm9sZT0iYnV0dG9uIoASBGlubGluIBQEdHJ1ZSKAEgNtaW5p4AQQBGljb249ILZgrwAigCRAEyEMCz0ibm90ZXh0Ij5SZWAeAjwvYWD9ATwvQTZgCgdObyBJbmZvIIDlDShzKSBGb3VuZDwvbGk+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string InfoSectionId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_420);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_420);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{InfoSectionId}}", string.IsNullOrEmpty(InfoSectionId)==false ? InfoSectionId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailListDetailItem	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailListDetailItem.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_636="HTxsaSBkYXRhLXJvbGU9Imxpc3QtZGl2aWRlciI+ICAAAjxoM2AHQAAdPHN0cm9uZz57e0luZm9EZXRhaWxOYW1lfX08L3N0YBoZIHt7I0lzSW5BY3RpdmV9fTxzcGFuIHN0eWwgXR9jb2xvcjogcmVkOyBmb250LXdlaWdodDogYm9sZDsiPgAowDcAKSBPIDcEPnt7L0ngAkxggwEvaICQIGgDRWRpdEBoAW9uoBsTcCBjbGFzcz0idWktbGktYXNpZGWAxkAABjxidXR0b26A7wxpbmxpbmU9InRydWUigBIJYWpheD0iZmFsc8ARBmljb249ImUgZgAigCIDbWluaeAEM0AhH3Bvcz0nbm90ZXh0JyBvbmNsaWNrPSJyZXR1cm4gZ2V0AUluwSwZU2F2ZVZpZXcoe3tJZH19LCB7e1BhZ2VOb31gCwdJdGVtc1BlckATAH1gEQBEIZoFSW5kZXh9IA4VJ3t7VGVtcGxhdGVTdWZmaXh9fScsIEDAQEgHSW5mb1NlY3QhDQhJZH19KTsiPkUguAE8L4D1YcQCPC9wYAcDe3svRSAbwTsHPC9saT48bGlgGwY8cD57e0luwLIHRGVzY3JpcHQgViFkADwgQAQ8L2xpPg==";

		#endregion		

        #region List Section Properties

		public class EditAction	
		{
			public string Id { get; set; }	
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
			public string InfoSectionId { get; set; }	
		}		
		public List<EditAction> EditActionList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool IsInActive { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string InfoDetailName { get; set; }	
		public string InfoDetailDescription { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_636);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_636);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#EditAction}}";
			sectionEndTag = "{{/EditAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (EditActionList !=null))
			{
				foreach (var editaction in EditActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(editaction.Id)==false ? editaction.Id : "");
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(editaction.PageNo)==false ? editaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(editaction.ItemsPerPage)==false ? editaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(editaction.DataIndex)==false ? editaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(editaction.TemplateSuffix)==false ? editaction.TemplateSuffix : "");
					sectionValueInstance = sectionValueInstance.Replace("{{InfoSectionId}}", string.IsNullOrEmpty(editaction.InfoSectionId)==false ? editaction.InfoSectionId : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(EditActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsInActive}}";
			sectionEndTag = "{{/IsInActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsInActive ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{InfoDetailName}}", string.IsNullOrEmpty(InfoDetailName)==false ? InfoDetailName : "");
			template = template.Replace("{{InfoDetailDescription}}", string.IsNullOrEmpty(InfoDetailDescription)==false ? InfoDetailDescription : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailSave	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailSave.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailSaveScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailSaveScript.min.js";										
		private const string TemplateSource_176="H3t7U2F2ZVNjcmlwdH19IDxkaXYgaWQ9ImluZm9EZXRhAmlsUyAfGlZpZXd7e0luZm9TZWN0aW9uSWR9fSIge3sjU6AdH0hpZGRlbn19c3R5bGU9ImRpc3BsYXk6IG5vbmU7Int7AS9T4AYoAT4gIAACe3tTIBaAZwd9fTwvZGl2Pg==";
		private const string MinScriptSource_2200="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaEluZm9EZXRhaWxGb3JtKGMsZSxkLGIsYSl7aWYoBnR5cGVvZijgCCoHTGlzdCk9PSLATAMiKXty4AdOQCMAKOABTgZ9JCgiI2luwGwaU2F2ZVZpZXciK2EpLmhpZGUoInNsb3ciKTsk4AQnQEUDU2VjdCCxYCoRdHJpZ2dlcigiZXhwYW5kIil9wIMFIGdldElu4AdfGShkLGEsYyxnLGssaCxqKXt2YXIgZj0neyJp4AAqFklkIiA6ICcrZDtmKz0nLCAicGFnZU5vgBQBYTugFAtpdGVtc1BlclBhZ2WAGgFjO6AaCGRhdGFJbmRleIAXAWc7oBcgMQlwbGF0ZVN1ZmZpYBwIIicraysnIic7oCEEaW5mb1OA3cCFAWo7IBsDIn0iO0CpB2U9IkdldElu4AfVACJgHQJiPXfhBt8BKytgGABpQOAHanNvbnJwYyIgdREyLjAiLCJtZXRob2QiOiInK2UghAAsIOcNcmFtcyI6JytmKycsImkgGxAnK2IrIn0iOyQuYWpheCh7dCIAEDoiUE9TVCIsY29udGVudFR5QBIHYXBwbGljYXQhlx8vanNvbjsgY2hhcnNldD11dGYtOCIsdXJsOiIvKiFAUw1lcnZpY2VVcmxAKi8iLEEsATppYAYAVGBJAGogPQkiLHN1Y2Nlc3M6wc0DKGwpe0EBH209bDtpZihsLmhhc093blByb3BlcnR5KCJlcnJvciIpBz09ZmFsc2UpQq3gCSQOZCIpKXttPWwuZH1lbHNl4A0kIwUDdWx0IqApgA0BfX0gdABt4Ah0BGh0bWwiICgFJCgiI2lu4QiVAytqKS5AIkA5ICkBKTvgEicAdMLICGNyZWF0ZSIpOyBxCGg9PXRydWUpe+ATOARvZ2dsZeMOLgBTI1YAU4JQASIrII0AdMBlAGXDLgF9fWEJBXNob3dFciFCITUAZUAHCi5uYW1lKyI6IitsoBAAbSGPA2FnZSkgMgkpO3JldHVybiBmQWwAfcGkBiBzYXZlSW7BBwwoZyxiLGUsbSxwLG8pYb0BYT1AzgA7QcgCZj0k4QQ0C05hbWUiKS52YWwoKUD6C2YubGVuZ3RoPT0wKeACnQciUGxlYXNlICJ4B2VyIHRoZSBJIG8AIISxAiBOYUBEATthggQBfXYj3ABp4AZnB0Rlc2NyaXB0IqjgA24CaS5s4Clu4ARLADvgA3UAZICBQMgAJOAE5gdJc0FjdGl2ZSB7EWlzKCI6Y2hlY2tlZCIpKXtkPUEcYLQAa+ARPgREZWxldEAw4Ag/AWs94AA/AGol2CI+ACTgBHoAU0X1Am5jZSA6gOsAPiFZAWo94BAkYYpC9wBhwoUgUgB0JAADb2YocyIuCVByb2dyZXNzKT3lBPjgAxoBKClgzgBsRH8BaW7kCNsCbztshWEBaW7CMwJOYW1lTiUZDmVuY29kZVVSSUNvbXBvbiHxAihmKSSoASc74Ag14QOIJbvgDTwAaeAFPABzoYggLwAnJevAUwBzwWBgFwFrO6AXAHPBPGAWAWo7oBYBaW7AuABJoDEBZzsgGgAiJTJC5AJjPXflDZEDaD0iUyOaAUluwD4AImAvAG5BKABqJQAAcuUOqABoIP8ALOUCqANsKycspagCYysiIHUAJOVVqABu5RmoAnEpe0D6AnI9cUL9AHHlCDPlCqjgCSQIZCIpKXtyPXEu5QGo4AkkhZoAIqApgA0AfUKwAHLgCHSkuwAiICsAcyK6AVN1Zl0gKKAYACkk0OgL/gBi5AC/AH1lJgFpZiAz5g56AClgWoVEAXIuZUQBKX3lCF4BcS5gGeUBXgBxoBDAiUDAo2UAaCkHAFDjDWUAaOACGgYoKX19fSl95Q2SAmRlbCQvAUluwmgAKMkAA2osaSlAzwFkPiQGINcAdOMr2ekTOgAiIo5CCQFiPeMOBANlPSJEZM0BSW7AnQAiYDEAaOMXBuh3rwBo4xkGAmspe0D8A2w9azshXABr4g806AOv4AkkCGQiKSl7bD1rLuMBBuAJJIL4ACKgKYANAH1CRQBs4Ah0omMAIiArAHPjAgYBbC6gGAAp4w4GAGFrMgNqLGkp4wAG4Alco9jjAwYAbIg6ASl94wgGAWsu4wYGAGugEMCJQMCikuMxBgF9Ow==";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveViewHidden { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string SaveScript { get; set; }	
		public string InfoSectionId { get; set; }	
		public string SaveDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2200);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2200);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2200);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_176);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_176);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveViewHidden}}";
			sectionEndTag = "{{/SaveViewHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveViewHidden ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveScript}}", string.IsNullOrEmpty(SaveScript)==false ? SaveScript : "");
			template = template.Replace("{{InfoSectionId}}", string.IsNullOrEmpty(InfoSectionId)==false ? InfoSectionId : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailSaveAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailSaveAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_452="GDxsaSBzdHlsZT0iY29sb3I6Z3JheTsiPiAgABI8aDY+SW5mbyBEZXRhaWw8L2g2YBcVIDxwIGNsYXNzPSJ1aS1saS1hc2lkZYAzQAAZPGEgb25jbGljaz0icmV0dXJuIGdldEluZm+ASBVTYXZlVmlldygwLCB7e1BhZ2VOb319QAsHSXRlbXNQZXJAEwB9YBEJRGF0YUluZGV4fSAOGSd7e1RlbXBsYXRlU3VmZml4fX0nLCB0cnVlYDwcbmZvU2VjdGlvbklkfX0pOyIgaHJlZj0iIyIgIGQgRwwtYWpheD0iZmFsc2UigBEIdGhlbWU9ImIigA4Ccm9sQA0FdXR0b24igBIFbWluaT0iQGOgMgVpbmxpbmXgBRIJY29uPSJwbHVzIoA0QBALcG9zPSdub3RleHQngTagAAJBZGSgCQMgPC9hYBoIPC9wPjwvbGk+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string InfoSectionId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_452);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_452);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{InfoSectionId}}", string.IsNullOrEmpty(InfoSectionId)==false ? InfoSectionId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailSaveDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailSaveDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1780="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAB88cD5EZXRhaWwgZm9yIEluZm8gU2VjdGlvbiA6IHt7SQNuZm9TgA8ITmFtZX19PC9wYDsCPC9sgEQAPOACTAd7eyNBZGRpdCA9A2FsQWNABwhWaXNpYmxlfX1AbEAAFjxkaXYgY2xhc3M9InVpLWxpLWFzaWRlgJnAAB88YSBocmVmPSIjIiBvbmNsaWNrPSJoaWRlTWVzc2FnZQooKTskKCcjaW5mb4C9HUFkdmFuY2VkJykudG9nZ2xlKCdzbG93Jyk7Ij5BZMCSAz88L2FgvUAABDwvZGl24AANA3t7L0HgACfgD7oDbGFiZWEoAz0iaW7AegNOYW1l4AW9AUluIUWAlx8gTmFtZTxzcGFuIHN0eWxlPSJjb2xvcjpSZWQ7Ij4qPAAvQBoCPjwvYFngAIwJPGlucHV0IHR5cCAwCHRleHQiIG5hbSALAWlu4AR1BSBpZD0iaeAGEwp2YWx1ZT0ie3tJbuADLAF9fWIVwAAfbWF4bGVuZ3RoPSI1MCIgcGxhY2Vob2xkZXI9IiYjMTgJNztFbnRlciBJbuAFywEqIiGQEGtleXByZXNzPSJyZXR1cm4gIp8IRm9jdXNPckNsIasPQnV0dG9uKGV2ZW50LCdpbsCHB0Rlc2NyaXB0Ih8CJywnQZwAL2Dz4gxMAjxwIOIXJgZ7e1JldmlzIE4BTm/iAmEAL4KiQADhDbLgAofhEbngAiQAPEGl4STAAHQhtANhcmVh4Qi34AJVACLhBr7gBBrhAp8CNDAwIaECcm93IW8AMiAJokgWbWluLWhlaWdodDoxNTBweDsiPnt7SW7hCmsEfX08L3SgkGFvQADhAHMAe+MDtOMErgA84wLaATxk4wIsQAAAPCSyBWlkPSJpbsBxw4MAIoR65ELK4AMA5AzWwRMCZGlzIskGeTpub25lO+EF2kAAAjxsaUDuAUlzQ+AKdmVIaWRkZW59fXODmeAFPAJ7ey/gByfhADvgAwABe3vgAE/iAYLgBwDjBNAHY2hlY2tib3jjCdTAcAAi4gYZ4AEXAGNAOwBlIXZgREFrg0HgBwABe3vgAL3gDZUCe3tewFXgDSBAAOA/tuAuo+MNvMC2BCI+SXMggcgAPOUGP8AA4gDj4AMAADzCBAdEZWxldGVkSOIYBeAGKOIRBqAo4TpQoEkAIuIICOAAGOIkCaA14A2YYgrgFCFAAOIgC6BJ4BG64hINoDvgDafiDw6gNAAiQg8ARIHTADziKhAHU2VxdWVuY2XkFxUAU+AGJ+IMDuANqwBToDrkCZnAAABToCHoKDLgAwDhBKroDT7gAIniBmDgARfoAEYAU6CSAH3oBUDgAwDmAqwBNSLoHUvA6OgNT6hWAWNs6AhMAHtmrQhNb2RlfX1idXQoZgRBZGRJbsaDAHtpxoAeA3t7XkHAC4AqBFNhdmVJ4AwrAicpO6iL4AMA5Ag/AjwvdekCpQEvZKcT4AAnAHtgfgJpdGlLAOcQSw57eyNTaG93VXNlckluZm/iAe0APOgDZwBmK9QBLXeH4AEgbEfnBWVyOyBjb0o+ASByaj8NUGxlYXNlIGxvZ2luIHQruSDfADyKUsDGA3t7L1PgDGvAAAR7e0FkZEVGAW9u4AGIAnt7RSDF4AMVYOIg/gFsPg==";

		#endregion		

        #region List Section Properties

		public class AdditionalVisible	
		{
			public string Sequence { get; set; }	
			public bool IsActiveHidden { get; set; }	
			public bool IsActive { get; set; }	
			public bool IsDeletedHidden { get; set; }	
			public bool IsDeleted { get; set; }	
			public bool SequenceHidden { get; set; }	
			public bool AddMode { get; set; }	
		}		
		public List<AdditionalVisible> AdditionalVisibleList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool AdditionalActionVisible { get; set; }	
		public bool ShowUserInfo { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string InfoSectionName { get; set; }	
		public string InfoDetailName { get; set; }	
		public string RevisionNo { get; set; }	
		public string InfoDetailDescription { get; set; }	
		public string AddAction { get; set; }	
		public string EditAction { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1780);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1780);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#AdditionalVisible}}";
			sectionEndTag = "{{/AdditionalVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (AdditionalVisibleList !=null))
			{
				foreach (var additionalvisible in AdditionalVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{Sequence}}", string.IsNullOrEmpty(additionalvisible.Sequence)==false ? additionalvisible.Sequence : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(AdditionalVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AdditionalActionVisible}}";
			sectionEndTag = "{{/AdditionalActionVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AdditionalActionVisible ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ShowUserInfo}}";
			sectionEndTag = "{{/ShowUserInfo}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ShowUserInfo ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{InfoSectionName}}", string.IsNullOrEmpty(InfoSectionName)==false ? InfoSectionName : "");
			template = template.Replace("{{InfoDetailName}}", string.IsNullOrEmpty(InfoDetailName)==false ? InfoDetailName : "");
			template = template.Replace("{{RevisionNo}}", string.IsNullOrEmpty(RevisionNo)==false ? RevisionNo : "");
			template = template.Replace("{{InfoDetailDescription}}", string.IsNullOrEmpty(InfoDetailDescription)==false ? InfoDetailDescription : "");
			template = template.Replace("{{AddAction}}", string.IsNullOrEmpty(AddAction)==false ? AddAction : "");
			template = template.Replace("{{EditAction}}", string.IsNullOrEmpty(EditAction)==false ? EditAction : "");
			return template;
		}

		protected string ProcessListAdditionalVisibleSection(AdditionalVisible additionalvisible, string template)
		{

			return template;
		}

		protected string ProcessBoolAdditionalVisibleSection(AdditionalVisible additionalvisible, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsActiveHidden}}";
			sectionEndTag = "{{/IsActiveHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.IsActiveHidden ? sectionValue : "");
            }

            #endregion

            #endregion
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsActive}}";
			sectionEndTag = "{{/IsActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsActive}}";
            invertedSectionEndTag = "{{/IsActive}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.IsActive ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, additionalvisible.IsActive ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDeletedHidden}}";
			sectionEndTag = "{{/IsDeletedHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.IsDeletedHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDeleted}}";
			sectionEndTag = "{{/IsDeleted}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsDeleted}}";
            invertedSectionEndTag = "{{/IsDeleted}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.IsDeleted ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, additionalvisible.IsDeleted ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SequenceHidden}}";
			sectionEndTag = "{{/SequenceHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.SequenceHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddMode}}";
			sectionEndTag = "{{/AddMode}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^AddMode}}";
            invertedSectionEndTag = "{{/AddMode}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.AddMode ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, additionalvisible.AddMode ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailSaveDetailAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailSaveDetailAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_496="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoSQWRkSW5mb0RldGFpbCIgbmFtZeAOGgJ0eXAgGhBzdWJtaXQiIGRhdGEtdGhlbSATGGIiIG9uY2xpY2s9InJldHVybiBzYXZlSW7AVw0oMCwge3tQYWdlTm99fUALB0l0ZW1zUGVyQBMAfWARAEQgUAVJbmRleH0gDhMne3tUZW1wbGF0ZVN1ZmZpeH19J2A2D25mb1NlY3Rpb25JZH19KSIgVgUjQWRkQWNAEgpEaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9B4AkoBT5BZGQ8L4EK4QBJ4AMAAjwvZKF+QAABPGThBIIBYmxBYwBi4QGDQAAAPIBNASB04RAvAWMi4QgvCHJlZnJlc2hJbsEyBUZvcm0oe+FMM+AI5wVDYW5jZWzhCAbgAfqACQU8L2Rpdj4=";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool AddActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string InfoSectionId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_496);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_496);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddActionDisabled}}";
			sectionEndTag = "{{/AddActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{InfoSectionId}}", string.IsNullOrEmpty(InfoSectionId)==false ? InfoSectionId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailSaveDetailEdit	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailSaveDetailEdit.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_592="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoPU2F2ZURldGFpbCIgbmFtZeADFwNJbmZvwBsCdHlwIBsQc3VibWl0IiBkYXRhLXRoZW0gExhiIiBvbmNsaWNrPSJyZXR1cm4gc2F2ZUluwDwRKHt7SWR9fSwge3tQYWdlTm99YAsHSXRlbXNQZXJAEwB9YBEARCBVBUluZGV4fSAOEyd7e1RlbXBsYXRlU3VmZml4fX0nQEIQSW5mb1NlY3Rpb25JZH19KSIgVgEjUyDFAUFjQBMKRGlzYWJsZWR9fWSgCQI9ImSgCQQie3svU+AKKQE+UyAUATwvgRDhAE/gAwACPC9koYRAAAE8ZOEEiAFibEFpAGLhAYlAAAA8gE0BIHThEDcBYyLhCDcIcmVmcmVzaEluwToFRm9ybSh74Uw24AjnBUNhbmNlbOEIBuAB+uACCQBk4QMN4CvpB2RlbGV0ZUluwOgg5ABJIc0CLCB74EzsIFYCI0RlQHLiHSUAROAMKwE+RGAW4QgiQACBHAU8L2Rpdj4=";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveActionDisabled { get; set; }	
		public bool DeleteActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string Id { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string InfoSectionId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_592);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_592);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveActionDisabled}}";
			sectionEndTag = "{{/SaveActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DeleteActionDisabled}}";
			sectionEndTag = "{{/DeleteActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DeleteActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{Id}}", string.IsNullOrEmpty(Id)==false ? Id : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{InfoSectionId}}", string.IsNullOrEmpty(InfoSectionId)==false ? InfoSectionId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoDetailView	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoDetail/InfoDetailView.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_612="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAAk8c3BhbiBzdHlsIE8fcG9zaXRpb246YWJzb2x1dGU7dG9wOjVweDtyaWdodDoBMTAgCoBDwAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXDnJlc2hJbmZvRGV0YWlsTCDXFih7e1BhZ2VOb319LCB7e0l0ZW1zUGVyQBMAfWARAEQhBwVJbmRleH0gDhMne3tUZW1wbGF0ZVN1ZmZpeH19J2A2Bm5mb1NlY3QgrAZJZH19KTsigO+BPwZidXR0b24igBIGaW5saW5lPeEDPgNtaW5p4AQQAGkhQQA9ILZgrwAioDcgEyEQCz0ibm90ZXh0Ij5SZWAeAjwvYeEBPQAvQT7gAA4dUGxlYXNlIGxvZ2luIGFzIFVzZXIgaGF2aW5nIEFkIHUML0F1dGhvciBSb2xlICAgE0RlZmF1bHQgdG8gQWRkIEluZm8ggSoCKHMpQWIJPC9saT48L3VsPg==";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string InfoSectionId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_612);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_612);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{InfoSectionId}}", string.IsNullOrEmpty(InfoSectionId)==false ? InfoSectionId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPage	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPage.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_36="E3t7U2F2ZURldGFpbH19e3tMaXN0gA0BfX0=";

		#endregion		

        #region PlaceHolder Properties

		public string SaveDetail { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_36);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_36);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageCategoryOption	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageCategoryOption.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_224="FXt7I0luZm9DYXRlZ29yeUl0ZW19fSAgABI8b3B0aW9uIHZhbHVlPSJ7e0lu4AEmAFZAFAh9fSIge3sjSW7gARcLU2VsZWN0ZWR9fXNlgAkCPSJzoAkFInt7L0lu4AsrASAg4AZGCURpc2FibGV9fWSACCBFAGSgCQUie3svSW7gAUXgACoDPnt7SeACFwdUZXh0fX08L4DFIBwCL0lu4AE1QOYBfX0=";

		#endregion		

        #region List Section Properties

		public class InfoCategoryItem	
		{
			public string InfoCategoryValue { get; set; }	
			public string InfoCategoryText { get; set; }	
			public bool InfoCategorySelected { get; set; }	
			public bool InfoCategoryDisable { get; set; }	
		}		
		public List<InfoCategoryItem> InfoCategoryItemList { get; set; }	

        #endregion

        #region Bool Section Properties


        #endregion		

        #region PlaceHolder Properties


        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_224);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_224);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategoryItem}}";
			sectionEndTag = "{{/InfoCategoryItem}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (InfoCategoryItemList !=null))
			{
				foreach (var infocategoryitem in InfoCategoryItemList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolInfoCategoryItemSection(infocategoryitem , sectionValueInstance);
					sectionValueInstance = ProcessBoolInfoCategoryItemSection(infocategoryitem , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{InfoCategoryValue}}", string.IsNullOrEmpty(infocategoryitem.InfoCategoryValue)==false ? infocategoryitem.InfoCategoryValue : "");
					sectionValueInstance = sectionValueInstance.Replace("{{InfoCategoryText}}", string.IsNullOrEmpty(infocategoryitem.InfoCategoryText)==false ? infocategoryitem.InfoCategoryText : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(InfoCategoryItemList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			return template;
		}

		protected string ProcessListInfoCategoryItemSection(InfoCategoryItem infocategoryitem, string template)
		{

			return template;
		}

		protected string ProcessBoolInfoCategoryItemSection(InfoCategoryItem infocategoryitem, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategorySelected}}";
			sectionEndTag = "{{/InfoCategorySelected}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, infocategoryitem.InfoCategorySelected ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategoryDisable}}";
			sectionEndTag = "{{/InfoCategoryDisable}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, infocategoryitem.InfoCategoryDisable ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageList	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageList.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlInfoPage/InfoPageListScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlInfoPage/InfoPageListScript.min.js";										
		private const string TemplateSource_104="Hnt7TGlzdFNjcmlwdH19PGRpdiBpZD0iaW5mb1BhZ2VAHBNWaWV3e3tEYXRhSW5kZXh9fSI+ICAAAXt7QBwFRGV0YWlsIDkEL2Rpdj4=";
		private const string MinScriptSource_1672="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiBpbnB1H3RGaWx0ZXJJbmZvUGFnZUxpc3QoaCxjLGYsZCxiLGcpC3t2YXIgYT1oP2g6d4BKH2V2ZW50O2lmKGEua2V5Q29kZT09PTEzKXtyZXR1cm4gAGbgCVDgAU4AfaAkBHRydWV94ACI4AoyCWEsZCxtLG8sZSlggQNsPWQ7QIkKaT0iaXRlbXNQZXJArgMiK207IIkUJCgiIyIraSkubGVuZ3RoPjApe2w9oBQFKyIgb3B0IO4SOnNlbGVjdGVkIikudmFsKCl9diDaAGZgJwBmYLMJQ2F0ZWdvcnlTZUAoACDgDjZghwBjYDYBaW6BNQBGYD7AV2AgBms9ZmFsc2XAnYBgAUlugC8FUHVibGljIC8JaXMoIjpjaGVja0CWAyl7az1hGECYAGchqaDaAWlugDYEVXNlckbAauAB6gBn4ASL4AEkACDgDsBgXhhqPSd7InBhZ2VObyIgOiAnK2E7ais9Jywg4QVfYBoBbDugGghkYXRhSW5kZXiAFwFtO6AXIZEJcGxhdGVTdWZmaWAcCCInK28rJyInO6AhC2FzeW5jTG9hZGluZ4A8AWU7oBoAZmDRA0luZm/BfwFJZIAiAWY74AgiAFAio0AcIF8BYyvgAl/gAURAIaFfYLwBazvgBEQHQ3JlYXRlZFUhSsBmAWc7ICECIn0iYbcHaD0iR2V0SW6BcYHYAEwjFARWaWV3ImAhAmI9d4MI4wBTASsrYBgAbkFSB2pzb25ycGMiIKIRMi4wIiwibWV0aG9kIjoiJytoILEALCF0DXJhbXMiOicraisnLCJpIBsfJytiKyJ9IjskLmFqYXgoe3R5cGU6IlBPU1QiLGNvbnQjcAFUeUASB2FwcGxpY2F0Is4fL2pzb247IGNoYXJzZXQ9dXRmLTgiLHVybDoiLyohQFMNZXJ2aWNlVXJsQCovIixBuQE6bmAGAFRgSQBqID0JIixzdWNjZXNzOsOJAyhwKXtDZgJxPXBCfBlwLmhhc093blByb3BlcnR5KCJlcnJvciIpPYLdASl7IqHgCSQOZCIpKXtxPXAuZH1lbHNl4A0kBnJlc3VsdCKgKYANAX19IE8AceAIdARodG1sIiAoBiQoIiNpbmZh4kSoI/0BKS5AHEAzICMBKTvgDCEAbCG+BHZpZXcoIHMAZiBoAmgiKeANKAl0cmlnZ2VyKCJjYiQDIil9fWC/BXNob3dFciD4IOsAZUAHCi5uYW1lKyI6IitwoBAAbSFFCGFnZSl9fX0pO6TjAGZEAAB9wVoAIKB8AUlu4AHAJOUHYyxpLGssZClhdgNoPWM7QX4BZj3jBYUBK2nERwMiK2Yp4wL6Amg9JCETIBTkFuUAZ0KaAHDjBO0AZ+MP7QFoO6AaQjPjAu0BaTugF+MM7QBrItsBJzugIeMJ7QFkOyAaACIi4kDeAGXjBGNByeMaXWSwAGoitQFycOMNXQBlIIHjA10DZysnLONjXQBq4xldAmwpe0D/Am09bENdAGziCOjjCl3gCSQBZCIjCgNtPWwu4wFd4AkkIuajXUApgA0AfUNdAG3gCHRDOQAiIFJCSQFpbuIBnAMiK2kpY1ZAM+MOXSAh4x1dICgAdOMYXQFsLuMGXQBsoBDjEF0BfTs=";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ListScript { get; set; }	
		public string DataIndex { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1672);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1672);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1672);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_104);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_104);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListScript}}", string.IsNullOrEmpty(ListScript)==false ? ListScript : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageListDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageListDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_212="Hzx1bCBpZD0iaW5mb1BhZ2VMaXN0e3tEYXRhSW5kZXh9A30iIGQgDActcm9sZT0ibCAdBHZpZXcigBQLaW5zZXQ9InRydWUigBEIdGhlbWU9ImMigA4GY29udGVudMAWAmIiICAAYE8JZGl2aWRlcnRoZUAvIBhgFQ5maWx0ZXI9ImZhbHNlIj4gLAIge3tAkgpJdGVtfX08L3VsPg==";

		#endregion		

        #region PlaceHolder Properties

		public string DataIndex { get; set; }	
		public string ListItem { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_212);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_212);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{ListItem}}", string.IsNullOrEmpty(ListItem)==false ? ListItem : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageListDetailEmpty	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageListDetailEmpty.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_416="BDxsaT4gIAAfPHNwYW4gc3R5bGU9InBvc2l0aW9uOmFic29sdXRlO3QOb3A6NXB4O3JpZ2h0OjEwIAoAImA3QAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXEnJlc2hJbmZvUGFnZUxpc3Qoe3tACg9Ob319LCB7e0l0ZW1zUGVyQBMAfWARCURhdGFJbmRleH0gDhMne3tUZW1wbGF0ZVN1ZmZpeH19J0A2EkFzeW5jTG9hZGluZ319KTsiIGQgNg0tcm9sZT0iYnV0dG9uIoASBGlubGluIBQEdHJ1ZSKAEgNtaW5p4AQQBGljb249ILNgrAAigCRAEyEJCz0ibm90ZXh0Ij5SZWAeAjwvYWD6ATwvQTNgCgdObyBJbmZvIEDDDShzKSBGb3VuZDwvbGk+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string AsyncLoading { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_416);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_416);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(AsyncLoading)==false ? AsyncLoading : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageListDetailFilter	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageListDetailFilter.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1216="HzxsaSBpZD0iaW5mb1BhZ2VMaXN0RmlsdGVyIiB7eyNJAG7gBxYfSGlkZGVufX1zdHlsZT0iZGlzcGxheTpub25lOyJ7ey8BSW7gDzEBPiAgAAM8ZGl2YAhAAAs8dWwgZGF0YS1yb2wgSwBsIHsEdmlldyKAFAtpbnNldD0idHJ1ZSKAEQh0aGVtZT0iYSKADgZjb250ZW50wBYBYiKAFglkaXZpZGVydGhlQCwBZSLgAG5AAAI8bGngBA9AACDnElNob3dQdWJsaWNDaGVja2VkfX1AGeADAAk8aW5wdXQgdHlwIIIBY2ggJQdib3giIG5hbSAPAGZhMgFJboD7gEkBIiBBWOANGQBjQD8DZWQ9ImBIFWVkIiBvbmNoYW5nZT0icmV0dXJuIGbgBFJBlwIoe3tBog9Ob319LCB7e0l0ZW1zUGVyQBMAfWARAEQhSwVJbmRleH0gDhMne3tUZW1wbGF0ZVN1ZmZpeH19J0A2EEFzeW5jTG9hZGluZ319KSIv4QoPAS9T4RkPA3t7I1PgACUDVW5DaCERAGXhVzcAb+GOJeAT/wpsYWJlbCBmb3I9IuIMDAE+UyFGACCCMwE8L2At4QSSAjwvbOIFswA8I7PgBCFAAOAJZg1DYXRlZ29yeVNlbGVjdOMFBsAABEluZm8gwCgAPOAKfEAAAjxzZUBB4gTqwDOgXOIC6uAHGQBkIpQRLW5hdGl2ZS1tZW51PSJmYWxzI9IAb+Fvy+EIO0AABnt7I0luZm/AvQBJI1zkCQPAABI8b3B0aW9uIHZhbHVlPSJ7e0lu4AE6AFZAFAN9fSIg4AZSgSoBZWQlO2FPBGVkPSJzoAkFInt7L0lu4AFD4AErAz57e0ngAhgHVGV4dH19PC+AgOAOyQIvSW7gAUrgDckCPC9zYHrgDEHiCHHgAAAAPOUKPuIDeQFpboUBAEZkvOENaUAAAUluIndEuAEgRmApBTxzcGFuIEUvAWlu4ANBBkluZm8iIHOGawBmJe8YLXNpemU6eHgtc21hbGw7Ij4mbmJzcDs8L0BAAT484g/B5QO7BXNlYXJjaMW5A2luZm9AiKCxYsgBaW7gBRPhBObgASwAfSHjC29ua2V5cHJlc3M9IqW4AGlGJ4dDAUlugEhlvQdldmVudCwge+VKxAI7IiDlBcbhBMECPC91hFECPC9kp30Ge3tVc2VyU2QZBn19PC9saT4=";

		#endregion		

        #region List Section Properties

		public class ShowPublicChecked	
		{
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
			public string AsyncLoading { get; set; }	
		}		
		public List<ShowPublicChecked> ShowPublicCheckedList { get; set; }	

		public class ShowPublicUnChecked	
		{
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
			public string AsyncLoading { get; set; }	
		}		
		public List<ShowPublicUnChecked> ShowPublicUnCheckedList { get; set; }	

		public class InfoCategoryItem	
		{
			public string InfoCategoryValue { get; set; }	
			public string InfoCategoryText { get; set; }	
			public bool InfoCategorySelected { get; set; }	
		}		
		public List<InfoCategoryItem> InfoCategoryItemList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool InfoPageListFilterHidden { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string AsyncLoading { get; set; }	
		public string InfoPageFilter { get; set; }	
		public string UserSelect { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1216);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1216);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#ShowPublicChecked}}";
			sectionEndTag = "{{/ShowPublicChecked}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (ShowPublicCheckedList !=null))
			{
				foreach (var showpublicchecked in ShowPublicCheckedList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(showpublicchecked.PageNo)==false ? showpublicchecked.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(showpublicchecked.ItemsPerPage)==false ? showpublicchecked.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(showpublicchecked.DataIndex)==false ? showpublicchecked.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(showpublicchecked.TemplateSuffix)==false ? showpublicchecked.TemplateSuffix : "");
					sectionValueInstance = sectionValueInstance.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(showpublicchecked.AsyncLoading)==false ? showpublicchecked.AsyncLoading : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(ShowPublicCheckedList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#ShowPublicUnChecked}}";
			sectionEndTag = "{{/ShowPublicUnChecked}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (ShowPublicUnCheckedList !=null))
			{
				foreach (var showpublicunchecked in ShowPublicUnCheckedList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(showpublicunchecked.PageNo)==false ? showpublicunchecked.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(showpublicunchecked.ItemsPerPage)==false ? showpublicunchecked.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(showpublicunchecked.DataIndex)==false ? showpublicunchecked.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(showpublicunchecked.TemplateSuffix)==false ? showpublicunchecked.TemplateSuffix : "");
					sectionValueInstance = sectionValueInstance.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(showpublicunchecked.AsyncLoading)==false ? showpublicunchecked.AsyncLoading : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(ShowPublicUnCheckedList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategoryItem}}";
			sectionEndTag = "{{/InfoCategoryItem}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (InfoCategoryItemList !=null))
			{
				foreach (var infocategoryitem in InfoCategoryItemList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolInfoCategoryItemSection(infocategoryitem , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{InfoCategoryValue}}", string.IsNullOrEmpty(infocategoryitem.InfoCategoryValue)==false ? infocategoryitem.InfoCategoryValue : "");
					sectionValueInstance = sectionValueInstance.Replace("{{InfoCategoryText}}", string.IsNullOrEmpty(infocategoryitem.InfoCategoryText)==false ? infocategoryitem.InfoCategoryText : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(InfoCategoryItemList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#InfoPageListFilterHidden}}";
			sectionEndTag = "{{/InfoPageListFilterHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, InfoPageListFilterHidden ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(AsyncLoading)==false ? AsyncLoading : "");
			template = template.Replace("{{InfoPageFilter}}", string.IsNullOrEmpty(InfoPageFilter)==false ? InfoPageFilter : "");
			template = template.Replace("{{UserSelect}}", string.IsNullOrEmpty(UserSelect)==false ? UserSelect : "");
			return template;
		}

		protected string ProcessListInfoCategoryItemSection(InfoCategoryItem infocategoryitem, string template)
		{

			return template;
		}

		protected string ProcessBoolInfoCategoryItemSection(InfoCategoryItem infocategoryitem, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategorySelected}}";
			sectionEndTag = "{{/InfoCategorySelected}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, infocategoryitem.InfoCategorySelected ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageListDetailItem	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageListDetailItem.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1060="HTxsaSBkYXRhLXJvbGU9Imxpc3QtZGl2aWRlciI+ICAABzxoMyB0aXRsIBwTe3tJbmZvQ2F0ZWdvcnlOYW1lfX2AJEAAEzxzdHJvbmc+e3tJbmZvUGFnZU5hQCEDPC9zdKAYHyNJc0RlbGV0ZWR9fTxzcGFuIHN0eWxlPSJjb2xvcjogGnJlZDsgZm9udC13ZWlnaHQ6IGJvbGQ7Ij4oRIA2ACkgTCA2IGMBL0ngAUpAfUAAAXt7IGAISW5BY3RpdmV94CZhwDcAKeADYsAU4AZjBlB1YmxpY33gJmGANQAp4ANfgBKAXQM8L2gzgXEAcGAGQAABZHQg1AVMYXN0VXAhpARlRGF0ZSAsAWJ5IBUIQ3JlYXRlZEJ5IBAgAAE8L4A1FDxwIGNsYXNzPSJ1aS1saS1hc2lkZYGkBnt7I0VkaXRBHgFvboA1QAAIPGJ1dHRvbiBkQgUMaW5saW5lPSJ0cnVlIoASCWFqYXg9ImZhbHPAEQZpY29uPSJlIEsAIoAiQBALcG9zPSdub3RleHQngBUDbWluacBJFG9uY2xpY2s9InJldHVybiBnZXRJboIlElNhdmVWaWV3KHt7SWR9fSwge3tiPAFvfWALB0l0ZW1zUGVyQBMAfWARAEQgqAVJbmRleH0gDhUne3tUZW1wbGF0ZVN1ZmZpeH19JywgQL4EKTsiPkUgowE8L4DgYVQDe3svRSAT4QMDB3t7I0FzeW5j4AMTQAAAPIA2gMvhIhcMY2hldnJvbi1kb3duIqA9ITDhJR8hJgxyaWV2ZUluZm9TZWN0IbEATCOaBSh0aGlzLCCyQQ3hNCEBe3tg9AdMb2FkaW5nfSFJA3t7SW6BkQNJZH19QT0CUmV0YIThB0EAQeEHLgA8InwHPC9saT48bGlhZgA8Q6XD3ABmQ9AQc2l6ZTogc21hbGwiPnt7SW6AagdEZXNjcmlwdCDfAX19oxlBfAN7e0lu4AD5AFYiHUAdIGAKe3sjQ29tbWVudFaAFMBwAmRpdqA44AIfBURldGFpbILqAjwvZCAiYKUCe3sv4AIkAX19";

		#endregion		

        #region List Section Properties

		public class EditAction	
		{
			public string Id { get; set; }	
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
		}		
		public List<EditAction> EditActionList { get; set; }	

		public class AsyncAction	
		{
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
			public string AsyncLoading { get; set; }	
			public string InfoPageId { get; set; }	
		}		
		public List<AsyncAction> AsyncActionList { get; set; }	

		public class CommentView	
		{
			public string CommentViewDetail { get; set; }	
		}		
		public List<CommentView> CommentViewList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool IsDeleted { get; set; }	
		public bool IsInActive { get; set; }	
		public bool IsPublic { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string InfoCategoryName { get; set; }	
		public string InfoPageName { get; set; }	
		public string LastUpdateDate { get; set; }	
		public string CreatedBy { get; set; }	
		public string InfoPageDescription { get; set; }	
		public string InfoSectionView { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1060);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1060);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#EditAction}}";
			sectionEndTag = "{{/EditAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (EditActionList !=null))
			{
				foreach (var editaction in EditActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(editaction.Id)==false ? editaction.Id : "");
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(editaction.PageNo)==false ? editaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(editaction.ItemsPerPage)==false ? editaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(editaction.DataIndex)==false ? editaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(editaction.TemplateSuffix)==false ? editaction.TemplateSuffix : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(EditActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#AsyncAction}}";
			sectionEndTag = "{{/AsyncAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (AsyncActionList !=null))
			{
				foreach (var asyncaction in AsyncActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(asyncaction.PageNo)==false ? asyncaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(asyncaction.ItemsPerPage)==false ? asyncaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(asyncaction.DataIndex)==false ? asyncaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(asyncaction.TemplateSuffix)==false ? asyncaction.TemplateSuffix : "");
					sectionValueInstance = sectionValueInstance.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(asyncaction.AsyncLoading)==false ? asyncaction.AsyncLoading : "");
					sectionValueInstance = sectionValueInstance.Replace("{{InfoPageId}}", string.IsNullOrEmpty(asyncaction.InfoPageId)==false ? asyncaction.InfoPageId : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(AsyncActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#CommentView}}";
			sectionEndTag = "{{/CommentView}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (CommentViewList !=null))
			{
				foreach (var commentview in CommentViewList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{CommentViewDetail}}", string.IsNullOrEmpty(commentview.CommentViewDetail)==false ? commentview.CommentViewDetail : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(CommentViewList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDeleted}}";
			sectionEndTag = "{{/IsDeleted}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsDeleted ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsInActive}}";
			sectionEndTag = "{{/IsInActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsInActive ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsPublic}}";
			sectionEndTag = "{{/IsPublic}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsPublic ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{InfoCategoryName}}", string.IsNullOrEmpty(InfoCategoryName)==false ? InfoCategoryName : "");
			template = template.Replace("{{InfoPageName}}", string.IsNullOrEmpty(InfoPageName)==false ? InfoPageName : "");
			template = template.Replace("{{LastUpdateDate}}", string.IsNullOrEmpty(LastUpdateDate)==false ? LastUpdateDate : "");
			template = template.Replace("{{CreatedBy}}", string.IsNullOrEmpty(CreatedBy)==false ? CreatedBy : "");
			template = template.Replace("{{InfoPageDescription}}", string.IsNullOrEmpty(InfoPageDescription)==false ? InfoPageDescription : "");
			template = template.Replace("{{InfoSectionView}}", string.IsNullOrEmpty(InfoSectionView)==false ? InfoSectionView : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageSave	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageSave.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlInfoPage/InfoPageSaveScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlInfoPage/InfoPageSaveScript.min.js";										
		private const string TemplateSource_172="H3t7U2F2ZVNjcmlwdH19IDxkaXYgaWQ9ImluZm9QYWdlAFMgHRZWaWV3e3tEYXRhSW5kZXh9fSIge3sjU6AZH0hpZGRlbn19c3R5bGU9ImRpc3BsYXk6IG5vbmU7Int7AC/AQsAoAT4gIAACe3tTID8NRGV0YWlsfX08L2Rpdj4=";
		private const string MinScriptSource_3148="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyFWVzaEluZm9QYWdlQ2F0ZWdvcnlPcHQgHRRMaXN0KGIsYyl7dmFyIGY9J3siaW6AKB9JZCIgOiAnK2I7Zis9JywgInRlbXBsYXRlU3VmZml4Igk6IicrYysnIic7IB8DIn0iO0BBB2E9IkdldElugELgCWsAImAlAmU9d+AGsAErK2AYAGRAgBZqc29ucnBjIjogIjIuMCIsIm1ldGhvZGBwAGEgcBEsInBhcmFtcyI6JytmKycsImkgGwQnK2UrIiCEHCQuYWpheCh7dHlwZToiUE9TVCIsY29udGVudFR5QBIHYXBwbGljYXQg/B8vanNvbjsgY2hhcnNldD11dGYtOCIsdXJsOiIvKiFAUxNlcnZpY2VVcmxAKi8iLGRhdGE6ZGAGAFRgSQBqID0JIixzdWNjZXNzOsFvAyhnKXtBCR9oPWc7aWYoZy5oYXNPd25Qcm9wZXJ0eSgiZXJyb3IiKQg9PWZhbHNlKXvgDCQEZCIpKXsgQgYuZH1lbHNl4A0kIdYDdWx0IqApgA0BfX0gTwBo4Ah0BGh0bWwiICgGJCgiI2luZuIECQ5TZWxlY3QiKVswXS5vcHQhGAdzLmxlbmd0aCJG4BMwAC5AVUBsIFwAKeAVKQJzZWwgYwRtZW51KCC2YpABIingFTIQdHJpZ2dlcigiY3JlYXRlIingDdkEd2FybiIg2QRzaG93VyALAmluZyCSAHcgCQIpfX1hOwBzIBkBRXIhdCGMAGVABwMubmFtIi0DOiIrZ6AQAG0hwQhhZ2UpfX19KX3ByeMHOQ5Gb3JtKGIsZCxjLGEsZSlBkwB0ImUCb2YooNEBSW6BWABMI1QDKT09IsBIAyIpe3KA8wFJbuABISN2wEoFfSQoIiNpoBsKU2F2ZVZpZXciK2MhYQhpZGUoInNsb3fhBi5AYwBTIVIhrQAiQCgAdMEtCGV4cGFuZCIpfcB9BSBnZXRJboB3wFsMKGIsYSxkLGcsaixoKWKu4xX6BXBhZ2VOb4QPAWE7I+9EDwBpJBAEc1BlclAkVYAaAWQ7oBpDKwRJbmRleIAXAWc7oBcgMeQDQiRfAyInK2oj0+QERABl5AREwLMBIjtDVgJjPXfkDToAaeQXOgBlIGYALCDJ5Ao6BGMrIn0i5Fc6AGnkGToCayl7QOMCbD1rRDoAa+MIxeQKOuAJJAhkIikpe2w9ay7kATrgCSSELAAioCmADQB9Q2AAbOAIdEPdACIgKAUkKCIjaW7iBUcjQQApY/5AN+QIBeEAuQArICUAdMKgw84LO2lmKGg9PXRydWUp4BFcBXRvZ2dsZeMMAkBeAFPDAuADYQBlwwIAfeQHAAFrLuQGAABroBDkAwAIO3JldHVybiBmRZ/jAUYFc2F2ZUlugP8KKGIseCxoLHYsbilhsQFvPUDGADtBvAJjPSThAigDTmFtZSUoBHZhbCgpQPABYy6lWQE9MISyhJgHIlBsZWFzZSAmpQtlciB0aGUgSW5mbyBDWQIgTmFAQgE7b4YvAX12J6cAYeAEYwdEZXNjcmlwdEQKACngAWoCYS5shcTgIWoAROADSQA74ANxAGzgBHEAQ6hDhjkAIIY0ATpzZeMBZWTgA4QBbD3gDX0AU2ZzBCBhIEluIOfAT+AGdAB1Jn8iJAAkIU8BYWNHZARHcm91cIA6IGSA3gA+IUgBdT3gDCPgD5hhQwB0gU9BlEA8AWlugdkIQXN5bmNMb2FkJkcgYwlpcygiOmNoZWNrQNcDKXt0PUHoYEAAa+ANQAdJc0FjdGl2ZeAKPAFrPeAAPARmPSIiO6DbAWlugHoJRXhwaXJ5RGF0ZSA74AHcAmY9JCEAAWlu4Aok4gBWAG/DRyBSAWYugDsBPT0hGQJzaG/iEGJiXQFFeEB0ACCAdQA7wfIAfWDhAHfgDeECQ29tJ8gEdGFibGUgqOEHIQF3PeAA5ABx4ALkAmNvbUA0Bm9yUm9sZVNh/SA8gKkAPiCoQzoAamDp4A0p4ADqAk9iaiPzBi5wcm90b3QnfgUudG9TdHIhrQ4uY2FsbChqKT09PSJbb2JAJRQgQXJyYXldIil7cT1qLmpvaW4oKX1oEyAPAH1g4wBt4A3jB0lzUHVibGljIKPgB+ABbT3gAOAAZeAPPEEiJ5gAKeAIPAFlPeAAPABy4A88BERlbGV0Qm7gCD0Bcj3gAD0AZOMBGwFpboIaAFNL/QJuY2UgOOEBVwBkYVMBaW7gCCJhUEXxAW89pYMiOwB0IVADb2YocyI1CFByb2dyZXNzKSFOyEohSOADGgEoKWFCZ3kBaW6AYQJOYW1oDCfXCGVuY29kZVVSSSENAnBvbiIyAyhjKStH6wBpqEAAboAz5APAiBLgCjoAYeAMOsQ5AEmsrAFsO6BbAGHkATDAGwF1O8Ab4wPjIHADJyt0O6AaAWlzo71gFgFrO6AWAWV4QyVjJCAYINssdyizoB0DY29tbSDdYxAgHgMnK3c74AUZgvVJskAfID4BZW7hCBoAceAF38J/YE8BbTugTwFpcyFLQllgFgFlO+AAFsIzYBcBcjugFwBzwhFgFgFkO6AWAWlugXfBGAFiOyAYACIpAUOEAnA9d+kNYANzPSJTKpoBSW6APAAiYC0AZ+kXdQBzISgALOkCdQNpKycsrbADcCsifelYdQBn6Rl1Ankpe0D4Ano9eUYMAHnpCADpCnXgCSQIZCIpKXt6PXku6QF14AkkiWcAIqApgA0AfUODAHrgCHSokAAiICsAcyONAVN1bmUgKKAYACkopW1GAUlugY9snaiUAix0KYTmAGngC1qgzwApYFiIfwF6LmkXASl96QgxAXkuYBntATIAeaAQwIdAvqQ2AGgsrABQ5A02AGjgAhokNgB9LWbpDWUCZGVsJPwBSW6A1AAojKcFaCxqLGUpQM0DYj4wKUAHAHTkK6hirAFpboBaAEmjLANiO2cr4gD87BBdA2Y9IkRllgFJboA+gv5k+gBqMAcBcnDwDa9EJ+ID/gNnKycsov4DYysifeJY/uyvdKJdASIpgqXC/gFsLqAY4g3+AGEu04Ir4gD+AGzjCFniCv4BbC5i5AEpfeII/uwZMCNiooziMf4BfTs=";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveViewHidden { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string SaveScript { get; set; }	
		public string DataIndex { get; set; }	
		public string SaveDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_3148);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_3148);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_3148);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_172);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_172);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveViewHidden}}";
			sectionEndTag = "{{/SaveViewHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveViewHidden ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveScript}}", string.IsNullOrEmpty(SaveScript)==false ? SaveScript : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageSaveAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageSaveAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_564="GDxsaSBzdHlsZT0iY29sb3I6IGdyYXkiPiAgABA8aDY+SW5mbyBQYWdlPC9oNoAVE3AgY2xhc3M9InVpLWxpLWFzaWRlgDBAAA17eyNBZGRBY3Rpb259fUARQAAZPGEgb25jbGljaz0icmV0dXJuIGdldEluZm9AWwpTYXZlVmlldygwLCA/QBEDTm99fUALB0l0ZW1zUGVyQBMAfWARCURhdGFJbmRleH0gDh8ne3tUZW1wbGF0ZVN1ZmZpeH19JywgdHJ1ZSk7IiBocgdlZj0iIyIgZCAzDC1hamF4PSJmYWxzZSKAEQh0aGVtZT0iYiKADgJyb2xADQV1dHRvbiKAEgVtaW5pPSJAT6AyBWlubGluZeAFEgljb249InBsdXMigDRAEBJwb3M9J25vdGV4dCc+QWRkPC9hYTxAAAN7ey9B4RUhBSQoJyNpboEbGUxpc3RGaWx0ZXInKS50b2dnbGUoJ3Nsb3cn4AniAGlgigBmYC0AIoCM4BD1AWEigCADbWluaeAFzwBu4Aji4QoZQGrgBOQARmB1wOcIPC9wPjwvbGk+";

		#endregion		

        #region List Section Properties

		public class AddAction	
		{
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
		}		
		public List<AddAction> AddActionList { get; set; }	

        #endregion

        #region Bool Section Properties


        #endregion		

        #region PlaceHolder Properties


        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_564);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_564);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#AddAction}}";
			sectionEndTag = "{{/AddAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (AddActionList !=null))
			{
				foreach (var addaction in AddActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(addaction.PageNo)==false ? addaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(addaction.ItemsPerPage)==false ? addaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(addaction.DataIndex)==false ? addaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(addaction.TemplateSuffix)==false ? addaction.TemplateSuffix : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(AddActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageSaveDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageSaveDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1764="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAA57eyNBZGRpdGlvbmFsQWNABwhWaXNpYmxlfX1AH0AAFjxkaXYgY2xhc3M9InVpLWxpLWFzaWRlgEzAAB88YSBocmVmPSIjIiBvbmNsaWNrPSJoaWRlTWVzc2FnZR8oKTskKCcjaW5mb1BhZ2VBZHZhbmNlZCcpLnRvZ2dsZQwoJ3Nsb3cnKTsiPkFkwJADPzwvYeAAqgQ8L2RpduAADQN7ey9B4AAn4A+4DGxhYmVsIGZvcj0iaW6AeANOYW1l4AW5BEluZm8gQJMfIE5hbWU8c3BhbiBzdHlsZT0iY29sb3I6UmVkOyI+KjwAL0AaAj48L2BV4ACICTxpbnB1dCB0eXAgMAh0ZXh0IiBuYW0gCwFpbuACcQUgaWQ9ImngBBEKdmFsdWU9Int7SW7gASgBfX1hvMAAH21heGxlbmd0aD0iNTAiIHBsYWNlaG9sZGVyPSImIzE4Bjc7RW50ZXLgAb4BKiIhfRBrZXlwcmVzcz0icmV0dXJuICI/CEZvY3VzT3JDbCGYD0J1dHRvbihldmVudCwnaW6AfgdEZXNjcmlwdCIKAicsJ0GJAC9g5AI8L2yCLwA84gI3AjxwIOIXEQZ7e1JldmlzIE4BTm/iAkwBL3BgVUAA4Quf4AKF4Q+mAETgAagAPEGS4SStAHQhoQNhcmVh4Qak4AN24QSr4AQY4QKOAjQwMCGQAnJvdyFlADIgCaIxEmhlaWdodDoxNTBweDsiPnt7SW7hCF0EfX08L3SghuEBCwAv4QplAHtjnQtJbmZvQ2F0ZWdvcnnhAkgAZCOVoHUEdG9wOjAgcAEgcmB9YAoRcG9zaXRpb246YWJzb2x1dGU74QVPAzxidXQiBQEgb+MNrKI5BGdldElu4AF+ClNhdmVWaWV3KDAsIJxDNANOb319QAsHSXRlbXNQZXJAEwB9YBEARCTQBUluZGV4fSAOBSd7e1RlbSK/DHRlU3VmZml4fX0nLCBE0gIpOyKEqwZpbmxpbmU95APnCWFqYXg9ImZhbHMjQQBkIE8BLWkk6wY9InBsdXMi4AEQIOoEPSJub3QhU2Q9ACDBLQE8L4Dk4QJl5AlDAUlu4AHf4QJeAWxh5Ag7wFYFU2VsZWN04QVOAUluJEXAIAA84iiWAXNlQFLiBpTATqBv4gSX4AcbYR0LbmF0aXZlLW1lbnU9wTYEe3sjSW7gAeGASwVEaXNhYmwl4wBkgAgDZD0iZKAJBSJ7ey9JbuAQMOEASmAA4AZcAEkiBOEBQsAAAzxvcHQiiOQF8MC/BVZhbHVlfST14AZKgKcEZWR9fXNhGgRlZD0ic6AJBSJ7ey9JbuAHogBlICsBICDgBkYARIDVAX194Bno4AAqAz57e0ngAl0AVCJWA319PC+AxeAE/wR7ey9JbuABh+AF/iYvYK/gADEDe3tJbuABLABTI0oFRGV0YWlsQLYBICDkAAAFe3tNb3JlgBoAc6Ab5QJ5Dnt7I1Nob3dVc2VySW5mb4AgQAAAPOIDdABmKBIBLXeEjAEgbEQVBWVyOyBjb0bPASByZtAQUGxlYXNlIGxvZ2luIHRvIFMgkAE8L0BE4AKxAS9T4AxrwAAEe3tBZGSHfeABiAJ7e0UnwOADFWDbBDwvdWw+";

		#endregion		

        #region List Section Properties

		public class AddInfoCategory	
		{
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
		}		
		public List<AddInfoCategory> AddInfoCategoryList { get; set; }	

		public class InfoCategoryItem	
		{
			public string InfoCategoryValue { get; set; }	
			public string InfoCategoryText { get; set; }	
			public bool InfoCategorySelected { get; set; }	
			public bool InfoCategoryDisable { get; set; }	
		}		
		public List<InfoCategoryItem> InfoCategoryItemList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool AdditionalActionVisible { get; set; }	
		public bool InfoCategorySelectDisable { get; set; }	
		public bool ShowUserInfo { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string InfoPageName { get; set; }	
		public string RevisionNo { get; set; }	
		public string InfoPageDescription { get; set; }	
		public string InfoCategorySaveDetail { get; set; }	
		public string MoreDetails { get; set; }	
		public string AddAction { get; set; }	
		public string EditAction { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1764);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1764);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#AddInfoCategory}}";
			sectionEndTag = "{{/AddInfoCategory}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (AddInfoCategoryList !=null))
			{
				foreach (var addinfocategory in AddInfoCategoryList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(addinfocategory.PageNo)==false ? addinfocategory.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(addinfocategory.ItemsPerPage)==false ? addinfocategory.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(addinfocategory.DataIndex)==false ? addinfocategory.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(addinfocategory.TemplateSuffix)==false ? addinfocategory.TemplateSuffix : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(AddInfoCategoryList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategoryItem}}";
			sectionEndTag = "{{/InfoCategoryItem}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (InfoCategoryItemList !=null))
			{
				foreach (var infocategoryitem in InfoCategoryItemList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolInfoCategoryItemSection(infocategoryitem , sectionValueInstance);
					sectionValueInstance = ProcessBoolInfoCategoryItemSection(infocategoryitem , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{InfoCategoryValue}}", string.IsNullOrEmpty(infocategoryitem.InfoCategoryValue)==false ? infocategoryitem.InfoCategoryValue : "");
					sectionValueInstance = sectionValueInstance.Replace("{{InfoCategoryText}}", string.IsNullOrEmpty(infocategoryitem.InfoCategoryText)==false ? infocategoryitem.InfoCategoryText : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(InfoCategoryItemList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AdditionalActionVisible}}";
			sectionEndTag = "{{/AdditionalActionVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AdditionalActionVisible ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategorySelectDisable}}";
			sectionEndTag = "{{/InfoCategorySelectDisable}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, InfoCategorySelectDisable ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ShowUserInfo}}";
			sectionEndTag = "{{/ShowUserInfo}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ShowUserInfo ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{InfoPageName}}", string.IsNullOrEmpty(InfoPageName)==false ? InfoPageName : "");
			template = template.Replace("{{RevisionNo}}", string.IsNullOrEmpty(RevisionNo)==false ? RevisionNo : "");
			template = template.Replace("{{InfoPageDescription}}", string.IsNullOrEmpty(InfoPageDescription)==false ? InfoPageDescription : "");
			template = template.Replace("{{InfoCategorySaveDetail}}", string.IsNullOrEmpty(InfoCategorySaveDetail)==false ? InfoCategorySaveDetail : "");
			template = template.Replace("{{MoreDetails}}", string.IsNullOrEmpty(MoreDetails)==false ? MoreDetails : "");
			template = template.Replace("{{AddAction}}", string.IsNullOrEmpty(AddAction)==false ? AddAction : "");
			template = template.Replace("{{EditAction}}", string.IsNullOrEmpty(EditAction)==false ? EditAction : "");
			return template;
		}

		protected string ProcessListInfoCategoryItemSection(InfoCategoryItem infocategoryitem, string template)
		{

			return template;
		}

		protected string ProcessBoolInfoCategoryItemSection(InfoCategoryItem infocategoryitem, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategorySelected}}";
			sectionEndTag = "{{/InfoCategorySelected}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, infocategoryitem.InfoCategorySelected ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#InfoCategoryDisable}}";
			sectionEndTag = "{{/InfoCategoryDisable}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, infocategoryitem.InfoCategoryDisable ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageSaveDetailAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageSaveDetailAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_492="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoQQWRkSW5mb1BhZ2UiIG5hbWXgDBgCdHlwIBgQc3VibWl0IiBkYXRhLXRoZW0gExhiIiBvbmNsaWNrPSJyZXR1cm4gc2F2ZUlugFMFKDAsIHt7QF0DTm99fUALB0l0ZW1zUGVyQBMAfWARAEQgTgVJbmRleH0gDhMne3tUZW1wbGF0ZVN1ZmZpeH19J0A2D0FzeW5jTG9hZGluZ319KSIgVRQjQWRkQWN0aW9uRGlzYWJsZWR9fWSgCQI9ImSgCQQie3svQeAJKAU+QWRkPC+BA+EAQuADAAI8L2Shd0AAATxk4QR7AWJsQVwAYuEBfEAAADyATQEgdOEQLAFjIuEILAhyZWZyZXNoSW6BLwVGb3JtKHvhSzDgCOQFQ2FuY2Vs4QgD4AH3gAkFPC9kaXY+";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool AddActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string AsyncLoading { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_492);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_492);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddActionDisabled}}";
			sectionEndTag = "{{/AddActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(AsyncLoading)==false ? AsyncLoading : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageSaveDetailEdit	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageSaveDetailEdit.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_588="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMf4AcACzxidXR0b24gaWQ9IoAKEVNhdmVJbmZvUGFnZSIgbmFtZeANGQJ0eXAgGRBzdWJtaXQiIGRhdGEtdGhlbSATGGIiIG9uY2xpY2s9InJldHVybiBzYXZlSW6AVAooe3tJZH19LCB7e0BjAk5vfWALB0l0ZW1zUGVyQBMAfWARAEQgUwVJbmRleH0gDhUne3tUZW1wbGF0ZVN1ZmZpeH19JykiIEMBI1MgsBBBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9T4AopAT5TIBQBPC+A++EARuADAAI8L2She0AAATxk4QR/AWJsQWAAYuEBgEAAADyATQEgdOEQIgFjIuEIIghyZWZyZXNoSW6BJQVGb3JtKHvhNyESLCB7e0FzeW5jTG9hZGluZ319KeAFqkAABUNhbmNlbOEIA+AB9+ACCQBk4QMK4CvmB2RlbGV0ZUlugOUBKHviPwvgC+kFIHt7I0RlQG/iHR8AROAMKwE+RGAW4QgfQACBGQU8L2Rpdj4=";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveActionDisabled { get; set; }	
		public bool DeleteActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string Id { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string AsyncLoading { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_588);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_588);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveActionDisabled}}";
			sectionEndTag = "{{/SaveActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DeleteActionDisabled}}";
			sectionEndTag = "{{/DeleteActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DeleteActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{Id}}", string.IsNullOrEmpty(Id)==false ? Id : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(AsyncLoading)==false ? AsyncLoading : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageSaveDetailMore	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageSaveDetailMore.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_2176="BDxsaT4gIAADPGRpdmAIQAAfPHVsIGlkPSJpbmZvUGFnZUFkdmFuY2VkIiBkYXRhLXINb2xlPSJsaXN0dmlldyKAFAtpbnNldD0idHJ1ZSKAEQh0aGVtZT0iYSKADgZjb250ZW50wBYBYiJAacAAYFcJZGl2aWRlcnRoZUA3IEYDc3R5bCBBDWRpc3BsYXk6bm9uZTsi4AClQAAAPKC+4AMACTxsYWJlbCBmb3IgfA9jY2Vzc0dyb3VwU2VsZWN04AU+wAABQWNAJgAgYCcBPC9gP+AEZkAAAjxzZUBABCBuYW1l4AtYACBBMeAJb4DaEW5hdGl2ZS1tZW51PSJmYWxzZeANiAN7eyNBYIsAR0CyBUl0ZW19fUB84AsAETxvcHRpb24gdmFsdWU9Int7QeABOQBWQBMDfX0iIOAFUIEDBGVkfX1zYMwEZWQ9InOgCQQie3svQeABQeABKgA+4ARZB1RleHR9fTwvgHzhCCVAAOAFSOANxAI8L3Ngd+AEQCF6go3AAAA84QreQNEKc3luY0xvYWRpbmfhCR8VPGlucHV0IHR5cGU9ImNoZWNrYm94IqG3AWluotLgAkIAImG6AWlu4AsZAGNAPwNlZD0iYEghJQAv4ASxQAAAeyE34AJN4AmQA3t7XkHgFCDgQ7HgKp/jAzABaW7hCgUAPmC+ACChcQA84wog4R+5AkNvbSMDBHRhYmxl4Qkn4RwG4AJBACLhBLfgBBjhHbbgAjLgCY0hteAUH+BBreEMsuAVewBs5AHiAWlugbHgAjABIj7gAgwAPOEvruADZAJjb21BtwZvclJvbGVT5RJJoHgDb3IgUiAp4At7QADlBUvgC1xh2uAMGGYq5QpPCyBtdWx0aXBsZT0ibaAJ5RBj4AClQM4ASeUlZeAEOwBW5QJn4AQYgSMAZeUQaeAOLAI+e3vgBBkAVOUcbeAEMuANzuUgbwAgJV8ESXNBY3QmrwVIaWRkZW4mLOcLhwJ7ey/gByflChrgAEvjCWBAAOMcRMBmACJiFAFpboLv4AEV4wjsACDjDFHAMOAJiSPq4BEcQADgO6bgJpPjA30BaW7gBvIDPklzIIGi4wth5QuMACDhCJsDPGRpdoNMic8EZmllbGRJpAJhaW7jDTjgC5IJRXhwaXJ5RGF0ZeANM0AAAEVgIwAgQCTgC67AAKFkAW5hSV8BaW6A8uACX+EE/eADFwB0Z+kAdCMqqW2A0ARkYXRlYkf5IAgBYS2IrxBzPSd7Im1vZGUiOiAiY2FsYiAfASwgYCoFRm9ybWF0QBcDZGQvbSAABi9ZWVlZIixAJgpUb2RheUJ1dHRvbiAgSuMgNAlhZnRlclRvZGF54AETBm1heERheXMgEAMxMDk1ICQquwBhJuwGTWFudWFsSUiYIBtAQAF9J8D34AcA6QDPAUV4wVsAfSnJ4gk/AjwvZOsCtMAA4Qj94AcAADwiDQF7eyOYBlB1YmxpY0jjGOTgBSfiCEhAAAB74AFP4wleodiBpmlH6QqQAEmgZgAi4QTm4AEV4wjk4QkPAXt7I+OArOAJhGPj4A8c4Duh4CaP4wtM4ADtQ9+AqwA85x69IN8h0UYIAm9uSOEY0eAFJ+EI0WHNgCPhCUjhHiuAPgAi4QbNwBXhH82AL+AJhGHN4A8c4Duh4Q7N4BByAGzpCZABSXOALQAiQc2ACgA84RvN4AcAADzB3QdEZWxldGVkSOEY3uAGKOEN36Ak4QlbQADhHkKgQwAi4Qbl4AAW4Qjm5ArFAXt7I7WgMeAJjGHo4BAdQADgParhDu/gEHnhDfCgLgAiQfEARIGtADzhMvIHU2VxdWVuY2XpF4cAU+AGJ+EI8OALrQBToDTnDbcAU6AdBDxzcGFu0YgMY29sb3I6UmVkOyI+KipRIBoAPuIL0kAA4QSWh5sBbmHnA9ngAH/iBDzgARXnAAYAU6CKRwTgCgAfbWF4bGVuZ3RoPSI1IiBwbGFjZWhvbGRlcj0iJiMxODcLO0VudGVyIEluZm8gUswAIMDaGioiIG9ua2V5cHJlc3M9InJldHVybiBjbGlja4fqCChldmVudCwnezGUCGRkTW9kZX19YmgEBEFkZEluiKAAey/9wBwAey/owAuAKARTYXZlSeAKKQMnKTsi4gVy5wS2AjwvdZKAh+IhYwFpPg==";

		#endregion		

        #region List Section Properties

		public class AccessGroupItem	
		{
			public string AccessGroupValue { get; set; }	
			public string AccessGroupText { get; set; }	
			public bool AccessGroupSelected { get; set; }	
		}		
		public List<AccessGroupItem> AccessGroupItemList { get; set; }	

		public class CommentorRoleItem	
		{
			public string CommentorRoleValue { get; set; }	
			public string CommentorRoleText { get; set; }	
			public bool CommentorRoleSelected { get; set; }	
		}		
		public List<CommentorRoleItem> CommentorRoleItemList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool AsyncLoading { get; set; }	
		public bool Commentable { get; set; }	
		public bool IsActiveHidden { get; set; }	
		public bool IsActive { get; set; }	
		public bool IsPublicHidden { get; set; }	
		public bool IsPublic { get; set; }	
		public bool IsCommonHidden { get; set; }	
		public bool IsCommon { get; set; }	
		public bool IsDeletedHidden { get; set; }	
		public bool IsDeleted { get; set; }	
		public bool SequenceHidden { get; set; }	
		public bool AddMode { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ExpiryDate { get; set; }	
		public string Sequence { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_2176);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_2176);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#AccessGroupItem}}";
			sectionEndTag = "{{/AccessGroupItem}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (AccessGroupItemList !=null))
			{
				foreach (var accessgroupitem in AccessGroupItemList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolAccessGroupItemSection(accessgroupitem , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{AccessGroupValue}}", string.IsNullOrEmpty(accessgroupitem.AccessGroupValue)==false ? accessgroupitem.AccessGroupValue : "");
					sectionValueInstance = sectionValueInstance.Replace("{{AccessGroupText}}", string.IsNullOrEmpty(accessgroupitem.AccessGroupText)==false ? accessgroupitem.AccessGroupText : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(AccessGroupItemList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#CommentorRoleItem}}";
			sectionEndTag = "{{/CommentorRoleItem}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (CommentorRoleItemList !=null))
			{
				foreach (var commentorroleitem in CommentorRoleItemList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolCommentorRoleItemSection(commentorroleitem , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{CommentorRoleValue}}", string.IsNullOrEmpty(commentorroleitem.CommentorRoleValue)==false ? commentorroleitem.CommentorRoleValue : "");
					sectionValueInstance = sectionValueInstance.Replace("{{CommentorRoleText}}", string.IsNullOrEmpty(commentorroleitem.CommentorRoleText)==false ? commentorroleitem.CommentorRoleText : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(CommentorRoleItemList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AsyncLoading}}";
			sectionEndTag = "{{/AsyncLoading}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^AsyncLoading}}";
            invertedSectionEndTag = "{{/AsyncLoading}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AsyncLoading ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, AsyncLoading ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#Commentable}}";
			sectionEndTag = "{{/Commentable}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^Commentable}}";
            invertedSectionEndTag = "{{/Commentable}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, Commentable ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, Commentable ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsActiveHidden}}";
			sectionEndTag = "{{/IsActiveHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsActiveHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsActive}}";
			sectionEndTag = "{{/IsActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsActive}}";
            invertedSectionEndTag = "{{/IsActive}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsActive ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, IsActive ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsPublicHidden}}";
			sectionEndTag = "{{/IsPublicHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsPublicHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsPublic}}";
			sectionEndTag = "{{/IsPublic}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsPublic}}";
            invertedSectionEndTag = "{{/IsPublic}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsPublic ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, IsPublic ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsCommonHidden}}";
			sectionEndTag = "{{/IsCommonHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsCommonHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsCommon}}";
			sectionEndTag = "{{/IsCommon}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsCommon}}";
            invertedSectionEndTag = "{{/IsCommon}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsCommon ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, IsCommon ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDeletedHidden}}";
			sectionEndTag = "{{/IsDeletedHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsDeletedHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDeleted}}";
			sectionEndTag = "{{/IsDeleted}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsDeleted}}";
            invertedSectionEndTag = "{{/IsDeleted}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsDeleted ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, IsDeleted ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SequenceHidden}}";
			sectionEndTag = "{{/SequenceHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SequenceHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddMode}}";
			sectionEndTag = "{{/AddMode}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^AddMode}}";
            invertedSectionEndTag = "{{/AddMode}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddMode ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, AddMode ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ExpiryDate}}", string.IsNullOrEmpty(ExpiryDate)==false ? ExpiryDate : "");
			template = template.Replace("{{Sequence}}", string.IsNullOrEmpty(Sequence)==false ? Sequence : "");
			return template;
		}

		protected string ProcessListAccessGroupItemSection(AccessGroupItem accessgroupitem, string template)
		{

			return template;
		}

		protected string ProcessListCommentorRoleItemSection(CommentorRoleItem commentorroleitem, string template)
		{

			return template;
		}

		protected string ProcessBoolAccessGroupItemSection(AccessGroupItem accessgroupitem, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AccessGroupSelected}}";
			sectionEndTag = "{{/AccessGroupSelected}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, accessgroupitem.AccessGroupSelected ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessBoolCommentorRoleItemSection(CommentorRoleItem commentorroleitem, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#CommentorRoleSelected}}";
			sectionEndTag = "{{/CommentorRoleSelected}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, commentorroleitem.CommentorRoleSelected ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoPageView	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoPage/InfoPageView.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_596="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAAk8c3BhbiBzdHlsIE8fcG9zaXRpb246YWJzb2x1dGU7dG9wOjVweDtyaWdodDoBMTAgCoBDwAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXCHJlc2hQYWdlTCDRAih7e0AKD05vfX0sIHt7SXRlbXNQZXJAEwB9YBEARCEBBUluZGV4fSAOEyd7e1RlbXBsYXRlU3VmZml4fX0nQDYQQXN5bmNMb2FkaW5nfX0pOyKA6IE4BmJ1dHRvbiKAEgZpbmxpbmU94QM3A21pbmngBBAAaSE6AD0gr2CoACKgNyATIQkLPSJub3RleHQiPlJlYB4CPC9h4QE2AC9BN+AADhdQbGVhc2UgbG9naW4gYXMgVXNlciBoYXYgowwgQXV0aG9yIFJvbGUgIBoVRGVmYXVsdCB0byBNb2RpZnkgSW5mb0EEAihzKUFVCTwvbGk+PC91bD4=";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string AsyncLoading { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_596);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_596);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(AsyncLoading)==false ? AsyncLoading : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSection	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSection.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_36="FHt7U2F2ZURldGFpbH19IHt7TGlzdKAOAX0g";

		#endregion		

        #region PlaceHolder Properties

		public string SaveDetail { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_36);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_36);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionList	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionList.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlInfoSection/InfoSectionListScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlInfoSection/InfoSectionListScript.min.js";										
		private const string TemplateSource_112="H3t7TGlzdFNjcmlwdH19PGRpdiBpZD0iaW5mb1NlY3RpAm9uTCAfFFZpZXd7e0luZm9QYWdlSWR9fSI+ICAAAXt7QD0FRGV0YWlsID0EL2Rpdj4=";
		private const string MinScriptSource_1052="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZXRyC2lldmVJbmZvU2VjdCATH0xpc3QoZyxjLGUsZCxiLGYsYSl7JChnKS5idXR0b24oCiJkaXNhYmxlIik7ID0CdXJuIEQGZnJlc2hJbuAFQwBj4AJBAH3gAnHgDCwUYSxkLGosbCxlLGIpe3ZhciBpPWQ7QAcbZz0iaXRlbXNQZXJQYWdlIitqO2lmKCQoIiMiKyCUC2xlbmd0aD4wKXtpPaAUBSsiIG9wdCDJAzpzZWwg0wtlZCIpLnZhbCgpfXYgWBhoPSd7InBhZ2VObyIgOiAnK2E7aCs9Jywg4AVnYBoBaTugGghkYXRhSW5kZXiAFwFqO6AXIJkJcGxhdGVTdWZmaWAcCCInK2wrJyInO6AhC2FzeW5jTG9hZGluZ4A8AWU7oBoEaW5mb1Ag0wFJZIAYAWI7IBgCIn0iYPcHZj0iR2V0SW7hBFMEVmlldyJgHgJjPXfhBtYBKytgGABrQN4HanNvbnJwYyIgjhMyLjAiLCJtZXRob2QiOiInK2YrJyAQD3BhcmFtcyI6JytoKycsImkgGx8nK2MrIn0iOyQuYWpheCh7dHlwZToiUE9TVCIsY29udARlbnRUeUASB2FwcGxpY2F0IWIfL2pzb247IGNoYXJzZXQ9dXRmLTgiLHVybDoiLyohQFMNZXJ2aWNlVXJsQCovIixBRQE6a2AGAFRgSQBqID0JIixzdWNjZXNzOsIjAyhtKXtB+gNuPW07IesfbS5oYXNPd25Qcm9wZXJ0eSgiZXJyb3IiKT09ZmFsc2UBKXvgDCQEZCIpKXsgQgYuZH1lbHNl4A0kIrcDdWx0IqApgA0BfX0gTwBu4Ah0BGh0bWwiICgFJCgiI2lu4QSWBCIrYikuQB9ANiAmASk74A8kAGwjXAR2aWV3KCB5YwcBIingECsSdHJpZ2dlcigiY3JlYXRlIil9fWDIBXNob3dFciEBIRkAZUAHCi5uYW1lKyI6IittoBAAbSFOB2FnZSl9fX0pw6UAZkErAX07";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ListScript { get; set; }	
		public string InfoPageId { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1052);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1052);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1052);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_112);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_112);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListScript}}", string.IsNullOrEmpty(ListScript)==false ? ListScript : "");
			template = template.Replace("{{InfoPageId}}", string.IsNullOrEmpty(InfoPageId)==false ? InfoPageId : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionListDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionListDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_212="Hzx1bCBpZD0iaW5mb1NlY3Rpb25MaXN0e3tJbmZvUGFnEmVJZH19IiBkYXRhLXJvbGU9ImwgHgR2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJkIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGXgAy8OZmlsdGVyPSJmYWxzZSI+ICwDIHt7TCB0Ckl0ZW19fTwvdWw+";

		#endregion		

        #region PlaceHolder Properties

		public string InfoPageId { get; set; }	
		public string ListItem { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_212);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_212);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{InfoPageId}}", string.IsNullOrEmpty(InfoPageId)==false ? InfoPageId : "");
			template = template.Replace("{{ListItem}}", string.IsNullOrEmpty(ListItem)==false ? ListItem : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionListDetailEmpty	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionListDetailEmpty.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_444="BDxsaT4gIAAfPHNwYW4gc3R5bGU9InBvc2l0aW9uOmFic29sdXRlO3QOb3A6NXB4O3JpZ2h0OjEwIAoAImA3QAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXC3Jlc2hJbmZvU2VjdCBUGkxpc3Qoe3tQYWdlTm99fSwge3tJdGVtc1BlckATAH1gEQlEYXRhSW5kZXh9IA4TJ3t7VGVtcGxhdGVTdWZmaXh9fSdANgxBc3luY0xvYWRpbmd9ICcFe3tJbmZvQEQISWR9fSk7IiBkIEYNLXJvbGU9ImJ1dHRvbiKAEgRpbmxpbiAUBHRydWUigBIDbWluaeAEEARpY29uPSDGYL8AIoAkQBMhHAs9Im5vdGV4dCI+UmVgHgI8L2FhDQE8L0FGYAoITm8gSW5mbyBTgPUNKHMpIEZvdW5kPC9saT4=";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string AsyncLoading { get; set; }	
		public string InfoPageId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_444);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_444);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(AsyncLoading)==false ? AsyncLoading : "");
			template = template.Replace("{{InfoPageId}}", string.IsNullOrEmpty(InfoPageId)==false ? InfoPageId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionListDetailItem	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionListDetailItem.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_824="HTxsaSBkYXRhLXJvbGU9Imxpc3QtZGl2aWRlciI+ICAAAjxoM2AHQAAePHN0cm9uZz57e0luZm9TZWN0aW9uTmFtZX19PC9zdGAbGSB7eyNJc0luQWN0aXZlfX08c3BhbiBzdHlsIF4fY29sb3I6IHJlZDsgZm9udC13ZWlnaHQ6IGJvbGQ7Ij4AKMA3ACkgTyA3BD57ey9J4AJMYIQBL2iAkRQ8cCBjbGFzcz0idWktbGktYXNpZGWAtCCDA0VkaXRAgwFvboA2QAAGPGJ1dHRvboDwDGlubGluZT0idHJ1ZSKAEglhamF4PSJmYWxzwBEGaWNvbj0iZSBLACKAIgNtaW5p4AQzQCEfcG9zPSdub3RleHQnIG9uY2xpY2s9InJldHVybiBnZXQBSW7hAC0ZU2F2ZVZpZXcoe3tJZH19LCB7e1BhZ2VOb31gCwdJdGVtc1BlckATAH1gEQBEIZwFSW5kZXh9IA4VJ3t7VGVtcGxhdGVTdWZmaXh9fScsIEDBQEgDSW5mb0A4CElkfX0pOyI+RSC2ATwvgPNhwwN7ey9FIBPhAxYHe3sjQXN5bmPgAxNAAAA8gDaA9OEiKgxjaGV2cm9uLWRvd24ioD0hQ+EEIQBkINLhBEgAb+EGMiE5D3JpZXZlSW5mb0RldGFpbEwilwUodGhpcywgsUDjAE7hMzAAe2F7QSICUmV0YGnhByYAQeEHEws8L3A+PC9saT48bGlhSwA8Qp/C1wBmQssQc2l6ZTogc21hbGwiPnt7SW7hAPQHRGVzY3JpcHRijKLZQWQDe3tJbsDhAFYiFAZ9fTwvbGk+";

		#endregion		

        #region List Section Properties

		public class EditAction	
		{
			public string Id { get; set; }	
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
			public string InfoPageId { get; set; }	
		}		
		public List<EditAction> EditActionList { get; set; }	

		public class AsyncAction	
		{
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
			public string Id { get; set; }	
		}		
		public List<AsyncAction> AsyncActionList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool IsInActive { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string InfoSectionName { get; set; }	
		public string InfoSectionDescription { get; set; }	
		public string InfoDetailView { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_824);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_824);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#EditAction}}";
			sectionEndTag = "{{/EditAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (EditActionList !=null))
			{
				foreach (var editaction in EditActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(editaction.Id)==false ? editaction.Id : "");
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(editaction.PageNo)==false ? editaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(editaction.ItemsPerPage)==false ? editaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(editaction.DataIndex)==false ? editaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(editaction.TemplateSuffix)==false ? editaction.TemplateSuffix : "");
					sectionValueInstance = sectionValueInstance.Replace("{{InfoPageId}}", string.IsNullOrEmpty(editaction.InfoPageId)==false ? editaction.InfoPageId : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(EditActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#AsyncAction}}";
			sectionEndTag = "{{/AsyncAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (AsyncActionList !=null))
			{
				foreach (var asyncaction in AsyncActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(asyncaction.PageNo)==false ? asyncaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(asyncaction.ItemsPerPage)==false ? asyncaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(asyncaction.DataIndex)==false ? asyncaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(asyncaction.TemplateSuffix)==false ? asyncaction.TemplateSuffix : "");
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(asyncaction.Id)==false ? asyncaction.Id : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(AsyncActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsInActive}}";
			sectionEndTag = "{{/IsInActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, IsInActive ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{InfoSectionName}}", string.IsNullOrEmpty(InfoSectionName)==false ? InfoSectionName : "");
			template = template.Replace("{{InfoSectionDescription}}", string.IsNullOrEmpty(InfoSectionDescription)==false ? InfoSectionDescription : "");
			template = template.Replace("{{InfoDetailView}}", string.IsNullOrEmpty(InfoDetailView)==false ? InfoDetailView : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionSave	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionSave.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlInfoSection/InfoSectionSaveScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlInfoSection/InfoSectionSaveScript.min.js";										
		private const string TemplateSource_176="H3t7U2F2ZVNjcmlwdH19IDxkaXYgaWQ9ImluZm9TZWN0A2lvblMgIBdWaWV3e3tJbmZvUGFnZUlkfX0iIHt7I1OgGh9IaWRkZW59fXN0eWxlPSJkaXNwbGF5OiBub25lOyJ7ewEvU+AGKAE+ICAAAnt7UyAWDURldGFpbH19PC9kaXY+";
		private const string MinScriptSource_2140="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyCmVzaEluZm9TZWN0IBIbRm9ybShjLGUsZCxiLGYsYSl7aWYodHlwZW9mKOAJLQdMaXN0KT09IsBQAyIpe3LgCFJAJAAo4ANSBn0kKCIjaW7gAHMaU2F2ZVZpZXciK2EpLmhpZGUoInNsb3ciKTsk4AUoQEkAU4CnYCsRdHJpZ2dlcigiZXhwYW5kIil9wIgFIGdldElu4AhhGShqLGEsZCxnLGssaCxiKXt2YXIgZj0neyJp4AErFklkIiA6ICcrajtmKz0nLCAicGFnZU5vgBQBYTugFAtpdGVtc1BlclBhZ2WAGgFkO6AaCGRhdGFJbmRleIAXAWc7oBcgMQlwbGF0ZVN1ZmZpYBwIIicraysnIic7oCEDaW5mb0BQwIIBYjsgGAMifSI7QKcHZT0iR2V0SW7gCNQAImAeAmM9d+EG6AErK2AYAGlA3wdqc29ucnBjIiBzETIuMCIsIm1ldGhvZCI6IicrZSCCACwg5Q1yYW1zIjonK2YrJywiaSAbECcrYysifSI7JC5hamF4KHt0IgYQOiJQT1NUIixjb250ZW50VHlAEgdhcHBsaWNhdCI/Hy9qc29uOyBjaGFyc2V0PXV0Zi04Iix1cmw6Ii8qIUBTDWVydmljZVVybEAqLyIsQSoBOmlgBgBUYEkAaiA9CSIsc3VjY2VzczrBzQMobCl7QQIfbT1sO2lmKGwuaGFzT3duUHJvcGVydHkoImVycm9yIikHPT1mYWxzZSlCs+AJJA5kIikpe209bC5kfWVsc2XgDSQjDgN1bHQioCmADQF9fSB0AG3gCHQEaHRtbCIgKAUkKCIjaW7hCZYDK2IpLkAjQDogKgEpO+ATKAB0wsoIY3JlYXRlIik7IHMIaD09dHJ1ZSl74BQ5BG9nZ2xl4w8yQ1sAU8MyIJAAdMBnAGXDMgF9fWENBXNob3dFciFGITkAZUAHCi5uYW1lKyI6IitsoBAAbSGTA2FnZSkgMgkpO3JldHVybiBmQXAAfcGoBiBzYXZlSW7hAAsCKHAsREUHbCxxLGcsYylhxAFhPUDTADtBzwduPSQoIiNpbuAALwtOYW1lIikudmFsKClBAAtuLmxlbmd0aD09MCngAqEHIlBsZWFzZSAigAdlciB0aGUgSSBDASBTgOcCIE5hQEUBO2GCDQF9diPkAGvgB2kHRGVzY3JpcHQisuADcAJrLmzgKnDgBEwAO+ADdwBtgINAywAk4AXqB0lzQWN0aXZlIH0NaXMoIjpjaGVja2VkIilCo0EhYLcAZeASPwREZWxldEAx4AhAAWU94ABAAGgl7iJJACTgBXwAU0YMAm5jZSA7gO8APiFfAWg94BElYZFDBgFhPaKSIFQAdCQPA29mKHMiOQlQcm9ncmVzcyk95gQM4AMaASgpYNEAakSOA2luZm/kA+gCYztqhWsBaW7iBQsliSUkDmVuY29kZVVSSUNvbXBvbiH2AihuKSS1ASc74Ak24QOL4BA9AGvgBT0Ac6GKIC8DJyttO8BUAHPBYWAXAWU7oBcAc8E8YBYBaDugFgFpbuAAugBJoDIBcDsgGwAiJUFC7AJkPXflD6ABIlMnAQFJbuAAPwAiYDAAb0EpAGolEABy5Q64AGkhAgAs5QK4A2orJyyluAJkKyIgduVWuABv5Rm4AnIpe0D7B3M9cjtpZihy5QhD5Qq44AkkAWQiIxQDcz1yLuUBuOAJJIWqACKgKYANAH1CsQBz4Ah0pMcAIkArIrsBU3VmbSAooBgAKSTc6QwX5APLAH1lNSDU4AlfoNQAKWBdhVMBcy5lUwEpfeUIbQFyLmAZ5QFtAHKgEMCMQMOjaQBoKRsAUOMNaQBo4AIaBigpfX19KX3lDaECZGVsJDUBSW7iAGwAKIkTB2gsayxlLGIpQNIDaj4wKUAHAHTjK+AAZ0K2AWlu4ABfAEmjDANqO2cr4wAM6BCtA2Y9IkRk1wFJbuAAQYMO6BjHKLcAIuMDDgNnKycsow4CYysi41kO6K/HomgAIqKwww4BbS6gGOMQDgBhK0rCNuMADgBt4whu4woOAW0uYvQBKX3jCA4AbKhrAW5h6A98I3Kil+MxDgF9Ow==";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveViewHidden { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string SaveScript { get; set; }	
		public string InfoPageId { get; set; }	
		public string SaveDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2140);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2140);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_2140);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_176);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_176);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveViewHidden}}";
			sectionEndTag = "{{/SaveViewHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveViewHidden ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveScript}}", string.IsNullOrEmpty(SaveScript)==false ? SaveScript : "");
			template = template.Replace("{{InfoPageId}}", string.IsNullOrEmpty(InfoPageId)==false ? InfoPageId : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionSaveAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionSaveAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_452="GDxsaSBzdHlsZT0iY29sb3I6Z3JheTsiPiAgABM8aDY+SW5mbyBTZWN0aW9uPC9oNmAYFSA8cCBjbGFzcz0idWktbGktYXNpZGWANEAAGjxhIG9uY2xpY2s9InJldHVybiBnZXRJbmZvU4BJFVNhdmVWaWV3KDAsIHt7UGFnZU5vfX1ACwdJdGVtc1BlckATAH1gEQlEYXRhSW5kZXh9IA4ZJ3t7VGVtcGxhdGVTdWZmaXh9fScsIHRydWVgPAJuZm9AOBJJZH19KTsiIGhyZWY9IiMiICBkIEQMLWFqYXg9ImZhbHNlIoARCHRoZW1lPSJiIoAOAnJvbEANBXV0dG9uIoASBW1pbmk9IkBgoDIFaW5saW5l4AUSCWNvbj0icGx1cyKANEAQC3Bvcz0nbm90ZXh0J4E0oAACQWRkoAkDIDwvYWAaCDwvcD48L2xpPg==";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string InfoPageId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_452);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_452);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{InfoPageId}}", string.IsNullOrEmpty(InfoPageId)==false ? InfoPageId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionSaveDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionSaveDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1792="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAABs8cD5TZWN0aW9uIGZvciBQYWdlIDoge3tJbmZvQAwITmFtZX19PC9wYDECPC9sgDoAPOACQgd7eyNBZGRpdCBDA2FsQWNABwhWaXNpYmxlfX1AYkAAFjxkaXYgY2xhc3M9InVpLWxpLWFzaWRlgI/gAwAfPGEgaHJlZj0iIyIgb25jbGljaz0iaGlkZU1lc3NhZ2ULKCk7JCgnI2luZm9TgLceQWR2YW5jZWQnKS50b2dnbGUoJ3Nsb3cnKTsiID5BZMCYAz88L2Fgw0AABDwvZGl24AANA3t7L0HgACfgD8AFbGFiZWwgISMDPSJpbuAAfANOYW1l4AXEBUluZm8gU4CaHyBOYW1lPHNwYW4gc3R5bGU9ImNvbG9yOlJlZDsiPio8AC9AGgI+PC9gW+AAjgk8aW5wdXQgdHlwIDAIdGV4dCIgbmFtIAsBaW7gBXcFIGlkPSJp4AcUCnZhbHVlPSJ7e0lu4AQuAX19YhbAAB9tYXhsZW5ndGg9IjUwIiBwbGFjZWhvbGRlcj0iJiMxOAc3O0VudGVyIOADygEqIiGTEGtleXByZXNzPSJyZXR1cm4gIpwIRm9jdXNPckNsIa4PQnV0dG9uKGV2ZW50LCdpbuAAhAdEZXNjcmlwdCInAicsJ0GfAC9g8+IMVAI8cCDiFy4Ge3tSZXZpcyBOAU5v4gJpAC+CqkAA4Q604AKI4RK74AIlADxBp+EkwgB0IbYDYXJlYeEJueACVgAi4QfA4AQb4QKgAjQwMCGiAnJvdyF0ADIgCaJMFm1pbi1oZWlnaHQ6MTUwcHg7Ij57e0lu4QtwBH19PC90oJNhdEAA4QB4AHvjA8HjBLsAPOMC5wE8ZOMCM0AAADwktQVpZD0iaW7gAHIAQaOMACKEfuRCzuADAOQM2sEVAmRpcyLMBnk6bm9uZTvhBd9AAAI8bGlA7wFJc0PoCnZlSGlkZGVufX1zg5/gBTwCe3sv4Acn4QA84AMAAXt74ABP4gGI4AcA4wTWB2NoZWNrYm944wrawHEAIuIHHeABGABjQD0AZSF5YEZBbYNJ4AcAAXt74AC/4A2XAnt7XsBV4A0gQADgQbjgLqXjDsbAuQQiPklzIIHNADzlBkrAAOIA6eADAAA8wgkHRGVsZXRlZEjiGArgBijiEQugKOE7U6BKACLiCQ3gABniJA6gNeANmmIP4BQhQADiIRCgSuASvOISEqA84A2p4hAToDUAIkIUAESB2AA84ioVB1NlcXVlbmNl5BcfAFPgBifiDBPgDqwAU6A75AmkwAAAU6Ah6ChD4AMA4QSu6A5P4ACK4gdm4AEY6ABXAFOglAB96AVQ4AMA5gK7ATUi6BFbAUlu6QIrwOvoDWSoawFjbOgIYQB7ZrwITW9kZX19YnV0KHsDQWRkSSBZAFOJhAB7ad2AHwN7e15BwAuAKwRTYXZlSeANLAInKTuooeADAOQISgI8L3XpArsBL2SnJOAAJwB7YH8CaXRpSx7nEFwOe3sjU2hvd1VzZXJJbmZv4gH0ADzoA3sAZivoAS13h/IBIGxH+QVlcjsgY29KVAEgcmpVDVBsZWFzZSBsb2dpbiB0Koog4AA8imjAxgN7ey9T4AxrwAAEe3tBZGRFUQFvbuABiAJ7e0UgxeADFWDiIP4BbD4=";

		#endregion		

        #region List Section Properties

		public class AdditionalVisible	
		{
			public string Sequence { get; set; }	
			public bool IsActiveHidden { get; set; }	
			public bool IsActive { get; set; }	
			public bool IsDeletedHidden { get; set; }	
			public bool IsDeleted { get; set; }	
			public bool SequenceHidden { get; set; }	
			public bool AddMode { get; set; }	
		}		
		public List<AdditionalVisible> AdditionalVisibleList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool AdditionalActionVisible { get; set; }	
		public bool ShowUserInfo { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string InfoPageName { get; set; }	
		public string InfoSectionName { get; set; }	
		public string RevisionNo { get; set; }	
		public string InfoSectionDescription { get; set; }	
		public string AddAction { get; set; }	
		public string EditAction { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1792);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1792);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#AdditionalVisible}}";
			sectionEndTag = "{{/AdditionalVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (AdditionalVisibleList !=null))
			{
				foreach (var additionalvisible in AdditionalVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = ProcessBoolAdditionalVisibleSection(additionalvisible , sectionValueInstance);
					sectionValueInstance = sectionValueInstance.Replace("{{Sequence}}", string.IsNullOrEmpty(additionalvisible.Sequence)==false ? additionalvisible.Sequence : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(AdditionalVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AdditionalActionVisible}}";
			sectionEndTag = "{{/AdditionalActionVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AdditionalActionVisible ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ShowUserInfo}}";
			sectionEndTag = "{{/ShowUserInfo}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ShowUserInfo ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{InfoPageName}}", string.IsNullOrEmpty(InfoPageName)==false ? InfoPageName : "");
			template = template.Replace("{{InfoSectionName}}", string.IsNullOrEmpty(InfoSectionName)==false ? InfoSectionName : "");
			template = template.Replace("{{RevisionNo}}", string.IsNullOrEmpty(RevisionNo)==false ? RevisionNo : "");
			template = template.Replace("{{InfoSectionDescription}}", string.IsNullOrEmpty(InfoSectionDescription)==false ? InfoSectionDescription : "");
			template = template.Replace("{{AddAction}}", string.IsNullOrEmpty(AddAction)==false ? AddAction : "");
			template = template.Replace("{{EditAction}}", string.IsNullOrEmpty(EditAction)==false ? EditAction : "");
			return template;
		}

		protected string ProcessListAdditionalVisibleSection(AdditionalVisible additionalvisible, string template)
		{

			return template;
		}

		protected string ProcessBoolAdditionalVisibleSection(AdditionalVisible additionalvisible, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsActiveHidden}}";
			sectionEndTag = "{{/IsActiveHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.IsActiveHidden ? sectionValue : "");
            }

            #endregion

            #endregion
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsActive}}";
			sectionEndTag = "{{/IsActive}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsActive}}";
            invertedSectionEndTag = "{{/IsActive}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.IsActive ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, additionalvisible.IsActive ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDeletedHidden}}";
			sectionEndTag = "{{/IsDeletedHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.IsDeletedHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDeleted}}";
			sectionEndTag = "{{/IsDeleted}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsDeleted}}";
            invertedSectionEndTag = "{{/IsDeleted}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.IsDeleted ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, additionalvisible.IsDeleted ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SequenceHidden}}";
			sectionEndTag = "{{/SequenceHidden}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.SequenceHidden ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddMode}}";
			sectionEndTag = "{{/AddMode}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^AddMode}}";
            invertedSectionEndTag = "{{/AddMode}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, additionalvisible.AddMode ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, additionalvisible.AddMode ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionSaveDetailAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionSaveDetailAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_524="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMf4AcACzxidXR0b24gaWQ9IoAKE0FkZEluZm9TZWN0aW9uIiBuYW1l4A8bAnR5cCAbEHN1Ym1pdCIgZGF0YS10aGVtIBMYYiIgb25jbGljaz0icmV0dXJuIHNhdmVJbuAAWQ0oMCwge3tQYWdlTm99fUALB0l0ZW1zUGVyQBMAfWARAEQgUQVJbmRleH0gDhMne3tUZW1wbGF0ZVN1ZmZpeH19J0A2DEFzeW5jTG9hZGluZ30gJwV7e0luZm9ARAVJZH19KSIgEAYjQWRkQWN0IM8KRGlzYWJsZWR9fWSgCQI9ImSgCQQie3svQeAJKAU+QWRkPC+BHOEAZ+ADAAI8L2ShnEAAATxk4QSgAWJsQYEAYuEBoUAAADyATQEgdOEQPwFjIuEIPwhyZWZyZXNoSW7hAEIFRm9ybSh74VtD4Aj3BUNhbmNlbOEIFuEBCoAJBTwvZGl2Pg==";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool AddActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string AsyncLoading { get; set; }	
		public string InfoPageId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_524);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_524);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddActionDisabled}}";
			sectionEndTag = "{{/AddActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(AsyncLoading)==false ? AsyncLoading : "");
			template = template.Replace("{{InfoPageId}}", string.IsNullOrEmpty(InfoPageId)==false ? InfoPageId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionSaveDetailEdit	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionSaveDetailEdit.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_616="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMf4AcACzxidXR0b24gaWQ9IoAKFFNhdmVJbmZvU2VjdGlvbiIgbmFtZeAQHAJ0eXAgHBBzdWJtaXQiIGRhdGEtdGhlbSATGGIiIG9uY2xpY2s9InJldHVybiBzYXZlSW7gAFoRKHt7SWR9fSwge3tQYWdlTm99YAsHSXRlbXNQZXJAEwB9YBEARCBWBUluZGV4fSAOEyd7e1RlbXBsYXRlU3VmZml4fX0nQEIMQXN5bmNMb2FkaW5nfSAnBXt7SW5mb0BEBUlkfX0pIiAQASNTINsCQWN0INYKRGlzYWJsZWR9fWSgCQI9ImSgCQQie3svU+AKKQE+UyAUATwvgSbhAHHgAwACPC9koaZAAAE8ZOEEqgFibEGLAGLhAatAAAA8gE0BIHThEEcBYyLhCEcIcmVmcmVzaElu4QBKBUZvcm0oe+FbRuAI9wVDYW5jZWzhCBbhAQrgAgkAZOEDHeAr+QdkZWxldGVJbuAA+CD0AEkh3gIsIHvgW/wgZQIjRGVAguIdRQBE4AwrAT5EYBbhCDJAAIEsBTwvZGl2Pg==";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveActionDisabled { get; set; }	
		public bool DeleteActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string Id { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string AsyncLoading { get; set; }	
		public string InfoPageId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_616);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_616);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveActionDisabled}}";
			sectionEndTag = "{{/SaveActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DeleteActionDisabled}}";
			sectionEndTag = "{{/DeleteActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DeleteActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{Id}}", string.IsNullOrEmpty(Id)==false ? Id : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(AsyncLoading)==false ? AsyncLoading : "");
			template = template.Replace("{{InfoPageId}}", string.IsNullOrEmpty(InfoPageId)==false ? InfoPageId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateInfoSectionView	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlInfoSection/InfoSectionView.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_632="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAAk8c3BhbiBzdHlsIE8fcG9zaXRpb246YWJzb2x1dGU7dG9wOjVweDtyaWdodDoBMTAgCoBDwAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXC3Jlc2hJbmZvU2VjdCBYAEwg2BYoe3tQYWdlTm99fSwge3tJdGVtc1BlckATAH1gEQBEIQgFSW5kZXh9IA4TJ3t7VGVtcGxhdGVTdWZmaXh9fSdANgxBc3luY0xvYWRpbmd9ICcFe3tJbmZvQEQGSWR9fSk7IoD/gU8GYnV0dG9uIoASBmlubGluZT3hA04DbWluaeAEEABpIVEAPSDGYL8AIqA3IBMhIAs9Im5vdGV4dCI+UmVgHgI8L2HhAU0AL0FO4AAOF1BsZWFzZSBsb2dpbiBhcyBVc2VyIGhhdiCzAiBBZCB1DC9BdXRob3IgUm9sZSAgIBREZWZhdWx0IHRvIEFkZCBJbmZvIFOBOgIocylBcwk8L2xpPjwvdWw+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	
		public string AsyncLoading { get; set; }	
		public string InfoPageId { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_632);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_632);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			template = template.Replace("{{AsyncLoading}}", string.IsNullOrEmpty(AsyncLoading)==false ? AsyncLoading : "");
			template = template.Replace("{{InfoPageId}}", string.IsNullOrEmpty(InfoPageId)==false ? InfoPageId : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriber	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/Subscriber.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_500="HzxkaXYgc3R5bGU9Im1hcmdpbjowIGF1dG87IHRleHQtEGFsaWduOmNlbnRlciA7Ij4gIAAAPEAzFWlkPSJzdWJzY3JpYmVyRGV0YWlscyLASgxkaXNwbGF5Om5vbmU7gDVAAAV7e1NhdmWALAF9fUARQAAFe3tMaXN04AMVBDwvZGl2gGsDc3BhbsBVH2ZvbnQtc2l6ZSA6eHgtc21hbGw7Y29sb3I6Z3JheTsiEj5XZSBwcm9taXNlIG5vdCB0byAgPgZtIHlvdTwvIAkAboBSA2JyIC+gCRd1dHRvbiBkYXRhLWlubGluZT0idHJ1ZSKAEgNhamF4IHUCYWxzwBEIdGhlbWU9ImIigCALaWNvbj0iZW1haWwigBEDbWluacBDDG9uY2xpY2s9IiQoJyPhCCsTJykudG9nZ2xlKCdzbG93JykiPlPBTxEgZm9yIG1vcmUgaW5mbyAoe3tBIQlDb3VudH19KTwvgLcGPjwvZGl2Pg==";

		#endregion		

        #region PlaceHolder Properties

		public string SaveDetail { get; set; }	
		public string ListDetail { get; set; }	
		public string ListCount { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_500);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_500);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			template = template.Replace("{{ListCount}}", string.IsNullOrEmpty(ListCount)==false ? ListCount : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriberList	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/SubscriberList.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlSubscriber/SubscriberListScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlSubscriber/SubscriberListScript.min.js";										
		private const string TemplateSource_92="H3t7TGlzdFNjcmlwdH19PGRpdiBpZD0ic3Vic2NyaWJlAXJMIB4GVmlldyI+ICAAAXt7QC4FRGV0YWlsIC4EL2Rpdj4=";
		private const string MinScriptSource_900="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaFN1YnNjcmliZXJMaXN0KGEsYyxoLGope3ZhciBnAj1jO0AHH2U9Iml0ZW1zUGVyUGFnZSIraDtpZigkKCIjIitlKS5sCmVuZ3RoPjApe2c9oBQFKyIgb3B0IGYSOnNlbGVjdGVkIikudmFsKCl9diBYF2Y9J3sicGFnZU5vIiA6JythO2YrPScsIOAFZgYgOiAnK2c7oBoIZGF0YUluZGV4gBcBaDugFyCYCXBsYXRlU3VmZmlgHAgiJytqKyciJzsgIQIifSJgwgVkPSJHZXTgBewEVmlldyJgHQJiPXfhBikBKytgGABpQKgHanNvbnJwYyIgWREyLjAiLCJtZXRob2QiOiInK2QgaAAsIMoNcmFtcyI6JytmKycsImkgGx8nK2IrIn0iOyQuYWpheCh7dHlwZToiUE9TVCIsY29udARlbnRUeUASB2FwcGxpY2F0ISwfL2pzb247IGNoYXJzZXQ9dXRmLTgiLHVybDoiLyohQFMNZXJ2aWNlVXJsQCovIixBEAE6aWAGAFRgSQBqID0JIixzdWNjZXNzOsHoAyhrKXtBxANsPWs7IbUfay5oYXNPd25Qcm9wZXJ0eSgiZXJyb3IiKT09ZmFsc2UBKXvgDCQOZCIpKXtsPWsuZH1lbHNl4A0kIk8DdWx0IqApgA0BfX0gTwBs4Ah0BGh0bWwiICgEJCgiI3PiBIIiKEAcQDMgIwEpO+AMIQBsIqsEdmlldyggc2LGASIp4A0oEnRyaWdnZXIoImNyZWF0ZSIpfX1gvwRzaG93RUD4IRAAZUAHCi5uYW1lKyI6IitroBAAbSFFEGFnZSl9fX0pO3JldHVybiBmQSIBfTs=";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string ListScript { get; set; }	
		public string ListDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_900);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_900);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_900);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_92);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_92);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListScript}}", string.IsNullOrEmpty(ListScript)==false ? ListScript : "");
			template = template.Replace("{{ListDetail}}", string.IsNullOrEmpty(ListDetail)==false ? ListDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriberListDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/SubscriberListDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_196="Hzx1bCBpZD0ic3Vic2NyaWJlckxpc3QiIGRhdGEtcm9sA2U9ImwgEAR2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVAL8A+DmZpbHRlcj0iZmFsc2UiPiAsAyB7e0wgdApJdGVtfX08L3VsPg==";

		#endregion		

        #region PlaceHolder Properties

		public string ListItem { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_196);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_196);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{ListItem}}", string.IsNullOrEmpty(ListItem)==false ? ListItem : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriberListDetailEmpty	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/SubscriberListDetailEmpty.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_396="BDxsaT4gIAAfPHNwYW4gc3R5bGU9InBvc2l0aW9uOmFic29sdXRlO3QOb3A6NXB4O3JpZ2h0OjEwIAoAImA3QAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXH3Jlc2hTdWJzY3JpYmVyTGlzdCh7e1BhZ2VOb319LCB7CHtJdGVtc1BlckATAH1gEQlEYXRhSW5kZXh9IA4YJ3t7VGVtcGxhdGVTdWZmaXh9fScpOyIgZCAkDS1yb2xlPSJidXR0b24igBIEaW5saW4gFAR0cnVlIoASA21pbmngBBAEaWNvbj0go2CcACKAJEATIPkLPSJub3RleHQiPlJlYB4CPC9hYOoBPC9BI2AKAk5vIOAB0Q0ocykgRm91bmQ8L2xpPg==";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_396);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_396);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriberListDetailItem	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/SubscriberListDetailItem.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_432="BDxsaT4gIAACPGgzYAdAAB48c3Ryb25nPnt7U3Vic2NyaWJlckVtYWlsfX08L3N0YBtgJwEvaIA0ATxw4AQrDE1lc3NhZ2V9fSA8L3BgKQx7eyNFZGl0QWN0aW9uIBegABU8cCBjbGFzcz0idWktbGktYXNpZGUiYDHAABk8YnV0dG9uIGRhdGEtaW5saW5lPSJ0cnVlIoASCWFqYXg9ImZhbHPAEQNtaW5p4AQiBmljb249ImUgfxUiICBvbmNsaWNrPSJyZXR1cm4gZ2V04AHkGVNhdmVWaWV3KHt7SWR9fSwge3tQYWdlTm99YAsHSXRlbXNQZXJAEwB9YBEARCCVBUluZGV4fSAOGSd7e1RlbXBsYXRlU3VmZml4fX0nKTsiID5FIHoBPC+AyOAA3AA84QAaAS9FIB/BGgQ8L2xpPg==";

		#endregion		

        #region List Section Properties

		public class EditAction	
		{
			public string Id { get; set; }	
			public string PageNo { get; set; }	
			public string ItemsPerPage { get; set; }	
			public string DataIndex { get; set; }	
			public string TemplateSuffix { get; set; }	
		}		
		public List<EditAction> EditActionList { get; set; }	

        #endregion

        #region Bool Section Properties


        #endregion		

        #region PlaceHolder Properties

		public string SubscriberEmail { get; set; }	
		public string SubscriberMessage { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_432);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_432);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#EditAction}}";
			sectionEndTag = "{{/EditAction}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (EditActionList !=null))
			{
				foreach (var editaction in EditActionList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = sectionValueInstance.Replace("{{Id}}", string.IsNullOrEmpty(editaction.Id)==false ? editaction.Id : "");
					sectionValueInstance = sectionValueInstance.Replace("{{PageNo}}", string.IsNullOrEmpty(editaction.PageNo)==false ? editaction.PageNo : "");
					sectionValueInstance = sectionValueInstance.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(editaction.ItemsPerPage)==false ? editaction.ItemsPerPage : "");
					sectionValueInstance = sectionValueInstance.Replace("{{DataIndex}}", string.IsNullOrEmpty(editaction.DataIndex)==false ? editaction.DataIndex : "");
					sectionValueInstance = sectionValueInstance.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(editaction.TemplateSuffix)==false ? editaction.TemplateSuffix : "");
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(EditActionList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SubscriberEmail}}", string.IsNullOrEmpty(SubscriberEmail)==false ? SubscriberEmail : "");
			template = template.Replace("{{SubscriberMessage}}", string.IsNullOrEmpty(SubscriberMessage)==false ? SubscriberMessage : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriberSave	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/SubscriberSave.html";
		public const string ScriptRelativeFilePath = "App_View/HtmlSubscriber/SubscriberSaveScript.js";		
		public const string MinScriptRelativeFilePath = "App_View/HtmlSubscriber/SubscriberSaveScript.min.js";										
		private const string TemplateSource_92="H3t7U2F2ZVNjcmlwdH19IDxkaXYgaWQ9InN1YnNjcmliAmVyUyAfB1ZpZXciID4gIAAAe2AwDURldGFpbH19PC9kaXY+";
		private const string MinScriptSource_1944="H3dpbmRvdy5yZXF1ZXN0SWQ9MDtmdW5jdGlvbiByZWZyH2VzaFN1YnNjcmliZXJGb3JtKGIsZCxjLGEpe2lmKHR5BHBlb2Yo4AgoB0xpc3QpPT0iwEoDIil7cuAHTEAjACjATAV9JCgiI3PgAGgXRGV0YWlscyIpLmhpZGUoInNsb3ciKTsk4AQkQEADU2VjdCCqICgRdHJpZ2dlcigiZXhwYW5kIil9wHwEIGdldFPgAFofU2F2ZVZpZXcoZixhLGMsZyxqLGgpe3ZhciBlPSd7InPgACgWSWQiIDogJytmO2UrPScsICJwYWdlTm+AFAFhO6AUC2l0ZW1zUGVyUGFnZYAaAWM7oBoIZGF0YUluZGV4gBcBZzugFyAxCXBsYXRlU3VmZmlgHAgiJytqKyciJzsgIQMifSI7QI0DZD0iR+ALtwAiYB0CYj134Qa4ASsrYBgAaUDEB2pzb25ycGMiIFkRMi4wIiwibWV0aG9kIjoiJytkIGgALCDLDXJhbXMiOicrZSsnLCJpIBsQJytiKyJ9IjskLmFqYXgoe3Qh2xA6IlBPU1QiLGNvbnRlbnRUeUASB2FwcGxpY2F0IXcfL2pzb247IGNoYXJzZXQ9dXRmLTgiLHVybDoiLyohQFMNZXJ2aWNlVXJsQCovIixBEAE6aWAGAFRgSQBqID0JIixzdWNjZXNzOsGvAyhrKXtBAR9sPWs7aWYoay5oYXNPd25Qcm9wZXJ0eSgiZXJyb3IiKQc9PWZhbHNlKUKI4AkkDmQiKSl7bD1rLmR9ZWxzZeANJCLeA3VsdCKgKYANAX19IHQAbOAIdARodG1sIiAoASQo4gOoAFOiTSKAQCBANyAnASk74BAlAHTCpghjcmVhdGUiKTsgbQhoPT10cnVlKXvgCTYAU+IS4AF9fWDZBXNob3dFciESIQUAZUAHCi5uYW1lKyI6IitroBAAbSFfA2FnZSkgMgkpO3JldHVybiBmQTwAfcF0AyBzYXYikQBio+kKKGosYixlLGssbSlhiwFhPUCgADtBlgFoPeAFpQRFbWFpbCD/BHZhbCgpYM0KLmxlbmd0aD09MCngApwHIlBsZWFzZSAiRwZlciB0aGUg4AFuACCgRAE7YYHTAX12I48AZOAGaABNgMTgA2oCZC5s4Chq4ABGADvgA2wAZ4B4QL8AJOEE4QhJc0RlbGV0ZWQgdwlpcygiOmNoZWNrQA4DKXtnPUEVQi8AYcHBIc0AdCM4A29mKHMhlglQcm9ncmVzcyk95QQL4AMaASgpYOxjtwBz4QB/gQ8kfyQaDmVuY29kZVVSSUNvbXBvbiE/AihoKSPFAic7aYSY4AE2wNvgEDgAZOAOOOAB5SA6RKvgCFoASaAaAWo7IBoAIiQnQgsBYz3kDoYDZj0iUyMaAFPgANAAImAvAGzkF50AZiDXACzkAp0DaSsnLKSdAmMrIiB15FadAGzkGZ0Cbil7QPoHbz1uO2lmKG7kCCjkCp3gCSQBZCIiPiBCAC7kAZ3gCSSEjwAioCmADQB9Qm0Ab+AIdKPgACIgKwBzIncBU3VlUiAooBgAKSP15w3Mg+QAfWRJIM/gCVqgzwApYFiEZwFvLmRnASl95AiBAW4uYBnkAYEAbqAQwIdAvqMgAGgn2gBQ4w0gAGjgAhoGKCl9fX0pfeQNtQJkZWwjm+IBZgAox9UBaSlAzQNmPjApQAcAdOMrkugTDwAiIopCBQFiPeMOAANkPSJEZDfgAZsAImAxAGjjFwLnd6AAaOMZAgJqKXtA/AJrPWpDAgBq4giNojLnA6DgCSQIZCIpKXtrPWou4wEC4AkkgvQAIqApgA0AfUJDAGvgCHSiYQAiICsAc+MCAgFrLqAY4w8CAGFqBQFpKeMAAuAJWqDP4wMCAGuHWQEpfeMIAgFqLuMGAgBqoBDAh0C+opDjMQIBfTs=";				

		#endregion		

        #region Script PlaceHolder Properties

		public string ScriptServiceUrl { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string SaveScript { get; set; }	
		public string SaveDetail { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsScriptValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods


		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;			
            string scriptTemplate = "";
			try
            {

				//scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1944);
                if (HttpBaseHandler.DevelopmentTestMode == false)
                {
                    scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1944);
                }
                else
                {
                    scriptTemplate = ResourceUtil.GetTextFromFile(ScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
                    if (string.IsNullOrEmpty(scriptTemplate) == true)
                    {
						scriptTemplate = ResourceUtil.GetTextFromFile(MinScriptRelativeFilePath, HttpBaseHandler.ResourceCache);
						if (string.IsNullOrEmpty(scriptTemplate) == true)
						{
							scriptTemplate = LZF.DecompressFromBase64(MinScriptSource_1944);
						}
                    }
                }
			}			
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return scriptTemplate.ToString();
		}


		public virtual string GetScriptFilled(bool includeScriptTag, bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
            string message = null;

			StringBuilder script = new StringBuilder();
			try
            {
				string scriptTemplate = GetScript(loadMinIfAvailable, validate, throwException, out message);

                if (includeScriptTag ==true)
                {
			        script.Append(ResourceUtil.ScriptMarkupStart);
                    script.Append(Environment.NewLine);
                }


				if ((string.IsNullOrEmpty(scriptTemplate) ==false) && ((validate ==false) || IsScriptValid(throwException, out message)))
				{
					scriptTemplate = scriptTemplate.Replace("/*!@ServiceUrl@*/", string.IsNullOrEmpty(ScriptServiceUrl)==false ? ScriptServiceUrl : "");

				}
					
				script.Append(scriptTemplate);
                if (includeScriptTag ==true)
                {
                    script.Append(Environment.NewLine);
			        script.Append(ResourceUtil.ScriptMarkupEnd);
                }

			}
			catch (Exception ex)
            {
                 message = "Error:" + ex.Message;
				if (throwException) throw;
            }
            retMessage = message;
			return script.ToString();   
		}


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_92);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_92);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{SaveScript}}", string.IsNullOrEmpty(SaveScript)==false ? SaveScript : "");
			template = template.Replace("{{SaveDetail}}", string.IsNullOrEmpty(SaveDetail)==false ? SaveDetail : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriberSaveDetail	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/SubscriberSaveDetail.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_1064="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAABQ8cCBjbGFzcz0idWktbGktYXNpZGWAJsAADXt7UmV2aXNpb25Ob319wBUCPC9w4AFEGWxhYmVsIGZvcj0ic3Vic2NyaWJlckVtYWls4AVKAFPgABwAIGAdGzxzcGFuIHN0eWxlPSJjb2xvcjpSZWQ7Ij4qPC9AGgI+PC9gWuABaQhpbnB1dCB0eXAgMAh0ZXh0IiBuYW0gCwBz4ABZYFgDIiBpZOAKFAl2YWx1ZT0ie3tT4AUuAX19ITICe3sj4AYWCURpc2FibGV9fWSACANkPSJkoAkDInt7L+APLcEiQAAfbWF4bGVuZ3RoPSI1MCIgcGxhY2Vob2xkZXI9IiYjMTgENztFbnQhDuABS4EZFSoiIG9ua2V5cHJlc3M9InJldHVybiAiBxpGb2N1c09yQ2xpY2tCdXR0b24oZXZlbnQsJ3PgANMOTWVzc2FnZScsJycpOyIv4QA9QAACPC9sgf0APOIDBQFsYeEKwKBG4RDCoB8APEGp4STEAHQhuANhcmVh4Qi7oFEAIuEGveAAFuEETSFOAnJvdyEcATUiwH0OaGVpZ2h0OjEwMHB4OyI+4QPnoEMEfX08L3SgfuEAF+EAExB7eyNJc0RlbGV0ZWRWaXNpYkH8YTXhAizgAySAHcAA4gSdB2NoZWNrYm944gmhAEnAYuIHpeACGABjQD0DZWQ9ImBGQCrhAcYDe3svScBC4AGAA3t7XkngCRVAAOBBluAXg+INR+AB1gQ+SXMgRIFeADzjAtnhAn/gAdnhE38LU2hvd1VzZXJJbmZv4QEBADziA4gAZiUTAS13gg8BIGxCFgVlcjsgY29EYwEgcmRkFFBsZWFzZSBsb2dpbiB0byBTYXZlPIR3QTZAAAB7I+XgDGvAAAd7e0FkZEFjdCUd4AGIBXt7RWRpdOADFWDhBDwvdWw+";

		#endregion		

        #region List Section Properties

		public class IsDeletedVisible	
		{
			public bool IsDeleted { get; set; }	
		}		
		public List<IsDeletedVisible> IsDeletedVisibleList { get; set; }	

        #endregion

        #region Bool Section Properties

		public bool SubscriberEmailDisable { get; set; }	
		public bool ShowUserInfo { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string RevisionNo { get; set; }	
		public string SubscriberEmail { get; set; }	
		public string SubscriberMessage { get; set; }	
		public string AddAction { get; set; }	
		public string EditAction { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_1064);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_1064);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{
			string sectionValueList;
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			string invertedSectionTag;
			string invertedSectionValue;

			#region List Section Processing

			#region Initialize Variables
 				
			sectionTag = "";
			sectionValue = "";
			invertedSectionTag = "";
			invertedSectionValue = "";
			
			#endregion

			#region Get Section Tag/Value

			sectionStartTag = "{{#IsDeletedVisible}}";
			sectionEndTag = "{{/IsDeletedVisible}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region List Section

            sectionValueList ="";
			if ((sectionTag.Trim().Length > 0) && (IsDeletedVisibleList !=null))
			{
				foreach (var isdeletedvisible in IsDeletedVisibleList)
                {
					var sectionValueInstance = sectionValue;
					sectionValueInstance = ProcessBoolIsDeletedVisibleSection(isdeletedvisible , sectionValueInstance);
					sectionValueList += sectionValueInstance;
                }
            }

            if (sectionTag.Trim().Length > 0)
            {
                template = template.Replace(sectionTag, sectionValueList);
            }
            if ((invertedSectionTag.Trim().Length > 0) && ((sectionValueList.Trim().Length == 0)||(IsDeletedVisibleList ==null)))
            {
                template = template.Replace(invertedSectionTag, invertedSectionValue);
            }
			else if (invertedSectionTag.Trim().Length > 0)
			{
                template = template.Replace(invertedSectionTag, "");
			}

            #endregion

            #endregion

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SubscriberEmailDisable}}";
			sectionEndTag = "{{/SubscriberEmailDisable}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SubscriberEmailDisable ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#ShowUserInfo}}";
			sectionEndTag = "{{/ShowUserInfo}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, ShowUserInfo ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{RevisionNo}}", string.IsNullOrEmpty(RevisionNo)==false ? RevisionNo : "");
			template = template.Replace("{{SubscriberEmail}}", string.IsNullOrEmpty(SubscriberEmail)==false ? SubscriberEmail : "");
			template = template.Replace("{{SubscriberMessage}}", string.IsNullOrEmpty(SubscriberMessage)==false ? SubscriberMessage : "");
			template = template.Replace("{{AddAction}}", string.IsNullOrEmpty(AddAction)==false ? AddAction : "");
			template = template.Replace("{{EditAction}}", string.IsNullOrEmpty(EditAction)==false ? EditAction : "");
			return template;
		}

		protected string ProcessListIsDeletedVisibleSection(IsDeletedVisible isdeletedvisible, string template)
		{

			return template;
		}

		protected string ProcessBoolIsDeletedVisibleSection(IsDeletedVisible isdeletedvisible, string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
			
			string invertedSectionTag;
			string invertedSectionValue;
			string invertedSectionStartTag ;
            string invertedSectionEndTag;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#IsDeleted}}";
			sectionEndTag = "{{/IsDeleted}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion
			
			#region Get Inverted Section Tag/Value

            invertedSectionStartTag = "{{^IsDeleted}}";
            invertedSectionEndTag = "{{/IsDeleted}}";
            invertedSectionTag = TemplateUtil.GetSectionTag(template, invertedSectionStartTag, invertedSectionEndTag);
            invertedSectionValue = invertedSectionTag.Replace(invertedSectionStartTag, "").Replace(invertedSectionEndTag, "");

            #endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, isdeletedvisible.IsDeleted ? sectionValue : "");
            }

            if (invertedSectionTag.Trim().Length > 0)
            {
                template = template.Replace(invertedSectionTag, isdeletedvisible.IsDeleted ? "" : invertedSectionValue);
            }                    

            #endregion

            #endregion
			
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriberSaveDetailAdd	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/SubscriberSaveDetailAdd.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_472="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMfQAALPGJ1dHRvbiBpZD0igAoSQWRkU3Vic2NyaWJlciIgbmFtZeAOGgJ0eXAgGhBzdWJtaXQiIGRhdGEtdGhlbSATF2IiIG9uY2xpY2s9InJldHVybiBzYXZlU+AAVw0oMCwge3tQYWdlTm99fUALB0l0ZW1zUGVyQBMAfWARAEQgUAVJbmRleH0gDgkne3tUZW1wbGF0IEUIZmZpeH19JykiIEMUI0FkZEFjdGlvbkRpc2FibGVkfX1koAkCPSJkoAkEInt7L0HgCSgFPkFkZDwvgPfhADbgAwACPC9koWtAAAE8ZOEEbwFibEFQAGLhAXBAAAA8gE0BIHThEBwBYyLhCBwGcmVmcmVzaOEBHwVGb3JtKHvhOSDgCNQFQ2FuY2Vs4Ajz4AHngAkFPC9kaXY+";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool AddActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_472);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_472);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#AddActionDisabled}}";
			sectionEndTag = "{{/AddActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, AddActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriberSaveDetailEdit	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/SubscriberSaveDetailEdit.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_548="BTxkaXY+ICAAADwgCBEgY2xhc3M9InVpLWdyaWQtYSJgGkAA4AYeBGJsb2Nr4AMf4AcACzxidXR0b24gaWQ9IoAKE1NhdmVTdWJzY3JpYmVyIiBuYW1l4A8bAnR5cCAbEHN1Ym1pdCIgZGF0YS10aGVtIBMUYiIgb25jbGljaz0icmV0dXJuIHNh4ANYESh7e0lkfX0sIHt7UGFnZU5vfWALB0l0ZW1zUGVyQBMAfWARAEQgVQVJbmRleH0gDgkne3tUZW1wbGF0IKMIZmZpeH19JykiIEMBI1MgthBBY3Rpb25EaXNhYmxlZH19ZKAJAj0iZKAJBCJ7ey9T4AopAT5TIBQBPC+BAeEATOADAAI8L2ShgUAAATxk4QSFAWJsQWYAYuEBhkAAADyATQEgdOEQJAFjIuEIJAdyZWZyZXNoU+EAgAVGb3JtKHvhOSPgCNQFQ2FuY2Vs4Ajz4AHn4AIJAGTgA/rgK9YDZGVsZUGywlYg0QFJZOFC/QFEZUBf4R3/AETgDCsBPkRgFuEID0AAgQkFPC9kaXY+";

		#endregion		

        #region List Section Properties

        #endregion

        #region Bool Section Properties

		public bool SaveActionDisabled { get; set; }	
		public bool DeleteActionDisabled { get; set; }	

        #endregion		

        #region PlaceHolder Properties

		public string Id { get; set; }	
		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_548);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_548);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			string sectionStartTag;
			string sectionEndTag;
			string sectionTag;
			string sectionValue;
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#SaveActionDisabled}}";
			sectionEndTag = "{{/SaveActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, SaveActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
				
			#region Bool Section Processing

            #region Get Section Tag/Value

			sectionStartTag = "{{#DeleteActionDisabled}}";
			sectionEndTag = "{{/DeleteActionDisabled}}";
            sectionTag = TemplateUtil.GetSectionTag(template, sectionStartTag, sectionEndTag);
			sectionValue = sectionTag.Replace(sectionStartTag, "").Replace(sectionEndTag, "");

			#endregion

            #region Bool Section

			if (sectionTag.Trim().Length > 0)
			{
				template = template.Replace(sectionTag, DeleteActionDisabled ? sectionValue : "");
            }

            #endregion

            #endregion
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{Id}}", string.IsNullOrEmpty(Id)==false ? Id : "");
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


	#region Html Template Classes	

    public class TemplateSubscriberView	: ITemplate 
    {
		#region Constants
	
		public const string RelativeFilePath = "App_View/HtmlSubscriber/SubscriberView.html";
		public const string ScriptRelativeFilePath = "";		
		public const string MinScriptRelativeFilePath = "";										
		private const string TemplateSource_524="Fzx1bCBkYXRhLXJvbGU9Imxpc3R2aWV3IoAUC2luc2V0PSJ0cnVlIoARCHRoZW1lPSJhIoAOBmNvbnRlbnTAFgJiIiAgAGBPCWRpdmlkZXJ0aGVALwJlIj4gGAMgPGxpYAdAAAk8c3BhbiBzdHlsIE8fcG9zaXRpb246YWJzb2x1dGU7dG9wOjVweDtyaWdodDoBMTAgCoBDwAAbPGEgaHJlZj0iIyIgb25jbGljaz0icmV0dXJuICAXDnJlc2hTdWJzY3JpYmVyTCDXFih7e1BhZ2VOb319LCB7e0l0ZW1zUGVyQBMAfWARAEQhBwVJbmRleH0gDhYne3tUZW1wbGF0ZVN1ZmZpeH19Jyk7IoDcgSwGYnV0dG9uIoASBmlubGluZT3hAysDbWluaeAEEABpIS4APSCjYJwAIqA3IBMg/Qs9Im5vdGV4dCI+UmVgHgI8L2HhASoAL0Er4AAOFFBsZWFzZSBsb2dpbiB0byBWaWV3IOAB6wIocylBIwk8L2xpPjwvdWw+";

		#endregion		

        #region PlaceHolder Properties

		public string PageNo { get; set; }	
		public string ItemsPerPage { get; set; }	
		public string DataIndex { get; set; }	
		public string TemplateSuffix { get; set; }	

        #endregion		

        #region Validation Method		

		public virtual bool IsValid(bool throwException, out string retMessage)
		{
			retMessage = "";
			return true;
		}


        #endregion

		#region Script Fill Methods

		public virtual string GetScript(bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }

		public virtual string GetScriptFilled(bool includeScriptTag,  bool loadMinIfAvailable, bool validate, bool throwException, out string retMessage)
        {
			throw new NotImplementedException();
        }


		#endregion

        #region Fill Method

		public virtual string GetTemplate(string templateSuffix, bool validate, bool throwException, out string retMessage)
        {
            string message ="";
            string template = "";
			
            if ((HttpBaseHandler.DevelopmentTestMode == false) && (HttpBaseHandler.ProductionTestMode == false))
            {
				if (string.IsNullOrEmpty(template) ==true)
                {
					template = LZF.DecompressFromBase64(TemplateSource_524);
				}
            }
            else
            {
				if (string.IsNullOrEmpty(templateSuffix) ==false)
				{
                    string fileExtension = IOManager.GetExtension(RelativeFilePath);
					string suffixRelativeFilePath = RelativeFilePath.Replace(fileExtension, "." + templateSuffix + fileExtension);
                    template = ResourceUtil.GetTextFromFile(suffixRelativeFilePath, HttpBaseHandler.ResourceCache);
				}
				if (string.IsNullOrEmpty(template) ==true)
				{
					template = ResourceUtil.GetTextFromFile(RelativeFilePath, HttpBaseHandler.ResourceCache);
				}
                if (string.IsNullOrEmpty(template) == true)
                {
					if (string.IsNullOrEmpty(template) == true)
					{
						template = LZF.DecompressFromBase64(TemplateSource_524);
					}
                }
            }
			
            retMessage = message;
			return template;
		}

		public virtual string GetFilled(string templateSuffix, bool validate, bool throwException, out string retMessage)
		{
            string message = "";
			string template = GetTemplate(templateSuffix, validate, throwException, out message);

			if ((string.IsNullOrEmpty(template)==false) && ((validate ==false) || IsValid(throwException, out message)))
            {
				template = ProcessListSection(template);

				template = ProcessBoolSection(template);
			
				template = ProcessPlaceHolder(template);
			}
			retMessage = message;
			return template;
		}

        #endregion

        #region Protected Methods

		protected string ProcessListSection(string template)
		{

			return template;
		}

		protected string ProcessBoolSection(string template)
		{
			
			return template;
		}

		protected string ProcessPlaceHolder(string template)
		{
			template = template.Replace("{{PageNo}}", string.IsNullOrEmpty(PageNo)==false ? PageNo : "");
			template = template.Replace("{{ItemsPerPage}}", string.IsNullOrEmpty(ItemsPerPage)==false ? ItemsPerPage : "");
			template = template.Replace("{{DataIndex}}", string.IsNullOrEmpty(DataIndex)==false ? DataIndex : "");
			template = template.Replace("{{TemplateSuffix}}", string.IsNullOrEmpty(TemplateSuffix)==false ? TemplateSuffix : "");
			return template;
		}

		
        #endregion
    }

	#endregion
	


}

