using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using log4net;


/// <summary>
/// UEditor编辑器通用上传类
/// </summary>
public  class Uploader
{
     string state = "SUCCESS";
     private readonly ILog Logger = LogManager.GetLogger(typeof(Uploader));
     string URL = null;
     string currentType = null;
     string uploadpath = null;
     string filename = null;
     string originalName = null;
     HttpPostedFile uploadFile = null;
     private readonly ILog log = LogManager.GetLogger(typeof(Uploader));
    /**
  * 上传文件的主处理方法
  * @param HttpContext
  * @param string
  * @param  string[]
  *@param int
  * @return Hashtable
  */
    public  Hashtable upFile(HttpContext cxt, string pathbase, string[] filetype, int size)
    {
        pathbase = pathbase + "/";
        uploadpath = cxt.Server.MapPath(pathbase);//获取文件上传路径

        try
        {
            uploadFile = cxt.Request.Files[0];
            originalName = uploadFile.FileName;

            //目录创建
            createFolder();

            //格式验证
            if (checkType(filetype))
            {
                //不允许的文件类型
                state = "不允许的文件类型";//"\u4e0d\u5141\u8bb8\u7684\u6587\u4ef6\u7c7b\u578b";
            }
            //大小验证
            if (checkSize(size))
            {
                //文件大小超出网站限制
                state = "文件大小超出网站限制";//"\u6587\u4ef6\u5927\u5c0f\u8d85\u51fa\u7f51\u7ad9\u9650\u5236";
            }
            //保存图片
            if (state == "SUCCESS")
            {
                filename = NameFormater.Format(cxt.Request["fileNameFormat"], originalName);
                var testname = filename;
                var ai = 1;
                while (File.Exists(uploadpath + testname))
                {
                    testname =  Path.GetFileNameWithoutExtension(filename) + "_" + ai++ + Path.GetExtension(filename); 
                }
                uploadFile.SaveAs(uploadpath + testname);
                ThumbPic(uploadpath, testname, 200, 179, true);
                URL = pathbase + testname;
            }
        }
        catch (Exception)
        {
            // 未知错误
            state = "未知错误";//"\u672a\u77e5\u9519\u8bef";
            URL = "";
        }
        return getUploadInfo();
    }

    /**
 * 上传涂鸦的主处理方法
  * @param HttpContext
  * @param string
  * @param  string[]
  *@param string
  * @return Hashtable
 */
    public  Hashtable upScrawl(HttpContext cxt, string pathbase, string tmppath, string base64Data)
    {
        pathbase = pathbase + DateTime.Now.ToString("yyyy-MM-dd") + "/";
        uploadpath = cxt.Server.MapPath(pathbase);//获取文件上传路径
        FileStream fs = null;
        try
        {
            //创建目录
            createFolder();
            //生成图片
            filename = System.Guid.NewGuid() + ".png";
            fs = File.Create(uploadpath + filename);
            byte[] bytes = Convert.FromBase64String(base64Data);
            fs.Write(bytes, 0, bytes.Length);

            URL = pathbase + filename;
        }
        catch (Exception e)
        {
            log.Error("文件错误",e);
            state = "未知错误";
            URL = "";
        }
        finally
        {
            fs.Close();
            deleteFolder(cxt.Server.MapPath(tmppath));
        }
        return getUploadInfo();
    }

    /**
* 获取文件信息
* @param context
* @param string
* @return string
*/
    public  string getOtherInfo(HttpContext cxt, string field)
    {
        string info = null;
        if (cxt.Request.Form[field] != null && !String.IsNullOrEmpty(cxt.Request.Form[field]))
        {
            info = field == "fileName" ? cxt.Request.Form[field].Split(',')[1] : cxt.Request.Form[field];
        }
        return info;
    }

    /**
     * 获取上传信息
     * @return Hashtable
     */
    private  Hashtable getUploadInfo()
    {
        Hashtable infoList = new Hashtable();

        infoList.Add("state", state);
        infoList.Add("url", URL);

        if (currentType != null)
            infoList.Add("currentType", currentType);
        if (originalName != null)
            infoList.Add("originalName", originalName);
        return infoList;
    }

    /**
     * 重命名文件
     * @return string
     */
    private  string reName()
    {
        return System.Guid.NewGuid() + getFileExt();
    }

    /**
     * 文件类型检测
     * @return bool
     */
    private  bool checkType(string[] filetype)
    {
        currentType = getFileExt();
        return Array.IndexOf(filetype, currentType) == -1;
    }

    /**
     * 文件大小检测
     * @param int
     * @return bool
     */
    private  bool checkSize(int size)
    {
        return uploadFile.ContentLength >= (size * 1024*1024);
    }

    /**
     * 获取文件扩展名
     * @return string
     */
    private  string getFileExt()
    {
        string[] temp = uploadFile.FileName.Split('.');
        return "." + temp[temp.Length - 1].ToLower();
    }

