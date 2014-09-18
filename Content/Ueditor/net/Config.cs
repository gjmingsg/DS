using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.ComponentModel;
using System.Runtime.Serialization;
/// <summary>
/// Config 的摘要说明
/// </summary>
public class Config
{
    public static string AttachmentPath { get { return HttpRuntime.AppDomainAppVirtualPath + ConfigurationManager.AppSettings["AttachmentPath"] + "/"; } }
    public static string[] ImageSavePath = new string[] { AttachmentPath + "img"
        , AttachmentPath + "flash"
        , AttachmentPath + "file" 
        , AttachmentPath + "video" 
    };


}