﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Pb.Wechat.WxMedias
{

    public static class UploadVideo
    {

        public static string GetUploadVideoResult(string accessToken, string filePath, string title, string introduction)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}", accessToken);
            var fileDictionary = new Dictionary<string, string>();
            fileDictionary["media"] = filePath;
            fileDictionary["description"] = string.Format("{{\"title\":\"{0}\", \"introduction\":\"{1}\"}}", title, introduction);

            string returnText = string.Empty;
            Dictionary<string, string> postDataDictionary = null;
            using (MemoryStream ms = new MemoryStream())
            {
                postDataDictionary.FillFormDataStream(ms); //填充formData  
                returnText = HttpPost(url, null, ms, fileDictionary, null, null, 1200000);
            }
            return returnText;
        }

        /// <summary>  
        /// 填充表单信息的Stream  
        /// </summary>  
        /// <param name="formData"></param>  
        /// <param name="stream"></param>  
        public static void FillFormDataStream(this Dictionary<string, string> formData, Stream stream)
        {
            string dataString = GetQueryString(formData);
            var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
            stream.Write(formDataBytes, 0, formDataBytes.Length);
            stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置  
        }

        /// <summary>  
        /// 组装QueryString的方法  
        /// 参数之间用&连接，首位没有符号，如：a=1&b=2&c=3  
        /// </summary>  
        /// <param name="formData"></param>  
        /// <returns></returns>  
        public static string GetQueryString(this Dictionary<string, string> formData)
        {
            if (formData == null || formData.Count == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            var i = 0;
            foreach (var kv in formData)
            {
                i++;
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                if (i < formData.Count)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }

        /// <summary>  
        /// 根据完整文件路径获取FileStream  
        /// </summary>  
        /// <param name="fileName"></param>  
        /// <returns></returns>  
        public static FileStream GetFileStream(string fileName)
        {
            FileStream fileStream = null;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                fileStream = new FileStream(fileName, FileMode.Open);
            }
            return fileStream;
        }

        /// <summary>  
        /// 使用Post方法获取字符串结果  
        /// </summary>  
        /// <param name="url"></param>  
        /// <param name="cookieContainer"></param>  
        /// <param name="postStream"></param>  
        /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>  
        /// <param name="timeOut">超时</param>  
        /// <returns></returns>  
        public static string HttpPost(string url, CookieContainer cookieContainer = null, Stream postStream = null, Dictionary<string, string> fileDictionary = null, string refererUrl = null, Encoding encoding = null, int timeOut = 1200000)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Timeout = timeOut;

            #region 处理Form表单文件上传  
            var formUploadFile = fileDictionary != null && fileDictionary.Count > 0;//是否用Form上传文件  
            if (formUploadFile)
            {
                //通过表单上传文件  
                postStream = postStream ?? new MemoryStream();

                string boundary = "----" + DateTime.Now.Ticks.ToString("x");
                //byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");  
                string fileFormdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
                string dataFormdataTemplate = "\r\n--" + boundary +
                                              "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
                foreach (var file in fileDictionary)
                {
                    try
                    {
                        var fileName = file.Value;

                        //准备文件流  
                        using (var fileStream = GetFileStream(fileName))
                        {
                            string formdata = null;
                            if (fileStream != null)
                            {
                                //存在文件  
                                formdata = string.Format(fileFormdataTemplate, file.Key, /*fileName*/ Path.GetFileName(fileName));
                            }
                            else
                            {
                                //不存在文件或只是注释  
                                formdata = string.Format(dataFormdataTemplate, file.Key, file.Value);
                            }

                            //统一处理  
                            var formdataBytes = Encoding.UTF8.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行  
                            postStream.Write(formdataBytes, 0, formdataBytes.Length);

                            //写入文件  
                            if (fileStream != null)
                            {
                                byte[] buffer = new byte[1024];
                                int bytesRead = 0;
                                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                {
                                    postStream.Write(buffer, 0, bytesRead);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                //结尾  
                var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                postStream.Write(footer, 0, footer.Length);

                request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            }
            else
            {
                request.ContentType = "application/x-www-form-urlencoded";
            }
            #endregion

            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.KeepAlive = true;

            if (!string.IsNullOrEmpty(refererUrl))
            {
                request.Referer = refererUrl;
            }
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }

            #region 输入二进制流  
            if (postStream != null)
            {
                postStream.Position = 0;

                //直接写入流  
                Stream requestStream = request.GetRequestStream();

                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                //debug  
                postStream.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(postStream);
                var postStr = sr.ReadToEnd();
                postStream.Close();//关闭文件访问  
            }
            #endregion

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (cookieContainer != null)
            {
                response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
            }

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.GetEncoding("utf-8")))
                {
                    string retString = myStreamReader.ReadToEnd();
                    return retString;
                }
            }
        }

    }

}