    /**
     * 按照日期自动创建存储文件夹
     */
    private  void createFolder()
    {
        if (!Directory.Exists(uploadpath))
        {
            Directory.CreateDirectory(uploadpath);
        }
    }

    /**
     * 删除存储文件夹
     * @param string
     */
    public  void deleteFolder(string path)
    {
        //if (Directory.Exists(path))
        //{
        //    Directory.Delete(path, true);
        //}
    }

    
    /// <summary>
    /// 缩略图处理
    /// </summary>
    /// <param name="StrPath">物理目录</param>
    /// <param name="FirstUrl">原图片路径</param>
    /// <param name="iWidth">缩略图宽度</param>
    /// <param name="iHeigth">缩略图高度</param>
    /// <param name="strLocation">缩略图片路径</param>
    /// <param name="bRate">是否按比例缩放</param>
    /// <returns></returns>
    public string ThumbPic(string StrPath, string FirstUrl, int iWidth, int iHeigth, bool bRate)
    {
        System.Drawing.Image oImage = null; //原图
        System.Drawing.Bitmap tImage = null;//缩略图
        System.Drawing.Graphics gx = null;
        int oWidth = 0;
        int oHeight = 0;
        int tWidth = iWidth;
        int tHeight = iHeigth;
        String filePath = string.Empty;
        try
        {
            //读取原图
            oImage = System.Drawing.Image.FromFile(StrPath + FirstUrl);
            oWidth = oImage.Width; //原图宽度 
            oHeight = oImage.Height; //原图高度 

            //按比例缩放
            if (bRate)
            {
                //按比例计算出缩略图的宽度和高度 
                if (oWidth >= oHeight)
                    tHeight = (int)Math.Floor(Convert.ToDouble(oHeight) * (Convert.ToDouble(tWidth) / Convert.ToDouble(oWidth)));
                else
                    tWidth = (int)Math.Floor(Convert.ToDouble(oWidth) * (Convert.ToDouble(tHeight) / Convert.ToDouble(oHeight)));
            }

            //生成缩略原图 
            tImage = new System.Drawing.Bitmap(tWidth, tHeight);
            gx = System.Drawing.Graphics.FromImage(tImage);
            gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High; //设置高质量插值法 
            gx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置高质量,低速度呈现平滑程度 
            gx.Clear(System.Drawing.Color.Transparent); //清空画布并以透明背景色填充 
            gx.DrawImage(oImage, new System.Drawing.Rectangle(0, 0, tWidth, tHeight), new System.Drawing.Rectangle(0, 0, oWidth, oHeight), System.Drawing.GraphicsUnit.Pixel);
            filePath = FirstUrl.Substring(0, FirstUrl.LastIndexOf('.')) + "_small" + FirstUrl.Substring(FirstUrl.LastIndexOf('.'));
            Logger.Info("xxx==========================================================");
            Logger.Info(FirstUrl);
            Logger.Info(filePath);
            Logger.Info(StrPath + filePath);
            Logger.Info("==========================================================");
            //先判断缩略图片路径是否存在，若存在则不保存
            if (!File.Exists(StrPath + filePath))
            {
                //以JPG格式保存图片 
                tImage.Save(StrPath + filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        catch (System.Exception ex)
        {
            Logger.Error("图片处理失败", ex);
            return "";
        }
        finally
        {
            //释放资源 
            oImage.Dispose();
            gx.Dispose();
            tImage.Dispose();
        }
        return filePath;
    }
}


public static class NameFormater
{
    public static string Format(string format, string filename)
    {
        if (String.IsNullOrEmpty(format))
        {
            format = "{filename}{rand:6}";
        }
        string ext = Path.GetExtension(filename);
        filename = Path.GetFileNameWithoutExtension(filename);
        format = format.Replace("{filename}", filename);
        format = new Regex(@"\{rand(\:?)(\d+)\}", RegexOptions.Compiled).Replace(format, new MatchEvaluator(delegate(Match match)
        {
            var digit = 6;
            if (match.Groups.Count > 2)
            {
                digit = Convert.ToInt32(match.Groups[2].Value);
            }
            var rand = new Random();
            return rand.Next((int)Math.Pow(10, digit), (int)Math.Pow(10, digit + 1)).ToString();
        }));
        format = format.Replace("{time}", DateTime.Now.Ticks.ToString());
        format = format.Replace("{yyyy}", DateTime.Now.Year.ToString());
        format = format.Replace("{yy}", (DateTime.Now.Year % 100).ToString("D2"));
        format = format.Replace("{mm}", DateTime.Now.Month.ToString("D2"));
        format = format.Replace("{dd}", DateTime.Now.Day.ToString("D2"));
        format = format.Replace("{hh}", DateTime.Now.Hour.ToString("D2"));
        format = format.Replace("{ii}", DateTime.Now.Minute.ToString("D2"));
        format = format.Replace("{ss}", DateTime.Now.Second.ToString("D2"));
        var invalidPattern = new Regex(@"[\\\/\:\*\?\042\<\>\|]");
        format = invalidPattern.Replace(format, "");
        return format + ext;
    }
}