using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.IO.Extensions;
using Abp.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pb.Wechat.MpArticleInsideImages;
using Pb.Wechat.Url;
using Pb.Wechat.UserMps;
using Pb.Wechat.Web.Resources.FileServers;
using Pb.Wechat.Web.Resources.WxMediaHelper;
using Pb.Wechat.WxMedias;

namespace Pb.Wechat.Web.Controllers
{
    [IgnoreAntiforgeryToken]
    public class KindEditorController : AbpZeroTemplateControllerBase
    {

        //�ļ�����Ŀ¼·��
        string SavePath = "";
        private IHostingEnvironment host = null;
        private readonly IWxMediaAppService _wxMediaAppService;
        private readonly IMpArticleInsideImageAppService _mpArticleInsideImageAppService;
        private readonly IUserMpAppService _userMpAppService;
        private readonly IFileServer _fileServer;
        private readonly IWxMediaUpload _wxMediaUpload;
        private readonly IWebUrlService _webUrlService;
        public KindEditorController(IHostingEnvironment _host
            , IWxMediaAppService wxMediaAppService
            , IMpArticleInsideImageAppService mpArticleInsideImageAppService
            , IUserMpAppService userMpAppService
            , IFileServer fileServer
            , IWxMediaUpload wxMediaUpload
            ,IWebUrlService webUrlService)
        {
            host = _host;
            _wxMediaAppService = wxMediaAppService;
            _mpArticleInsideImageAppService = mpArticleInsideImageAppService;
            _userMpAppService = userMpAppService;
            _fileServer = fileServer;
            _wxMediaUpload = wxMediaUpload;
            _webUrlService = webUrlService;
            SavePath = _webUrlService.KindEditorSavePath;
        }
        #region uploadJson

        //
        // GET: /KindEditorHandler/Upload
        [DontWrapResult]
        public async Task<ActionResult> Upload()
        {
            ////�ļ�����Ŀ¼·��
            //const string savePath = "/Content/Uploads/";

            //�ļ�����Ŀ¼URL
            //var saveUrl = SavePath;

            //���������ϴ����ļ���չ��
            var extTable = new Hashtable
                               {
                                    {"image", "gif,jpg,jpeg,png,bmp"},
                                    {"flash", "swf,flv"},
                                   {"media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb"},
                                   {"file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2"}
                                };

            //����ļ���С
            const int maxSize = 2000000;

            var imgFile = Request.Form.Files["imgFile"];

            if (imgFile == null)
            {
                return ShowError("��ѡ���ļ���");
            }
            //var dirPath = host.WebRootPath + SavePath.Replace("/", @"\");

            //if (!Directory.Exists(dirPath))
            //{
              
            //    Directory.CreateDirectory(dirPath);
            //}
            //var query = Request.Query;
            //var value = Microsoft.Extensions.Primitives.StringValues.Empty;
            //query.TryGetValue("dir", out value);
            //var path = value.ToString();
            //var dirName = value.ToString();
       
            //if (String.IsNullOrEmpty(dirName))
            //{
            //    dirName = "image";
            //}

            //if (!extTable.ContainsKey(dirName))
            //{
            //    return ShowError("Ŀ¼������ȷ��");
            //}

            var fileName = imgFile.FileName;
            var extension = Path.GetExtension(fileName);
            if (extension == null)
            {
                return ShowError("extension == null");
            }

            var fileExt = extension.ToLower();
            byte[] fileBytes;
            using (var stream = imgFile.OpenReadStream())
            {
                if (stream == null || stream.Length > maxSize)
                    return ShowError("�ϴ��ļ���С�������ơ�");
                fileBytes = stream.GetAllBytes();
            }

            //if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
            //{
            //    return ShowError("�ϴ��ļ���С�������ơ�");
            //}

            //if (String.IsNullOrEmpty(fileExt) ||
            //   Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
            //{
            //    return ShowError("�ϴ��ļ���չ���ǲ��������չ����\nֻ����" + ((String)extTable[dirName]) + "��ʽ��");
            //}

            ////�����ļ���
            //dirPath += dirName + "/";
            //saveUrl += dirName + "/";
            //if (!Directory.Exists(dirPath))
            //{
            //    Directory.CreateDirectory(dirPath);
            //}
            //var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            //dirPath += ymd + "/";
            //saveUrl += ymd + "/";
            //if (!Directory.Exists(dirPath))
            //{
            //    Directory.CreateDirectory(dirPath);
            //}

            //var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            //var filePath = dirPath + newFileName;
            //System.IO.File.WriteAllBytes(filePath, fileBytes);

            //var fileUrl = saveUrl + newFileName;
          
            var guid = Request.Query["articleGuid"];
            var mpid = await _userMpAppService.GetDefaultMpId();
            var extra = fileExt.Substring(fileExt.IndexOf(".") + 1);
            var url =await _fileServer.UploadFile(fileBytes, extra, "KindEditor");
           

            var wxurl = await _wxMediaUpload.UploadArtImageAndGetMediaID(mpid, url, MpEvents.Dto.MpMessageType.image);
            await _mpArticleInsideImageAppService.Create(new MpArticleInsideImages.Dto.MpArticleInsideImageDto {ArticleGrid=guid,LocalImageUrl=url,MpID= mpid,WxImageUrl= wxurl });
            
            var hash = new Hashtable();
            hash["error"] = 0;
            hash["url"] = url;

            return Json(hash);
        }

        private JsonResult ShowError(string message)
        {
            var hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;

            return Json(hash);
        }

        #endregion

        #region fileManagerJson

        //
        // GET: /KindEditorHandler/FileManager
        [HttpGet]
        [DontWrapResult]
        public ActionResult FileManager()
        {
            ////��Ŀ¼·�������·��
            //String rootPath = "/Content/Uploads/";

            //��Ŀ¼URL������ָ������·�������� http://www.yoursite.com/attached/
            var rootUrl = SavePath;

            //ͼƬ��չ��
            const string fileTypes = "gif,jpg,jpeg,png,bmp";

            String currentPath;
            String currentUrl;
            String currentDirPath;
            String moveupDirPath;
            var dirPath = host.WebRootPath + SavePath.Replace("/", @"\");

            var query = Request.Query;

            var value = Microsoft.Extensions.Primitives.StringValues.Empty;
            query.TryGetValue("dir", out value);
            var dirName = value.ToString();
            if (!String.IsNullOrEmpty(dirName))
            {
                if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
                {
                    return Content("Invalid Directory name.");
                }
                dirPath += dirName + "/";
                rootUrl += dirName + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
            }

            //����path���������ø�·����URL
            var value2 = Microsoft.Extensions.Primitives.StringValues.Empty;
            query.TryGetValue("path", out value2);
            var path = value2.ToString();

            path = String.IsNullOrEmpty(path) ? "" : path;
            if (path == "")
            {
                currentPath = dirPath;
                currentUrl = rootUrl;
                currentDirPath = "";
                moveupDirPath = "";
            }
            else
            {
                currentPath = dirPath + path;
                currentUrl = rootUrl + path;
                currentDirPath = path;
                moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
            }

            //������ʽ��name or size or type

            var value3 = Microsoft.Extensions.Primitives.StringValues.Empty;
            query.TryGetValue("order", out value3);
            var order = value3.ToString();

            //String order = Request.QueryString["order"];
            order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

            //������ʹ��..�ƶ�����һ��Ŀ¼
            if (Regex.IsMatch(path, @"\.\."))
            {
                return Content("Access is not allowed.");
            }

            //���һ���ַ�����/
            if (path != "" && !path.EndsWith("/"))
            {
                return Content("Parameter is not valid.");
            }
            //Ŀ¼�����ڻ���Ŀ¼
            if (!Directory.Exists(currentPath))
            {
                return Content("Directory does not exist.");
            }

            //����Ŀ¼ȡ���ļ���Ϣ
            string[] dirList = Directory.GetDirectories(currentPath);
            string[] fileList = Directory.GetFiles(currentPath);

            switch (order)
            {
                case "size":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new SizeSorter());
                    break;
                case "type":
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new TypeSorter());
                    break;
                default:
                    Array.Sort(dirList, new NameSorter());
                    Array.Sort(fileList, new NameSorter());
                    break;
            }

            var result = new Hashtable();
            result["moveup_dir_path"] = moveupDirPath;
            result["current_dir_path"] = currentDirPath;
            result["current_url"] = currentUrl;
            result["total_count"] = dirList.Length + fileList.Length;
            var dirFileList = new List<Hashtable>();
            result["file_list"] = dirFileList;
            foreach (var t in dirList)
            {
                var dir = new DirectoryInfo(t);
                var hash = new Hashtable();
                hash["is_dir"] = true;
                hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
                hash["filesize"] = 0;
                hash["is_photo"] = false;
                hash["filetype"] = "";
                hash["filename"] = dir.Name;
                hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }
            foreach (var t in fileList)
            {
                var file = new FileInfo(t);
                var hash = new Hashtable();
                hash["is_dir"] = false;
                hash["has_file"] = false;
                hash["filesize"] = file.Length;
                hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
                hash["filetype"] = file.Extension.Substring(1);
                hash["filename"] = file.Name;
                hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dirFileList.Add(hash);
            }

            return Json(result);
        }


        private class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                var xInfo = new FileInfo(x.ToString());
                var yInfo = new FileInfo(y.ToString());

                return String.CompareOrdinal(xInfo.FullName, yInfo.FullName);
            }
        }

        private class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                var xInfo = new FileInfo(x.ToString());
                var yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        private class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                var xInfo = new FileInfo(x.ToString());
                var yInfo = new FileInfo(y.ToString());

                return String.CompareOrdinal(xInfo.Extension, yInfo.Extension);
            }
        }

        #endregion
    }
}